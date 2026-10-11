using BE.Dominio;


namespace BE.Personas
{
    public class BECliente : BEPersona
    {
        #region "Propiedades"
        public string Cuit { get; set; }
        public CondicionFiscal CondicionFiscal { get; set; }
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
        public override string ToString()
        {
            return !string.IsNullOrWhiteSpace(RazonSocial)
                ? RazonSocial
                : $"{Nombre} {Apellido}";
        }
        #endregion
    }
}
