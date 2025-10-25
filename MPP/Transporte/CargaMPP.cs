using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE;
using BE.Enums;
using BE.Transversal;
using MPP.Transversal;

namespace MPP.Cargas
{
    public class CargaMPP : IRepositorio<BECarga>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BECarga>();

        #region "Alta"

        public void Alta(BECarga obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                // Crear nodo de carga
                var nuevaCarga = new XElement("Carga",
                    new XAttribute("Id", obj.Id),
                    new XElement("Descripcion", obj.Descripcion ?? string.Empty),
                    new XElement("Marca", obj.Tipo.ToString()),
                    new XElement("Peso", obj.Peso),
                    new XElement("Volumen", obj.Volumen),
                    new XElement("OrigenId", obj.Origen != null ? obj.Origen.Id : 0),
                    new XElement("DestinoId", obj.Destino != null ? obj.Destino.Id : 0),
                    new XElement("FechaRegistro", obj.FechaRegistro.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("FechaEntregaEstimada", obj.FechaEntregaEstimada.ToString("yyyy-MM-ddTHH:mm:ss"))
                );

                // Agregar al documento
                var root = doc.Element("Cargas");
                if (root == null)
                    doc.Add(new XElement("Cargas", nuevaCarga));
                else
                    root.Add(nuevaCarga);

                // Guardar cambios
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de cargas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de cargas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la carga en XML.", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BECarga obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Buscar nodo por Id
                var carga = doc.Descendants("Carga")
                               .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Eliminar si existe
                if (carga != null)
                {
                    carga.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de cargas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de cargas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la carga en XML.", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BECarga obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Buscar carga por Id
                var carga = doc.Descendants("Carga")
                               .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Modificar si existe
                if (carga != null)
                {
                    carga.SetElementValue("Descripcion", obj.Descripcion ?? string.Empty);
                    carga.SetElementValue("Marca", obj.Tipo.ToString());
                    carga.SetElementValue("Peso", obj.Peso);
                    carga.SetElementValue("Volumen", obj.Volumen);
                    carga.SetElementValue("OrigenId", obj.Origen != null ? obj.Origen.Id : 0);
                    carga.SetElementValue("DestinoId", obj.Destino != null ? obj.Destino.Id : 0);
                    carga.SetElementValue("FechaRegistro", obj.FechaRegistro.ToString("yyyy-MM-ddTHH:mm:ss"));
                    carga.SetElementValue("FechaEntregaEstimada", obj.FechaEntregaEstimada.ToString("yyyy-MM-ddTHH:mm:ss"));

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de cargas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de cargas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la carga en XML.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BECarga> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Reconstrucción de objetos Carga
                return doc.Descendants("Carga")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string descripcion = (string)x.Element("Descripcion") ?? string.Empty;

                              TipoCargaEnum tipo;
                              Enum.TryParse((string)x.Element("Marca"), true, out tipo);

                              decimal peso;
                              decimal.TryParse((string)x.Element("Peso"), out peso);

                              decimal volumen;
                              decimal.TryParse((string)x.Element("Volumen"), out volumen);

                              DateTime fechaRegistro;
                              DateTime.TryParse((string)x.Element("FechaRegistro"), out fechaRegistro);

                              DateTime fechaEntrega;
                              DateTime.TryParse((string)x.Element("FechaEntregaEstimada"), out fechaEntrega);

                              long origenId;
                              long.TryParse((string)x.Element("OrigenId"), out origenId);

                              long destinoId;
                              long.TryParse((string)x.Element("DestinoId"), out destinoId);

                              return new BECarga
                              {
                                  Id = id,
                                  Descripcion = descripcion,
                                  Tipo = tipo,
                                  Peso = peso,
                                  Volumen = volumen,
                                  Origen = origenId > 0 ? new BEDireccion { Id = origenId } : null,
                                  Destino = destinoId > 0 ? new BEDireccion { Id = destinoId } : null,
                                  FechaRegistro = fechaRegistro,
                                  FechaEntregaEstimada = fechaEntrega
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de cargas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de cargas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar cargas desde XML.", ex);
            }
        }

        public BECarga BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(c => c.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de cargas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de cargas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar carga por Id en XML.", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Carga").Any()
                    ? doc.Descendants("Carga").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de cargas", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de cargas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último Id de carga en XML.", ex);
            }
        }

        #endregion
    }
}
