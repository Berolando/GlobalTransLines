using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class EstadoAsignacion
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        private EstadoAsignacion(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }

        public static EstadoAsignacion Pendiente = new EstadoAsignacion("PEN", "Pendiente");
        public static EstadoAsignacion Confirmada = new EstadoAsignacion("CON", "Confirmada");
        public static EstadoAsignacion Cancelada = new EstadoAsignacion("CAN", "Cancelada");

        public static IEnumerable<EstadoAsignacion> ObtenerTodos()
        {
            return new[] { Pendiente, Confirmada, Cancelada };
        }

        public static EstadoAsignacion ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(e => e.Codigo == codigo);
        }

        public override string ToString() => Descripcion;
    }
}
