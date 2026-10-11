using BE.Dominio;
using BE.Personas;
using BE.ServiciosSubcontratados;
using BE.Transporte;
using System;
using System.Collections.Generic;

namespace BE.Finanzas
{
    public class BECotizacion : BEEntidad
    {
        #region "Propiedades"

        public string Numero { get; set; }
        public BECliente Cliente { get; set; }
        public List<BECarga> Cargas { get; set; } = new List<BECarga>();
        public List<BEServicioSubcontratado> Servicios { get; set; } = new List<BEServicioSubcontratado>();
        public TipoContenedor TipoContenedor { get; set; }
        public decimal MontoTotal { get; set; }
        public EstadoCotizacion Estado { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        #endregion

        #region "Constructores"
        public BECotizacion() { }

      public BECotizacion(BECliente cliente, TipoContenedor contenedor, DateTime fechaEmision, DateTime fechaVencimiento, EstadoCotizacion estado)
        {
            Cliente = cliente;
            TipoContenedor = contenedor;
            FechaEmision = fechaEmision;
            FechaVencimiento = fechaVencimiento;
            Estado = estado;
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
