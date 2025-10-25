using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Enums
{
    public enum TipoContenedorEnum
    {
        /// <summary>
        /// Contenedor estándar de 20 pies
        /// Capacidad: 33.2 m³ | Peso máximo: 28,200 kg
        /// Uso: Carga general seca
        /// </summary>
        Standard20,

        /// <summary>
        /// Contenedor estándar de 40 pies
        /// Capacidad: 67.7 m³ | Peso máximo: 30,480 kg
        /// Uso: Carga general seca
        /// </summary>
        Standard40,

        /// <summary>
        /// Contenedor High Cube de 40 pies
        /// Capacidad: 76.4 m³ | Peso máximo: 32,500 kg
        /// Uso: Carga voluminosa (altura extra)
        /// </summary>
        HighCube40,

        /// <summary>
        /// Contenedor refrigerado de 20 pies
        /// Capacidad: 28.0 m³ | Peso máximo: 27,700 kg
        /// Uso: Productos perecederos, temperatura controlada
        /// </summary>
        Reefer20,

        /// <summary>
        /// Contenedor refrigerado de 40 pies
        /// Capacidad: 64.0 m³ | Peso máximo: 29,600 kg
        /// Uso: Productos perecederos, temperatura controlada
        /// </summary>
        Reefer40,

        /// <summary>
        /// Contenedor abierto (sin techo)
        /// Capacidad: 32.0 m³ | Peso máximo: 28,400 kg
        /// Uso: Carga de altura superior, grúa superior
        /// </summary>
        OpenTop,

        /// <summary>
        /// Contenedor plano (sin paredes laterales)
        /// Capacidad: Variable | Peso máximo: 40,000 kg
        /// Uso: Maquinaria pesada, carga sobredimensionada
        /// </summary>
        FlatRack,

        /// <summary>
        /// Contenedor tanque
        /// Capacidad: 21,000-26,000 litros | Peso máximo: 34,000 kg
        /// Uso: Líquidos a granel, productos químicos
        /// </summary>
        Tank
    }
}
