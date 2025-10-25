using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Enums
{
    public enum TipoOperacionEnum
    {
        //Seguridad y usuarios
        Login,
        Logout,
        AltaUsuario,
        ModificacionUsuario,
        BajaUsuario,
        Operacion,

        // Backup / Restore
        Backup,
        Restore,

        // Operaciones generales
        Alta,
        Modificacion,
        Baja,

        // Sistema y administración
        Error,
        Alerta,
        Informativo
    }
}
