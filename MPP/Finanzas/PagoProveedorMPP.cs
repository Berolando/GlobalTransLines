using Abstraccion;
using BE.Dominio;
using BE.Finanzas;
using Transversales;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MPP.Finanzas
{
    public class PagoProveedorMPP : IRepositorio<BEPagoProveedor>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEPagoProveedor>();

        #region "Alta"

        public void Alta(BEPagoProveedor obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                obj.Id = ObtenerUltimoId() + 1;

                string tipoMedio = obj.MedioDePago != null
                    ? obj.MedioDePago.GetType().Name.Replace("BE", "")
                    : string.Empty;

                var nuevoPagoProveedor = new XElement("PagoProveedor",
                    new XAttribute("Id", obj.Id),
                    new XElement("OrdenDeCompraId", obj.OrdenDeCompra != null ? obj.OrdenDeCompra.Id : 0),
                    new XElement("Importe", obj.Importe),
                    new XElement("FechaPago", obj.FechaPago.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("NumeroFacturaProveedor", obj.NumeroFacturaProveedor ?? string.Empty),
                    new XElement("FechaFacturaProveedor", obj.FechaFacturaProveedor.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("ImporteFacturaProveedor", obj.ImporteFacturaProveedor),
                    new XElement("Estado", obj.Estado != null ? obj.Estado.Codigo : string.Empty),
                    new XElement("MedioDePagoId", obj.MedioDePago != null ? obj.MedioDePago.Id : 0),
                    new XElement("TipoMedio", tipoMedio)
                );

                var root = doc.Element("PagoProveedores");
                if (root == null)
                    doc.Add(new XElement("PagoProveedores", nuevoPagoProveedor));
                else
                    root.Add(nuevoPagoProveedor);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de pagos a proveedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de pagos a proveedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar pago a proveedor en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEPagoProveedor obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var pago = doc.Descendants("PagoProveedor")
                              .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (pago != null)
                {
                    pago.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de pagos a proveedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de pagos a proveedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar pago a proveedor en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEPagoProveedor obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var pago = doc.Descendants("PagoProveedor")
                              .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (pago != null)
                {
                    pago.SetElementValue("OrdenDeCompraId", obj.OrdenDeCompra != null ? obj.OrdenDeCompra.Id : 0);
                    pago.SetElementValue("Importe", obj.Importe);
                    pago.SetElementValue("FechaPago", obj.FechaPago.ToString("yyyy-MM-ddTHH:mm:ss"));
                    pago.SetElementValue("NumeroFacturaProveedor", obj.NumeroFacturaProveedor ?? string.Empty);
                    pago.SetElementValue("FechaFacturaProveedor", obj.FechaFacturaProveedor.ToString("yyyy-MM-ddTHH:mm:ss"));
                    pago.SetElementValue("ImporteFacturaProveedor", obj.ImporteFacturaProveedor);
                    pago.SetElementValue("Estado", obj.Estado != null ? obj.Estado.Codigo : string.Empty);
                    pago.SetElementValue("MedioDePagoId", obj.MedioDePago != null ? obj.MedioDePago.Id : 0);
                    pago.SetElementValue("TipoMedio", obj.MedioDePago != null ? obj.MedioDePago.GetType().Name.Replace("BE", "") : string.Empty);

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de pagos a proveedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de pagos a proveedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar pago a proveedor en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEPagoProveedor> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("PagoProveedor")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              long ordenId = (long?)x.Element("OrdenDeCompraId") ?? 0;
                              long medioId = (long?)x.Element("MedioDePagoId") ?? 0;
                              string tipoMedio = (string)x.Element("TipoMedio") ?? string.Empty;

                              decimal importe;
                              decimal.TryParse((string)x.Element("Importe"), NumberStyles.Any,
                                  CultureInfo.InvariantCulture, out importe);

                              DateTime fechaPago;
                              DateTime.TryParse((string)x.Element("FechaPago"), out fechaPago);

                              DateTime fechaFacturaProveedor;
                              DateTime.TryParse((string)x.Element("FechaFacturaProveedor"), out fechaFacturaProveedor);

                              decimal importeFacturaProveedor;
                              decimal.TryParse((string)x.Element("ImporteFacturaProveedor"), NumberStyles.Any,
                                  CultureInfo.InvariantCulture, out importeFacturaProveedor);

                              string codigoEstado = (string)x.Element("Estado") ?? string.Empty;
                              var estado = EstadoPago.ObtenerPorCodigo(codigoEstado);

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

                              return new BEPagoProveedor
                              {
                                  Id = id,
                                  OrdenDeCompra = ordenId > 0 ? new BEOrdenDeCompra { Id = ordenId } : null,
                                  Importe = importe,
                                  FechaPago = fechaPago,
                                  NumeroFacturaProveedor = (string)x.Element("NumeroFacturaProveedor") ?? string.Empty,
                                  FechaFacturaProveedor = fechaFacturaProveedor,
                                  ImporteFacturaProveedor = importeFacturaProveedor,
                                  Estado = estado,
                                  MedioDePago = medio
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de pagos a proveedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de pagos a proveedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar pagos a proveedores desde XML", ex);
            }
        }

        public BEPagoProveedor BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(p => p.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de pagos a proveedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de pagos a proveedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar pago a proveedor por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("PagoProveedor").Any()
                    ? doc.Descendants("PagoProveedor").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de pagos a proveedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de pagos a proveedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de pago a proveedor en XML", ex);
            }
        }

        #endregion
    }
}
