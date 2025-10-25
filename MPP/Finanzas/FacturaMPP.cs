using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE.Finanzas;
using BE;
using BE.Enums;
using MPP.Transversal;
using MPP.Utilidades;

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
                obj.NumeroFactura = GeneradorNumeros.GenerarNumeroFactura(obj.Tipo, obj.Id);

                var nuevaFactura = new XElement("Factura",
                    new XAttribute("Id", obj.Id),
                    new XElement("NumeroFactura", obj.NumeroFactura ?? string.Empty),
                    new XElement("BookingId", obj.Booking != null ? obj.Booking.Id : 0),
                    new XElement("Estado", obj.Estado.ToString()),
                    new XElement("Fecha", obj.Fecha.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Impuesto", obj.Impuesto),
                    new XElement("Subtotal", obj.Subtotal),
                    new XElement("Marca", obj.Tipo.ToString()),
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
                    factura.Remove();
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
                throw new Exception("Error al eliminar factura en XML", ex);
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
                    factura.SetElementValue("Estado", obj.Estado.ToString());
                    factura.SetElementValue("Fecha", obj.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
                    factura.SetElementValue("Impuesto", obj.Impuesto);
                    factura.SetElementValue("Subtotal", obj.Subtotal);
                    factura.SetElementValue("Marca", obj.Tipo.ToString());
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

                              DateTime fecha;
                              DateTime.TryParse((string)x.Element("Fecha"), out fecha);

                              decimal impuesto;
                              decimal.TryParse((string)x.Element("Impuesto"), out impuesto);

                              decimal subtotal;
                              decimal.TryParse((string)x.Element("Subtotal"), out subtotal);

                              decimal total;
                              decimal.TryParse((string)x.Element("Total"), out total);

                              EstadoFacturaEnum estado;
                              Enum.TryParse((string)x.Element("Estado"), true, out estado);

                              TipoFacturaEnum tipo;
                              Enum.TryParse((string)x.Element("Marca"), true, out tipo);

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
