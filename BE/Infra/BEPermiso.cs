using System;
using System.Collections.Generic;

namespace BE.Infra
{
    // Hoja del patrón Composite: permiso simple, sin hijos.
    public class BEPermiso : BEComponent
    {
        #region "Propiedades"

        public string Descripcion { get; set; }

        #endregion

        #region "Constructores"

        public BEPermiso()
        {
        }

        public BEPermiso(string nombre, string descripcion, bool activo = true)
            : base(nombre)
        {
            Descripcion = descripcion;
            Activo = activo;
        }

        #endregion

        #region "Métodos (Composite)"

        public override void Agregar(BEComponent componente)
        {
            throw new InvalidOperationException("Un permiso simple no puede contener subcomponentes.");
        }

        public override void Eliminar(BEComponent componente)
        {
            throw new InvalidOperationException("Un permiso simple no puede eliminar subcomponentes.");
        }

        public override IList<BEComponent> ObtenerHijos()
        {
            // Una hoja no tiene hijos.
            return SubComponentes;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Nombre, Activo ? "Activo" : "Inactivo");
        }

        #endregion
    }
}
