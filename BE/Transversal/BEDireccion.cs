using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEDireccion : BEEntidad
    {
        #region Propiedades
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public string CodigoPostal { get; set; }
        public string Puerto { get; set; } 
        #endregion

        #region Constructores
        public BEDireccion() { }

        public BEDireccion(string calle, string numero, string ciudad, string provincia, string pais, string codigoPostal, string puerto = null)
        {
            Calle = calle;
            Numero = numero;
            Ciudad = ciudad;
            Provincia = provincia;
            Pais = pais;
            CodigoPostal = codigoPostal;
            Puerto = puerto;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Puerto))
                return $"{Puerto}, {Ciudad}, {Pais}";
            return $"{Calle} {Numero}, {Ciudad}, {Provincia}, {Pais}";
        }
        #endregion
    }
}
