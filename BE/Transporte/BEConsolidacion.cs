using BE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Transporte
{
    public class BEConsolidacion : BEEntidad
    {
        #region "Propiedades"
        public BEContenedor Contenedor { get; set; }
        public List<BECarga> Cargas { get; set; } = new List<BECarga>();
        public double CapacidadOcupada { get; set; }
        public double CapacidadTotal { get; set; }
        public EstadoConsolidacionEnum Estado { get; set; }
        public DateTime FechaConsolidacion { get; set; }
        #endregion

        #region "Constructores"
        public BEConsolidacion() { }

        public BEConsolidacion(BEContenedor contenedor, double capacidadTotal)
        {
            Contenedor = contenedor;
            CapacidadTotal = capacidadTotal;
            Estado = EstadoConsolidacionEnum.Pendiente;
            FechaConsolidacion = DateTime.Now;
        }
        #endregion
    }
}
