using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class EstadoCotizacion
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        private EstadoCotizacion(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }
        public static readonly EstadoCotizacion Pendiente = new EstadoCotizacion("PEN", "Pendiente");
        public static readonly EstadoCotizacion PendienteAprobacion = new EstadoCotizacion("PDA", "Pendiente Aprobacion");
        public static readonly EstadoCotizacion Aceptada = new EstadoCotizacion("ACE", "Aceptada");
        public static readonly EstadoCotizacion Rechazada = new EstadoCotizacion("REC", "Rechazada");
        public static readonly EstadoCotizacion Vencida = new EstadoCotizacion("VEN", "Vencida");
        public static readonly EstadoCotizacion EnBooking = new EstadoCotizacion("EBG", "EnBooking");
        public static IEnumerable<EstadoCotizacion> Listar()
        {
            return new[] { Pendiente,PendienteAprobacion,Aceptada, Rechazada, Vencida, EnBooking };
        }
        public override string ToString()
        {
            return Descripcion;
        }
        public static EstadoCotizacion ObtenerPorCodigo(string codigo)
        {
            return Listar().FirstOrDefault(e => e.Codigo == codigo);
        }
    }
}

