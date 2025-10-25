using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Transversal;

namespace BE
{
    public abstract class BEPersona:BEEntidad
    {
        #region "Propiedades"

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }

        public BEDireccion Direccion { get; set; }

        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }

        #endregion

        #region "Constructores"
        public BEPersona() { }

        public BEPersona( string pNombre, string pApellido, string pDNI, BEDireccion pDireccion, string pTelefono, string pEmail, DateTime pFechaNac )
        {
            Nombre = pNombre;
            Apellido = pApellido;
            DNI = pDNI;
            Telefono = pTelefono;
            Direccion = pDireccion;
            Email = pEmail;
            FechaNacimiento = pFechaNac;
           
          
        }

        #endregion

        #region "Métodos"

        public virtual string ObtenerContacto()
        {
            return $"{Nombre} - Tel: {Telefono}, Email: {Email}";
        }
        #endregion
    }
}
