using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE;
using BE.Transversal;
using MPP.Transversal;

namespace MPP.Personas
{
    public class EmpleadoMPP : IRepositorio<BEEmpleado>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEEmpleado>();

        #region "Alta"

        public void Alta(BEEmpleado obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                var nuevoEmpleado = new XElement("Empleado",
                    new XAttribute("Id", obj.Id),
                    new XElement("Nombre", obj.Nombre ?? string.Empty),
                    new XElement("Apellido", obj.Apellido ?? string.Empty),
                    new XElement("Telefono", obj.Telefono ?? string.Empty),
                    new XElement("Email", obj.Email ?? string.Empty),
                    new XElement("Cargo", obj.Cargo ?? string.Empty),
                    new XElement("Sector", obj.Sector ?? string.Empty),
                    new XElement("Sueldo", obj.Sueldo),
                    new XElement("Usuario", obj.Usuario != null ? obj.Usuario.Username : string.Empty),
                    new XElement("DireccionId", obj.Direccion != null ? obj.Direccion.Id : 0)
                );

                var root = doc.Element("Empleados");
                if (root == null)
                    doc.Add(new XElement("Empleados", nuevoEmpleado));
                else
                    root.Add(nuevoEmpleado);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de empleados", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de empleados. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar empleado en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEEmpleado obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var empleado = doc.Descendants("Empleado")
                                  .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (empleado != null)
                {
                    empleado.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de empleados", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de empleados. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar empleado en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEEmpleado obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var empleado = doc.Descendants("Empleado")
                                  .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (empleado != null)
                {
                    empleado.SetElementValue("Nombre", obj.Nombre ?? string.Empty);
                    empleado.SetElementValue("Apellido", obj.Apellido ?? string.Empty);
                    empleado.SetElementValue("Telefono", obj.Telefono ?? string.Empty);
                    empleado.SetElementValue("Email", obj.Email ?? string.Empty);
                    empleado.SetElementValue("Cargo", obj.Cargo ?? string.Empty);
                    empleado.SetElementValue("Sector", obj.Sector ?? string.Empty);
                    empleado.SetElementValue("Sueldo", obj.Sueldo);
                    empleado.SetElementValue("Usuario", obj.Usuario != null ? obj.Usuario.Username : string.Empty);
                    empleado.SetElementValue("DireccionId", obj.Direccion != null ? obj.Direccion.Id : 0);
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de empleados", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de empleados. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar empleado en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEEmpleado> Listar()
        {
            try
            {
                var doc = XDocument.Load(ruta);

                return doc.Descendants("Empleado")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              string nombre = (string)x.Element("Nombre") ?? string.Empty;
                              string apellido = (string)x.Element("Apellido") ?? string.Empty;
                              string telefono = (string)x.Element("Telefono") ?? string.Empty;
                              string email = (string)x.Element("Email") ?? string.Empty;
                              string cargo = (string)x.Element("Cargo") ?? string.Empty;
                              string sector = (string)x.Element("Sector") ?? string.Empty;

                              double sueldo;
                              double.TryParse((string)x.Element("Sueldo"), out sueldo);

                              string username = (string)x.Element("Usuario") ?? string.Empty;

                              long dirId;
                              long.TryParse((string)x.Element("DireccionId"), out dirId);

                              return new BEEmpleado
                              {
                                  Id = id,
                                  Nombre = nombre,
                                  Apellido = apellido,
                                  Telefono = telefono,
                                  Email = email,
                                  Cargo = cargo,
                                  Sector = sector,
                                  Sueldo = sueldo,
                                  Usuario = !string.IsNullOrEmpty(username)
                                      ? new BEUsuario { Username = username }
                                      : null,
                                  Direccion = dirId > 0 ? new BEDireccion { Id = dirId } : null
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de empleados", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de empleados. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar empleados desde XML", ex);
            }
        }

        public BEEmpleado BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(e => e.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de empleados", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de empleados. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar empleado por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                var doc = XDocument.Load(ruta);
                return doc.Descendants("Empleado").Any()
                    ? doc.Descendants("Empleado").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de empleados", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de empleados. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de empleado en XML", ex);
            }
        }

        #endregion
    }
}
