using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BEServicioSubcontratado:BEEntidad
    {
        #region Propiedades
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public BEProveedor Proveedor { get; set; }
        #endregion

        #region Constructores
        public BEServicioSubcontratado() { }

        public BEServicioSubcontratado(string nombre, string descripcion, decimal costo, BEProveedor proveedor)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Costo = costo;
            Proveedor = proveedor;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return $"Servicio {Nombre} - Proveedor: {Proveedor?.RazonSocial} - Costo: {Costo:C}";
        }
        #endregion
    }
}
