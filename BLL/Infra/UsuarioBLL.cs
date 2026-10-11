using Abstraccion;
using BE.Personas;
using BE.Infra;
using Transversales;
using System;
using System.Collections.Generic;
using System.Linq;
using MPP.Infra;
using BLL.Personas;

namespace BLL.Infra
{
    public class UsuarioBLL : IGestor<BEUsuario>
    {
        private const string UsuarioAdministradorInicial = "admin";
        private const string ClaveAdministradorInicial = "1234";

        private UsuarioMPP usuarioMPP = new UsuarioMPP();
        private RolMPP rolMPP = new RolMPP();
        private readonly EmpleadoBLL empleadoBLL = new EmpleadoBLL();


        #region "Guardar"

        public void Guardar(BEUsuario usuario)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(usuario, "Usuario");
                ValidadorBLL.CampoObligatorio(usuario.Username, "Nombre de usuario");

                var listaUsuarios = usuarioMPP.Listar();

                // Regla de negocio: no puede haber dos usuarios con el mismo nombre.
                bool duplicado = listaUsuarios.Any(u =>
                    u.Username.Equals(usuario.Username, StringComparison.OrdinalIgnoreCase)
                    && u.Id != usuario.Id);

                if (duplicado)
                    throw new Exception("Ya existe un usuario con el mismo nombre.");

                // Relación 1 a 1: un empleado activo solo puede estar asociado a un usuario.
                long empleadoId = 0;
                if (usuario.Empleado != null)
                    empleadoId = usuario.Empleado.Id;

                if (empleadoId > 0)
                {
                    var usuarios = usuarioMPP.Listar();

                    bool empleadoYaAsignado = usuarios.Any(u =>
                        u != null
                        && u.Activo
                        && u.Id != usuario.Id
                        && u.Empleado != null
                        && u.Empleado.Id > 0
                        && u.Empleado.Id == empleadoId
                    );

                    if (empleadoYaAsignado)
                        throw new Exception("Ese empleado ya está asociado a otro usuario activo.");
                }


                // Alta: la contraseña es obligatoria porque todavía no existe una guardada.
                if (usuario.Id == 0)
                {
                    ValidadorBLL.CampoObligatorio(usuario.Password, "Contraseña");

                    var resultado = Encriptador.Encriptar(usuario.Password);
                    usuario.Password = resultado.TextoCifrado;
                    usuario.ClaveEncriptacion = resultado.Clave;

                    usuarioMPP.Alta(usuario);
                    return; 
                }

                // Modificación: se recupera el usuario guardado para conservar datos sensibles.
                var usuarioExistente = usuarioMPP.BuscarPorId(usuario.Id);
                if (usuarioExistente == null)
                    throw new Exception("Usuario no encontrado para modificar.");

                // Si no se escribió una nueva contraseña, se conserva la cifrada existente.
                if (!string.IsNullOrWhiteSpace(usuario.Password) && usuario.Password == usuarioExistente.Password)
                {
                    usuario.ClaveEncriptacion = usuarioExistente.ClaveEncriptacion;
                    usuario.Password = usuarioExistente.Password;
                }
                else if (string.IsNullOrWhiteSpace(usuario.Password))
                {
                    usuario.Password = usuarioExistente.Password;
                    usuario.ClaveEncriptacion = usuarioExistente.ClaveEncriptacion;
                }
                else
                {
                    var resultado = Encriptador.Encriptar(usuario.Password);
                    usuario.Password = resultado.TextoCifrado;
                    usuario.ClaveEncriptacion = resultado.Clave;
                }

                usuarioMPP.Modificar(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el usuario.", ex);
            }
        }

        public void GuardarDesdeFormulario(BEUsuario usuario, long? empleadoIdSeleccionado, bool quitarEmpleado)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(usuario, "Usuario");

                // La pantalla solo informa la intención; la BLL decide qué conservar o modificar.
                if (usuario.Id > 0)
                {
                    var usuarioExistente = usuarioMPP.BuscarPorId(usuario.Id);
                    if (usuarioExistente == null)
                        throw new Exception("Usuario no encontrado para modificar.");

                    // Estos datos se administran en otros flujos, por eso se conservan al modificar desde el formulario.
                    usuario.Activo = usuarioExistente.Activo;
                    usuario.Roles = usuarioExistente.Roles;

                    // El empleado solo se quita si la pantalla lo pidió explícitamente.
                    if (quitarEmpleado)
                    {
                        usuario.Empleado = null;
                    }
                    else if (empleadoIdSeleccionado.HasValue && empleadoIdSeleccionado.Value > 0)
                    {
                        usuario.Empleado = new BEEmpleado { Id = empleadoIdSeleccionado.Value };
                    }
                    else
                    {
                        usuario.Empleado = usuarioExistente.Empleado;
                    }
                }

