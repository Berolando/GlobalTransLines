using Abstraccion;
using BE.Infra;
using MPP.Personas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Infra
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

     
        public void Guardar(BEDireccion dir)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(dir, "Dirección");
                if (string.IsNullOrWhiteSpace(dir.Puerto))
                {
                    ValidadorBLL.CampoObligatorio(dir.Calle, "Calle");
                    ValidadorBLL.CampoObligatorio(dir.Ciudad, "Ciudad");
                    ValidadorBLL.CampoObligatorio(dir.Pais, "País");
                }
                else
                {
                    // Si hay puerto, valida solo que no sea vacío
                    ValidadorBLL.CampoObligatorio(dir.Puerto, "Puerto");
                }

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

        #region "Guardar Puerto"

        public void GuardarPuerto(BEDireccion puerto)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(puerto, "Puerto");
                ValidadorBLL.CampoObligatorio(puerto.Puerto, "Puerto");
                ValidadorBLL.CampoObligatorio(puerto.Pais, "País");
                ValidadorBLL.CampoObligatorio(puerto.Ciudad, "Ciudad");

                puerto.Tipo = "PUERTO";
                puerto.Calle = string.Empty;
                puerto.Numero = string.Empty;
                puerto.CodigoPostal = string.Empty;

                Guardar(puerto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar puerto.", ex);
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

        public List<BEDireccion> ListarPuertos()
        {
            var direcciones = ListarTodo();
            bool existeCatalogoTipado = direcciones.Any(d =>
                string.Equals(d.Tipo, "PUERTO", StringComparison.OrdinalIgnoreCase));

            return direcciones
                .Where(d => existeCatalogoTipado
                    ? string.Equals(d.Tipo, "PUERTO", StringComparison.OrdinalIgnoreCase)
                    : d.EsPuerto)
                .OrderBy(d => d.Pais)
                .ThenBy(d => d.Puerto)
                .ToList();
        }

        public List<BEDireccion> ListarPuertosArgentinos()
        {
            return ListarPuertos()
                .Where(d => string.Equals(d.Pais, "Argentina", StringComparison.OrdinalIgnoreCase))
                .ToList();
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
