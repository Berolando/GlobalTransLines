using Abstraccion;
using BE.Dominio;
using BE.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Transversales;

namespace MPP.Infra
{
    public class EventoMPP : IRepositorio<BEEvento>
    {
        // Ruta dinámica generada por el inicializador transversal
        private readonly string ruta = GeneradorRutas.UbicacionEntidad<BEEvento>();

        #region "Alta"

        // Guarda un nuevo evento en el archivo XML de eventos
        public void Alta(BEEvento obj)
        {
            try
            {
                if (obj == null)
                    throw new ArgumentNullException("obj", "El evento no puede ser nulo.");

                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                var nuevoEvento = new XElement("Evento",
                    new XAttribute("Id", obj.Id),
                    new XElement("Fecha", obj.Fecha.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("UsuarioId", obj.Usuario != null ? obj.Usuario.Id : 0),
                    new XElement("Descripcion", obj.Descripcion ?? string.Empty),
                    new XElement("Tipo", obj.Tipo != null ? obj.Tipo.Codigo : string.Empty),
                    new XElement("Origen", obj.Origen ?? "Sistema")
                );

                doc.Root.Add(nuevoEvento);
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de eventos.", ex);
            }
            catch (XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de eventos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar evento en XML.", ex);
            }
        }

        #endregion

        #region "Listar"

        // Devuelve la lista completa de eventos registrados en el archivo XML
        public List<BEEvento> Listar()
        {
            try
            {
                var lista = new List<BEEvento>();
                var doc = XDocument.Load(ruta);

                foreach (var e in doc.Root.Elements("Evento"))
                {
                    string codigoTipo = (string)e.Element("Tipo") ?? string.Empty;
                    TipoOperacion tipo = TipoOperacion.ObtenerPorCodigo(codigoTipo);

                    var evento = new BEEvento
                    {
                        Id = (long?)e.Attribute("Id") ?? 0,
                        Fecha = DateTime.TryParse((string)e.Element("Fecha"), out var f) ? f : DateTime.MinValue,
                        Usuario = new BEUsuario { Id = (long?)e.Element("UsuarioId") ?? 0 },
                        Descripcion = (string)e.Element("Descripcion") ?? string.Empty,
                        Tipo = tipo ?? TipoOperacion.Informativo,
                        Origen = (string)e.Element("Origen") ?? "Sistema"
                    };

                    lista.Add(evento);
                }

                // Devuelve la lista ordenada del más reciente al más antiguo
                return lista.OrderByDescending(x => x.Fecha).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de eventos.", ex);
            }
            catch (XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de eventos. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar eventos desde XML.", ex);
            }
        }

        #endregion

        #region "Obtener último Id"

        // Devuelve el último Id usado en el archivo XML
        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                var ids = doc.Root.Elements("Evento")
                                  .Select(x => (long?)x.Attribute("Id") ?? 0);
                return ids.Any() ? ids.Max() : 0;
            }
            catch
            {
                return 0; // Si el archivo está vacío o da error
            }
        }

        #endregion

        #region "No implementados (por compatibilidad con IRepositorio)"

        public void Baja(BEEvento obj) { }

        public BEEvento BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar evento por Id.", ex);
            }
        }

        public void Modificar(BEEvento obj) { }

        #endregion
    }
}
