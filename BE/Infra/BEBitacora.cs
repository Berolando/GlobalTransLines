using System;
using System.Collections.Generic;

namespace BE.Infra
{
    // Agrupa eventos para consultar una bitácora por día, usuario u operación crítica.
    // La fuente operativa de auditoría son los eventos individuales persistidos en Eventos.xml.
    public class BEBitacora : BEEntidad
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
