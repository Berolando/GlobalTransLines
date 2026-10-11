
using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class EstadoBooking
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        private EstadoBooking(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }

        public static EstadoBooking Pendiente = new EstadoBooking("PEN", "Pendiente");
        public static EstadoBooking Confirmado = new EstadoBooking("CON", "Confirmado");
        public static EstadoBooking Asignado = new EstadoBooking("ASI", "Asignado");
        public static EstadoBooking Facturado = new EstadoBooking("FAC", "Facturado");

        public static IEnumerable<EstadoBooking> ObtenerTodos()
        {
            return new[] { Pendiente, Confirmado, Asignado, Facturado };
        }

        public override string ToString()
        {
            return Descripcion;
        }

        public static EstadoBooking ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(x => x.Codigo == codigo);
        }
    }
}