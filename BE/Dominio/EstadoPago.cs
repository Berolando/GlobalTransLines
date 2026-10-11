using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class EstadoPago
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        private EstadoPago(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }
        
        public static EstadoPago Pendiente = new EstadoPago("PEN", "Pendiente");
        public static EstadoPago Confirmado = new EstadoPago("CON", "Confirmado");
        public static EstadoPago Pagado = new EstadoPago("PAG", "Pagado");
        public static EstadoPago Rechazado = new EstadoPago("RECH", "Rechazado");

        
        public static IEnumerable<EstadoPago> ObtenerTodos()
        {
            return new[] { Pendiente, Confirmado, Pagado, Rechazado };
        }

       
        public override string ToString()
        {
            return Descripcion;
        }

     
        public static EstadoPago ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(x => x.Codigo == codigo);
        }
    }
}