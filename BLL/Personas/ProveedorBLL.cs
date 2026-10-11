using Abstraccion;
using BE.Personas;
using BE.Infra;
using BLL.Infra;
using MPP.Personas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Personas
{
    public class ProveedorBLL : IGestor<BEProveedor>
    {
        private ProveedorMPP proveedorMPP = new ProveedorMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir un proveedor por Id
        static ProveedorBLL()
        {
            ReconstructorBLL.Registrar<BEProveedor>(
                id => new ProveedorBLL().ListarObjeto(new BEProveedor { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEProveedor proveedor)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(proveedor, "Proveedor");
                ValidadorBLL.CampoObligatorio(proveedor.Nombre, "Nombre");
                ValidadorBLL.CampoObligatorio(proveedor.RazonSocial, "Razón Social");
                ValidadorBLL.CampoObligatorio(proveedor.Cuit, "CUIT");

                var lista = proveedorMPP.Listar();
                bool duplicado = lista.Any(p =>
                    p.Cuit.Equals(proveedor.Cuit, StringComparison.OrdinalIgnoreCase)
                    && p.Id != proveedor.Id);

                if (duplicado)
                    throw new Exception("Ya existe un proveedor con el mismo CUIT.");

                // La dirección se guarda primero porque el proveedor persiste la relación por Id.
                if (proveedor.Direccion != null)
                {
                    var direccionBLL = new DireccionBLL();
                    direccionBLL.Guardar(proveedor.Direccion);
                }

                if (proveedor.Id == 0)
                    proveedorMPP.Alta(proveedor);
                else
                    proveedorMPP.Modificar(proveedor);

                // Se limpia la cache para que futuras consultas reconstruyan el proveedor actualizado.
                ReconstructorBLL.LimpiarCache<BEProveedor>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar proveedor.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEProveedor proveedor)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(proveedor, "Proveedor");
                proveedorMPP.Baja(proveedor);
                ReconstructorBLL.LimpiarCache<BEProveedor>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar proveedor.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEProveedor> ListarTodo()
        {
            try
            {
                var lista = proveedorMPP.Listar();

                foreach (var prov in lista)
                {
                    if (prov.Direccion != null && prov.Direccion.Id > 0)
                    {
                        // En XML se guarda la dirección por Id; la BLL reconstruye el proveedor completo.
                        var dir = ReconstructorBLL.Reconstruir<BEDireccion>(prov.Direccion.Id);
                        if (dir != null)
                            prov.Direccion = dir;
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar proveedores.", ex);
            }
        }

        public List<BEProveedor> ListarActivos()
        {
            try
            {
                var lista = proveedorMPP.Listar();

                foreach (var prov in lista)
                {
                    if (prov.Direccion != null && prov.Direccion.Id > 0)
                    {
                        // En XML se guarda la dirección por Id; la BLL reconstruye el proveedor completo.
                        var dir = ReconstructorBLL.Reconstruir<BEDireccion>(prov.Direccion.Id);
                        if (dir != null)
                            prov.Direccion = dir;
                    }
                }

                return lista.Where(p => p.Activo).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar proveedores activos.", ex);
            }
        }

        public BEProveedor ListarObjeto(BEProveedor proveedor)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(proveedor, "Proveedor");

                var encontrado = proveedorMPP.BuscarPorId(proveedor.Id);

                if (encontrado != null && encontrado.Direccion != null)
                {
                    // Se reconstruye la dirección para devolver el proveedor completo.
                    var dir = ReconstructorBLL.Reconstruir<BEDireccion>(encontrado.Direccion.Id);
                    if (dir != null)
                        encontrado.Direccion = dir;
                }

                return encontrado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar proveedor.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return proveedorMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de proveedor.", ex);
            }
        }

        #endregion
    }
}
