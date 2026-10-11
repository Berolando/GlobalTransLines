using Abstraccion;
using BE.Finanzas;
using Transversales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MPP.Finanzas
{
    public class EfectivoMPP : IRepositorio<BEEfectivo>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEEfectivo>();

        #region "Alta"

        public void Alta(BEEfectivo obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                obj.Id = ObtenerUltimoId() + 1;

                var nuevoEfectivo = new XElement("Efectivo",
                    new XAttribute("Id", obj.Id),
                    new XElement("MontoEntregado", obj.MontoEntregado),
                    new XElement("Moneda", obj.Moneda),
                    new XElement("FechaPago", obj.FechaPago),
                    new XElement("Vuelto", obj.Vuelto)
                );

                var root = doc.Element("Efectivos");
                if (root == null)
                    doc.Add(new XElement("Efectivos", nuevoEfectivo));
                else
                    root.Add(nuevoEfectivo);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de efectivos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de efectivos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar pago en efectivo en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEEfectivo obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var nodo = doc.Descendants("Efectivo")
                              .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (nodo != null)
                {
                    nodo.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de efectivos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de efectivos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar pago en efectivo en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEEfectivo obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var nodo = doc.Descendants("Efectivo")
                              .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (nodo != null)
                {
                    nodo.SetElementValue("MontoEntregado", obj.MontoEntregado);
                    nodo.SetElementValue("Moneda", obj.Moneda);
                    nodo.SetElementValue("FechaPago", obj.FechaPago);
                    nodo.SetElementValue("Vuelto", obj.Vuelto);

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de efectivos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de efectivos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar pago en efectivo en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEEfectivo> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Efectivo")
                          .Select(x => new BEEfectivo
                          {
                              Id = (long?)x.Attribute("Id") ?? 0,
                              MontoEntregado = (double?)x.Element("MontoEntregado") ?? 0d,
                              Moneda = (string)x.Element("Moneda") ?? string.Empty,
                              FechaPago = (DateTime?)x.Element("FechaPago") ?? DateTime.MinValue,
                              Vuelto = (decimal?)x.Element("Vuelto") ?? 0m
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de efectivos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de efectivos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar pagos en efectivo desde XML", ex);
            }
        }

        public BEEfectivo BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(e => e.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de efectivos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de efectivos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar pago en efectivo por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var ids = doc.Descendants("Efectivo")
                             .Select(x => (long?)x.Attribute("Id") ?? 0L);
                return ids.Any() ? ids.Max() : 0L;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de efectivos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de efectivos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception)
            {
                return 0L;
            }
        }

        #endregion
    }
}
