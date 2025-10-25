using BE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEProveedor:BEPersona
    {
        #region "Propiedades"
        public string Cuit { get; set; }
        public CondicionFiscalEnum CondicionFiscal { get; set; }
        public string RazonSocial { get; set; }
        public List<BEServicioSubcontratado> Servicios { get; set; } = new List<BEServicioSubcontratado>();
        #endregion

        #region "Constructores"
        public BEProveedor() { }
        #endregion

        #region "Métodos"
        public override string ObtenerContacto()
        {
            return $"{RazonSocial} - CUIT: {Cuit} - Tel: {Telefono} - Email: {Email}";
        }
        #endregion
    }
}
