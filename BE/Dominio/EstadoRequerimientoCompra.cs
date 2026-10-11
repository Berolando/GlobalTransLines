using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class EstadoRequerimientoCompra
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        private EstadoRequerimientoCompra(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }

        public static EstadoRequerimientoCompra Pendiente =
            new EstadoRequerimientoCompra("PEN", "Pendiente");

        public static EstadoRequerimientoCompra Procesado =
            new EstadoRequerimientoCompra("PRO", "Procesado");

        public static EstadoRequerimientoCompra Anulado =
            new EstadoRequerimientoCompra("ANU", "Anulado");

        public static IEnumerable<EstadoRequerimientoCompra> ObtenerTodos()
        {
            return new[] { Pendiente, Procesado, Anulado };
        }

        public override string ToString()
        {
            return Descripcion;
        }

        public static EstadoRequerimientoCompra ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(x => x.Codigo == codigo);
        }
    }
}
