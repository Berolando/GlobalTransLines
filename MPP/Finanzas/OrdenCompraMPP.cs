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
    public class OrdenDeCompraMPP : IRepositorio<BEOrdenDeCompra>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEOrdenDeCompra>();

        #region "Alta"

        public void Alta(BEOrdenDeCompra obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                obj.Id = ObtenerUltimoId() + 1;
                obj.Numero = GeneradorNumeros.GenerarNumero(typeof(BEOrdenDeCompra), obj.Id);

                var nuevaOC = new XElement("OrdenDeCompra",
                    new XAttribute("Id", obj.Id),
                    new XElement("Numero", obj.Numero),
                    new XElement("ProveedorId", obj.Proveedor != null ? obj.Proveedor.Id : 0),
                    new XElement("BookingId", obj.BookingOrigen != null ? obj.BookingOrigen.Id : 0),
                    new XElement("Servicios",
                        obj.Servicios != null
                            ? obj.Servicios.Select(s => new XElement("ServicioId", s.Id))
                            : new List<XElement>()
                    ),
                    new XElement("FechaEmision", obj.FechaEmision.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Estado", obj.Estado != null ? obj.Estado.Codigo : string.Empty),
                    new XElement("ImporteTotal", obj.ImporteTotal)
                );

                var root = doc.Element("OrdenDeCompras");
                if (root == null)
                    doc.Add(new XElement("OrdenDeCompras", nuevaOC));
                else
                    root.Add(nuevaOC);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de órdenes de compra", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de órdenes de compra. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar orden de compra en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEOrdenDeCompra obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var oc = doc.Descendants("OrdenDeCompra")
                            .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (oc != null)
                {
                    oc.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de órdenes de compra", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de órdenes de compra. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar orden de compra en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEOrdenDeCompra obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var oc = doc.Descendants("OrdenDeCompra")
                            .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (oc != null)
                {
                    oc.SetElementValue("Numero", obj.Numero);
                    oc.SetElementValue("ProveedorId", obj.Proveedor != null ? obj.Proveedor.Id : 0);
                    oc.SetElementValue("BookingId", obj.BookingOrigen != null ? obj.BookingOrigen.Id : 0);

                    oc.Element("Servicios")?.Remove();
                    oc.Add(new XElement("Servicios",
                        obj.Servicios != null
                            ? obj.Servicios.Select(s => new XElement("ServicioId", s.Id))
                            : new List<XElement>()
                    ));

                    oc.SetElementValue("FechaEmision", obj.FechaEmision.ToString("yyyy-MM-ddTHH:mm:ss"));
                    oc.SetElementValue("Estado", obj.Estado != null ? obj.Estado.Codigo : string.Empty);
                    oc.SetElementValue("ImporteTotal", obj.ImporteTotal);

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de órdenes de compra", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de órdenes de compra. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar orden de compra en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEOrdenDeCompra> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("OrdenDeCompra")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string numero = (string)x.Element("Numero") ?? string.Empty;

                              long proveedorId = (long?)x.Element("ProveedorId") ?? 0;
                              long bookingId = (long?)x.Element("BookingId") ?? 0;

                              DateTime fechaEmision;
                              DateTime.TryParse((string)x.Element("FechaEmision"), out fechaEmision);

                              decimal importeTotal;
                              decimal.TryParse((string)x.Element("ImporteTotal"), NumberStyles.Any,
                                  CultureInfo.InvariantCulture, out importeTotal);

                              string codigoEstado = (string)x.Element("Estado") ?? string.Empty;
                              var estado = EstadoOrdenCompra.ObtenerPorCodigo(codigoEstado);

                              return new BEOrdenDeCompra
                              {
                                  Id = id,
                                  Numero = numero,
                                  Proveedor = proveedorId > 0 ? new BEProveedor { Id = proveedorId } : null,
                                  BookingOrigen = bookingId > 0 ? new BEBooking { Id = bookingId } : null,
                                  Servicios = x.Element("Servicios") != null
                                      ? x.Element("Servicios")
                                          .Elements("ServicioId")
                                          .Select(s => new BEServicioSubcontratado { Id = (long?)s ?? 0 })
                                          .Where(s => s.Id > 0)
                                          .ToList()
                                      : new List<BEServicioSubcontratado>(),
                                  FechaEmision = fechaEmision,
                                  Estado = estado,
                                  ImporteTotal = importeTotal
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de órdenes de compra", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de órdenes de compra. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar órdenes de compra desde XML", ex);
            }
        }

        public BEOrdenDeCompra BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(oc => oc.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de órdenes de compra", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de órdenes de compra. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar orden de compra por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("OrdenDeCompra").Any()
                    ? doc.Descendants("OrdenDeCompra").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de órdenes de compra", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de órdenes de compra. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de orden de compra en XML", ex);
            }
        }

        #endregion
    }
}
