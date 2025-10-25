using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraccion
{
    public interface IGestor<T> where T : IEntidad
    {
        void Guardar(T objeto);          // Alta + Modificación
        void Eliminar(T objeto);         // Baja
        List<T> ListarTodo();            // Listar todos
        T ListarObjeto(T objeto);        // Buscar por Id u otro criterio
        long ObtenerUltimoId();
    }
}
