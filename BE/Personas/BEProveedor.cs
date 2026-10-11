
using BE.Dominio;
using BE.ServiciosSubcontratados;
using System.Collections.Generic;

namespace BE.Personas
{
    public class BEProveedor : BEPersona
    {
        #region "Propiedades"
        public string Cuit { get; set; }
        public CondicionFiscal CondicionFiscal { get; set; }
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

        public override string ToString()
        {
            return !string.IsNullOrWhiteSpace(RazonSocial)
                ? RazonSocial
                : $"{Nombre} {Apellido}";
        }
        #endregion
    }
}
