using BE.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEEmpleado:BEPersona
    {
        #region "Propiedades"
        public string Cargo { get; set; }
        public string Sector { get; set; }
        public double Sueldo { get; set; }
        public BEUsuario Usuario { get; set; }   // Relación con seguridad
        #endregion

        #region "Constructores"
        public BEEmpleado() { }
        #endregion

        #region "Métodos"
        public override string ObtenerContacto()
        {
            return $"{Nombre} {Apellido} - Cargo: {Cargo} - Tel: {Telefono} - Email: {Email}";
        }
        #endregion
    }
}
