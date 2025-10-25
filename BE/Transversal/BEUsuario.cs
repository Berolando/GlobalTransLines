using System;

namespace BE.Transversal
{
    public class BEUsuario : BEEntidad
    {
        #region "Propiedades"

        // Nombre de usuario (login del sistema)
        public string Username { get; set; }

        // Contraseña encriptada
        public string Password { get; set; }

        // Clave utilizada para el proceso de encriptación
        public byte ClaveEncriptacion { get; set; }

        // Indica si el usuario está activo
        public bool Activo { get; set; } = true;

        // Rol principal asignado al usuario
        public BERol Rol { get; set; }

        // Empleado asociado al usuario (si aplica)
        public BEEmpleado Empleado { get; set; }

        #endregion

        #region "Constructores"

        public BEUsuario() { }

        public BEUsuario(string username, string password, BERol rol, bool activo = true)
        {
            Username = username;
            Password = password;
            Rol = rol;
            Activo = activo;
        }

        public BEUsuario(string username, string password, byte claveEncriptacion, BERol rol, bool activo = true)
        {
            Username = username;
            Password = password;
            ClaveEncriptacion = claveEncriptacion;
            Rol = rol;
            Activo = activo;
        }

        #endregion

        #region "Métodos"

        public override string ToString()
        {
            string estado = Activo ? "Activo" : "Inactivo";
            string rolNombre = Rol != null ? Rol.Nombre : "Sin rol";
            string empleadoInfo = Empleado != null ? $" | Empleado: {Empleado.Nombre}" : "";

            return $"Usuario: {Username} | Rol: {rolNombre} | Estado: {estado}{empleadoInfo}";
        }

        #endregion
    }
}