                Guardar(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el usuario desde el formulario.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        // Baja lógica: el registro queda en XML, pero se marca como inactivo.
        public void Eliminar(BEUsuario usuario)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(usuario, "Usuario");

                // Se valida contra el usuario persistido, no contra el objeto incompleto de la UI.
                var usuarioPersistido = usuarioMPP.BuscarPorId(usuario.Id);
                if (usuarioPersistido == null)
                    throw new InvalidOperationException("No se encontró el registro solicitado.");

                if (UsuarioEsAdministrador(usuarioPersistido))
                    throw new InvalidOperationException("No se puede eliminar este registro.");

                usuarioMPP.Baja(usuarioPersistido);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario.", ex);
            }
        }

        private bool UsuarioEsAdministrador(BEUsuario usuario)
        {
            if (usuario == null || usuario.Roles == null) return false;

            // Si los roles ya están reconstruidos, se valida directamente por nombre.
            if (usuario.Roles.Any(r =>
                r != null &&
                !string.IsNullOrEmpty(r.Nombre) &&
                r.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            // Si la UI trajo roles incompletos, se valida contra el rol persistido por Id.
            var rolAdministrador = rolMPP.Listar()
                .FirstOrDefault(r =>
                    r != null &&
                    r.Activo &&
                    r.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase));

            return rolAdministrador != null &&
                   usuario.Roles.Any(r => r != null && r.Id == rolAdministrador.Id);
        }

        #endregion

        #region "Inicialización del sistema"

        public void InicializarSistema()
        {
            try
            {
                // Crea la estructura minima de XML base.
                InicializadorSistema.CrearEstructuraBase();

                // Registra los reconstructores necesarios para resolver referencias por Id.
                InicializadorSistema.InicializarReconstructores();

                // Inicializa permisos base del sistema.
                var permisoBLL = new PermisoBLL();
                permisoBLL.InicializarPermisos();

                // Inicializa el rol Administrador con todos los permisos.
                var rolBLL = new RolBLL();
                rolBLL.InicializarRolAdministrador();

                // Asigna permisos al rol administrador.
                var composite = new ComponentBLL();
                composite.AsignarPermisosBaseAlAdministrador();

                // Crea el usuario semilla para el primer acceso al sistema.
                InicializarAdminPorDefecto();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el sistema.", ex);
            }
        }

        private void InicializarAdminPorDefecto()
        {
            try
            {
                var usuarios = usuarioMPP.Listar();

                // La inicialización solo crea el usuario base si todavía no existe.
                bool existeAdmin = usuarios.Any(u =>
                    u.Username.Equals(UsuarioAdministradorInicial, StringComparison.OrdinalIgnoreCase));

                if (!existeAdmin)
                {
                    var resultado = Encriptador.Encriptar(ClaveAdministradorInicial);

                    var rolesJerarquia = new ComponentBLL().ConstruirJerarquia();
                    var rolAdmin = rolesJerarquia
                        .FirstOrDefault(r => r.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase));

                    BEUsuario admin = new BEUsuario
                    {
                        Username = UsuarioAdministradorInicial,
                        Password = resultado.TextoCifrado,
                        ClaveEncriptacion = resultado.Clave,
                        Activo = true,
                        Roles = new List<BERol>(),
                        Empleado =  null 
                    };

                    if (rolAdmin != null)
                        admin.Roles.Add(rolAdmin);

                    usuarioMPP.Alta(admin);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el usuario administrador.", ex);
            }
        }

        #endregion

        #region "Asignación de Roles (Usuario-Rol)"

        // Agrega un rol a un usuario sin modificar sus credenciales.
        public void AsignarRol(long usuarioId, long rolId)
        {
            try
            {
                if (usuarioId <= 0) throw new Exception("UsuarioId inválido.");
                if (rolId <= 0) throw new Exception("RolId inválido.");

                // Se trae el usuario completo para no pisar contraseña ni clave de cifrado.
                var usuario = usuarioMPP.BuscarPorId(usuarioId);
                if (usuario == null) throw new Exception("Usuario no encontrado.");

                if (usuario.Roles == null)
                    usuario.Roles = new List<BERol>();

                // Evitar duplicados
                if (!usuario.Roles.Any(r => r != null && r.Id == rolId))
                    usuario.Roles.Add(new BERol { Id = rolId });

                // Se persisten solo los roles, conservando las credenciales ya guardadas.
                usuarioMPP.Modificar(usuario);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al asignar rol al usuario.", ex);
            }
        }

        public void QuitarRol(long usuarioId, long rolId)
        {
            try
            {
                if (usuarioId <= 0) throw new Exception("UsuarioId inválido.");
                if (rolId <= 0) throw new Exception("RolId inválido.");

                var usuario = usuarioMPP.BuscarPorId(usuarioId);
                if (usuario == null) throw new Exception("Usuario no encontrado.");

                if (usuario.Roles == null)
                    usuario.Roles = new List<BERol>();

                // Regla de negocio: un usuario activo debe conservar al menos un rol.
                if (usuario.Activo && usuario.Roles.Count(r => r != null) <= 1)
                    throw new Exception("El usuario debe tener al menos un rol asignado.");

                usuario.Roles.RemoveAll(r => r != null && r.Id == rolId);

                usuarioMPP.Modificar(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al quitar rol al usuario.", ex);
            }
        }

        // (Opcional) Para el popup: asignar varios de una, sin duplicar
        public void AsignarRoles(long usuarioId, List<long> rolesIds)
        {
            try
            {
                if (rolesIds == null) rolesIds = new List<long>();

                var usuario = usuarioMPP.BuscarPorId(usuarioId);
                if (usuario == null) throw new Exception("Usuario no encontrado.");

                if (usuario.Roles == null)
                    usuario.Roles = new List<BERol>();

                foreach (var id in rolesIds.Where(x => x > 0).Distinct())
                {
                    if (!usuario.Roles.Any(r => r != null && r.Id == id))
                        usuario.Roles.Add(new BERol { Id = id });
                }

                usuarioMPP.Modificar(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asignar roles al usuario.", ex);
            }
        }

        #endregion


        #region "Listar"

        public List<BEUsuario> ListarTodo()
        {
            try
            {
                var usuarios = usuarioMPP.Listar();

                // Única fuente de reconstrucción: jerarquía real del composite.
                var rolesJerarquia = new ComponentBLL().ConstruirJerarquia();

                foreach (var usuario in usuarios)
                {
                    ReconstruirRolesUsuario(usuario, rolesJerarquia);
                    if (usuario.Empleado != null && usuario.Empleado.Id > 0)
                        usuario.Empleado = ReconstructorBLL.Reconstruir<BEEmpleado>(usuario.Empleado.Id);
                }

                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios.", ex);
            }
        }

        public BEUsuario ListarObjeto(BEUsuario usuario)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(usuario, "Usuario");

                var encontrado = usuarioMPP.BuscarPorId(usuario.Id);
                if (encontrado == null) return null;

                var rolesJerarquia = new ComponentBLL().ConstruirJerarquia();
                ReconstruirRolesUsuario(encontrado, rolesJerarquia);

                // Reconstruye el empleado completo porque en XML se guarda solo su Id.
                if (encontrado.Empleado != null && encontrado.Empleado.Id > 0)
                    encontrado.Empleado = ReconstructorBLL.Reconstruir<BEEmpleado>(encontrado.Empleado.Id);

                return encontrado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuario.", ex);
            }
        }

        private void ReconstruirRolesUsuario(BEUsuario usuario, List<BERol> rolesJerarquia)
        {
            if (usuario == null) return;
            if (usuario.Roles == null) usuario.Roles = new List<BERol>();
            if (rolesJerarquia == null) rolesJerarquia = new List<BERol>();

            // En XML el usuario guarda IDs de roles; acá se reemplazan por roles completos.
            var reconstruidos = new List<BERol>();

            foreach (var rolPlaceHolder in usuario.Roles)
            {
                if (rolPlaceHolder == null || rolPlaceHolder.Id <= 0) continue;

                var real = rolesJerarquia.FirstOrDefault(r => r.Id == rolPlaceHolder.Id);
                if (real != null && !reconstruidos.Any(x => x.Id == real.Id))
                    reconstruidos.Add(real);
            }

            usuario.Roles = reconstruidos;
        }

        #endregion

        #region "Autenticación"

        public BEUsuario BuscarPorCredenciales(string username, string password)
        {
            try
            {
                ValidadorBLL.CampoObligatorio(username, "Nombre de usuario");
                ValidadorBLL.CampoObligatorio(password, "Contraseña");

                var usuarios = usuarioMPP.Listar();

                // Se compara contra la contraseña descifrada porque el XML guarda el valor cifrado.
                foreach (var u in usuarios)
                {
                    string passwordDesencriptada = Encriptador.Desencriptar(u.Password, u.ClaveEncriptacion);

                    if (u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                        && passwordDesencriptada == password
                        && u.Activo)
                    {
                        // Reconstrucción consistente con ListarTodo: jerarquía composite.
                        var rolesJerarquia = new ComponentBLL().ConstruirJerarquia();
                        ReconstruirRolesUsuario(u, rolesJerarquia);

                        if (u.Empleado != null && u.Empleado.Id > 0)
                            u.Empleado = ReconstructorBLL.Reconstruir<BEEmpleado>(u.Empleado.Id);


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
