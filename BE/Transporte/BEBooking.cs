using BE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEBooking:BEEntidad
    {
        #region Propiedades
        public string Numero { get; set; }
        public BECliente Cliente { get; set; }
        public List<BECarga> Cargas { get; set; }
        public List<BEServicioSubcontratado> Servicios { get; set; }
        public BEContenedor Contenedor { get; set; }
        public BEBuque Buque { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public EstadoBookingEnum Estado { get; set; }
        #endregion

        #region Constructores
        public BEBooking()
        {
            Cargas = new List<BECarga>();
            Servicios = new List<BEServicioSubcontratado>();
        }

        public BEBooking(string numero, BECliente cliente, DateTime fecha, EstadoBookingEnum estado)
            : this()
        {
            Numero = numero;
            Cliente = cliente;
            FechaGeneracion = fecha;
            Estado = estado;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return $"Booking {Numero} - Cliente: {Cliente?.RazonSocial} - Estado: {Estado}";
        }
        #endregion
    }
}
