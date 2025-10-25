using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seguridad
{
    public class EncriptacionException : Exception
    {
        public EncriptacionException(string mensaje, Exception inner = null)
            : base(mensaje, inner) { }
    }
    
}
