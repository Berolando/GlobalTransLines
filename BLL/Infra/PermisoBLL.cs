using Abstraccion;
using BE.Dominio;
using BE.Infra;
using MPP.Infra;
using Transversales;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Infra
{
    public class PermisoBLL 
    {
        private PermisoMPP permisoMPP = new PermisoMPP();
        private CompositeMPP compositeMPP = new CompositeMPP();

        static PermisoBLL() 
        {
            // Registra como reconstruir un permiso completo cuando otra entidad solo tiene su Id.
            ReconstructorBLL.Registrar<BEPermiso>(
                id => new PermisoBLL().ListarObjeto(new BEPermiso { Id = id })
            );
        }

        #region "Guardar"

        // Guarda o modifica un permiso segun su Id
        public void Guardar(BEPermiso permiso)
        {
            try
            {
                // Validaciones basicas antes de persistir
                ValidadorBLL.ObjetoNoNulo(permiso, "Permiso");
                ValidadorBLL.CampoObligatorio(permiso.Nombre, "Nombre del permiso");
                ValidadorBLL.CampoObligatorio(permiso.Descripcion, "Descripcion del permiso");

                var listaPermisos = permisoMPP.Listar();

                // Verifica duplicados por nombre (ignora el mismo Id en modificacion)
                bool duplicado = listaPermisos.Any(p =>
                    p.Nombre.Equals(permiso.Nombre, StringComparison.OrdinalIgnoreCase)
                    && p.Id != permiso.Id);

                if (duplicado)
                    throw new Exception("Ya existe un permiso con el mismo nombre.");

                if (permiso.Id == 0)
                    permisoMPP.Alta(permiso);
                else
                    permisoMPP.Modificar(permiso);
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEPermiso permiso)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(permiso, "Permiso");

                if (PermisoEstaAsignadoARol(permiso.Id))
                    throw new Exception("No se puede eliminar el permiso porque esta asignado a un rol.");

                // Baja logica: el permiso queda en XML, pero marcado como inactivo.
                permisoMPP.Baja(permiso);

                // Limpia la cache para que futuras reconstrucciones tomen el estado actualizado.
                ReconstructorBLL.LimpiarCache<BEPermiso>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el permiso.", ex);
            }
        }

        private bool PermisoEstaAsignadoARol(long permisoId)
        {
            return compositeMPP.ListarRelaciones()
                .Any(r => r.PermisoId.HasValue && r.PermisoId.Value == permisoId);
        }

        #endregion

        #region "Listar"

        // Devuelve todos los permisos existentes

        public List<BEPermiso> ListarTodo()
        {
            try
            {
                // Para la UI se devuelven solo permisos activos.
                return permisoMPP.Listar()
                                 .Where(p => p.Activo)
                                 .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar permisos.", ex);
            }
        }


        // Devuelve un permiso especifico segun Id
        public BEPermiso ListarObjeto(BEPermiso permiso)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(permiso, "Permiso");
                return permisoMPP.BuscarPorId(permiso.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar permiso.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        // Devuelve el ultimo Id registrado
        public long ObtenerUltimoId()
        {
            try
            {
                return permisoMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el ultimo Id de permiso.", ex);
            }
        }

        #endregion

        #region "Inicializacion Base"

        // Sincroniza el catalogo de permisos con las pantallas vigentes del sistema.
        public void InicializarPermisos()
        {
            try
            {
                var listaPermisos = permisoMPP.Listar();
                var permisosBase = InicializarPermisosBase();
                var nombresBase = new HashSet<string>(
                    permisosBase.Select(p => p.Nombre),
                    StringComparer.OrdinalIgnoreCase);

                foreach (var permiso in permisosBase)
                {
                    var permisoExistente = listaPermisos.FirstOrDefault(p =>
                        p.Nombre.Equals(permiso.Nombre, StringComparison.OrdinalIgnoreCase));

                    if (permisoExistente == null)
                    {
                        Guardar(permiso);
                    }
                    else if (!permisoExistente.Activo || permisoExistente.Descripcion != permiso.Descripcion)
                    {
                        permisoExistente.Descripcion = permiso.Descripcion;
                        permisoExistente.Activo = true;
                        permisoMPP.Modificar(permisoExistente);
                    }
                }

                var permisosFueraDeCatalogo = listaPermisos
                    .Where(p => !nombresBase.Contains(p.Nombre))
                    .ToList();

                foreach (var permisoFueraDeCatalogo in permisosFueraDeCatalogo.Where(p => p.Activo))
                {
                    permisoFueraDeCatalogo.Activo = false;
                    permisoMPP.Modificar(permisoFueraDeCatalogo);
                }

                compositeMPP.QuitarRelacionesDePermisos(permisosFueraDeCatalogo.Select(p => p.Id));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar permisos base.", ex);
            }
        }

        // Catalogo inicial de permisos: cada nombre coincide con un formulario del sistema.
        private List<BEPermiso> InicializarPermisosBase()
        {
            return new List<BEPermiso>
            {
                // GESTION 1: Comercial y Ventas
                new BEPermiso { Nombre = "FrmCliente", Descripcion = "Gestion de clientes", Activo = true },
                new BEPermiso { Nombre = "FrmCarga", Descripcion = "Gestion de cargas", Activo = true },
                new BEPermiso { Nombre = "FrmCotizacion", Descripcion = "Gestion de cotizaciones", Activo = true },
                new BEPermiso { Nombre = "FrmCotizaciones", Descripcion = "Listado de cotizaciones", Activo = true },
                new BEPermiso { Nombre = "FrmTarifario", Descripcion = "Administracion de tarifario", Activo = true },
                new BEPermiso { Nombre = "FrmBooking", Descripcion = "Gestion de reservas", Activo = true },

                // GESTION 2: Planificacion Naviera
                new BEPermiso { Nombre = "FrmAsignarBuqueContenedor", Descripcion = "Asignacion de buques y contenedores", Activo = true },
                new BEPermiso { Nombre = "FrmBillOfLading", Descripcion = "Gestion de Bill of Lading", Activo = true },
                new BEPermiso { Nombre = "FrmManifiesto", Descripcion = "Gestion de manifiestos de carga", Activo = true },
                new BEPermiso { Nombre = "FrmContenedor", Descripcion = "Administracion de contenedores", Activo = true },
                new BEPermiso { Nombre = "FrmBuque", Descripcion = "Administracion de buques", Activo = true },

                // GESTION 3: Facturacion y Cobranzas
                new BEPermiso { Nombre = "FrmFactura", Descripcion = "Facturacion", Activo = true },
                new BEPermiso { Nombre = "FrmPago", Descripcion = "Gestion de pagos", Activo = true },

                // GESTION 4: Compras y Proveedores
                new BEPermiso { Nombre = "FrmProveedor", Descripcion = "Gestion de proveedores", Activo = true },
                new BEPermiso { Nombre = "FrmServicioSubcontratado", Descripcion = "Gestion de servicios subcontratados", Activo = true },
                new BEPermiso { Nombre = "FrmOrdenCompra", Descripcion = "Ordenes de compra", Activo = true },
                new BEPermiso { Nombre = "FrmRecepcionServicio", Descripcion = "Recepcion de servicios", Activo = true },
                new BEPermiso { Nombre = "FrmPagoProveedor", Descripcion = "Pagos a proveedores", Activo = true },

                // ADMINISTRACION Y SEGURIDAD
                new BEPermiso { Nombre = "FrmUsuario", Descripcion = "Gestion de usuarios", Activo = true },
                new BEPermiso { Nombre = "FrmEmpleado", Descripcion = "Gestion de empleados", Activo = true },
                new BEPermiso { Nombre = "FrmRolesYPermisos", Descripcion = "Administracion de roles y permisos", Activo = true },
                new BEPermiso { Nombre = "FrmBackup", Descripcion = "Modulo de respaldo y restauracion", Activo = true },
                new BEPermiso { Nombre = "FrmBitacora", Descripcion = "Bitacora del sistema", Activo = true },

                // DASHBOARD Y REPORTES
                new BEPermiso { Nombre = "FrmDashboard", Descripcion = "Dashboard general", Activo = true },
                new BEPermiso { Nombre = "FrmReportes", Descripcion = "Reportes por gestion", Activo = true }
            };
        }




        #endregion
    }
}
