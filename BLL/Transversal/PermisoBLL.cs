using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE.Transversal;
using MPP.Transversal;

namespace BLL.Transversal
{
    public class PermisoBLL : IGestor<BEPermiso>
    {
        private PermisoMPP permisoMPP = new PermisoMPP();

        #region "Guardar"

        // Guarda o modifica un permiso según su Id
        public void Guardar(BEPermiso permiso)
        {
            try
            {
                // Validaciones básicas antes de persistir
                ValidadorBLL.ObjetoNoNulo(permiso, "Permiso");
                ValidadorBLL.CampoObligatorio(permiso.Nombre, "Nombre del permiso");
                ValidadorBLL.CampoObligatorio(permiso.Descripcion, "Descripción del permiso");

                var listaPermisos = permisoMPP.Listar();

                // Verifica duplicados por nombre (ignora el mismo Id en modificación)
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
                throw new Exception("Error al guardar el permiso.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        // Elimina un permiso solo si no está asociado a ningún rol
        public void Eliminar(BEPermiso permiso)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(permiso, "Permiso");

                var ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Transversal", "Roles_Permisos.xml");
                var doc = System.Xml.Linq.XDocument.Load(ruta);

                bool asociado = doc.Descendants("Relacion")
                    .Any(x => (long?)x.Attribute("PermisoId") == permiso.Id);

                if (asociado)
                    throw new Exception("No se puede eliminar el permiso porque está asociado a un rol.");

                permisoMPP.Baja(permiso);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el permiso.", ex);
            }
        }

        #endregion

        #region "Listar"

        // Devuelve todos los permisos existentes
        // Si el XML está vacío, genera automáticamente los permisos base del sistema.
        public List<BEPermiso> ListarTodo()
        {
            try
            {
                var lista = permisoMPP.Listar();

                // 🧩 Si no hay permisos cargados, se crean los permisos base
                if (lista == null || lista.Count == 0)
                {
                    lista = InicializarPermisosBase();

                    foreach (var permiso in lista)
                        permisoMPP.Alta(permiso);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar permisos.", ex);
            }
        }

        // Devuelve un permiso específico según Id
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

        // Devuelve el último Id registrado
        public long ObtenerUltimoId()
        {
            try
            {
                return permisoMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último Id de permiso.", ex);
            }
        }

        #endregion

        #region "Inicialización Base"

        // 🧠 Crea automáticamente la lista de permisos base si el XML está vacío
        private List<BEPermiso> InicializarPermisosBase()
        {
            return new List<BEPermiso>
            {
                new BEPermiso { Id = 1, Nombre = "FrmCliente", Descripcion = "Gestión de clientes" },
                new BEPermiso { Id = 2, Nombre = "FrmCarga", Descripcion = "Gestión de cargas" },
                new BEPermiso { Id = 3, Nombre = "FrmCotizacion", Descripcion = "Gestión de cotizaciones" },
                new BEPermiso { Id = 4, Nombre = "FrmBooking", Descripcion = "Gestión de reservas" },
                new BEPermiso { Id = 5, Nombre = "FrmFactura", Descripcion = "Facturación" },
                new BEPermiso { Id = 6, Nombre = "FrmPago", Descripcion = "Gestión de pagos" },
                new BEPermiso { Id = 7, Nombre = "FrmProveedor", Descripcion = "Gestión de proveedores" },
                new BEPermiso { Id = 8, Nombre = "FrmServicio", Descripcion = "Gestión de servicios subcontratados" },
                new BEPermiso { Id = 9, Nombre = "FrmOrdenCompra", Descripcion = "Órdenes de compra" },
                new BEPermiso { Id = 10, Nombre = "FrmRecepcionServicio", Descripcion = "Recepción de servicios" },
                new BEPermiso { Id = 11, Nombre = "FrmPagoProveedor", Descripcion = "Pagos a proveedores" },
                new BEPermiso { Id = 12, Nombre = "FrmUsuario", Descripcion = "Gestión de usuarios" },
                new BEPermiso { Id = 13, Nombre = "FrmEmpleado", Descripcion = "Gestión de empleados" },
                new BEPermiso { Id = 14, Nombre = "FrmRolPermiso", Descripcion = "Administración de roles y permisos" },
                new BEPermiso { Id = 15, Nombre = "FrmBackup", Descripcion = "Módulo de respaldo y restauración" },
                new BEPermiso { Id = 16, Nombre = "FrmBitacora", Descripcion = "Bitácora del sistema" },
                new BEPermiso { Id = 17, Nombre = "FrmDashboard", Descripcion = "Dashboard general" },
                new BEPermiso { Id = 18, Nombre = "FrmReportes", Descripcion = "Reportes por gestión" }
            };
        }

        #endregion
    }
}
