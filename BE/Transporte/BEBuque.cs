using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Enums;

namespace BE
{
    public class BEBuque : BEEntidad
    {
        #region Propiedades
        public string Nombre { get; set; }
        public string CodigoIMO { get; set; }
        public int CapacidadTEU { get; set; }   // Capacidad en contenedores de 20'
        public decimal CapacidadPeso { get; set; } // Capacidad máxima en toneladas
        public decimal Disponibilidad { get; set; } // Peso disponible actual
        public EstadoBuqueEnum Estado { get; set; }
        public List<string> Itinerarios { get; set; }
        #endregion

        #region Constructores
        public BEBuque()
        {
            Itinerarios = new List<string>();
        }

        public BEBuque(string nombre, string codigoIMO, int capacidadTEU, decimal capacidadPeso, EstadoBuqueEnum estado)
            : this()
        {
            Nombre = nombre;
            CodigoIMO = codigoIMO;
            CapacidadTEU = capacidadTEU;
            CapacidadPeso = capacidadPeso;
            Estado = estado;
            Disponibilidad = capacidadPeso; // al inicio el buque está libre
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
