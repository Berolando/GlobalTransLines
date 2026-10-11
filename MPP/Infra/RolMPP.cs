using Abstraccion;
using BE.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Transversales;

namespace MPP.Infra
{
    public class RolMPP 
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BERol>();

        #region "Alta"

        public void Alta(BERol obj)
        {
            try
            {
                // Cargar documento XML de Roles
                var doc = XDocument.Load(ruta);

                // Generar nuevo Id incremental
                obj.Id = ObtenerUltimoId() + 1;

                // Crear nuevo nodo Rol
                var nuevoRol = new XElement("Rol",
                    new XAttribute("Id", obj.Id),
                    new XElement("Nombre", obj.Nombre ?? string.Empty),
                    new XElement("Descripcion", obj.Descripcion ?? string.Empty),
                    new XElement("Activo", obj.Activo)
                // IMPORTANTE: ya no se persisten permisos aquí.
                );

                // Agregar al documento
                var root = doc.Element("Roles");
                if (root == null)
                    doc.Add(new XElement("Roles", nuevoRol));
                else
                    root.Add(nuevoRol);

                // Guardar cambios
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de roles", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de roles. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar rol en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BERol obj)
        {
            try
            {
                // Cargar documento
                var doc = XDocument.Load(ruta);

                // Buscar rol por Id
                var rol = doc.Descendants("Rol")
                             .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Baja logica
                if (rol != null)
                {
                    rol.SetElementValue("Activo", false);
                    doc.Save(ruta);
                }

            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de roles", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de roles. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar rol en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BERol obj)
        {
            try
            {
                // Cargar documento
                var doc = XDocument.Load(ruta);

                // Buscar nodo rol por Id
                var rol = doc.Descendants("Rol")
                             .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Actualizar datos
                if (rol != null)
                {
                    rol.SetElementValue("Nombre", obj.Nombre ?? string.Empty);
                    rol.SetElementValue("Descripcion", obj.Descripcion ?? string.Empty);
                    rol.SetElementValue("Activo", obj.Activo);

                    // IMPORTANTE:
                    // Ya NO se persisten permisos aquí.
                    // Roles_Permisos.xml es la única fuente de relaciones.

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de roles", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de roles. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar rol en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BERol> Listar()
        {
            try
            {
                // Cargar documento
                var doc = XDocument.Load(ruta);

                // Crear lista de roles SIN subcomponentes
                var listaRoles = doc.Descendants("Rol")
                    .Select(x => new BERol
                    {
                        Id = (long?)x.Attribute("Id") ?? 0,
                        Nombre = (string)x.Element("Nombre") ?? string.Empty,
                        Descripcion = (string)x.Element("Descripcion") ?? string.Empty,
                        Activo = (bool?)x.Element("Activo") ?? true
                        // SubComponentes se manejarán exclusivamente desde CompositeBLL + Roles_Permisos.xml
                    }).ToList();

                return listaRoles;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de roles", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de roles. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar roles desde XML", ex);
            }
        }

        public BERol BuscarPorId(long id)
        {
            try
            {
                // Buscar rol por Id en la lista
                return Listar().FirstOrDefault(r => r.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de roles", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de roles. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar rol por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                // Cargar documento y obtener Id máximo
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Rol").Any()
                    ? doc.Descendants("Rol").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de roles", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de roles. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de rol en XML", ex);
            }
        }

        #endregion
    }
}
