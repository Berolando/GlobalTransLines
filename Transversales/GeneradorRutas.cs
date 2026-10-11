using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Transversales
{
    public static class GeneradorRutas
    {
        #region "Ubicación de entidades"

        public static string UbicacionEntidad<T>()
        {
            try
            {
                // Determina subcarpeta según namespace
                string subcarpeta = "Infra";
                string ns = typeof(T).Namespace ?? string.Empty;

                if (ns.Contains("Personas"))
                    subcarpeta = "Personas";
                else if (ns.Contains("Finanzas"))
                    subcarpeta = "Finanzas";
                else if (ns.Contains("ServiciosSubcontratados"))
                    subcarpeta = "ServiciosSubcontratados";
                else if (ns.Contains("Transporte"))
                    subcarpeta = "Transporte";
                else if (ns.Contains("Seguridad"))
                    subcarpeta = "Infra";

                // Carpeta base final
                string carpetaBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Data",subcarpeta );

                if (!Directory.Exists(carpetaBase))
                    Directory.CreateDirectory(carpetaBase);

                // Extraer nombre real de la entidad (sin prefijo BE)
                string nombreEntidad = typeof(T).Name.Replace("BE", string.Empty);

                // Generar plural de forma genérica
                string nombrePlural = GenerarPlural(nombreEntidad);

                // Construir nombre de archivo y ruta
                string nombreArchivo = nombrePlural + ".xml";
                string rutaCompleta = Path.Combine(carpetaBase, nombreArchivo);

                // Validar o inicializar estructura
                InicializarArchivoXML(rutaCompleta, nombrePlural);
                return rutaCompleta;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML en GeneradorRutas.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura XML. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar la ruta de la entidad XML.", ex);
            }
        }

        #endregion

        #region "Pluralización genérica"

        private static string GenerarPlural(string nombre)
        {
            string lower = nombre.ToLower();

            // Casos especiales por patrón
            if (lower.EndsWith("ing"))
                return nombre + "s";

            if (lower.EndsWith("s") || lower.EndsWith("x") || lower.EndsWith("z"))
                return nombre + "es";

            if (lower.EndsWith("p") || lower.EndsWith("k"))
                return nombre + "s";

            if ("aeiou".Contains(lower.Last()))
                return nombre + "s";

            // Por defecto
            string plural = nombre + "es";

            // Corrección automática de errores 
            if (plural.EndsWith("upes", StringComparison.OrdinalIgnoreCase))
                plural = plural.Replace("upes", "ups");

            if (plural.EndsWith("inges", StringComparison.OrdinalIgnoreCase))
                plural = plural.Replace("inges", "ings");

            return plural;
        }

        #endregion

        #region "Inicialización automática del archivo XML"

        private static void InicializarArchivoXML(string ruta, string nombreRaiz)
        {
            try
            {
                // Si el archivo no existe, crear raíz abierta y cerrada: <Entidad></Entidad>
                if (!File.Exists(ruta))
                {
                    var docNuevo = new XDocument(new XElement(nombreRaiz));
                    docNuevo.Save(ruta);
                    return;
                }

                // Cargar documento existente
                var doc = XDocument.Load(ruta);

                //  Verificar raíz; si falta o es distinta, reparar manteniendo el backup
                if (doc.Root == null || doc.Root.Name != nombreRaiz)
                {
                    string carpeta = Path.GetDirectoryName(ruta);
                    string nombreArchivo = Path.GetFileNameWithoutExtension(ruta);
                    string rutaBackup = Path.Combine(carpeta, nombreArchivo + "_backup.xml");

                    File.Copy(ruta, rutaBackup, true);

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
                throw new Exception("Estructura inválida en el archivo XML (" + nombreRaiz + "). Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el archivo XML (" + nombreRaiz + ").", ex);
            }
        }

        #endregion

        #region "Helper privado de numeraciones"

        private static string CrearNumeracionSiNoExiste(string carpeta)
        {
            try
            {
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                string rutaArchivo = Path.Combine(carpeta, "Numeraciones.xml");

                if (!File.Exists(rutaArchivo))
                {
                    // Crear estructura raíz completa, no autocerrada
                    var doc = new XDocument(new XElement("Numeraciones"));
                    doc.Save(rutaArchivo);
                }

                return rutaArchivo;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de numeraciones.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de numeraciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar archivo de numeraciones.", ex);
            }
        }

        #endregion

        #region "Ubicación de numeraciones"

        public static string UbicacionNumeracionFacturas()
        {
            return CrearNumeracionSiNoExiste(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Finanzas", "Facturas"));
        }

        public static string UbicacionNumeracionOrdenCompra()
        {
            return CrearNumeracionSiNoExiste(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Finanzas", "OrdenesDeCompra"));
        }

        public static string UbicacionNumeracionBookings()
        {
            return CrearNumeracionSiNoExiste(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Transporte", "Bookings"));
        }

        public static string UbicacionNumeracionBillsOfLading()
        {
            return CrearNumeracionSiNoExiste(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Transporte", "BillsOfLading"));
        }

        #endregion

        #region "Archivos de infraestructura (no entidades)"

        public static string UbicacionArchivoInfra(string nombreArchivo)
        {
            try
            {
                string carpeta = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Data",
                    "Infra"
                );

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                // Si no existe, lo crea vacío con raíz básica
                if (!File.Exists(rutaCompleta))
                {
                    var doc = new XDocument(new XElement("Root"));
                    doc.Save(rutaCompleta);
                }

                return rutaCompleta;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de infraestructura.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura XML. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar la ruta de archivo de infraestructura.", ex);
            }
        }

        #endregion
    }
}
