using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Transversal
{
    public class BEBitacora:BEEntidad
    {
        #region Propiedades
        public DateTime FechaCreacion { get; set; }
        public BEUsuario UsuarioResponsable { get; set; } 
        public List<BEEvento> Eventos { get; set; }
        #endregion

        #region Constructores
        public BEBitacora()
        {
            Eventos = new List<BEEvento>();
            FechaCreacion = DateTime.Now;
        }

        public BEBitacora(BEUsuario responsable) : this()
        {
            UsuarioResponsable = responsable;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return $"Bitácora {Id} - Eventos registrados: {Eventos.Count}";
        }
        #endregion
    }
}
