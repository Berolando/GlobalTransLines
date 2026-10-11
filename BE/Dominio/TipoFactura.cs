using System.Collections.Generic;
using System.Linq;
namespace BE.Dominio
{

    public class TipoFactura
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        private TipoFactura(string codigo, string descripcion) { Codigo = codigo; Descripcion = descripcion; }

        public static TipoFactura FA = new TipoFactura("FA", "Factura A");
        public static TipoFactura FB = new TipoFactura("FB", "Factura B");
        public static TipoFactura FC = new TipoFactura("FC", "Factura C");
        public static TipoFactura FE = new TipoFactura("FE", "Factura E");
        public static TipoFactura NC = new TipoFactura("NC", "Nota de Crédito");
        public static TipoFactura ND = new TipoFactura("ND", "Nota de Débito");

        public static IEnumerable<TipoFactura> ObtenerTodos()
        {
            return new[] { FA, FB, FC, FE, NC, ND };
        }

        public override string ToString() { return Descripcion; }
        public static TipoFactura ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(x => x.Codigo == codigo);
        }
    }
}