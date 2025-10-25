using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE.Finanzas;
using MPP.Transversal;

namespace MPP.Finanzas
{
    public class TarjetaMPP : IRepositorio<BETarjeta>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BETarjeta>();

        #region "Alta"

        public void Alta(BETarjeta obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                obj.Id = ObtenerUltimoId() + 1;

                var nuevaTarjeta = new XElement("Tarjeta",
                    new XAttribute("Id", obj.Id),
                    new XElement("Numero", obj.Numero),
                    new XElement("Titular", obj.Titular),
                    new XElement("Vencimiento", obj.Vencimiento),
                    new XElement("CVV", obj.CVV),
                    new XElement("Marca", obj.Marca),
                    new XElement("BancoEmisor", obj.BancoEmisor)
                );

                var root = doc.Element("Tarjetas");
                if (root == null)
                    doc.Add(new XElement("Tarjetas", nuevaTarjeta));
                else
                    root.Add(nuevaTarjeta);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de tarjetas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de tarjetas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar tarjeta en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BETarjeta obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var nodo = doc.Descendants("Tarjeta")
                              .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (nodo != null)
                {
                    nodo.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de tarjetas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de tarjetas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar tarjeta en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BETarjeta obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var nodo = doc.Descendants("Tarjeta")
                              .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (nodo != null)
                {
                    nodo.SetElementValue("Numero", obj.Numero);
                    nodo.SetElementValue("Titular", obj.Titular);
                    nodo.SetElementValue("Vencimiento", obj.Vencimiento);
                    nodo.SetElementValue("CVV", obj.CVV);
                    nodo.SetElementValue("Marca", obj.Marca);
                    nodo.SetElementValue("BancoEmisor", obj.BancoEmisor);

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de tarjetas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de tarjetas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar tarjeta en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BETarjeta> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Tarjeta")
                          .Select(x => new BETarjeta
                          {
                              Id = (long?)x.Attribute("Id") ?? 0,
                              Numero = (string)x.Element("Numero") ?? string.Empty,
                              Titular = (string)x.Element("Titular") ?? string.Empty,
                              Vencimiento = (DateTime?)x.Element("Vencimiento") ?? DateTime.MinValue,
                              CVV = (string)x.Element("CVV") ?? string.Empty,
                              Marca = (string)x.Element("Marca") ?? string.Empty,
                              BancoEmisor = (string)x.Element("BancoEmisor") ?? string.Empty
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de tarjetas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de tarjetas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar tarjetas desde XML", ex);
            }
        }

        public BETarjeta BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(t => t.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de tarjetas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de tarjetas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar tarjeta por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var ids = doc.Descendants("Tarjeta")
                             .Select(x => (long?)x.Attribute("Id") ?? 0L);
                return ids.Any() ? ids.Max() : 0L;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de tarjetas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de tarjetas. Línea "
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
