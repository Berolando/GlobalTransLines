using System;
using System.Linq;
using Abstraccion;
using BE.Transversal;
using BE.Enums;
using Seguridad;
using MPP.Transversal;

namespace BLL.Transversal
{
    public class SeguridadBLL : ISeguridad
    {
        private UsuarioBLL usuarioBLL = new UsuarioBLL();
        private CompositeBLL compositeBLL = new CompositeBLL();
        private BitacoraBLL bitacoraBLL = new BitacoraBLL();

        #region "Propiedades"

        private BEUsuario usuarioActual;
        public string NombreUsuario { get; private set; }

        public bool EstaAutenticado => usuarioActual != null;

        #endregion

        #region "Login y autenticación"

        public BEUsuario Login(string username, string password)
        {
            try
            {
                // Validaciones básicas
                ValidadorBLL.CampoObligatorio(username, "Nombre de usuario");
                ValidadorBLL.CampoObligatorio(password, "Contraseña");

                // Garantiza que existan permisos, roles y usuario admin
                usuarioBLL.InicializarSistema();

                // Busca usuario activo
                var usuario = usuarioBLL.ListarTodo()
                    .FirstOrDefault(u =>
                        u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && u.Activo);

                if (usuario == null)
                {
                    bitacoraBLL.RegistrarEvento(
                        new BEUsuario { Username = username },
                        TipoOperacionEnum.Error,
                        "Intento de inicio de sesión con usuario inexistente.",
                        "Transversal/Seguridad");

                    throw new Exception("Usuario o contraseña incorrectos.");
                }

                // Desencripta la contraseña almacenada y compara
                string passwordDesencriptada = Encriptador.Desencriptar(usuario.Password, usuario.ClaveEncriptacion);

                if (!password.Equals(passwordDesencriptada))
                {
                    bitacoraBLL.RegistrarEvento(
                        new BEUsuario { Username = username },
                        TipoOperacionEnum.Error,
                        "Contraseña incorrecta para el usuario.",
                        "Transversal/Seguridad");

                    throw new Exception("Usuario o contraseña incorrectos.");
                }

                // Autenticación exitosa
                usuarioActual = usuario;
                NombreUsuario = usuario.Username;

                bitacoraBLL.RegistrarEvento(
                    usuario,
                    TipoOperacionEnum.Login,
                    "Inicio de sesión exitoso.",
                    "Transversal/Seguridad");

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

        public void Logout()
        {
            try
            {
                if (usuarioActual == null)
                    return;

                bitacoraBLL.RegistrarEvento(
                    usuarioActual,
                    TipoOperacionEnum.Logout,
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
                if (usuarioActual == null || usuarioActual.Rol == null)
                    return false;

                // Superpermiso para el usuario administrador
                if (usuarioActual.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    return true;

                // Validación normal del Composite
                return compositeBLL.TienePermiso(usuarioActual.Rol, accion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar permisos del usuario.", ex);
            }
        }

        #endregion
    }
}
