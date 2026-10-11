using Abstraccion;
using BE.Dominio;
using BE.Finanzas;
using BE.Personas;
using BE.Transporte;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Transversales.Documentos
{
    public class GeneradorPdfBasico<T> : IGeneradorDocumento<T>
    {
        #region "Atributos"
        private readonly Func<T, string> obtenerTitulo;
        private readonly Func<T, string> obtenerContenido;

        private readonly Func<T, bool> estaAnulado;
        private readonly Func<T, string> textoMarcaAgua;

        private readonly string rutaLogo;

        // Genérico: el documento decide qué va a la derecha (tipo, nro, fecha, etc.)
        private readonly Func<T, string> obtenerEncabezadoDerecho;
        #endregion

        #region "Constructor"
        public GeneradorPdfBasico(
            Func<T, string> titulo,
            Func<T, string> contenido,
            Func<T, bool> anulacion = null,
            Func<T, string> marcaAgua = null,
            string rutaLogo = null,
            Func<T, string> encabezadoDerecho = null)
        {
            obtenerTitulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
            obtenerContenido = contenido ?? throw new ArgumentNullException(nameof(contenido));

            estaAnulado = anulacion ?? (_ => false);
            textoMarcaAgua = marcaAgua ?? (_ => "ANULADA");

            this.rutaLogo = rutaLogo;

            obtenerEncabezadoDerecho = encabezadoDerecho ?? (_ => "");
        }
        #endregion

        #region "Generación"
        public void GenerarDocumento(string rutaDestino, T entidad)
        {
            if (string.IsNullOrWhiteSpace(rutaDestino))
                throw new ArgumentException("Ruta destino obligatoria.", nameof(rutaDestino));

            var doc = new Document();
            var section = doc.AddSection();

            ConfigurarPagina(section);

            // Logo marca de agua
            if (!string.IsNullOrEmpty(rutaLogo) && File.Exists(rutaLogo))
                AgregarLogoComoFondo(section, rutaLogo);

            // Encabezado corporativo (genérico)
            string headerDer = obtenerEncabezadoDerecho(entidad);
            AgregarEncabezadoEmpresa(section, headerDer);

            // Marca de agua por estado
            if (estaAnulado(entidad))
                AgregarMarcaDeAgua(section, textoMarcaAgua(entidad));

            // Título y cuerpo
            AgregarTitulo(section, obtenerTitulo(entidad));
            AgregarContenidoComoTabla(section, obtenerContenido(entidad));

            var renderer = new PdfDocumentRenderer();
            renderer.Document = doc;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(rutaDestino);
        }
        public void GenerarReporteGestion(
            string rutaDestino,
            string tituloReporte,
            string periodo,
            List<KeyValuePair<string, string>> resumen,
            List<string> columnas,
            List<List<string>> filas)
        {
            if (string.IsNullOrWhiteSpace(rutaDestino))
                throw new ArgumentException("Ruta destino obligatoria.", nameof(rutaDestino));

            var doc = new Document();
            var section = doc.AddSection();

            ConfigurarPaginaReporte(section);
            AgregarEncabezadoReporte(section, tituloReporte, periodo);
            AgregarResumenReporte(section, resumen);
            AgregarTablaReporte(section, columnas, filas);

            var renderer = new PdfDocumentRenderer();
            renderer.Document = doc;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(rutaDestino);
        }
        public void GenerarFactura(string rutaDestino, BEFactura factura)
        {
            if (string.IsNullOrWhiteSpace(rutaDestino))
                throw new ArgumentException("Ruta destino obligatoria.", nameof(rutaDestino));

            if (factura == null)
                throw new ArgumentNullException(nameof(factura));

            var doc = new Document();
            var section = doc.AddSection();

            ConfigurarPaginaFactura(section);
            AgregarEncabezadoFactura(section, factura);
            AgregarClienteFactura(section, factura);
            AgregarDetalleFactura(section, factura);
            AgregarTotalesFactura(section, factura);
            AgregarPieFactura(section, factura);

            if (factura.Estado != null && factura.Estado.Codigo == EstadoFactura.Anulada.Codigo)
                AgregarMarcaDeAgua(section, "ANULADA");

            var renderer = new PdfDocumentRenderer();
            renderer.Document = doc;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(rutaDestino);
        }
        public void GenerarBillOfLading(string rutaDestino, BEBillofLading bill)
        {
            if (string.IsNullOrWhiteSpace(rutaDestino))
                throw new ArgumentException("Ruta destino obligatoria.", nameof(rutaDestino));

            if (bill == null)
                throw new ArgumentNullException(nameof(bill));

            var doc = new Document();
            var section = doc.AddSection();

            ConfigurarPaginaDocumentoTransporte(section);
            AgregarEncabezadoDocumentoTransporte(section, "Bill of Lading", bill.Numero, bill.FechaEmision);
            AgregarDatosBillOfLading(section, bill);
            AgregarDetalleBillOfLading(section, bill);
            AgregarPieDocumentoTransporte(section, "Documento emitido por GlobalTransLines para respaldar la carga transportada.");

            var renderer = new PdfDocumentRenderer();
            renderer.Document = doc;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(rutaDestino);
        }

        public void GenerarManifiesto(string rutaDestino, BEManifiesto manifiesto)
        {
            if (string.IsNullOrWhiteSpace(rutaDestino))
                throw new ArgumentException("Ruta destino obligatoria.", nameof(rutaDestino));

            if (manifiesto == null)
                throw new ArgumentNullException(nameof(manifiesto));

            var doc = new Document();
            var section = doc.AddSection();

            ConfigurarPaginaDocumentoTransporte(section);
            AgregarEncabezadoDocumentoTransporte(section, "Manifiesto de carga", manifiesto.Numero, manifiesto.FechaEmision);
            AgregarDatosManifiesto(section, manifiesto);
            AgregarDetalleManifiesto(section, manifiesto);
            AgregarPieDocumentoTransporte(section, "Documento emitido por GlobalTransLines para informar los contenedores manifestados.");

            var renderer = new PdfDocumentRenderer();
            renderer.Document = doc;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(rutaDestino);
        }
        #endregion

        #region "Configuración de página"
        private void ConfigurarPagina(Section section)
        {
            section.PageSetup.TopMargin = Unit.FromCentimeter(2.5);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(2.5);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(2);
            section.PageSetup.RightMargin = Unit.FromCentimeter(2);
        }
        #endregion

        #region "Logo como fondo"
        private void AgregarLogoComoFondo(Section section, string logoPath)
        {
            var img = section.AddImage(logoPath);

            img.LockAspectRatio = true;
            img.Width = Unit.FromCentimeter(3);

            img.Top = ShapePosition.Top;
            img.Left = ShapePosition.Left;

            img.RelativeVertical = RelativeVertical.Page;
            img.RelativeHorizontal = RelativeHorizontal.Margin;

            img.WrapFormat.Style = WrapStyle.TopBottom;
        }


        #endregion

        #region "Encabezado corporativo (genérico)"
        private void AgregarEncabezadoEmpresa(Section section, string encabezadoDerecho)
        {
            Table tabla = section.AddTable();
            tabla.Borders.Visible = false;

            tabla.AddColumn(Unit.FromCentimeter(7.5)); // mitad izquierda
            tabla.AddColumn(Unit.FromCentimeter(7.5)); // mitad derecha


            Row fila = tabla.AddRow();
            fila.BottomPadding = "0.4cm"; // más aire
            fila.Shading.Color = new Color (245,245,245);

            // Izquierda: empresa
            Paragraph pIzq = fila.Cells[0].AddParagraph();
            pIzq.Format.Font.Size = 9;

            pIzq.AddFormattedText(EmpresaConfig.RazonSocial + "\n", TextFormat.Bold);
            pIzq.AddText("CUIT: " + EmpresaConfig.CUIT + "\n");
            pIzq.AddText(EmpresaConfig.CondicionFiscal.Descripcion + "\n");
            pIzq.AddText(EmpresaConfig.DomicilioFiscal + " – " + EmpresaConfig.Localidad + "\n");
            pIzq.AddText(EmpresaConfig.SitioWeb + " | " + EmpresaConfig.Email);

            // Derecha: bloque configurable
            Paragraph pDer = fila.Cells[1].AddParagraph();
            pDer.Format.Alignment = ParagraphAlignment.Right;
            pDer.Format.Font.Size = 10;
            pDer.Format.Font.Bold = true;

            if (!string.IsNullOrWhiteSpace(encabezadoDerecho))
            {
                string[] lineas = encabezadoDerecho.Split('\n');
                foreach (string l in lineas)
                {
                    string linea = (l ?? "").Trim();
                    if (linea.Length == 0) continue;

                    pDer.AddText(linea);
                    pDer.AddLineBreak();
                }
            }
        }
        #endregion


        #region "Marca de agua"
        private void AgregarMarcaDeAgua(Section section, string texto)
        {
            Paragraph anulado = section.AddParagraph();
            anulado.Format.SpaceBefore = Unit.FromCentimeter(1.0);
            anulado.Format.Alignment = ParagraphAlignment.Center;
            anulado.Format.Font.Size = 36;
            anulado.Format.Font.Bold = true;
            anulado.Format.Font.Color = new Color(180, 180, 180); // gris claro
            anulado.AddText(texto);
        }
        #endregion

        #region "Título"
        private void AgregarTitulo(Section section, string titulo)
        {
            Paragraph p = section.AddParagraph();
            p.Format.SpaceBefore = Unit.FromCentimeter(1); // espacio del logo/encabezado
            p.Format.SpaceAfter = Unit.FromCentimeter(0.8);
            p.Format.Alignment = ParagraphAlignment.Center;
            p.Format.Font.Size = 16;
            p.Format.Font.Bold = true;

            p.AddText(titulo);
        }


        #endregion

        #region "Contenido"
        private void AgregarContenidoComoTabla(Section section, string contenido)
        {
            Table tabla = section.AddTable();
            tabla.Rows.LeftIndent = 0;

            tabla.Borders.Width = 0.25;
            tabla.Borders.Color = Colors.Gray;

            Column c1 = tabla.AddColumn("5.5cm");
            Column c2 = tabla.AddColumn("9.5cm");
            c1.Format.Alignment = ParagraphAlignment.Left;
            c2.Format.Alignment = ParagraphAlignment.Left;

            string[] lineas = contenido.Split('\n');

            foreach (string raw in lineas)
            {
                string linea = (raw ?? "").Trim();
                if (linea.Length == 0) continue;

                // Sección: --- Detalle ---
                if (linea.StartsWith("---") && linea.EndsWith("---"))
                {
                    Row r = tabla.AddRow();
                    r.Cells[0].MergeRight = 1;
                    r.Shading.Color = Colors.LightGray;

                    Paragraph ps = r.Cells[0].AddParagraph(linea.Replace("-", "").Trim());
                    ps.Format.Font.Bold = true;
                    ps.Format.Alignment = ParagraphAlignment.Center;
                    continue;
                }

                // Item: - Algo: Valor
                bool esItem = linea.StartsWith("-");
                if (esItem) linea = linea.Substring(1).Trim();

                string[] partes = linea.Split(new[] { ':' }, 2);
                string k = partes[0].Trim();
                string v = partes.Length > 1 ? partes[1].Trim() : "";

                Row fila = tabla.AddRow();
                fila.TopPadding = 2;
                fila.BottomPadding = 2;
                fila.Shading.Color = Colors.White;

                Paragraph pk = fila.Cells[0].AddParagraph(k);
                pk.Format.Font.Bold = !esItem;

                fila.Cells[1].AddParagraph(v);

                // Totales
                if (k.Equals("Total", StringComparison.OrdinalIgnoreCase))
                {
                    fila.Shading.Color = Colors.LightGray;
                    fila.Cells[0].Format.Font.Bold = true;
                    fila.Cells[1].Format.Font.Bold = true;

                    fila.Cells[0].Format.Font.Size = 11;
                    fila.Cells[1].Format.Font.Size = 11;
                }
                else if (k.Equals("Subtotal", StringComparison.OrdinalIgnoreCase)
                      || k.Equals("Impuesto", StringComparison.OrdinalIgnoreCase))
                {
                    fila.Cells[0].Format.Font.Bold = true;
                }
            }
        }
        #endregion

        #region "Factura"

        private void ConfigurarPaginaFactura(Section section)
        {
            section.PageSetup.TopMargin = Unit.FromCentimeter(1.4);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1.4);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1.6);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1.6);
        }

        private void AgregarEncabezadoFactura(Section section, BEFactura factura)
        {
            AgregarFondoEncabezadoFactura(section);

            Table tabla = section.AddTable();
            tabla.Borders.Width = 0.5;
            tabla.Borders.Color = Colors.Gray;
            tabla.AddColumn(Unit.FromCentimeter(8.2));
            tabla.AddColumn(Unit.FromCentimeter(2.2));
            tabla.AddColumn(Unit.FromCentimeter(7.1));

            Row fila = tabla.AddRow();
            fila.Height = Unit.FromCentimeter(4.8);
            fila.HeightRule = RowHeightRule.Exactly;
            fila.TopPadding = 12;
            fila.BottomPadding = 8;


            Paragraph empresa = fila.Cells[0].AddParagraph();
            empresa.Format.Font.Size = 9;
            empresa.AddFormattedText(EmpresaConfig.RazonSocial + "\n", TextFormat.Bold);
            empresa.AddText(EmpresaConfig.DomicilioFiscal + "\n");
            empresa.AddText(EmpresaConfig.Localidad + ", " + EmpresaConfig.Provincia + "\n");
            empresa.AddText("Tel: " + EmpresaConfig.Telefono + "\n");
            empresa.AddText(EmpresaConfig.Email);

            Paragraph tipo = fila.Cells[1].AddParagraph();
            tipo.Format.Alignment = ParagraphAlignment.Center;
            tipo.Format.Font.Size = 28;
            tipo.Format.Font.Bold = true;
            tipo.AddText(ObtenerLetraFactura(factura));

            Paragraph datos = fila.Cells[2].AddParagraph();
            datos.Format.Font.Size = 9;
            datos.AddFormattedText((factura.Tipo != null ? factura.Tipo.Descripcion : "Factura") + "\n", TextFormat.Bold);
            datos.AddText("Factura Nro: " + ValorFactura(factura.NumeroFactura) + "\n");
            datos.AddText("Fecha: " + factura.Fecha.ToString("dd/MM/yyyy") + "\n");
            datos.AddText("CUIT: " + EmpresaConfig.CUIT + "\n");
            datos.AddText("Ingresos Brutos: " + EmpresaConfig.IngresosBrutos + "\n");
            datos.AddText("Inicio de actividades: " + EmpresaConfig.InicioActividades.ToString("dd/MM/yyyy"));
        }


        private void AgregarFondoEncabezadoFactura(Section section)
        {
            if (string.IsNullOrEmpty(rutaLogo) || !File.Exists(rutaLogo))
                return;

            Image fondo = section.AddImage(rutaLogo);
            fondo.LockAspectRatio = false;
            fondo.Width = Unit.FromCentimeter(17.5);
            fondo.Height = Unit.FromCentimeter(4.8);
            fondo.Left = ShapePosition.Left;
            fondo.Top = Unit.FromCentimeter(1.4);
            fondo.RelativeHorizontal = RelativeHorizontal.Margin;
            fondo.RelativeVertical = RelativeVertical.Page;
            fondo.WrapFormat.Style = WrapStyle.Through;
        }
        private void AgregarClienteFactura(Section section, BEFactura factura)
        {
            BECliente cliente = ObtenerClienteFactura(factura);

            Paragraph titulo = section.AddParagraph("Datos del cliente");
            titulo.Format.SpaceBefore = Unit.FromCentimeter(0.6);
            titulo.Format.SpaceAfter = Unit.FromCentimeter(0.15);
            titulo.Format.Font.Bold = true;

            Table tabla = section.AddTable();
            tabla.Borders.Width = 0.25;
            tabla.Borders.Color = Colors.Gray;
            tabla.AddColumn(Unit.FromCentimeter(4));
            tabla.AddColumn(Unit.FromCentimeter(13.5));

            AgregarFilaFactura(tabla, "Razon social", cliente != null ? cliente.RazonSocial : "-");
            AgregarFilaFactura(tabla, "CUIT", cliente != null ? cliente.Cuit : "-");
            AgregarFilaFactura(tabla, "Condicion fiscal", cliente != null && cliente.CondicionFiscal != null ? cliente.CondicionFiscal.Descripcion : "-");
            AgregarFilaFactura(tabla, "Contacto", ObtenerContactoCliente(cliente));
            AgregarFilaFactura(tabla, "Booking", factura.Booking != null ? factura.Booking.Numero : "-");
        }

        private void AgregarDetalleFactura(Section section, BEFactura factura)
        {
            Paragraph titulo = section.AddParagraph("Detalle facturado");
            titulo.Format.SpaceBefore = Unit.FromCentimeter(0.6);
            titulo.Format.SpaceAfter = Unit.FromCentimeter(0.15);
            titulo.Format.Font.Bold = true;

            Table tabla = section.AddTable();
            tabla.Borders.Width = 0.25;
            tabla.Borders.Color = Colors.Gray;
            tabla.AddColumn(Unit.FromCentimeter(2.2));
            tabla.AddColumn(Unit.FromCentimeter(7.1));
            tabla.AddColumn(Unit.FromCentimeter(2.3));
            tabla.AddColumn(Unit.FromCentimeter(2.4));
            tabla.AddColumn(Unit.FromCentimeter(3.5));

            Row encabezado = tabla.AddRow();
            encabezado.Shading.Color = new Color(222, 235, 247);
            encabezado.Format.Font.Bold = true;
            encabezado.Cells[0].AddParagraph("Codigo");
            encabezado.Cells[1].AddParagraph("Descripcion");
            encabezado.Cells[2].AddParagraph("Peso kg");
            encabezado.Cells[3].AddParagraph("Volumen m3");
            encabezado.Cells[4].AddParagraph("Referencia");

            List<BECarga> cargas = ObtenerCargasFactura(factura);

            if (cargas.Count == 0)
            {
                Row sinDetalle = tabla.AddRow();
                sinDetalle.Cells[0].AddParagraph("-");
                sinDetalle.Cells[1].AddParagraph("Servicio logistico segun booking");
                sinDetalle.Cells[2].AddParagraph("-");
                sinDetalle.Cells[3].AddParagraph("-");
                sinDetalle.Cells[4].AddParagraph(factura.Booking != null ? factura.Booking.Numero : "-");
                return;
            }

            foreach (BECarga carga in cargas)
            {
                Row fila = tabla.AddRow();
                fila.Cells[0].AddParagraph(ValorFactura(carga.Codigo));
                fila.Cells[1].AddParagraph(ValorFactura(carga.Descripcion));
                fila.Cells[2].AddParagraph(carga.PesoKg.ToString("N2"));
                fila.Cells[3].AddParagraph(carga.VolumenM3.ToString("N2"));
                fila.Cells[4].AddParagraph(factura.Booking != null ? factura.Booking.Numero : "-");
            }
        }

        private void AgregarTotalesFactura(Section section, BEFactura factura)
        {
            Table tabla = section.AddTable();
            tabla.Rows.LeftIndent = Unit.FromCentimeter(9.7);
            tabla.Borders.Width = 0.25;
            tabla.Borders.Color = Colors.Gray;
            tabla.AddColumn(Unit.FromCentimeter(4));
            tabla.AddColumn(Unit.FromCentimeter(3.8));

            AgregarFilaImporteFactura(tabla, "Subtotal", factura.Subtotal, false);
            AgregarFilaImporteFactura(tabla, "IVA 21%", factura.Impuesto, false);
            AgregarFilaImporteFactura(tabla, "TOTAL", factura.Total, true);
        }

        private void AgregarPieFactura(Section section, BEFactura factura)
        {
            Paragraph p = section.AddParagraph();
            p.Format.SpaceBefore = Unit.FromCentimeter(0.7);
            p.Format.Font.Size = 8;
            p.AddFormattedText("Observaciones: ", TextFormat.Bold);
            p.AddText("Documento emitido por GlobalTransLines. Estado: " +
                (factura.Estado != null ? factura.Estado.Descripcion : "-") + ".");
        }

        private void AgregarFilaFactura(Table tabla, string etiqueta, string valor)
        {
            Row fila = tabla.AddRow();
            fila.TopPadding = 3;
            fila.BottomPadding = 3;
            fila.Cells[0].Shading.Color = new Color(245, 248, 252);
            fila.Cells[0].Format.Font.Bold = true;
            fila.Cells[0].AddParagraph(etiqueta);
            fila.Cells[1].AddParagraph(ValorFactura(valor));
        }

        private void AgregarFilaImporteFactura(Table tabla, string etiqueta, decimal importe, bool destacar)
        {
            Row fila = tabla.AddRow();
            fila.TopPadding = 4;
            fila.BottomPadding = 4;

            if (destacar)
            {
                fila.Shading.Color = new Color(222, 235, 247);
                fila.Format.Font.Bold = true;
            }

            fila.Cells[0].AddParagraph(etiqueta);
            Paragraph valor = fila.Cells[1].AddParagraph(FormatearImporteFactura(importe));
            valor.Format.Alignment = ParagraphAlignment.Right;
        }

        private BECliente ObtenerClienteFactura(BEFactura factura)
        {
            if (factura.Booking == null)
                return null;

            return factura.Booking.Cliente ?? factura.Booking.Cotizacion?.Cliente;
        }

        private List<BECarga> ObtenerCargasFactura(BEFactura factura)
        {
            if (factura.Booking == null)
                return new List<BECarga>();

            if (factura.Booking.Cargas != null && factura.Booking.Cargas.Count > 0)
                return factura.Booking.Cargas;

            if (factura.Booking.Cotizacion != null &&
                factura.Booking.Cotizacion.Cargas != null &&
                factura.Booking.Cotizacion.Cargas.Count > 0)
                return factura.Booking.Cotizacion.Cargas;

            return new List<BECarga>();
        }

        private string ObtenerContactoCliente(BECliente cliente)
        {
            if (cliente == null)
                return "-";

            List<string> datos = new List<string>();

            if (!string.IsNullOrWhiteSpace(cliente.Telefono))
                datos.Add(cliente.Telefono);

            if (!string.IsNullOrWhiteSpace(cliente.Email))
                datos.Add(cliente.Email);

            return datos.Count > 0 ? string.Join(" | ", datos) : "-";
        }

        private string ObtenerLetraFactura(BEFactura factura)
        {
            string codigo = factura.Tipo != null ? factura.Tipo.Codigo : "FA";
            return codigo.StartsWith("F", StringComparison.OrdinalIgnoreCase) && codigo.Length > 1
                ? codigo.Substring(1, 1)
                : "A";
        }

        private string ValorFactura(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? "-" : texto;
        }

        private string FormatearImporteFactura(decimal importe)
        {
            return importe.ToString("C2", CultureInfo.CurrentCulture);
        }
        #region "Documentos de transporte"

        private void ConfigurarPaginaDocumentoTransporte(Section section)
        {
            section.PageSetup.TopMargin = Unit.FromCentimeter(1.4);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1.4);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1.6);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1.6);
        }

        private void AgregarEncabezadoDocumentoTransporte(Section section, string tituloDocumento, string numeroDocumento, DateTime fechaEmision)
        {
            AgregarFondoEncabezadoDocumentoTransporte(section);

            Table tabla = section.AddTable();
            tabla.Borders.Width = 0.5;
            tabla.Borders.Color = Colors.Gray;
            tabla.AddColumn(Unit.FromCentimeter(8.2));
            tabla.AddColumn(Unit.FromCentimeter(9.3));

            Row fila = tabla.AddRow();
            fila.Height = Unit.FromCentimeter(3.8);
            fila.HeightRule = RowHeightRule.Exactly;
            fila.TopPadding = 12;
            fila.BottomPadding = 8;

            Paragraph empresa = fila.Cells[0].AddParagraph();
            empresa.Format.Font.Size = 9;
            empresa.AddFormattedText(EmpresaConfig.RazonSocial + "\n", TextFormat.Bold);
            empresa.AddText(EmpresaConfig.DomicilioFiscal + "\n");
            empresa.AddText(EmpresaConfig.Localidad + ", " + EmpresaConfig.Provincia + "\n");
            empresa.AddText("Tel: " + EmpresaConfig.Telefono + "\n");
            empresa.AddText(EmpresaConfig.Email);

            Paragraph datos = fila.Cells[1].AddParagraph();
            datos.Format.Alignment = ParagraphAlignment.Right;
            datos.Format.Font.Size = 10;
            datos.AddFormattedText(ValorDocumento(tituloDocumento) + "\n", TextFormat.Bold);
            datos.AddText("Nro: " + ValorDocumento(numeroDocumento) + "\n");
            datos.AddText("Fecha: " + FechaDocumento(fechaEmision) + "\n");
            datos.AddText("CUIT: " + EmpresaConfig.CUIT);
        }

        private void AgregarFondoEncabezadoDocumentoTransporte(Section section)
        {
            if (string.IsNullOrEmpty(rutaLogo) || !File.Exists(rutaLogo))
                return;

            Image fondo = section.AddImage(rutaLogo);
            fondo.LockAspectRatio = false;
            fondo.Width = Unit.FromCentimeter(17.5);
            fondo.Height = Unit.FromCentimeter(3.8);
            fondo.Left = ShapePosition.Left;
            fondo.Top = Unit.FromCentimeter(1.4);
            fondo.RelativeHorizontal = RelativeHorizontal.Margin;
            fondo.RelativeVertical = RelativeVertical.Page;
            fondo.WrapFormat.Style = WrapStyle.Through;
        }

        private void AgregarDatosBillOfLading(Section section, BEBillofLading bill)
        {
            Paragraph titulo = section.AddParagraph("Datos del Bill of Lading");
            titulo.Format.SpaceBefore = Unit.FromCentimeter(0.6);
            titulo.Format.SpaceAfter = Unit.FromCentimeter(0.15);
            titulo.Format.Font.Bold = true;

            Table tabla = CrearTablaDatosDocumento(section);
            AgregarFilaDocumento(tabla, "Booking", bill.Booking != null ? bill.Booking.Numero : "-");
            AgregarFilaDocumento(tabla, "Cliente", bill.Cliente != null ? bill.Cliente.RazonSocial : "-");
            AgregarFilaDocumento(tabla, "Buque", bill.Buque != null ? bill.Buque.Nombre : "-");
            AgregarFilaDocumento(tabla, "Contenedor", bill.Contenedor != null ? bill.Contenedor.Codigo : "-");
            AgregarFilaDocumento(tabla, "Puerto destino", bill.PuertoDestino);
        }

        private void AgregarDetalleBillOfLading(Section section, BEBillofLading bill)
        {
            Paragraph titulo = section.AddParagraph("Detalle de la carga");
            titulo.Format.SpaceBefore = Unit.FromCentimeter(0.6);
            titulo.Format.SpaceAfter = Unit.FromCentimeter(0.15);
            titulo.Format.Font.Bold = true;

            Table tabla = section.AddTable();
            tabla.Borders.Width = 0.25;
            tabla.Borders.Color = Colors.Gray;
            tabla.AddColumn(Unit.FromCentimeter(3));
            tabla.AddColumn(Unit.FromCentimeter(6.5));
            tabla.AddColumn(Unit.FromCentimeter(2.5));
            tabla.AddColumn(Unit.FromCentimeter(2.5));
            tabla.AddColumn(Unit.FromCentimeter(3));

            Row encabezado = tabla.AddRow();
            encabezado.Shading.Color = new Color(222, 235, 247);
            encabezado.Format.Font.Bold = true;
            encabezado.Cells[0].AddParagraph("Codigo");
            encabezado.Cells[1].AddParagraph("Descripcion");
            encabezado.Cells[2].AddParagraph("Peso kg");
            encabezado.Cells[3].AddParagraph("Volumen m3");
            encabezado.Cells[4].AddParagraph("Destino");

            BECarga carga = bill.Carga;
            Row fila = tabla.AddRow();
            fila.Cells[0].AddParagraph(carga != null ? ValorDocumento(carga.Codigo) : "-");
            fila.Cells[1].AddParagraph(carga != null ? ValorDocumento(carga.Descripcion) : "-");
            fila.Cells[2].AddParagraph(carga != null ? carga.PesoKg.ToString("N2") : "-");
            fila.Cells[3].AddParagraph(carga != null ? carga.VolumenM3.ToString("N2") : "-");
            fila.Cells[4].AddParagraph(ValorDocumento(bill.PuertoDestino));
        }

        private void AgregarDatosManifiesto(Section section, BEManifiesto manifiesto)
        {
            Paragraph titulo = section.AddParagraph("Datos del manifiesto");
            titulo.Format.SpaceBefore = Unit.FromCentimeter(0.6);
            titulo.Format.SpaceAfter = Unit.FromCentimeter(0.15);
            titulo.Format.Font.Bold = true;

            Table tabla = CrearTablaDatosDocumento(section);
            AgregarFilaDocumento(tabla, "Buque", manifiesto.Buque != null ? manifiesto.Buque.Nombre : "-");
            AgregarFilaDocumento(tabla, "Autoridad / destino", manifiesto.AutoridadDestino);
            AgregarFilaDocumento(tabla, "Contenedores", manifiesto.Contenedores != null ? manifiesto.Contenedores.Count.ToString() : "0");
            AgregarFilaDocumento(tabla, "Peso total", manifiesto.PesoTotal.ToString("N2") + " kg");
        }

        private void AgregarDetalleManifiesto(Section section, BEManifiesto manifiesto)
        {
            Paragraph titulo = section.AddParagraph("Contenedores manifestados");
            titulo.Format.SpaceBefore = Unit.FromCentimeter(0.6);
            titulo.Format.SpaceAfter = Unit.FromCentimeter(0.15);
            titulo.Format.Font.Bold = true;

            Table tabla = section.AddTable();
            tabla.Borders.Width = 0.25;
            tabla.Borders.Color = Colors.Gray;
            tabla.AddColumn(Unit.FromCentimeter(3.3));
            tabla.AddColumn(Unit.FromCentimeter(2.4));
            tabla.AddColumn(Unit.FromCentimeter(3));
            tabla.AddColumn(Unit.FromCentimeter(3));
            tabla.AddColumn(Unit.FromCentimeter(5.8));

            Row encabezado = tabla.AddRow();
            encabezado.Shading.Color = new Color(222, 235, 247);
            encabezado.Format.Font.Bold = true;
            encabezado.Cells[0].AddParagraph("Contenedor");
            encabezado.Cells[1].AddParagraph("Tipo");
            encabezado.Cells[2].AddParagraph("Peso kg");
            encabezado.Cells[3].AddParagraph("Cargas");
            encabezado.Cells[4].AddParagraph("Detalle");

            if (manifiesto.Contenedores == null || manifiesto.Contenedores.Count == 0)
            {
                Row vacia = tabla.AddRow();
                vacia.Cells[0].AddParagraph("-");
                vacia.Cells[4].AddParagraph("Sin contenedores asociados.");
                return;
            }

            foreach (BEContenedor contenedor in manifiesto.Contenedores)
            {
                List<BECarga> cargas = contenedor.Cargas ?? new List<BECarga>();
                decimal peso = cargas.Sum(c => c.PesoKg);

                Row fila = tabla.AddRow();
                fila.Cells[0].AddParagraph(ValorDocumento(contenedor.Codigo));
                fila.Cells[1].AddParagraph(contenedor.Tipo != null ? ValorDocumento(contenedor.Tipo.Codigo) : "-");
                fila.Cells[2].AddParagraph(peso.ToString("N2"));
                fila.Cells[3].AddParagraph(cargas.Count.ToString());
                fila.Cells[4].AddParagraph(ObtenerDetalleCargasDocumento(cargas));
            }
        }

        private Table CrearTablaDatosDocumento(Section section)
        {
            Table tabla = section.AddTable();
            tabla.Borders.Width = 0.25;
            tabla.Borders.Color = Colors.Gray;
            tabla.AddColumn(Unit.FromCentimeter(4));
            tabla.AddColumn(Unit.FromCentimeter(13.5));
            return tabla;
        }

        private void AgregarFilaDocumento(Table tabla, string etiqueta, string valor)
        {
            Row fila = tabla.AddRow();
            fila.TopPadding = 3;
            fila.BottomPadding = 3;
            fila.Cells[0].Shading.Color = new Color(245, 248, 252);
            fila.Cells[0].Format.Font.Bold = true;
            fila.Cells[0].AddParagraph(etiqueta);
            fila.Cells[1].AddParagraph(ValorDocumento(valor));
        }

        private void AgregarPieDocumentoTransporte(Section section, string texto)
        {
            Paragraph p = section.AddParagraph();
            p.Format.SpaceBefore = Unit.FromCentimeter(0.7);
            p.Format.Font.Size = 8;
            p.AddText(ValorDocumento(texto));
        }

        private string ObtenerDetalleCargasDocumento(List<BECarga> cargas)
        {
            if (cargas == null || cargas.Count == 0)
                return "-";

            return string.Join(" | ", cargas.Select(c => ValorDocumento(c.Descripcion)));
        }

        private string FechaDocumento(DateTime fecha)
        {
            return fecha == DateTime.MinValue ? "-" : fecha.ToString("dd/MM/yyyy");
        }

        private string ValorDocumento(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? "-" : texto;
        }

        #endregion
        #region "Reportes"

        private void ConfigurarPaginaReporte(Section section)
        {
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.TopMargin = Unit.FromCentimeter(1.2);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1.2);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1.4);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1.4);
        }

        private void AgregarEncabezadoReporte(Section section, string tituloReporte, string periodo)
        {
            AgregarFondoEncabezadoReporte(section);

            Table tabla = section.AddTable();
            tabla.Borders.Width = 0.5;
            tabla.Borders.Color = Colors.Gray;
            tabla.AddColumn(Unit.FromCentimeter(11));
            tabla.AddColumn(Unit.FromCentimeter(14.5));

            Row fila = tabla.AddRow();
            fila.Height = Unit.FromCentimeter(3.2);
            fila.HeightRule = RowHeightRule.Exactly;
            fila.TopPadding = 10;
            fila.BottomPadding = 8;

            Paragraph empresa = fila.Cells[0].AddParagraph();
            empresa.Format.Font.Size = 9;
            empresa.AddFormattedText(EmpresaConfig.RazonSocial + "\n", TextFormat.Bold);
            empresa.AddText(EmpresaConfig.Email + "\n");
            empresa.AddText(EmpresaConfig.Telefono);

            Paragraph datos = fila.Cells[1].AddParagraph();
            datos.Format.Alignment = ParagraphAlignment.Right;
            datos.Format.Font.Size = 11;
            datos.AddFormattedText(ValorReporte(tituloReporte) + "\n", TextFormat.Bold);
            datos.AddText("Periodo: " + ValorReporte(periodo) + "\n");
            datos.AddText("Emision: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
        }

        private void AgregarFondoEncabezadoReporte(Section section)
        {
            if (string.IsNullOrEmpty(rutaLogo) || !File.Exists(rutaLogo))
                return;

            Image fondo = section.AddImage(rutaLogo);
            fondo.LockAspectRatio = false;
            fondo.Width = Unit.FromCentimeter(25.5);
            fondo.Height = Unit.FromCentimeter(3.2);
            fondo.Left = ShapePosition.Left;
            fondo.Top = Unit.FromCentimeter(1.2);
            fondo.RelativeHorizontal = RelativeHorizontal.Margin;
            fondo.RelativeVertical = RelativeVertical.Page;
            fondo.WrapFormat.Style = WrapStyle.Through;
        }

        private void AgregarResumenReporte(Section section, List<KeyValuePair<string, string>> resumen)
        {
            if (resumen == null || resumen.Count == 0)
                return;

            Paragraph titulo = section.AddParagraph("Resumen");
            titulo.Format.SpaceBefore = Unit.FromCentimeter(0.5);
            titulo.Format.SpaceAfter = Unit.FromCentimeter(0.15);
            titulo.Format.Font.Bold = true;

            Table tabla = section.AddTable();
            tabla.Borders.Width = 0.25;
            tabla.Borders.Color = Colors.Gray;

            for (int i = 0; i < 4; i++)
                tabla.AddColumn(Unit.FromCentimeter(6.35));

            Row fila = null;
            for (int i = 0; i < resumen.Count; i++)
            {
                if (i % 4 == 0)
                {
                    fila = tabla.AddRow();
                    fila.TopPadding = 5;
                    fila.BottomPadding = 5;
                }

                Paragraph celda = fila.Cells[i % 4].AddParagraph();
                celda.Format.Font.Size = 8;
                celda.AddFormattedText(ValorReporte(resumen[i].Key) + "\n", TextFormat.Bold);
                celda.AddText(ValorReporte(resumen[i].Value));
            }
        }

        private void AgregarTablaReporte(Section section, List<string> columnas, List<List<string>> filas)
        {
            Paragraph titulo = section.AddParagraph("Detalle");
            titulo.Format.SpaceBefore = Unit.FromCentimeter(0.5);
            titulo.Format.SpaceAfter = Unit.FromCentimeter(0.15);
            titulo.Format.Font.Bold = true;

            if (columnas == null || columnas.Count == 0)
            {
                section.AddParagraph("No hay datos para imprimir.");
                return;
            }

            Table tabla = section.AddTable();
            tabla.Borders.Width = 0.25;
            tabla.Borders.Color = Colors.Gray;

            double ancho = 25.5 / columnas.Count;
            foreach (string columna in columnas)
                tabla.AddColumn(Unit.FromCentimeter(ancho));

            Row encabezado = tabla.AddRow();
            encabezado.Shading.Color = Colors.AliceBlue;
            encabezado.Format.Font.Bold = true;
            encabezado.Format.Font.Size = 7;
            encabezado.TopPadding = 3;
            encabezado.BottomPadding = 3;

            for (int i = 0; i < columnas.Count; i++)
                encabezado.Cells[i].AddParagraph(ValorReporte(columnas[i]));

            if (filas != null)
            {
                foreach (List<string> valores in filas)
                {
                    Row fila = tabla.AddRow();
                    fila.Format.Font.Size = 7;
                    fila.TopPadding = 2;
                    fila.BottomPadding = 2;

                    for (int i = 0; i < columnas.Count; i++)
                    {
                        string valor = valores != null && i < valores.Count ? valores[i] : "";
                        fila.Cells[i].AddParagraph(ValorReporte(valor));
                    }
                }
            }
        }

        private string ValorReporte(string texto)
        {
            return string.IsNullOrWhiteSpace(texto) ? "-" : texto;
        }

        #endregion

        #endregion
    }
}
