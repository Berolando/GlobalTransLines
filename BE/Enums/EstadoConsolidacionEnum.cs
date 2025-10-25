using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Enums
{
    public enum EstadoConsolidacionEnum
    {
        Pendiente,      // El contenedor está asignado, pero aún no se agruparon cargas
        EnProceso,      // El planificador está consolidando cargas
        Consolidado,    // El contenedor ya quedó consolidado
        ListoParaEstiba,// Consolidación cerrada y disponible para CU-080
        Cerrado         // Consolidación finalizada, ya embarcada o facturada
    }
}
