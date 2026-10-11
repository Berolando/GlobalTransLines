using System.Collections.Generic;

namespace Abstraccion
{
    // Define las operaciones de negocio que expone cada BLL hacia la UI.
    public interface IGestor<T> where T : IEntidad
    {
        void Guardar(T objeto);
        void Eliminar(T objeto);
        List<T> ListarTodo();
        T ListarObjeto(T objeto);
        long ObtenerUltimoId();
    }
}
