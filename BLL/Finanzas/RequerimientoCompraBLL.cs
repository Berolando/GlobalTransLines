using Abstraccion;
using BE.Dominio;
using BE.Finanzas;
using BE.ServiciosSubcontratados;
using BE.Transporte;
using BLL.Infra;
using MPP.Finanzas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Finanzas
{
    public class RequerimientoCompraBLL : IGestor<BERequerimientoCompra>
    {
        private RequerimientoCompraMPP requerimientoMPP = new RequerimientoCompraMPP();

        #region "Constructor estatico"

        static RequerimientoCompraBLL()
        {
            ReconstructorBLL.Registrar<BERequerimientoCompra>(
                id => new RequerimientoCompraBLL().ListarObjeto(new BERequerimientoCompra { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BERequerimientoCompra obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Requerimiento de compra");
                ValidadorBLL.ObjetoNoNulo(obj.BookingOrigen, "Booking asociado");
                ValidadorBLL.ObjetoNoNulo(obj.Servicio, "Servicio subcontratado");

                if (obj.BookingOrigen.Id <= 0)
                    throw new Exception("El booking asociado debe estar registrado previamente.");

                if (obj.Servicio.Id <= 0)
                    throw new Exception("El servicio asociado debe estar registrado previamente.");

                if (obj.FechaSolicitud == DateTime.MinValue)
                    obj.FechaSolicitud = DateTime.Now;

                if (obj.FechaSolicitud > DateTime.Now)
                    throw new Exception("La fecha de solicitud no puede ser futura.");

                if (obj.Estado == null)
                    obj.Estado = EstadoRequerimientoCompra.Pendiente;

                // Evita crear dos requerimientos activos para el mismo Booking y servicio.
                if (obj.Id == 0 && ExisteRequerimientoActivo(obj.BookingOrigen.Id, obj.Servicio.Id))
                    throw new Exception("Ya existe un requerimiento de compra para ese booking y servicio.");

                if (obj.Id == 0)
                    requerimientoMPP.Alta(obj);
                else
                    requerimientoMPP.Modificar(obj);

                ReconstructorBLL.LimpiarCache<BERequerimientoCompra>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar requerimiento de compra.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BERequerimientoCompra obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Requerimiento de compra");

                var requerimiento = requerimientoMPP.BuscarPorId(obj.Id);
                if (requerimiento == null)
                    throw new Exception("No se encontro el requerimiento de compra especificado.");

                if (requerimiento.Estado == EstadoRequerimientoCompra.Procesado)
                    throw new Exception("No se puede eliminar un requerimiento ya procesado.");

                requerimientoMPP.Baja(requerimiento);
                ReconstructorBLL.LimpiarCache<BERequerimientoCompra>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar requerimiento de compra.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BERequerimientoCompra> ListarTodo()
        {
            try
            {
                var lista = requerimientoMPP.Listar();

                foreach (var r in lista)
                {
                    if (r.BookingOrigen != null && r.BookingOrigen.Id > 0)
                        r.BookingOrigen = ReconstructorBLL.Reconstruir<BEBooking>(r.BookingOrigen.Id);

                    if (r.Servicio != null && r.Servicio.Id > 0)
                        r.Servicio = ReconstructorBLL.Reconstruir<BEServicioSubcontratado>(r.Servicio.Id);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar requerimientos de compra.", ex);
            }
        }

        public BERequerimientoCompra ListarObjeto(BERequerimientoCompra obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Requerimiento de compra");

                var requerimiento = requerimientoMPP.BuscarPorId(obj.Id);

                if (requerimiento != null)
                {
                    if (requerimiento.BookingOrigen != null && requerimiento.BookingOrigen.Id > 0)
                        requerimiento.BookingOrigen = ReconstructorBLL.Reconstruir<BEBooking>(requerimiento.BookingOrigen.Id);

                    if (requerimiento.Servicio != null && requerimiento.Servicio.Id > 0)
                        requerimiento.Servicio = ReconstructorBLL.Reconstruir<BEServicioSubcontratado>(requerimiento.Servicio.Id);
                }

                return requerimiento;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar requerimiento de compra.", ex);
            }
        }

        public List<BERequerimientoCompra> ListarPendientes()
        {
            try
            {
                return ListarTodo()
                    .Where(r => r.Estado != null && r.Estado.Codigo == EstadoRequerimientoCompra.Pendiente.Codigo)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar requerimientos de compra pendientes.", ex);
            }
        }

        #endregion

        #region "Flujo de compras"

        public void GenerarDesdeBooking(BEBooking booking)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(booking, "Booking");

                if (booking.Id <= 0)
                    throw new Exception("El booking debe estar registrado previamente.");

                if (booking.Estado == null || booking.Estado.Codigo != EstadoBooking.Confirmado.Codigo)
                    return;

                if (booking.Servicios == null || booking.Servicios.Count == 0)
                    return;

                foreach (var servicio in booking.Servicios)
                {
                    if (servicio == null || servicio.Id <= 0)
                        continue;

                    if (ExisteRequerimientoActivo(booking.Id, servicio.Id))
                        continue;

                    Guardar(new BERequerimientoCompra
                    {
                        BookingOrigen = new BEBooking { Id = booking.Id },
                        Servicio = new BEServicioSubcontratado { Id = servicio.Id },
                        FechaSolicitud = DateTime.Now,
                        Estado = EstadoRequerimientoCompra.Pendiente
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar requerimientos de compra desde booking.", ex);
            }
        }

        public void MarcarProcesado(BERequerimientoCompra obj)
        {
            CambiarEstado(obj, EstadoRequerimientoCompra.Procesado);
        }

        public void MarcarAnulado(BERequerimientoCompra obj)
        {
            CambiarEstado(obj, EstadoRequerimientoCompra.Anulado);
        }

        private void CambiarEstado(BERequerimientoCompra obj, EstadoRequerimientoCompra nuevoEstado)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Requerimiento de compra");
                ValidadorBLL.ObjetoNoNulo(nuevoEstado, "Estado del requerimiento");

                var requerimiento = requerimientoMPP.BuscarPorId(obj.Id);
                if (requerimiento == null)
                    throw new Exception("No se encontro el requerimiento de compra especificado.");

                requerimiento.Estado = nuevoEstado;
                requerimientoMPP.Modificar(requerimiento);

                ReconstructorBLL.LimpiarCache<BERequerimientoCompra>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el estado del requerimiento de compra.", ex);
            }
        }

        private bool ExisteRequerimientoActivo(long bookingId, long servicioId)
        {
            return requerimientoMPP.Listar().Any(r =>
                r.BookingOrigen != null &&
                r.Servicio != null &&
                r.BookingOrigen.Id == bookingId &&
                r.Servicio.Id == servicioId &&
                r.Estado != null &&
                r.Estado.Codigo != EstadoRequerimientoCompra.Anulado.Codigo);
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return requerimientoMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ultimo Id de requerimiento de compra.", ex);
            }
        }

        #endregion
    }
}

