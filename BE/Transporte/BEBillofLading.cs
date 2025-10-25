using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEBillofLading : BEEntidad
    {
        #region Propiedades
        public string Numero { get; set; }
        public BEBooking Booking { get; set; }
        public BECliente Cliente { get; set; }
        public BECarga Carga { get; set; }
        public BEContenedor Contenedor { get; set; }
        public BEBuque Buque { get; set; }
        public DateTime FechaEmision { get; set; }
        public string PuertoDestino { get; set; }
        #endregion

        #region Constructores
        public BEBillofLading() { }

        public BEBillofLading(string numero, BEBooking booking, BECliente cliente, BECarga carga,
                              BEContenedor contenedor, BEBuque buque, DateTime fecha, string puertoDestino)
        {
            Numero = numero;
            Booking = booking;
            Cliente = cliente;
            Carga = carga;
            Contenedor = contenedor;
            Buque = buque;
            FechaEmision = fecha;
            PuertoDestino = puertoDestino;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return $"B/L {Numero} - Cliente: {Cliente?.RazonSocial} - Buque: {Buque?.Nombre} - Destino: {PuertoDestino}";
        }
        #endregion
    }
}
