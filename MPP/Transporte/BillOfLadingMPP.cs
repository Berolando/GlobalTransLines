using Abstraccion;
using BE.Personas;
using BE.Transporte;
using Transversales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace MPP.Documentacion
{
    public class BillofLadingMPP : IRepositorio<BEBillofLading>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEBillofLading>();

        #region "Alta"

        public void Alta(BEBillofLading obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;
                obj.Numero = GeneradorNumeros.GenerarNumero(typeof(BEBillofLading), obj.Id);

                var nuevoBL = new XElement("BillofLading",
                    new XAttribute("Id", obj.Id),
                    new XElement("Numero", obj.Numero ?? string.Empty),
                    new XElement("BookingId", obj.Booking != null ? obj.Booking.Id : 0),
                    new XElement("ClienteId", obj.Cliente != null ? obj.Cliente.Id : 0),
                    new XElement("CargaId", obj.Carga != null ? obj.Carga.Id : 0),
                    new XElement("ContenedorId", obj.Contenedor != null ? obj.Contenedor.Id : 0),
                    new XElement("BuqueId", obj.Buque != null ? obj.Buque.Id : 0),
                    new XElement("FechaEmision", obj.FechaEmision.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("PuertoDestino", obj.PuertoDestino ?? string.Empty)
                );

                var root = doc.Element("BillofLadings");
                if (root == null)
                    doc.Add(new XElement("BillofLadings", nuevoBL));
                else
                    root.Add(nuevoBL);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de Bill of Lading", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de Bill of Lading. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar Bill of Lading en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEBillofLading obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var bl = doc.Descendants("BillofLading").FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (bl != null)
                {
                    bl.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de Bill of Lading", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de Bill of Lading. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar Bill of Lading en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEBillofLading obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var bl = doc.Descendants("BillofLading").FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (bl != null)
                {
                    bl.SetElementValue("Numero", obj.Numero ?? string.Empty);
                    bl.SetElementValue("BookingId", obj.Booking != null ? obj.Booking.Id : 0);
                    bl.SetElementValue("ClienteId", obj.Cliente != null ? obj.Cliente.Id : 0);
                    bl.SetElementValue("CargaId", obj.Carga != null ? obj.Carga.Id : 0);
                    bl.SetElementValue("ContenedorId", obj.Contenedor != null ? obj.Contenedor.Id : 0);
                    bl.SetElementValue("BuqueId", obj.Buque != null ? obj.Buque.Id : 0);
                    bl.SetElementValue("FechaEmision", obj.FechaEmision.ToString("yyyy-MM-ddTHH:mm:ss"));
                    bl.SetElementValue("PuertoDestino", obj.PuertoDestino ?? string.Empty);
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de Bill of Lading", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de Bill of Lading. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar Bill of Lading en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEBillofLading> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("BillofLading")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string numero = (string)x.Element("Numero") ?? string.Empty;
                              long bookingId; long.TryParse((string)x.Element("BookingId"), out bookingId);
                              long clienteId; long.TryParse((string)x.Element("ClienteId"), out clienteId);
                              long cargaId; long.TryParse((string)x.Element("CargaId"), out cargaId);
                              long contenedorId; long.TryParse((string)x.Element("ContenedorId"), out contenedorId);
                              long buqueId; long.TryParse((string)x.Element("BuqueId"), out buqueId);
                              DateTime fechaEmision; DateTime.TryParse((string)x.Element("FechaEmision"), out fechaEmision);
                              string puertoDestino = (string)x.Element("PuertoDestino") ?? string.Empty;

                              return new BEBillofLading
                              {
                                  Id = id,
                                  Numero = numero,
                                  Booking = bookingId > 0 ? new BEBooking { Id = bookingId } : null,
                                  Cliente = clienteId > 0 ? new BECliente { Id = clienteId } : null,
                                  Carga = cargaId > 0 ? new BECarga { Id = cargaId } : null,
                                  Contenedor = contenedorId > 0 ? new BEContenedor { Id = contenedorId } : null,
                                  Buque = buqueId > 0 ? new BEBuque { Id = buqueId } : null,
                                  FechaEmision = fechaEmision,
                                  PuertoDestino = puertoDestino
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de Bill of Lading", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de Bill of Lading. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar Bill of Lading desde XML", ex);
            }
        }

        public BEBillofLading BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(b => b.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de Bill of Lading", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de Bill of Lading. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar Bill of Lading por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("BillofLading").Any()
                    ? doc.Descendants("BillofLading").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de Bill of Lading", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de Bill of Lading. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de Bill of Lading en XML", ex);
            }
        }

        #endregion
    }
}
