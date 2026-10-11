using Abstraccion;
using BE.Dominio;
using BE.Finanzas;
using BE.Transporte;
using BLL.Infra;
using BLL.Transporte;
using MPP.Finanzas;
using System;
using System.Collections.Generic;
using System.Linq;
using Transversales;

namespace BLL.Finanzas
{
    public class FacturaBLL : IGestor<BEFactura>
    {
        private FacturaMPP facturaMPP = new FacturaMPP();

        #region "Constructor estático"

        static FacturaBLL()
        {
            ReconstructorBLL.Registrar<BEFactura>(
                id => new FacturaBLL().ListarObjeto(new BEFactura { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEFactura fc)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(fc, "Factura");
                ValidadorBLL.ObjetoNoNulo(fc.Booking, "Booking asociado");

                // Solo se factura un booking asignado: ya pasó por asignación de buque/contenedor.
                var bookingActual = ReconstructorBLL.Reconstruir<BEBooking>(fc.Booking.Id);

                if (bookingActual == null)
                    throw new Exception("No se encontró el booking asociado a la factura.");

                if (bookingActual.Estado == null || bookingActual.Estado.Codigo != EstadoBooking.Asignado.Codigo)
                    throw new Exception("Solo se pueden facturar bookings asignados.");

                fc.Estado = EstadoFactura.Emitida;

                if (fc.Id != 0)
                    ValidadorBLL.CampoObligatorio(fc.NumeroFactura, "Número de factura");

                if (fc.Subtotal <= 0)
                    throw new Exception("El subtotal debe ser mayor a cero.");

                if (fc.Impuesto < 0)
                    throw new Exception("El impuesto no puede ser negativo.");

                if (fc.Fecha > DateTime.Now)
                    throw new Exception("La fecha de la factura no puede ser futura.");

                if (fc.Estado == EstadoFactura.Anulada)
                    throw new Exception("No se puede guardar una factura anulada.");

                // El total se calcula en BLL para que la UI no decida importes finales.
                fc.Total = fc.Subtotal + fc.Impuesto;

                var existentes = facturaMPP.Listar();

                bool yaFacturado = existentes.Any(f =>
                    f.Booking != null &&
                    f.Booking.Id == fc.Booking.Id &&
                    f.Id != fc.Id &&
                    (f.Estado == null || f.Estado.Codigo != EstadoFactura.Anulada.Codigo)
                );

                if (yaFacturado)
                    throw new Exception("Este booking ya fue facturado. No se puede emitir una nueva factura para el mismo booking.");

                if (fc.Id == 0)
                    facturaMPP.Alta(fc);
                else
                    facturaMPP.Modificar(fc);

                // Si la factura se emite correctamente, la BLL actualiza el booking a Facturado.
                var bookingBLL = new BookingBLL();
                bookingBLL.ActualizarEstado(bookingActual, EstadoBooking.Facturado);

                ReconstructorBLL.LimpiarCache<BEBooking>();
                ReconstructorBLL.LimpiarCache<BEFactura>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar factura.", ex);
            }
        }

        #endregion

        #region "Anular"

        public void Eliminar(BEFactura obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Factura");

                // Se consulta la factura persistida para validar con el estado real, no con datos incompletos de UI.
                var actual = facturaMPP.BuscarPorId(obj.Id);
                if (actual == null)
                    throw new Exception("No se encontró la factura para anular.");

                if (actual.Estado != null && actual.Estado.Codigo == EstadoFactura.Pagada.Codigo)
                    throw new Exception("No se puede anular una factura ya pagada.");

                if (actual.Estado != null && actual.Estado.Codigo == EstadoFactura.Anulada.Codigo)
                    throw new Exception("La factura ya se encuentra anulada.");

                // Anular es una baja lógica: la factura queda registrada, pero no operativa.
                facturaMPP.Baja(actual);

                ReconstructorBLL.LimpiarCache<BEFactura>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al anular factura.", ex);
            }
        }

        #endregion
        #region "Marcar como pagada"

        public void MarcarComoPagada(BEFactura factura)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(factura, "Factura");

                if (factura.Id <= 0)
                    throw new Exception("La factura no tiene un Id válido.");

                var actual = facturaMPP.BuscarPorId(factura.Id);

                if (actual == null)
                    throw new Exception("No se encontró la factura indicada.");

                if (actual.Estado != null && actual.Estado.Codigo == EstadoFactura.Anulada.Codigo)
                    throw new Exception("No se puede pagar una factura anulada.");

                if (actual.Estado != null && actual.Estado.Codigo == EstadoFactura.Pagada.Codigo)
                    throw new Exception("La factura ya se encuentra pagada.");

                actual.Estado = EstadoFactura.Pagada;

                facturaMPP.Modificar(actual);

                ReconstructorBLL.LimpiarCache<BEFactura>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al marcar la factura como pagada.", ex);
            }
        }

        #endregion
        #region "Listar / Buscar"

        public List<BEFactura> ListarTodo()
        {
            try
            {
                var lista = facturaMPP.Listar();

                foreach (var f in lista)
                {
                    if (f.Booking != null && f.Booking.Id > 0)
                        f.Booking = ReconstructorBLL.Reconstruir<BEBooking>(f.Booking.Id);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar facturas.", ex);
            }
        }

        public BEFactura ListarObjeto(BEFactura fc)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(fc, "Factura");

                var factura = facturaMPP.BuscarPorId(fc.Id);
                if (factura != null && factura.Booking != null && factura.Booking.Id > 0)
                    factura.Booking = ReconstructorBLL.Reconstruir<BEBooking>(factura.Booking.Id);

                return factura;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar factura.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return facturaMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de factura.", ex);
            }
        }
        public string ObtenerProximoNumeroPreview(TipoFactura tipo)
        {
            try
            {
                return GeneradorNumeros.ObtenerProximoNumeroFactura(tipo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar número de vista previa de factura.", ex);
            }
        }

        #endregion
    }
}
