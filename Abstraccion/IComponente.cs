using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraccion
{
    public interface IComponente
    {
        long Id { get; }
        string Nombre { get; }
        void Agregar(IComponente componente);
        void Eliminar(IComponente componente);

        IList<IComponente> ObtenerHijos();
    }
}
