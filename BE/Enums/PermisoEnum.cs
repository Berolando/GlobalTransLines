using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Enums
{
    /// <summary>
    /// Define las categorías funcionales principales del sistema GlobalTrans Lines.
    /// Cada permiso se clasifica según la gestión o módulo al que pertenece,
    /// </summary>
    public enum PermisoEnum
    {
        GestionComercial,      // Gestión 1 - Toma de Cargas y Generación de Reserva
        OperacionesNavieras,   // Gestión 2 - Asignación y Optimización de Transporte
        Finanzas,              // Gestión 3 - Facturación y Cobros
        Subcontratados,        // Gestión 4 - Servicios Subcontratados
        Configuracion          // Módulos transversales (Seguridad, Usuarios, Bitácora, Backup, Reportes)
    }
}
