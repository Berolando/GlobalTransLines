using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Abstraccion
{
    public interface IRepositorio<T> where T : IEntidad
    {
        void Alta(T obj);
        void Baja(T obj);
        void Modificar(T obj);
        List<T> Listar();
        T BuscarPorId(long id);
        long ObtenerUltimoId();
    }
}
