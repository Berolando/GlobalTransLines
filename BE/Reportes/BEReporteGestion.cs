using System;
using System.Collections.Generic;

namespace BE.Reportes
{
    public class BEReporteComercial
    {
        public int Cotizaciones { get; set; }
        public int BookingsGenerados { get; set; }
        public decimal MontoCotizado { get; set; }
        public decimal PorcentajeConversion { get; set; }
        public List<BEReporteComercialFila> Detalle { get; set; } = new List<BEReporteComercialFila>();
        public List<BEReporteAgrupado> Estados { get; set; } = new List<BEReporteAgrupado>();
    }

    public class BEReporteComercialFila
    {
        public DateTime Fecha { get; set; }
        public string Cotizacion { get; set; }
        public string Cliente { get; set; }
        public string Estado { get; set; }
        public decimal Monto { get; set; }
        public string Booking { get; set; }
    }

    public class BEReporteTransporte
    {
        public long BuqueId { get; set; }
        public string Buque { get; set; }
        public int CapacidadTotalTEU { get; set; }
        public int CapacidadOcupadaTEU { get; set; }
        public int CapacidadLibreTEU { get; set; }
        public decimal CapacidadTotalToneladas { get; set; }
        public decimal PesoCargadoToneladas { get; set; }
        public decimal CapacidadLibreToneladas { get; set; }
        public decimal PorcentajeOcupacionTEU { get; set; }
        public decimal PorcentajeOcupacionPeso { get; set; }
        public int BillsEmitidos { get; set; }
        public int ManifiestosEmitidos { get; set; }
        public string Itinerario { get; set; }
        public List<BEReporteTransporteFila> Contenedores { get; set; } = new List<BEReporteTransporteFila>();
    }

    public class BEReporteTransporteFila
    {
        public string Contenedor { get; set; }
        public string Tipo { get; set; }
        public int TEU { get; set; }
        public decimal PesoKg { get; set; }
        public string Booking { get; set; }
        public string Cliente { get; set; }
        public string Destino { get; set; }
        public string Estado { get; set; }
    }

    public class BEReporteFacturacion
    {
        public decimal TotalFacturado { get; set; }
        public decimal TotalCobrado { get; set; }
        public decimal PendienteCobro { get; set; }
        public int Facturas { get; set; }
        public List<BEReporteFacturacionFila> Detalle { get; set; } = new List<BEReporteFacturacionFila>();
        public List<BEReporteAgrupado> Estados { get; set; } = new List<BEReporteAgrupado>();
    }

    public class BEReporteFacturacionFila
    {
        public DateTime Fecha { get; set; }
        public string Factura { get; set; }
        public string Booking { get; set; }
        public string Cliente { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
        public decimal Cobrado { get; set; }
        public decimal Pendiente { get; set; }
    }

    public class BEReporteCompras
    {
        public decimal TotalOrdenado { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal PendientePago { get; set; }
        public int Ordenes { get; set; }
        public List<BEReporteComprasFila> Detalle { get; set; } = new List<BEReporteComprasFila>();
        public List<BEReporteAgrupado> Estados { get; set; } = new List<BEReporteAgrupado>();
    }

    public class BEReporteComprasFila
    {
        public DateTime Fecha { get; set; }
        public string OrdenCompra { get; set; }
        public string Proveedor { get; set; }
        public string Booking { get; set; }
        public string Servicios { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
        public decimal Pagado { get; set; }
        public decimal Pendiente { get; set; }
    }

    public class BEReporteTrazabilidadFila
    {
        public int Orden { get; set; }
        public string Etapa { get; set; }
        public string Referencia { get; set; }
        public string Estado { get; set; }
        public DateTime? Fecha { get; set; }
        public string Detalle { get; set; }
    }

    public class BEReporteAgrupado
    {
        public string Nombre { get; set; }
        public decimal Valor { get; set; }
    }
}
