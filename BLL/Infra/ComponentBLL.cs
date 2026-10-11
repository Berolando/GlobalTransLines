using System;
using System.Collections.Generic;
using System.Linq;
using BE.Infra;
using Transversales;
using MPP.Infra;

namespace BLL.Infra
{
    public class ComponentBLL
    {
        private readonly RolMPP rolMPP = new RolMPP();
        private readonly CompositeMPP compositeMPP = new CompositeMPP();

        #region "Construcción de la jerarquía completa"

        // Reconstruye en memoria el árbol de seguridad con roles, subroles y permisos desde los XML.
        public List<BERol> ConstruirJerarquia()
        {
            try
            {
                // Se parte de los roles activos persistidos en XML.
                var roles = (rolMPP.Listar() ?? new List<BERol>()).Where(r => r != null && r.Activo).ToList();

                // Las relaciones rol-permiso y rol-subrol se leen desde el XML del composite.
                var relaciones = compositeMPP.ListarRelaciones();

                // Limpia hijos antes de reconstruir para que llamar varias veces no duplique subcomponentes.
                foreach (var rol in roles)
                {
                    rol.SubComponentes.Clear();
                }

                // Todos los subárboles se construyen con el mismo listado para mantener una jerarquía consistente.
                foreach (var rol in roles)
                {
                    ConstruirJerarquiaRecursivo(rol, roles, relaciones);
                }

                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al construir la jerarquía de roles y permisos.", ex);
            }
        }

        // Construye recursivamente los permisos y subroles de un rol dado.
        private void ConstruirJerarquiaRecursivo(
            BERol rol,
            List<BERol> roles,
            List<(long RolId, long? PermisoId, long? SubRolId, string Tipo)> relaciones)
        {
            if (rol == null) return;

            // Permisos directos del rol.
            var idsPermisos = relaciones
                .Where(r => r.RolId == rol.Id && r.PermisoId.HasValue)
                .Select(r => r.PermisoId.Value)
                .Distinct()
                .ToList();

            foreach (var idPermiso in idsPermisos)
            {
                // Los permisos son hojas del Composite, por eso se reconstruyen como BEPermiso.
                var permiso = ReconstructorBLL.Reconstruir<BEPermiso>(idPermiso);

                if (permiso != null && permiso.Activo && !rol.SubComponentes.OfType<BEPermiso>().Any(p => p.Id == idPermiso))
                {
                    rol.SubComponentes.Add(permiso);
                }
            }

            // Subroles heredados por el rol.
            var idsSubRoles = relaciones
                .Where(r => r.RolId == rol.Id && r.SubRolId.HasValue)
                .Select(r => r.SubRolId.Value)
                .Distinct()
                .ToList();

            foreach (var idSub in idsSubRoles)
            {
                // Evita una relación directa del rol consigo mismo.
                if (idSub == rol.Id)
                    continue;

                
                // Los subroles se toman del mismo listado para mantener una única instancia por rol en la jerarquía.
                var subRol = roles.FirstOrDefault(r => r.Id == idSub);
                if (subRol == null)
                    continue;

                // Evita duplicados directos dentro del mismo rol.
                if (rol.SubComponentes.OfType<BERol>().Any(r => r.Id == idSub))
                    continue;

                // Evita ciclos al reconstruir, por ejemplo Rol A contiene Rol B y Rol B contiene Rol A.
                if (YaExisteEnJerarquia(rol, subRol))
                    continue;

                rol.SubComponentes.Add(subRol);

                // Continúa la reconstrucción para incluir permisos heredados en profundidad.
                ConstruirJerarquiaRecursivo(subRol, roles, relaciones);
            }
        }

        // Verifica si rolPadre ya aparece por debajo de subRol para evitar ciclos.
        private bool YaExisteEnJerarquia(BERol rolPadre, BEComponent subRol)
        {
            if (subRol == null || subRol.SubComponentes == null)
                return false;

            foreach (var comp in subRol.SubComponentes)
            {
                if (comp is BERol r)
                {
                    if (r.Id == rolPadre.Id)
                        return true;

                    if (YaExisteEnJerarquia(rolPadre, r))
                        return true;
                }
            }

            return false;
        }

        #endregion

        #region "Asociaciones Rol–Permiso y Rol–Rol"

