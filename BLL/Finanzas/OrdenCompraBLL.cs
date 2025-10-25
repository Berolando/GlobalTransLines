using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE.Enums;
using BE.Finanzas;
using BE;
using BLL.Transversal;
using MPP.Finanzas;

namespace BLL.Finanzas
{
    public class OrdenCompraBLL : IGestor<BEOrdenDeCompra>
    {
        private OrdenDeCompraMPP ordenMPP = new OrdenDeCompraMPP();

        #region "Constructor estático"

        static OrdenCompraBLL()
        {
            ReconstructorBLL.Registrar<BEOrdenDeCompra>(
                id => new OrdenCompraBLL().ListarObjeto(new BEOrdenDeCompra { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEOrdenDeCompra obj)
        {
            try
            {
                // Validaciones base (Estilo Bet v2)
                ValidadorBLL.ObjetoNoNulo(obj, "Orden de compra");
                ValidadorBLL.ObjetoNoNulo(obj.Proveedor, "Proveedor asociado");
                ValidadorBLL.ListaNoVacia(obj.Servicios, "Servicios subcontratados");

                if (obj.ImporteTotal <= 0)
                    throw new Exception("El importe total debe ser mayor a cero.");

                if (obj.FechaEmision > DateTime.Now)
                    throw new Exception("La fecha de emisión no puede ser futura.");

                // Coherencia mínima si trae booking
                if (obj.BookingOrigen != null && obj.BookingOrigen.Id == 0)
                    throw new Exception("El booking asociado debe estar registrado previamente.");

                // Persistencia
                if (obj.Id == 0)
                    ordenMPP.Alta(obj);
                else
                    ordenMPP.Modificar(obj);

                // Cache
                ReconstructorBLL.LimpiarCache<BEOrdenDeCompra>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar orden de compra.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEOrdenDeCompra obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Orden de compra");

                // No permitir eliminar si ya fue recibida o pagada
                if (obj.Estado == EstadoOrdenCompraEnum.Recibida || obj.Estado == EstadoOrdenCompraEnum.Pagada)
                    throw new Exception("No se puede eliminar una orden de compra recibida o pagada.");

                ordenMPP.Baja(obj);
                ReconstructorBLL.LimpiarCache<BEOrdenDeCompra>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar orden de compra.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEOrdenDeCompra> ListarTodo()
        {
            try
            {
                var lista = ordenMPP.Listar();

                foreach (var oc in lista)
                {
                    if (oc.Proveedor != null && oc.Proveedor.Id > 0)
                        oc.Proveedor = ReconstructorBLL.Reconstruir<BEProveedor>(oc.Proveedor.Id);

                    if (oc.BookingOrigen != null && oc.BookingOrigen.Id > 0)
                        oc.BookingOrigen = ReconstructorBLL.Reconstruir<BEBooking>(oc.BookingOrigen.Id);

                    if (oc.Servicios != null && oc.Servicios.Count > 0)
                    {
                        var ids = oc.Servicios.Select(s => s.Id).ToList();
                        oc.Servicios = ReconstructorBLL.ReconstruirLista<BEServicioSubcontratado>(ids);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar órdenes de compra.", ex);
            }
        }

        public BEOrdenDeCompra ListarObjeto(BEOrdenDeCompra obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Orden de compra");

                var orden = ordenMPP.BuscarPorId(obj.Id);

                if (orden != null)
                {
                    if (orden.Proveedor != null && orden.Proveedor.Id > 0)
                        orden.Proveedor = ReconstructorBLL.Reconstruir<BEProveedor>(orden.Proveedor.Id);

                    if (orden.BookingOrigen != null && orden.BookingOrigen.Id > 0)
                        orden.BookingOrigen = ReconstructorBLL.Reconstruir<BEBooking>(orden.BookingOrigen.Id);

                    if (orden.Servicios != null && orden.Servicios.Count > 0)
                    {
                        var ids = orden.Servicios.Select(s => s.Id).ToList();
                        orden.Servicios = ReconstructorBLL.ReconstruirLista<BEServicioSubcontratado>(ids);
                    }
                }

                return orden;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar orden de compra.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return ordenMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de orden de compra.", ex);
            }
        }

        #endregion
    }
}
