using BE.Enums;
using System;

namespace BE.Transversal
{
    public class BEBackup : BEEntidad
    {
        #region "Propiedades"

        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public string RutaArchivo { get; set; }
        public string Descripcion { get; set; }
        public TipoOperacionEnum TipoOperacion { get; set; }

        #endregion

        #region "Constructores"

        public BEBackup() { }

        public BEBackup(DateTime fecha, string usuario, string rutaArchivo, string descripcion, TipoOperacionEnum tipoOperacion)
        {
            Fecha = fecha;
            Usuario = usuario;
            RutaArchivo = rutaArchivo;
            Descripcion = descripcion;
            TipoOperacion = tipoOperacion;
        }

        #endregion

        #region "Métodos"

        public override string ToString()
        {
            return $"{Fecha:dd/MM/yyyy HH:mm} - {Usuario} - {TipoOperacion} - {Descripcion}";
        }

        #endregion
    }
}
