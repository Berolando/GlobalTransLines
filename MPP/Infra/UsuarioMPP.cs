using Abstraccion;
using BE.Personas;
using BE.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Transversales;

namespace MPP.Infra
{
    public class UsuarioMPP : IRepositorio<BEUsuario>
    {
        private string ruta = GeneradorRutas.UbicacionEntidad<BEUsuario>();

        #region "Helpers seguros"

        private static string ValStr(XElement parent, string name)
        {
            var e = parent.Element(name);
            return e != null ? (string)e : string.Empty;
        }

        private static long ValLong(XElement parent, string name, long def = 0)
        {
            var e = parent.Element(name);
            if (e == null) return def;
            long v;
            return long.TryParse((string)e, out v) ? v : def;
        }

        private static bool ValBool(XElement parent, string name, bool def = true)
        {
            var e = parent.Element(name);
            if (e == null) return def;
            bool v;
            return bool.TryParse((string)e, out v) ? v : def;
        }

        private static byte ValByte(XElement parent, string name, byte def = 0)
        {
            var e = parent.Element(name);
            if (e == null) return def;
            byte v;
            return byte.TryParse((string)e, out v) ? v : def;
        }
        private static List<BERol> LeerRolesIds(XElement usuario)
        {
            var lista = new List<BERol>();

            var roles = usuario.Element("Roles");
            if (roles != null)
            {
                foreach (var r in roles.Elements("RolId"))
                {
                    long id;
                    if (long.TryParse((string)r, out id) && id > 0)
                        lista.Add(new BERol { Id = id });
                }
                return lista;
            }

            // Compatibilidad con formato viejo
            long single = ValLong(usuario, "RolId", 0);
            if (single > 0)
                lista.Add(new BERol { Id = single });

            return lista;
        }


        #endregion

        #region "Alta"

        public void Alta(BEUsuario obj)
        {
            try
            {
                // Asegurar existencia del archivo
                if (!File.Exists(ruta))
                    new XDocument(new XElement("Usuarios")).Save(ruta);

                var doc = XDocument.Load(ruta);
                obj.Id = ObtenerUltimoId() + 1;

                var nuevoUsuario = new XElement("Usuario",
                    new XAttribute("Id", obj.Id),
                    new XElement("Activo", "true"),
                    new XElement("Username", obj.Username ?? string.Empty),
                    new XElement("Password", obj.Password ?? string.Empty),
                    new XElement("ClaveEncriptacion", obj.ClaveEncriptacion),
                    new XElement("Roles", (obj.Roles ?? new List<BERol>()).Where(r => r != null && r.Id > 0).Select(r => new XElement("RolId", r.Id))
),                  new XElement("EmpleadoId", (obj.Empleado != null) ? obj.Empleado.Id : 0)
                );

                var root = doc.Element("Usuarios");
                if (root == null)
                    throw new Exception("El archivo XML de usuarios no tiene la raíz <Usuarios>.");

                else
                    root.Add(nuevoUsuario);

                doc.Save(ruta);

            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de usuarios", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de usuarios. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar usuario en XML", ex);
            }
        }

        #endregion

        #region "Baja"

        public void Baja(BEUsuario obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var usuario = doc.Descendants("Usuario")
                                 .FirstOrDefault(x => (long?)x.Attribute("Id") == obj.Id);

                if (usuario != null)
                {
                    var activoElement = usuario.Element("Activo");
                    if (activoElement == null)
                        usuario.Add(new XElement("Activo", "false"));
                    else
                        activoElement.Value = "false";

                    doc.Save(ruta); 
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de usuarios", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de usuarios. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar baja lógica de usuario en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEUsuario obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var usuario = doc.Descendants("Usuario")
                                 .FirstOrDefault(u => (long?)u.Attribute("Id") == obj.Id);

                if (usuario != null)
                {
                    usuario.SetElementValue("Username", obj.Username ?? string.Empty);
                    usuario.SetElementValue("Password", obj.Password ?? string.Empty);
                    usuario.SetElementValue("ClaveEncriptacion", obj.ClaveEncriptacion);
                    usuario.SetElementValue("Activo", obj.Activo.ToString());
                    var rolesElem = usuario.Element("Roles");
                    var nuevoRoles = new XElement("Roles",
                        (obj.Roles ?? new List<BERol>())
                            .Where(r => r != null && r.Id > 0)
                            .Select(r => new XElement("RolId", r.Id))
                    );

                    if (rolesElem == null) usuario.Add(nuevoRoles);
                    else rolesElem.ReplaceWith(nuevoRoles);
                    usuario.SetElementValue("EmpleadoId", obj.Empleado != null ? obj.Empleado.Id : 0);

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de usuarios", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de usuarios. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar usuario en XML", ex);
            }
        }

        #endregion

        #region "Listar / Buscar / Id"

        public List<BEUsuario> Listar()
        {
            try
            {
                var lista = new List<BEUsuario>();
                if (!File.Exists(ruta))
                    return lista;

                var doc = XDocument.Load(ruta);

                return doc.Descendants("Usuario")
                          .Select(x =>
                          {
                              long id = (long?)x.Attribute("Id") ?? 0;
                              long empId = ValLong(x, "EmpleadoId", 0);
                              return new BEUsuario
                              {
                                  Id = id,
                                  Username = ValStr(x, "Username"),
                                  Password = ValStr(x, "Password"),
                                  ClaveEncriptacion = ValByte(x, "ClaveEncriptacion", 0),
                                  Activo = ValBool(x, "Activo", true),
                                  Roles = LeerRolesIds(x),
                                  Empleado = empId > 0? new BEEmpleado { Id = empId } : null
                              
                              };
                          }).ToList();
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de usuarios", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de usuarios. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios desde XML", ex);
            }
        }

        public BEUsuario BuscarPorId(long id)
        {
            try
            {
                return Listar().FirstOrDefault(u => u.Id == id);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de usuarios", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de usuarios. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuario por Id en XML", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                if (!File.Exists(ruta))
                    return 0;

                var doc = XDocument.Load(ruta);
                return doc.Descendants("Usuario").Any()
                    ? doc.Descendants("Usuario").Max(x => (long?)x.Attribute("Id") ?? 0)
                    : 0;
            }
            catch(IOException ex)
            {
                            throw new Exception("Error de acceso al archivo XML de usuarios", ex);
                        }
            catch (System.Xml.XmlException ex)
            {
                            throw new Exception(
                                "Error en la estructura del archivo XML de usuarios. Línea "
                                + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message,
                                ex
                            );
                        }
            catch (Exception ex)
            {
                            throw new Exception("Error al obtener el último Id de usuario en XML", ex);
                        }

        }

        #endregion
    }
}
