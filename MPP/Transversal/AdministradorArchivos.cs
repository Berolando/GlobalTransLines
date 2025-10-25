using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Transversal
{
    internal class AdministradorArchivos
    {
        public static bool Existe(string rutaCarpeta)
        {
            return Directory.Exists(rutaCarpeta);
        }

        public static void Crear(string rutaCarpeta)
        {
            if (!Existe(rutaCarpeta))
                Directory.CreateDirectory(rutaCarpeta);
        }

        public static void Borrar(string rutaCarpeta)
        {
            if (Existe(rutaCarpeta))
                Directory.Delete(rutaCarpeta, true);
        }
    }
}
