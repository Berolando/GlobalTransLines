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
                    new XElement("Activo", "true"),
                    new XElement("Nombre", obj.Nombre ?? string.Empty),
                    new XElement("Apellido", obj.Apellido ?? string.Empty),
                    new XElement("DNI", obj.DNI ?? string.Empty),
                    new XElement("Telefono", obj.Telefono ?? string.Empty),
                    new XElement("Email", obj.Email ?? string.Empty),
                    new XElement("FechaNacimiento", obj.FechaNacimiento.ToString("yyyy-MM-dd")),
                    new XElement("Cargo", obj.cargoEmpleado?.Codigo ?? string.Empty),
                    new XElement("Sueldo", obj.Sueldo),
                    new XElement("RutaFoto", obj.RutaFoto ?? string.Empty),
                    
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
                    empleado.SetElementValue("Activo", "false");
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
                    empleado.SetElementValue("DNI", obj.DNI ?? string.Empty);
                    empleado.SetElementValue("Telefono", obj.Telefono ?? string.Empty);
                    empleado.SetElementValue("Email", obj.Email ?? string.Empty);
                    empleado.SetElementValue("FechaNacimiento", obj.FechaNacimiento.ToString("yyyy-MM-dd"));
                    empleado.SetElementValue("Cargo", obj.cargoEmpleado?.Codigo ?? string.Empty);
                    empleado.SetElementValue("Sueldo", obj.Sueldo);
                    empleado.SetElementValue("RutaFoto", obj.RutaFoto ?? string.Empty);
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
                              string dni = (string)x.Element("DNI") ?? string.Empty;
                              string telefono = (string)x.Element("Telefono") ?? string.Empty;
                              string email = (string)x.Element("Email") ?? string.Empty;
                              DateTime fechaNacimiento;
                              DateTime.TryParse((string)x.Element("FechaNacimiento"), out fechaNacimiento);
                              string cargo = (string)x.Element("Cargo") ?? string.Empty;
                              var cargoEmpleado = CargoEmpleado.ObtenerPorCodigo(cargo);
                             
                              

                              double sueldo;
                              double.TryParse((string)x.Element("Sueldo"), out sueldo);
                              string rutaFoto = (string)x.Element("RutaFoto") ?? string.Empty;

                              string username = (string)x.Element("Usuario") ?? string.Empty;
                              bool activo = true;
                              var eActivo = x.Element("Activo");
                              if (eActivo != null)
                                  bool.TryParse(eActivo.Value, out activo);
                              long dirId;
                              long.TryParse((string)x.Element("DireccionId"), out dirId);

                              var empleado = new BEEmpleado()
                              {
                                  Id = id,
                                  Nombre = nombre,
                                  Apellido = apellido,
                                  DNI = dni,
                                  Telefono = telefono,
                                  Email = email,
                                  FechaNacimiento = fechaNacimiento,
                                  cargoEmpleado = cargoEmpleado,
                                  Sueldo = sueldo,
                                  RutaFoto = rutaFoto,
                                  
                                  Direccion =  null,
                                  Activo = activo
                              };
                              if (dirId > 0)
                              {
                                  try
                                  {
                                      var direccionMPP = new DireccionMPP();
                                      var direccion = direccionMPP.BuscarPorId(dirId);
                                      empleado.Direccion = direccion ?? new BEDireccion { Id = dirId };
                                  }
                                  catch
                                  {
                                      empleado.Direccion = new BEDireccion { Id = dirId };
                                  }
                              }

                              return empleado;

                              //return new BEEmpleado
                              //{
                              //    Id = id,
                              //    Nombre = nombre,
                              //    Apellido = apellido,
                              //    DNI = dni,
                              //    Telefono = telefono,
                              //    Email = email,
                              //    FechaNacimiento = fechaNacimiento,
                              //    cargoEmpleado = cargoEmpleado,
                              //    Sueldo = sueldo,
                              //    Usuario = !string.IsNullOrEmpty(username)
                              //        ? new BEUsuario { Username = username }
                              //        : null,
                              //    Direccion = dirId > 0 ? new BEDireccion { Id = dirId } : null,
                              //    Activo = activo
                              //};
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
