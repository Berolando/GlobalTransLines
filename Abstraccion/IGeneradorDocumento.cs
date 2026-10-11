using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraccion
{
    // Contrato para clases que generan documentos a partir de una entidad del sistema.
    public interface IGeneradorDocumento<T>
    {
        void GenerarDocumento(string rutaDestino, T entidad);
    }
}
