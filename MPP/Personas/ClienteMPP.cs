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
    public class ClienteMPP : IRepositorio<BECliente>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BECliente>();

        #region "Alta"

        public void Alta(BECliente obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                var nuevoCliente = new XElement("Cliente",
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
                    new XElement("CondicionFiscal", obj.CondicionFiscal?.Codigo ?? string.Empty),
                    new XElement("DireccionId", obj.Direccion != null ? obj.Direccion.Id : 0)
                );

                var root = doc.Element("Clientes");
                if (root == null)
                    doc.Add(new XElement("Clientes", nuevoCliente));
                else
                    root.Add(nuevoCliente);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de clientes", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de clientes. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar cliente en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BECliente obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var cliente = doc.Descendants("Cliente")
                                 .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (cliente != null)
                {
                    var activoElement = cliente.Element("Activo");

                    // Si el elemento no existe (clientes antiguos), lo agregamos
                    if (activoElement == null)
                    {
                        cliente.Add(new XElement("Activo", "false"));
                    }
                    else
                    {
                        activoElement.Value = "false";
                    }

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de clientes", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de clientes. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar baja lógica de cliente en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BECliente obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var cliente = doc.Descendants("Cliente")
                                 .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (cliente != null)
                {
                    cliente.SetElementValue("Nombre", obj.Nombre ?? string.Empty);
                    cliente.SetElementValue("Apellido", obj.Apellido ?? string.Empty);
                    cliente.SetElementValue("DNI", obj.DNI ?? string.Empty);
                    cliente.SetElementValue("Telefono", obj.Telefono ?? string.Empty);
                    cliente.SetElementValue("Email", obj.Email ?? string.Empty);
                    cliente.SetElementValue("FechaNacimiento", obj.FechaNacimiento.ToString("yyyy-MM-dd"));
                    cliente.SetElementValue("RazonSocial", obj.RazonSocial ?? string.Empty);
                    cliente.SetElementValue("Cuit", obj.Cuit ?? string.Empty);
                    cliente.SetElementValue("CondicionFiscal", obj.CondicionFiscal?.Codigo ?? string.Empty);
                    cliente.SetElementValue("DireccionId", obj.Direccion != null ? obj.Direccion.Id : 0);
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de clientes", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de clientes. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar cliente en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"



        public List<BECliente> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Cliente")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string nombre = (string)x.Element("Nombre") ?? string.Empty;
                              string apellido = (string)x.Element("Apellido") ?? string.Empty;
                              string dni = (string)x.Element("DNI") ?? string.Empty;
                              string telefono = (string)x.Element("Telefono") ?? string.Empty;
                              string email = (string)x.Element("Email") ?? string.Empty;

                              DateTime.TryParse((string)x.Element("FechaNacimiento"), out DateTime fechaNacimiento);

                              string razonSocial = (string)x.Element("RazonSocial") ?? string.Empty;
                              string cuit = (string)x.Element("Cuit") ?? string.Empty;

                              string condicionFiscalCod = (string)x.Element("CondicionFiscal") ?? string.Empty;
                              var condicion = CondicionFiscal.ObtenerPorCodigo(condicionFiscalCod)
                                              ?? CondicionFiscal.ConsumidorFinal;

                              //  Baja lógica: ahora se lee desde el ELEMENTO <Activo>
                              bool activo = true;
                              var eActivo = x.Element("Activo");
                              if (eActivo != null)
                                  bool.TryParse(eActivo.Value, out activo);

                              //  Dirección
                              long.TryParse((string)x.Element("DireccionId"), out long dirId);

                              var cliente = new BECliente
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

                              //  Reconstrucción segura de la dirección
                              if (dirId > 0)
                              {
                                  try
                                  {
                                      var direccionMPP = new DireccionMPP();
                                      var direccion = direccionMPP.BuscarPorId(dirId);
                                      cliente.Direccion = direccion ?? new BEDireccion { Id = dirId };
                                  }
                                  catch
                                  {
                                      cliente.Direccion = new BEDireccion { Id = dirId };
                                  }
                              }

                              return cliente;
                          })
                          .ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de clientes", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de clientes. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar clientes desde XML", ex);
            }
        }




        public BECliente BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(c => c.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de clientes", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de clientes. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cliente por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Cliente").Any()
                    ? doc.Descendants("Cliente").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de clientes", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de clientes. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de cliente en XML", ex);
            }
        }

        #endregion
    }
}
