using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE;
using BE.Enums;
using MPP.Transversal;

namespace MPP.Personas
{
    public class ProveedorMPP : IRepositorio<BEProveedor>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEProveedor>();

        #region "Alta"

        public void Alta(BEProveedor obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                var nuevoProveedor = new XElement("Proveedor",
                    new XAttribute("Id", obj.Id),
                    new XElement("RazonSocial", obj.RazonSocial ?? string.Empty),
                    new XElement("Cuit", obj.Cuit ?? string.Empty),
                    new XElement("CondicionFiscal", obj.CondicionFiscal.ToString()),
                    new XElement("Telefono", obj.Telefono ?? string.Empty),
                    new XElement("Email", obj.Email ?? string.Empty),
                    new XElement("DireccionId", obj.Direccion != null ? obj.Direccion.Id : 0)
                );

                var root = doc.Element("Proveedores");
                if (root == null)
                    doc.Add(new XElement("Proveedores", nuevoProveedor));
                else
                    root.Add(nuevoProveedor);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de proveedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de proveedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar proveedor en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEProveedor obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var proveedor = doc.Descendants("Proveedor")
                                   .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (proveedor != null)
                {
                    proveedor.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de proveedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de proveedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar proveedor en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEProveedor obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var proveedor = doc.Descendants("Proveedor")
                                   .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (proveedor != null)
                {
                    proveedor.SetElementValue("RazonSocial", obj.RazonSocial ?? string.Empty);
                    proveedor.SetElementValue("Cuit", obj.Cuit ?? string.Empty);
                    proveedor.SetElementValue("CondicionFiscal", obj.CondicionFiscal.ToString());
                    proveedor.SetElementValue("Telefono", obj.Telefono ?? string.Empty);
                    proveedor.SetElementValue("Email", obj.Email ?? string.Empty);
                    proveedor.SetElementValue("DireccionId", obj.Direccion != null ? obj.Direccion.Id : 0);
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de proveedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de proveedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar proveedor en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEProveedor> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Proveedor")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string razonSocial = (string)x.Element("RazonSocial") ?? string.Empty;
                              string cuit = (string)x.Element("Cuit") ?? string.Empty;
                              string telefono = (string)x.Element("Telefono") ?? string.Empty;
                              string email = (string)x.Element("Email") ?? string.Empty;

                              CondicionFiscalEnum condicion;
                              Enum.TryParse((string)x.Element("CondicionFiscal"), true, out condicion);

                              long dirId;
                              long.TryParse((string)x.Element("DireccionId"), out dirId);

                              return new BEProveedor
                              {
                                  Id = id,
                                  RazonSocial = razonSocial,
                                  Cuit = cuit,
                                  Telefono = telefono,
                                  Email = email,
                                  CondicionFiscal = condicion,
                                  Direccion = dirId > 0 ? new BEDireccion { Id = dirId } : null
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de proveedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de proveedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar proveedores desde XML", ex);
            }
        }

        public BEProveedor BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(p => p.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de proveedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de proveedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar proveedor por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Proveedor").Any()
                    ? doc.Descendants("Proveedor").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de proveedores", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de proveedores. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de proveedor en XML", ex);
            }
        }

        #endregion
    }
}
