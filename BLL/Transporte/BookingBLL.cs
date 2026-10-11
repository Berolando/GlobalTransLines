using Abstraccion;
using BE.Dominio;
using BE.Personas;
using BE.ServiciosSubcontratados;
using BE.Transporte;
using BE.Finanzas;
using MPP.Bookings;
using BLL.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using BE.Infra;
using BLL.Finanzas;
using Transversales;


namespace BLL.Transporte
{
    public class BookingBLL : IGestor<BEBooking>
    {
        private BookingMPP bookingMPP = new BookingMPP();
        private CotizacionBLL cotizacionBLL = new CotizacionBLL();

        #region "Constructor estático"

        static BookingBLL()
        {
            ReconstructorBLL.Registrar<BEBooking>(
                id => new BookingBLL().ListarObjeto(new BEBooking { Id = id })
            );
        }

        #endregion

        #region "Guardar"
       
        public void Guardar(BEBooking booking)
        {
            Guardar(booking, null);
        }
        // Guarda el booking y, si corresponde, deja registrado el evento en bitácora.
        public void Guardar(BEBooking booking, BEUsuario usuarioActual)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(booking, "Booking");

                if (booking.Cliente == null || booking.Cliente.Id <= 0)
                    throw new Exception("Debe seleccionar un cliente válido.");

                ValidadorBLL.ListaNoVacia(booking.Cargas, "Cargas asociadas");

                var cargasCompletas = booking.Cargas
                    .Select(c => c != null && c.Id > 0
                        ? ReconstructorBLL.Reconstruir<BECarga>(c.Id)
                        : c)
                    .Where(c => c != null)
                    .ToList();

                if (cargasCompletas.Count != booking.Cargas.Count)
                    throw new Exception("No se pudieron reconstruir todas las cargas del booking.");

                var tiposContenedor = cargasCompletas
                    .Select(c => c.TipoContenedor?.Codigo)
                    .Where(codigo => !string.IsNullOrWhiteSpace(codigo))
                    .Distinct()
                    .ToList();

                if (tiposContenedor.Count != 1)
                    throw new Exception("Las cargas del booking deben requerir el mismo tipo de contenedor.");

                booking.Cargas = cargasCompletas;
                booking.TipoContenedorCodigo = tiposContenedor[0];

                if (booking.FechaTentativaSalida.HasValue &&
                    booking.FechaTentativaSalida.Value < booking.FechaGeneracion)
                    throw new Exception("La fecha tentativa de salida no puede ser anterior a la fecha de generación.");

                var idsDuplicados = booking.Cargas.GroupBy(c => c.Id)
                                    .Where(g => g.Count() > 1)
                                    .Select(g => g.Key)
                                    .ToList();

                if (idsDuplicados.Count > 0)
                    throw new Exception("Existen cargas duplicadas en el booking.");

                if (booking.Cotizacion != null && booking.Cotizacion.Id > 0)
                {
                    var coti = ReconstructorBLL.Reconstruir<BECotizacion>(booking.Cotizacion.Id);
                    ValidadorBLL.ValidarCotizacionParaBooking(coti);
                }

                foreach (var carga in booking.Cargas)
                {
                    ValidadorBLL.ValidarCargaParaBooking(carga);
                }

                bool esNuevo = booking.Id == 0;
                if (esNuevo && booking.Estado == null)
                    booking.Estado = EstadoBooking.Confirmado;

                if (esNuevo)
                    bookingMPP.Alta(booking);
                else
                    bookingMPP.Modificar(booking);

                if (booking.Estado != null && booking.Estado.Codigo == EstadoBooking.Confirmado.Codigo &&
                     booking.Servicios != null && booking.Servicios.Count > 0)
                {
                    // Al confirmar el booking, los servicios subcontratados generan requerimientos para compras.
                    new RequerimientoCompraBLL().GenerarDesdeBooking(booking);
                }
                if (booking.Cargas != null && booking.Cargas.Count > 0)
                {
                    var cargaBLL = new CargaBLL();
                    foreach (var carga in booking.Cargas)
                    {
                        var cFull = ReconstructorBLL.Reconstruir<BECarga>(carga.Id);
                        if (cFull != null)
                            cargaBLL.ActualizarEstado(cFull, EstadoCarga.AsignadaBooking);
                    }
                }

