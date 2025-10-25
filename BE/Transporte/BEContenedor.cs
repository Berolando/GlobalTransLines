using BE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BE
{
    public class BEContenedor : BEEntidad
    {
        #region Propiedades
        public string Codigo { get; set; } // Ej: "MSCU1234567"
        public TipoContenedorEnum Tipo { get; set; }
        public decimal CapacidadPeso { get; set; }    // toneladas
        public decimal CapacidadVolumen { get; set; } // m3
        public EstadoContenedorEnum Estado { get; set; }
        public List<BECarga> Cargas { get; set; }
        public BEBuque Buque { get; set; }
        #endregion

        #region Constructores
        public BEContenedor()
        {
            Cargas = new List<BECarga>();
        }

        public BEContenedor(string codigo, TipoContenedorEnum tipo, decimal capacidadPeso, decimal capacidadVolumen, EstadoContenedorEnum estado)
            : this()
        {
            Codigo = codigo;
            Tipo = tipo;
            CapacidadPeso = capacidadPeso;
            CapacidadVolumen = capacidadVolumen;
            Estado = estado;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return $"Contenedor {Codigo} - {Tipo} - Estado: {Estado}";
        }
        #endregion
    }
}
