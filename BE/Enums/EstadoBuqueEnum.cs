using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Enums
{
    public enum EstadoBuqueEnum
    {
        Disponible,    // Puede recibir asignaciones de carga
        EnPuerto,      // Está en puerto, descargando o esperando
        EnRuta,        // Está navegando
        Mantenimiento  // No disponible
    }
}
