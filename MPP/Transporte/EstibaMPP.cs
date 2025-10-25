using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE;
using BE.Enums;
using MPP.Transversal;

namespace MPP.Transporte
{
    public class EstibaMPP : IRepositorio<BEEstiba>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEEstiba>();

        #region "Alta"

        public void Alta(BEEstiba obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                // Crear nodo Estiba
                var nuevaEstiba = new XElement("Estiba",
                    new XAttribute("Id", obj.Id),
                    new XElement("ContenedorId", obj.Contenedor != null ? obj.Contenedor.Id : 0),
                    new XElement("BuqueId", obj.Buque != null ? obj.Buque.Id : 0),
                    new XElement("UbicacionBodega", obj.UbicacionBodega ?? string.Empty),
                    new XElement("Layout", obj.Layout ?? string.Empty),
                    new XElement("FechaPlan", obj.FechaPlan.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Estado", obj.Estado.ToString()),
                    new XElement("Cargas",
                        obj.Cargas != null
                            ? obj.Cargas.Select(c => new XElement("CargaId", c.Id))
                            : null
                    )
                );

                // Agregar al documento
                var root = doc.Element("Estibas");
                if (root == null)
                    doc.Add(new XElement("Estibas", nuevaEstiba));
                else
                    root.Add(nuevaEstiba);

                // Guardar cambios
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de estibas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de estibas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar estiba en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEEstiba obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Buscar nodo por Id
                var estiba = doc.Descendants("Estiba")
                                .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Eliminar si existe
                if (estiba != null)
                {
                    estiba.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de estibas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de estibas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar estiba en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEEstiba obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Buscar estiba por Id
                var estiba = doc.Descendants("Estiba")
                                .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Modificar si existe
                if (estiba != null)
                {
                    estiba.SetElementValue("ContenedorId", obj.Contenedor != null ? obj.Contenedor.Id : 0);
                    estiba.SetElementValue("BuqueId", obj.Buque != null ? obj.Buque.Id : 0);
                    estiba.SetElementValue("UbicacionBodega", obj.UbicacionBodega ?? string.Empty);
                    estiba.SetElementValue("Layout", obj.Layout ?? string.Empty);
                    estiba.SetElementValue("FechaPlan", obj.FechaPlan.ToString("yyyy-MM-ddTHH:mm:ss"));
                    estiba.SetElementValue("Estado", obj.Estado.ToString());

                    // Actualizar cargas
                    estiba.Element("Cargas")?.Remove();
                    estiba.Add(new XElement("Cargas",
                        obj.Cargas != null
                            ? obj.Cargas.Select(c => new XElement("CargaId", c.Id))
                            : null
                    ));

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de estibas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de estibas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar estiba en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEEstiba> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Reconstrucción de objetos Estiba
                return doc.Descendants("Estiba")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;

                              long contenedorId;
                              long.TryParse((string)x.Element("ContenedorId"), out contenedorId);

                              long buqueId;
                              long.TryParse((string)x.Element("BuqueId"), out buqueId);

                              string ubicacion = (string)x.Element("UbicacionBodega") ?? string.Empty;
                              string layout = (string)x.Element("Layout") ?? string.Empty;

                              DateTime fechaPlan;
                              DateTime.TryParse((string)x.Element("FechaPlan"), out fechaPlan);

                              EstadoEstibaEnum estado;
                              Enum.TryParse((string)x.Element("Estado"), true, out estado);

                              var cargas = x.Element("Cargas")?
                                  .Elements("CargaId")
                                  .Select(c => new BECarga { Id = (long?)c ?? 0 })
                                  .Where(c => c.Id > 0)
                                  .ToList() ?? new List<BECarga>();

                              return new BEEstiba
                              {
                                  Id = id,
                                  Contenedor = contenedorId > 0 ? new BEContenedor { Id = contenedorId } : null,
                                  Buque = buqueId > 0 ? new BEBuque { Id = buqueId } : null,
                                  UbicacionBodega = ubicacion,
                                  Layout = layout,
                                  FechaPlan = fechaPlan,
                                  Estado = estado,
                                  Cargas = cargas
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de estibas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de estibas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar estibas desde XML", ex);
            }
        }

        public BEEstiba BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(e => e.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de estibas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de estibas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar estiba por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Estiba").Any()
                    ? doc.Descendants("Estiba").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de estibas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de estibas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de estiba en XML", ex);
            }
        }

        #endregion
    }
}
