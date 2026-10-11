using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class EstadoOrdenCompra
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }
        private EstadoOrdenCompra(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }
        public static EstadoOrdenCompra Emitida = new EstadoOrdenCompra("EMI", "Emitida");
        public static EstadoOrdenCompra Recibida = new EstadoOrdenCompra("REC", "Recibida");
        public static EstadoOrdenCompra Pagada = new EstadoOrdenCompra("PAG", "Pagada");
        public static EstadoOrdenCompra Rechazada = new EstadoOrdenCompra("RECH", "Rechazada");
        public static IEnumerable<EstadoOrdenCompra> ObtenerTodos()
        {
            return new[] { Emitida, Recibida, Pagada, Rechazada };
        }
        public override string ToString()
        {
            return Descripcion;
        }
        public static EstadoOrdenCompra ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(x => x.Codigo == codigo);
        }

    }
}
