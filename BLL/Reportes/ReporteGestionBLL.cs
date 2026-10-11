using BE.Dominio;
using BE.Finanzas;
using BE.Reportes;
using BE.Transporte;
using BLL.Finanzas;
using BLL.Transporte;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Reportes
{
    public class ReporteGestionBLL
    {
        private readonly CotizacionBLL cotizacionBLL = new CotizacionBLL();
        private readonly BookingBLL bookingBLL = new BookingBLL();
        private readonly AsignacionBLL asignacionBLL = new AsignacionBLL();
        private readonly BuqueBLL buqueBLL = new BuqueBLL();
        private readonly FacturaBLL facturaBLL = new FacturaBLL();
        private readonly PagoBLL pagoBLL = new PagoBLL();
        private readonly OrdenCompraBLL ordenCompraBLL = new OrdenCompraBLL();
        private readonly PagoProveedorBLL pagoProveedorBLL = new PagoProveedorBLL();
        private readonly BillOfLadingBLL billOfLadingBLL = new BillOfLadingBLL();
        private readonly ManifiestoBLL manifiestoBLL = new ManifiestoBLL();

        #region "Gestión comercial"

        public BEReporteComercial ObtenerComercial(DateTime desde, DateTime hasta)
        {
            ValidarRango(desde, hasta);

            List<BECotizacion> cotizaciones = cotizacionBLL.ListarTodo()
                .Where(c => EstaEnRango(c.FechaEmision, desde, hasta))
                .ToList();

            List<BEBooking> bookings = bookingBLL.ListarTodo();
            BEReporteComercial reporte = new BEReporteComercial();

            foreach (BECotizacion cotizacion in cotizaciones)
            {
                BEBooking booking = bookings.FirstOrDefault(b =>
                    b.Cotizacion != null && b.Cotizacion.Id == cotizacion.Id);

                reporte.Detalle.Add(new BEReporteComercialFila
                {
                    Fecha = cotizacion.FechaEmision,
                    Cotizacion = cotizacion.Numero,
                    Cliente = cotizacion.Cliente != null ? cotizacion.Cliente.RazonSocial : string.Empty,
                    Estado = cotizacion.Estado != null ? cotizacion.Estado.Descripcion : "Sin estado",
                    Monto = cotizacion.MontoTotal,
                    Booking = booking != null ? booking.Numero : string.Empty
                });
            }

            reporte.Cotizaciones = cotizaciones.Count;
            reporte.BookingsGenerados = reporte.Detalle.Count(x => !string.IsNullOrWhiteSpace(x.Booking));
            reporte.MontoCotizado = cotizaciones.Sum(c => c.MontoTotal);
            reporte.PorcentajeConversion = reporte.Cotizaciones > 0
                ? reporte.BookingsGenerados * 100m / reporte.Cotizaciones
                : 0;
            reporte.Estados = reporte.Detalle
                .GroupBy(x => x.Estado)
                .Select(g => new BEReporteAgrupado { Nombre = g.Key, Valor = g.Count() })
                .OrderBy(x => x.Nombre)
                .ToList();

            reporte.Detalle = reporte.Detalle.OrderByDescending(x => x.Fecha).ToList();
            return reporte;
        }

        #endregion

        #region "Gestión de transporte"

        public List<BEBuque> ListarBuques()
        {
            return buqueBLL.ListarTodo().OrderBy(b => b.Nombre).ToList();
        }

        public BEReporteTransporte ObtenerTransporte(long buqueId, DateTime desde, DateTime hasta)
        {
            ValidarRango(desde, hasta);

            BEBuque buque = buqueBLL.ListarTodo().FirstOrDefault(b => b.Id == buqueId);
            if (buque == null)
                throw new Exception("No se encontró el buque seleccionado.");

            List<BEAsignacion> asignaciones = asignacionBLL.ListarTodo()
                .Where(a => a.BuqueId == buqueId && a.Activo)
                .ToList();

            BEReporteTransporte reporte = new BEReporteTransporte
            {
                BuqueId = buque.Id,
                Buque = buque.Nombre,
                CapacidadTotalTEU = buque.CapacidadTEU,
                CapacidadTotalToneladas = buque.CapacidadPeso,
                Itinerario = buque.Itinerario != null && buque.Itinerario.Count > 0
                    ? string.Join(" -> ", buque.Itinerario.Select(p => p.Puerto))
                    : "Sin itinerario registrado"
            };

            foreach (BEAsignacion asignacion in asignaciones)
            {
                BEContenedor contenedor = asignacion.Contenedor;
                BEBooking booking = asignacion.Booking;

                if (contenedor == null)
                    continue;

                int teu = ObtenerTEU(contenedor);
                decimal pesoKg = contenedor.Cargas != null
                    ? contenedor.Cargas.Sum(c => c.PesoKg)
                    : 0;

                reporte.Contenedores.Add(new BEReporteTransporteFila
                {
                    Contenedor = contenedor.Codigo,
                    Tipo = contenedor.Tipo != null ? contenedor.Tipo.Descripcion : string.Empty,
                    TEU = teu,
                    PesoKg = pesoKg,
                    Booking = booking != null ? booking.Numero : string.Empty,
                    Cliente = booking != null && booking.Cliente != null
                        ? booking.Cliente.RazonSocial
                        : string.Empty,
                    Destino = ObtenerDestino(booking),
                    Estado = contenedor.Estado != null ? contenedor.Estado.Descripcion : "Sin estado"
                });
            }

            reporte.Contenedores = reporte.Contenedores
                .GroupBy(c => c.Contenedor)
                .Select(g => g.First())
                .OrderBy(c => c.Contenedor)
                .ToList();

            reporte.CapacidadOcupadaTEU = reporte.Contenedores.Sum(c => c.TEU);
            reporte.CapacidadLibreTEU = Math.Max(0, reporte.CapacidadTotalTEU - reporte.CapacidadOcupadaTEU);
            reporte.PesoCargadoToneladas = reporte.Contenedores.Sum(c => c.PesoKg) / 1000m;
            reporte.CapacidadLibreToneladas = Math.Max(0, reporte.CapacidadTotalToneladas - reporte.PesoCargadoToneladas);
            reporte.PorcentajeOcupacionTEU = reporte.CapacidadTotalTEU > 0
                ? reporte.CapacidadOcupadaTEU * 100m / reporte.CapacidadTotalTEU
                : 0;
            reporte.PorcentajeOcupacionPeso = reporte.CapacidadTotalToneladas > 0
                ? reporte.PesoCargadoToneladas * 100m / reporte.CapacidadTotalToneladas
                : 0;
            reporte.BillsEmitidos = billOfLadingBLL.ListarTodo()
                .Count(b => b.Buque != null && b.Buque.Id == buqueId
                    && EstaEnRango(b.FechaEmision, desde, hasta));
            reporte.ManifiestosEmitidos = manifiestoBLL.ListarTodo()
                .Count(m => m.Buque != null && m.Buque.Id == buqueId
                    && EstaEnRango(m.FechaEmision, desde, hasta));

            return reporte;
        }

        #endregion

        #region "Gestión de facturación"

        public BEReporteFacturacion ObtenerFacturacion(DateTime desde, DateTime hasta)
        {
            ValidarRango(desde, hasta);

            List<BEFactura> facturas = facturaBLL.ListarTodo()
                .Where(f => EstaEnRango(f.Fecha, desde, hasta))
                .ToList();
            List<BEPago> pagos = pagoBLL.ListarTodo();
            BEReporteFacturacion reporte = new BEReporteFacturacion();

            foreach (BEFactura factura in facturas)
            {
                decimal cobrado = pagos
                    .Where(p => p.Factura != null && p.Factura.Id == factura.Id
                        && p.Estado != null && p.Estado.Codigo == EstadoPago.Pagado.Codigo)
                    .Sum(p => p.Importe);

                bool anulada = factura.Estado != null
                    && factura.Estado.Codigo == EstadoFactura.Anulada.Codigo;
                decimal totalVigente = anulada ? 0 : factura.Total;

                reporte.Detalle.Add(new BEReporteFacturacionFila
                {
                    Fecha = factura.Fecha,
                    Factura = factura.NumeroFactura,
                    Booking = factura.Booking != null ? factura.Booking.Numero : string.Empty,
                    Cliente = factura.Booking != null && factura.Booking.Cliente != null
                        ? factura.Booking.Cliente.RazonSocial
                        : string.Empty,
                    Estado = factura.Estado != null ? factura.Estado.Descripcion : "Sin estado",
                    Total = totalVigente,
                    Cobrado = cobrado,
                    Pendiente = Math.Max(0, totalVigente - cobrado)
                });
            }

            reporte.Facturas = reporte.Detalle.Count;
            reporte.TotalFacturado = reporte.Detalle.Sum(x => x.Total);
            reporte.TotalCobrado = reporte.Detalle.Sum(x => x.Cobrado);
            reporte.PendienteCobro = reporte.Detalle.Sum(x => x.Pendiente);
            reporte.Estados = reporte.Detalle
                .GroupBy(x => x.Estado)
                .Select(g => new BEReporteAgrupado { Nombre = g.Key, Valor = g.Count() })
                .OrderBy(x => x.Nombre)
                .ToList();
            reporte.Detalle = reporte.Detalle.OrderByDescending(x => x.Fecha).ToList();

            return reporte;
        }

        #endregion

        #region "Gestión de compras"

        public BEReporteCompras ObtenerCompras(DateTime desde, DateTime hasta)
        {
            ValidarRango(desde, hasta);

            List<BEOrdenDeCompra> ordenes = ordenCompraBLL.ListarTodo()
                .Where(o => EstaEnRango(o.FechaEmision, desde, hasta))
                .ToList();
            List<BEPagoProveedor> pagos = pagoProveedorBLL.ListarTodo();
            BEReporteCompras reporte = new BEReporteCompras();

            foreach (BEOrdenDeCompra orden in ordenes)
            {
                decimal pagado = pagos
                    .Where(p => p.OrdenDeCompra != null && p.OrdenDeCompra.Id == orden.Id)
                    .Sum(p => p.Importe);

                reporte.Detalle.Add(new BEReporteComprasFila
                {
                    Fecha = orden.FechaEmision,
                    OrdenCompra = orden.Numero,
                    Proveedor = orden.Proveedor != null ? orden.Proveedor.RazonSocial : string.Empty,
                    Booking = orden.BookingOrigen != null ? orden.BookingOrigen.Numero : string.Empty,
                    Servicios = orden.Servicios != null
                        ? string.Join(", ", orden.Servicios.Select(s => s.Nombre))
                        : string.Empty,
                    Estado = orden.Estado != null ? orden.Estado.Descripcion : "Sin estado",
                    Total = orden.ImporteTotal,
                    Pagado = pagado,
                    Pendiente = Math.Max(0, orden.ImporteTotal - pagado)
                });
            }

            reporte.Ordenes = reporte.Detalle.Count;
            reporte.TotalOrdenado = reporte.Detalle.Sum(x => x.Total);
            reporte.TotalPagado = reporte.Detalle.Sum(x => x.Pagado);
            reporte.PendientePago = reporte.Detalle.Sum(x => x.Pendiente);
            reporte.Estados = reporte.Detalle
                .GroupBy(x => x.Estado)
                .Select(g => new BEReporteAgrupado { Nombre = g.Key, Valor = g.Count() })
                .OrderBy(x => x.Nombre)
                .ToList();
            reporte.Detalle = reporte.Detalle.OrderByDescending(x => x.Fecha).ToList();

            return reporte;
        }

        #endregion

        #region "Trazabilidad"

        public List<BEBooking> ListarBookings()
        {
            return bookingBLL.ListarTodo().OrderByDescending(b => b.FechaGeneracion).ToList();
        }

        public List<BEReporteTrazabilidadFila> ObtenerTrazabilidad(long bookingId)
        {
            BEBooking booking = bookingBLL.ListarTodo().FirstOrDefault(b => b.Id == bookingId);
            if (booking == null)
                throw new Exception("No se encontró el booking seleccionado.");

            List<BEReporteTrazabilidadFila> filas = new List<BEReporteTrazabilidadFila>();
            BECotizacion cotizacion = booking.Cotizacion;

            AgregarEtapa(filas, 1, "Cotización",
                cotizacion != null ? cotizacion.Numero : string.Empty,
                cotizacion != null && cotizacion.Estado != null ? cotizacion.Estado.Descripcion : "No registrada",
                cotizacion != null ? (DateTime?)cotizacion.FechaEmision : null,
                cotizacion != null ? "Origen comercial del booking" : "El booking no posee cotización reconstruida");

            AgregarEtapa(filas, 2, "Booking", booking.Numero,
                booking.Estado != null ? booking.Estado.Descripcion : "Sin estado",
                booking.FechaGeneracion,
                (booking.Cargas != null ? booking.Cargas.Count : 0) + " carga/s y "
                    + (booking.Servicios != null ? booking.Servicios.Count : 0) + " servicio/s");

            BEAsignacion asignacion = asignacionBLL.ListarTodo()
                .FirstOrDefault(a => a.BookingId == booking.Id);
            AgregarEtapa(filas, 3, "Asignación",
                asignacion != null ? asignacion.Id.ToString() : string.Empty,
                asignacion != null && asignacion.Estado != null
                    ? asignacion.Estado.Descripcion
                    : "Pendiente",
                asignacion != null ? (DateTime?)asignacion.FechaAsignacion : null,
                asignacion != null
                    ? "Buque: " + (asignacion.Buque != null ? asignacion.Buque.Nombre : "-")
                        + " | Contenedor: " + (asignacion.Contenedor != null ? asignacion.Contenedor.Codigo : "-")
                    : "Todavía no se asignaron recursos");

            BEBillofLading bill = billOfLadingBLL.ListarTodo()
                .FirstOrDefault(b => b.Booking != null && b.Booking.Id == booking.Id);
            AgregarEtapa(filas, 4, "Bill of Lading",
                bill != null ? bill.Numero : string.Empty,
                bill != null ? "Emitido" : "Pendiente",
                bill != null ? (DateTime?)bill.FechaEmision : null,
                bill != null ? "Destino: " + bill.PuertoDestino : "Sin documento emitido");

            BEFactura factura = facturaBLL.ListarTodo()
                .FirstOrDefault(f => f.Booking != null && f.Booking.Id == booking.Id);
            AgregarEtapa(filas, 5, "Factura",
                factura != null ? factura.NumeroFactura : string.Empty,
                factura != null && factura.Estado != null ? factura.Estado.Descripcion : "Pendiente",
                factura != null ? (DateTime?)factura.Fecha : null,
                factura != null ? "Total: " + factura.Total.ToString("N2") : "Sin factura emitida");

            List<BEPago> pagosCliente = factura == null
                ? new List<BEPago>()
                : pagoBLL.ListarTodo()
                    .Where(p => p.Factura != null && p.Factura.Id == factura.Id)
                    .ToList();
            AgregarEtapa(filas, 6, "Cobro",
                pagosCliente.Count > 0 ? string.Join(", ", pagosCliente.Select(p => p.Id.ToString())) : string.Empty,
                pagosCliente.Count > 0 ? "Registrado" : "Pendiente",
                pagosCliente.Count > 0 ? (DateTime?)pagosCliente.Max(p => p.FechaPago) : null,
                pagosCliente.Count > 0
                    ? "Cobrado: " + pagosCliente.Sum(p => p.Importe).ToString("N2")
                    : "Sin pagos del cliente");

            List<BEOrdenDeCompra> ordenes = ordenCompraBLL.ListarTodo()
                .Where(o => o.BookingOrigen != null && o.BookingOrigen.Id == booking.Id)
                .ToList();
            AgregarEtapa(filas, 7, "Compras",
                ordenes.Count > 0 ? string.Join(", ", ordenes.Select(o => o.Numero)) : string.Empty,
                ordenes.Count > 0 ? "Generada" : "No requerida",
                ordenes.Count > 0 ? (DateTime?)ordenes.Min(o => o.FechaEmision) : null,
                ordenes.Count > 0
                    ? ordenes.Count + " orden/es de compra"
                    : "El booking no originó órdenes de compra");

            List<long> ordenesIds = ordenes.Select(o => o.Id).ToList();
            List<BEPagoProveedor> pagosProveedor = pagoProveedorBLL.ListarTodo()
                .Where(p => p.OrdenDeCompra != null && ordenesIds.Contains(p.OrdenDeCompra.Id))
                .ToList();
            AgregarEtapa(filas, 8, "Pago a proveedor",
                pagosProveedor.Count > 0
                    ? string.Join(", ", pagosProveedor.Select(p => p.Id.ToString()))
                    : string.Empty,
                pagosProveedor.Count > 0 ? "Registrado" : "Pendiente",
                pagosProveedor.Count > 0 ? (DateTime?)pagosProveedor.Max(p => p.FechaPago) : null,
                pagosProveedor.Count > 0
                    ? "Pagado: " + pagosProveedor.Sum(p => p.Importe).ToString("N2")
                    : "Sin pagos a proveedores");

            return filas;
        }

        #endregion

        #region "Métodos auxiliares"

        private void ValidarRango(DateTime desde, DateTime hasta)
        {
            if (desde.Date > hasta.Date)
                throw new Exception("La fecha desde no puede ser mayor a la fecha hasta.");
        }

        private bool EstaEnRango(DateTime fecha, DateTime desde, DateTime hasta)
        {
            return fecha.Date >= desde.Date && fecha.Date <= hasta.Date;
        }

        private int ObtenerTEU(BEContenedor contenedor)
        {
            string codigo = contenedor.Tipo != null ? contenedor.Tipo.Codigo : string.Empty;
            return codigo.StartsWith("40", StringComparison.OrdinalIgnoreCase) ? 2 : 1;
        }

        private string ObtenerDestino(BEBooking booking)
        {
            if (booking == null || booking.Cargas == null)
                return string.Empty;

            return string.Join(", ", booking.Cargas
                .Where(c => c.Destino != null)
                .Select(c => c.Destino.Puerto)
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Distinct());
        }

        private void AgregarEtapa(List<BEReporteTrazabilidadFila> filas, int orden,
            string etapa, string referencia, string estado, DateTime? fecha, string detalle)
        {
            filas.Add(new BEReporteTrazabilidadFila
            {
                Orden = orden,
                Etapa = etapa,
                Referencia = referencia,
                Estado = estado,
                Fecha = fecha,
                Detalle = detalle
            });
        }

        #endregion
    }
}
