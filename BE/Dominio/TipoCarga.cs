using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class TipoCarga
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }
        public RestriccionContenedor RestriccionRequerida { get; private set; }

        private TipoCarga(string codigo, string descripcion, RestriccionContenedor restriccion = null)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            RestriccionRequerida = restriccion ?? RestriccionContenedor.Ninguna;
        }

        #region "Instancias estáticas"

        public static readonly TipoCarga FCL = new TipoCarga("FCL", "Full Container Load - Contenedor completo destinado a un solo cliente",
                RestriccionContenedor.Ninguna);

        public static readonly TipoCarga LCL =  new TipoCarga("LCL", "Less than Container Load - Consolidación de cargas parciales de distintos clientes",
                RestriccionContenedor.Ninguna);

        public static readonly TipoCarga Reefer = new TipoCarga("REF",  "Reefer - Contenedor refrigerado, para cargas con control de temperatura",
                RestriccionContenedor.Refrigerado);

        public static readonly TipoCarga Peligrosa =  new TipoCarga("PEL","Carga peligrosa (IMO), requiere manipulación especial",
                RestriccionContenedor.Peligroso_IMDG);

        public static readonly TipoCarga General =  new TipoCarga("GEN", "Carga general, sin requisitos especiales",
                RestriccionContenedor.Ninguna);

        public static readonly TipoCarga Granel = new TipoCarga("GRA","Granel - Mercancía a granel (granos, minerales, etc.)",
                RestriccionContenedor.Liquidos);

        #endregion

        #region "Métodos de utilidad"

        public static TipoCarga ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(t => t.Codigo == codigo) ?? General;
        }

        public static IEnumerable<TipoCarga> ObtenerTodos()
        {
            return new[]
            {
                FCL, LCL, Reefer, Peligrosa, General, Granel
            };
        }

        public override string ToString()
        {
            return Descripcion;
        }

        #endregion
    }
}
