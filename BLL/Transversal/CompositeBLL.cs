using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Abstraccion;
using BE.Transversal;
using MPP.Transversal;

namespace BLL.Transversal
{
    public class CompositeBLL
    {
        RolMPP rolMPP = new RolMPP();
        PermisoMPP permisoMPP = new PermisoMPP();
        CompositeMPP compositeMPP = new CompositeMPP();

        #region "Construcción de Jerarquía"

        // Reconstruye la jerarquía completa de Roles y Permisos desde los XML
        public List<BERol> ConstruirJerarquia()
        {
            try
            {
                var roles = rolMPP.Listar();
                var permisos = permisoMPP.Listar();

                var docRoles = roles ?? new List<BERol>();

                foreach (var rol in docRoles)
                {
                    var permisosRol = ObtenerPermisosPorRol(rol.Id);

                    if (permisosRol != null && permisosRol.Any())
                        rol.SubComponentes.AddRange(permisosRol);
                }

                return docRoles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al construir la jerarquía de roles y permisos", ex);
            }
        }

        #endregion

        #region "Asociaciones"

        // Asocia un permiso existente a un rol
        public void AsociarPermisoARol(BERol rol, BEPermiso permiso)
        {
            try
            {
                if (rol == null || permiso == null)
                    throw new ArgumentNullException("Rol o Permiso no pueden ser nulos");

                compositeMPP.AsociarRolPermiso(rol.Id, permiso.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asociar un permiso a rol", ex);
            }
        }

        // Desasocia un permiso de un rol
        public void DesasociarPermisoARol(BERol rol, BEPermiso permiso)
        {
            try
            {
                if (rol == null || permiso == null)
                    throw new ArgumentNullException("Rol o Permiso no pueden ser nulos");

                compositeMPP.DesasociarRolPermiso(rol.Id, permiso.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desasociar permiso de rol", ex);
            }
        }

        #endregion

        #region "Consultas"

        // Devuelve todos los permisos asociados a un rol
        public List<BEPermiso> ObtenerPermisosPorRol(long rolId)
        {
            try
            {
                var permisos = permisoMPP.Listar();
                if (permisos == null || !permisos.Any())
                    return new List<BEPermiso>();

                var doc = XDocument.Load(System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "Data", "Transversal", "Roles_Permisos.xml"));

                var idsAsociados = doc.Descendants("Relacion")
                    .Where(x => (long?)x.Attribute("RolId") == rolId)
                    .Select(x => (long)x.Attribute("PermisoId"))
                    .ToList();

                return permisos.Where(p => idsAsociados.Contains(p.Id)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener permisos por rol", ex);
            }
        }

        // Validación recursiva de permisos
        private bool BuscarPermisoRecursivo(IComponente componente, string nombrePermiso)
        {
            if (componente is BEPermiso permiso &&
                permiso.Nombre.Equals(nombrePermiso, StringComparison.OrdinalIgnoreCase))
                return true;

            foreach (var hijo in componente.ObtenerHijos())
            {
                if (BuscarPermisoRecursivo(hijo, nombrePermiso))
                    return true;
            }

            return false;
        }

        // Verifica si un rol tiene un permiso (recursivo)
        public bool TienePermiso(BERol rol, string nombrePermiso)
        {
            try
            {
                if (rol == null || string.IsNullOrEmpty(nombrePermiso))
                    return false;

                foreach (var componente in rol.SubComponentes)
                {
                    if (BuscarPermisoRecursivo(componente, nombrePermiso))
                        return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar permiso del rol", ex);
            }
        }

        #endregion

        #region "Listados"

        // Devuelve todos los roles activos
        public List<BERol> ListarRolesActivos()
        {
            try
            {
                return rolMPP.Listar().Where(r => r.Activo).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar roles activos", ex);
            }
        }

        // Devuelve todos los permisos activos
        public List<BEPermiso> ListarPermisosActivos()
        {
            try
            {
                return permisoMPP.Listar().Where(p => p.Activo).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar permisos activos", ex);
            }
        }

        #endregion
    }
}
