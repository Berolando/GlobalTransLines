using System;
using System.Collections.Generic;

namespace BE.Infra
{
    // Composite del patrón: rol que puede contener permisos y subroles
    public class BERol : BEComponent
    {
        #region "Propiedades"

        public string Descripcion { get; set; }

        #endregion

        #region "Constructores"

        public BERol()
        {
        }

        public BERol(string nombre, string descripcion, bool activo = true)
            : base(nombre)
        {
            Descripcion = descripcion;
            Activo = activo;
        }

        #endregion

        #region "Métodos (Composite)"

        public override void Agregar(BEComponent componente)
        {
            if (componente == null)
                return;

            // Evita agregar dos veces el mismo componente directo.
            if (!SubComponentes.Contains(componente))
                SubComponentes.Add(componente);
        }

        public override void Eliminar(BEComponent componente)
        {
            if (componente == null)
                return;

            SubComponentes.Remove(componente);
        }

        public override IList<BEComponent> ObtenerHijos()
        {
            return SubComponentes;
        }

        public override string ToString()
        {
            string estado = Activo ? "Activo" : "Inactivo";
            return string.Format(
                "Rol: {0} | Estado: {1} | Subcomponentes: {2}",
                Nombre,
                estado,
                SubComponentes != null ? SubComponentes.Count : 0);
        }

        #endregion
    }
}
