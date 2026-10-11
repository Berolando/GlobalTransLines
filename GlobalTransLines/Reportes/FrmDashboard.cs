using BE.Dominio;
using BE.Finanzas;
using BE.Transporte;
using BLL.Finanzas;
using BLL.Transporte;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GlobalTransLines.UI.Reportes
{
    public partial class FrmDashboard : Form
    {
        #region "Atributos y Constructor"

        private readonly FacturaBLL facturaBLL = new FacturaBLL();
        private readonly PagoBLL pagoBLL = new PagoBLL();
        private readonly PagoProveedorBLL pagoProveedorBLL = new PagoProveedorBLL();
        private readonly OrdenCompraBLL ordenCompraBLL = new OrdenCompraBLL();
        private readonly BookingBLL bookingBLL = new BookingBLL();
        private readonly CotizacionBLL cotizacionBLL = new CotizacionBLL();
        private readonly AsignacionBLL asignacionBLL = new AsignacionBLL();
        private readonly BuqueBLL buqueBLL = new BuqueBLL();
        private readonly BillOfLadingBLL billOfLadingBLL = new BillOfLadingBLL();
        private readonly ManifiestoBLL manifiestoBLL = new ManifiestoBLL();

        private List<BEFactura> facturas = new List<BEFactura>();
        private List<BEPago> pagos = new List<BEPago>();
        private List<BEPagoProveedor> pagosProveedor = new List<BEPagoProveedor>();
        private List<BEOrdenDeCompra> ordenesCompra = new List<BEOrdenDeCompra>();
        private List<BEBooking> bookings = new List<BEBooking>();
        private List<BECotizacion> cotizaciones = new List<BECotizacion>();
        private List<BEAsignacion> asignaciones = new List<BEAsignacion>();
        private List<BEBuque> buques = new List<BEBuque>();
        private List<BEBillofLading> billsOfLading = new List<BEBillofLading>();
        private List<BEManifiesto> manifiestos = new List<BEManifiesto>();
        private Chart chartOcupacionBuques;
        private bool dashboardCargado;
        private Dictionary<string, bool> ordenGrillas = new Dictionary<string, bool>();

        public FrmDashboard()
        {
            InitializeComponent();
            ConfigurarCards();
            ConfigurarGrillas();
            ConfigurarVistaOperaciones();
            Activated += FrmDashboard_Activated;
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            InicializarFiltros();
            CargarDashboard();
            dashboardCargado = true;
        }

        private void FrmDashboard_Activated(object sender, EventArgs e)
        {
            if (dashboardCargado)
                CargarDashboard();
        }

        #endregion

        #region "Eventos de botones"

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarDashboard();
        }

        private void btnRestablecer_Click(object sender, EventArgs e)
        {
            InicializarFiltros();
            CargarDashboard();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarDashboard();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region "Configuración Visual"

        private void ConfigurarCards()
        {
            ConfigurarCard(cardFacturado, lblFacturadoTitulo, lblFacturadoValor, "Total facturado");
            ConfigurarCard(cardCobrado, lblCobradoTitulo, lblCobradoValor, "Total cobrado");
            ConfigurarCard(cardPendiente, lblPendienteTitulo, lblPendienteValor, "Pendiente de cobro");
            ConfigurarCard(cardFacturas, lblFacturasTitulo, lblFacturasValor, "Facturas");
            ConfigurarCard(cardBookings, lblBookingsTitulo, lblBookingsValor, "Bookings");
            ConfigurarCard(cardCotizaciones, lblCotizacionesTitulo, lblCotizacionesValor, "Cotizaciones");
        }

        private void ConfigurarCard(Panel panel, Label titulo, Label valor, string textoTitulo)
        {
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Dock = DockStyle.Fill;
            panel.Margin = new Padding(6);

            titulo.Dock = DockStyle.Top;
            titulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            titulo.ForeColor = Color.DimGray;
            titulo.Height = 32;
            titulo.Text = textoTitulo;
            titulo.TextAlign = ContentAlignment.MiddleCenter;

            valor.Dock = DockStyle.Fill;
            valor.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            valor.Text = "0";
            valor.TextAlign = ContentAlignment.MiddleCenter;

            panel.Controls.Clear();
            panel.Controls.Add(valor);
            panel.Controls.Add(titulo);
        }

        private void ConfigurarGrillas()
        {
            DataGridView[] grillas =
            {
                dgvFacturas,
                dgvOperaciones,
                dgvAlertas
            };

            foreach (DataGridView grilla in grillas)
            {
                grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grilla.MultiSelect = false;
                grilla.ReadOnly = true;
                grilla.RowHeadersVisible = false;
                grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                grilla.AllowUserToAddRows = false;
                grilla.AllowUserToDeleteRows = false;
                grilla.RowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
                grilla.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 250, 240);
                grilla.ColumnHeaderMouseClick -= Grilla_ColumnHeaderMouseClick;
                grilla.ColumnHeaderMouseClick += Grilla_ColumnHeaderMouseClick;
            }
        }

        private void ConfigurarVistaOperaciones()
        {
            chartOcupacionBuques = new Chart();
            chartOcupacionBuques.ChartAreas.Add(new ChartArea("ChartArea1"));
            chartOcupacionBuques.Legends.Add(new Legend("Legend1"));
            chartOcupacionBuques.Dock = DockStyle.Fill;
            chartOcupacionBuques.Name = "chartOcupacionBuques";

            layoutOperaciones.SuspendLayout();
            layoutOperaciones.Controls.Clear();
            layoutOperaciones.ColumnStyles.Clear();
            layoutOperaciones.RowStyles.Clear();

            layoutOperaciones.ColumnCount = 2;
            layoutOperaciones.RowCount = 2;
            layoutOperaciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutOperaciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            layoutOperaciones.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            layoutOperaciones.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));

            layoutOperaciones.Controls.Add(chartBookingsEstado, 0, 0);
            layoutOperaciones.Controls.Add(chartOcupacionBuques, 1, 0);
            layoutOperaciones.Controls.Add(dgvOperaciones, 0, 1);
            layoutOperaciones.SetColumnSpan(dgvOperaciones, 2);
            layoutOperaciones.ResumeLayout();
        }

        private void PrepararChart(Chart chart, string titulo)
        {
            chart.Series.Clear();
            chart.Titles.Clear();

            Title title = new Title();
            title.Text = titulo;
            title.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(45, 45, 45);
            chart.Titles.Add(title);

            chart.BackColor = Color.White;
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BorderlineColor = Color.Gainsboro;

            if (chart.Legends.Count > 0)
            {
                chart.Legends[0].Docking = Docking.Right;
                chart.Legends[0].Font = new Font("Segoe UI", 9F);
                chart.Legends[0].BackColor = Color.Transparent;
            }

            if (chart.ChartAreas.Count > 0)
            {
                ChartArea area = chart.ChartAreas[0];

                area.BackColor = Color.White;
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
                area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

                area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
                area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F);

                area.AxisX.LineColor = Color.Gray;
                area.AxisY.LineColor = Color.Gray;

                area.AxisY.LabelStyle.Format = "$ #,##0";
            }
        }

        private Series CrearSerie(string nombre, SeriesChartType tipo, Color color)
        {
            Series serie = new Series(nombre);
            serie.ChartType = tipo;
            serie.Color = color;
            serie.BorderWidth = 3;
            serie.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            serie.IsValueShownAsLabel = tipo == SeriesChartType.Pie || tipo == SeriesChartType.Doughnut;

            if (tipo == SeriesChartType.Line)
            {
                serie.MarkerStyle = MarkerStyle.Circle;
                serie.MarkerSize = 8;
                serie.BorderWidth = 3;
            }

            if (tipo == SeriesChartType.Column)
            {
                serie["PointWidth"] = "0.55";
                serie.LabelForeColor = Color.Black;
            }

            if (tipo == SeriesChartType.Pie || tipo == SeriesChartType.Doughnut)
            {
                serie["PieLabelStyle"] = "Outside";
                serie["DoughnutRadius"] = "55";
                serie.Label = "#VALX\n#PERCENT{P0}";
                serie.LegendText = "#VALX";
            }

            return serie;
        }

        #endregion

        #region "Orden de grillas"

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

        #endregion

        #region "Carga principal del dashboard"

        private void InicializarFiltros()
        {
            DateTime hoy = DateTime.Today;
            dtpDesde.Value = new DateTime(hoy.Year, 1, 1);
            dtpHasta.Value = hoy;
        }

        private void CargarDashboard()
        {
            try
            {
                DateTime desde = dtpDesde.Value.Date;
                DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddTicks(-1);

                if (desde > hasta)
                {
                    MessageBox.Show("La fecha desde no puede ser mayor a la fecha hasta.",
                        "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CargarDatos();

                List<BEFactura> facturasFiltradas = facturas
                    .Where(f => f.Fecha >= desde && f.Fecha <= hasta)
                    .ToList();

                List<BEPago> pagosFiltrados = pagos
                    .Where(p => p.FechaPago >= desde && p.FechaPago <= hasta)
                    .ToList();

                List<BEPagoProveedor> pagosProveedorFiltrados = pagosProveedor
                    .Where(p => p.FechaPago >= desde && p.FechaPago <= hasta)
                    .ToList();

                List<BEOrdenDeCompra> ordenesCompraFiltradas = ordenesCompra
                    .Where(oc => oc.FechaEmision >= desde && oc.FechaEmision <= hasta)
                    .ToList();

                List<BEBooking> bookingsFiltrados = bookings
                    .Where(b => b.FechaGeneracion >= desde && b.FechaGeneracion <= hasta)
                    .ToList();

                List<BECotizacion> cotizacionesFiltradas = cotizaciones
                    .Where(c => c.FechaEmision >= desde && c.FechaEmision <= hasta)
                    .ToList();

                CargarResumen(facturasFiltradas, pagosFiltrados, bookingsFiltrados, cotizacionesFiltradas);
                CargarGraficos(facturasFiltradas, pagosFiltrados, pagosProveedorFiltrados, bookingsFiltrados);
                CargarGrillas(facturasFiltradas, pagosFiltrados, pagosProveedorFiltrados,
                    ordenesCompraFiltradas, bookingsFiltrados, cotizacionesFiltradas);

                lblUltimaActualizacion.Text = "Última actualización: "
                    + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el dashboard. " + ObtenerMensaje(ex),
                    "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatos()
        {
            facturas = facturaBLL.ListarTodo();
            pagos = pagoBLL.ListarTodo();
            pagosProveedor = pagoProveedorBLL.ListarTodo();
            ordenesCompra = ordenCompraBLL.ListarTodo();
            bookings = bookingBLL.ListarTodo();
            cotizaciones = cotizacionBLL.ListarTodo();
            asignaciones = asignacionBLL.ListarTodo();
            buques = buqueBLL.ListarTodo();
            billsOfLading = billOfLadingBLL.ListarTodo();
            manifiestos = manifiestoBLL.ListarTodo();
        }

        #endregion

        #region "Resumen"

        private void CargarResumen(
            List<BEFactura> facturasFiltradas,
            List<BEPago> pagosFiltrados,
            List<BEBooking> bookingsFiltrados,
            List<BECotizacion> cotizacionesFiltradas)
        {
            decimal totalFacturado = CalcularTotalFacturado(facturasFiltradas);
            decimal totalCobrado = CalcularTotalCobrado(pagosFiltrados);

            decimal pendienteCobro = facturasFiltradas
                .Where(f => EsEstado(f.Estado, EstadoFactura.Emitida.Codigo)
                    || EsEstado(f.Estado, EstadoFactura.Pendiente.Codigo))
                .Sum(f => f.Total);

            int facturasVigentes = facturasFiltradas
                .Count(f => !EsEstado(f.Estado, EstadoFactura.Anulada.Codigo));

            int bookingsActivos = bookingsFiltrados
                .Count(b => b.Estado != null);

            int cotizacionesActivas = cotizacionesFiltradas
                .Count(c => c.Estado != null);

            lblFacturadoValor.Text = FormatearImporte(totalFacturado);
            lblCobradoValor.Text = FormatearImporte(totalCobrado);
            lblPendienteValor.Text = FormatearImporte(pendienteCobro);
            lblFacturasValor.Text = facturasVigentes.ToString();
            lblBookingsValor.Text = bookingsActivos.ToString();
            lblCotizacionesValor.Text = cotizacionesActivas.ToString();
        }

        #endregion

        #region "Gráficos"

        private void CargarGraficos(
            List<BEFactura> facturasFiltradas,
            List<BEPago> pagosFiltrados,
            List<BEPagoProveedor> pagosProveedorFiltrados,
            List<BEBooking> bookingsFiltrados)
        {
            CargarGraficoFacturadoCobrado(facturasFiltradas, pagosFiltrados);
            CargarGraficoFacturasPorEstado(facturasFiltradas);
            CargarGraficoResultadoFinanciero(facturasFiltradas, pagosFiltrados, pagosProveedorFiltrados);
            CargarGraficoBookingsPorCliente(bookingsFiltrados);
            CargarGraficoOcupacionBuques();
        }

        private void CargarGraficoFacturadoCobrado(
            List<BEFactura> facturasFiltradas,
            List<BEPago> pagosFiltrados)
        {
            PrepararChart(chartFacturadoCobrado, "Facturación y cobranza");

            Series serieFacturado = CrearSerie("Facturado", SeriesChartType.Column, Color.SteelBlue);
            Series serieCobrado = CrearSerie("Cobrado", SeriesChartType.Column, Color.SeaGreen);

            decimal facturado = CalcularTotalFacturado(facturasFiltradas);
            decimal cobrado = CalcularTotalCobrado(pagosFiltrados);

            serieFacturado.Points.AddXY("Periodo", facturado);
            serieCobrado.Points.AddXY("Periodo", cobrado);

            serieFacturado.ToolTip = "Facturado: #VALY{C0}";
            serieCobrado.ToolTip = "Cobrado: #VALY{C0}";

            chartFacturadoCobrado.Series.Add(serieFacturado);
            chartFacturadoCobrado.Series.Add(serieCobrado);
        }

        private void CargarGraficoFacturasPorEstado(List<BEFactura> facturasFiltradas)
        {
            PrepararChart(chartFacturasEstado, "Facturas por estado");

            Series serie = CrearSerie("Facturas", SeriesChartType.Doughnut, Color.SteelBlue);

            var grupos = facturasFiltradas
                .GroupBy(f => f.Estado != null ? f.Estado.Descripcion : "Sin estado")
                .Select(g => new { Estado = g.Key, Cantidad = g.Count() })
                .OrderBy(x => x.Estado)
                .ToList();

            if (grupos.Count == 0)
            {
                serie.Points.AddXY("Sin datos", 0);
            }
            else
            {
                foreach (var item in grupos)
                {
                    DataPoint punto = new DataPoint();
                    punto.SetValueXY(item.Estado, item.Cantidad);
                    punto.ToolTip = item.Estado + ": " + item.Cantidad.ToString() + " factura/s";
                    serie.Points.Add(punto);
                }
            }

            chartFacturasEstado.Series.Add(serie);
        }

        private void CargarGraficoResultadoFinanciero(
            List<BEFactura> facturasFiltradas,
            List<BEPago> pagosFiltrados,
            List<BEPagoProveedor> pagosProveedorFiltrados)
        {
            PrepararChart(chartFacturadoMes, "Resultado financiero estimado");

            Series serieFacturado = CrearSerie("Facturado", SeriesChartType.Column, Color.SteelBlue);
            Series serieCobrado = CrearSerie("Cobrado", SeriesChartType.Column, Color.SeaGreen);
            Series serieCostoProveedor = CrearSerie("Costos proveedor", SeriesChartType.Column, Color.DarkOrange);
            Series serieGanancia = CrearSerie("Ganancia estimada", SeriesChartType.Column, Color.MediumPurple);

            decimal totalFacturado = CalcularTotalFacturado(facturasFiltradas);
            decimal totalCobrado = CalcularTotalCobrado(pagosFiltrados);
            decimal totalCostoProveedor = CalcularTotalCostoProveedor(pagosProveedorFiltrados);
            decimal gananciaEstimada = totalCobrado - totalCostoProveedor;

            serieFacturado.Points.AddXY("Periodo", totalFacturado);
            serieCobrado.Points.AddXY("Periodo", totalCobrado);
            serieCostoProveedor.Points.AddXY("Periodo", totalCostoProveedor);
            serieGanancia.Points.AddXY("Periodo", gananciaEstimada);

            serieFacturado.ToolTip = "Facturado: #VALY{C0}";
            serieCobrado.ToolTip = "Cobrado: #VALY{C0}";
            serieCostoProveedor.ToolTip = "Costos proveedor: #VALY{C0}";
            serieGanancia.ToolTip = "Ganancia estimada: #VALY{C0}";

            chartFacturadoMes.Series.Add(serieFacturado);
            chartFacturadoMes.Series.Add(serieCobrado);
            chartFacturadoMes.Series.Add(serieCostoProveedor);
            chartFacturadoMes.Series.Add(serieGanancia);
        }

        private void CargarGraficoBookingsPorCliente(List<BEBooking> bookingsFiltrados)
        {
            PrepararChart(chartBookingsEstado, "Bookings por cliente");

            Series serie = CrearSerie("Bookings", SeriesChartType.Bar, Color.SteelBlue);
            serie.IsValueShownAsLabel = true;

            var grupos = bookingsFiltrados
                .GroupBy(b => b.Cliente != null ? b.Cliente.RazonSocial : "Sin cliente")
                .Select(g => new { Cliente = g.Key, Cantidad = g.Count() })
                .OrderByDescending(x => x.Cantidad)
                .ThenBy(x => x.Cliente)
                .Take(8)
                .ToList();

            if (grupos.Count == 0)
            {
                serie.Points.AddXY("Sin datos", 0);
            }
            else
            {
                foreach (var item in grupos)
                {
                    DataPoint punto = new DataPoint();
                    punto.SetValueXY(item.Cliente, item.Cantidad);
                    punto.Label = item.Cantidad.ToString();
                    punto.ToolTip = item.Cliente + ": " + item.Cantidad.ToString() + " booking/s";
                    serie.Points.Add(punto);
                }
            }

            chartBookingsEstado.ChartAreas[0].AxisY.LabelStyle.Format = "0";
            chartBookingsEstado.Series.Add(serie);
        }

        private void CargarGraficoOcupacionBuques()
        {
            PrepararChart(chartOcupacionBuques, "Ocupación por buque");

            Series serie = CrearSerie("Ocupación TEU", SeriesChartType.Bar, Color.SeaGreen);
            serie.IsValueShownAsLabel = true;
            serie.Label = "#VALY{N2}%";

            foreach (BEBuque buque in buques.OrderBy(b => b.Nombre))
            {
                decimal porcentaje = buqueBLL.CalcularPorcentajeOcupado(buque.Id);
                DataPoint punto = new DataPoint();
                punto.SetValueXY(buque.Nombre, porcentaje);
                punto.ToolTip = buque.Nombre + ": " + porcentaje.ToString("N2") + "% ocupado";
                serie.Points.Add(punto);
            }

            if (serie.Points.Count == 0)
                serie.Points.AddXY("Sin datos", 0);

            chartOcupacionBuques.ChartAreas[0].AxisY.LabelStyle.Format = "0'%'";
            chartOcupacionBuques.ChartAreas[0].AxisY.Maximum = 100;
            chartOcupacionBuques.Series.Add(serie);
        }

        #endregion

        #region "Configuración Visual"

        private void CargarGrillas(
            List<BEFactura> facturasFiltradas,
            List<BEPago> pagosFiltrados,
            List<BEPagoProveedor> pagosProveedorFiltrados,
            List<BEOrdenDeCompra> ordenesCompraFiltradas,
            List<BEBooking> bookingsFiltrados,
            List<BECotizacion> cotizacionesFiltradas)
        {
            dgvFacturas.DataSource = ArmarResumenFinanciero(
                facturasFiltradas,
                pagosFiltrados,
                pagosProveedorFiltrados);

            dgvOperaciones.DataSource = ArmarOperacionesPendientes(bookingsFiltrados);

            dgvAlertas.DataSource = ArmarAlertas(
                facturasFiltradas,
                ordenesCompraFiltradas,
                bookingsFiltrados,
                cotizacionesFiltradas);
        }

        private List<ResumenFinancieroDashboard> ArmarResumenFinanciero(
            List<BEFactura> facturasFiltradas,
            List<BEPago> pagosFiltrados,
            List<BEPagoProveedor> pagosProveedorFiltrados)
        {
            decimal totalFacturado = CalcularTotalFacturado(facturasFiltradas);
            decimal totalCobrado = CalcularTotalCobrado(pagosFiltrados);
            decimal totalCostoProveedor = CalcularTotalCostoProveedor(pagosProveedorFiltrados);
            decimal gananciaEstimada = totalCobrado - totalCostoProveedor;
            decimal margenSobreCobrado = 0;

            if (totalCobrado > 0)
            {
                margenSobreCobrado = (gananciaEstimada / totalCobrado) * 100;
            }

            List<ResumenFinancieroDashboard> resumen = new List<ResumenFinancieroDashboard>();

            resumen.Add(new ResumenFinancieroDashboard
            {
                Concepto = "Total facturado a clientes",
                Importe = FormatearImporte(totalFacturado),
                Observacion = "Facturas emitidas no anuladas"
            });

            resumen.Add(new ResumenFinancieroDashboard
            {
                Concepto = "Total cobrado a clientes",
                Importe = FormatearImporte(totalCobrado),
                Observacion = "Pagos de clientes registrados como pagados"
            });

            resumen.Add(new ResumenFinancieroDashboard
            {
                Concepto = "Total pagado a proveedores",
                Importe = FormatearImporte(totalCostoProveedor),
                Observacion = "Pagos a proveedor registrados en Gestión 4"
            });

            resumen.Add(new ResumenFinancieroDashboard
            {
                Concepto = "Ganancia estimada",
                Importe = FormatearImporte(gananciaEstimada),
                Observacion = "Cobrado a clientes menos pagado a proveedores"
            });

            resumen.Add(new ResumenFinancieroDashboard
            {
                Concepto = "Margen estimado sobre cobrado",
                Importe = margenSobreCobrado.ToString("N2") + " %",
                Observacion = "Ganancia estimada / total cobrado"
            });

            return resumen;
        }

        private List<OperacionDashboard> ArmarOperacionesPendientes(List<BEBooking> bookingsFiltrados)
        {
            List<OperacionDashboard> operaciones = new List<OperacionDashboard>();

            foreach (BEBooking booking in bookingsFiltrados.OrderByDescending(b => b.FechaGeneracion))
            {
                BEAsignacion asignacion = asignaciones
                    .FirstOrDefault(a => a.BookingId == booking.Id);

                BEBillofLading bill = billsOfLading
                    .FirstOrDefault(b => b.Booking != null && b.Booking.Id == booking.Id);

                BEFactura factura = facturas
                    .FirstOrDefault(f => f.Booking != null && f.Booking.Id == booking.Id
                        && !EsEstado(f.Estado, EstadoFactura.Anulada.Codigo));

                bool tieneManifiesto = asignacion != null && manifiestos.Any(m =>
                    m.Contenedores != null &&
                    m.Contenedores.Any(c => c.Id == asignacion.ContenedorId));

                string etapaPendiente = ObtenerEtapaPendiente(booking, asignacion, bill, factura, tieneManifiesto);

                operaciones.Add(new OperacionDashboard
                {
                    Fecha = booking.FechaGeneracion.ToString("dd/MM/yyyy"),
                    Booking = booking.Numero,
                    Cliente = booking.Cliente != null ? booking.Cliente.RazonSocial : string.Empty,
                    Estado = booking.Estado != null ? booking.Estado.Descripcion : string.Empty,
                    EtapaPendiente = etapaPendiente,
                    Buque = asignacion != null && asignacion.Buque != null ? asignacion.Buque.Nombre : string.Empty,
                    Contenedor = asignacion != null && asignacion.Contenedor != null ? asignacion.Contenedor.Codigo : string.Empty,
                    Cotizacion = booking.Cotizacion != null ? booking.Cotizacion.Numero : string.Empty
                });
            }

            if (operaciones.Count == 0)
            {
                operaciones.Add(new OperacionDashboard
                {
                    Fecha = "-",
                    Booking = "-",
                    Cliente = "-",
                    Estado = "-",
                    EtapaPendiente = "Sin movimientos en el rango seleccionado",
                    Buque = "-",
                    Contenedor = "-",
                    Cotizacion = "-"
                });
            }

            return operaciones;
        }

        #endregion

        #region "Alertas"

        private List<AlertaDashboard> ArmarAlertas(
            List<BEFactura> facturasFiltradas,
            List<BEOrdenDeCompra> ordenesCompraFiltradas,
            List<BEBooking> bookingsFiltrados,
            List<BECotizacion> cotizacionesFiltradas)
        {
            List<AlertaDashboard> alertas = new List<AlertaDashboard>();

            foreach (BEFactura factura in facturasFiltradas
                .Where(f => EsEstado(f.Estado, EstadoFactura.Emitida.Codigo)
                    || EsEstado(f.Estado, EstadoFactura.Pendiente.Codigo)))
            {
                alertas.Add(new AlertaDashboard
                {
                    Tipo = "Cobranza",
                    Referencia = factura.NumeroFactura,
                    Fecha = factura.Fecha.ToString("dd/MM/yyyy"),
                    Detalle = "Factura emitida sin pago registrado",
                    Importe = FormatearImporte(factura.Total)
                });
            }

            foreach (BEOrdenDeCompra orden in ordenesCompraFiltradas
                .Where(oc => EsEstado(oc.Estado, EstadoOrdenCompra.Emitida.Codigo)))
            {
                alertas.Add(new AlertaDashboard
                {
                    Tipo = "Compras",
                    Referencia = orden.Numero,
                    Fecha = orden.FechaEmision.ToString("dd/MM/yyyy"),
                    Detalle = "Orden de compra pendiente de recepción",
                    Importe = FormatearImporte(orden.ImporteTotal)
                });
            }

            foreach (BEOrdenDeCompra orden in ordenesCompraFiltradas
                .Where(oc => EsEstado(oc.Estado, EstadoOrdenCompra.Recibida.Codigo)))
            {
                alertas.Add(new AlertaDashboard
                {
                    Tipo = "Proveedor",
                    Referencia = orden.Numero,
                    Fecha = orden.FechaEmision.ToString("dd/MM/yyyy"),
                    Detalle = "Orden de compra recibida pendiente de pago",
                    Importe = FormatearImporte(orden.ImporteTotal)
                });
            }

            foreach (BEBooking booking in bookingsFiltrados
                .Where(b => EsEstado(b.Estado, EstadoBooking.Confirmado.Codigo)))
            {
                alertas.Add(new AlertaDashboard
                {
                    Tipo = "Operación",
                    Referencia = booking.Numero,
                    Fecha = booking.FechaGeneracion.ToString("dd/MM/yyyy"),
                    Detalle = "Booking confirmado pendiente de asignación/facturación",
                    Importe = string.Empty
                });
            }

            foreach (BECotizacion cotizacion in cotizacionesFiltradas
                .Where(c => EsEstado(c.Estado, EstadoCotizacion.Pendiente.Codigo)
                    || EsEstado(c.Estado, EstadoCotizacion.PendienteAprobacion.Codigo)))
            {
                alertas.Add(new AlertaDashboard
                {
                    Tipo = "Comercial",
                    Referencia = cotizacion.Numero,
                    Fecha = cotizacion.FechaEmision.ToString("dd/MM/yyyy"),
                    Detalle = "Cotización pendiente de resolución",
                    Importe = FormatearImporte(cotizacion.MontoTotal)
                });
            }

            if (alertas.Count == 0)
            {
                alertas.Add(new AlertaDashboard
                {
                    Tipo = "Sin alertas",
                    Referencia = "-",
                    Fecha = DateTime.Today.ToString("dd/MM/yyyy"),
                    Detalle = "No hay alertas para el rango seleccionado",
                    Importe = string.Empty
                });
            }

            return alertas;
        }

        #endregion

        #region "Métodos auxiliares"

        private decimal CalcularTotalFacturado(List<BEFactura> facturasFiltradas)
        {
            return facturasFiltradas
                .Where(f => !EsEstado(f.Estado, EstadoFactura.Anulada.Codigo))
                .Sum(f => f.Total);
        }

        private decimal CalcularTotalCobrado(List<BEPago> pagosFiltrados)
        {
            return pagosFiltrados
                .Where(p => EsEstado(p.Estado, EstadoPago.Pagado.Codigo))
                .Sum(p => p.Importe);
        }

        private decimal CalcularTotalCostoProveedor(List<BEPagoProveedor> pagosProveedorFiltrados)
        {
            return pagosProveedorFiltrados
                .Sum(p => p.Importe);
        }

        private bool EsEstado(EstadoFactura estado, string codigo)
        {
            return estado != null && estado.Codigo == codigo;
        }

        private bool EsEstado(EstadoPago estado, string codigo)
        {
            return estado != null && estado.Codigo == codigo;
        }

        private bool EsEstado(EstadoBooking estado, string codigo)
        {
            return estado != null && estado.Codigo == codigo;
        }

        private bool EsEstado(EstadoCotizacion estado, string codigo)
        {
            return estado != null && estado.Codigo == codigo;
        }

        private bool EsEstado(EstadoOrdenCompra estado, string codigo)
        {
            return estado != null && estado.Codigo == codigo;
        }

        private string ObtenerEtapaPendiente(
            BEBooking booking,
            BEAsignacion asignacion,
            BEBillofLading bill,
            BEFactura factura,
            bool tieneManifiesto)
        {
            if (booking == null)
                return "Sin booking";

            if (asignacion == null)
                return "Asignar buque/contenedor";

            if (bill == null)
                return "Emitir Bill of Lading";

            if (!tieneManifiesto)
                return "Generar manifiesto";

            if (factura == null)
                return "Facturar";

            return "Circuito operativo documentado";
        }

        private string FormatearImporte(decimal importe)
        {
            return importe.ToString("C0", CultureInfo.CurrentCulture);
        }

        private string ObtenerMensaje(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            return ex.Message;
        }

        #endregion

        #region "Clases internas"

        private class ResumenFinancieroDashboard
        {
            public string Concepto { get; set; }
            public string Importe { get; set; }
            public string Observacion { get; set; }
        }

        private class AlertaDashboard
        {
            public string Tipo { get; set; }
            public string Referencia { get; set; }
            public string Fecha { get; set; }
            public string Detalle { get; set; }
            public string Importe { get; set; }
        }

        private class OperacionDashboard
        {
            public string Fecha { get; set; }
            public string Booking { get; set; }
            public string Cliente { get; set; }
            public string Estado { get; set; }
            public string EtapaPendiente { get; set; }
            public string Buque { get; set; }
            public string Contenedor { get; set; }
            public string Cotizacion { get; set; }
        }

        #endregion
    }
}



