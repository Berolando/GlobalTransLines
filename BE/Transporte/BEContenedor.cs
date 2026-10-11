using BE.Dominio;
using System.Collections.Generic;


namespace BE.Transporte
{
    public class BEContenedor : BEEntidad
    {
        #region Propiedades
        public string Codigo { get; set; } 
        public TipoContenedor Tipo { get; set; }
        public EstadoContenedor Estado { get; set; }
        public RestriccionContenedor Restriccion { get; set; }
        public List<BECarga> Cargas { get; set; }
        public BEBuque Buque { get; set; }
        #endregion

        #region Constructores
        public BEContenedor()
        {
            Cargas = new List<BECarga>();
        }

        public BEContenedor(string codigo, TipoContenedor tipo, EstadoContenedor estado)
            : this()
        {
            Codigo = codigo;
            Tipo = tipo;
           
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
