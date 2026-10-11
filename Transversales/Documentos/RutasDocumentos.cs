using System;
using System.IO;
using BE.Dominio;

namespace Transversales.Documentos
{
    public class RutasDocumentos
    {
        private readonly string raiz;

        public RutasDocumentos(string raizDocumentos = null)
        {
            raiz = string.IsNullOrWhiteSpace(raizDocumentos)
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Documentos")
                : raizDocumentos;

            if (!Directory.Exists(raiz))
                Directory.CreateDirectory(raiz);
        }

        public string ObtenerRutaPdf(DocumentoTipo tipoDocumento, string numeroDocumento, bool usarTimestamp = false, DateTime? fecha = null)
        {
            if (tipoDocumento == null) throw new ArgumentNullException(nameof(tipoDocumento));
            if (string.IsNullOrWhiteSpace(numeroDocumento)) throw new ArgumentException("Número obligatorio.", nameof(numeroDocumento));

            string carpetaTipo = SanearNombre(tipoDocumento.Descripcion);
            string nombreArchivo = SanearNombre(numeroDocumento);

            if (usarTimestamp)
            {
                string stamp = (fecha ?? DateTime.Now).ToString("yyyyMMdd_HHmmss");
                nombreArchivo += "_" + stamp;
            }

            string carpetaFinal = Path.Combine(raiz, carpetaTipo);

            if (!Directory.Exists(carpetaFinal))
                Directory.CreateDirectory(carpetaFinal);

            return Path.Combine(carpetaFinal, nombreArchivo + ".pdf");
        }

        public bool ExistePdf(DocumentoTipo tipoDocumento, string numeroDocumento)
        {
            string ruta = ObtenerRutaPdf(tipoDocumento, numeroDocumento, usarTimestamp: false);
            return File.Exists(ruta);
        }

        private static string SanearNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return "Generico";

            foreach (char c in Path.GetInvalidFileNameChars())
                nombre = nombre.Replace(c.ToString(), "");

            return nombre.Trim();
        }
    }
}
