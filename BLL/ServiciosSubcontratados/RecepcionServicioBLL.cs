using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE.ServiciosSubcontratados;
using BE.Finanzas;
using MPP.ServiciosSubcontratados;
using BLL.Transversal;
using BE;

namespace BLL.ServiciosSubcontratados
{
    public class RecepcionServicioBLL : IGestor<BERecepcionServicio>
    {
        private RecepcionServicioMPP recepcionMPP = new RecepcionServicioMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir una recepción de servicio por Id
        static RecepcionServicioBLL()
        {
            ReconstructorBLL.Registrar<BERecepcionServicio>(
                id => new RecepcionServicioBLL().ListarObjeto(new BERecepcionServicio { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BERecepcionServicio obj)
        {
            try
            {
                // Validaciones de negocio
                ValidadorBLL.ObjetoNoNulo(obj, "Recepción de servicio");
                ValidadorBLL.ObjetoNoNulo(obj.Servicio, "Servicio subcontratado asociado");
                ValidadorBLL.ObjetoNoNulo(obj.OrdenDeCompra, "Orden de compra asociada");

                if (obj.FechaRecepcion > DateTime.Now)
                    throw new Exception("La fecha de recepción no puede ser futura.");

                if (string.IsNullOrWhiteSpace(obj.Observaciones))
                    obj.Observaciones = "Sin observaciones.";

                if (obj.Id == 0)
                    recepcionMPP.Alta(obj);
                else
                    recepcionMPP.Modificar(obj);

                // Limpieza del cache
                ReconstructorBLL.LimpiarCache<BERecepcionServicio>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar recepción de servicio.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BERecepcionServicio obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Recepción de servicio");
                recepcionMPP.Baja(obj);

                ReconstructorBLL.LimpiarCache<BERecepcionServicio>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar recepción de servicio.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BERecepcionServicio> ListarTodo()
        {
            try
            {
                var lista = recepcionMPP.Listar();

                // Reconstrucción de relaciones por Id
                foreach (var r in lista)
                {
                    if (r.Servicio != null && r.Servicio.Id > 0)
                        r.Servicio = ReconstructorBLL.Reconstruir<BEServicioSubcontratado>(r.Servicio.Id);

                    if (r.OrdenDeCompra != null && r.OrdenDeCompra.Id > 0)
                        r.OrdenDeCompra = ReconstructorBLL.Reconstruir<BEOrdenDeCompra>(r.OrdenDeCompra.Id);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar recepciones de servicio.", ex);
            }
        }

        public BERecepcionServicio ListarObjeto(BERecepcionServicio obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Recepción de servicio");

                var recepcion = recepcionMPP.BuscarPorId(obj.Id);

                if (recepcion != null)
                {
                    if (recepcion.Servicio != null && recepcion.Servicio.Id > 0)
                        recepcion.Servicio = ReconstructorBLL.Reconstruir<BEServicioSubcontratado>(recepcion.Servicio.Id);

                    if (recepcion.OrdenDeCompra != null && recepcion.OrdenDeCompra.Id > 0)
                        recepcion.OrdenDeCompra = ReconstructorBLL.Reconstruir<BEOrdenDeCompra>(recepcion.OrdenDeCompra.Id);
                }

                return recepcion;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar recepción de servicio.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return recepcionMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de recepción de servicio.", ex);
            }
        }

        #endregion
    }
}
