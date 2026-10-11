using Abstraccion;
using BE.Dominio;
using BE.Finanzas;
using BE.Personas;
using BE.ServiciosSubcontratados;
using BE.Transporte;
using BLL.Infra;
using MPP.Finanzas;
using System;
using System.Collections.Generic;
using System.Linq;

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
                // La orden debe quedar asociada a un proveedor y a servicios concretos antes de emitirse.
                ValidadorBLL.ObjetoNoNulo(obj, "Orden de compra");
                ValidadorBLL.ObjetoNoNulo(obj.Proveedor, "Proveedor asociado");
                ValidadorBLL.ListaNoVacia(obj.Servicios, "Servicios subcontratados");

                if (obj.ImporteTotal <= 0)
                    throw new Exception("El importe total debe ser mayor a cero.");

                if (obj.FechaEmision > DateTime.Now)
                    throw new Exception("La fecha de emisión no puede ser futura.");

                // Si viene de un booking, la relación debe apuntar a un booking ya persistido.
                if (obj.BookingOrigen != null && obj.BookingOrigen.Id == 0)
                    throw new Exception("El booking asociado debe estar registrado previamente.");

                if (obj.Id == 0)
                    ordenMPP.Alta(obj);
                else
                    ordenMPP.Modificar(obj);

                // El estado de la OC impacta en recepción, pagos y dashboard.
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

                // La OC no se elimina si ya avanzó en el circuito de recepción o pago.
                if (obj.Estado == EstadoOrdenCompra.Recibida || obj.Estado == EstadoOrdenCompra.Pagada)
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

        #region "Estados"

        public List<BEOrdenDeCompra> ListarEmitidas()
        {
            try
            {
                return ListarTodo()
                    .Where(oc => oc.Estado != null &&
                                 oc.Estado.Codigo == EstadoOrdenCompra.Emitida.Codigo)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar órdenes de compra emitidas.", ex);
            }
        }

        public void MarcarRecibida(BEOrdenDeCompra obj)
        {
            CambiarEstado(obj, EstadoOrdenCompra.Recibida);
        }

        public void MarcarPagada(BEOrdenDeCompra obj)
        {
            CambiarEstado(obj, EstadoOrdenCompra.Pagada);
        }

        private void CambiarEstado(BEOrdenDeCompra obj, EstadoOrdenCompra nuevoEstado)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Orden de compra");
                ValidadorBLL.ObjetoNoNulo(nuevoEstado, "Estado de orden de compra");

                var orden = ordenMPP.BuscarPorId(obj.Id);
                if (orden == null)
                    throw new Exception("No se encontró la orden de compra especificada.");

                // El cambio de estado se hace desde la BLL para mantener ordenado el flujo EMI -> REC -> PAG.
                orden.Estado = nuevoEstado;
                ordenMPP.Modificar(orden);

                ReconstructorBLL.LimpiarCache<BEOrdenDeCompra>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el estado de la orden de compra.", ex);
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
