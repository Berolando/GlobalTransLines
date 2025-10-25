using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraccion;

namespace BE
{
    public abstract class BEEntidad:IEntidad
    {
        public long Id { get; set; }
       
    }
}
