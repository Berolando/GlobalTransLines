using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE;
using BE.ServiciosSubcontratados;
using MPP.Transversal;

namespace MPP.ServiciosSubcontratados
{
    public class RecepcionServicioMPP : IRepositorio<BERecepcionServicio>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BERecepcionServicio>();

        #region "Alta"

        public void Alta(BERecepcionServicio obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                var nuevaRecepcion = new XElement("RecepcionServicio",
                    new XAttribute("Id", obj.Id),
                    new XElement("FechaRecepcion", obj.FechaRecepcion.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Observaciones", obj.Observaciones ?? string.Empty),
                    new XElement("ServicioId", obj.Servicio != null ? obj.Servicio.Id : 0),
                    new XElement("OrdenDeCompraId", obj.OrdenDeCompra != null ? obj.OrdenDeCompra.Id : 0)
                );

                var root = doc.Element("RecepcionesServicio");
                if (root == null)
                    doc.Add(new XElement("RecepcionesServicio", nuevaRecepcion));
                else
                    root.Add(nuevaRecepcion);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de recepciones de servicio", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de recepciones de servicio. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar recepción de servicio en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BERecepcionServicio obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var recepcion = doc.Descendants("RecepcionServicio")
                                   .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (recepcion != null)
                {
                    recepcion.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de recepciones de servicio", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de recepciones de servicio. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar recepción de servicio en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BERecepcionServicio obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var recepcion = doc.Descendants("RecepcionServicio")
                                   .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (recepcion != null)
                {
                    recepcion.SetElementValue("FechaRecepcion", obj.FechaRecepcion.ToString("yyyy-MM-ddTHH:mm:ss"));
                    recepcion.SetElementValue("Observaciones", obj.Observaciones ?? string.Empty);
                    recepcion.SetElementValue("ServicioId", obj.Servicio != null ? obj.Servicio.Id : 0);
                    recepcion.SetElementValue("OrdenDeCompraId", obj.OrdenDeCompra != null ? obj.OrdenDeCompra.Id : 0);
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de recepciones de servicio", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de recepciones de servicio. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar recepción de servicio en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BERecepcionServicio> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("RecepcionServicio")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              DateTime fechaRecepcion;
                              DateTime.TryParse((string)x.Element("FechaRecepcion"), out fechaRecepcion);

                              string observaciones = (string)x.Element("Observaciones") ?? string.Empty;
                              long servicioId; long.TryParse((string)x.Element("ServicioId"), out servicioId);
                              long ordenId; long.TryParse((string)x.Element("OrdenDeCompraId"), out ordenId);

                              return new BERecepcionServicio
                              {
                                  Id = id,
                                  FechaRecepcion = fechaRecepcion,
                                  Observaciones = observaciones,
                                  Servicio = servicioId > 0 ? new BEServicioSubcontratado { Id = servicioId } : null,
                                  OrdenDeCompra = ordenId > 0 ? new BE.Finanzas.BEOrdenDeCompra { Id = ordenId } : null
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de recepciones de servicio", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de recepciones de servicio. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar recepciones de servicio desde XML", ex);
            }
        }

        public BERecepcionServicio BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(r => r.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de recepciones de servicio", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de recepciones de servicio. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar recepción de servicio por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("RecepcionServicio").Any()
                    ? doc.Descendants("RecepcionServicio").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de recepciones de servicio", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de recepciones de servicio. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de recepción de servicio en XML", ex);
            }
        }

        #endregion
    }
}
