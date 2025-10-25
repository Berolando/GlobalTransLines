using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE;
using BE.Enums;
using BE.Transporte;
using MPP.Transversal;

namespace MPP.Transporte
{
    public class ConsolidacionMPP : IRepositorio<BEConsolidacion>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEConsolidacion>();

        #region "Alta"

        public void Alta(BEConsolidacion obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                // Crear nodo Consolidación
                var nuevaConsolidacion = new XElement("Consolidacion",
                    new XAttribute("Id", obj.Id),
                    new XElement("ContenedorId", obj.Contenedor != null ? obj.Contenedor.Id : 0),
                    new XElement("CapacidadOcupada", obj.CapacidadOcupada),
                    new XElement("CapacidadTotal", obj.CapacidadTotal),
                    new XElement("Estado", obj.Estado.ToString()),
                    new XElement("FechaConsolidacion", obj.FechaConsolidacion.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Cargas",
                        obj.Cargas != null
                            ? obj.Cargas.Select(c => new XElement("CargaId", c.Id))
                            : null
                    )
                );

                // Agregar al documento
                var root = doc.Element("Consolidaciones");
                if (root == null)
                    doc.Add(new XElement("Consolidaciones", nuevaConsolidacion));
                else
                    root.Add(nuevaConsolidacion);

                // Guardar cambios
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de consolidaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de consolidaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar consolidación en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEConsolidacion obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Buscar nodo por Id
                var consolidacion = doc.Descendants("Consolidacion")
                                       .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Eliminar si existe
                if (consolidacion != null)
                {
                    consolidacion.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de consolidaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de consolidaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar consolidación en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEConsolidacion obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Buscar consolidación por Id
                var consolidacion = doc.Descendants("Consolidacion")
                                       .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Modificar si existe
                if (consolidacion != null)
                {
                    consolidacion.SetElementValue("ContenedorId", obj.Contenedor != null ? obj.Contenedor.Id : 0);
                    consolidacion.SetElementValue("CapacidadOcupada", obj.CapacidadOcupada);
                    consolidacion.SetElementValue("CapacidadTotal", obj.CapacidadTotal);
                    consolidacion.SetElementValue("Estado", obj.Estado.ToString());
                    consolidacion.SetElementValue("FechaConsolidacion", obj.FechaConsolidacion.ToString("yyyy-MM-ddTHH:mm:ss"));

                    // Actualizar cargas
                    consolidacion.Element("Cargas")?.Remove();
                    consolidacion.Add(new XElement("Cargas",
                        obj.Cargas != null
                            ? obj.Cargas.Select(c => new XElement("CargaId", c.Id))
                            : null
                    ));

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de consolidaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de consolidaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar consolidación en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEConsolidacion> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Reconstrucción de objetos Consolidación
                return doc.Descendants("Consolidacion")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;

                              long contenedorId;
                              long.TryParse((string)x.Element("ContenedorId"), out contenedorId);

                              double capOcupada;
                              double.TryParse((string)x.Element("CapacidadOcupada"), out capOcupada);

                              double capTotal;
                              double.TryParse((string)x.Element("CapacidadTotal"), out capTotal);

                              EstadoConsolidacionEnum estado;
                              Enum.TryParse((string)x.Element("Estado"), true, out estado);

                              DateTime fecha;
                              DateTime.TryParse((string)x.Element("FechaConsolidacion"), out fecha);

                              var cargas = x.Element("Cargas")?
                                  .Elements("CargaId")
                                  .Select(c => new BECarga { Id = (long?)c ?? 0 })
                                  .Where(c => c.Id > 0)
                                  .ToList() ?? new List<BECarga>();

                              return new BEConsolidacion
                              {
                                  Id = id,
                                  Contenedor = contenedorId > 0 ? new BEContenedor { Id = contenedorId } : null,
                                  CapacidadOcupada = capOcupada,
                                  CapacidadTotal = capTotal,
                                  Estado = estado,
                                  FechaConsolidacion = fecha,
                                  Cargas = cargas
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de consolidaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de consolidaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar consolidaciones desde XML", ex);
            }
        }

        public BEConsolidacion BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(c => c.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de consolidaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de consolidaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar consolidación por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Consolidacion").Any()
                    ? doc.Descendants("Consolidacion").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de consolidaciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de consolidaciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de consolidación en XML", ex);
            }
        }

        #endregion
    }
}
