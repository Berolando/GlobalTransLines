using Abstraccion;
using BE.Dominio;
using BE.Transporte;
using Transversales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MPP.Transporte
{
    public class AsignacionMPP : IRepositorio<BEAsignacion>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEAsignacion>();

        #region "Alta"
        public void Alta(BEAsignacion obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                var nuevaAsignacion = new XElement("Asignacion",
                    new XAttribute("Id", obj.Id),
                    new XElement("BookingId", obj.BookingId),
                    new XElement("ContenedorId", obj.ContenedorId),
                    new XElement("BuqueId", obj.BuqueId),
                    new XElement("FechaAsignacion", obj.FechaAsignacion.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Estado", obj.Estado != null ? obj.Estado.Codigo : EstadoAsignacion.Pendiente.Codigo)
                );

                var root = doc.Element("Asignaciones");
                if (root == null)
                    doc.Add(new XElement("Asignaciones", nuevaAsignacion));
                else
                    root.Add(nuevaAsignacion);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de asignaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de asignaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar asignación en XML.", ex);
            }
        }
        #endregion

        #region "Baja"
        public void Baja(BEAsignacion obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var nodo = doc.Descendants("Asignacion")
                              .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);
                if (nodo != null)
                {
                    nodo.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de asignaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de asignaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar asignación en XML.", ex);
            }
        }
        #endregion

        #region "Modificar"
        public void Modificar(BEAsignacion obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var nodo = doc.Descendants("Asignacion")
                              .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (nodo != null)
                {
                    nodo.SetElementValue("BookingId", obj.BookingId);
                    nodo.SetElementValue("ContenedorId", obj.ContenedorId);
                    nodo.SetElementValue("BuqueId", obj.BuqueId);
                    nodo.SetElementValue("FechaAsignacion", obj.FechaAsignacion.ToString("yyyy-MM-ddTHH:mm:ss"));
                    nodo.SetElementValue("Estado", obj.Estado != null ? obj.Estado.Codigo : EstadoAsignacion.Pendiente.Codigo);
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de asignaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de asignaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar asignación en XML.", ex);
            }
        }
        #endregion

        #region "Listar / Buscar / Id"
        public List<BEAsignacion> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Asignacion")
                    .Select(x => new BEAsignacion
                    {
                        Id = (long?)x.Attribute("Id") ?? 0,
                        BookingId = (long?)x.Element("BookingId") ?? 0,
                        ContenedorId = (long?)x.Element("ContenedorId") ?? 0,
                        BuqueId = (long?)x.Element("BuqueId") ?? 0,
                        FechaAsignacion = DateTime.Parse((string)x.Element("FechaAsignacion") ?? DateTime.Now.ToString()),
                        Estado = EstadoAsignacion.ObtenerPorCodigo((string)x.Element("Estado")) ?? EstadoAsignacion.Pendiente
                    })
                    .ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de asignaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de asignaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar asignaciones desde XML.", ex);
            }
        }

        public BEAsignacion BuscarPorId(long id)
        {
            return Listar().FirstOrDefault(a => a.Id == id);
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Asignacion").Any()
                    ? doc.Descendants("Asignacion").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de asignaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de asignaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de asignación en XML.", ex);
            }
        }
        #endregion
    }
}
