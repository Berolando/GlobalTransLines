using BE.Dominio;
using BE.Personas;
using BE.ServiciosSubcontratados;
using BE.Transporte;
using System;
using System.Collections.Generic;

namespace BE.Finanzas
{
    public class BEOrdenDeCompra : BEEntidad
    {
        #region "Propiedades"
        public string Numero { get; set; }
        public BEProveedor Proveedor { get; set; }
        public BEBooking BookingOrigen { get; set; }  
        public List<BEServicioSubcontratado> Servicios { get; set; } = new List<BEServicioSubcontratado>();
        public DateTime FechaEmision { get; set; }
        public EstadoOrdenCompra Estado { get; set; }
        public decimal ImporteTotal { get; set; }
        #endregion

        #region "Constructores"
        public BEOrdenDeCompra() { }

        public BEOrdenDeCompra(string numero, BEProveedor proveedor, BEBooking bookingOrigen,
                               List<BEServicioSubcontratado> servicios, DateTime fecha,
                                decimal importeTotal)
        {
            Numero = numero;
            Proveedor = proveedor;
            BookingOrigen = bookingOrigen;
            Servicios = servicios ?? new List<BEServicioSubcontratado>();
            FechaEmision = fecha;
            Estado = EstadoOrdenCompra.Emitida;
            ImporteTotal = importeTotal;
        }
        #endregion

        #region "Métodos"
        public override string ToString()
        {
            return $"Orden de Compra Nº {Numero} - Proveedor: {Proveedor?.RazonSocial} - " +
                   $"Booking: {BookingOrigen?.Numero} - Total: {ImporteTotal:C} - Estado: {Estado}";
        }
        #endregion
    }
}
