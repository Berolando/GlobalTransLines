using BE.Dominio;
using System;

namespace BE.Infra
{
    // Representa un hecho auditable del sistema: login, backup, restore, error u operación relevante.
    public class BEEvento : BEEntidad
    {
        #region "Propiedades"

        public DateTime Fecha { get; set; }
        public BEUsuario Usuario { get; set; }
        public string Descripcion { get; set; }
        public TipoOperacion Tipo { get; set; }
        public string Origen { get; set; }

        // Propiedad auxiliar para persistir/reconstruir el usuario desde XML usando solo el Id.
        public long UsuarioId
        {
            get
            {
                return Usuario != null ? Usuario.Id : 0;
            }
            set
            {
                if (Usuario == null)
                    Usuario = new BEUsuario();

                Usuario.Id = value;
            }
        }

        #endregion

        #region "Constructores"

        public BEEvento() { }

        public BEEvento(DateTime fecha, BEUsuario usuario, string descripcion, TipoOperacion tipo, string origen)
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
