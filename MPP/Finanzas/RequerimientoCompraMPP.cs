using Abstraccion;
using BE.Dominio;
using BE.Finanzas;
using BE.ServiciosSubcontratados;
using BE.Transporte;
using Transversales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MPP.Finanzas
{
    public class RequerimientoCompraMPP : IRepositorio<BERequerimientoCompra>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BERequerimientoCompra>();

        #region "Alta"

        public void Alta(BERequerimientoCompra obj)
        {
            try
            {
                if (!File.Exists(ruta))
                    new XDocument(new XElement("RequerimientoCompra")).Save(ruta);

                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                var nuevo = new XElement("RequerimientoCompra",
                    new XAttribute("Id", obj.Id),
                    new XElement("BookingId", obj.BookingOrigen != null ? obj.BookingOrigen.Id : 0),
                    new XElement("ServicioId", obj.Servicio != null ? obj.Servicio.Id : 0),
                    new XElement("FechaSolicitud", obj.FechaSolicitud.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Estado", obj.Estado != null ? obj.Estado.Codigo : EstadoRequerimientoCompra.Pendiente.Codigo)
                );

                doc.Root.Add(nuevo);
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de requerimientos de compra", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de requerimientos de compra. Linea "
                    + ex.LineNumber + ", posicion " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar requerimiento de compra en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BERequerimientoCompra obj)
        {
            try
            {
                if (!File.Exists(ruta)) return;

                var doc = XDocument.Load(ruta);
                var requerimiento = doc.Descendants("RequerimientoCompra")
                                       .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (requerimiento != null)
                {
                    requerimiento.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de requerimientos de compra", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de requerimientos de compra. Linea "
                    + ex.LineNumber + ", posicion " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar requerimiento de compra en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BERequerimientoCompra obj)
        {
            try
            {
                if (!File.Exists(ruta)) return;

                var doc = XDocument.Load(ruta);
                var requerimiento = doc.Descendants("RequerimientoCompra")
                                       .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (requerimiento != null)
                {
                    requerimiento.SetElementValue("BookingId", obj.BookingOrigen != null ? obj.BookingOrigen.Id : 0);
                    requerimiento.SetElementValue("ServicioId", obj.Servicio != null ? obj.Servicio.Id : 0);
                    requerimiento.SetElementValue("FechaSolicitud", obj.FechaSolicitud.ToString("yyyy-MM-ddTHH:mm:ss"));
                    requerimiento.SetElementValue("Estado", obj.Estado != null ? obj.Estado.Codigo : EstadoRequerimientoCompra.Pendiente.Codigo);

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de requerimientos de compra", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de requerimientos de compra. Linea "
                    + ex.LineNumber + ", posicion " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar requerimiento de compra en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BERequerimientoCompra> Listar()
        {
            try
            {
                if (!File.Exists(ruta))
                    return new List<BERequerimientoCompra>();

                var doc = XDocument.Load(ruta);

                return doc.Descendants("RequerimientoCompra")
                          .Select(x =>
                          {
                              long bookingId = (long?)x.Element("BookingId") ?? 0;
                              long servicioId = (long?)x.Element("ServicioId") ?? 0;

                              DateTime fechaSolicitud;
                              DateTime.TryParse((string)x.Element("FechaSolicitud"), out fechaSolicitud);

                              string codigoEstado = (string)x.Element("Estado") ?? EstadoRequerimientoCompra.Pendiente.Codigo;
                              var estado = EstadoRequerimientoCompra.ObtenerPorCodigo(codigoEstado) ?? EstadoRequerimientoCompra.Pendiente;

                              return new BERequerimientoCompra
                              {
                                  Id = (long?)x.Attribute("Id") ?? 0,
                                  BookingOrigen = bookingId > 0 ? new BEBooking { Id = bookingId } : null,
                                  Servicio = servicioId > 0 ? new BEServicioSubcontratado { Id = servicioId } : null,
                                  FechaSolicitud = fechaSolicitud,
                                  Estado = estado
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de requerimientos de compra", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de requerimientos de compra. Linea "
                    + ex.LineNumber + ", posicion " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar requerimientos de compra desde XML", ex);
            }
        }

        public BERequerimientoCompra BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(r => r.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de requerimientos de compra", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de requerimientos de compra. Linea "
                    + ex.LineNumber + ", posicion " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar requerimiento de compra por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                if (!File.Exists(ruta))
                    return 0;

                var doc = XDocument.Load(ruta);
                return doc.Descendants("RequerimientoCompra").Any()
                    ? doc.Descendants("RequerimientoCompra").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de requerimientos de compra", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de requerimientos de compra. Linea "
                    + ex.LineNumber + ", posicion " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ultimo Id de requerimiento de compra en XML", ex);
            }
        }

        #endregion
    }
}