                // Registra el alta/modificación del booking cuando el flujo informa un usuario responsable.
                if (usuarioActual != null)
                {
                    new BitacoraBLL().RegistrarEvento(
                        usuarioActual,
                        TipoOperacion.Alta,
                        "Booking",
                        "N° " + booking.Numero + " - Estado: " + booking.Estado.Descripcion
                    );
                }

                // La cotización queda marcada como usada para no volver a generar otro booking con la misma base.
                if (booking.Cotizacion != null && booking.Cotizacion.Id > 0)
                {
                    cotizacionBLL.CambiarEstado(booking.Cotizacion, EstadoCotizacion.EnBooking);
                }

                // Se limpian las entidades afectadas para que futuras consultas reconstruyan datos actualizados.
                ReconstructorBLL.LimpiarCache<BEBooking>();
                ReconstructorBLL.LimpiarCache<BECarga>();
                ReconstructorBLL.LimpiarCache<BECotizacion>();
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

                var booking = bookingMPP.BuscarPorId(obj.Id);
                if (booking == null)
                    throw new Exception("No se encontró el booking especificado.");

                if (booking.Cargas != null && booking.Cargas.Any())
                    throw new Exception("No se puede eliminar el booking porque tiene cargas asociadas.");

                if (booking.Servicios != null && booking.Servicios.Any())
                    throw new Exception("No se puede eliminar el booking porque tiene servicios subcontratados asociados.");

                bookingMPP.Baja(booking);
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

                foreach (var b in lista)
                {
                    if (b.Cliente != null && b.Cliente.Id > 0)
                        b.Cliente = ReconstructorBLL.Reconstruir<BECliente>(b.Cliente.Id);

                    if (b.Cotizacion != null && b.Cotizacion.Id > 0)
                        b.Cotizacion = ReconstructorBLL.Reconstruir<BECotizacion>(b.Cotizacion.Id);

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

                    if (booking.Cotizacion != null && booking.Cotizacion.Id > 0)
                        booking.Cotizacion = ReconstructorBLL.Reconstruir<BECotizacion>(booking.Cotizacion.Id);

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

        public string ObtenerProximoNumeroPreview()
        {
            try
            {
                long proximoId = ObtenerUltimoId() + 1;
                return GeneradorNumeros.GenerarNumeroBooking(proximoId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar número de vista previa de booking.", ex);
            }
        }
        #region "Estados"

        public void ActualizarEstado(BEBooking booking, EstadoBooking nuevoEstado)
        {
            try
            {
                // Valido que el booking no llegue nulo.
                ValidadorBLL.ObjetoNoNulo(booking, "Booking");

                // Valido que el nuevo estado no llegue nulo.
                ValidadorBLL.ObjetoNoNulo(nuevoEstado, "Estado del booking");

                // Valido que el booking tenga Id porque necesito buscarlo en XML.
                if (booking.Id <= 0)
                    throw new ArgumentException("El booking debe tener un Id válido para actualizar su estado.");

                // Busco el booking completo desde persistencia.
                // No uso Guardar() porque Guardar valida cotización, cargas y cliente como si fuera un alta/modificación completa.
                var bookingActual = bookingMPP.BuscarPorId(booking.Id);

                if (bookingActual == null)
                    throw new Exception("No se encontró el booking especificado.");

                // Actualizo únicamente el estado del booking.
                bookingActual.Estado = nuevoEstado;

                // Persisto solamente la modificación del booking ya existente.
                bookingMPP.Modificar(bookingActual);

                // Limpio cache porque cambió el estado del booking.
                ReconstructorBLL.LimpiarCache<BEBooking>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado del booking.", ex);
            }
        }

        #endregion

        #endregion

    }
}
