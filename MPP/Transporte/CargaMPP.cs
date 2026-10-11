using Abstraccion;
using BE.Dominio; // TipoCarga y EstadoCarga
using BE.Finanzas;
using BE.Transporte;
using BE.Infra;
using Transversales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MPP.Cargas
{
    public class CargaMPP : IRepositorio<BECarga>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BECarga>();

        #region "Alta"

        public void Alta(BECarga obj)
        {
            System.Diagnostics.Debug.WriteLine("==> Entrando a CargaMPP.Alta()"); // o Modificar()
            System.Diagnostics.Debug.WriteLine($"Archivo XML: {ruta}");

            try
            {
                var doc = XDocument.Load(ruta);

                obj.Id = ObtenerUltimoId() + 1;
                obj.Codigo = GeneradorNumeros.GenerarNumeroCarga(obj.Id);

                var nuevaCarga = new XElement("Carga",
                    new XAttribute("Id", obj.Id),
                    new XElement("Codigo", obj.Codigo ?? string.Empty),
                    new XElement("Descripcion", obj.Descripcion ?? string.Empty),
                    new XElement("Tipo", obj.Tipo != null ? obj.Tipo.Codigo : TipoCarga.General.Codigo),
                    new XElement("TipoContenedorCodigo", obj.TipoContenedor != null ? obj.TipoContenedor.Codigo : string.Empty),
                    new XElement("Peso", obj.Peso),
                    new XElement("Volumen", obj.Volumen),
                    new XElement("OrigenId", obj.Origen != null ? obj.Origen.Id : 0),
                    new XElement("PuertoEmbarqueId", obj.PuertoEmbarque != null ? obj.PuertoEmbarque.Id : 0),
                    new XElement("DestinoId", obj.Destino != null ? obj.Destino.Id : 0),
                    new XElement("FechaRegistro", obj.FechaRegistro.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("FechaEntregaEstimada", obj.FechaEntregaEstimada.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Estado", obj.Estado != null ? obj.Estado.Codigo : EstadoCarga.Disponible.Codigo)
                );

                var root = doc.Element("Cargas");
                if (root == null)
                    doc.Add(new XElement("Cargas", nuevaCarga));
                else
                    root.Add(nuevaCarga);

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
                System.Diagnostics.Debug.WriteLine($"==> ERROR en MPP: {ex.Message}");

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

                var carga = doc.Descendants("Carga")
                               .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

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

                var carga = doc.Descendants("Carga")
                               .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (carga != null)
                {
                    carga.SetElementValue("Codigo", obj.Codigo ?? string.Empty);
                    carga.SetElementValue("Descripcion", obj.Descripcion ?? string.Empty);
                    carga.SetElementValue("Tipo", obj.Tipo != null ? obj.Tipo.Codigo : TipoCarga.General.Codigo);
                    carga.SetElementValue("TipoContenedorCodigo", obj.TipoContenedor != null ? obj.TipoContenedor.Codigo : string.Empty);
                    carga.SetElementValue("Peso", obj.Peso);
                    carga.SetElementValue("Volumen", obj.Volumen);
                    carga.SetElementValue("OrigenId", obj.Origen != null ? obj.Origen.Id : 0);
                    carga.SetElementValue("PuertoEmbarqueId", obj.PuertoEmbarque != null ? obj.PuertoEmbarque.Id : 0);
                    carga.SetElementValue("DestinoId", obj.Destino != null ? obj.Destino.Id : 0);
                    carga.SetElementValue("FechaRegistro", obj.FechaRegistro.ToString("yyyy-MM-ddTHH:mm:ss"));
                    carga.SetElementValue("FechaEntregaEstimada", obj.FechaEntregaEstimada.ToString("yyyy-MM-ddTHH:mm:ss"));
                    carga.SetElementValue("Estado", obj.Estado != null ? obj.Estado.Codigo : EstadoCarga.Disponible.Codigo);

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

                return doc.Descendants("Carga")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string codigo = (string)x.Element("Codigo") ?? string.Empty;
                              string descripcion = (string)x.Element("Descripcion") ?? string.Empty;

                              string codigoTipo = (string)x.Element("Tipo") ?? TipoCarga.General.Codigo;
                              var tipo = TipoCarga.ObtenerPorCodigo(codigoTipo) ?? TipoCarga.General;
                              var tipoContenedorCodigo = (string)x.Element("TipoContenedorCodigo") ?? string.Empty;
                              var tipoCont = TipoContenedor.ObtenerPorCodigo(tipoContenedorCodigo);

                              decimal peso;
                              decimal.TryParse((string)x.Element("Peso"),
                                  System.Globalization.NumberStyles.Any,
                                  System.Globalization.CultureInfo.InvariantCulture,
                                  out peso);

                              decimal volumen;
                              decimal.TryParse((string)x.Element("Volumen"),
                                  System.Globalization.NumberStyles.Any,
                                  System.Globalization.CultureInfo.InvariantCulture,
                                  out volumen);

                              DateTime.TryParse((string)x.Element("FechaRegistro"), out DateTime fechaRegistro);
                              DateTime.TryParse((string)x.Element("FechaEntregaEstimada"), out DateTime fechaEntrega);

                              long.TryParse((string)x.Element("OrigenId"), out long origenId);
                              long.TryParse((string)x.Element("PuertoEmbarqueId"), out long puertoEmbarqueId);
                              long.TryParse((string)x.Element("DestinoId"), out long destinoId);

                             
                              string codigoEstado = (string)x.Element("Estado") ?? EstadoCarga.Disponible.Codigo;
                              var estado = EstadoCarga.Listar().FirstOrDefault(e => e.Codigo == codigoEstado)
                                     ?? EstadoCarga.Disponible;  

                              return new BECarga
                              {
                                  Id = id,
                                  Codigo = codigo,
                                  Descripcion = descripcion,
                                  Tipo = tipo,
                                  TipoContenedor = tipoCont,
                                  Peso = peso,
                                  Volumen = volumen,
                                  Origen = origenId > 0 ? new BEDireccion { Id = origenId } : null,
                                  PuertoEmbarque = puertoEmbarqueId > 0 ? new BEDireccion { Id = puertoEmbarqueId } : null,
                                  Destino = destinoId > 0 ? new BEDireccion { Id = destinoId } : null,
                                  FechaRegistro = fechaRegistro,
                                  FechaEntregaEstimada = fechaEntrega,
                                  Estado = estado
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
