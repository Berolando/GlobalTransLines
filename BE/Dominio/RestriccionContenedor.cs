using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class RestriccionContenedor
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        private RestriccionContenedor(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }

        #region "Instancias estáticas"

        public static readonly RestriccionContenedor Ninguna =
            new RestriccionContenedor("NING", "Sin restricciones especiales - Carga general seca");

        public static readonly RestriccionContenedor Peligroso_IMDG =
            new RestriccionContenedor("PEL", "Materiales peligrosos (Clase IMDG 1-9)");

        public static readonly RestriccionContenedor Refrigerado =
            new RestriccionContenedor("REF", "Requiere control de temperatura");

        public static readonly RestriccionContenedor Corrosivo =
            new RestriccionContenedor("COR", "Materiales corrosivos (Clase 8)");

        public static readonly RestriccionContenedor Inflamable =
            new RestriccionContenedor("INF", "Materiales inflamables (Clase 3)");

        public static readonly RestriccionContenedor Toxico =
            new RestriccionContenedor("TOX", "Sustancias tóxicas (Clase 6.1)");

        public static readonly RestriccionContenedor Liquidos =
            new RestriccionContenedor("LIQ", "Carga líquida a granel");

        public static readonly RestriccionContenedor SobreDimensionado =
            new RestriccionContenedor("SOB", "Carga con dimensiones/peso excepcionales");

        public static readonly RestriccionContenedor Alto_Valor =
            new RestriccionContenedor("VAL", "Mercancías con valor alto");

        public static readonly RestriccionContenedor Fragil =
            new RestriccionContenedor("FRA", "Carga que requiere manipulación especial");

        #endregion

        #region "Métodos de utilidad"

        public static RestriccionContenedor ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(r => r.Codigo == codigo) ?? Ninguna;
        }

        public static IEnumerable<RestriccionContenedor> ObtenerTodos()
        {
            return new[]
            {
                Ninguna, Peligroso_IMDG, Refrigerado, Corrosivo,
                Inflamable, Toxico, Liquidos, SobreDimensionado,
                Alto_Valor, Fragil
            };
        }

        public override string ToString()
        {
            return Descripcion;
        }

        #endregion
    }
}
