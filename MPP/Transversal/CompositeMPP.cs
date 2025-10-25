using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MPP.Transversal
{
    public class CompositeMPP
    {
        // Ruta fija para el archivo de relaciones entre roles y permisos
        private  string rutaRelaciones = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data", "Transversal", "Roles_Permisos.xml");

        #region "Creación / Validación XML"

        // Crea el archivo Roles_Permisos.xml si no existe, o lo repara si está corrupto
        private void CrearXMLRelaciones()
        {
            try
            {
                if (!File.Exists(rutaRelaciones))
                {
                    var doc = new XDocument(new XElement("RolPermisos"));
                    doc.Save(rutaRelaciones);
                }
                else
                {
                    var doc = XDocument.Load(rutaRelaciones);

                    if (doc.Root == null || doc.Root.Name != "RolPermisos")
                    {
                        doc = new XDocument(new XElement("RolPermisos"));
                        doc.Save(rutaRelaciones);
                    }
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de roles y permisos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de roles y permisos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear o validar Roles_Permisos.xml", ex);
            }
        }

        #endregion

        #region "Asociar Rol - Permiso"

        // Asocia un permiso a un rol, evitando duplicados
        public void AsociarRolPermiso(long rolId, long permisoId)
        {
            try
            {
                CrearXMLRelaciones();

                var doc = XDocument.Load(rutaRelaciones);
                var root = doc.Element("RolPermisos");

                // Verificar si la relación ya existe
                bool existe = root.Elements("Relacion")
                    .Any(x => (long)x.Attribute("RolId") == rolId &&
                              (long)x.Attribute("PermisoId") == permisoId);

                // Agregar solo si no existe
                if (!existe)
                {
                    root.Add(new XElement("Relacion",
                        new XAttribute("RolId", rolId),
                        new XAttribute("PermisoId", permisoId)));

                    doc.Save(rutaRelaciones);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de roles y permisos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de roles y permisos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asociar rol y permiso en Roles_Permisos.xml", ex);
            }
        }

        #endregion

        #region "Desasociar Rol - Permiso"

        // Elimina la relación entre un rol y un permiso
        public void DesasociarRolPermiso(long rolId, long permisoId)
        {
            try
            {
                CrearXMLRelaciones();

                var doc = XDocument.Load(rutaRelaciones);
                var root = doc.Element("RolPermisos");

                // Buscar relaciones a eliminar
                var relaciones = root.Elements("Relacion")
                    .Where(x => (long)x.Attribute("RolId") == rolId &&
                                (long)x.Attribute("PermisoId") == permisoId)
                    .ToList();

                // Eliminar relaciones encontradas
                if (relaciones.Any())
                {
                    foreach (var rel in relaciones)
                        rel.Remove();

                    doc.Save(rutaRelaciones);
                }

                // Eliminar permisos huérfanos si quedaron sin roles
                EliminarPermisosHuerfanos(permisoId);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de roles y permisos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de roles y permisos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desasociar rol y permiso en Roles_Permisos.xml", ex);
            }
        }

        #endregion

        #region "Eliminar Permisos Huérfanos"

        // Elimina permisos sin roles asociados en el sistema
        private void EliminarPermisosHuerfanos(long permisoId)
        {
            try
            {
                CrearXMLRelaciones();

                var doc = XDocument.Load(rutaRelaciones);
                var root = doc.Element("RolPermisos");

                // Verificar si el permiso sigue asociado a algún rol
                bool sigueAsociado = root.Elements("Relacion")
                    .Any(x => (long)x.Attribute("PermisoId") == permisoId);

                // Si no está asociado, eliminarlo del XML de permisos
                if (!sigueAsociado)
                {
                    var permisoMPP = new PermisoMPP();
                    var permiso = permisoMPP.BuscarPorId(permisoId);
                    if (permiso != null)
                        permisoMPP.Baja(permiso);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de roles y permisos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de roles y permisos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar permisos huérfanos en Roles_Permisos.xml", ex);
            }
        }

        #endregion
    }
}
