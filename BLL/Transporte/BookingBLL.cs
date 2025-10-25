using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE;
using BE.Enums;
using BE.Transporte;
using MPP.Bookings;
using BLL.Transversal;

namespace BLL.Transporte
{
    public class BookingBLL : IGestor<BEBooking>
    {
        private BookingMPP bookingMPP = new BookingMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir un booking por Id
        static BookingBLL()
        {
            ReconstructorBLL.Registrar<BEBooking>(
                id => new BookingBLL().ListarObjeto(new BEBooking { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEBooking obj)
        {
            try
            {
                // Validaciones
                ValidadorBLL.ObjetoNoNulo(obj, "Booking");
                ValidadorBLL.ObjetoNoNulo(obj.Cliente, "Cliente asociado");
                ValidadorBLL.ListaNoVacia(obj.Cargas, "Cargas asociadas");
                ValidadorBLL.CampoObligatorio(obj.Numero, "Número de booking");

                if (obj.FechaGeneracion > DateTime.Now)
                    throw new Exception("La fecha de generación no puede ser futura.");

                if (obj.Estado == EstadoBookingEnum.Cancelado && obj.Cargas.Count > 0)
                    throw new Exception("No se puede cancelar un booking con cargas activas.");

                // Persistencia
                if (obj.Id == 0)
                    bookingMPP.Alta(obj);
                else
                    bookingMPP.Modificar(obj);

                ReconstructorBLL.LimpiarCache<BEBooking>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar booking.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEBooking obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Booking");
                bookingMPP.Baja(obj);

                ReconstructorBLL.LimpiarCache<BEBooking>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar booking.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEBooking> ListarTodo()
        {
            try
            {
                var lista = bookingMPP.Listar();

                // Reconstrucción de relaciones por Id
                foreach (var b in lista)
                {
                    if (b.Cliente != null && b.Cliente.Id > 0)
                        b.Cliente = ReconstructorBLL.Reconstruir<BECliente>(b.Cliente.Id);

                    if (b.Contenedor != null && b.Contenedor.Id > 0)
                        b.Contenedor = ReconstructorBLL.Reconstruir<BEContenedor>(b.Contenedor.Id);

                    if (b.Buque != null && b.Buque.Id > 0)
                        b.Buque = ReconstructorBLL.Reconstruir<BEBuque>(b.Buque.Id);

                    if (b.Cargas != null && b.Cargas.Count > 0)
                    {
                        var ids = b.Cargas.Select(c => c.Id).ToList();
                        b.Cargas = ReconstructorBLL.ReconstruirLista<BECarga>(ids);
                    }

                    if (b.Servicios != null && b.Servicios.Count > 0)
                    {
                        var ids = b.Servicios.Select(s => s.Id).ToList();
                        b.Servicios = ReconstructorBLL.ReconstruirLista<BEServicioSubcontratado>(ids);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar bookings.", ex);
            }
        }

        public BEBooking ListarObjeto(BEBooking obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Booking");

                var booking = bookingMPP.BuscarPorId(obj.Id);

                if (booking != null)
                {
                    if (booking.Cliente != null && booking.Cliente.Id > 0)
                        booking.Cliente = ReconstructorBLL.Reconstruir<BECliente>(booking.Cliente.Id);

                    if (booking.Contenedor != null && booking.Contenedor.Id > 0)
                        booking.Contenedor = ReconstructorBLL.Reconstruir<BEContenedor>(booking.Contenedor.Id);

                    if (booking.Buque != null && booking.Buque.Id > 0)
                        booking.Buque = ReconstructorBLL.Reconstruir<BEBuque>(booking.Buque.Id);

                    if (booking.Cargas != null && booking.Cargas.Count > 0)
                    {
                        var ids = booking.Cargas.Select(c => c.Id).ToList();
                        booking.Cargas = ReconstructorBLL.ReconstruirLista<BECarga>(ids);
                    }

                    if (booking.Servicios != null && booking.Servicios.Count > 0)
                    {
                        var ids = booking.Servicios.Select(s => s.Id).ToList();
                        booking.Servicios = ReconstructorBLL.ReconstruirLista<BEServicioSubcontratado>(ids);
                    }
                }

                return booking;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar booking.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return bookingMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de booking.", ex);
            }
        }

        #endregion
    }
}
