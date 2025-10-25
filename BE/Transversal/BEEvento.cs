using BE.Enums;
using System;

namespace BE.Transversal
{
    public class BEEvento : BEEntidad
    {
        #region "Propiedades"

        public DateTime Fecha { get; set; }
        public BEUsuario Usuario { get; set; }
        public string Descripcion { get; set; }
        public TipoOperacionEnum Tipo { get; set; }
        public string Origen { get; set; }

        #endregion

        #region "Constructores"

        public BEEvento() { }

        public BEEvento(DateTime fecha, BEUsuario usuario, string descripcion, TipoOperacionEnum tipo, string origen)
        {
            Fecha = fecha;
            Usuario = usuario;
            Descripcion = descripcion;
            Tipo = tipo;
            Origen = string.IsNullOrWhiteSpace(origen) ? "Sistema" : origen;
        }

        #endregion

        #region "Métodos"

        public override string ToString()
        {
            return $"[{Tipo}] {Fecha:dd/MM/yyyy HH:mm} - {Origen} - {Descripcion} (Usuario: {Usuario?.Username})";
        }

        #endregion
    }
}
