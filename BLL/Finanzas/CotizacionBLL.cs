using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE;
using BE.Finanzas;
using BE.Enums;
using MPP.Finanzas;
using BLL.Transversal;

namespace BLL.Finanzas
{
    public class CotizacionBLL : IGestor<BECotizacion>
    {
        private CotizacionMPP cotizacionMPP = new CotizacionMPP();

        #region "Constructor estático"

        // Registro para reconstrucción dinámica de Cotizaciones
        static CotizacionBLL()
        {
            ReconstructorBLL.Registrar<BECotizacion>(
                id => new CotizacionBLL().ListarObjeto(new BECotizacion { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BECotizacion obj)
        {
            try
            {
                // Validaciones obligatorias
                ValidadorBLL.ObjetoNoNulo(obj, "Cotización");
                ValidadorBLL.ObjetoNoNulo(obj.Cliente, "Cliente asociado");
                ValidadorBLL.ListaNoVacia(obj.Cargas, "Cargas");
                ValidadorBLL.ListaNoVacia(obj.Servicios, "Servicios subcontratados");

                if (obj.MontoTotal <= 0)
                    throw new Exception("El monto total debe ser mayor a cero.");

                if (obj.FechaVencimiento <= obj.FechaEmision)
                    throw new Exception("La fecha de vencimiento debe ser posterior a la de emisión.");

                if (obj.Estado == 0)
                    obj.Estado = EstadoCotizacionEnum.Pendiente;

                // Guardar o modificar
                if (obj.Id == 0)
                    cotizacionMPP.Alta(obj);
                else
                    cotizacionMPP.Modificar(obj);

                // Limpieza de cache
                ReconstructorBLL.LimpiarCache<BECotizacion>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar cotización.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BECotizacion obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Cotización");
                cotizacionMPP.Baja(obj);

                ReconstructorBLL.LimpiarCache<BECotizacion>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cotización.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BECotizacion> ListarTodo()
        {
            try
            {
                var lista = cotizacionMPP.Listar();

                foreach (var c in lista)
                {
                    if (c.Cliente != null && c.Cliente.Id > 0)
                        c.Cliente = ReconstructorBLL.Reconstruir<BECliente>(c.Cliente.Id);

                    if (c.Cargas != null && c.Cargas.Count > 0)
                        c.Cargas = c.Cargas
                            .Select(x => ReconstructorBLL.Reconstruir<BECarga>(x.Id))
                            .Where(x => x != null)
                            .ToList();

                    if (c.Servicios != null && c.Servicios.Count > 0)
                        c.Servicios = c.Servicios
                            .Select(x => ReconstructorBLL.Reconstruir<BEServicioSubcontratado>(x.Id))
                            .Where(x => x != null)
                            .ToList();
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar cotizaciones.", ex);
            }
        }

        public BECotizacion ListarObjeto(BECotizacion obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Cotización");

                var cot = cotizacionMPP.BuscarPorId(obj.Id);

                if (cot != null)
                {
                    if (cot.Cliente != null && cot.Cliente.Id > 0)
                        cot.Cliente = ReconstructorBLL.Reconstruir<BECliente>(cot.Cliente.Id);

                    if (cot.Cargas != null && cot.Cargas.Count > 0)
                        cot.Cargas = cot.Cargas
                            .Select(x => ReconstructorBLL.Reconstruir<BECarga>(x.Id))
                            .Where(x => x != null)
                            .ToList();

                    if (cot.Servicios != null && cot.Servicios.Count > 0)
                        cot.Servicios = cot.Servicios
                            .Select(x => ReconstructorBLL.Reconstruir<BEServicioSubcontratado>(x.Id))
                            .Where(x => x != null)
                            .ToList();
                }

                return cot;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cotización.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return cotizacionMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de cotización.", ex);
            }
        }

        #endregion
    }
}
