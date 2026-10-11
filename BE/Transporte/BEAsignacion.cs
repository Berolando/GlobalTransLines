using System;
using BE.Dominio;

namespace BE.Transporte
{
    public class BEAsignacion : BEEntidad
    {
        #region "Propiedades persistentes"
        public long BookingId { get; set; }
        public long ContenedorId { get; set; }
        public long BuqueId { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public EstadoAsignacion Estado { get; set; }
        #endregion

        #region "Propiedades no persistentes "
        public BEBooking Booking { get; set; }
        public BEContenedor Contenedor { get; set; }
        public BEBuque Buque { get; set; }
        #endregion

        #region "Métodos"
        public override string ToString()
        {
            
            string bookingTxt = Booking != null ? Booking.Numero : $"BK-{BookingId}";
            string contTxt = Contenedor != null ? Contenedor.Codigo : $"CT-{ContenedorId}";
            string buqueTxt = Buque != null ? Buque.Nombre : $"BQ-{BuqueId}";
            string estadoTxt = Estado?.Descripcion ?? "(sin estado)";

            return $"Booking {bookingTxt} → Contenedor {contTxt} → Buque {buqueTxt} ({estadoTxt})";
        }
        #endregion
    }
}
