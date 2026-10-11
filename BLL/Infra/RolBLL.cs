using Abstraccion;
using BE.Infra;
using Transversales;
using System;
using System.Collections.Generic;
using System.Linq;
using MPP.Infra;

namespace BLL.Infra
{
    public class RolBLL 
    {
        private RolMPP rolMPP = new RolMPP();
        private UsuarioMPP usuarioMPP = new UsuarioMPP();
        private CompositeMPP compositeMPP = new CompositeMPP();

        static RolBLL()
        {
           ReconstructorBLL.Registrar<BERol>(
                id => new RolBLL().ListarObjeto(new BERol { Id = id })
            );
        }

        #region "Guardar"

        // Guarda o modifica un rol según su Id
        public void Guardar(BERol rol)
        {
            try
            {
                // Validaciones generales antes de persistir
                ValidadorBLL.ObjetoNoNulo(rol, "Rol");
                ValidadorBLL.CampoObligatorio(rol.Nombre, "Nombre del rol");

                // El rol Administrador nunca puede quedar inactivo
                if (rol.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase) && !rol.Activo)
                {
                    throw new Exception("El rol Administrador no puede desactivarse.");
                }
                
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
                
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region "Eliminar"

        // Realiza la baja lógica del rol solo si no está en uso.
        public void Eliminar(BERol rol)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(rol, "Rol");

                var rolPersistido = rolMPP.BuscarPorId(rol.Id);
                if (rolPersistido == null)
                    throw new Exception("Rol no encontrado.");

                // El rol Administrador no puede eliminarse ni desactivarse.
                if (rolPersistido.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("No se puede eliminar este registro.");
                }

                ValidarRolSinUso(rolPersistido);

                // Baja lógica: desactiva el rol.
                rolMPP.Baja(rolPersistido);

                // Limpia cache por si el rol estaba reconstruido.
                ReconstructorBLL.LimpiarCache<BERol>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private void ValidarRolSinUso(BERol rol)
        {
            long rolId = rol.Id;

            // Regla de negocio: no se puede dar de baja un rol asignado a usuarios activos.
            bool asignadoAUsuario = usuarioMPP.Listar().Any(u =>
                u != null &&
                u.Activo &&
                u.Roles != null &&
                u.Roles.Any(r => r != null && r.Id == rolId));

            if (asignadoAUsuario)
                throw new Exception("No se puede eliminar el rol porque está asignado a usuarios activos.");

            var relaciones = compositeMPP.ListarRelaciones();

            // Regla de negocio: no se puede dar de baja un rol que participa en la jerarquía de roles.
            bool usadoEnJerarquia = relaciones.Any(r =>
                r.RolId == rolId ||
                (r.SubRolId.HasValue && r.SubRolId.Value == rolId));

            if (usadoEnJerarquia)
                throw new Exception("No se puede eliminar el rol porque está asociado a permisos o subroles.");
        }


        #endregion

        #region "Listar"

        // Devuelve solo los roles activos para mostrar en la UI.
        public List<BERol> ListarTodo()
        {
            try
            {
                return rolMPP.Listar()
                             .Where(r => r.Activo)
                             .ToList();
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

        //  Inicializa el rol Administrador si no existe
        public void InicializarRolAdministrador()
        {
            try
            {
                var lista = rolMPP.Listar();

                var rolAdminExistente = lista.FirstOrDefault(r =>
                    r.Nombre.Equals("Administrador", StringComparison.OrdinalIgnoreCase));

                // Si no existe, lo crea
                if (rolAdminExistente == null)
                {
                    BERol rolAdmin = new BERol
                    {
                        Nombre = "Administrador",
                        Descripcion = "Rol con acceso total al sistema",
                        Activo = true
                    };

                    rolMPP.Alta(rolAdmin);
                    return;
                }

                // Si existe pero está inactivo, lo reactiva
                if (!rolAdminExistente.Activo)
                {
                    rolAdminExistente.Activo = true;
                    rolMPP.Modificar(rolAdminExistente);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el rol Administrador.", ex);
            }
        }


        #endregion
    }
}
