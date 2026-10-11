using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class TipoOperacion
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        private TipoOperacion(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }

        #region "Instancias estáticas"

        //  Seguridad y usuarios
        public static readonly TipoOperacion Login = new TipoOperacion("LOG-IN", "Inicio de sesión");
        public static readonly TipoOperacion Logout = new TipoOperacion("LOG-OUT", "Cierre de sesión");
        public static readonly TipoOperacion AltaUsuario = new TipoOperacion("USR-ALT", "Alta de usuario");
        public static readonly TipoOperacion ModificacionUsuario = new TipoOperacion("USR-MOD", "Modificación de usuario");
        public static readonly TipoOperacion BajaUsuario = new TipoOperacion("USR-BAJ", "Baja de usuario");
        public static readonly TipoOperacion Operacion = new TipoOperacion("USR-OP", "Operación general de usuario");

        //  Backup / Restore
        public static readonly TipoOperacion Backup = new TipoOperacion("SYS-BK", "Backup del sistema");
        public static readonly TipoOperacion Restore = new TipoOperacion("SYS-RE", "Restauración del sistema");

        //  Operaciones generales
        public static readonly TipoOperacion Alta = new TipoOperacion("GEN-ALT", "Alta de entidad");
        public static readonly TipoOperacion Modificacion = new TipoOperacion("GEN-MOD", "Modificación de entidad");
        public static readonly TipoOperacion Baja = new TipoOperacion("GEN-BAJ", "Baja de entidad");

        //  Sistema y administración
        public static readonly TipoOperacion Error = new TipoOperacion("SYS-ERR", "Error del sistema");
        public static readonly TipoOperacion Alerta = new TipoOperacion("SYS-ALR", "Alerta del sistema");
        public static readonly TipoOperacion Informativo = new TipoOperacion("SYS-INF", "Información del sistema");

        #endregion

        #region "Métodos de utilidad"

        public static TipoOperacion ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(t => t.Codigo == codigo) ?? Informativo;
        }

        public static IEnumerable<TipoOperacion> ObtenerTodos()
        {
            return new[]
            {
                Login, Logout, AltaUsuario, ModificacionUsuario, BajaUsuario, Operacion,
                Backup, Restore, Alta, Modificacion, Baja,
                Error, Alerta, Informativo
            };
        }

        public override string ToString()
        {
            return Descripcion;
        }

        #endregion
    }
}
