using System;
using System.IO;
using BE.Dominio;
using Transversales.Documentos; 
namespace Transversales.Docu
{
    public class GestorDocumentos
    {
        private readonly RutasDocumentos rutas;

        public GestorDocumentos(RutasDocumentos rutasDocumentos)
        {
            rutas = rutasDocumentos ?? throw new ArgumentNullException(nameof(rutasDocumentos));
        }

        public bool ExistePdf(DocumentoTipo tipoDocumento, string numeroDocumento)
        {
            ValidarEntrada(tipoDocumento, numeroDocumento);
            return rutas.ExistePdf(tipoDocumento, numeroDocumento);
        }

        public string ObtenerRutaPdf(DocumentoTipo tipoDocumento, string numeroDocumento)
        {
            ValidarEntrada(tipoDocumento, numeroDocumento);
            return rutas.ObtenerRutaPdf(tipoDocumento, numeroDocumento, usarTimestamp: false);
        }

        public string ObtenerRutaPdfParaEscritura(DocumentoTipo tipoDocumento, string numeroDocumento)
        {
            ValidarEntrada(tipoDocumento, numeroDocumento);
            string rutaPdf = rutas.ObtenerRutaPdf(tipoDocumento, numeroDocumento, usarTimestamp: false);
            AsegurarDirectorio(rutaPdf);
            return rutaPdf;
        }

        /// <summary>
        /// Devuelve la ruta del PDF.
        /// Si ya existe por número => se reutiliza.
        /// Si no existe => lo genera usando el delegado "generarPdf".
        /// </summary>
        public string ObtenerOGenerarPdf(DocumentoTipo tipoDocumento, string numeroDocumento, Action<string> generarPdf)
        {
            if (generarPdf == null) throw new ArgumentNullException(nameof(generarPdf));
            ValidarEntrada(tipoDocumento, numeroDocumento);

            string rutaPdf = rutas.ObtenerRutaPdf(tipoDocumento, numeroDocumento, usarTimestamp: false);

            if (File.Exists(rutaPdf))
                return rutaPdf;

            AsegurarDirectorio(rutaPdf);
            generarPdf(rutaPdf);

            if (!File.Exists(rutaPdf))
                throw new Exception("No se generó el PDF esperado. Verificar el generador del documento.");

            return rutaPdf;
        }

        private static void ValidarEntrada(DocumentoTipo tipoDocumento, string numeroDocumento)
        {
            if (tipoDocumento == null) throw new ArgumentNullException(nameof(tipoDocumento));
            if (string.IsNullOrWhiteSpace(numeroDocumento))
                throw new ArgumentException("El número de documento es obligatorio.", nameof(numeroDocumento));
        }

        private static void AsegurarDirectorio(string rutaArchivo)
        {
            string dir = Path.GetDirectoryName(rutaArchivo);
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }
}
