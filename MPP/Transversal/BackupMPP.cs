using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE.Transversal;
using BE.Enums;
using Abstraccion;

namespace MPP.Transversal
{
    public class BackupMPP : IRepositorio<BEBackup>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEBackup>();

        #region "Alta"

        public void Alta(BEBackup obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                var nuevoBackup = new XElement("Backup",
                    new XAttribute("Id", obj.Id),
                    new XElement("Fecha", obj.Fecha.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Usuario", obj.Usuario ?? string.Empty),
                    new XElement("RutaArchivo", obj.RutaArchivo ?? string.Empty),
                    new XElement("Descripcion", obj.Descripcion ?? string.Empty),
                    new XElement("TipoOperacion", obj.TipoOperacion.ToString())
                );

                var root = doc.Element("Backups");
                if (root == null)
                    doc.Add(new XElement("Backups", nuevoBackup));
                else
                    root.Add(nuevoBackup);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de backups", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de backups. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar backup en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEBackup obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var backup = doc.Descendants("Backup")
                    .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (backup != null)
                {
                    backup.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de backups", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de backups. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar backup en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEBackup obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var backup = doc.Descendants("Backup")
                    .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (backup != null)
                {
                    backup.SetElementValue("Fecha", obj.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
                    backup.SetElementValue("Usuario", obj.Usuario ?? string.Empty);
                    backup.SetElementValue("RutaArchivo", obj.RutaArchivo ?? string.Empty);
                    backup.SetElementValue("Descripcion", obj.Descripcion ?? string.Empty);
                    backup.SetElementValue("TipoOperacion", obj.TipoOperacion.ToString());
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de backups", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de backups. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar backup en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEBackup> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Backup")
                    .Select(x =>
                    {
                        long id = (long?)x.Attribute("Id") ?? 0;
                        DateTime.TryParse((string)x.Element("Fecha"), out DateTime fecha);
                        Enum.TryParse((string)x.Element("TipoOperacion"), out TipoOperacionEnum tipoOperacion);

                        return new BEBackup
                        {
                            Id = id,
                            Fecha = fecha,
                            Usuario = (string)x.Element("Usuario") ?? string.Empty,
                            RutaArchivo = (string)x.Element("RutaArchivo") ?? string.Empty,
                            Descripcion = (string)x.Element("Descripcion") ?? string.Empty,
                            TipoOperacion = tipoOperacion
                        };
                    }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de backups", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de backups. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar backups desde XML", ex);
            }
        }

        public BEBackup BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(b => b.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de backups", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de backups. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar backup por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Backup").Any()
                    ? doc.Descendants("Backup").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de backups", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de backups. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de backup en XML", ex);
            }
        }

        #endregion

        #region "Operaciones de respaldo recursivas"

        // Crea una copia completa de la carpeta Data
        public void CrearRespaldoCompleto()
        {
            try
            {
                string baseData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                string carpetaBackups = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");

                AdministradorArchivos.Crear(carpetaBackups);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string carpetaBackupActual = Path.Combine(carpetaBackups, "Backup_" + timestamp);
                AdministradorArchivos.Crear(carpetaBackupActual);

                CopiarRecursivo(baseData, carpetaBackupActual);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso a los archivos durante la creación del backup.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura de un archivo XML al crear el backup. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error general al crear el backup del sistema.", ex);
            }
        }

        // Restaura un backup completo con copia temporal del Data actual
        public void RestaurarRespaldoCompleto(string carpetaBackupSeleccionada)
        {
            try
            {
                string baseData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                string carpetaTemporal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data_TempRestore");

                if (Directory.Exists(carpetaTemporal))
                    AdministradorArchivos.Borrar(carpetaTemporal);

                // 1. Crear copia temporal
                CopiarRecursivo(baseData, carpetaTemporal);

                // 2. Restaurar desde el backup seleccionado
                CopiarRecursivo(carpetaBackupSeleccionada, baseData);

                // 3. Si todo sale bien, eliminar la copia temporal
                AdministradorArchivos.Borrar(carpetaTemporal);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso a los archivos durante la restauración del backup.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML al restaurar el backup. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error general al restaurar el backup del sistema.", ex);
            }
        }

        // Copia recursiva de carpetas, solo archivos .xml
        private void CopiarRecursivo(string origen, string destino)
        {
            AdministradorArchivos.Crear(destino);

            foreach (string archivo in Directory.GetFiles(origen, "*.xml"))
            {
                string destinoArchivo = Path.Combine(destino, Path.GetFileName(archivo));
                File.Copy(archivo, destinoArchivo, true);
            }

            foreach (string subcarpeta in Directory.GetDirectories(origen))
            {
                string nombreSub = Path.GetFileName(subcarpeta);
                string destinoSub = Path.Combine(destino, nombreSub);
                CopiarRecursivo(subcarpeta, destinoSub);
            }
        }

        #endregion
    }
}
