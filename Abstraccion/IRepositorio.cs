using System.Collections.Generic;



namespace Abstraccion
{
    // Define las operaciones básicas de persistencia que implementan los MPP.
    public interface IRepositorio<T> where T : IEntidad
    {
        // Alta, baja y modificación sobre el origen de datos.
        void Alta(T obj);
        void Baja(T obj);
        void Modificar(T obj);

        // Consultas comunes para listar, buscar y generar nuevos IDs.
        List<T> Listar();
        T BuscarPorId(long id);
        long ObtenerUltimoId();
    }
}
