using Abstraccion;
using System;
using System.Collections.Generic;

namespace BE.Transversal
{
    public class BERol : BEComposite
    {
        // Propiedades adicionales
        public string Descripcion { get; set; }
        public bool Activo { get; set; } = true;

        // Constructores
        public BERol() { }

        public BERol(string nombre, string descripcion, bool activo = true)
            : base(nombre)
        {
            Descripcion = descripcion;
            Activo = activo;
        }

        // Métodos del patrón Composite 
        public override void Agregar(IComponente componente)
        {
            if (componente != null)
                SubComponentes.Add(componente);
        }

        public override void Eliminar(IComponente componente)
        {
            if (componente != null)
                SubComponentes.Remove(componente);
        }

        public override IList<IComponente> ObtenerHijos()
        {
            return SubComponentes.ToArray();
        }

        // ToString
        public override string ToString()
        {
            string estado = Activo ? "Activo" : "Inactivo";
            return $"Rol: {Nombre} | Estado: {estado} | Subcomponentes: {SubComponentes.Count}";
        }
    }
}
