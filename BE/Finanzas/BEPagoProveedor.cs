using System;
using BE.Enums;
using BE.Finanzas;

namespace BE.Finanzas
{
    public class BEPagoProveedor : BEEntidad
    {
        #region "Propiedades"
        public BEOrdenDeCompra OrdenDeCompra { get; set; }   
        public BEMedioDePago MedioDePago { get; set; }     
        public decimal Importe { get; set; }                  
        public DateTime FechaPago { get; set; }             
        public EstadoPagoEnum Estado { get; set; }           
        #endregion

        #region "Constructores"
        public BEPagoProveedor() { }

        public BEPagoProveedor(BEOrdenDeCompra orden, BEMedioDePago medio, decimal importe, DateTime fecha, EstadoPagoEnum estado)
        {
            OrdenDeCompra = orden;
            MedioDePago = medio;
            Importe = importe;
            FechaPago = fecha;
            Estado = estado;
        }
        #endregion

        #region "Métodos"
        public override string ToString()
        {
            return $"Pago a Proveedor (OC {OrdenDeCompra?.Numero}) - {MedioDePago?.Nombre} - Importe: {Importe:C} - Estado: {Estado}";
        }
        #endregion
    }
}
