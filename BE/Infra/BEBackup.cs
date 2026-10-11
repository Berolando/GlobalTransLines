using BE.Dominio;
using System;

namespace BE.Infra
{
    // Registra los datos de un backup generado por el sistema.
    public class BEBackup : BEEntidad
    {
        #region "Propiedades"

        public DateTime Fecha { get; set; }
        public long UsuarioId { get; set; }
        public string Usuario { get; set; }
        public string RutaArchivo { get; set; }
        public string Descripcion { get; set; }
        public TipoOperacion TipoOperacion { get; set; }

        #endregion

        #region "Constructores"

        public BEBackup() { }

        public BEBackup(DateTime fecha, string usuario, string rutaArchivo, string descripcion, TipoOperacion tipoOperacion)
        {
            Fecha = fecha;
            Usuario = usuario;
            RutaArchivo = rutaArchivo;
            Descripcion = descripcion;
            TipoOperacion = tipoOperacion;
        }

        public BEBackup(DateTime fecha, long usuarioId, string usuario, string rutaArchivo, string descripcion, TipoOperacion tipoOperacion)
            : this(fecha, usuario, rutaArchivo, descripcion, tipoOperacion)
        {
            UsuarioId = usuarioId;
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
