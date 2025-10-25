using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE.Transversal;
using BE.Enums;

namespace MPP.Transversal
{
    public class PermisoMPP : IRepositorio<BEPermiso>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEPermiso>();

        #region "Alta"

        public void Alta(BEPermiso obj)
        {
            try
            {
                // Cargar documento XML de permisos
                var doc = XDocument.Load(ruta);

                // Asignar nuevo Id incremental
                obj.Id = ObtenerUltimoId() + 1;

                // Crear nodo permiso
                var nuevoPermiso = new XElement("Permiso",
                    new XAttribute("Id", obj.Id),
                    new XElement("Nombre", obj.Nombre ?? string.Empty),
                    new XElement("Descripcion", obj.Descripcion ?? string.Empty),
                    new XElement("Tipo", obj.Tipo.ToString()),
                    new XElement("Activo", obj.Activo)
                );

                // Agregar al documento
                var root = doc.Element("Permisos");
                if (root == null)
                    doc.Add(new XElement("Permisos", nuevoPermiso));
                else
                    root.Add(nuevoPermiso);

                // Guardar cambios
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de permisos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de permisos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar permiso en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEPermiso obj)
        {
            try
            {
                // Cargar documento
                var doc = XDocument.Load(ruta);

                // Buscar permiso por Id
                var permiso = doc.Descendants("Permiso")
                                 .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Eliminar si existe
                if (permiso != null)
                {
                    permiso.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de permisos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de permisos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar permiso en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEPermiso obj)
        {
            try
            {
                // Cargar documento
                var doc = XDocument.Load(ruta);

                // Buscar permiso por Id
                var permiso = doc.Descendants("Permiso")
                                 .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Actualizar datos
                if (permiso != null)
                {
                    permiso.SetElementValue("Nombre", obj.Nombre ?? string.Empty);
                    permiso.SetElementValue("Descripcion", obj.Descripcion ?? string.Empty);
                    permiso.SetElementValue("Tipo", obj.Tipo.ToString());
                    permiso.SetElementValue("Activo", obj.Activo);

                    // Guardar cambios
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de permisos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de permisos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar permiso en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEPermiso> Listar()
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);

                // Construir lista de permisos desde los nodos
                var lista = doc.Descendants("Permiso")
                               .Select(x => new BEPermiso
                               {
                                   Id = (long?)x.Attribute("Id") ?? 0,
                                   Nombre = (string)x.Element("Nombre") ?? string.Empty,
                                   Descripcion = (string)x.Element("Descripcion") ?? string.Empty,
                                   Tipo = Enum.TryParse((string)x.Element("Tipo"), out PermisoEnum tipo) ? tipo : PermisoEnum.Configuracion,
                                   Activo = (bool?)x.Element("Activo") ?? true
                               })
                               .ToList();

                return lista;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de permisos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de permisos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar permisos desde XML", ex);
            }
        }

        public BEPermiso BuscarPorId(long id)
        {
            try
            {
                // Buscar permiso por Id dentro de la lista
                return Listar().FirstOrDefault(p => p.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de permisos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de permisos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar permiso por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                // Cargar documento y devolver el Id máximo
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Permiso").Any()
                    ? doc.Descendants("Permiso").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de permisos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de permisos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de permiso en XML", ex);
            }
        }

        #endregion
    }
}
