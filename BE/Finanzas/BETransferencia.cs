using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Finanzas
{
    public class BETransferencia:BEMedioDePago
    {
        #region Propiedades
        public string Banco { get; set; }
        public string CBU { get; set; }
        #endregion

        #region Constructores
        public BETransferencia() { }

        public BETransferencia(string banco, string cbu)
        {
            Banco = banco;
            CBU = cbu;
        }
        #endregion
        #region Metodos

        public override string ToString() 
        {
            return $"Banco {Banco}";
        }

        #endregion

    }
}
