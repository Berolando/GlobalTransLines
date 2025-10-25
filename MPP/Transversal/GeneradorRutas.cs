using System;
using System.IO;
using System.Xml.Linq;   // <-- necesario para XDocument/XElement

namespace MPP.Transversal
{
    public static class GeneradorRutas
    {
        
        #region "Ubicación de entidades"

        public static string UbicacionEntidad<T>()
        {
            try
            {
                // Carpeta base general del sistema
                string carpetaBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Transversal");

                // Crea carpeta si no existe
                if (!Directory.Exists(carpetaBase))
                    Directory.CreateDirectory(carpetaBase);

                // BEUsuario -> "Usuario"
                string nombreEntidad = typeof(T).Name.Replace("BE", string.Empty);

                // Pluralización simple: vocal -> +s, consonante -> +es
                string ultimaLetra = nombreEntidad.Substring(nombreEntidad.Length - 1).ToLower();
                string nombrePlural = "aeiou".Contains(ultimaLetra)
                    ? nombreEntidad + "s"
                    : nombreEntidad + "es";

                // Nombre del archivo y ruta
                string nombreArchivo = nombrePlural + ".xml";
                string rutaCompleta = Path.Combine(carpetaBase, nombreArchivo);

                // Asegura la existencia/estructura del XML con raíz = nombrePlural
                InicializarArchivoXML(rutaCompleta, nombrePlural);

                return rutaCompleta;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML en GeneradorRutas.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura XML. Línea " +
                    ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar la ruta de la entidad XML.", ex);
            }
        }

        #endregion

        #region "Inicialización automática del archivo XML"

        // Crea el archivo si no existe, y si existe valida/repara la raíz
        private static void InicializarArchivoXML(string ruta, string nombreRaiz)
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    var docNuevo = new XDocument(new XElement(nombreRaiz));
                    docNuevo.Save(ruta);
                    return;
                }

                var doc = XDocument.Load(ruta);

                if (doc.Root == null || doc.Root.Name != nombreRaiz)
                {
                    var docReparado = new XDocument(new XElement(nombreRaiz));
                    docReparado.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML al inicializar estructura.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Estructura inválida en el archivo XML (" + nombreRaiz + "). Línea " +
                    ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el archivo XML (" + nombreRaiz + ").", ex);
            }
        }

        #endregion
    }
}
