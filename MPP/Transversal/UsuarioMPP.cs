using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE;
using BE.Transversal;

namespace MPP.Transversal
{
    public class UsuarioMPP : IRepositorio<BEUsuario>
    {
        private  string ruta = GeneradorRutas.UbicacionEntidad<BEUsuario>();

        // Helpers seguros (C# 7.3) para evitar nulls y conversiones peligrosas
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

        #region "Alta"

        public void Alta(BEUsuario obj)
        {
            try
            {
                // Asegurar existencia del archivo
                if (!File.Exists(ruta))
                    new XDocument(new XElement("Usuarios")).Save(ruta);

                var doc = XDocument.Load(ruta);

                // Id incremental
                obj.Id = ObtenerUltimoId() + 1;

                // Crear nodo usuario
                var nuevoUsuario = new XElement("Usuario",
                    new XAttribute("Id", obj.Id),
                    new XElement("Username", obj.Username ?? string.Empty),
                    new XElement("Password", obj.Password ?? string.Empty),
                    new XElement("ClaveEncriptacion", obj.ClaveEncriptacion),
                    new XElement("Activo", obj.Activo),
                    new XElement("RolId", (obj.Rol != null) ? obj.Rol.Id : 0),
                    new XElement("EmpleadoId", (obj.Empleado != null) ? obj.Empleado.Id : 0)
                );

                // Agregar al documento
                var root = doc.Element("Usuarios");
                if (root == null)
                {
                    root = new XElement("Usuarios");
                    doc.Add(root);
                }
                root.Add(nuevoUsuario);

                doc.Save(ruta);
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de usuarios", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception(
                    "Error en la estructura del archivo XML de usuarios. " +
                    "Línea " + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
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
                if (!File.Exists(ruta))
                    throw new FileNotFoundException("No se encontró el archivo de usuarios.");

                var doc = XDocument.Load(ruta);

                // Buscar usuario por Id
                var usuario = doc.Descendants("Usuario")
                                 .FirstOrDefault(u =>
                                 {
                                     var attr = u.Attribute("Id");
                                     long idAttr;
                                     return attr != null && long.TryParse(attr.Value, out idAttr) && idAttr == obj.Id;
                                 });

                if (usuario != null)
                {
                    usuario.Remove();
                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de usuarios", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception(
                    "Error en la estructura del archivo XML de usuarios. " +
                    "Línea " + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario en XML", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void Modificar(BEUsuario obj)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                // Buscar usuario
                var usuario = doc.Descendants("Usuario")
                                 .FirstOrDefault(u =>
                                 {
                                     var attr = u.Attribute("Id");
                                     long idAttr;
                                     return attr != null && long.TryParse(attr.Value, out idAttr) && idAttr == obj.Id;
                                 });

                if (usuario != null)
                {
                    var eUsername = usuario.Element("Username");
                    var ePassword = usuario.Element("Password");
                    var eClave = usuario.Element("ClaveEncriptacion");
                    var eActivo = usuario.Element("Activo");
                    var eRolId = usuario.Element("RolId");
                    var eEmpId = usuario.Element("EmpleadoId");

                    if (eUsername == null) usuario.Add(eUsername = new XElement("Username"));
                    if (ePassword == null) usuario.Add(ePassword = new XElement("Password"));
                    if (eClave == null) usuario.Add(eClave = new XElement("ClaveEncriptacion"));
                    if (eActivo == null) usuario.Add(eActivo = new XElement("Activo"));
                    if (eRolId == null) usuario.Add(eRolId = new XElement("RolId"));
                    if (eEmpId == null) usuario.Add(eEmpId = new XElement("EmpleadoId"));

                    eUsername.Value = obj.Username ?? string.Empty;
                    ePassword.Value = obj.Password ?? string.Empty;
                    eClave.Value = obj.ClaveEncriptacion.ToString();
                    eActivo.Value = obj.Activo.ToString();
                    eRolId.Value = (obj.Rol != null ? obj.Rol.Id : 0).ToString();
                    eEmpId.Value = (obj.Empleado != null ? obj.Empleado.Id : 0).ToString();

                    doc.Save(ruta);
                }
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de usuarios", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception(
                    "Error en la estructura del archivo XML de usuarios. " +
                    "Línea " + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar usuario en XML", ex);
            }
        }

        #endregion

        #region "Listar"

        public List<BEUsuario> Listar()
        {
            try
            {
                var lista = new List<BEUsuario>();

                if (!File.Exists(ruta))
                    return lista;

                var doc = XDocument.Load(ruta);

                foreach (var xUsuario in doc.Descendants("Usuario"))
                {
                    var attr = xUsuario.Attribute("Id");
                    long id = 0;
                    if (attr != null) long.TryParse(attr.Value, out id);

                    var usuario = new BEUsuario
                    {
                        Id = id,
                        Username = ValStr(xUsuario, "Username"),
                        Password = ValStr(xUsuario, "Password"),
                        ClaveEncriptacion = ValByte(xUsuario, "ClaveEncriptacion", 0),
                        Activo = ValBool(xUsuario, "Activo", true),
                        Rol = new BERol { Id = ValLong(xUsuario, "RolId", 0) },
                        Empleado = new BEEmpleado { Id = ValLong(xUsuario, "EmpleadoId", 0) }
                    };

                    lista.Add(usuario);
                }

                return lista;
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de usuarios", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception(
                    "Error en la estructura del archivo XML de usuarios. " +
                    "Línea " + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar usuarios desde XML", ex);
            }
        }

        #endregion

        #region "BuscarPorId"

        public BEUsuario BuscarPorId(long id)
        {
            try
            {
                var doc = XDocument.Load(ruta);

                var xUsuario = doc.Descendants("Usuario")
                                  .FirstOrDefault(u =>
                                  {
                                      var attr = u.Attribute("Id");
                                      long idAttr;
                                      return attr != null && long.TryParse(attr.Value, out idAttr) && idAttr == id;
                                  });

                if (xUsuario == null)
                    return null;

                var attrId = xUsuario.Attribute("Id");
                long finalId = 0;
                if (attrId != null) long.TryParse(attrId.Value, out finalId);

                return new BEUsuario
                {
                    Id = finalId,
                    Username = ValStr(xUsuario, "Username"),
                    Password = ValStr(xUsuario, "Password"),
                    ClaveEncriptacion = ValByte(xUsuario, "ClaveEncriptacion", 0),
                    Activo = ValBool(xUsuario, "Activo", true),
                    Rol = new BERol { Id = ValLong(xUsuario, "RolId", 0) },
                    Empleado = new BEEmpleado { Id = ValLong(xUsuario, "EmpleadoId", 0) }
                };
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de usuarios", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception(
                    "Error en la estructura del archivo XML de usuarios. " +
                    "Línea " + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuario en XML", ex);
            }
        }

        #endregion

        #region "ObtenerUltimoId"

        public long ObtenerUltimoId()
        {
            try
            {
                if (!File.Exists(ruta))
                    return 0;

                var doc = XDocument.Load(ruta);
                var ids = doc.Descendants("Usuario")
                             .Select(u =>
                             {
                                 var a = u.Attribute("Id");
                                 long val;
                                 return (a != null && long.TryParse(a.Value, out val)) ? val : 0L;
                             });

                return ids.Any() ? ids.Max() : 0L;
            }
            catch
            {
                return 0L;
            }
        }

        #endregion
    }
}
