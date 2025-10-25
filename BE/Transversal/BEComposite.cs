using Abstraccion;
using System;
using System.Collections.Generic;

namespace BE.Transversal
{
    public abstract class BEComposite : BEEntidad, IComponente
    {
        #region Propiedades

        public string Nombre { get; set; }
       
        public List<IComponente> SubComponentes { get; set; }
        #endregion

        #region Constructores
        protected BEComposite()
        {
            
            SubComponentes = new List<IComponente>();
        }

        protected BEComposite(string nombre) : this()
        {
            Nombre = nombre;
        }
        #endregion

        #region Métodos
        public abstract void Agregar(IComponente componente);
        public abstract void Eliminar(IComponente componente);
        public abstract IList<IComponente> ObtenerHijos();

        public override string ToString()
        {
            return $"{GetType().Name}: {Nombre} (SubComponentes: {SubComponentes?.Count ?? 0})";
        }
        #endregion
    }
}
