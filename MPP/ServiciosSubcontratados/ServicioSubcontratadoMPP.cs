using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE;
using MPP.Transversal;

namespace MPP.ServiciosSubcontratados
{
    public class ServicioSubcontratadoMPP : IRepositorio<BEServicioSubcontratado>
    {
        private  string ruta = GeneradorRutas.UbicacionEntidad<BEServicioSubcontratado>();

        #region "Alta"

        public void Alta(BEServicioSubcontratado obj)
        {
            try
            {
                if (!File.Exists(ruta))
                    new XDocument(new XElement("ServiciosSubcontratados")).Save(ruta);

                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                var nuevoServicio = new XElement("ServicioSubcontratado",
                    new XAttribute("Id", obj.Id),
                    new XElement("Nombre", obj.Nombre ?? string.Empty),
                    new XElement("Descripcion", obj.Descripcion ?? string.Empty),
                    new XElement("Costo", obj.Costo),
                    new XElement("ProveedorId", obj.Proveedor != null ? obj.Proveedor.Id : 0)
                );

                doc.Root.Add(nuevoServicio);
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de servicios subcontratados", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de servicios subcontratados. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar servicio subcontratado en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEServicioSubcontratado obj)
        {
            try
            {
                if (!File.Exists(ruta)) return;

                var doc = XDocument.Load(ruta);
                var servicio = doc.Descendants("ServicioSubcontratado")
                                  .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (servicio != null)
                {
                    servicio.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de servicios subcontratados", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de servicios subcontratados. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar servicio subcontratado en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEServicioSubcontratado obj)
        {
            try
            {
                if (!File.Exists(ruta)) return;

                var doc = XDocument.Load(ruta);
                var servicio = doc.Descendants("ServicioSubcontratado")
                                  .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (servicio != null)
                {
                    servicio.SetElementValue("Nombre", obj.Nombre ?? string.Empty);
                    servicio.SetElementValue("Descripcion", obj.Descripcion ?? string.Empty);
                    servicio.SetElementValue("Costo", obj.Costo);
                    servicio.SetElementValue("ProveedorId", obj.Proveedor != null ? obj.Proveedor.Id : 0);
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de servicios subcontratados", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de servicios subcontratados. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar servicio subcontratado en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEServicioSubcontratado> Listar()
        {
            try
            {
                if (!File.Exists(ruta))
                    return new List<BEServicioSubcontratado>();

                var doc = XDocument.Load(ruta);

                return doc.Descendants("ServicioSubcontratado")
                          .Select(x => new BEServicioSubcontratado
                          {
                              Id = (long?)x.Attribute("Id") ?? 0,
                              Nombre = (string)x.Element("Nombre") ?? string.Empty,
                              Descripcion = (string)x.Element("Descripcion") ?? string.Empty,
                              Costo = (decimal?)x.Element("Costo") ?? 0,
                              Proveedor = new BEProveedor
                              {
                                  Id = (long?)x.Element("ProveedorId") ?? 0
                              }
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de servicios subcontratados", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de servicios subcontratados. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar servicios subcontratados desde XML", ex);
            }
        }

        public BEServicioSubcontratado BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(s => s.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de servicios subcontratados", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de servicios subcontratados. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar servicio subcontratado por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                if (!File.Exists(ruta))
                    return 0;

                var doc = XDocument.Load(ruta);
                return doc.Descendants("ServicioSubcontratado").Any()
                    ? doc.Descendants("ServicioSubcontratado").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de servicios subcontratados", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de servicios subcontratados. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de servicio subcontratado en XML", ex);
            }
        }

        #endregion
    }
}
