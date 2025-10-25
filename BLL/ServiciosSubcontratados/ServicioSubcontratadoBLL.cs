using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE;
using MPP.ServiciosSubcontratados;
using BLL.Transversal;

namespace BLL.ServiciosSubcontratados
{
    public class ServicioSubcontratadoBLL : IGestor<BEServicioSubcontratado>
    {
        private ServicioSubcontratadoMPP servicioMPP = new ServicioSubcontratadoMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir un servicio subcontratado por Id
        static ServicioSubcontratadoBLL()
        {
            ReconstructorBLL.Registrar<BEServicioSubcontratado>(
                id => new ServicioSubcontratadoBLL().ListarObjeto(new BEServicioSubcontratado { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEServicioSubcontratado obj)
        {
            try
            {
                // Validaciones
                ValidadorBLL.ObjetoNoNulo(obj, "Servicio subcontratado");
                ValidadorBLL.CampoObligatorio(obj.Nombre, "Nombre del servicio");
                ValidadorBLL.CampoObligatorio(obj.Descripcion, "Descripción del servicio");
                ValidadorBLL.ObjetoNoNulo(obj.Proveedor, "Proveedor asociado");

                if (obj.Costo <= 0)
                    throw new Exception("El costo del servicio debe ser mayor a cero.");

                if (obj.Id == 0)
                    servicioMPP.Alta(obj);
                else
                    servicioMPP.Modificar(obj);

                // Limpieza de cache
                ReconstructorBLL.LimpiarCache<BEServicioSubcontratado>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar servicio subcontratado.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEServicioSubcontratado obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Servicio subcontratado");
                servicioMPP.Baja(obj);

                ReconstructorBLL.LimpiarCache<BEServicioSubcontratado>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar servicio subcontratado.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEServicioSubcontratado> ListarTodo()
        {
            try
            {
                var lista = servicioMPP.Listar();

                // Reconstrucción de relación con proveedor
                foreach (var s in lista)
                {
                    if (s.Proveedor != null && s.Proveedor.Id > 0)
                        s.Proveedor = ReconstructorBLL.Reconstruir<BEProveedor>(s.Proveedor.Id);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar servicios subcontratados.", ex);
            }
        }

        public BEServicioSubcontratado ListarObjeto(BEServicioSubcontratado obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Servicio subcontratado");

                var servicio = servicioMPP.BuscarPorId(obj.Id);

                if (servicio != null && servicio.Proveedor != null && servicio.Proveedor.Id > 0)
                    servicio.Proveedor = ReconstructorBLL.Reconstruir<BEProveedor>(servicio.Proveedor.Id);

                return servicio;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar servicio subcontratado.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return servicioMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de servicio subcontratado.", ex);
            }
        }

        #endregion
    }
}
