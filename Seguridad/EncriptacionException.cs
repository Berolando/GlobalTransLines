using System;

namespace Seguridad
{
    public class EncriptacionException : Exception
    {
        public EncriptacionException(string mensaje, Exception inner = null)
            : base(mensaje, inner) { }
    }

}
