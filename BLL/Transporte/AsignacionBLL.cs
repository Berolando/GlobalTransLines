using Abstraccion;
using BE.Dominio;
using BE.Transporte;
using BLL.Transporte;
using BLL.Infra;
using MPP.Transporte;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Transporte
{
    public class AsignacionBLL : IGestor<BEAsignacion>
    {
        private readonly AsignacionMPP asignacionMPP = new AsignacionMPP();
        private readonly ContenedorBLL contenedorBLL = new ContenedorBLL();
        private readonly BuqueBLL buqueBLL = new BuqueBLL();
        private readonly BookingBLL bookingBLL = new BookingBLL();

        #region "Constructor estático"


        static AsignacionBLL()
        {
            ReconstructorBLL.Registrar<BEAsignacion>(
                id => new AsignacionBLL().ListarObjeto(new BEAsignacion { Id = id })
            );
        }

        #endregion

        #region "Guardar / Eliminar"

        public void Guardar(BEAsignacion obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Asignación");

                if (obj.BookingId <= 0 || obj.ContenedorId <= 0 || obj.BuqueId <= 0)
                    throw new ArgumentException("La asignación debe incluir BookingId, ContenedorId y BuqueId válidos.");

               
                if (obj.Id == 0)
                {
                    bool bookingYaAsignado = asignacionMPP.Listar()
                        .Any(a => a.BookingId == obj.BookingId);

                    if (bookingYaAsignado)
                        throw new InvalidOperationException(
                            "El booking seleccionado ya tiene una asignación registrada."
                        );

                    asignacionMPP.Alta(obj);
                }
                else
                {
                    asignacionMPP.Modificar(obj);
                }

                ReconstructorBLL.LimpiarCache<BEAsignacion>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar asignación.", ex);
            }
        }

        public void Eliminar(BEAsignacion obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Asignación");
                asignacionMPP.Baja(obj);
                ReconstructorBLL.LimpiarCache<BEAsignacion>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al eliminar asignación.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEAsignacion> ListarTodo()
        {
            try
            {
                var lista = asignacionMPP.Listar();

                // Reconstruye las relaciones de cada asignación
                foreach (var a in lista)
                {
                    a.Booking = ReconstructorBLL.Reconstruir<BEBooking>(a.BookingId);
                    a.Contenedor = ReconstructorBLL.Reconstruir<BEContenedor>(a.ContenedorId);
                    a.Buque = ReconstructorBLL.Reconstruir<BEBuque>(a.BuqueId);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar asignaciones.", ex);
            }
        }


        public BEAsignacion ListarObjeto(BEAsignacion filtro)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(filtro, "Filtro");
                return asignacionMPP.BuscarPorId(filtro.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar asignación.", ex);
            }
        }

        #endregion

        #region "Negocio principal: Asignar"

        // Asigna un contenedor y un buque a un booking, validando compatibilidad, capacidad y estados
        public void Asignar(BEBooking booking, BEContenedor contenedor, BEBuque buque)
        {
            try
            {
                // Primero valido que los objetos principales no lleguen nulos.
                // Si alguno llega nulo, no puedo continuar porque la asignación necesita los tres:
                // booking, contenedor y buque.

                ValidadorBLL.ObjetoNoNulo(booking, "Booking");
                ValidadorBLL.ObjetoNoNulo(contenedor, "Contenedor");
                ValidadorBLL.ObjetoNoNulo(buque, "Buque");

                // Se reconstruyen las entidades para validar con datos completos y no con objetos parciales de la UI.
                if (booking.Id > 0)
                    booking = ReconstructorBLL.Reconstruir<BEBooking>(booking.Id);

                if (contenedor.Id > 0)
                    contenedor = ReconstructorBLL.Reconstruir<BEContenedor>(contenedor.Id);

                if (buque.Id > 0)
                    buque = ReconstructorBLL.Reconstruir<BEBuque>(buque.Id);

                if (booking == null || contenedor == null || buque == null)
                    throw new InvalidOperationException("No se pudieron reconstruir los datos completos de la asignación.");

                bool bookingYaAsignado = asignacionMPP.Listar()
                    .Any(a => a.BookingId == booking.Id);
               
                if (bookingYaAsignado)
                    throw new InvalidOperationException($"El booking {booking.Numero} ya tiene una asignación. No se puede asignar nuevamente.");

                // El booking debe estar en un estado operativo que permita asignarle buque y contenedor.
                if (booking.Estado == null || booking.Estado.Codigo != EstadoBooking.Confirmado.Codigo)
                    throw new InvalidOperationException(
                        "El booking " + booking.Numero + " debe estar confirmado antes de asignar."
                    );

                // Valida disponibilidad del contenedor y operatividad del buque
                contenedorBLL.ValidarDisponibilidad(contenedor);

                if (buque.Estado == null || buque.Estado.Codigo != "DIS")
                    throw new InvalidOperationException($"El buque {buque.Nombre} no se encuentra operativo o disponible.");

                ValidarRuta(booking, buque);

                // Validaciones de compatibilidad y capacidad
                if (booking.Cargas != null && booking.Cargas.Count > 0)
                {
                    foreach (var carga in booking.Cargas)
                    {
                        contenedorBLL.ValidarCompatibilidad(carga, contenedor);
                    }

                    contenedor.Cargas = booking.Cargas;
                    contenedorBLL.ValidarCapacidad(contenedor);
                }

                decimal pesoRequeridoToneladas = booking.Cargas.Sum(c => c.PesoKg) / 1000m;
                decimal pesoDisponibleToneladas = buqueBLL.CalcularDisponibilidadPesoToneladas(buque.Id);

                if (pesoRequeridoToneladas > pesoDisponibleToneladas)
                {
                    throw new ArgumentException(
                        "No se puede asignar: el buque excede su capacidad de peso.\n" +
                        "Disponible: " + pesoDisponibleToneladas.ToString("0.##") + " tn\n" +
                        "Requerido: " + pesoRequeridoToneladas.ToString("0.##") + " tn");
                }

                // Calculo cuántos TEU ya tiene asignados el buque.
                // TEU significa Twenty-foot Equivalent Unit / Unidad equivalente a veinte pies.
                int teuAsignados = buqueBLL.CalcularTeuAsignados(buque.Id);
                int teuNuevo = buqueBLL.CalcularTeuPorContenedor(contenedor);

                if (teuAsignados + teuNuevo > buque.CapacidadTEU)
                {
                    decimal porcentaje = buqueBLL.CalcularPorcentajeOcupado(buque.Id);
                    throw new ArgumentException( "No se puede asignar: el buque excede su capacidad TEU.\n" +
                        "Buque: " + buque.Nombre + "\n" +
                        "Capacidad: " + buque.CapacidadTEU + " TEU\n" +
                        "Ocupado: " + teuAsignados + " TEU (" + porcentaje.ToString("0.##") + "%)\n" +
                        "Este contenedor suma: " + teuNuevo + " TEU"
                    );
                }


                // Si pasé todas las validaciones, creo la asignación.
                // Antes de este punto no persisto nada para evitar dejar datos a medias.
                var asignacion = new BEAsignacion
                {
                    BookingId = booking.Id,
                    ContenedorId = contenedor.Id,
                    BuqueId = buque.Id,
                    FechaAsignacion = DateTime.Now,
                    Estado = EstadoAsignacion.Confirmada
                };

                asignacionMPP.Alta(asignacion);

                // Al asignarse, el contenedor deja de estar disponible para otros bookings.
                contenedor.Estado = EstadoContenedor.Asignado;
                // Vincula el contenedor al buque (persiste BuqueId)
                contenedor.Buque = new BEBuque { Id = buque.Id };

                contenedorBLL.Guardar(contenedor);
                bookingBLL.ActualizarEstado(booking, EstadoBooking.Asignado);


                // Se limpian las entidades afectadas para que próximas consultas reflejen la asignación.
                ReconstructorBLL.LimpiarCache<BEContenedor>();
                ReconstructorBLL.LimpiarCache<BEAsignacion>();
                ReconstructorBLL.LimpiarCache<BEBooking>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar la asignación.", ex);
            }
        }

        #endregion

        #region "Sugerencia de recursos"

        // Sugiere contenedores y buques compatibles para un booking determinado
        public (List<BEContenedor>, List<BEBuque>) SugerirRecursosPara(BEBooking booking)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(booking, "Booking");

                if (booking.Id > 0)
                    booking = ReconstructorBLL.Reconstruir<BEBooking>(booking.Id);

                var contenedores = contenedorBLL.ListarTodo().ToList()
                    .Where(c => c.Estado.EsDisponible())
                    .Where(c =>
                    {
                        try
                        {
                            foreach (var carga in booking.Cargas)
                                contenedorBLL.ValidarCompatibilidad(carga, c);

                            var candidato = new BEContenedor
                            {
                                Id = c.Id,
                                Tipo = c.Tipo,
                                Restriccion = c.Restriccion,
                                Cargas = booking.Cargas
                            };
                            contenedorBLL.ValidarCapacidad(candidato);
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    })
                    .ToList();

                var buques = buqueBLL.ListarTodo()
                    .Where(b => b.Estado != null && b.Estado.Codigo == EstadoBuque.Disponible.Codigo)
                    .Where(b => buqueBLL.CalcularDisponibilidadPesoToneladas(b.Id) >=
                                booking.Cargas.Sum(c => c.PesoKg) / 1000m)
                    .Where(b =>
                    {
                        try
                        {
                            ValidarRuta(booking, b);
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    })
                    .ToList();

                return (contenedores, buques);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al sugerir recursos para el booking.", ex);
            }
        }

        #endregion

        #region "Validación de ruta"

        private void ValidarRuta(BEBooking booking, BEBuque buque)
        {
            var cargas = booking.Cargas ?? new List<BECarga>();
            if (cargas.Count == 0)
                throw new InvalidOperationException("El booking no contiene cargas para determinar su ruta.");

            var destinos = cargas
                .Where(c => c.Destino != null && c.Destino.Id > 0)
                .Select(c => c.Destino)
                .GroupBy(d => d.Id)
                .Select(g => g.First())
                .ToList();

            if (destinos.Count != 1)
                throw new InvalidOperationException(
                    "Todas las cargas del booking deben compartir un único puerto de destino.");

            var puertosEmbarque = cargas
                .Where(c => c.PuertoEmbarque != null && c.PuertoEmbarque.Id > 0)
                .Select(c => c.PuertoEmbarque)
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .ToList();

            if (puertosEmbarque.Count != 1)
                throw new InvalidOperationException(
                    "Todas las cargas del booking deben compartir un único puerto de embarque.");

            var puertoEmbarque = puertosEmbarque[0];
            var destino = destinos[0];

            if (!buqueBLL.RecorrePuerto(buque, puertoEmbarque) ||
                !buqueBLL.RecorrePuerto(buque, destino))
            {
                throw new InvalidOperationException(
                    "El buque " + buque.Nombre + " no cubre la ruta " +
                    puertoEmbarque.Puerto + " - " + destino.Puerto + ".");
            }
        }

        #endregion

        #region "Obtener último Id"

        public long ObtenerUltimoId()
        {
            try
            {
                return asignacionMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el último Id de asignación.", ex);
            }
        }

        #endregion
    }
}
