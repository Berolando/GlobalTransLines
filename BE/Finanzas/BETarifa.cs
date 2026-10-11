using BE.Dominio;
using BE.Infra;
using System;

namespace BE.Finanzas
{
    public class BETarifa : BEEntidad
    {
        #region Propiedades
        public TipoCarga TipoCarga { get; set; }
        public TipoContenedor TipoContenedor { get; set; }
        public string Descripcion { get; set; }
        public decimal BaseUSD { get; set; }
        public decimal CoefPeso { get; set; }
        public decimal CoefVolumen { get; set; }
        #endregion

        #region Constructores
        public BETarifa() { }
       public BETarifa(TipoCarga tipoCarga, TipoContenedor tipoContenedor, string descripcion,
                        decimal baseUSD, decimal coefPeso, decimal coefVolumen)
        {
            TipoCarga = tipoCarga;
            TipoContenedor = tipoContenedor;
            Descripcion = descripcion;
            BaseUSD = baseUSD;
            CoefPeso = coefPeso;
            CoefVolumen = coefVolumen;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return $"{Descripcion} (${BaseUSD:N0} USD base)";
        }
        #endregion
    }
}
