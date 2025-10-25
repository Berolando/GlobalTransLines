using System;
using System.Collections.Generic;
using Abstraccion;
using BE.Transversal;
using MPP.Transversal;
using BLL.Transversal;
using BE;
using MPP.Personas;

namespace BLL.Transversal
{
    public class DireccionBLL : IGestor<BEDireccion>
    {
        private DireccionMPP direccionMPP = new DireccionMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir una dirección por Id
        static DireccionBLL()
        {
            ReconstructorBLL.Registrar<BEDireccion>(
                id => new DireccionBLL().ListarObjeto(new BEDireccion { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        // Alta o modificación
        public void Guardar(BEDireccion dir)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(dir, "Dirección");
                ValidadorBLL.CampoObligatorio(dir.Calle, "Calle");
                ValidadorBLL.CampoObligatorio(dir.Ciudad, "Ciudad");
                ValidadorBLL.CampoObligatorio(dir.Pais, "País");

                if (dir.Id == 0)
                    direccionMPP.Alta(dir);
                else
                    direccionMPP.Modificar(dir);

                // Limpia cache de direcciones
                ReconstructorBLL.LimpiarCache<BEDireccion>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar dirección.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        // Elimina dirección
        public void Eliminar(BEDireccion dir)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(dir, "Dirección");
                direccionMPP.Baja(dir);

                // Limpia cache
                ReconstructorBLL.LimpiarCache<BEDireccion>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar dirección.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        // Lista todas las direcciones
        public List<BEDireccion> ListarTodo()
        {
            try
            {
                return direccionMPP.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar direcciones.", ex);
            }
        }

        // Busca una dirección por Id
        public BEDireccion ListarObjeto(BEDireccion dir)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(dir, "Dirección");
                return direccionMPP.BuscarPorId(dir.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar dirección.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        // Devuelve el último Id
        public long ObtenerUltimoId()
        {
            try
            {
                return direccionMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de dirección.", ex);
            }
        }

        #endregion
    }
}
