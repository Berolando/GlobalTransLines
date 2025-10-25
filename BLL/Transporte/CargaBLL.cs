using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE;
using BE.Enums;
using BE.Transversal;
using MPP.Cargas;
using BLL.Transversal;

namespace BLL.Transporte
{
    public class CargaBLL : IGestor<BECarga>
    {
        private CargaMPP cargaMPP = new CargaMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir una carga por Id
        static CargaBLL()
        {
            ReconstructorBLL.Registrar<BECarga>(
                id => new CargaBLL().ListarObjeto(new BECarga { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BECarga obj)
        {
            try
            {
                // Validaciones
                ValidadorBLL.ObjetoNoNulo(obj, "Carga");
                ValidadorBLL.CampoObligatorio(obj.Descripcion, "Descripción de la carga");
                ValidadorBLL.ObjetoNoNulo(obj.Origen, "Dirección de origen");
                ValidadorBLL.ObjetoNoNulo(obj.Destino, "Dirección de destino");

                if (obj.Peso <= 0)
                    throw new Exception("El peso de la carga debe ser mayor a cero.");

                if (obj.Volumen <= 0)
                    throw new Exception("El volumen de la carga debe ser mayor a cero.");

                if (obj.FechaEntregaEstimada < obj.FechaRegistro)
                    throw new Exception("La fecha estimada de entrega no puede ser anterior a la fecha de registro.");

                if (obj.Id == 0)
                    cargaMPP.Alta(obj);
                else
                    cargaMPP.Modificar(obj);

                ReconstructorBLL.LimpiarCache<BECarga>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar carga.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BECarga obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Carga");
                cargaMPP.Baja(obj);

                ReconstructorBLL.LimpiarCache<BECarga>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar carga.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BECarga> ListarTodo()
        {
            try
            {
                var lista = cargaMPP.Listar();

                // Reconstrucción de relaciones Origen y Destino
                foreach (var c in lista)
                {
                    if (c.Origen != null && c.Origen.Id > 0)
                        c.Origen = ReconstructorBLL.Reconstruir<BEDireccion>(c.Origen.Id);

                    if (c.Destino != null && c.Destino.Id > 0)
                        c.Destino = ReconstructorBLL.Reconstruir<BEDireccion>(c.Destino.Id);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar cargas.", ex);
            }
        }

        public BECarga ListarObjeto(BECarga obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Carga");

                var carga = cargaMPP.BuscarPorId(obj.Id);

                if (carga != null)
                {
                    if (carga.Origen != null && carga.Origen.Id > 0)
                        carga.Origen = ReconstructorBLL.Reconstruir<BEDireccion>(carga.Origen.Id);

                    if (carga.Destino != null && carga.Destino.Id > 0)
                        carga.Destino = ReconstructorBLL.Reconstruir<BEDireccion>(carga.Destino.Id);
                }

                return carga;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar carga.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return cargaMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de carga.", ex);
            }
        }

        #endregion
    }
}
