using System;
using System.Collections.Generic;
using Abstraccion;
using BE.Enums;

namespace BE.Transversal
{
    public class BEPermiso : BEComposite
    {
        // Propiedades adicionales
        public string Descripcion { get; set; }
        public PermisoEnum Tipo { get; set; }
        public bool Activo { get; set; } = true;

        // Constructores
        public BEPermiso() { }

        public BEPermiso(string nombre, string descripcion, PermisoEnum tipo, bool activo = true)
            : base(nombre)
        {
            Descripcion = descripcion;
            Tipo = tipo;
            Activo = activo;
        }

        // Métodos del patrón Composite (Leaf)
        public override void Agregar(IComponente componente)
        {
            throw new InvalidOperationException("Un permiso no puede contener subcomponentes.");
        }

        public override void Eliminar(IComponente componente)
        {
            throw new InvalidOperationException("Un permiso no puede eliminar subcomponentes.");
        }

        public override IList<IComponente> ObtenerHijos()
        {
            return null; // No tiene hijos
        }

        // ToString
        public override string ToString()
        {
            return $"{Nombre} ({Tipo}) - {(Activo ? "Activo" : "Inactivo")}";
        }
    }
}
