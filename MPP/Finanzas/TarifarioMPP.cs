using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BE.Dominio;
using BE.Finanzas;
using Transversales;

namespace MPP.Finanzas
{
    public class TarifarioMPP
    {
        private readonly string rutaArchivo = GeneradorRutas.UbicacionEntidad<BETarifa>();

        #region "Alta"

        public void Alta(BETarifa obj)
        {
            try
            {
                XDocument doc = XDocument.Load(rutaArchivo);
                long nuevoId = ObtenerUltimoId() + 1;
                obj.Id = nuevoId;

                XElement nodo = new XElement("Tarifa",
                    new XAttribute("Id", nuevoId),
                    new XAttribute("TipoCarga", obj.TipoCarga != null ? obj.TipoCarga.Codigo : string.Empty),
                    new XAttribute("TipoContenedor", obj.TipoContenedor != null ? obj.TipoContenedor.Codigo : string.Empty),
                    new XAttribute("Descripcion", obj.Descripcion ?? string.Empty),
                    new XAttribute("BaseUSD", obj.BaseUSD.ToString(CultureInfo.InvariantCulture)),
                    new XAttribute("CoefPeso", obj.CoefPeso.ToString(CultureInfo.InvariantCulture)),
                    new XAttribute("CoefVolumen", obj.CoefVolumen.ToString(CultureInfo.InvariantCulture)),
                    new XAttribute("Activo", obj.Activo)
                );

                doc.Root.Add(nodo);
                doc.Save(rutaArchivo);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de Tarifario", ex);
            }
            catch (XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de Tarifario. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar tarifa en XML", ex);
            }
        }

        #endregion

        #region "Baja lógica"

        public void Baja(BETarifa obj)
        {
            try
            {
                XDocument doc = XDocument.Load(rutaArchivo);
                var nodo = doc.Root.Elements("Tarifa")
                    .FirstOrDefault(x => (long)x.Attribute("Id") == obj.Id);

                if (nodo != null)
                {
                    nodo.SetAttributeValue("Activo", false);
                    doc.Save(rutaArchivo);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de Tarifario", ex);
            }
            catch (XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de Tarifario. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desactivar tarifa en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BETarifa obj)
        {
            try
            {
                XDocument doc = XDocument.Load(rutaArchivo);
                var nodo = doc.Root.Elements("Tarifa")
                    .FirstOrDefault(x => (long)x.Attribute("Id") == obj.Id);

                if (nodo != null)
                {
                    nodo.SetAttributeValue("TipoCarga", obj.TipoCarga != null ? obj.TipoCarga.Codigo : string.Empty);
                    nodo.SetAttributeValue("TipoContenedor", obj.TipoContenedor != null ? obj.TipoContenedor.Codigo : string.Empty);
                    nodo.SetAttributeValue("Descripcion", obj.Descripcion ?? string.Empty);
                    nodo.SetAttributeValue("BaseUSD", obj.BaseUSD.ToString(CultureInfo.InvariantCulture));
                    nodo.SetAttributeValue("CoefPeso", obj.CoefPeso.ToString(CultureInfo.InvariantCulture));
                    nodo.SetAttributeValue("CoefVolumen", obj.CoefVolumen.ToString(CultureInfo.InvariantCulture));
                    nodo.SetAttributeValue("Activo", obj.Activo);
                    doc.Save(rutaArchivo);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de Tarifario", ex);
            }
            catch (XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de Tarifario. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar tarifa en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / ID"

        public List<BETarifa> Listar()
        {
            try
            {
                XDocument doc = XDocument.Load(rutaArchivo);

                return doc.Root.Elements("Tarifa")
                    .Select(x => new BETarifa
                    {
                        Id = (long)x.Attribute("Id"),
                        TipoCarga = TipoCarga.ObtenerPorCodigo((string)x.Attribute("TipoCarga")),
                        TipoContenedor = TipoContenedor.ObtenerPorCodigo((string)x.Attribute("TipoContenedor")),
                        Descripcion = (string)x.Attribute("Descripcion"),
                        BaseUSD = decimal.Parse((string)x.Attribute("BaseUSD"), NumberStyles.Any, CultureInfo.InvariantCulture),
                        CoefPeso = decimal.Parse((string)x.Attribute("CoefPeso"), NumberStyles.Any, CultureInfo.InvariantCulture),
                        CoefVolumen = decimal.Parse((string)x.Attribute("CoefVolumen"), NumberStyles.Any, CultureInfo.InvariantCulture),
                        Activo = (bool?)x.Attribute("Activo") ?? true
                    })
                    .ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de Tarifario", ex);
            }
            catch (XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de Tarifario. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar tarifas desde XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            XDocument doc = XDocument.Load(rutaArchivo);
            var ids = doc.Root.Elements("Tarifa").Select(x => (long)x.Attribute("Id"));
            return ids.Any() ? ids.Max() : 0;
        }

        #endregion
    }
}