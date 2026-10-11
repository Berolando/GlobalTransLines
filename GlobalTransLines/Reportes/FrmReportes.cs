using BE.Dominio;
using BE.Reportes;
using BE.Transporte;
using BLL.Reportes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Transversales.Documentos;

namespace GlobalTransLines.UI.Reportes
{
    public partial class FrmReportes : Form
    {
        #region "Atributos y constructor"

        private readonly ReporteGestionBLL reporteBLL = new ReporteGestionBLL();
        private Dictionary<string, bool> ordenGrillas = new Dictionary<string, bool>();

        public FrmReportes()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            InicializarFiltros();
            ConfigurarGrillas();
            CargarSelectores();
            CargarReporteActual();
        }

        private void tabReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated)
                CargarReporteActual();
        }

        #endregion

        #region "Eventos de botones"

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarReporteActual();
        }

        private void btnRestablecer_Click(object sender, EventArgs e)
        {
            InicializarFiltros();
            CargarReporteActual();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarSelectores();
            CargarReporteActual();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView grilla = ObtenerGrillaActual();
                if (grilla == null)
                    throw new Exception("No hay una grilla disponible para imprimir.");

                string nombreReporte = tabReportes.SelectedTab.Text;
                string titulo = "Reporte de gestion - " + nombreReporte;
                string periodo = dtpDesde.Value.ToString("dd/MM/yyyy")
                    + " al " + dtpHasta.Value.ToString("dd/MM/yyyy");

                var generador = new GeneradorPdfBasico<string>(
                    r => titulo,
                    r => string.Empty,
                    rutaLogo: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ReporteEncabezadoFondo.png")
                );

                var rutas = new RutasDocumentos();
                string numeroDocumento = "Reporte_" + nombreReporte.Replace(" ", "_")
                    + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string rutaPdf = rutas.ObtenerRutaPdf(DocumentoTipo.ReporteGestion, numeroDocumento);

                generador.GenerarReporteGestion(
                    rutaPdf,
                    titulo,
                    periodo,
                    ObtenerResumenReporteActual(),
                    ObtenerColumnasReporte(grilla),
                    ObtenerFilasReporte(grilla)
                );

                System.Diagnostics.Process.Start(rutaPdf);
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo generar el PDF del reporte.", ex);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region "Eventos de selección"

        private void cboBuque_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabReportes.SelectedTab == tabTransporte && cboBuque.SelectedValue != null)
                CargarTransporte();
        }

        private void cboBooking_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabReportes.SelectedTab == tabTrazabilidad && cboBooking.SelectedValue != null)
                CargarTrazabilidad();
        }

        #endregion

        #region "Carga principal"

        private void InicializarFiltros()
        {
            DateTime hoy = DateTime.Today;
            dtpDesde.Value = new DateTime(hoy.Year, 1, 1);
            dtpHasta.Value = hoy;
        }

        private void CargarSelectores()
        {
            long? buqueSeleccionado = ObtenerIdSeleccionado(cboBuque);
            List<BEBuque> buques = reporteBLL.ListarBuques();

            cboBuque.DataSource = buques;
            cboBuque.DisplayMember = "Nombre";
            cboBuque.ValueMember = "Id";
            RestaurarSeleccion(cboBuque, buqueSeleccionado);

            long? bookingSeleccionado = ObtenerIdSeleccionado(cboBooking);
            cboBooking.DataSource = reporteBLL.ListarBookings()
                .Select(b => new
                {
                    b.Id,
                    Texto = b.Numero + " | "
                        + (b.Cliente != null ? b.Cliente.RazonSocial : "Sin cliente")
                })
                .ToList();
            cboBooking.DisplayMember = "Texto";
            cboBooking.ValueMember = "Id";
            RestaurarSeleccion(cboBooking, bookingSeleccionado);
        }

        private void CargarReporteActual()
        {
            try
            {
                ValidarFechas();

                if (tabReportes.SelectedTab == tabComercial)
                    CargarComercial();
                else if (tabReportes.SelectedTab == tabTransporte)
                    CargarTransporte();
                else if (tabReportes.SelectedTab == tabFacturacion)
                    CargarFacturacion();
                else if (tabReportes.SelectedTab == tabCompras)
                    CargarCompras();
                else if (tabReportes.SelectedTab == tabTrazabilidad)
                    CargarTrazabilidad();
            }
            catch (Exception ex)
            {
                MostrarError("No se pudo cargar el reporte.", ex);
            }
        }

        private void CargarComercial()
        {
            BEReporteComercial reporte = reporteBLL.ObtenerComercial(
                dtpDesde.Value.Date, dtpHasta.Value.Date);

            lblCotizacionesValor.Text = reporte.Cotizaciones.ToString();
            lblBookingsValor.Text = reporte.BookingsGenerados.ToString();
            lblMontoCotizadoValor.Text = FormatearImporteResumen(reporte.MontoCotizado);
            lblConversionValor.Text = reporte.PorcentajeConversion.ToString("N1") + " %";
            dgvComercial.DataSource = reporte.Detalle
                .OrderByDescending(x => x.Fecha)
                .ToList();

            PrepararGraficoCircular(chartComercial, "Cotizaciones por estado");
            CargarSerieAgrupada(chartComercial, reporte.Estados, "Cotizaciones");
            FormatearColumnasImporte(dgvComercial, "Monto");
            HabilitarOrdenPorColumnas(dgvComercial);
        }

        private void CargarTransporte()
        {
            long? buqueId = ObtenerIdSeleccionado(cboBuque);
            if (!buqueId.HasValue)
                return;

            BEReporteTransporte reporte = reporteBLL.ObtenerTransporte(
                buqueId.Value, dtpDesde.Value.Date, dtpHasta.Value.Date);

            lblTeuTransporte.Text = "TEU: " + reporte.CapacidadOcupadaTEU
                + " ocupados / " + reporte.CapacidadLibreTEU + " disponibles"
                + " (" + reporte.PorcentajeOcupacionTEU.ToString("N1") + " %)";
            lblPesoTransporte.Text = "Peso: " + reporte.PesoCargadoToneladas.ToString("N2")
                + " tn ocupadas / " + reporte.CapacidadLibreToneladas.ToString("N2")
                + " tn disponibles (" + reporte.PorcentajeOcupacionPeso.ToString("N1") + " %)";
            lblDocumentacionTransporte.Text = "Documentación: "
                + reporte.BillsEmitidos + " Bill of Lading | "
                + reporte.ManifiestosEmitidos + " manifiestos";
            txtItinerario.Text = reporte.Itinerario;
            dgvTransporte.DataSource = reporte.Contenedores
                .OrderByDescending(x => x.PesoKg)
                .ToList();

            PrepararGraficoCircular(chartTransporte, "Ocupación del buque en TEU");
            List<BEReporteAgrupado> ocupacion = new List<BEReporteAgrupado>
            {
                new BEReporteAgrupado { Nombre = "Ocupado", Valor = reporte.CapacidadOcupadaTEU },
                new BEReporteAgrupado { Nombre = "Disponible", Valor = reporte.CapacidadLibreTEU }
            };
            CargarSerieAgrupada(chartTransporte, ocupacion, "Capacidad");
            FormatearColumnasImporte(dgvTransporte, "PesoKg");
            HabilitarOrdenPorColumnas(dgvTransporte);
        }

        private void CargarFacturacion()
        {
            BEReporteFacturacion reporte = reporteBLL.ObtenerFacturacion(
                dtpDesde.Value.Date, dtpHasta.Value.Date);

            lblFacturadoValor.Text = FormatearImporteResumen(reporte.TotalFacturado);
            lblCobradoValor.Text = FormatearImporteResumen(reporte.TotalCobrado);
            lblPendienteCobroValor.Text = FormatearImporteResumen(reporte.PendienteCobro);
            dgvFacturacion.DataSource = reporte.Detalle
                .OrderByDescending(x => x.Fecha)
                .ToList();

            PrepararGraficoColumnas(chartFacturacion, "Facturación y cobranza");
            AgregarColumna(chartFacturacion, "Facturado", reporte.TotalFacturado, Color.SteelBlue);
            AgregarColumna(chartFacturacion, "Cobrado", reporte.TotalCobrado, Color.SeaGreen);
            AgregarColumna(chartFacturacion, "Pendiente", reporte.PendienteCobro, Color.DarkOrange);
            FormatearColumnasImporte(dgvFacturacion, "Total", "Cobrado", "Pendiente");
            HabilitarOrdenPorColumnas(dgvFacturacion);
        }

        private void CargarCompras()
        {
            BEReporteCompras reporte = reporteBLL.ObtenerCompras(
                dtpDesde.Value.Date, dtpHasta.Value.Date);

            lblOrdenadoValor.Text = FormatearImporteResumen(reporte.TotalOrdenado);
            lblPagadoProveedorValor.Text = FormatearImporteResumen(reporte.TotalPagado);
            lblPendienteProveedorValor.Text = FormatearImporteResumen(reporte.PendientePago);
            dgvCompras.DataSource = reporte.Detalle
                .OrderByDescending(x => x.Fecha)
                .ToList();

            PrepararGraficoColumnas(chartCompras, "Compras y pagos a proveedores");
            AgregarColumna(chartCompras, "Ordenado", reporte.TotalOrdenado, Color.SteelBlue);
            AgregarColumna(chartCompras, "Pagado", reporte.TotalPagado, Color.SeaGreen);
            AgregarColumna(chartCompras, "Pendiente", reporte.PendientePago, Color.DarkOrange);
            FormatearColumnasImporte(dgvCompras, "Total", "Pagado", "Pendiente");
            HabilitarOrdenPorColumnas(dgvCompras);
        }

        private void CargarTrazabilidad()
        {
            long? bookingId = ObtenerIdSeleccionado(cboBooking);
            if (!bookingId.HasValue)
                return;

            dgvTrazabilidad.DataSource = reporteBLL.ObtenerTrazabilidad(bookingId.Value)
                .Select(x => new
                {
                    x.Orden,
                    x.Etapa,
                    x.Referencia,
                    x.Estado,
                    Fecha = x.Fecha.HasValue ? x.Fecha.Value.ToString("dd/MM/yyyy") : string.Empty,
                    x.Detalle
                })
                .ToList();
            HabilitarOrdenPorColumnas(dgvTrazabilidad);

            if (dgvTrazabilidad.Columns["Orden"] != null)
                dgvTrazabilidad.Columns["Orden"].FillWeight = 35;
            if (dgvTrazabilidad.Columns["Detalle"] != null)
                dgvTrazabilidad.Columns["Detalle"].FillWeight = 220;
        }

        #endregion

        #region "Configuración Visual"

        private void ConfigurarGrillas()
        {
            DataGridView[] grillas =
            {
                dgvComercial, dgvTransporte, dgvFacturacion, dgvCompras, dgvTrazabilidad
            };

            foreach (DataGridView grilla in grillas)
            {
                grilla.BorderStyle = BorderStyle.FixedSingle;
                grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grilla.MultiSelect = false;
                grilla.ReadOnly = true;
                grilla.RowHeadersVisible = false;
                grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                grilla.AllowUserToAddRows = false;
                grilla.AllowUserToDeleteRows = false;
                grilla.EnableHeadersVisualStyles = false;
                grilla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 239, 247);
                grilla.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(35, 45, 55);
                grilla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                grilla.ColumnHeadersHeight = 34;
                grilla.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                grilla.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
                grilla.DefaultCellStyle.SelectionForeColor = Color.White;
                grilla.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
                grilla.RowTemplate.Height = 30;
            }
        }

        private void PrepararGraficoCircular(Chart chart, string titulo)
        {
            PrepararGrafico(chart, titulo);

            if (chart.ChartAreas.Count > 0)
                chart.ChartAreas[0].AxisY.LabelStyle.Format = string.Empty;
        }

        private void PrepararGraficoColumnas(Chart chart, string titulo)
        {
            PrepararGrafico(chart, titulo);
            chart.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            chart.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            chart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gainsboro;
        }

        private void PrepararGrafico(Chart chart, string titulo)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.BackColor = Color.White;

            Title encabezado = new Title(titulo);
            encabezado.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            chart.Titles.Add(encabezado);

            if (chart.ChartAreas.Count > 0)
                chart.ChartAreas[0].BackColor = Color.White;
        }

        private void CargarSerieAgrupada(Chart chart, List<BEReporteAgrupado> datos, string nombre)
        {
            Series serie = new Series(nombre);
            serie.ChartType = SeriesChartType.Doughnut;
            serie.IsValueShownAsLabel = true;
            serie.Label = "#VALX: #VALY";
            serie.LegendText = "#VALX";
            serie["DoughnutRadius"] = "55";
            serie.Palette = ChartColorPalette.BrightPastel;

            foreach (BEReporteAgrupado item in datos.Where(x => x.Valor > 0))
                serie.Points.AddXY(item.Nombre, item.Valor);

            if (serie.Points.Count == 0)
                serie.Points.AddXY("Sin datos", 1);

            chart.Series.Add(serie);
        }

        private void AgregarColumna(Chart chart, string nombre, decimal valor, Color color)
        {
            Series serie = new Series(nombre);
            serie.ChartType = SeriesChartType.Column;
            serie.Color = color;
            serie.IsValueShownAsLabel = true;
            serie.Label = FormatearImporteResumen(valor);
            serie["PointWidth"] = "0.55";
            serie.Points.AddXY("Período", valor);
            serie.ToolTip = nombre + ": " + FormatearImporte(valor);
            chart.Series.Add(serie);
        }

        private void FormatearColumnasImporte(DataGridView grilla, params string[] columnas)
        {
            foreach (string nombre in columnas)
            {
                if (grilla.Columns[nombre] != null)
                {
                    grilla.Columns[nombre].DefaultCellStyle.Format = "N2";
                    grilla.Columns[nombre].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        #endregion

        #region "Impresión"


        private DataGridView ObtenerGrillaActual()
        {
            if (tabReportes.SelectedTab == tabComercial) return dgvComercial;
            if (tabReportes.SelectedTab == tabTransporte) return dgvTransporte;
            if (tabReportes.SelectedTab == tabFacturacion) return dgvFacturacion;
            if (tabReportes.SelectedTab == tabCompras) return dgvCompras;
            if (tabReportes.SelectedTab == tabTrazabilidad) return dgvTrazabilidad;
            return null;
        }
        private List<KeyValuePair<string, string>> ObtenerResumenReporteActual()
        {
            List<KeyValuePair<string, string>> resumen = new List<KeyValuePair<string, string>>();

            if (tabReportes.SelectedTab == tabComercial)
            {
                resumen.Add(new KeyValuePair<string, string>("Cotizaciones emitidas", lblCotizacionesValor.Text));
                resumen.Add(new KeyValuePair<string, string>("Bookings generados", lblBookingsValor.Text));
                resumen.Add(new KeyValuePair<string, string>("Monto cotizado", lblMontoCotizadoValor.Text));
                resumen.Add(new KeyValuePair<string, string>("Conversion a booking", lblConversionValor.Text));
            }
            else if (tabReportes.SelectedTab == tabTransporte)
            {
                resumen.Add(new KeyValuePair<string, string>("Buque", cboBuque.Text));
                resumen.Add(new KeyValuePair<string, string>("Capacidad TEU", lblTeuTransporte.Text));
                resumen.Add(new KeyValuePair<string, string>("Peso", lblPesoTransporte.Text));
                resumen.Add(new KeyValuePair<string, string>("Documentacion", lblDocumentacionTransporte.Text));
            }
            else if (tabReportes.SelectedTab == tabFacturacion)
            {
                resumen.Add(new KeyValuePair<string, string>("Total facturado", lblFacturadoValor.Text));
                resumen.Add(new KeyValuePair<string, string>("Total cobrado", lblCobradoValor.Text));
                resumen.Add(new KeyValuePair<string, string>("Pendiente de cobro", lblPendienteCobroValor.Text));
            }
            else if (tabReportes.SelectedTab == tabCompras)
            {
                resumen.Add(new KeyValuePair<string, string>("Total ordenado", lblOrdenadoValor.Text));
                resumen.Add(new KeyValuePair<string, string>("Pagado a proveedores", lblPagadoProveedorValor.Text));
                resumen.Add(new KeyValuePair<string, string>("Pendiente de pago", lblPendienteProveedorValor.Text));
            }
            else if (tabReportes.SelectedTab == tabTrazabilidad)
            {
                resumen.Add(new KeyValuePair<string, string>("Booking", cboBooking.Text));
                resumen.Add(new KeyValuePair<string, string>("Recorrido", "Cotizacion, booking, documentacion, facturacion, cobranza y compras"));
            }

            return resumen;
        }

        private List<string> ObtenerColumnasReporte(DataGridView grilla)
        {
            return grilla.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .Select(c => c.HeaderText)
                .ToList();
        }

        private List<List<string>> ObtenerFilasReporte(DataGridView grilla)
        {
            List<DataGridViewColumn> columnas = grilla.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            List<List<string>> filas = new List<List<string>>();

            foreach (DataGridViewRow fila in grilla.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                List<string> valores = new List<string>();
                foreach (DataGridViewColumn columna in columnas)
                {
                    object valor = fila.Cells[columna.Index].FormattedValue;
                    valores.Add(valor != null ? valor.ToString() : string.Empty);
                }

                filas.Add(valores);
            }

            return filas;
        }

        #endregion

        #region "Métodos auxiliares"

        private void ValidarFechas()
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
                throw new Exception("La fecha desde no puede ser mayor a la fecha hasta.");
        }

        private long? ObtenerIdSeleccionado(ComboBox combo)
        {
            if (combo.SelectedValue == null)
                return null;

            long id;
            return long.TryParse(combo.SelectedValue.ToString(), out id) ? (long?)id : null;
        }

        private void RestaurarSeleccion(ComboBox combo, long? id)
        {
            if (id.HasValue)
                combo.SelectedValue = id.Value;

            if (combo.SelectedIndex < 0 && combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }

        private string FormatearImporte(decimal importe)
        {
            return importe.ToString("C2", CultureInfo.CurrentCulture);
        }

        private string FormatearImporteResumen(decimal importe)
        {
            decimal absoluto = Math.Abs(importe);

            if (absoluto >= 1000000000m)
                return "$ " + (importe / 1000000000m).ToString("N2", CultureInfo.CurrentCulture) + " B";

            if (absoluto >= 1000000m)
                return "$ " + (importe / 1000000m).ToString("N2", CultureInfo.CurrentCulture) + " M";

            return FormatearImporte(importe);
        }

        private void HabilitarOrdenPorColumnas(DataGridView grilla)
        {
            foreach (DataGridViewColumn columna in grilla.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            grilla.ColumnHeaderMouseClick -= Grilla_ColumnHeaderMouseClick;
            grilla.ColumnHeaderMouseClick += Grilla_ColumnHeaderMouseClick;
        }

        private void Grilla_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView grilla = sender as DataGridView;

            if (grilla == null || e.ColumnIndex < 0)
                return;

            string columna = grilla.Columns[e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrWhiteSpace(columna))
                columna = grilla.Columns[e.ColumnIndex].Name;

            string clave = grilla.Name + "." + columna;
            bool ascendente = !ordenGrillas.ContainsKey(clave) || !ordenGrillas[clave];
            ordenGrillas[clave] = ascendente;

            OrdenarGrilla(grilla, columna, ascendente);
        }

        private void OrdenarGrilla(DataGridView grilla, string columna, bool ascendente)
        {
            System.Collections.IEnumerable datos = grilla.DataSource as System.Collections.IEnumerable;

            if (datos == null || grilla.DataSource is string)
                return;

            var lista = datos.Cast<object>().ToList();

            if (lista.Count == 0)
                return;

            var propiedad = lista[0].GetType().GetProperty(columna);

            if (propiedad == null)
                return;

            lista = ascendente
                ? lista.OrderBy(x => propiedad.GetValue(x, null)).ToList()
                : lista.OrderByDescending(x => propiedad.GetValue(x, null)).ToList();

            grilla.DataSource = null;
            grilla.DataSource = lista;

            foreach (DataGridViewColumn col in grilla.Columns)
                col.SortMode = DataGridViewColumnSortMode.Automatic;
        }

        private void MostrarError(string mensaje, Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;

            MessageBox.Show(mensaje + Environment.NewLine + ex.Message,
                "Reportes por gestión", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}



