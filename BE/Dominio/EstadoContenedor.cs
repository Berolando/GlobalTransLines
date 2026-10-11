using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class EstadoContenedor
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }

        private EstadoContenedor(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }

        
        public static readonly EstadoContenedor Disponible = new EstadoContenedor("DIS", "Disponible");
        public static readonly EstadoContenedor Asignado = new EstadoContenedor("ASI", "Asignado");
        public static readonly EstadoContenedor Consolidado = new EstadoContenedor("CON", "Consolidado");
        public static readonly EstadoContenedor EnRuta = new EstadoContenedor("ENR", "En Ruta");
        public static readonly EstadoContenedor Mantenimiento = new EstadoContenedor("MAN", "Mantenimiento");

        public static IEnumerable<EstadoContenedor> ObtenerTodos()
        {
            return new[] { Disponible, Asignado, Consolidado, EnRuta, Mantenimiento };
        }

        
        public bool EsDisponible() => this == Disponible;
        public bool EsAsignado() => this == Asignado;
        public bool EsConsolidado() => this == Consolidado;
        public bool EsOperativo() => this == Disponible || this == Asignado || this == Consolidado;

        public override string ToString() => Descripcion;

        public static EstadoContenedor ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(e => e.Codigo == codigo);
        }
    }
}
