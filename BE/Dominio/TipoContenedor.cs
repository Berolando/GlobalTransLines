using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class TipoContenedor
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }
        public RestriccionContenedor Restriccion { get; private set; }

        private TipoContenedor(string codigo, string descripcion, RestriccionContenedor restriccion = null)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            Restriccion = restriccion ?? RestriccionContenedor.Ninguna;
        }

        #region "Instancias estáticas"

        public static readonly TipoContenedor Standard20 = new TipoContenedor("20STD","Contenedor estándar de 20 pies | 33.2 m³ | 28,200 kg | Carga general seca",
         RestriccionContenedor.Ninguna);

        public static readonly TipoContenedor Standard40 = new TipoContenedor("40STD","Contenedor estándar de 40 pies | 67.7 m³ | 30,480 kg | Carga general seca",
                RestriccionContenedor.Ninguna);

        public static readonly TipoContenedor HighCube40 = new TipoContenedor("40HC", "High Cube de 40 pies | 76.4 m³ | 32,500 kg | Carga voluminosa (altura extra)",
                RestriccionContenedor.Ninguna);

        public static readonly TipoContenedor Reefer20 =new TipoContenedor("20REF", "Reefer 20 pies | 28.0 m³ | 27,700 kg | Productos perecederos",
                RestriccionContenedor.Refrigerado);

        public static readonly TipoContenedor Reefer40 = new TipoContenedor("40REF", "Reefer 40 pies | 64.0 m³ | 29,600 kg | Productos perecederos",
                RestriccionContenedor.Refrigerado);

        public static readonly TipoContenedor OpenTop = new TipoContenedor("OPNT", "Contenedor abierto (sin techo) | 32.0 m³ | 28,400 kg | Carga alta",
                RestriccionContenedor.SobreDimensionado);

        public static readonly TipoContenedor FlatRack = new TipoContenedor("FLRK", "Contenedor plano (sin paredes laterales) | Peso máx. 40,000 kg | Maquinaria pesada",
                RestriccionContenedor.SobreDimensionado);

        public static readonly TipoContenedor Tank = new TipoContenedor("TANK","Contenedor tanque | 21,000–26,000 L | 34,000 kg | Líquidos a granel",
                RestriccionContenedor.Liquidos);

        #endregion
        #region "Propiedades calculadas"

        // Volumen máximo en metros cúbicos (estimado)
        public decimal CapacidadVolumenM3
        {
            get
            {
                switch (Codigo)
                {
                    case "20STD": return 33.2m;
                    case "40STD": return 67.7m;
                    case "40HC": return 76.4m;
                    case "20REF": return 28.0m;
                    case "40REF": return 64.0m;
                    case "OPNT": return 32.0m;
                    case "FLRK": return 0m;     // no aplica volumen cerrado
                    case "TANK": return 26.0m;  // en m³ aprox.
                    default: return 33.2m;
                }
            }
        }

        // Peso máximo en kilogramos (estimado)
        public decimal CapacidadPesoKg
        {
            get
            {
                switch (Codigo)
                {
                    case "20STD": return 28200m;
                    case "40STD": return 30480m;
                    case "40HC": return 32500m;
                    case "20REF": return 27700m;
                    case "40REF": return 29600m;
                    case "OPNT": return 28400m;
                    case "FLRK": return 40000m;
                    case "TANK": return 34000m;
                    default: return 28200m;
                }
            }
        }

        #endregion

        #region "Métodos de utilidad"

        public static TipoContenedor ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(t => t.Codigo == codigo) ?? Standard20;
        }

        public static IEnumerable<TipoContenedor> ObtenerTodos()
        {
            return new[]
            {
                Standard20, Standard40, HighCube40, Reefer20,
                Reefer40, OpenTop, FlatRack, Tank
            };
        }

        public override string ToString()
        {
            return Descripcion;
        }

        #endregion
    }
}
