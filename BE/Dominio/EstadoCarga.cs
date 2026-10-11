using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class EstadoCarga
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        private EstadoCarga(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }

        // Estados posibles de la carga
        public static readonly EstadoCarga Disponible = new EstadoCarga("DIS", "Disponible");
        public static readonly EstadoCarga AceptadaCotizacion = new EstadoCarga("ACC", "Aceptada en Cotización");
        public static readonly EstadoCarga AsignadaBooking = new EstadoCarga("ASB", "Asignada a Booking");
        public static readonly EstadoCarga EnTransito = new EstadoCarga("ENT", "En tránsito");
        public static readonly EstadoCarga Entregada = new EstadoCarga("ENTR", "Entregada");

        public static IEnumerable<EstadoCarga> Listar()
        {
            return new[] { Disponible, AceptadaCotizacion, AsignadaBooking, EnTransito, Entregada };
        }

        public static EstadoCarga ObtenerPorCodigo(string codigo)
        {
            return Listar().FirstOrDefault(e => e.Codigo == codigo) ?? Disponible;
        }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
