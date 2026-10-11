using Abstraccion;
using BE.Dominio;
using BE.Finanzas;
using BE.Transporte;
using Transversales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace MPP.Finanzas
{
    public class FacturaMPP : IRepositorio<BEFactura>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEFactura>();

        #region "Alta"

        public void Alta(BEFactura obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                obj.Id = ObtenerUltimoId() + 1;
                obj.NumeroFactura = GeneradorNumeros.GenerarNumeroFactura(obj.Tipo ?? TipoFactura.FA, obj.Id);

                var nuevaFactura = new XElement("Factura",
                    new XAttribute("Id", obj.Id),
                    new XElement("NumeroFactura", obj.NumeroFactura ?? string.Empty),
                    new XElement("BookingId", obj.Booking != null ? obj.Booking.Id : 0),
                    new XElement("Estado", obj.Estado != null ? obj.Estado.Codigo : EstadoFactura.Emitida.Codigo),
                    new XElement("Fecha", obj.Fecha.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Impuesto", obj.Impuesto),
                    new XElement("Subtotal", obj.Subtotal),
                    new XElement("Tipo", obj.Tipo != null ? obj.Tipo.Codigo : TipoFactura.FA.Codigo),
                    new XElement("Total", obj.Total)
                );

                var root = doc.Element("Facturas");
                if (root == null)
                    doc.Add(new XElement("Facturas", nuevaFactura));
                else
                    root.Add(nuevaFactura);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de facturas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de facturas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar factura en XML", ex);
            }
        }



        #endregion

        #region "Baja"

        public void Baja(BEFactura obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var factura = doc.Descendants("Factura")
                                 .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (factura != null)
                {
                    // Baja lógica = Anular (no se borra el nodo)
                    factura.SetElementValue("Estado", EstadoFactura.Anulada.Codigo);
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de facturas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de facturas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al anular factura en XML", ex);
            }
        }

        #endregion


        #region "Modificar"

        public void Modificar(BEFactura obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var factura = doc.Descendants("Factura")
                                 .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (factura != null)
                {
                    factura.SetElementValue("NumeroFactura", obj.NumeroFactura ?? string.Empty);
                    factura.SetElementValue("BookingId", obj.Booking != null ? obj.Booking.Id : 0);
                    factura.SetElementValue("Estado", obj.Estado != null ? obj.Estado.Codigo : EstadoFactura.Emitida.Codigo);
                    factura.SetElementValue("Fecha", obj.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
                    factura.SetElementValue("Impuesto", obj.Impuesto);
                    factura.SetElementValue("Subtotal", obj.Subtotal);
                    factura.SetElementValue("Tipo", obj.Tipo != null ? obj.Tipo.Codigo : TipoFactura.FA.Codigo);
                    factura.SetElementValue("Total", obj.Total);

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de facturas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de facturas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar factura en XML", ex);
            }
        }


        #endregion

        #region "Listar / Buscar / Id"

        public List<BEFactura> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Factura")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string numeroFactura = (string)x.Element("NumeroFactura") ?? string.Empty;
                              long bookingId = (long?)x.Element("BookingId") ?? 0;

                              DateTime.TryParse((string)x.Element("Fecha"), out DateTime fecha);
                              decimal.TryParse((string)x.Element("Impuesto"), System.Globalization.NumberStyles.Any,
                                  System.Globalization.CultureInfo.InvariantCulture, out decimal impuesto);
                              decimal.TryParse((string)x.Element("Subtotal"), System.Globalization.NumberStyles.Any,
                                  System.Globalization.CultureInfo.InvariantCulture, out decimal subtotal);
                              decimal.TryParse((string)x.Element("Total"), System.Globalization.NumberStyles.Any,
                                  System.Globalization.CultureInfo.InvariantCulture, out decimal total);

                              string codigoEstado = (string)x.Element("Estado") ?? string.Empty;
                              var estado = EstadoFactura.ObtenerPorCodigo(codigoEstado);

                              string codigoTipo = (string)x.Element("Tipo") ?? string.Empty;
                              var tipo = TipoFactura.ObtenerPorCodigo(codigoTipo) ?? TipoFactura.FA;

                              return new BEFactura
                              {
                                  Id = id,
                                  NumeroFactura = numeroFactura,
                                  Booking = bookingId > 0 ? new BEBooking { Id = bookingId } : null,
                                  Estado = estado,
                                  Fecha = fecha,
                                  Impuesto = impuesto,
                                  Subtotal = subtotal,
                                  Tipo = tipo,
                                  Total = total
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de facturas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de facturas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar facturas desde XML", ex);
            }
        }


        public BEFactura BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(f => f.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de facturas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de facturas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar factura por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Factura").Any()
                    ? doc.Descendants("Factura").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de facturas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de facturas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de factura en XML", ex);
            }
        }

        #endregion
    }
}
