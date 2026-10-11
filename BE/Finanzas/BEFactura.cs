using BE.Dominio;
using BE.Transporte;
using System;

namespace BE.Finanzas
{
    public class BEFactura : BEEntidad
    {
        #region "Propiedades"
        public string NumeroFactura { get; set; }
        public BEBooking Booking { get; set; }
        public EstadoFactura Estado { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Subtotal { get; set; }
        public TipoFactura Tipo { get; set; }
        public decimal Total { get; set; }

        #endregion

        #region "Constructores"
        public BEFactura() { }

        public BEFactura(string numeroFactura, BEBooking booking, DateTime fecha, decimal subtotal, decimal impuesto, TipoFactura tipo)
        {
            NumeroFactura = numeroFactura;
            Booking = booking;
            Estado = EstadoFactura.Emitida;
            Fecha = fecha;
            Subtotal = subtotal;
            Impuesto = impuesto;
            Tipo = tipo;
            Total = subtotal + impuesto;
        }
        #endregion

        #region "Métodos"
        public override string ToString()
        {
            return $"Factura {Tipo}, Factura Numero {NumeroFactura} - Booking {Booking?.Id} - Total: {Total:C} - Estado: {Estado}";
        }
        #endregion
    }
}
