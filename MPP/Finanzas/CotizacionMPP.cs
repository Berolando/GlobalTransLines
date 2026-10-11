using Abstraccion;
using BE.Dominio;
using BE.Finanzas;
using BE.Personas;
using BE.ServiciosSubcontratados;
using BE.Transporte;
using Transversales;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MPP.Finanzas
{
    public class CotizacionMPP : IRepositorio<BECotizacion>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BECotizacion>();

        #region "Alta"

        public void Alta(BECotizacion obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;
                obj.Numero = GeneradorNumeros.GenerarNumeroCotizacion(obj.Id);

                var nuevaCotizacion = new XElement("Cotizacion",
                    new XAttribute("Id", obj.Id),
                    new XElement("Numero", obj.Numero ?? string.Empty),
                    new XElement("ClienteId", obj.Cliente != null ? obj.Cliente.Id : 0),
                    new XElement("TipoContenedorCodigo", obj.TipoContenedor != null ? obj.TipoContenedor.Codigo : string.Empty),
                    new XElement("Cargas",
                        obj.Cargas != null
                            ? obj.Cargas.Select(c => new XElement("CargaId", c.Id))
                            : new List<XElement>()
                    ),
                    new XElement("Servicios",
                        obj.Servicios != null
                            ? obj.Servicios.Select(s => new XElement("ServicioId", s.Id))
                            : new List<XElement>()
                    ),
                    new XElement("MontoTotal", obj.MontoTotal),
                    new XElement("Estado", obj.Estado != null ? obj.Estado.Codigo : string.Empty),
                    new XElement("FechaEmision", obj.FechaEmision.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("FechaVencimiento", obj.FechaVencimiento.ToString("yyyy-MM-ddTHH:mm:ss"))
                );

                var root = doc.Element("Cotizaciones");
                if (root == null)
                    doc.Add(new XElement("Cotizaciones", nuevaCotizacion));
                else
                    root.Add(nuevaCotizacion);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de cotizaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de cotizaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la cotización en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BECotizacion obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var cotizacion = doc.Descendants("Cotizacion")
                                    .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);
                if (cotizacion != null)
                {
                    cotizacion.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de cotizaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de cotizaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la cotización en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BECotizacion obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var cotizacion = doc.Descendants("Cotizacion")
                                    .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);
                if (cotizacion != null)
                {
                    cotizacion.SetElementValue("Numero", obj.Numero ?? string.Empty);
                    cotizacion.SetElementValue("ClienteId", obj.Cliente != null ? obj.Cliente.Id : 0);
                    cotizacion.SetElementValue("TipoContenedorCodigo", obj.TipoContenedor != null ? obj.TipoContenedor.Codigo : string.Empty);
                    cotizacion.Element("Cargas")?.Remove();
                    cotizacion.Add(new XElement("Cargas",
                        obj.Cargas != null ? obj.Cargas.Select(c => new XElement("CargaId", c.Id))
                            : new List<XElement>()
                    ));
                    cotizacion.Element("Servicios")?.Remove();
                    cotizacion.Add(new XElement("Servicios",
                        obj.Servicios != null
                            ? obj.Servicios.Select(s => new XElement("ServicioId", s.Id))
                            : new List<XElement>()
                    ));
                    cotizacion.SetElementValue("MontoTotal", obj.MontoTotal);
                    cotizacion.SetElementValue("Estado", obj.Estado != null ? obj.Estado.Codigo : string.Empty);
                    cotizacion.SetElementValue("FechaEmision", obj.FechaEmision.ToString("yyyy-MM-ddTHH:mm:ss"));
                    cotizacion.SetElementValue("FechaVencimiento", obj.FechaVencimiento.ToString("yyyy-MM-ddTHH:mm:ss"));
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de cotizaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de cotizaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la cotización en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / ID"

        public List<BECotizacion> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Cotizacion")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string numero = (string)x.Element("Numero") ?? string.Empty;
                              long clienteId = (long?)x.Element("ClienteId") ?? 0;
                              DateTime.TryParse((string)x.Element("FechaEmision"), out DateTime fechaEmision);
                              DateTime.TryParse((string)x.Element("FechaVencimiento"), out DateTime fechaVenc);
                              decimal.TryParse((string)x.Element("MontoTotal"), NumberStyles.Any,
                                  CultureInfo.InvariantCulture, out decimal monto);
                              string codigoEstado = (string)x.Element("Estado") ?? string.Empty;
                              var estado = EstadoCotizacion.ObtenerPorCodigo(codigoEstado);
                              var tipoContenedorCodigo = (string)x.Element("TipoContenedorCodigo") ?? string.Empty;
                              var tipoCont = TipoContenedor.ObtenerPorCodigo(tipoContenedorCodigo);

                              var cot = new BECotizacion
                              {
                                  Id = id,
                                  Numero = numero,
                                  Cliente = clienteId > 0 ? new BECliente { Id = clienteId } : null,
                                  Cargas = x.Element("Cargas") != null
                                      ? x.Element("Cargas").Elements("CargaId")
                                          .Select(c => new BECarga { Id = (long?)c ?? 0 })
                                          .Where(c => c.Id > 0).ToList()
                                      : new List<BECarga>(),
                                  Servicios = x.Element("Servicios") != null
                                      ? x.Element("Servicios").Elements("ServicioId")
                                          .Select(s => new BEServicioSubcontratado { Id = (long?)s ?? 0 })
                                          .Where(s => s.Id > 0).ToList()
                                      : new List<BEServicioSubcontratado>(),
                                  MontoTotal = monto,
                                  Estado = estado,
                                  TipoContenedor = tipoCont,
                                  FechaEmision = fechaEmision,
                                  FechaVencimiento = fechaVenc
                              };

                              return cot;
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de cotizaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de cotizaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar cotizaciones desde XML", ex);
            }
        }

        public BECotizacion BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(c => c.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de cotizaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de cotizaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cotización por Id", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Cotizacion").Any()
                    ? doc.Descendants("Cotizacion").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de cotizaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de cotizaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de cotización", ex);
            }
        }

        #endregion
    }
}
