using BE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Finanzas
{
    public class BECotizacion:BEEntidad
    {
        #region "Propiedades"
        public BECliente Cliente { get; set; }
        public List<BECarga> Cargas { get; set; } = new List<BECarga>();
        public List<BEServicioSubcontratado> Servicios { get; set; } = new List<BEServicioSubcontratado>();
        public decimal MontoTotal { get; set; }
        public EstadoCotizacionEnum Estado { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        #endregion

        #region "Constructores"
        public BECotizacion() { }

        public BECotizacion(BECliente cliente, List<BECarga> cargas, List<BEServicioSubcontratado> servicios, decimal montoTotal, DateTime fechaEmision, DateTime fechaVencimiento)
        {
            Cliente = cliente;
            Cargas = cargas ?? new List<BECarga>();
            Servicios = servicios ?? new List<BEServicioSubcontratado>();
            MontoTotal = montoTotal;
            Estado = EstadoCotizacionEnum.Pendiente;
            FechaEmision = fechaEmision;
            FechaVencimiento = fechaVencimiento;
        }
        #endregion

        #region "Métodos"
        public override string ToString()
        {
            return $"Cotización para {Cliente?.Nombre} - Total: {MontoTotal:C} - Estado: {Estado}";
        }
        #endregion
    }
}
