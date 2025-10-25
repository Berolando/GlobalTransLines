using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Finanzas
{
    public class BEEfectivo: BEMedioDePago
    {
        #region "Propiedades"
        public double MontoEntregado { get; set; }
        public string Moneda { get; set; }   // Ej: "ARS", "USD", etc.
        public DateTime FechaPago { get; set; }
        public decimal Vuelto { get; set; }
        #endregion

        #region "Constructores"
        public BEEfectivo() { }

        public BEEfectivo(double montoEntregado, string moneda, DateTime fechaPago, decimal vuelto = 0)
        {
            MontoEntregado = montoEntregado;
            Moneda = moneda;
            FechaPago = fechaPago;
            Vuelto = vuelto;
        }
        #endregion

        #region Metodos
        public override string ToString()
        {
            return $"{Nombre} - Monto: {MontoEntregado:C} - Fecha: {FechaPago:d}";
        }
        #endregion
    }
}
