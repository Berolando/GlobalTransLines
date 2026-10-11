
using System.Collections.Generic;

namespace BE.Infra
{
    // Base del patrón Composite: permite tratar roles y permisos con un contrato común.
    public abstract class BEComponent 
    {
        #region Propiedades

        public long Id { get; set; }
        public bool Activo { get; set; }
        public string Nombre { get; set; }

        // Hijos del componente: en roles pueden ser permisos o subroles.
        public List<BEComponent> SubComponentes { get; protected set; }
        #endregion

        #region Constructores
        protected BEComponent()
        {
            SubComponentes = new List<BEComponent>();
        }

        protected BEComponent(string nombre)
            : this()
        {
            Nombre = nombre;
        }
        #endregion

        #region "Operaciones del patrón Composite"

        public abstract void Agregar(BEComponent componente);
        public abstract void Eliminar(BEComponent componente);
        public abstract IList<BEComponent> ObtenerHijos();

        public override string ToString()
        {
            return string.Format("{0}: {1} (SubComponentes: {2})",
                GetType().Name,
                Nombre,
                SubComponentes != null ? SubComponentes.Count : 0);
        }

        #endregion
    }
}
