using Abstraccion;
using BE.Transporte;
using Transversales;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace MPP.Transporte
{
    public class ManifiestoMPP : IRepositorio<BEManifiesto>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEManifiesto>();

        #region "Alta"

        public void Alta(BEManifiesto obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;
                obj.Numero = GeneradorNumeros.GenerarNumero(typeof(BEManifiesto), obj.Id);

                // Crear nodo Manifiesto
                var nuevoManifiesto = new XElement("Manifiesto",
                    new XAttribute("Id", obj.Id),
                    new XElement("Numero", obj.Numero ?? string.Empty),
                    new XElement("BuqueId", obj.Buque != null ? obj.Buque.Id : 0),
                    new XElement("FechaEmision", obj.FechaEmision.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("AutoridadDestino", obj.AutoridadDestino ?? string.Empty),
                    new XElement("PesoTotal", obj.PesoTotal),
                    new XElement("Contenedores",
                        obj.Contenedores != null
                            ? obj.Contenedores.Select(c => new XElement("ContenedorId", c.Id))
                            : null
                    )
                );

                // Agregar al documento
                var root = doc.Element("Manifiestos");
                if (root == null)
                    doc.Add(new XElement("Manifiestos", nuevoManifiesto));
                else
                    root.Add(nuevoManifiesto);

                // Guardar cambios
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de manifiestos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de manifiestos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar manifiesto en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEManifiesto obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Buscar nodo por Id
                var manifiesto = doc.Descendants("Manifiesto")
                                    .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Eliminar si existe
                if (manifiesto != null)
                {
                    manifiesto.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de manifiestos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de manifiestos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar manifiesto en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEManifiesto obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Buscar manifiesto por Id
                var manifiesto = doc.Descendants("Manifiesto")
                                    .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Modificar si existe
                if (manifiesto != null)
                {
                    manifiesto.SetElementValue("Numero", obj.Numero ?? string.Empty);
                    manifiesto.SetElementValue("BuqueId", obj.Buque != null ? obj.Buque.Id : 0);
                    manifiesto.SetElementValue("FechaEmision", obj.FechaEmision.ToString("yyyy-MM-ddTHH:mm:ss"));
                    manifiesto.SetElementValue("AutoridadDestino", obj.AutoridadDestino ?? string.Empty);
                    manifiesto.SetElementValue("PesoTotal", obj.PesoTotal);

                    // Actualizar contenedores
                    manifiesto.Element("Contenedores")?.Remove();
                    manifiesto.Add(new XElement("Contenedores",
                        obj.Contenedores != null
                            ? obj.Contenedores.Select(c => new XElement("ContenedorId", c.Id))
                            : null
                    ));

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de manifiestos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de manifiestos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar manifiesto en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEManifiesto> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Reconstrucción de objetos Manifiesto
                return doc.Descendants("Manifiesto")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string numero = (string)x.Element("Numero") ?? string.Empty;

                              long buqueId;
                              long.TryParse((string)x.Element("BuqueId"), out buqueId);

                              DateTime fechaEmision;
                              DateTime.TryParse((string)x.Element("FechaEmision"), out fechaEmision);

                              string autoridad = (string)x.Element("AutoridadDestino") ?? string.Empty;

                              decimal peso;
                              decimal.TryParse((string)x.Element("PesoTotal"), NumberStyles.Any,
                                  CultureInfo.InvariantCulture, out peso);

                              var contenedores = x.Element("Contenedores")?
                                  .Elements("ContenedorId")
                                  .Select(c => new BEContenedor { Id = (long?)c ?? 0 })
                                  .Where(c => c.Id > 0)
                                  .ToList() ?? new List<BEContenedor>();

                              return new BEManifiesto
                              {
                                  Id = id,
                                  Numero = numero,
                                  Buque = buqueId > 0 ? new BEBuque { Id = buqueId } : null,
                                  FechaEmision = fechaEmision,
                                  AutoridadDestino = autoridad,
                                  PesoTotal = peso,
                                  Contenedores = contenedores
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de manifiestos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de manifiestos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar manifiestos desde XML", ex);
            }
        }

        public BEManifiesto BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(m => m.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de manifiestos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de manifiestos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar manifiesto por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Manifiesto").Any()
                    ? doc.Descendants("Manifiesto").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de manifiestos", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de manifiestos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de manifiesto en XML", ex);
            }
        }

        #endregion
    }
}
