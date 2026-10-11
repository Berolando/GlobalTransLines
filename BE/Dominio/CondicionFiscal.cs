using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class CondicionFiscal
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        private CondicionFiscal(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }

        public static CondicionFiscal Monotributo = new CondicionFiscal("MT", "Monotributo");
        public static CondicionFiscal ResponsableInscripto = new CondicionFiscal("RI", "Responsable Inscripto");
        public static CondicionFiscal Exento = new CondicionFiscal("EX", "Exento");
        public static CondicionFiscal ConsumidorFinal = new CondicionFiscal("CF", "Consumidor Final");

        public static IEnumerable<CondicionFiscal> ObtenerTodos()
        {
            return new[] { Monotributo, ResponsableInscripto, Exento, ConsumidorFinal };
        }

        public override string ToString()
        {
            return Descripcion;
        }

        public static CondicionFiscal ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(x => x.Codigo == codigo);
        }
    }
}