using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class CargoEmpleado
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }
        private CargoEmpleado(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }
        public static CargoEmpleado Director = new CargoEmpleado("DIR", "Director General");
        public static CargoEmpleado AsesorComercial = new CargoEmpleado("AC", "Asesor Comercial ");
        public static CargoEmpleado JefeVentas = new CargoEmpleado("JV", "Jefe de Ventas y Cotizaciones");
        public static CargoEmpleado PlanificadorCarga = new CargoEmpleado("PC", "Planificador de Carga");
        public static CargoEmpleado JefePlanificacion = new CargoEmpleado("JP", "Jefe de Planificación Naviera");
        public static CargoEmpleado AnalistaFacturacion = new CargoEmpleado("AF", "Analista de Facturación y Cobranzas");
        public static CargoEmpleado AnalistaCompras = new CargoEmpleado("ACR", "Analista de Compras y Proveedores");
       
        public static IEnumerable<CargoEmpleado> ObtenerTodos()
        {
            return new[] { Director, AsesorComercial, JefeVentas, PlanificadorCarga, JefePlanificacion, AnalistaFacturacion, AnalistaCompras };
        }

        public override string ToString()
        {
            return Descripcion;
        }
        public static CargoEmpleado ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(x => x.Codigo == codigo);
        }
    }
}

