using BE.Dominio;
using BE.Infra;
using Seguridad;
using System;
using System.Linq;

namespace BLL.Infra
{
    public class SeguridadBLL : ISeguridad
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();
        private ComponentBLL compositeBLL = new ComponentBLL();
        private BitacoraBLL bitacoraBLL = new BitacoraBLL();

        #region "Propiedades"

        // Mantiene en memoria el usuario autenticado durante la sesión actual.
        private BEUsuario usuarioActual;
        public string NombreUsuario { get; private set; }

        public BEUsuario UsuarioActual => usuarioActual;
        public bool EstaAutenticado => usuarioActual != null;

        #endregion

        #region "Login y autenticación"

        public BEUsuario Login(string username, string password)
        {
            try
            {
                ValidadorBLL.CampoObligatorio(username, "Nombre de usuario");
                ValidadorBLL.CampoObligatorio(password, "Contraseña");

                // Refuerzo defensivo: asegura XML, permisos, roles y usuario base antes de autenticar.
                // El login también lo inicializa al cargar; repetirlo no debería duplicar datos.
                usuarioBLL.InicializarSistema();
               
                // La validación de credenciales se delega en UsuarioBLL.
                var usuario = usuarioBLL.BuscarPorCredenciales(username, password);

                if (usuario == null)
                {
                    // También se registra el intento fallido para auditoría.
                    bitacoraBLL.RegistrarEvento(
                        new BEUsuario { Username = username },
                        TipoOperacion.Error,
                        "Intento de inicio de sesión fallido (usuario o contraseña incorrectos).",
                        "Transversal/Seguridad");

                    throw new Exception("Usuario o contraseña incorrectos.");
                }

                usuarioActual = usuario;
                NombreUsuario = usuario.Username;

                bitacoraBLL.RegistrarEvento(
                    usuario,
                    TipoOperacion.Login,
                    "Inicio de sesión exitoso.",
                    "Transversal/Seguridad");

                // El respaldo se ejecuta después de autenticar. Una falla se audita, pero no invalida la sesión.
                try
                {
                    new BackupBLL().GenerarAutomaticoAlIniciarSesion(usuario);
                }
                catch (Exception exBackup)
                {
                    try
                    {
                        bitacoraBLL.RegistrarEvento(
                            usuario,
                            TipoOperacion.Error,
                            "No se pudo generar el backup automático de inicio de sesión. " +
                            ObtenerMensajeOriginal(exBackup),
                            "Transversal/Backup");
                    }
                    catch
                    {
                        // El error del registro de auditoría tampoco debe impedir un login válido.
                    }
                }

                return usuario;
            }
            catch (EncriptacionException ex)
            {
                throw new Exception("Error en el proceso de encriptación/desencriptación.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error durante la autenticación del usuario.", ex);
            }
        }

        private string ObtenerMensajeOriginal(Exception ex)
        {
            var actual = ex;
            while (actual.InnerException != null)
                actual = actual.InnerException;

            return actual.Message;
        }

        public void Logout()
        {
            try
            {
                if (usuarioActual == null)
                    return;

                // Registrar logout antes de limpiar la sesión permite conservar el usuario responsable.
                bitacoraBLL.RegistrarEvento(
                    usuarioActual,
                    TipoOperacion.Logout,
                    "Cierre de sesión.",
                    "Transversal/Seguridad");

                usuarioActual = null;
                NombreUsuario = string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("Error durante el cierre de sesión.", ex);
            }
        }

        #endregion

        #region "Permisos"

        public bool TienePermiso(string accion)
        {
            try
            {
                if (usuarioActual == null)
                    return false;

                // El usuario base tiene acceso total para asegurar administración inicial.
                if (usuarioActual.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    return true;

                // Multi-rol: alcanza con que uno de los roles tenga el permiso solicitado.
                if (usuarioActual.Roles == null || usuarioActual.Roles.Count == 0)
                    return false;

                // Los roles ya vienen reconstruidos con sus permisos desde UsuarioBLL al iniciar sesión.
                foreach (var rol in usuarioActual.Roles)
                {
                    if (rol == null) continue;

                    if (compositeBLL.TienePermiso(rol, accion))
                        return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar permisos del usuario.", ex);
            }
        }

        #endregion
    }
}
