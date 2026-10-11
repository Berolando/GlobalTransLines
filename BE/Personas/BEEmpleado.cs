using BE.Infra;
using BE.Dominio;

namespace BE.Personas
{
    public class BEEmpleado : BEPersona
    {
        #region "Propiedades"
        
        public CargoEmpleado cargoEmpleado { get; set; }
       
        public double Sueldo { get; set; }

        public string RutaFoto { get; set; }
       
        #endregion

        #region "Constructores"
        public BEEmpleado() { }
        #endregion

        #region "Métodos"
        public override string ObtenerContacto()
        {
            return $"{Nombre} {Apellido} - Cargo: {cargoEmpleado} - Tel: {Telefono} - Email: {Email}";
        }
        #endregion
    }
}