        public void AsociarPermiso(long rolId, long permisoId)
        {
            if (rolId <= 0 || permisoId <= 0)
                throw new Exception("IDs inválidos para asociar permiso.");

            try
            {
                compositeMPP.AsociarRolPermiso(rolId, permisoId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asociar permiso al rol.", ex);
            }
        }

        public void DesasociarPermiso(long rolId, long permisoId)
        {
            if (rolId <= 0 || permisoId <= 0)
                throw new Exception("IDs inválidos para desasociar permiso.");

            try
            {
                compositeMPP.DesasociarRolPermiso(rolId, permisoId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desasociar permiso del rol.", ex);
            }
        }

        public void AsociarRolPermiso(long rolId, long permisoId)
        {
            AsociarPermiso(rolId, permisoId);
        }

        public void QuitarRolPermiso(long rolId, long permisoId)
        {
            DesasociarPermiso(rolId, permisoId);
        }

        public void AsociarSubRol(long rolPadreId, long subRolId)
        {
            if (rolPadreId <= 0 || subRolId <= 0)
                throw new Exception("IDs inválidos para asociar subrol.");

            if (rolPadreId == subRolId)
                throw new Exception("Un rol no puede asociarse como subrol de sí mismo.");

            try
            {
                // Antes de guardar la relación, valida que no genere una jerarquía circular.
                var roles = ConstruirJerarquia();

                var rolPadre = roles.FirstOrDefault(r => r.Id == rolPadreId);
                var subRol = roles.FirstOrDefault(r => r.Id == subRolId);

                if (rolPadre == null)
                    throw new Exception("No se encontró el rol padre.");

                if (subRol == null)
                    throw new Exception("No se encontró el subrol.");

                if (TieneSubRol(subRol, rolPadreId))
                    throw new Exception("No se puede asociar el subrol porque generaría una jerarquía circular.");

                compositeMPP.AsociarRolRol(rolPadreId, subRolId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asociar subrol.", ex);
            }
        }

        private bool TieneSubRol(BERol rol, long subRolIdBuscado)
        {
            if (rol == null || rol.SubComponentes == null)
                return false;

            // Busca recursivamente si un rol ya contiene a otro dentro de su árbol.
            foreach (var subRol in rol.SubComponentes.OfType<BERol>())
            {
                if (subRol.Id == subRolIdBuscado)
                    return true;

                if (TieneSubRol(subRol, subRolIdBuscado))
                    return true;
            }

            return false;
        }

        public void QuitarSubRol(long rolPadreId, long subRolId)
        {
            if (rolPadreId <= 0 || subRolId <= 0)
                throw new Exception("IDs inválidos para desasociar subrol.");

            try
            {
                compositeMPP.DesasociarRolRol(rolPadreId, subRolId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desasociar subrol.", ex);
            }
        }

        #endregion

        #region "Validación de permisos"

        // Recorre permisos directos e indirectos heredados por subroles.
        public bool TienePermiso(BEComponent nodo, string nombrePermiso)
        {
            if (nodo == null || string.IsNullOrWhiteSpace(nombrePermiso))
                return false;

            if (nodo.SubComponentes == null)
                return false;

            foreach (var hijo in nodo.SubComponentes)
            {
                if (hijo is BEPermiso permiso &&
                    permiso.Nombre.Equals(nombrePermiso, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (hijo is BERol subRol)
                {
                    if (TienePermiso(subRol, nombrePermiso))
                        return true;
                }
            }

            return false;
        }

        #endregion

        #region "Asignar permisos base al rol Administrador"

        public void AsignarPermisosBaseAlAdministrador()
        {
            try
            {
                // Garantiza que el rol administrador tenga acceso a todos los permisos activos del sistema.
                var permisoBLL = new PermisoBLL();
                var rolBLL = new RolBLL();

                var permisos = permisoBLL.ListarTodo();
                var roles = rolBLL.ListarTodo();

                var rolAdmin = roles.FirstOrDefault(r =>
                    r.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase));


                if (rolAdmin == null)
                    throw new Exception("No se encontró el rol Administrador para asignar permisos base.");

                if (permisos == null || permisos.Count == 0)
                    throw new Exception("No existen permisos base activos para asignar al rol Administrador.");


                foreach (var p in permisos)
                {
                    compositeMPP.AsociarRolPermiso(rolAdmin.Id, p.Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al asignar permisos base al rol Administrador.", ex);
            }
        }

        #endregion
    }
}
