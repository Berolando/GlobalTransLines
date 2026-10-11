using Abstraccion;
using BE.Infra;
using Transversales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MPP.Personas
{
    public class DireccionMPP : IRepositorio<BEDireccion>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEDireccion>();

        #region "Alta"

        public void Alta(BEDireccion obj)
        {
            try
            {
                // Verificar si ya existe una dirección con los mismos datos
                var existente = BuscarCoincidencia(obj);
                if (existente != null)
                {
                    obj.Id = existente.Id;
                    return;
                }

                // Cargar documento XML
                var doc = XDocument.Load(ruta);

                // Generar nuevo Id incremental
                obj.Id = ObtenerUltimoId() + 1;

                // Crear nodo dirección
                var nuevaDireccion = new XElement("Direccion",
                    new XAttribute("Id", obj.Id),
                    new XElement("Calle", obj.Calle ?? string.Empty),
                    new XElement("Numero", obj.Numero ?? string.Empty),
                    new XElement("Ciudad", obj.Ciudad ?? string.Empty),
                    new XElement("Provincia", obj.Provincia ?? string.Empty),
                    new XElement("Pais", obj.Pais ?? string.Empty),
                    new XElement("CodigoPostal", obj.CodigoPostal ?? string.Empty),
                    new XElement("Puerto", obj.Puerto ?? string.Empty),
                    new XElement("Tipo", obj.Tipo ?? string.Empty)
                );

                // Agregar nodo al documento
                var root = doc.Element("Direcciones");
                if (root == null)
                    doc.Add(new XElement("Direcciones", nuevaDireccion));
                else
                    root.Add(nuevaDireccion);

                // Guardar cambios
                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de direcciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de direcciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar dirección en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEDireccion obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);

                // Buscar dirección por Id
                var direccion = doc.Descendants("Direccion")
                                   .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Eliminar si existe
                if (direccion != null)
                {
                    direccion.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de direcciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de direcciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar dirección en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEDireccion obj)
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);

                // Buscar dirección por Id
                var direccion = doc.Descendants("Direccion")
                                   .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                // Actualizar valores
                if (direccion != null)
                {
                    direccion.SetElementValue("Calle", obj.Calle ?? string.Empty);
                    direccion.SetElementValue("Numero", obj.Numero ?? string.Empty);
                    direccion.SetElementValue("Ciudad", obj.Ciudad ?? string.Empty);
                    direccion.SetElementValue("Provincia", obj.Provincia ?? string.Empty);
                    direccion.SetElementValue("Pais", obj.Pais ?? string.Empty);
                    direccion.SetElementValue("CodigoPostal", obj.CodigoPostal ?? string.Empty);
                    direccion.SetElementValue("Puerto", obj.Puerto ?? string.Empty);
                    direccion.SetElementValue("Tipo", obj.Tipo ?? string.Empty);

                    // Guardar cambios
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de direcciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de direcciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar dirección en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEDireccion> Listar()
        {
            try
            {
                // Cargar documento XML
                var doc = XDocument.Load(ruta);

                // Construir lista de direcciones
                return doc.Descendants("Direccion")
                          .Select(x => new BEDireccion
                          {
                              Id = (long?)x.Attribute("Id") ?? 0,
                              Calle = (string)x.Element("Calle") ?? string.Empty,
                              Numero = (string)x.Element("Numero") ?? string.Empty,
                              Ciudad = (string)x.Element("Ciudad") ?? string.Empty,
                              Provincia = (string)x.Element("Provincia") ?? string.Empty,
                              Pais = (string)x.Element("Pais") ?? string.Empty,
                              CodigoPostal = (string)x.Element("CodigoPostal") ?? string.Empty,
                              Puerto = (string)x.Element("Puerto") ?? string.Empty,
                              Tipo = (string)x.Element("Tipo") ?? string.Empty
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de direcciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de direcciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar direcciones desde XML", ex);
            }
        }

        public BEDireccion BuscarPorId(long id)
        {
            try
            {
                // Buscar dirección por Id dentro de la lista
                return Listar().FirstOrDefault(d => d.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de direcciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de direcciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar dirección por Id en XML", ex);
            }
        }

        #endregion

        #region "Auxiliares"

        // Busca coincidencias de dirección existentes para evitar duplicados
        private BEDireccion BuscarCoincidencia(BEDireccion obj)
        {
            try
            {
                var direcciones = Listar();

                return direcciones.FirstOrDefault(d =>
                    string.Equals(d.Calle, obj.Calle, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(d.Numero, obj.Numero, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(d.Ciudad, obj.Ciudad, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(d.Pais, obj.Pais, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(d.Puerto, obj.Puerto, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(d.Tipo, obj.Tipo, StringComparison.OrdinalIgnoreCase));
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de direcciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de direcciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar coincidencia de dirección en XML", ex);
            }
        }

        // Obtiene el último Id usado en el XML
        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Direccion").Any()
                    ? doc.Descendants("Direccion").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de direcciones", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de direcciones. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de dirección en XML", ex);
            }
        }

        #endregion
    }
}
