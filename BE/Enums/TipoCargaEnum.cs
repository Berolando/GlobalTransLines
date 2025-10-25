using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Enums
{
    /// <summary>
    /// Representa los diferentes tipos de carga que maneja la naviera.
    /// </summary>
    public enum TipoCargaEnum
    {
        /// <summary>
        /// Full Container Load - Contenedor completo destinado a un solo cliente.
        /// </summary>
        FCL,

        /// <summary>
        /// Less than Container Load - Consolidación de cargas parciales de distintos clientes en un mismo contenedor.
        /// </summary>
        LCL,

        /// <summary>
        /// Reefer - Contenedor refrigerado, para cargas que requieren control de temperatura.
        /// </summary>
        Reefer,

        /// <summary>
        /// Carga peligrosa (IMO), que requiere condiciones especiales de manipulación y transporte.
        /// </summary>
        Peligrosa,

        /// <summary>
        /// Carga general, sin requisitos especiales.
        /// </summary>
        General,

        /// <summary>
        /// Granel - Mercancía transportada sin empaquetar en grandes cantidades (ej.: granos, minerales).
        /// </summary>
        Granel,
    }
}
