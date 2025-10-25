using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MPP.Transversal
{
    internal class AdministradorArchivosIndividuales
    {
        public static bool Existe(string rutaArchivo)
        {
            return File.Exists(rutaArchivo);
        }

        public static void Crear(string rutaArchivo)
        {
            if (!Existe(rutaArchivo))
            {
                using (File.Create(rutaArchivo)) { }
            }
        }

        public static void Borrar(string rutaArchivo)
        {
            if (Existe(rutaArchivo))
                File.Delete(rutaArchivo);
        }
    }
}
