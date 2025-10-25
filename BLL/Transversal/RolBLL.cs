using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE.Transversal;
using MPP.Transversal;

namespace BLL.Transversal
{
    public class RolBLL : IGestor<BERol>
    {
        private RolMPP rolMPP = new RolMPP();

        #region "Guardar"

        // Guarda o modifica un rol según su Id
        public void Guardar(BERol rol)
        {
            try
            {
                // Validaciones generales antes de persistir
                ValidadorBLL.ObjetoNoNulo(rol, "Rol");
                ValidadorBLL.CampoObligatorio(rol.Nombre, "Nombre del rol");
                ValidadorBLL.CampoObligatorio(rol.Descripcion, "Descripción del rol");

                // Se obtiene la lista actual para verificar duplicados
                var listaRoles = rolMPP.Listar();

                bool duplicado = listaRoles.Any(r =>
                    r.Nombre.Equals(rol.Nombre, StringComparison.OrdinalIgnoreCase)
                    && r.Id != rol.Id);

                if (duplicado)
                    throw new Exception("Ya existe un rol con el mismo nombre.");

                // Inserta o modifica según el Id
                if (rol.Id == 0)
                    rolMPP.Alta(rol);
                else
                    rolMPP.Modificar(rol);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el rol.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        // Elimina un rol solo si no está asociado a ningún usuario
        public void Eliminar(BERol rol)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(rol, "Rol");

                // Verificar si el rol está asignado a algún usuario
                var ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Transversal", "Usuarios.xml");
                var doc = System.Xml.Linq.XDocument.Load(ruta);

                bool asociado = doc.Descendants("Usuario")
                    .Any(x => (long?)x.Element("RolId") == rol.Id);

                if (asociado)
                    throw new Exception("No se puede eliminar el rol porque está asociado a un usuario.");

                rolMPP.Baja(rol);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el rol.", ex);
            }
        }

        #endregion

        #region "Listar"

        // Devuelve todos los roles existentes
        // Si el XML está vacío, genera automáticamente el rol Administrador con todos los permisos base.
        public List<BERol> ListarTodo()
        {
            try
            {
                var lista = rolMPP.Listar();

                // 🧩 Si no hay roles cargados, se crea el Administrador
                if (lista == null || lista.Count == 0)
                {
                    // Recupera la lista de permisos base
                    var permisos = new PermisoBLL().ListarTodo();

                    BERol rolAdmin = new BERol
                    {
                        Id = 1,
                        Nombre = "Administrador",
                        Descripcion = "Rol con acceso total al sistema",
                        Activo = true,
                        SubComponentes = new List<IComponente>(permisos)
                    };

                    rolMPP.Alta(rolAdmin);
                    lista = new List<BERol> { rolAdmin };
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar roles.", ex);
            }
        }

        // Devuelve un rol específico según Id
        public BERol ListarObjeto(BERol rol)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(rol, "Rol");
                return rolMPP.BuscarPorId(rol.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar rol.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        // Devuelve el último Id registrado
        public long ObtenerUltimoId()
        {
            try
            {
                return rolMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último Id de rol.", ex);
            }
        }

        #endregion

        #region "Inicialización Base"

        // 🧠 Inicializa el rol Administrador si no existe
        public void InicializarRolAdministrador()
        {
            try
            {
                var lista = rolMPP.Listar();
                if (lista.Any(r => r.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase)))
                    return; // Ya existe

                var permisos = new PermisoBLL().ListarTodo();

                BERol rolAdmin = new BERol
                {
                    Id = 1,
                    Nombre = "Administrador",
                    Descripcion = "Rol con acceso total al sistema",
                    Activo = true,
                    SubComponentes = new List<IComponente>(permisos)
                };

                rolMPP.Alta(rolAdmin);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el rol Administrador.", ex);
            }
        }

        #endregion
    }
}
