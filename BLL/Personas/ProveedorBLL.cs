using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE;
using BE.Transversal;
using BE.Enums;
using MPP.Personas;
using BLL.Transversal;

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

        // Valida y guarda (alta o modificación)
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

                if (proveedor.Id == 0)
                    proveedorMPP.Alta(proveedor);
                else
                    proveedorMPP.Modificar(proveedor);

                // Limpia cache de proveedores
                ReconstructorBLL.LimpiarCache<BEProveedor>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar proveedor.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        // Elimina proveedor
        public void Eliminar(BEProveedor proveedor)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(proveedor, "Proveedor");
                proveedorMPP.Baja(proveedor);

                // Limpia cache
                ReconstructorBLL.LimpiarCache<BEProveedor>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar proveedor.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        // Lista todos los proveedores y reconstruye direcciones
        public List<BEProveedor> ListarTodo()
        {
            try
            {
                var lista = proveedorMPP.Listar();

                foreach (var prov in lista)
                {
                    if (prov.Direccion != null && prov.Direccion.Id > 0)
                        prov.Direccion = ReconstructorBLL.Reconstruir<BEDireccion>(prov.Direccion.Id);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar proveedores.", ex);
            }
        }

        // Busca un proveedor por Id
        public BEProveedor ListarObjeto(BEProveedor proveedor)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(proveedor, "Proveedor");

                var encontrado = proveedorMPP.BuscarPorId(proveedor.Id);

                if (encontrado != null && encontrado.Direccion != null)
                    encontrado.Direccion = ReconstructorBLL.Reconstruir<BEDireccion>(encontrado.Direccion.Id);

                return encontrado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar proveedor.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        // Devuelve el último Id registrado
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
