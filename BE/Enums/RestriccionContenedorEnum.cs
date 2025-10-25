using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Enums
{
    public enum RestriccionContenedorEnum
    { 
       /// <summary>
      /// Sin restricciones especiales - Carga general seca
      /// </summary>
        Ninguna,

        /// <summary>
        /// Materiales peligrosos (Clase IMDG 1-9)
        /// </summary>
        Peligroso_IMDG,

        /// <summary>
        /// Requiere control de temperatura
        /// </summary>
        Refrigerado,

        /// <summary>
        /// Materiales corrosivos (Clase 8)
        /// </summary>
        Corrosivo,

        /// <summary>
        /// Materiales inflamables (Clase 3)
        /// </summary>
        Inflamable,

        /// <summary>
        /// Sustancias tóxicas (Clase 6.1)
        /// </summary>
        Toxico,

        /// <summary>
        /// Carga líquida a granel
        /// </summary>
        Liquidos,

        /// <summary>
        /// Carga con dimensiones/peso excepcionales
        /// </summary>
        SobreDimensionado,

        /// <summary>
        /// Mercancías con valor alto
        /// </summary>
        Alto_Valor,

        /// <summary>
        /// Carga que requiere manipulación especial
        /// </summary>
        Fragil
    }
}
