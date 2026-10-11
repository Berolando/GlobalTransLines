using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class EstadoFactura
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        private EstadoFactura(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }
        public static EstadoFactura Pendiente = new EstadoFactura("PEN", "Pendiente");
        public static EstadoFactura Emitida = new EstadoFactura("EMI", "Emitida");
        public static EstadoFactura Pagada = new EstadoFactura("PAG", "Pagada");
        public static EstadoFactura Anulada = new EstadoFactura("ANU", "Anulada");
        public static IEnumerable<EstadoFactura> ObtenerTodos()
        {
            return new[] { Pendiente, Emitida, Pagada, Anulada };
        }
        public override string ToString()
        {
            return Descripcion;
        }
        public static EstadoFactura ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(x => x.Codigo == codigo);
        }

    }
}
