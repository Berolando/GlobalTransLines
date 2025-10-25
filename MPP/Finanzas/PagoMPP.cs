using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE.Finanzas;
using BE.Enums;
using BE; // BEFactura / medios
using MPP.Transversal;

namespace MPP.Finanzas
{
    public class PagoMPP : IRepositorio<BEPago>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEPago>();

        #region "Alta"

        public void Alta(BEPago obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                obj.Id = ObtenerUltimoId() + 1;

                // Tipo textual del medio (nombre de clase sin prefijo BE)
                string tipoMedio = obj.MedioDePago != null
                    ? obj.MedioDePago.GetType().Name.Replace("BE", "")
                    : string.Empty;

                var nuevoPago = new XElement("Pago",
                    new XAttribute("Id", obj.Id),
                    new XElement("FacturaId", obj.Factura != null ? obj.Factura.Id : 0),
                    new XElement("Fecha", obj.Fecha.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Importe", obj.Importe),
                    new XElement("FechaPago", obj.FechaPago.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Estado", obj.Estado.ToString()),
                    new XElement("MedioDePagoId", obj.MedioDePago != null ? obj.MedioDePago.Id : 0),
                    new XElement("TipoMedio", tipoMedio)
                );

                var root = doc.Element("Pagos");
                if (root == null)
                    doc.Add(new XElement("Pagos", nuevoPago));
                else
                    root.Add(nuevoPago);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de pagos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de pagos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar pago en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEPago obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var pago = doc.Descendants("Pago")
                              .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (pago != null)
                {
                    pago.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de pagos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de pagos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar pago en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEPago obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var pago = doc.Descendants("Pago")
                              .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (pago != null)
                {
                    pago.SetElementValue("FacturaId", obj.Factura != null ? obj.Factura.Id : 0);
                    pago.SetElementValue("Fecha", obj.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"));
                    pago.SetElementValue("Importe", obj.Importe);
                    pago.SetElementValue("FechaPago", obj.FechaPago.ToString("yyyy-MM-ddTHH:mm:ss"));
                    pago.SetElementValue("Estado", obj.Estado.ToString());
                    pago.SetElementValue("MedioDePagoId", obj.MedioDePago != null ? obj.MedioDePago.Id : 0);
                    pago.SetElementValue("TipoMedio", obj.MedioDePago != null ? obj.MedioDePago.GetType().Name.Replace("BE", "") : string.Empty);

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de pagos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de pagos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar pago en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEPago> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Pago")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              long facturaId = (long?)x.Element("FacturaId") ?? 0;
                              long medioId = (long?)x.Element("MedioDePagoId") ?? 0;
                              string tipoMedio = (string)x.Element("TipoMedio") ?? string.Empty;

                              DateTime fecha;
                              DateTime.TryParse((string)x.Element("Fecha"), out fecha);

                              DateTime fechaPago;
                              DateTime.TryParse((string)x.Element("FechaPago"), out fechaPago);

                              decimal importe;
                              decimal.TryParse((string)x.Element("Importe"), out importe);

                              EstadoPagoEnum estado;
                              Enum.TryParse((string)x.Element("Estado"), true, out estado);

                              // Reconstrucción mínima del medio (solo Id)
                              BEMedioDePago medio = null;
                              if (medioId > 0)
                              {
                                  switch (tipoMedio)
                                  {
                                      case "Tarjeta":
                                          medio = new BETarjeta { Id = medioId };
                                          break;
                                      case "Transferencia":
                                          medio = new BETransferencia { Id = medioId };
                                          break;
                                      case "Efectivo":
                                          medio = new BEEfectivo { Id = medioId };
                                          break;
                                  }
                              }

                              return new BEPago
                              {
                                  Id = id,
                                  Factura = facturaId > 0 ? new BEFactura { Id = facturaId } : null,
                                  Fecha = fecha,
                                  Importe = importe,
                                  FechaPago = fechaPago,
                                  Estado = estado,
                                  MedioDePago = medio
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de pagos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de pagos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar pagos desde XML", ex);
            }
        }

        public BEPago BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(p => p.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de pagos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de pagos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar pago por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Pago").Any()
                    ? doc.Descendants("Pago").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de pagos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de pagos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de pago en XML", ex);
            }
        }

        #endregion
    }
}
