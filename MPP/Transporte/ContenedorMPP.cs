using Abstraccion;
using BE.Dominio; // Para TipoContenedor y EstadoContenedor
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
    public class ContenedorMPP : IRepositorio<BEContenedor>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEContenedor>();

        #region "Alta"

        public void Alta(BEContenedor obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;
                obj.Codigo = GeneradorNumeros.GenerarNumeroContenedor((obj.Tipo ?? TipoContenedor.Standard20).Codigo,obj.Id);

                var nuevoContenedor = new XElement("Contenedor",
                    new XAttribute("Id", obj.Id),
                    new XElement("Codigo", obj.Codigo ?? string.Empty),
                    new XElement("Tipo", obj.Tipo != null ? obj.Tipo.Codigo : TipoContenedor.Standard20.Codigo),
                    new XElement("Estado", obj.Estado != null ? obj.Estado.Codigo : EstadoContenedor.Disponible.Codigo),
                    new XElement("Restriccion", obj.Restriccion != null ? obj.Restriccion.Codigo : RestriccionContenedor.Ninguna.Codigo),
                    new XElement("BuqueId", obj.Buque != null ? obj.Buque.Id : 0),
                    new XElement("Cargas", obj.Cargas != null && obj.Cargas.Any()? obj.Cargas.Select(c => new XElement("CargaId", c.Id))
                            : new List<XElement>() 
                    )
                );

                var root = doc.Element("Contenedores");
                if (root == null)
                    doc.Add(new XElement("Contenedores", nuevoContenedor));
                else
                    root.Add(nuevoContenedor);

                doc.Save(ruta);

            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de contenedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de contenedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar contenedor en XML.", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEContenedor obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var contenedor = doc.Descendants("Contenedor")
                                    .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

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
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar contenedor en XML.", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEContenedor obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var contenedor = doc.Descendants("Contenedor")
                                    .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (contenedor != null)
                {
                    contenedor.SetElementValue("Codigo", obj.Codigo ?? string.Empty);
                    contenedor.SetElementValue("Tipo", obj.Tipo != null ? obj.Tipo.Codigo : TipoContenedor.Standard20.Codigo);
                    contenedor.SetElementValue("Estado", obj.Estado != null ? obj.Estado.Codigo : EstadoContenedor.Disponible.Codigo);
                    contenedor.SetElementValue("Restriccion", obj.Restriccion != null ? obj.Restriccion.Codigo : RestriccionContenedor.Ninguna.Codigo);
                    contenedor.SetElementValue("BuqueId", obj.Buque != null ? obj.Buque.Id : 0);

                    // Actualizar cargas
                    contenedor.Element("Cargas")?.Remove();
                    contenedor.Add(new XElement("Cargas",
                        obj.Cargas != null && obj.Cargas.Any()
                            ? obj.Cargas.Select(c => new XElement("CargaId", c.Id))
                            : new List<XElement>() 
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
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar contenedor en XML.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEContenedor> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var root = doc.Element("Contenedores");
                if (root == null)
                    return new List<BEContenedor>();

                return doc.Descendants("Contenedor")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string codigo = (string)x.Element("Codigo") ?? string.Empty;

                              string codigoTipo = (string)x.Element("Tipo") ?? TipoContenedor.Standard20.Codigo;
                              var tipo = TipoContenedor.ObtenerPorCodigo(codigoTipo) ?? TipoContenedor.Standard20;

                              decimal.TryParse((string)x.Element("CapacidadPeso"), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal capPeso);
                              decimal.TryParse((string)x.Element("CapacidadVolumen"), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal capVol);

                              string codigoEstado = (string)x.Element("Estado") ?? EstadoContenedor.Disponible.Codigo;
                              var estado = EstadoContenedor.ObtenerPorCodigo(codigoEstado) ?? EstadoContenedor.Disponible;
                             
                              string codigoRestriccion = (string)x.Element("Restriccion") ?? RestriccionContenedor.Ninguna.Codigo;
                              var restriccion = RestriccionContenedor.ObtenerPorCodigo(codigoRestriccion) ?? RestriccionContenedor.Ninguna;
                              
                              long buqueId;
                              long.TryParse((string)x.Element("BuqueId"), out buqueId);

                              var cargas = new List<BECarga>();
                              var nodoCargas = x.Element("Cargas");
                              if (nodoCargas != null)
                              {
                                  foreach (var cargaNodo in nodoCargas.Elements("CargaId"))
                                  {
                                      long cargaId;
                                      if (long.TryParse(cargaNodo.Value, out cargaId) && cargaId > 0)
                                          cargas.Add(new BECarga { Id = cargaId });
                                  }
                              }

                              return new BEContenedor
                              {
                                  Id = id,
                                  Codigo = codigo,
                                  Tipo = tipo,
                                  Estado = estado,
                                  Restriccion = restriccion,
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
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar contenedores desde XML.", ex);
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
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar contenedor por Id en XML.", ex);
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
                    + ex.LineNumber + ", posición " + ex.LinePosition + ".", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último Id de contenedor en XML.", ex);
            }
        }

        #endregion
    }
}
