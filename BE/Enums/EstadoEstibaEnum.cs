using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Enums
{
    public enum EstadoEstibaEnum
    {
        Pendiente,        // Aún no se armó el plan de estiba
        Confirmada,       // El planificador validó y confirmó la disposición
        ListoParaEmbarque // Estiba final aprobada y lista para zarpe
    }
}
