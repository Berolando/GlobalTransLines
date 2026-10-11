using BE.Dominio;
using BE.Personas;
using BE.ServiciosSubcontratados;
using BE.Finanzas; 
using System;
using System.Collections.Generic;

namespace BE.Transporte
{
    public class BEBooking : BEEntidad
    {
        #region "Propiedades"

        public string Numero { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public BECliente Cliente { get; set; }
        public BECotizacion Cotizacion { get; set; }  

        public string TipoContenedorCodigo { get; set; }   // Ej.: "20STD", "40HC", "20REF"
        public DateTime? FechaTentativaSalida { get; set; } // Ventana estimada de salida

        public List<BECarga> Cargas { get; set; }
        public List<BEServicioSubcontratado> Servicios { get; set; }

        public EstadoBooking Estado { get; set; }

        #endregion

        #region "Constructores"

        public BEBooking()
        {
            Cargas = new List<BECarga>();
            Servicios = new List<BEServicioSubcontratado>();
        }

        public BEBooking(string numero, BECliente cliente, DateTime fechaGeneracion, EstadoBooking estado)
            : this()
        {
            Numero = numero;
            Cliente = cliente;
            FechaGeneracion = fechaGeneracion;
            Estado = estado;
        }

        #endregion

        #region "Métodos"

        public override string ToString()
        {
            return $"Booking Nº {Numero} | Cliente: {Cliente?.RazonSocial ?? "(sin cliente)"} | " +
                   $"Tipo contenedor: {TipoContenedorCodigo} | " +
                   $"Fecha tentativa: {FechaTentativaSalida?.ToShortDateString()} | " +
                   $"Estado: {Estado?.Descripcion ?? "(sin estado)"}";
        }

        #endregion
    }
}
