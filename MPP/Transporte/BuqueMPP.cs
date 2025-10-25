using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE;
using BE.Enums;
using MPP.Transversal;

namespace MPP.Flota
{
    public class BuqueMPP : IRepositorio<BEBuque>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEBuque>();

        #region "Alta"

        public void Alta(BEBuque obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                // Crear nodo Buque
                var nuevoBuque = new XElement("Buque",
                    new XAttribute("Id", obj.Id),
                    new XElement("Nombre", obj.Nombre ?? string.Empty),
                    new XElement("CodigoIMO", obj.CodigoIMO ?? string.Empty),
                    new XElement("CapacidadTEU", obj.CapacidadTEU),
                    new XElement("CapacidadPeso", obj.CapacidadPeso),
                    new XElement("Disponibilidad", obj.Disponibilidad),
                    new XElement("Estado", obj.Estado.ToString()),
                    new XElement("Itinerarios",
                        obj.Itinerarios != null
                            ? obj.Itinerarios.Select(i => new XElement("Itinerario", i))
                            : null
                    )
                );

                // Agregar al documento
                var root = doc.Element("Buques");
                if (root == null)
                    doc.Add(new XElement("Buques", nuevoBuque));
                else
                    root.Add(nuevoBuque);

                // Guardar cambios
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de buques", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de buques. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar buque en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEBuque obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);

                // Buscar buque por Id
                var buque = doc.Descendants("Buque")
                               .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Eliminar si existe
                if (buque != null)
                {
                    buque.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de buques", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de buques. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar buque en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEBuque obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);

                // Buscar buque por Id
                var buque = doc.Descendants("Buque")
                               .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Actualizar si existe
                if (buque != null)
                {
                    buque.SetElementValue("Nombre", obj.Nombre ?? string.Empty);
                    buque.SetElementValue("CodigoIMO", obj.CodigoIMO ?? string.Empty);
                    buque.SetElementValue("CapacidadTEU", obj.CapacidadTEU);
                    buque.SetElementValue("CapacidadPeso", obj.CapacidadPeso);
                    buque.SetElementValue("Disponibilidad", obj.Disponibilidad);
                    buque.SetElementValue("Estado", obj.Estado.ToString());

                    // Actualizar itinerarios
                    buque.Element("Itinerarios")?.Remove();
                    buque.Add(new XElement("Itinerarios",
                        obj.Itinerarios != null
                            ? obj.Itinerarios.Select(i => new XElement("Itinerario", i))
                            : null
                    ));

                    // Guardar cambios
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de buques", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de buques. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar buque en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEBuque> Listar()
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);

                // Reconstruir lista de buques
                return doc.Descendants("Buque")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string nombre = (string)x.Element("Nombre") ?? string.Empty;
                              string codigoIMO = (string)x.Element("CodigoIMO") ?? string.Empty;

                              int capacidadTEU;
                              int.TryParse((string)x.Element("CapacidadTEU"), out capacidadTEU);

                              decimal capacidadPeso;
                              decimal.TryParse((string)x.Element("CapacidadPeso"), out capacidadPeso);

                              decimal disponibilidad;
                              decimal.TryParse((string)x.Element("Disponibilidad"), out disponibilidad);

                              EstadoBuqueEnum estado;
                              Enum.TryParse((string)x.Element("Estado"), true, out estado);

                              var itinerarios = x.Element("Itinerarios")?
                                  .Elements("Itinerario")
                                  .Select(i => (string)i ?? string.Empty)
                                  .ToList() ?? new List<string>();

                              return new BEBuque
                              {
                                  Id = id,
                                  Nombre = nombre,
                                  CodigoIMO = codigoIMO,
                                  CapacidadTEU = capacidadTEU,
                                  CapacidadPeso = capacidadPeso,
                                  Disponibilidad = disponibilidad,
                                  Estado = estado,
                                  Itinerarios = itinerarios
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de buques", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de buques. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar buques desde XML", ex);
            }
        }

        public BEBuque BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(b => b.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de buques", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de buques. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar buque por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                // Cargar documento XML y obtener Id máximo
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Buque").Any()
                    ? doc.Descendants("Buque").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de buques", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de buques. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de buque en XML", ex);
            }
        }

        #endregion
    }
}
