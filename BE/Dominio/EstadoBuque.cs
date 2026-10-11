using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class EstadoBuque
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }


        private EstadoBuque(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }

        public static EstadoBuque Disponible = new EstadoBuque("DIS", "Disponible");// Puede recibir asignaciones de carga
        public static EstadoBuque EnRuta = new EstadoBuque("RUT", "En Ruta"); // Está navegando
        public static EstadoBuque Mantenimiento = new EstadoBuque("MAN", "Mantenimiento");

        public static IEnumerable<EstadoBuque> ObtenerTodos()
        {
            return new[] { Disponible, EnRuta, Mantenimiento };
        }
        public override string ToString()
        {
            return Descripcion;
        }

        public static EstadoBuque ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(x => x.Codigo == codigo);
        }

    }

}


