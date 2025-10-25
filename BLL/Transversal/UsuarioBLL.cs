using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE;
using BE.Transversal;
using MPP.Transversal;

namespace BLL.Transversal
{
    public class UsuarioBLL : IGestor<BEUsuario>
    {
        private UsuarioMPP usuarioMPP = new UsuarioMPP();
        private RolMPP rolMPP = new RolMPP();

        #region "Guardar"

        // Guarda o modifica un usuario según su Id
        public void Guardar(BEUsuario usuario)
        {
            try
            {
                // Validaciones previas
                ValidadorBLL.ObjetoNoNulo(usuario, "Usuario");
                ValidadorBLL.CampoObligatorio(usuario.Username, "Nombre de usuario");
                ValidadorBLL.CampoObligatorio(usuario.Password, "Contraseña");
                ValidadorBLL.ObjetoNoNulo(usuario.Rol, "Rol asignado");

                var listaUsuarios = usuarioMPP.Listar();

                // Verifica duplicado de nombre de usuario
                bool duplicado = listaUsuarios.Any(u =>
                    u.Username.Equals(usuario.Username, StringComparison.OrdinalIgnoreCase)
                    && u.Id != usuario.Id);

                if (duplicado)
                    throw new Exception("Ya existe un usuario con el mismo nombre.");

                // Alta o modificación según Id
                if (usuario.Id == 0)
                    usuarioMPP.Alta(usuario);
                else
                    usuarioMPP.Modificar(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el usuario.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        // Elimina un usuario, excepto si es el administrador
        public void Eliminar(BEUsuario usuario)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(usuario, "Usuario");

                if (usuario.Rol != null &&
                    usuario.Rol.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
                    throw new Exception("No se puede eliminar el usuario Administrador.");

                usuarioMPP.Baja(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario.", ex);
            }
        }

        #endregion

        #region "Inicialización del sistema"

        // 🧠 Crea la estructura mínima (Permisos, Rol y Usuario admin)
        public void InicializarSistema()
        {
            try
            {
                // Garantiza existencia física de los archivos XML
                string _ = MPP.Transversal.GeneradorRutas.UbicacionEntidad<BEUsuario>();

                // Inicializa permisos base del sistema
                var permisoBLL = new PermisoBLL();
                permisoBLL.ListarTodo(); // genera si no existen

                //  Inicializa el rol Administrador con todos los permisos
                var rolBLL = new RolBLL();
                rolBLL.InicializarRolAdministrador();

                //  Crea el usuario admin si no existe
                InicializarAdminPorDefecto();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el sistema.", ex);
            }
        }

        //  Crea el usuario Admin (solo si no existe)
        private void InicializarAdminPorDefecto()
        {
            try
            {
                var usuarios = usuarioMPP.Listar();

                bool existeAdmin = usuarios.Any(u =>
                    u.Username.Equals("admin", StringComparison.OrdinalIgnoreCase));

                if (!existeAdmin)
                {
                    var resultado = Encriptador.Encriptar("1234");

                    var rolBLL = new RolBLL();
                    var rolAdmin = rolBLL.ListarTodo()
                        .FirstOrDefault(r => r.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase));

                    BEUsuario admin = new BEUsuario
                    {
                        Username = "admin",
                        Password = resultado.TextoCifrado,
                        ClaveEncriptacion = resultado.Clave,
                        Activo = true,
                        Rol = rolAdmin, // Rol completo, no solo Id
                        Empleado = new BEEmpleado { Id = 0 }
                    };

                    usuarioMPP.Alta(admin);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el usuario administrador.", ex);
            }
        }

        #endregion

        #region "Listar"

        // Devuelve todos los usuarios, reconstruyendo sus roles
        public List<BEUsuario> ListarTodo()
        {
            try
            {
                var usuarios = usuarioMPP.Listar();
                var roles = rolMPP.Listar();

                foreach (var usuario in usuarios)
                {
                    if (usuario.Rol != null && usuario.Rol.Id > 0)
                        usuario.Rol = roles.FirstOrDefault(r => r.Id == usuario.Rol.Id);
                }

                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios.", ex);
            }
        }

        // Devuelve un usuario específico según Id
        public BEUsuario ListarObjeto(BEUsuario usuario)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(usuario, "Usuario");

                var encontrado = usuarioMPP.BuscarPorId(usuario.Id);

                if (encontrado != null && encontrado.Rol != null)
                    encontrado.Rol = rolMPP.BuscarPorId(encontrado.Rol.Id);

                return encontrado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuario.", ex);
            }
        }

        #endregion

        #region "Autenticación"

        // Valida las credenciales de login
        public BEUsuario BuscarPorCredenciales(string username, string password)
        {
            try
            {
                ValidadorBLL.CampoObligatorio(username, "Nombre de usuario");
                ValidadorBLL.CampoObligatorio(password, "Contraseña");

                var usuarios = usuarioMPP.Listar();

                foreach (var u in usuarios)
                {
                    string passwordDesencriptada = Encriptador.Desencriptar(u.Password, u.ClaveEncriptacion);

                    if (u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                        && passwordDesencriptada == password
                        && u.Activo)
                    {
                        if (u.Rol != null)
                            u.Rol = rolMPP.BuscarPorId(u.Rol.Id);

                        return u;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuario por credenciales.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        // Devuelve el último Id registrado
        public long ObtenerUltimoId()
        {
            try
            {
                return usuarioMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último Id de usuario.", ex);
            }
        }

        #endregion
    }
}
