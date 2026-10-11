using Abstraccion;
using BE.Dominio;
using BE.Personas;
using BE.Infra;
using Transversales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

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
                    new XElement("Activo", "true"),
                    new XElement("Nombre", obj.Nombre ?? string.Empty),
                    new XElement("Apellido", obj.Apellido ?? string.Empty),
                    new XElement("DNI", obj.DNI ?? string.Empty),
                    new XElement("Telefono", obj.Telefono ?? string.Empty),
                    new XElement("Email", obj.Email ?? string.Empty),
                    new XElement("FechaNacimiento", obj.FechaNacimiento.ToString("yyyy-MM-dd")),
                    new XElement("RazonSocial", obj.RazonSocial ?? string.Empty),
                    new XElement("Cuit", obj.Cuit ?? string.Empty),
                    new XElement("CondicionFiscal", obj.CondicionFiscal?.Codigo ?? CondicionFiscal.ConsumidorFinal.Codigo),
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
                    var activoElement = proveedor.Element("Activo");

                    if (activoElement == null)
                        proveedor.Add(new XElement("Activo", "false"));
                    else
                        activoElement.Value = "false";

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
                throw new Exception("Error al realizar baja lógica de proveedor en XML", ex);
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
                    proveedor.SetElementValue("Nombre", obj.Nombre ?? string.Empty);
                    proveedor.SetElementValue("Apellido", obj.Apellido ?? string.Empty);
                    proveedor.SetElementValue("DNI", obj.DNI ?? string.Empty);
                    proveedor.SetElementValue("Telefono", obj.Telefono ?? string.Empty);
                    proveedor.SetElementValue("Email", obj.Email ?? string.Empty);
                    proveedor.SetElementValue("FechaNacimiento", obj.FechaNacimiento.ToString("yyyy-MM-dd"));
                    proveedor.SetElementValue("RazonSocial", obj.RazonSocial ?? string.Empty);
                    proveedor.SetElementValue("Cuit", obj.Cuit ?? string.Empty);
                    proveedor.SetElementValue("CondicionFiscal", obj.CondicionFiscal?.Codigo ?? CondicionFiscal.ConsumidorFinal.Codigo);
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
                              string nombre = (string)x.Element("Nombre") ?? string.Empty;
                              string apellido = (string)x.Element("Apellido") ?? string.Empty;
                              string dni = (string)x.Element("DNI") ?? string.Empty;
                              string telefono = (string)x.Element("Telefono") ?? string.Empty;
                              string email = (string)x.Element("Email") ?? string.Empty;
                              DateTime fechaNacimiento;
                              DateTime.TryParse((string)x.Element("FechaNacimiento"), out fechaNacimiento);

                              string razonSocial = (string)x.Element("RazonSocial") ?? string.Empty;
                              string cuit = (string)x.Element("Cuit") ?? string.Empty;
                              string condCod = (string)x.Element("CondicionFiscal") ?? CondicionFiscal.ConsumidorFinal.Codigo;
                              var condicion = CondicionFiscal.ObtenerPorCodigo(condCod) ?? CondicionFiscal.ConsumidorFinal;

                              bool activo = true;
                              var eActivo = x.Element("Activo");
                              if (eActivo != null)
                                  bool.TryParse(eActivo.Value, out activo);

                              long dirId;
                              long.TryParse((string)x.Element("DireccionId"), out dirId);

                              var proveedor = new BEProveedor
                              {
                                  Id = id,
                                  Nombre = nombre,
                                  Apellido = apellido,
                                  DNI = dni,
                                  Telefono = telefono,
                                  Email = email,
                                  FechaNacimiento = fechaNacimiento,
                                  RazonSocial = razonSocial,
                                  Cuit = cuit,
                                  CondicionFiscal = condicion,
                                  Activo = activo,
                                  Direccion = null
                              };

                              // Reconstrucción segura de dirección
                              if (dirId > 0)
                              {
                                  try
                                  {
                                      var direccionMPP = new DireccionMPP();
                                      var direcciones = direccionMPP.Listar();
                                      var direccion = direcciones.FirstOrDefault(d => d.Id == dirId);
                                      proveedor.Direccion = direccion ?? new BEDireccion { Id = dirId };
                                  }
                                  catch (Exception)
                                  {
                                      proveedor.Direccion = new BEDireccion { Id = dirId };
                                  }
                              }

                              return proveedor;
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
