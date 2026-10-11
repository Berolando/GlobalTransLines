using BE.Infra;
using Transversales;
using System;
using System.IO;
using MPP.Infra;
using System.Linq;

namespace BLL.Infra
{
    public static class InicializadorSistema
    {
        #region "Estructura base del sistema"

        // Prepara los XML mínimos para que el sistema pueda iniciar aunque no haya datos cargados.
        public static void CrearEstructuraBase()
        {
            try
            {
                // Entidades con XML propio.
                GeneradorRutas.UbicacionEntidad<BEUsuario>();
                GeneradorRutas.UbicacionEntidad<BERol>();
                GeneradorRutas.UbicacionEntidad<BEPermiso>();
                GeneradorRutas.UbicacionEntidad<BEDireccion>();
                GeneradorRutas.UbicacionEntidad<BEEvento>();
                GeneradorRutas.UbicacionEntidad<BEBitacora>();
                GeneradorRutas.UbicacionEntidad<BEBackup>();

                // Relaciones de seguridad del patrón Composite.
                new CompositeMPP();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso a los archivos XML base.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura de los XML base. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error general al crear la estructura XML base.", ex);
            }
        }

        #endregion

        #region "Inicialización de Reconstructores"

        public static void InicializarReconstructores()
        {
            try
            {
                // Al instanciar estas BLL se registran sus reconstructores para poder resolver objetos por Id.
                _ = new DireccionBLL();
                _ = new BLL.Personas.ClienteBLL();
                _ = new BLL.Transporte.CargaBLL();
                _ = new BLL.ServiciosSubcontratados.ServicioSubcontratadoBLL();
                _ = new BLL.Finanzas.CotizacionBLL();
                _ = new BLL.Transporte.BookingBLL();
                _ = new BLL.Personas.ProveedorBLL();
                _ = new BLL.Transporte.ContenedorBLL();
                _ = new RolBLL();
                _ = new PermisoBLL();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso a los XML al inicializar reconstructores.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura XML al inicializar reconstructores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error general al inicializar reconstructores.", ex);
            }
        }

        #endregion

        #region "Inicialización de datos base"

        public static void InicializarDatosBase()
        {
            try
            {
                // Carga permisos y roles mínimos necesarios para operar el sistema.
                var permisoBLL = new PermisoBLL();
                permisoBLL.InicializarPermisos();

                InicializarRolAdministrador();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar datos base del sistema.", ex);
            }
        }

        private static void InicializarRolAdministrador()
        {
            var rolMPP = new RolMPP();

            // Evita duplicar el rol base si el sistema ya fue inicializado antes.
            if (rolMPP.Listar().Any(r =>
                r.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase)))
                return;

            rolMPP.Alta(new BERol
            {
                Nombre = "Administrador",
                Descripcion = "Rol con acceso total al sistema",
                Activo = true
            });
        }

        #endregion
    }
}
