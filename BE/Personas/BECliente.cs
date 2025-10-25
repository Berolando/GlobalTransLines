using BE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BECliente:BEPersona
    {
        #region "Propiedades"
        public string Cuit { get; set; }
        public CondicionFiscalEnum CondicionFiscal { get; set; }
        public string RazonSocial { get; set; }
        #endregion

        #region "Constructores"
        public BECliente() { }
        #endregion

        #region "Métodos"
        public override string ObtenerContacto()
        {
            return $"{RazonSocial} - CUIT: {Cuit} - Tel: {Telefono} - Email: {Email}";
        }
        #endregion
    }
}
