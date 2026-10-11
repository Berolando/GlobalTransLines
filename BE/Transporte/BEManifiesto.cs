using System;
using System.Collections.Generic;

namespace BE.Transporte
{
    public class BEManifiesto : BEEntidad
    {
        #region Propiedades
        public string Numero { get; set; }                // Identificador único del manifiesto
        public BEBuque Buque { get; set; }                // Buque asociado
        public DateTime FechaEmision { get; set; }        // Fecha de emisión del manifiesto
        public List<BEContenedor> Contenedores { get; set; } // Contenedores incluidos en el manifiesto
        public string AutoridadDestino { get; set; }      // Aduana / Puerto destino
        public decimal PesoTotal { get; set; }            // Peso total de todas las cargas
        #endregion

        #region Constructores
        public BEManifiesto()
        {
            Contenedores = new List<BEContenedor>();
        }

        public BEManifiesto(string numero, BEBuque buque, DateTime fecha, string autoridadDestino)
            : this()
        {
            Numero = numero;
            Buque = buque;
            FechaEmision = fecha;
            AutoridadDestino = autoridadDestino;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return $"Manifiesto {Numero} - Buque: {Buque?.Nombre} - Fecha: {FechaEmision.ToShortDateString()} - Contenedores: {Contenedores.Count}";
        }
        #endregion
    }
}
