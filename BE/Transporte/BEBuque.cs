using BE.Dominio;
using BE.Infra;
using System.Collections.Generic;

namespace BE.Transporte
{
    public class BEBuque : BEEntidad
    {
        #region Propiedades
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string CodigoIMO { get; set; }
        public int CapacidadTEU { get; set; }   
        public decimal CapacidadPeso { get; set; } // Capacidad máxima en toneladas
        public decimal Disponibilidad { get; set; } // Peso disponible actual
        public EstadoBuque Estado { get; set; }
        public List<long> ItinerarioPuertoIds { get; set; }
        public List<BEDireccion> Itinerario { get; set; }
        #endregion

        #region Constructores
        public BEBuque()
        {
            ItinerarioPuertoIds = new List<long>();
            Itinerario = new List<BEDireccion>();
        }

        public BEBuque(string nombre, string codigoIMO, int capacidadTEU, decimal capacidadPeso, EstadoBuque estado)
            : this()
        {
            Nombre = nombre;
            CodigoIMO = codigoIMO;
            CapacidadTEU = capacidadTEU;
            CapacidadPeso = capacidadPeso;
            Estado = estado;
           
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return $"Buque {Nombre} (IMO: {CodigoIMO}) - Capacidad: {CapacidadTEU} TEU / {CapacidadPeso} tn - Estado: {Estado}";
        }
        #endregion
    }
}
