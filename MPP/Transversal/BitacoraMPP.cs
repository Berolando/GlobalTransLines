using Abstraccion;
using BE.Transversal;
using BE.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MPP.Transversal
{
    public class BitacoraMPP : IRepositorio<BEBitacora>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEBitacora>();

        #region "Alta"

        public void Alta(BEBitacora obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);

                // Asignar nuevo Id incremental
                obj.Id = ObtenerUltimoId() + 1;

                // Crear nodo Bitácora
                var nuevaBitacora = new XElement("Bitacora",
                    new XAttribute("Id", obj.Id),
                    new XElement("FechaCreacion", obj.FechaCreacion.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("UsuarioResponsableId", obj.UsuarioResponsable?.Id ?? 0),
                    new XElement("Eventos",
                        obj.Eventos != null
                            ? obj.Eventos.Select(e =>
                                new XElement("Evento",
                                    new XAttribute("Id", e.Id),
                                    new XElement("Fecha", e.Fecha.ToString("yyyy-MM-ddTHH:mm:ss")),
                                    new XElement("UsuarioId", e.Usuario?.Id ?? 0),
                                    new XElement("Descripcion", e.Descripcion ?? string.Empty),
                                    new XElement("Marca", e.Tipo.ToString())
                                ))
                            : null
                    )
                );

                // Agregar al documento
                var root = doc.Element("Bitacoras");
                if (root == null)
                    doc.Add(new XElement("Bitacoras", nuevaBitacora));
                else
                    root.Add(nuevaBitacora);

                // Guardar cambios
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de bitácoras", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de bitácoras. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar bitácora en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEBitacora obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);

                // Buscar bitácora por Id
                var bitacora = doc.Descendants("Bitacora")
                                  .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Eliminar si existe
                if (bitacora != null)
                {
                    bitacora.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de bitácoras", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de bitácoras. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar bitácora en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEBitacora obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);

                // Buscar bitácora por Id
                var bitacora = doc.Descendants("Bitacora")
                                  .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Actualizar datos si existe
                if (bitacora != null)
                {
                    bitacora.SetElementValue("FechaCreacion", obj.FechaCreacion.ToString("yyyy-MM-ddTHH:mm:ss"));
                    bitacora.SetElementValue("UsuarioResponsableId", obj.UsuarioResponsable?.Id ?? 0);

                    // Reemplazar nodo de eventos
                    bitacora.Element("Eventos")?.Remove();
                    bitacora.Add(new XElement("Eventos",
                        obj.Eventos?.Select(e =>
                            new XElement("Evento",
                                new XAttribute("Id", e.Id),
                                new XElement("Fecha", e.Fecha.ToString("yyyy-MM-ddTHH:mm:ss")),
                                new XElement("UsuarioId", e.Usuario?.Id ?? 0),
                                new XElement("Descripcion", e.Descripcion ?? string.Empty),
                                new XElement("Marca", e.Tipo.ToString())
                            ))
                    ));

                    // Guardar cambios
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de bitácoras", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de bitácoras. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar bitácora en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEBitacora> Listar()
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);

                // Reconstruir lista de bitácoras
                return doc.Descendants("Bitacora")
                    .Select(x =>
                    {
                        long id = (long?)x.Attribute("Id") ?? 0;
                        DateTime.TryParse((string)x.Element("FechaCreacion"), out DateTime fechaCreacion);
                        long usuarioId = (long?)x.Element("UsuarioResponsableId") ?? 0;

                        var eventos = x.Element("Eventos")?
                            .Elements("Evento")
                            .Select(e =>
                            {
                                DateTime.TryParse((string)e.Element("Fecha"), out DateTime fechaEvento);
                                Enum.TryParse((string)e.Element("Marca"), out TipoOperacionEnum tipo);

                                return new BEEvento
                                {
                                    Id = (long?)e.Attribute("Id") ?? 0,
                                    Fecha = fechaEvento,
                                    Usuario = ((long?)e.Element("UsuarioId") ?? 0) > 0
                                        ? new BEUsuario { Id = (long)e.Element("UsuarioId") }
                                        : null,
                                    Descripcion = (string)e.Element("Descripcion") ?? string.Empty,
                                    Tipo = tipo
                                };
                            }).ToList() ?? new List<BEEvento>();

                        return new BEBitacora
                        {
                            Id = id,
                            FechaCreacion = fechaCreacion,
                            UsuarioResponsable = usuarioId > 0 ? new BEUsuario { Id = usuarioId } : null,
                            Eventos = eventos
                        };
                    }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de bitácoras", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de bitácoras. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar bitácoras desde XML", ex);
            }
        }

        public BEBitacora BuscarPorId(long id)
        {
            try
            {
                // Buscar bitácora por Id en la lista
                return Listar().FirstOrDefault(b => b.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de bitácoras", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de bitácoras. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar bitácora por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                // Cargar documento XML y obtener el Id máximo
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Bitacora").Any()
                    ? doc.Descendants("Bitacora").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de bitácoras", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de bitácoras. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de bitácora en XML", ex);
            }
        }

        #endregion
    }
}
