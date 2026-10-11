namespace GlobalTransLines.UI.Reportes
{
    partial class FrmReportes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnRestablecer = new System.Windows.Forms.Button();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblDesde = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabReportes = new System.Windows.Forms.TabControl();
            this.tabComercial = new System.Windows.Forms.TabPage();
            this.layoutComercial = new System.Windows.Forms.TableLayoutPanel();
            this.panelComercialResumen = new System.Windows.Forms.Panel();
            this.lblConversionValor = new System.Windows.Forms.Label();
            this.lblConversionTitulo = new System.Windows.Forms.Label();
            this.lblMontoCotizadoValor = new System.Windows.Forms.Label();
            this.lblMontoCotizadoTitulo = new System.Windows.Forms.Label();
            this.lblBookingsValor = new System.Windows.Forms.Label();
            this.lblBookingsTitulo = new System.Windows.Forms.Label();
            this.lblCotizacionesValor = new System.Windows.Forms.Label();
            this.lblCotizacionesTitulo = new System.Windows.Forms.Label();
            this.chartComercial = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvComercial = new System.Windows.Forms.DataGridView();
            this.tabTransporte = new System.Windows.Forms.TabPage();
            this.layoutTransporte = new System.Windows.Forms.TableLayoutPanel();
            this.panelTransporteResumen = new System.Windows.Forms.Panel();
            this.lblDocumentacionTransporte = new System.Windows.Forms.Label();
            this.lblPesoTransporte = new System.Windows.Forms.Label();
            this.lblTeuTransporte = new System.Windows.Forms.Label();
            this.lblSeleccionBuque = new System.Windows.Forms.Label();
            this.cboBuque = new System.Windows.Forms.ComboBox();
            this.chartTransporte = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvTransporte = new System.Windows.Forms.DataGridView();
            this.panelItinerario = new System.Windows.Forms.Panel();
            this.lblItinerarioTitulo = new System.Windows.Forms.Label();
            this.txtItinerario = new System.Windows.Forms.TextBox();
            this.tabFacturacion = new System.Windows.Forms.TabPage();
            this.layoutFacturacion = new System.Windows.Forms.TableLayoutPanel();
            this.panelFacturacionResumen = new System.Windows.Forms.Panel();
            this.lblPendienteCobroValor = new System.Windows.Forms.Label();
            this.lblPendienteCobroTitulo = new System.Windows.Forms.Label();
            this.lblCobradoValor = new System.Windows.Forms.Label();
            this.lblCobradoTitulo = new System.Windows.Forms.Label();
            this.lblFacturadoValor = new System.Windows.Forms.Label();
            this.lblFacturadoTitulo = new System.Windows.Forms.Label();
            this.chartFacturacion = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvFacturacion = new System.Windows.Forms.DataGridView();
            this.tabCompras = new System.Windows.Forms.TabPage();
            this.layoutCompras = new System.Windows.Forms.TableLayoutPanel();
            this.panelComprasResumen = new System.Windows.Forms.Panel();
            this.lblPendienteProveedorValor = new System.Windows.Forms.Label();
            this.lblPendienteProveedorTitulo = new System.Windows.Forms.Label();
            this.lblPagadoProveedorValor = new System.Windows.Forms.Label();
            this.lblPagadoProveedorTitulo = new System.Windows.Forms.Label();
            this.lblOrdenadoValor = new System.Windows.Forms.Label();
            this.lblOrdenadoTitulo = new System.Windows.Forms.Label();
            this.chartCompras = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvCompras = new System.Windows.Forms.DataGridView();
            this.tabTrazabilidad = new System.Windows.Forms.TabPage();
            this.layoutTrazabilidad = new System.Windows.Forms.TableLayoutPanel();
            this.panelTrazabilidad = new System.Windows.Forms.Panel();
            this.lblSeleccionBooking = new System.Windows.Forms.Label();
            this.cboBooking = new System.Windows.Forms.ComboBox();
            this.lblGuiaTrazabilidad = new System.Windows.Forms.Label();
            this.dgvTrazabilidad = new System.Windows.Forms.DataGridView();
            this.panelFiltros.SuspendLayout();
            this.tabReportes.SuspendLayout();
            this.tabComercial.SuspendLayout();
            this.layoutComercial.SuspendLayout();
            this.panelComercialResumen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartComercial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComercial)).BeginInit();
            this.tabTransporte.SuspendLayout();
            this.layoutTransporte.SuspendLayout();
            this.panelTransporteResumen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTransporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransporte)).BeginInit();
            this.panelItinerario.SuspendLayout();
            this.tabFacturacion.SuspendLayout();
            this.layoutFacturacion.SuspendLayout();
            this.panelFacturacionResumen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartFacturacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturacion)).BeginInit();
            this.tabCompras.SuspendLayout();
            this.layoutCompras.SuspendLayout();
            this.panelComprasResumen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).BeginInit();
            this.tabTrazabilidad.SuspendLayout();
            this.layoutTrazabilidad.SuspendLayout();
            this.panelTrazabilidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrazabilidad)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFiltros
            // 
            this.panelFiltros.BackColor = System.Drawing.Color.White;
            this.panelFiltros.Controls.Add(this.btnCerrar);
            this.panelFiltros.Controls.Add(this.btnImprimir);
            this.panelFiltros.Controls.Add(this.btnActualizar);
            this.panelFiltros.Controls.Add(this.btnRestablecer);
            this.panelFiltros.Controls.Add(this.btnFiltrar);
            this.panelFiltros.Controls.Add(this.dtpHasta);
            this.panelFiltros.Controls.Add(this.lblHasta);
            this.panelFiltros.Controls.Add(this.dtpDesde);
            this.panelFiltros.Controls.Add(this.lblDesde);
            this.panelFiltros.Controls.Add(this.lblTitulo);
            this.panelFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltros.Location = new System.Drawing.Point(0, 0);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(1380, 72);
            this.panelFiltros.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Location = new System.Drawing.Point(1260, 21);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(105, 32);
            this.btnCerrar.TabIndex = 9;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(1010, 21);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(105, 32);
            this.btnImprimir.TabIndex = 8;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(895, 21);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(105, 32);
            this.btnActualizar.TabIndex = 7;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnRestablecer
            // 
            this.btnRestablecer.Location = new System.Drawing.Point(780, 21);
            this.btnRestablecer.Name = "btnRestablecer";
            this.btnRestablecer.Size = new System.Drawing.Size(105, 32);
            this.btnRestablecer.TabIndex = 6;
            this.btnRestablecer.Text = "Restablecer";
            this.btnRestablecer.UseVisualStyleBackColor = true;
            this.btnRestablecer.Click += new System.EventHandler(this.btnRestablecer_Click);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(665, 21);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(105, 32);
            this.btnFiltrar.TabIndex = 5;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(526, 27);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(120, 22);
            this.dtpHasta.TabIndex = 4;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(478, 30);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(44, 16);
            this.lblHasta.TabIndex = 3;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(343, 27);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(120, 22);
            this.dtpDesde.TabIndex = 2;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(290, 30);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(49, 16);
            this.lblDesde.TabIndex = 1;
            this.lblDesde.Text = "Desde:";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(18, 18);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(248, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Reportes de gestión";
            // 
            // tabReportes
            // 
            this.tabReportes.Controls.Add(this.tabComercial);
            this.tabReportes.Controls.Add(this.tabTransporte);
            this.tabReportes.Controls.Add(this.tabFacturacion);
            this.tabReportes.Controls.Add(this.tabCompras);
            this.tabReportes.Controls.Add(this.tabTrazabilidad);
            this.tabReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabReportes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabReportes.Location = new System.Drawing.Point(0, 72);
            this.tabReportes.Name = "tabReportes";
            this.tabReportes.SelectedIndex = 0;
            this.tabReportes.Size = new System.Drawing.Size(1380, 728);
            this.tabReportes.TabIndex = 1;
            this.tabReportes.SelectedIndexChanged += new System.EventHandler(this.tabReportes_SelectedIndexChanged);
            // 
            // tabComercial
            // 
            this.tabComercial.Controls.Add(this.layoutComercial);
            this.tabComercial.Location = new System.Drawing.Point(4, 32);
            this.tabComercial.Name = "tabComercial";
            this.tabComercial.Padding = new System.Windows.Forms.Padding(8);
            this.tabComercial.Size = new System.Drawing.Size(1372, 692);
            this.tabComercial.TabIndex = 0;
            this.tabComercial.Text = "Comercial";
            this.tabComercial.UseVisualStyleBackColor = true;
            // 
            // layoutComercial
            // 
            this.layoutComercial.ColumnCount = 2;
            this.layoutComercial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.layoutComercial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.layoutComercial.Controls.Add(this.panelComercialResumen, 0, 0);
            this.layoutComercial.Controls.Add(this.chartComercial, 1, 0);
            this.layoutComercial.Controls.Add(this.dgvComercial, 0, 1);
            this.layoutComercial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutComercial.RowCount = 2;
            this.layoutComercial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.layoutComercial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutComercial.SetColumnSpan(this.dgvComercial, 2);
            // 
            // panelComercialResumen
            // 
            this.panelComercialResumen.BackColor = System.Drawing.Color.White;
            this.panelComercialResumen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelComercialResumen.Controls.Add(this.lblConversionValor);
            this.panelComercialResumen.Controls.Add(this.lblConversionTitulo);
            this.panelComercialResumen.Controls.Add(this.lblMontoCotizadoValor);
            this.panelComercialResumen.Controls.Add(this.lblMontoCotizadoTitulo);
            this.panelComercialResumen.Controls.Add(this.lblBookingsValor);
            this.panelComercialResumen.Controls.Add(this.lblBookingsTitulo);
            this.panelComercialResumen.Controls.Add(this.lblCotizacionesValor);
            this.panelComercialResumen.Controls.Add(this.lblCotizacionesTitulo);
            this.panelComercialResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComercialResumen.Margin = new System.Windows.Forms.Padding(4);
            // 
            // etiquetas comercial
            // 
            this.lblCotizacionesTitulo.AutoSize = true;
            this.lblCotizacionesTitulo.ForeColor = System.Drawing.Color.DimGray;
            this.lblCotizacionesTitulo.Location = new System.Drawing.Point(28, 28);
            this.lblCotizacionesTitulo.Text = "Cotizaciones emitidas";
            this.lblCotizacionesValor.AutoSize = true;
            this.lblCotizacionesValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblCotizacionesValor.Location = new System.Drawing.Point(25, 50);
            this.lblCotizacionesValor.Text = "0";
            this.lblBookingsTitulo.AutoSize = true;
            this.lblBookingsTitulo.ForeColor = System.Drawing.Color.DimGray;
            this.lblBookingsTitulo.Location = new System.Drawing.Point(330, 28);
            this.lblBookingsTitulo.Text = "Bookings generados";
            this.lblBookingsValor.AutoSize = true;
            this.lblBookingsValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblBookingsValor.Location = new System.Drawing.Point(327, 50);
            this.lblBookingsValor.Text = "0";
            this.lblMontoCotizadoTitulo.AutoSize = true;
            this.lblMontoCotizadoTitulo.ForeColor = System.Drawing.Color.DimGray;
            this.lblMontoCotizadoTitulo.Location = new System.Drawing.Point(28, 125);
            this.lblMontoCotizadoTitulo.Text = "Monto cotizado";
            this.lblMontoCotizadoValor.AutoSize = true;
            this.lblMontoCotizadoValor.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblMontoCotizadoValor.Location = new System.Drawing.Point(25, 148);
            this.lblMontoCotizadoValor.Text = "$ 0";
            this.lblConversionTitulo.AutoSize = true;
            this.lblConversionTitulo.ForeColor = System.Drawing.Color.DimGray;
            this.lblConversionTitulo.Location = new System.Drawing.Point(330, 125);
            this.lblConversionTitulo.Text = "Conversión a booking";
            this.lblConversionValor.AutoSize = true;
            this.lblConversionValor.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.lblConversionValor.Location = new System.Drawing.Point(327, 148);
            this.lblConversionValor.Text = "0 %";
            // 
            // chartComercial
            // 
            chartArea1.Name = "AreaComercial";
            this.chartComercial.ChartAreas.Add(chartArea1);
            this.chartComercial.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
            legend1.Name = "LeyendaComercial";
            this.chartComercial.Legends.Add(legend1);
            this.chartComercial.Margin = new System.Windows.Forms.Padding(4);
            // 
            // dgvComercial
            // 
            this.dgvComercial.AllowUserToAddRows = false;
            this.dgvComercial.AllowUserToDeleteRows = false;
            this.dgvComercial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvComercial.BackgroundColor = System.Drawing.Color.White;
            this.dgvComercial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComercial.ReadOnly = true;
            this.dgvComercial.RowHeadersVisible = false;
            this.dgvComercial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // tabTransporte
            // 
            this.tabTransporte.Controls.Add(this.layoutTransporte);
            this.tabTransporte.Location = new System.Drawing.Point(4, 32);
            this.tabTransporte.Name = "tabTransporte";
            this.tabTransporte.Padding = new System.Windows.Forms.Padding(8);
            this.tabTransporte.Size = new System.Drawing.Size(1372, 692);
            this.tabTransporte.TabIndex = 1;
            this.tabTransporte.Text = "Transporte";
            this.tabTransporte.UseVisualStyleBackColor = true;
            // 
            // layoutTransporte
            // 
            this.layoutTransporte.ColumnCount = 2;
            this.layoutTransporte.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56F));
            this.layoutTransporte.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.layoutTransporte.Controls.Add(this.panelTransporteResumen, 0, 0);
            this.layoutTransporte.Controls.Add(this.chartTransporte, 1, 0);
            this.layoutTransporte.Controls.Add(this.dgvTransporte, 0, 1);
            this.layoutTransporte.Controls.Add(this.panelItinerario, 0, 2);
            this.layoutTransporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTransporte.RowCount = 3;
            this.layoutTransporte.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.layoutTransporte.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTransporte.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.layoutTransporte.SetColumnSpan(this.dgvTransporte, 2);
            this.layoutTransporte.SetColumnSpan(this.panelItinerario, 2);
            // 
            // panelTransporteResumen
            // 
            this.panelTransporteResumen.BackColor = System.Drawing.Color.White;
            this.panelTransporteResumen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTransporteResumen.Controls.Add(this.lblDocumentacionTransporte);
            this.panelTransporteResumen.Controls.Add(this.lblPesoTransporte);
            this.panelTransporteResumen.Controls.Add(this.lblTeuTransporte);
            this.panelTransporteResumen.Controls.Add(this.lblSeleccionBuque);
            this.panelTransporteResumen.Controls.Add(this.cboBuque);
            this.panelTransporteResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTransporteResumen.Margin = new System.Windows.Forms.Padding(4);
            this.lblSeleccionBuque.AutoSize = true;
            this.lblSeleccionBuque.Location = new System.Drawing.Point(24, 24);
            this.lblSeleccionBuque.Text = "Buque:";
            this.cboBuque.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuque.FormattingEnabled = true;
            this.cboBuque.Location = new System.Drawing.Point(92, 20);
            this.cboBuque.Size = new System.Drawing.Size(330, 31);
            this.cboBuque.SelectedIndexChanged += new System.EventHandler(this.cboBuque_SelectedIndexChanged);
            this.lblTeuTransporte.AutoSize = true;
            this.lblTeuTransporte.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTeuTransporte.Location = new System.Drawing.Point(24, 76);
            this.lblTeuTransporte.Text = "TEU: 0 ocupados / 0 disponibles";
            this.lblPesoTransporte.AutoSize = true;
            this.lblPesoTransporte.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPesoTransporte.Location = new System.Drawing.Point(24, 122);
            this.lblPesoTransporte.Text = "Peso: 0 tn ocupadas / 0 tn disponibles";
            this.lblDocumentacionTransporte.AutoSize = true;
            this.lblDocumentacionTransporte.ForeColor = System.Drawing.Color.DimGray;
            this.lblDocumentacionTransporte.Location = new System.Drawing.Point(24, 181);
            this.lblDocumentacionTransporte.Text = "Documentación: 0 Bill of Lading | 0 manifiestos";
            // 
            // chartTransporte
            // 
            chartArea2.Name = "AreaTransporte";
            this.chartTransporte.ChartAreas.Add(chartArea2);
            this.chartTransporte.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
            legend2.Name = "LeyendaTransporte";
            this.chartTransporte.Legends.Add(legend2);
            this.chartTransporte.Margin = new System.Windows.Forms.Padding(4);
            // 
            // dgvTransporte
            // 
            this.dgvTransporte.AllowUserToAddRows = false;
            this.dgvTransporte.AllowUserToDeleteRows = false;
            this.dgvTransporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransporte.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransporte.ReadOnly = true;
            this.dgvTransporte.RowHeadersVisible = false;
            this.dgvTransporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // panelItinerario
            // 
            this.panelItinerario.Controls.Add(this.lblItinerarioTitulo);
            this.panelItinerario.Controls.Add(this.txtItinerario);
            this.panelItinerario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblItinerarioTitulo.AutoSize = true;
            this.lblItinerarioTitulo.Location = new System.Drawing.Point(5, 9);
            this.lblItinerarioTitulo.Text = "Itinerario:";
            this.txtItinerario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtItinerario.BackColor = System.Drawing.Color.White;
            this.txtItinerario.Location = new System.Drawing.Point(90, 6);
            this.txtItinerario.Multiline = true;
            this.txtItinerario.ReadOnly = true;
            this.txtItinerario.Size = new System.Drawing.Size(1260, 58);
            // 
            // tabFacturacion
            // 
            this.tabFacturacion.Controls.Add(this.layoutFacturacion);
            this.tabFacturacion.Location = new System.Drawing.Point(4, 32);
            this.tabFacturacion.Name = "tabFacturacion";
            this.tabFacturacion.Padding = new System.Windows.Forms.Padding(8);
            this.tabFacturacion.Size = new System.Drawing.Size(1372, 692);
            this.tabFacturacion.TabIndex = 2;
            this.tabFacturacion.Text = "Facturación y cobranzas";
            this.tabFacturacion.UseVisualStyleBackColor = true;
            // 
            // layoutFacturacion
            // 
            this.layoutFacturacion.ColumnCount = 2;
            this.layoutFacturacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.layoutFacturacion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.layoutFacturacion.Controls.Add(this.panelFacturacionResumen, 0, 0);
            this.layoutFacturacion.Controls.Add(this.chartFacturacion, 1, 0);
            this.layoutFacturacion.Controls.Add(this.dgvFacturacion, 0, 1);
            this.layoutFacturacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutFacturacion.RowCount = 2;
            this.layoutFacturacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.layoutFacturacion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutFacturacion.SetColumnSpan(this.dgvFacturacion, 2);
            // 
            // panelFacturacionResumen
            // 
            this.panelFacturacionResumen.BackColor = System.Drawing.Color.White;
            this.panelFacturacionResumen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFacturacionResumen.Controls.Add(this.lblPendienteCobroValor);
            this.panelFacturacionResumen.Controls.Add(this.lblPendienteCobroTitulo);
            this.panelFacturacionResumen.Controls.Add(this.lblCobradoValor);
            this.panelFacturacionResumen.Controls.Add(this.lblCobradoTitulo);
            this.panelFacturacionResumen.Controls.Add(this.lblFacturadoValor);
            this.panelFacturacionResumen.Controls.Add(this.lblFacturadoTitulo);
            this.panelFacturacionResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFacturacionResumen.Margin = new System.Windows.Forms.Padding(4);
            this.lblFacturadoTitulo.AutoSize = true;
            this.lblFacturadoTitulo.ForeColor = System.Drawing.Color.DimGray;
            this.lblFacturadoTitulo.Location = new System.Drawing.Point(30, 30);
            this.lblFacturadoTitulo.Text = "Total facturado";
            this.lblFacturadoValor.AutoSize = true;
            this.lblFacturadoValor.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblFacturadoValor.Location = new System.Drawing.Point(27, 56);
            this.lblFacturadoValor.Text = "$ 0";
            this.lblCobradoTitulo.AutoSize = true;
            this.lblCobradoTitulo.ForeColor = System.Drawing.Color.DimGray;
            this.lblCobradoTitulo.Location = new System.Drawing.Point(330, 30);
            this.lblCobradoTitulo.Text = "Total cobrado";
            this.lblCobradoValor.AutoSize = true;
            this.lblCobradoValor.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCobradoValor.Location = new System.Drawing.Point(327, 56);
            this.lblCobradoValor.Text = "$ 0";
            this.lblPendienteCobroTitulo.AutoSize = true;
            this.lblPendienteCobroTitulo.ForeColor = System.Drawing.Color.DimGray;
            this.lblPendienteCobroTitulo.Location = new System.Drawing.Point(30, 136);
            this.lblPendienteCobroTitulo.Text = "Pendiente de cobro";
            this.lblPendienteCobroValor.AutoSize = true;
            this.lblPendienteCobroValor.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPendienteCobroValor.Location = new System.Drawing.Point(27, 162);
            this.lblPendienteCobroValor.Text = "$ 0";
            // 
            // chartFacturacion
            // 
            chartArea3.Name = "AreaFacturacion";
            this.chartFacturacion.ChartAreas.Add(chartArea3);
            this.chartFacturacion.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
            legend3.Name = "LeyendaFacturacion";
            this.chartFacturacion.Legends.Add(legend3);
            this.chartFacturacion.Margin = new System.Windows.Forms.Padding(4);
            // 
            // dgvFacturacion
            // 
            this.dgvFacturacion.AllowUserToAddRows = false;
            this.dgvFacturacion.AllowUserToDeleteRows = false;
            this.dgvFacturacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFacturacion.BackgroundColor = System.Drawing.Color.White;
            this.dgvFacturacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFacturacion.ReadOnly = true;
            this.dgvFacturacion.RowHeadersVisible = false;
            this.dgvFacturacion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // tabCompras
            // 
            this.tabCompras.Controls.Add(this.layoutCompras);
            this.tabCompras.Location = new System.Drawing.Point(4, 32);
            this.tabCompras.Name = "tabCompras";
            this.tabCompras.Padding = new System.Windows.Forms.Padding(8);
            this.tabCompras.Size = new System.Drawing.Size(1372, 692);
            this.tabCompras.TabIndex = 3;
            this.tabCompras.Text = "Compras y proveedores";
            this.tabCompras.UseVisualStyleBackColor = true;
            // 
            // layoutCompras
            // 
            this.layoutCompras.ColumnCount = 2;
            this.layoutCompras.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.layoutCompras.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.layoutCompras.Controls.Add(this.panelComprasResumen, 0, 0);
            this.layoutCompras.Controls.Add(this.chartCompras, 1, 0);
            this.layoutCompras.Controls.Add(this.dgvCompras, 0, 1);
            this.layoutCompras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutCompras.RowCount = 2;
            this.layoutCompras.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.layoutCompras.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutCompras.SetColumnSpan(this.dgvCompras, 2);
            // 
            // panelComprasResumen
            // 
            this.panelComprasResumen.BackColor = System.Drawing.Color.White;
            this.panelComprasResumen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelComprasResumen.Controls.Add(this.lblPendienteProveedorValor);
            this.panelComprasResumen.Controls.Add(this.lblPendienteProveedorTitulo);
            this.panelComprasResumen.Controls.Add(this.lblPagadoProveedorValor);
            this.panelComprasResumen.Controls.Add(this.lblPagadoProveedorTitulo);
            this.panelComprasResumen.Controls.Add(this.lblOrdenadoValor);
            this.panelComprasResumen.Controls.Add(this.lblOrdenadoTitulo);
            this.panelComprasResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelComprasResumen.Margin = new System.Windows.Forms.Padding(4);
            this.lblOrdenadoTitulo.AutoSize = true;
            this.lblOrdenadoTitulo.ForeColor = System.Drawing.Color.DimGray;
            this.lblOrdenadoTitulo.Location = new System.Drawing.Point(30, 30);
            this.lblOrdenadoTitulo.Text = "Total ordenado";
            this.lblOrdenadoValor.AutoSize = true;
            this.lblOrdenadoValor.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblOrdenadoValor.Location = new System.Drawing.Point(27, 56);
            this.lblOrdenadoValor.Text = "$ 0";
            this.lblPagadoProveedorTitulo.AutoSize = true;
            this.lblPagadoProveedorTitulo.ForeColor = System.Drawing.Color.DimGray;
            this.lblPagadoProveedorTitulo.Location = new System.Drawing.Point(330, 30);
            this.lblPagadoProveedorTitulo.Text = "Pagado a proveedores";
            this.lblPagadoProveedorValor.AutoSize = true;
            this.lblPagadoProveedorValor.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPagadoProveedorValor.Location = new System.Drawing.Point(327, 56);
            this.lblPagadoProveedorValor.Text = "$ 0";
            this.lblPendienteProveedorTitulo.AutoSize = true;
            this.lblPendienteProveedorTitulo.ForeColor = System.Drawing.Color.DimGray;
            this.lblPendienteProveedorTitulo.Location = new System.Drawing.Point(30, 136);
            this.lblPendienteProveedorTitulo.Text = "Pendiente de pago";
            this.lblPendienteProveedorValor.AutoSize = true;
            this.lblPendienteProveedorValor.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPendienteProveedorValor.Location = new System.Drawing.Point(27, 162);
            this.lblPendienteProveedorValor.Text = "$ 0";
            // 
            // chartCompras
            // 
            chartArea4.Name = "AreaCompras";
            this.chartCompras.ChartAreas.Add(chartArea4);
            this.chartCompras.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Right;
            legend4.Name = "LeyendaCompras";
            this.chartCompras.Legends.Add(legend4);
            this.chartCompras.Margin = new System.Windows.Forms.Padding(4);
            // 
            // dgvCompras
            // 
            this.dgvCompras.AllowUserToAddRows = false;
            this.dgvCompras.AllowUserToDeleteRows = false;
            this.dgvCompras.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCompras.BackgroundColor = System.Drawing.Color.White;
            this.dgvCompras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCompras.ReadOnly = true;
            this.dgvCompras.RowHeadersVisible = false;
            this.dgvCompras.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // tabTrazabilidad
            // 
            this.tabTrazabilidad.Controls.Add(this.layoutTrazabilidad);
            this.tabTrazabilidad.Location = new System.Drawing.Point(4, 32);
            this.tabTrazabilidad.Name = "tabTrazabilidad";
            this.tabTrazabilidad.Padding = new System.Windows.Forms.Padding(8);
            this.tabTrazabilidad.Size = new System.Drawing.Size(1372, 692);
            this.tabTrazabilidad.TabIndex = 4;
            this.tabTrazabilidad.Text = "Trazabilidad";
            this.tabTrazabilidad.UseVisualStyleBackColor = true;
            // 
            // layoutTrazabilidad
            // 
            this.layoutTrazabilidad.ColumnCount = 1;
            this.layoutTrazabilidad.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutTrazabilidad.Controls.Add(this.panelTrazabilidad, 0, 0);
            this.layoutTrazabilidad.Controls.Add(this.dgvTrazabilidad, 0, 1);
            this.layoutTrazabilidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTrazabilidad.RowCount = 2;
            this.layoutTrazabilidad.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layoutTrazabilidad.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            // 
            // panelTrazabilidad
            // 
            this.panelTrazabilidad.BackColor = System.Drawing.Color.White;
            this.panelTrazabilidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTrazabilidad.Controls.Add(this.lblSeleccionBooking);
            this.panelTrazabilidad.Controls.Add(this.cboBooking);
            this.panelTrazabilidad.Controls.Add(this.lblGuiaTrazabilidad);
            this.panelTrazabilidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTrazabilidad.Margin = new System.Windows.Forms.Padding(4);
            this.lblSeleccionBooking.AutoSize = true;
            this.lblSeleccionBooking.Location = new System.Drawing.Point(22, 22);
            this.lblSeleccionBooking.Text = "Booking:";
            this.cboBooking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBooking.FormattingEnabled = true;
            this.cboBooking.Location = new System.Drawing.Point(105, 18);
            this.cboBooking.Size = new System.Drawing.Size(520, 31);
            this.cboBooking.SelectedIndexChanged += new System.EventHandler(this.cboBooking_SelectedIndexChanged);
            this.lblGuiaTrazabilidad.AutoSize = true;
            this.lblGuiaTrazabilidad.ForeColor = System.Drawing.Color.DimGray;
            this.lblGuiaTrazabilidad.Location = new System.Drawing.Point(22, 70);
            this.lblGuiaTrazabilidad.Text = "Recorrido integral: cotización, booking, asignación, documentación, facturación, cobranza y compras.";
            // 
            // dgvTrazabilidad
            // 
            this.dgvTrazabilidad.AllowUserToAddRows = false;
            this.dgvTrazabilidad.AllowUserToDeleteRows = false;
            this.dgvTrazabilidad.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTrazabilidad.BackgroundColor = System.Drawing.Color.White;
            this.dgvTrazabilidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTrazabilidad.ReadOnly = true;
            this.dgvTrazabilidad.RowHeadersVisible = false;
            this.dgvTrazabilidad.RowTemplate.Height = 38;
            this.dgvTrazabilidad.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // FrmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 800);
            this.Controls.Add(this.tabReportes);
            this.Controls.Add(this.panelFiltros);
            this.MinimumSize = new System.Drawing.Size(1180, 700);
            this.Name = "FrmReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "FrmReportes";
            this.Text = "Reportes por gestión";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmReportes_Load);
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            this.tabReportes.ResumeLayout(false);
            this.tabComercial.ResumeLayout(false);
            this.layoutComercial.ResumeLayout(false);
            this.panelComercialResumen.ResumeLayout(false);
            this.panelComercialResumen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartComercial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComercial)).EndInit();
            this.tabTransporte.ResumeLayout(false);
            this.layoutTransporte.ResumeLayout(false);
            this.panelTransporteResumen.ResumeLayout(false);
            this.panelTransporteResumen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTransporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransporte)).EndInit();
            this.panelItinerario.ResumeLayout(false);
            this.panelItinerario.PerformLayout();
            this.tabFacturacion.ResumeLayout(false);
            this.layoutFacturacion.ResumeLayout(false);
            this.panelFacturacionResumen.ResumeLayout(false);
            this.panelFacturacionResumen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartFacturacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturacion)).EndInit();
            this.tabCompras.ResumeLayout(false);
            this.layoutCompras.ResumeLayout(false);
            this.panelComprasResumen.ResumeLayout(false);
            this.panelComprasResumen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartCompras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).EndInit();
            this.tabTrazabilidad.ResumeLayout(false);
            this.layoutTrazabilidad.ResumeLayout(false);
            this.panelTrazabilidad.ResumeLayout(false);
            this.panelTrazabilidad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTrazabilidad)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Button btnRestablecer;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TabControl tabReportes;
        private System.Windows.Forms.TabPage tabComercial;
        private System.Windows.Forms.TabPage tabTransporte;
        private System.Windows.Forms.TabPage tabFacturacion;
        private System.Windows.Forms.TabPage tabCompras;
        private System.Windows.Forms.TabPage tabTrazabilidad;
        private System.Windows.Forms.TableLayoutPanel layoutComercial;
        private System.Windows.Forms.Panel panelComercialResumen;
        private System.Windows.Forms.Label lblCotizacionesTitulo;
        private System.Windows.Forms.Label lblCotizacionesValor;
        private System.Windows.Forms.Label lblBookingsTitulo;
        private System.Windows.Forms.Label lblBookingsValor;
        private System.Windows.Forms.Label lblMontoCotizadoTitulo;
        private System.Windows.Forms.Label lblMontoCotizadoValor;
        private System.Windows.Forms.Label lblConversionTitulo;
        private System.Windows.Forms.Label lblConversionValor;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartComercial;
        private System.Windows.Forms.DataGridView dgvComercial;
        private System.Windows.Forms.TableLayoutPanel layoutTransporte;
        private System.Windows.Forms.Panel panelTransporteResumen;
        private System.Windows.Forms.Label lblSeleccionBuque;
        private System.Windows.Forms.ComboBox cboBuque;
        private System.Windows.Forms.Label lblTeuTransporte;
        private System.Windows.Forms.Label lblPesoTransporte;
        private System.Windows.Forms.Label lblDocumentacionTransporte;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTransporte;
        private System.Windows.Forms.DataGridView dgvTransporte;
        private System.Windows.Forms.Panel panelItinerario;
        private System.Windows.Forms.Label lblItinerarioTitulo;
        private System.Windows.Forms.TextBox txtItinerario;
        private System.Windows.Forms.TableLayoutPanel layoutFacturacion;
        private System.Windows.Forms.Panel panelFacturacionResumen;
        private System.Windows.Forms.Label lblFacturadoTitulo;
        private System.Windows.Forms.Label lblFacturadoValor;
        private System.Windows.Forms.Label lblCobradoTitulo;
        private System.Windows.Forms.Label lblCobradoValor;
        private System.Windows.Forms.Label lblPendienteCobroTitulo;
        private System.Windows.Forms.Label lblPendienteCobroValor;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFacturacion;
        private System.Windows.Forms.DataGridView dgvFacturacion;
        private System.Windows.Forms.TableLayoutPanel layoutCompras;
        private System.Windows.Forms.Panel panelComprasResumen;
        private System.Windows.Forms.Label lblOrdenadoTitulo;
        private System.Windows.Forms.Label lblOrdenadoValor;
        private System.Windows.Forms.Label lblPagadoProveedorTitulo;
        private System.Windows.Forms.Label lblPagadoProveedorValor;
        private System.Windows.Forms.Label lblPendienteProveedorTitulo;
        private System.Windows.Forms.Label lblPendienteProveedorValor;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCompras;
        private System.Windows.Forms.DataGridView dgvCompras;
        private System.Windows.Forms.TableLayoutPanel layoutTrazabilidad;
        private System.Windows.Forms.Panel panelTrazabilidad;
        private System.Windows.Forms.Label lblSeleccionBooking;
        private System.Windows.Forms.ComboBox cboBooking;
        private System.Windows.Forms.Label lblGuiaTrazabilidad;
        private System.Windows.Forms.DataGridView dgvTrazabilidad;
    }
}
