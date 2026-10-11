using System.Collections.Generic;
using System.Linq;

namespace BE.Dominio
{
    public class DocumentoTipo
    {
        public string Codigo { get; private set; }
        public string Descripcion { get; private set; }
        private DocumentoTipo(string codigo, string descripcion) { Codigo = codigo; Descripcion = descripcion; }

        public static DocumentoTipo Factura = new DocumentoTipo("FAC", "Factura");
        public static DocumentoTipo Booking = new DocumentoTipo("BKG", "Booking");
        public static DocumentoTipo Cotizacion = new DocumentoTipo("COT", "Cotización");
        public static DocumentoTipo OrdenDeCompra = new DocumentoTipo("OC", "Orden de Compra");
        public static DocumentoTipo PagoCliente = new DocumentoTipo("RC", "Recibo");
        public static DocumentoTipo PagoProveedor = new DocumentoTipo("RP", "Recibo Proveedor");
        public static DocumentoTipo BillOfLading = new DocumentoTipo("BL", "Bill of Lading");
        public static DocumentoTipo Manifiesto = new DocumentoTipo("MAN", "Manifiesto");
        public static DocumentoTipo ReporteGestion = new DocumentoTipo("REP", "Reporte de Gestion");

        public static IEnumerable<DocumentoTipo> ObtenerTodos()
        {
            return new[] { Factura, Booking, Cotizacion, OrdenDeCompra, PagoCliente, PagoProveedor, BillOfLading, Manifiesto, ReporteGestion };
        }

        public static DocumentoTipo ObtenerPorCodigo(string codigo)
        {
            return ObtenerTodos().FirstOrDefault(x => x.Codigo == codigo);
        }

        public override string ToString() { return Descripcion; }
    }
}
