using BE.Dominio;
using BE.ServiciosSubcontratados;
using BE.Transporte;
using System;

namespace BE.Finanzas
{
    public class BERequerimientoCompra : BEEntidad
    {
        #region "Propiedades"

        // Booking confirmado que origino la necesidad de compra.
        public BEBooking BookingOrigen { get; set; }

        // Servicio subcontratado requerido por la operacion.
        public BEServicioSubcontratado Servicio { get; set; }

        public DateTime FechaSolicitud { get; set; }
        public EstadoRequerimientoCompra Estado { get; set; }

        #endregion

        #region "Constructores"

        public BERequerimientoCompra()
        {
            FechaSolicitud = DateTime.Now;
            Estado = EstadoRequerimientoCompra.Pendiente;
        }

        public BERequerimientoCompra(BEBooking bookingOrigen, BEServicioSubcontratado servicio)
            : this()
        {
            BookingOrigen = bookingOrigen;
            Servicio = servicio;
        }

        #endregion

        #region "Metodos"

        public override string ToString()
        {
            return $"Requerimiento Compra - Booking: {BookingOrigen?.Numero} - " +
                   $"Servicio: {Servicio?.Nombre} - Estado: {Estado}";
        }

        #endregion
    }
}
