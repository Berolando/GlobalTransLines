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
    public class ContenedorMPP : IRepositorio<BEContenedor>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEContenedor>();

        #region "Alta"

        public void Alta(BEContenedor obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                // Crear nodo Contenedor
                var nuevoContenedor = new XElement("Contenedor",
                    new XAttribute("Id", obj.Id),
                    new XElement("Codigo", obj.Codigo ?? string.Empty),
                    new XElement("Marca", obj.Tipo.ToString()),
                    new XElement("CapacidadPeso", obj.CapacidadPeso),
                    new XElement("CapacidadVolumen", obj.CapacidadVolumen),
                    new XElement("Estado", obj.Estado.ToString()),
                    new XElement("BuqueId", obj.Buque != null ? obj.Buque.Id : 0),
                    new XElement("Cargas",
                        obj.Cargas != null
                            ? obj.Cargas.Select(c => new XElement("CargaId", c.Id))
                            : null
                    )
                );

                // Agregar al documento
                var root = doc.Element("Contenedores");
                if (root == null)
                    doc.Add(new XElement("Contenedores", nuevoContenedor));
                else
                    root.Add(nuevoContenedor);

                // Guardar cambios
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de contenedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de contenedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar contenedor en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEContenedor obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Buscar nodo por Id
                var contenedor = doc.Descendants("Contenedor")
                                    .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Eliminar si existe
                if (contenedor != null)
                {
                    contenedor.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de contenedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de contenedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar contenedor en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEContenedor obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Buscar contenedor por Id
                var contenedor = doc.Descendants("Contenedor")
                                    .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Modificar si existe
                if (contenedor != null)
                {
                    contenedor.SetElementValue("Codigo", obj.Codigo ?? string.Empty);
                    contenedor.SetElementValue("Marca", obj.Tipo.ToString());
                    contenedor.SetElementValue("CapacidadPeso", obj.CapacidadPeso);
                    contenedor.SetElementValue("CapacidadVolumen", obj.CapacidadVolumen);
                    contenedor.SetElementValue("Estado", obj.Estado.ToString());
                    contenedor.SetElementValue("BuqueId", obj.Buque != null ? obj.Buque.Id : 0);

                    // Actualizar cargas
                    contenedor.Element("Cargas")?.Remove();
                    contenedor.Add(new XElement("Cargas",
                        obj.Cargas != null
                            ? obj.Cargas.Select(c => new XElement("CargaId", c.Id))
                            : null
                    ));

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de contenedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de contenedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar contenedor en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEContenedor> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Reconstrucción de objetos Contenedor
                return doc.Descendants("Contenedor")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string codigo = (string)x.Element("Codigo") ?? string.Empty;

                              TipoContenedorEnum tipo;
                              Enum.TryParse((string)x.Element("Marca"), true, out tipo);

                              decimal capacidadPeso;
                              decimal.TryParse((string)x.Element("CapacidadPeso"), out capacidadPeso);

                              decimal capacidadVolumen;
                              decimal.TryParse((string)x.Element("CapacidadVolumen"), out capacidadVolumen);

                              EstadoContenedorEnum estado;
                              Enum.TryParse((string)x.Element("Estado"), true, out estado);

                              long buqueId;
                              long.TryParse((string)x.Element("BuqueId"), out buqueId);

                              var cargas = x.Element("Cargas")?
                                  .Elements("CargaId")
                                  .Select(c => new BECarga { Id = (long?)c ?? 0 })
                                  .Where(c => c.Id > 0)
                                  .ToList() ?? new List<BECarga>();

                              return new BEContenedor
                              {
                                  Id = id,
                                  Codigo = codigo,
                                  Tipo = tipo,
                                  CapacidadPeso = capacidadPeso,
                                  CapacidadVolumen = capacidadVolumen,
                                  Estado = estado,
                                  Buque = buqueId > 0 ? new BEBuque { Id = buqueId } : null,
                                  Cargas = cargas
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de contenedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de contenedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar contenedores desde XML", ex);
            }
        }

        public BEContenedor BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(c => c.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de contenedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de contenedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar contenedor por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Contenedor").Any()
                    ? doc.Descendants("Contenedor").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de contenedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de contenedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último Id de contenedor en XML", ex);
            }
        }

        #endregion
    }
}
