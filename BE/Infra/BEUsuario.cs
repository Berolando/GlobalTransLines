using BE.Personas;
using System.Collections.Generic;

namespace BE.Infra
{
    public class BEUsuario : BEEntidad
    {
        #region "Propiedades"


        public string Username { get; set; }

        // Contraseña almacenada cifrada, no en texto plano.
        public string Password { get; set; }

        // Clave necesaria para descifrar la contraseña guardada.
        public byte ClaveEncriptacion { get; set; }



        // Roles asignados al usuario para validar permisos.
        public List<BERol> Roles { get; set; }


        // Empleado vinculado al usuario, si corresponde.
        public BEEmpleado Empleado { get; set; }

        #endregion

        #region "Constructores"

        public BEUsuario()
        {
            Roles = new List<BERol>();
            Activo = true;
        }
        public BEUsuario(string username, string password, byte claveEncriptacion)
        {
            Username = username;
            Password = password;
            ClaveEncriptacion = claveEncriptacion;
            Roles = new List<BERol>();
            Activo = true;
        }

        public BEUsuario(string username, string password, byte claveEncriptacion, BEEmpleado empleado)
            : this(username, password, claveEncriptacion)
        {
            Empleado = empleado;
        }

        #endregion

        #region "Métodos"

       public override string ToString()
        {
            string estado = Activo ? "Activo" : "Inactivo";
            return string.Format(
                "Usuario: {0} | Estado: {1} | Empleado: {2}",
                Username,
                estado,
                Empleado != null ? $"{Empleado.Nombre} {Empleado.Apellido}" : "N/A"
            );
        }

        #endregion
    }
}
