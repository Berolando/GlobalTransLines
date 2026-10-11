using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Transversales;

namespace MPP.Infra
{
    public class CompositeMPP
    {

        private readonly string rutaRelaciones =  GeneradorRutas.UbicacionArchivoInfra("Roles_Permisos.xml");


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
                    .Any(x => ((long?)x.Attribute("RolId")) == rolId &&
                              ((long?)x.Attribute("PermisoId")) == permisoId);

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
                    .Where(x => ((long?)x.Attribute("RolId")) == rolId &&
                                ((long?)x.Attribute("PermisoId")) == permisoId)
                    .ToList();

                // Eliminar relaciones encontradas
                if (relaciones.Any())
                {
                    foreach (var rel in relaciones)
                        rel.Remove();

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
                throw new Exception("Error al desasociar rol y permiso en Roles_Permisos.xml", ex);
            }
        }

        #endregion

        #region "Asociar / Desasociar Rol - Rol"
        public void AsociarRolRol(long rolId, long subRolId)
        {
            try
            {
                CrearXMLRelaciones();

                var doc = XDocument.Load(rutaRelaciones);
                var root = doc.Element("RolPermisos");

                bool existe = root.Elements("Relacion")
                    .Any(x =>
                        ((long?)x.Attribute("RolId")) == rolId &&
                        (long?)x.Attribute("SubRolId") == subRolId
                    );

                if (!existe)
                {
                    root.Add(new XElement("Relacion",
                        new XAttribute("RolId", rolId),
                        new XAttribute("SubRolId", subRolId)
                    ));

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
                throw new Exception("Error al asociar rol con otro rol en Roles_Permisos.xml", ex);
            }
        }

        public void DesasociarRolRol(long rolId, long subRolId)
        {
            try
            {
                CrearXMLRelaciones();

                var doc = XDocument.Load(rutaRelaciones);
                var root = doc.Element("RolPermisos");

                var relaciones = root.Elements("Relacion")
                    .Where(x =>
                        ((long?)x.Attribute("RolId")) == rolId &&
                        (long?)x.Attribute("SubRolId") == subRolId
                    ).ToList();

                foreach (var rel in relaciones)
                    rel.Remove();

                doc.Save(rutaRelaciones);
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
                throw new Exception("Error al desasociar rol de rol en Roles_Permisos.xml", ex);
            }
        }

        #endregion

        #region "Limpieza de relaciones"

        public void QuitarRelacionesDePermisos(IEnumerable<long> permisosId)
        {
            try
            {
                CrearXMLRelaciones();

                var ids = new HashSet<long>(permisosId ?? Enumerable.Empty<long>());
                if (!ids.Any())
                    return;

                var doc = XDocument.Load(rutaRelaciones);
                var root = doc.Element("RolPermisos");

                var relaciones = root.Elements("Relacion")
                    .Where(x =>
                        ((long?)x.Attribute("PermisoId")).HasValue &&
                        ids.Contains(((long?)x.Attribute("PermisoId")).Value))
                    .ToList();

                if (!relaciones.Any())
                    return;

                foreach (var relacion in relaciones)
                    relacion.Remove();

                doc.Save(rutaRelaciones);
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
                throw new Exception("Error al limpiar relaciones de permisos en Roles_Permisos.xml", ex);
            }
        }

        #endregion

        #region"Listar Relaciones"
        public List<(long RolId, long? PermisoId, long? SubRolId, string Tipo)> ListarRelaciones()
        {
            try
            {
                CrearXMLRelaciones();

                var doc = XDocument.Load(rutaRelaciones);
                var lista = new List<(long, long?, long?, string)>();

                foreach (var r in doc.Descendants("Relacion"))
                {
                    long rolId = (long)r.Attribute("RolId");
                    long? permisoId = (long?)r.Attribute("PermisoId");
                    long? subRolId = (long?)r.Attribute("SubRolId");

                    // Tipo = Permiso o SubRol
                    string tipo = permisoId.HasValue ? "Permiso" : "SubRol";

                    lista.Add((rolId, permisoId, subRolId, tipo));
                }

                return lista;
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
                throw new Exception("Error al listar las relaciones de Roles_Permisos.xml", ex);
            }
        }
        #endregion
    }
}
