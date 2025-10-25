using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Enums
{
    public enum EstadoContenedorEnum
    {
        Disponible,     // Contenedor vacío en puerto
        Asignado,       // Asociado a un booking/carga
        Consolidado,    // Lleno con cargas LCL agrupadas
        EnRuta,         // Embarcado en un buque
        Mantenimiento   // Fuera de servicio
    }
}
