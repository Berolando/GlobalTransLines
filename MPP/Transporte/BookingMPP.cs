using Abstraccion;
using BE.Dominio;
using BE.Finanzas;
using BE.Personas;
using BE.ServiciosSubcontratados;
using BE.Transporte;
using Transversales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MPP.Bookings
{
    public class BookingMPP : IRepositorio<BEBooking>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEBooking>();

        #region "Alta"

        public void Alta(BEBooking obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;
                obj.Numero = GeneradorNumeros.GenerarNumeroBooking(obj.Id);
                var nuevoBooking = new XElement("Booking",
                    new XAttribute("Id", obj.Id),
                    new XElement("Numero", obj.Numero ?? string.Empty),
                    new XElement("ClienteId", obj.Cliente != null ? obj.Cliente.Id : 0),
                    new XElement("CotizacionId", obj.Cotizacion != null ? obj.Cotizacion.Id : 0),
                    new XElement("FechaGeneracion", obj.FechaGeneracion.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Estado", obj.Estado != null ? obj.Estado.Codigo : string.Empty),
                    new XElement("TipoContenedorCodigo", obj.TipoContenedorCodigo ?? string.Empty),
                    new XElement("FechaTentativaSalida",
                        obj.FechaTentativaSalida.HasValue
                            ? obj.FechaTentativaSalida.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                            : string.Empty),
                    new XElement("Cargas",
                        obj.Cargas != null
                            ? obj.Cargas.Select(c => new XElement("CargaId", c.Id))
                            : null
                    ),
                    new XElement("Servicios",
                        obj.Servicios != null
                            ? obj.Servicios.Select(s => new XElement("ServicioId", s.Id))
                            : null
                    )
                );

                var root = doc.Element("Bookings");
                if (root == null)
                    doc.Add(new XElement("Bookings", nuevoBooking));
                else
                    root.Add(nuevoBooking);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de bookings", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de bookings. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar booking en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEBooking obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var booking = doc.Descendants("Booking")
                                 .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (booking != null)
                {
                    booking.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de bookings", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de bookings. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar booking en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEBooking obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var booking = doc.Descendants("Booking")
                                 .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (booking != null)
                {
                    booking.SetElementValue("Numero", obj.Numero ?? string.Empty);
                    booking.SetElementValue("ClienteId", obj.Cliente != null ? obj.Cliente.Id : 0);
                    booking.SetElementValue("CotizacionId", obj.Cotizacion != null ? obj.Cotizacion.Id : 0);
                    booking.SetElementValue("FechaGeneracion", obj.FechaGeneracion.ToString("yyyy-MM-ddTHH:mm:ss"));
                    booking.SetElementValue("Estado", obj.Estado != null ? obj.Estado.Codigo : string.Empty);
                    booking.SetElementValue("TipoContenedorCodigo", obj.TipoContenedorCodigo ?? string.Empty);
                    booking.SetElementValue("FechaTentativaSalida",
                        obj.FechaTentativaSalida.HasValue
                            ? obj.FechaTentativaSalida.Value.ToString("yyyy-MM-ddTHH:mm:ss")
                            : string.Empty);

                    booking.Element("Cargas")?.Remove();
                    booking.Add(new XElement("Cargas",
                        obj.Cargas != null
                            ? obj.Cargas.Select(c => new XElement("CargaId", c.Id))
                            : null
                    ));

                    booking.Element("Servicios")?.Remove();
                    booking.Add(new XElement("Servicios",
                        obj.Servicios != null
                            ? obj.Servicios.Select(s => new XElement("ServicioId", s.Id))
                            : null
                    ));

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de bookings", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de bookings. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar booking en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEBooking> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Booking")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string numero = (string)x.Element("Numero") ?? string.Empty;

                              long clienteId = (long?)x.Element("ClienteId") ?? 0;
                              long cotizacionId = (long?)x.Element("CotizacionId") ?? 0;

                              DateTime.TryParse((string)x.Element("FechaGeneracion"), out DateTime fechaGeneracion);
                              string codigoEstado = (string)x.Element("Estado") ?? string.Empty;
                              var estado = EstadoBooking.ObtenerPorCodigo(codigoEstado);

                              string tipoCodigo = (string)x.Element("TipoContenedorCodigo") ?? string.Empty;
                              DateTime.TryParse((string)x.Element("FechaTentativaSalida"), out DateTime fechaTentativa);

                              var cargas = x.Element("Cargas")?.Elements("CargaId")
                                  .Select(c => new BECarga { Id = (long?)c ?? 0 })
                                  .Where(c => c.Id > 0).ToList() ?? new List<BECarga>();

                              var servicios = x.Element("Servicios")?.Elements("ServicioId")
                                  .Select(s => new BEServicioSubcontratado { Id = (long?)s ?? 0 })
                                  .Where(s => s.Id > 0).ToList() ?? new List<BEServicioSubcontratado>();

                              return new BEBooking
                              {
                                  Id = id,
                                  Numero = numero,
                                  Cliente = clienteId > 0 ? new BECliente { Id = clienteId } : null,
                                  Cotizacion = cotizacionId > 0 ? new BECotizacion { Id = cotizacionId } : null,
                                  FechaGeneracion = fechaGeneracion,
                                  Estado = estado,
                                  TipoContenedorCodigo = tipoCodigo,
                                  FechaTentativaSalida = fechaTentativa == DateTime.MinValue ? (DateTime?)null : fechaTentativa,
                                  Cargas = cargas,
                                  Servicios = servicios
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de bookings", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de bookings. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar bookings desde XML", ex);
            }
        }

        public BEBooking BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(b => b.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de bookings", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de bookings. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar booking por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Booking").Any()
                    ? doc.Descendants("Booking").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de bookings", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de bookings. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de booking en XML", ex);
            }
        }

        #endregion
    }
}
