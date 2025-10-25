using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Enums;

namespace BE
{
    public class BEEstiba :BEEntidad
    {
        #region Propiedades
        public BEContenedor Contenedor { get; set; }
        public List<BECarga> Cargas { get; set; }
        public BEBuque Buque { get; set; }
        public string UbicacionBodega { get; set; }
        public string Layout { get; set; } // Representación simplificada del plan de estiba
        public DateTime FechaPlan { get; set; }
        public EstadoEstibaEnum Estado { get; set; }
        #endregion

        #region Constructores
        public BEEstiba()
        {
            Cargas = new List<BECarga>();
        }

        public BEEstiba(BEContenedor contenedor, BEBuque buque, string ubicacion, DateTime fecha, EstadoEstibaEnum estado)
            : this()
        {
            Contenedor = contenedor;
            Buque = buque;
            UbicacionBodega = ubicacion;
            FechaPlan = fecha;
            Estado = estado;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return $"Estiba {Id} - Contenedor {Contenedor?.Codigo} en {Buque?.Nombre}, Ubicación: {UbicacionBodega}, Estado: {Estado}";
        }
        #endregion
    }
}
