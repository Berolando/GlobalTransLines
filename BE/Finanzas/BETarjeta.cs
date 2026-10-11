using System;

namespace BE.Finanzas
{
    public class BETarjeta : BEMedioDePago
    {
        #region Propiedades

        public string Numero { get; set; }
        public string Titular { get; set; }
        public DateTime Vencimiento { get; set; }
        public string CVV { get; set; }
        public string Marca { get; set; } // Visa, MasterCard, etc.
        public string BancoEmisor { get; set; }
        #endregion

        #region Constructores
        public BETarjeta() { }
        public BETarjeta(string numero, string titular, DateTime vencimiento, string cVV, string tipo, string bancoEmisor)
        {
            Numero = numero;
            Titular = titular;
            Vencimiento = vencimiento;
            CVV = cVV;
            Marca = tipo;
            BancoEmisor = bancoEmisor;
        }


        #endregion

        #region

        public override string ToString()
        {
            string ultimos4 = Numero != null && Numero.Length >= 4
                ? Numero.Substring(Numero.Length - 4)
                : "****";

            return $"{Titular} - N° ****{ultimos4} - Vence: {Vencimiento:MM/yy}";
        }
        #endregion
    }
}
