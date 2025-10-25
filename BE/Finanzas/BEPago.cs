using System;
using BE.Enums;
using BE.Finanzas;

namespace BE.Finanzas
{
    public class BEPago : BEEntidad
    {
        #region "Propiedades"
        public BEFactura Factura { get; set; }
        public DateTime Fecha { get; set; }
        public BEMedioDePago MedioDePago { get; set; }     
        public decimal Importe { get; set; }
        public DateTime FechaPago { get; set; }
        public EstadoPagoEnum Estado { get; set; }
        #endregion

        #region "Constructores"
        public BEPago() { }

        public BEPago(BEFactura factura, BEMedioDePago medioDePago, decimal importe, DateTime fechaPago, EstadoPagoEnum estado)
        {
            Factura = factura;
            MedioDePago = medioDePago;
            Importe = importe;
            FechaPago = fechaPago;
            Estado = estado;
        }
        #endregion

        #region "Métodos"
        public override string ToString()
        {
            return $"Pago {Id} - Factura {Factura?.NumeroFactura} - {MedioDePago?.Nombre} - Importe: {Importe:C} - Estado: {Estado}";
        }
        #endregion
    }
}
