using Abstraccion;
using BE.Dominio;
using BE.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Transversales;

namespace MPP.Infra
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
                    new XElement("UsuarioId", obj.UsuarioId),
                    new XElement("Usuario", obj.Usuario ?? string.Empty),
                    new XElement("RutaArchivo", obj.RutaArchivo ?? string.Empty),
                    new XElement("Descripcion", obj.Descripcion ?? string.Empty),
                    new XElement("TipoOperacion", obj.TipoOperacion != null ? obj.TipoOperacion.Codigo : TipoOperacion.Backup.Codigo)
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
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar backup en XML.", ex);
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
                    string rutaBackup = (string)backup.Element("RutaArchivo") ?? obj.RutaArchivo;

                    backup.Remove();
                    doc.Save(ruta);

                    EliminarCarpetaBackupSiCorresponde(rutaBackup);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de backups", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de backups. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar backup en XML.", ex);
            }
        }

        #endregion

        #region "Limpieza física"

        private void EliminarCarpetaBackupSiCorresponde(string rutaBackup)
        {
            if (string.IsNullOrWhiteSpace(rutaBackup))
                return;

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string carpetaBackups = Path.GetFullPath(Path.Combine(basePath, "Backups"));
            string carpetaAEliminar = Path.GetFullPath(rutaBackup);

            bool estaDentroDeBackups = carpetaAEliminar.StartsWith(
                carpetaBackups + Path.DirectorySeparatorChar,
                StringComparison.OrdinalIgnoreCase);

            if (!estaDentroDeBackups)
                return;

            if (!Directory.Exists(carpetaAEliminar))
                return;

            Directory.Delete(carpetaAEliminar, true);
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
                    backup.SetElementValue("UsuarioId", obj.UsuarioId);
                    backup.SetElementValue("Usuario", obj.Usuario ?? string.Empty);
                    backup.SetElementValue("RutaArchivo", obj.RutaArchivo ?? string.Empty);
                    backup.SetElementValue("Descripcion", obj.Descripcion ?? string.Empty);
                    backup.SetElementValue("TipoOperacion", obj.TipoOperacion != null ? obj.TipoOperacion.Codigo : TipoOperacion.Backup.Codigo);
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
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar backup en XML.", ex);
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
                        string codigoOperacion = (string)x.Element("TipoOperacion") ?? TipoOperacion.Backup.Codigo;
                        var tipoOperacion = TipoOperacion.ObtenerPorCodigo(codigoOperacion) ?? TipoOperacion.Backup;

                        return new BEBackup
                        {
                            Id = id,
                            Fecha = fecha,
                            UsuarioId = (long?)x.Element("UsuarioId") ?? 0,
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
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar backups desde XML.", ex);
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
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar backup por Id en XML.", ex);
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
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de backup en XML.", ex);
            }
        }

        #endregion

        #region "Operaciones de respaldo recursivas"

        public string CrearRespaldoCompleto(string usuarioActual)
        {
            try
            {
                // Base: carpeta donde está el EXE
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                // Carpeta de datos principal (fuente del backup)
                string baseData = Path.Combine(basePath, "Data");

                // Carpeta donde se guardarán los backups (junto al EXE)
                string carpetaBackups = Path.Combine(basePath, "Backups");

                if (!Directory.Exists(carpetaBackups))
                    Directory.CreateDirectory(carpetaBackups);

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string carpetaBackupActual = Path.Combine(carpetaBackups,
                    "Backup_" + timestamp + "_" + usuarioActual);

                if (!Directory.Exists(carpetaBackupActual))
                    Directory.CreateDirectory(carpetaBackupActual);

                CopiarRecursivo(baseData, carpetaBackupActual);

                return carpetaBackupActual;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso a los archivos durante la creación del backup.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura de un archivo XML al crear el backup. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error general al crear el backup del sistema.", ex);
            }
        }



        public void RestaurarRespaldoCompleto(string carpetaBackupSeleccionada)
        {
            string carpetaRespaldoPrevio = string.Empty;

            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string baseData = Path.Combine(basePath, "Data");
                string carpetaBackups = Path.Combine(basePath, "Backups");

                if (string.IsNullOrWhiteSpace(carpetaBackupSeleccionada))
                    throw new Exception("No se indicó la carpeta del backup a restaurar.");

                if (!Directory.Exists(carpetaBackupSeleccionada))
                    throw new Exception("La carpeta del backup seleccionado no existe.");

                if (!Directory.Exists(baseData))
                    throw new Exception("La carpeta Data del sistema no existe.");
              
                ValidarEstructuraBackup(carpetaBackupSeleccionada);

                if (!Directory.Exists(carpetaBackups))
                    Directory.CreateDirectory(carpetaBackups);

                // Respaldo previo real del estado actual antes de restaurar
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                carpetaRespaldoPrevio = Path.Combine(carpetaBackups, "PreRestore_" + timestamp);

                Directory.CreateDirectory(carpetaRespaldoPrevio);
                CopiarRecursivo(baseData, carpetaRespaldoPrevio);

                // Aplica la restauración
                RestaurarRecursivoSeguro(carpetaBackupSeleccionada, baseData);
            }
            catch (IOException ex)
            {
                try
                {
                    RevertirDesdeRespaldoPrevio(carpetaRespaldoPrevio);
                }
                catch
                {
                    // Si falla la reversión, se conserva la excepción original
                }

                throw new Exception("Error de acceso a los archivos durante la restauración del backup.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                try
                {
                    RevertirDesdeRespaldoPrevio(carpetaRespaldoPrevio);
                }
                catch
                {
                    // Si falla la reversión, se conserva la excepción original
                }

                throw new Exception("Error en la estructura del archivo XML al restaurar el backup. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                try
                {
                    RevertirDesdeRespaldoPrevio(carpetaRespaldoPrevio);
                }
                catch
                {
                    // Si falla la reversión, se conserva la excepción original
                }

                throw new Exception("Error general al restaurar el backup del sistema.", ex);
            }
        }
        private void RevertirDesdeRespaldoPrevio(string carpetaRespaldoPrevio)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(carpetaRespaldoPrevio))
                    return;

                if (!Directory.Exists(carpetaRespaldoPrevio))
                    return;

                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string baseData = Path.Combine(basePath, "Data");

                RestaurarRecursivoSeguro(carpetaRespaldoPrevio, baseData);
            }
            catch (IOException ex)
            {
                throw new Exception("Error al intentar revertir la restauración desde el respaldo previo.", ex);
            }
        }

        private void RestaurarRecursivoSeguro(string origen, string destino)
        {
            try
            {
                if (!Directory.Exists(destino)) Directory.CreateDirectory(destino);

                foreach (string archivo in Directory.GetFiles(origen, "*.xml"))
                {
                    string nombre = Path.GetFileName(archivo);

                    if (nombre.Equals("Eventos.xml", StringComparison.OrdinalIgnoreCase) ||
                        nombre.Equals("Bitacora.xml", StringComparison.OrdinalIgnoreCase))
                        continue;

                    string destinoArchivo = Path.Combine(destino, nombre);
                    File.Copy(archivo, destinoArchivo, true);
                }

                foreach (string subcarpeta in Directory.GetDirectories(origen))
                {
                    string nombreSub = Path.GetFileName(subcarpeta);
                    string destinoSub = Path.Combine(destino, nombreSub);
                    Directory.CreateDirectory(destinoSub);
                    RestaurarRecursivoSeguro(subcarpeta, destinoSub);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error durante la restauración recursiva.", ex);
            }
        }

        private void CopiarRecursivo(string origen, string destino)
        {
            try
            {
                Directory.CreateDirectory(destino);

                foreach (string archivo in Directory.GetFiles(origen, "*.xml"))
                {
                    string nombre = Path.GetFileName(archivo);

                    if (nombre.Equals("Eventos.xml", StringComparison.OrdinalIgnoreCase) ||
                        nombre.Equals("Bitacora.xml", StringComparison.OrdinalIgnoreCase))
                        continue;

                    string destinoArchivo = Path.Combine(destino, nombre);
                    File.Copy(archivo, destinoArchivo, true);
                }

                foreach (string subcarpeta in Directory.GetDirectories(origen))
                {
                    string nombreSub = Path.GetFileName(subcarpeta);

                    // Evita copiar carpetas que no forman parte del estado operativo actual.
                    if (DebeExcluirCarpetaDelBackup(nombreSub))
                        continue;

                    string destinoSub = Path.Combine(destino, nombreSub);
                    Directory.CreateDirectory(destinoSub);
                    CopiarRecursivo(subcarpeta, destinoSub);
                }

            }
            catch (IOException ex)
            {
                throw new Exception("Error durante la copia recursiva.", ex);
            }
        }

        private bool DebeExcluirCarpetaDelBackup(string nombreCarpeta)
        {
            if (string.IsNullOrWhiteSpace(nombreCarpeta))
                return false;

            return nombreCarpeta.Equals("Backups", StringComparison.OrdinalIgnoreCase)
                || nombreCarpeta.Equals("Transversal", StringComparison.OrdinalIgnoreCase)
                || nombreCarpeta.Equals("Backup Infra 170126", StringComparison.OrdinalIgnoreCase);
        }

        #endregion
        #region "Validaciones"

        private void ValidarEstructuraBackup(string carpetaBackupSeleccionada)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(carpetaBackupSeleccionada))
                    throw new Exception("No se indicó la carpeta del backup.");

                if (!Directory.Exists(carpetaBackupSeleccionada))
                    throw new Exception("La carpeta del backup no existe.");

                var subdirectorios = Directory.GetDirectories(carpetaBackupSeleccionada);
                if (subdirectorios == null || subdirectorios.Length == 0)
                    throw new Exception("El backup no contiene estructura de datos válida.");

                string carpetaInfra = Path.Combine(carpetaBackupSeleccionada, "Infra");

                bool existeUsuarios = File.Exists(Path.Combine(carpetaInfra, "Usuarios.xml"));
                bool existeRoles = File.Exists(Path.Combine(carpetaInfra, "Roles.xml"));
                bool existePermisos = File.Exists(Path.Combine(carpetaInfra, "Permisos.xml"));
                bool existeRelaciones = File.Exists(Path.Combine(carpetaInfra, "Roles_Permisos.xml"));

                if (!existeUsuarios || !existeRoles || !existePermisos || !existeRelaciones)
                {
                    throw new Exception("El backup está incompleto. Faltan archivos críticos en la carpeta Infra (Usuarios, Roles, Permisos o Roles_Permisos).");
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error al acceder a la estructura del backup.", ex);
            }
        }

        #endregion
    }
}
