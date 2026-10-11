namespace GlobalTransLines.UI.Reportes
{
    partial class FrmDashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.btnRestablecer = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblUltimaActualizacion = new System.Windows.Forms.Label();
            this.tabDashboard = new System.Windows.Forms.TabControl();
            this.tabResumen = new System.Windows.Forms.TabPage();
            this.layoutResumen = new System.Windows.Forms.TableLayoutPanel();
            this.cardFacturado = new System.Windows.Forms.Panel();
            this.lblFacturadoValor = new System.Windows.Forms.Label();
            this.lblFacturadoTitulo = new System.Windows.Forms.Label();
            this.cardCobrado = new System.Windows.Forms.Panel();
            this.lblCobradoValor = new System.Windows.Forms.Label();
            this.lblCobradoTitulo = new System.Windows.Forms.Label();
            this.cardPendiente = new System.Windows.Forms.Panel();
            this.lblPendienteValor = new System.Windows.Forms.Label();
            this.lblPendienteTitulo = new System.Windows.Forms.Label();
            this.cardFacturas = new System.Windows.Forms.Panel();
            this.lblFacturasValor = new System.Windows.Forms.Label();
            this.lblFacturasTitulo = new System.Windows.Forms.Label();
            this.cardBookings = new System.Windows.Forms.Panel();
            this.lblBookingsValor = new System.Windows.Forms.Label();
            this.lblBookingsTitulo = new System.Windows.Forms.Label();
            this.cardCotizaciones = new System.Windows.Forms.Panel();
            this.lblCotizacionesValor = new System.Windows.Forms.Label();
            this.lblCotizacionesTitulo = new System.Windows.Forms.Label();
            this.chartFacturadoCobrado = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartFacturasEstado = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabFinanzas = new System.Windows.Forms.TabPage();
            this.layoutFinanzas = new System.Windows.Forms.TableLayoutPanel();
            this.chartFacturadoMes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            this.tabOperaciones = new System.Windows.Forms.TabPage();
            this.layoutOperaciones = new System.Windows.Forms.TableLayoutPanel();
            this.chartBookingsEstado = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvOperaciones = new System.Windows.Forms.DataGridView();
            this.tabAlertas = new System.Windows.Forms.TabPage();
            this.dgvAlertas = new System.Windows.Forms.DataGridView();
            this.panelFiltros.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            this.tabResumen.SuspendLayout();
            this.layoutResumen.SuspendLayout();
            this.cardFacturado.SuspendLayout();
            this.cardCobrado.SuspendLayout();
            this.cardPendiente.SuspendLayout();
            this.cardFacturas.SuspendLayout();
            this.cardBookings.SuspendLayout();
            this.cardCotizaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartFacturadoCobrado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFacturasEstado)).BeginInit();
            this.tabFinanzas.SuspendLayout();
            this.layoutFinanzas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartFacturadoMes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            this.tabOperaciones.SuspendLayout();
            this.layoutOperaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBookingsEstado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperaciones)).BeginInit();
            this.tabAlertas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertas)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFiltros
            // 
            this.panelFiltros.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelFiltros.Controls.Add(this.lblTitulo);
            this.panelFiltros.Controls.Add(this.lblDesde);
            this.panelFiltros.Controls.Add(this.dtpDesde);
            this.panelFiltros.Controls.Add(this.lblHasta);
            this.panelFiltros.Controls.Add(this.dtpHasta);
            this.panelFiltros.Controls.Add(this.btnFiltrar);
            this.panelFiltros.Controls.Add(this.btnRestablecer);
            this.panelFiltros.Controls.Add(this.btnActualizar);
            this.panelFiltros.Controls.Add(this.btnCerrar);
            this.panelFiltros.Controls.Add(this.lblUltimaActualizacion);
            this.panelFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltros.Location = new System.Drawing.Point(0, 0);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(1280, 86);
            this.panelFiltros.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(16, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(268, 32);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Dashboard operativo";
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(360, 18);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(49, 16);
            this.lblDesde.TabIndex = 1;
            this.lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(415, 15);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(130, 22);
            this.dtpDesde.TabIndex = 2;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(565, 18);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(46, 16);
            this.lblHasta.TabIndex = 3;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(617, 15);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(130, 22);
            this.dtpHasta.TabIndex = 4;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(765, 11);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(110, 31);
            this.btnFiltrar.TabIndex = 5;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // btnRestablecer
            // 
            this.btnRestablecer.Location = new System.Drawing.Point(885, 11);
            this.btnRestablecer.Name = "btnRestablecer";
            this.btnRestablecer.Size = new System.Drawing.Size(110, 31);
            this.btnRestablecer.TabIndex = 6;
            this.btnRestablecer.Text = "Restablecer";
            this.btnRestablecer.UseVisualStyleBackColor = true;
            this.btnRestablecer.Click += new System.EventHandler(this.btnRestablecer_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(1005, 11);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(110, 31);
            this.btnActualizar.TabIndex = 7;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Location = new System.Drawing.Point(1155, 11);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(110, 31);
            this.btnCerrar.TabIndex = 9;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblUltimaActualizacion
            // 
            this.lblUltimaActualizacion.AutoSize = true;
            this.lblUltimaActualizacion.ForeColor = System.Drawing.Color.DimGray;
            this.lblUltimaActualizacion.Location = new System.Drawing.Point(19, 52);
            this.lblUltimaActualizacion.Name = "lblUltimaActualizacion";
            this.lblUltimaActualizacion.Size = new System.Drawing.Size(134, 16);
            this.lblUltimaActualizacion.TabIndex = 8;
            this.lblUltimaActualizacion.Text = "Última actualización:";
            // 
            // tabDashboard
            // 
            this.tabDashboard.Controls.Add(this.tabResumen);
            this.tabDashboard.Controls.Add(this.tabFinanzas);
            this.tabDashboard.Controls.Add(this.tabOperaciones);
            this.tabDashboard.Controls.Add(this.tabAlertas);
            this.tabDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDashboard.Location = new System.Drawing.Point(0, 86);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.SelectedIndex = 0;
            this.tabDashboard.Size = new System.Drawing.Size(1280, 634);
            this.tabDashboard.TabIndex = 1;
            // 
            // tabResumen
            // 
            this.tabResumen.Controls.Add(this.layoutResumen);
            this.tabResumen.Location = new System.Drawing.Point(4, 25);
            this.tabResumen.Name = "tabResumen";
            this.tabResumen.Padding = new System.Windows.Forms.Padding(8);
            this.tabResumen.Size = new System.Drawing.Size(1272, 605);
            this.tabResumen.TabIndex = 0;
            this.tabResumen.Text = "Resumen";
            this.tabResumen.UseVisualStyleBackColor = true;
            // 
            // layoutResumen
            // 
            this.layoutResumen.ColumnCount = 3;
            this.layoutResumen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutResumen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutResumen.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.layoutResumen.Controls.Add(this.cardFacturado, 0, 0);
            this.layoutResumen.Controls.Add(this.cardCobrado, 1, 0);
            this.layoutResumen.Controls.Add(this.cardPendiente, 2, 0);
            this.layoutResumen.Controls.Add(this.cardFacturas, 0, 1);
            this.layoutResumen.Controls.Add(this.cardBookings, 1, 1);
            this.layoutResumen.Controls.Add(this.cardCotizaciones, 2, 1);
            this.layoutResumen.Controls.Add(this.chartFacturadoCobrado, 0, 2);
            this.layoutResumen.Controls.Add(this.chartFacturasEstado, 2, 2);
            this.layoutResumen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutResumen.Location = new System.Drawing.Point(8, 8);
            this.layoutResumen.Name = "layoutResumen";
            this.layoutResumen.RowCount = 3;
            this.layoutResumen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layoutResumen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.layoutResumen.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutResumen.Size = new System.Drawing.Size(1256, 589);
            this.layoutResumen.TabIndex = 0;
            // 
            // chartFacturadoCobrado
            // 
            chartArea1.Name = "ChartArea1";
            this.chartFacturadoCobrado.ChartAreas.Add(chartArea1);
            this.layoutResumen.SetColumnSpan(this.chartFacturadoCobrado, 2);
            this.chartFacturadoCobrado.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartFacturadoCobrado.Legends.Add(legend1);
            this.chartFacturadoCobrado.Location = new System.Drawing.Point(3, 223);
            this.chartFacturadoCobrado.Name = "chartFacturadoCobrado";
            this.chartFacturadoCobrado.Size = new System.Drawing.Size(831, 363);
            this.chartFacturadoCobrado.TabIndex = 6;
            // 
            // chartFacturasEstado
            // 
            chartArea2.Name = "ChartArea1";
            this.chartFacturasEstado.ChartAreas.Add(chartArea2);
            this.chartFacturasEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartFacturasEstado.Legends.Add(legend2);
            this.chartFacturasEstado.Location = new System.Drawing.Point(840, 223);
            this.chartFacturasEstado.Name = "chartFacturasEstado";
            this.chartFacturasEstado.Size = new System.Drawing.Size(413, 363);
            this.chartFacturasEstado.TabIndex = 7;
            // 
            // tabFinanzas
            // 
            this.tabFinanzas.Controls.Add(this.layoutFinanzas);
            this.tabFinanzas.Location = new System.Drawing.Point(4, 25);
            this.tabFinanzas.Name = "tabFinanzas";
            this.tabFinanzas.Padding = new System.Windows.Forms.Padding(8);
            this.tabFinanzas.Size = new System.Drawing.Size(1272, 605);
            this.tabFinanzas.TabIndex = 1;
            this.tabFinanzas.Text = "Finanzas";
            this.tabFinanzas.UseVisualStyleBackColor = true;
            // 
            // layoutFinanzas
            // 
            this.layoutFinanzas.ColumnCount = 1;
            this.layoutFinanzas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutFinanzas.Controls.Add(this.chartFacturadoMes, 0, 0);
            this.layoutFinanzas.Controls.Add(this.dgvFacturas, 0, 1);
            this.layoutFinanzas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutFinanzas.Location = new System.Drawing.Point(8, 8);
            this.layoutFinanzas.Name = "layoutFinanzas";
            this.layoutFinanzas.RowCount = 2;
            this.layoutFinanzas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.layoutFinanzas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.layoutFinanzas.Size = new System.Drawing.Size(1256, 589);
            this.layoutFinanzas.TabIndex = 0;
            // 
            // chartFacturadoMes
            // 
            chartArea3.Name = "ChartArea1";
            this.chartFacturadoMes.ChartAreas.Add(chartArea3);
            this.chartFacturadoMes.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.chartFacturadoMes.Legends.Add(legend3);
            this.chartFacturadoMes.Location = new System.Drawing.Point(3, 3);
            this.chartFacturadoMes.Name = "chartFacturadoMes";
            this.chartFacturadoMes.Size = new System.Drawing.Size(1250, 317);
            this.chartFacturadoMes.TabIndex = 0;
            // 
            // dgvFacturas
            // 
            this.dgvFacturas.AllowUserToAddRows = false;
            this.dgvFacturas.AllowUserToDeleteRows = false;
            this.dgvFacturas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFacturas.Location = new System.Drawing.Point(3, 326);
            this.dgvFacturas.Name = "dgvFacturas";
            this.dgvFacturas.ReadOnly = true;
            this.dgvFacturas.RowHeadersWidth = 24;
            this.dgvFacturas.RowTemplate.Height = 24;
            this.dgvFacturas.Size = new System.Drawing.Size(1250, 260);
            this.dgvFacturas.TabIndex = 1;
            // 
            // tabOperaciones
            // 
            this.tabOperaciones.Controls.Add(this.layoutOperaciones);
            this.tabOperaciones.Location = new System.Drawing.Point(4, 25);
            this.tabOperaciones.Name = "tabOperaciones";
            this.tabOperaciones.Padding = new System.Windows.Forms.Padding(8);
            this.tabOperaciones.Size = new System.Drawing.Size(1272, 605);
            this.tabOperaciones.TabIndex = 2;
            this.tabOperaciones.Text = "Operaciones";
            this.tabOperaciones.UseVisualStyleBackColor = true;
            // 
            // layoutOperaciones
            // 
            this.layoutOperaciones.ColumnCount = 1;
            this.layoutOperaciones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutOperaciones.Controls.Add(this.chartBookingsEstado, 0, 0);
            this.layoutOperaciones.Controls.Add(this.dgvOperaciones, 0, 1);
            this.layoutOperaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutOperaciones.Location = new System.Drawing.Point(8, 8);
            this.layoutOperaciones.Name = "layoutOperaciones";
            this.layoutOperaciones.RowCount = 2;
            this.layoutOperaciones.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutOperaciones.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutOperaciones.Size = new System.Drawing.Size(1256, 589);
            this.layoutOperaciones.TabIndex = 0;
            // 
            // chartBookingsEstado
            // 
            chartArea4.Name = "ChartArea1";
            this.chartBookingsEstado.ChartAreas.Add(chartArea4);
            this.chartBookingsEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.chartBookingsEstado.Legends.Add(legend4);
            this.chartBookingsEstado.Location = new System.Drawing.Point(3, 3);
            this.chartBookingsEstado.Name = "chartBookingsEstado";
            this.chartBookingsEstado.Size = new System.Drawing.Size(1250, 288);
            this.chartBookingsEstado.TabIndex = 0;
            // 
            // dgvOperaciones
            // 
            this.dgvOperaciones.AllowUserToAddRows = false;
            this.dgvOperaciones.AllowUserToDeleteRows = false;
            this.dgvOperaciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOperaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOperaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOperaciones.Location = new System.Drawing.Point(3, 297);
            this.dgvOperaciones.Name = "dgvOperaciones";
            this.dgvOperaciones.ReadOnly = true;
            this.dgvOperaciones.RowHeadersWidth = 24;
            this.dgvOperaciones.RowTemplate.Height = 24;
            this.dgvOperaciones.Size = new System.Drawing.Size(1250, 289);
            this.dgvOperaciones.TabIndex = 1;
            // 
            // tabAlertas
            // 
            this.tabAlertas.Controls.Add(this.dgvAlertas);
            this.tabAlertas.Location = new System.Drawing.Point(4, 25);
            this.tabAlertas.Name = "tabAlertas";
            this.tabAlertas.Padding = new System.Windows.Forms.Padding(8);
            this.tabAlertas.Size = new System.Drawing.Size(1272, 605);
            this.tabAlertas.TabIndex = 3;
            this.tabAlertas.Text = "Alertas";
            this.tabAlertas.UseVisualStyleBackColor = true;
            // 
            // dgvAlertas
            // 
            this.dgvAlertas.AllowUserToAddRows = false;
            this.dgvAlertas.AllowUserToDeleteRows = false;
            this.dgvAlertas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAlertas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlertas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlertas.Location = new System.Drawing.Point(8, 8);
            this.dgvAlertas.Name = "dgvAlertas";
            this.dgvAlertas.ReadOnly = true;
            this.dgvAlertas.RowHeadersWidth = 24;
            this.dgvAlertas.RowTemplate.Height = 24;
            this.dgvAlertas.Size = new System.Drawing.Size(1256, 589);
            this.dgvAlertas.TabIndex = 0;
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.tabDashboard);
            this.Controls.Add(this.panelFiltros);
            this.Name = "FrmDashboard";
            this.Tag = "FrmDashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.FrmDashboard_Load);
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            this.tabDashboard.ResumeLayout(false);
            this.tabResumen.ResumeLayout(false);
            this.layoutResumen.ResumeLayout(false);
            this.cardFacturado.ResumeLayout(false);
            this.cardCobrado.ResumeLayout(false);
            this.cardPendiente.ResumeLayout(false);
            this.cardFacturas.ResumeLayout(false);
            this.cardBookings.ResumeLayout(false);
            this.cardCotizaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartFacturadoCobrado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFacturasEstado)).EndInit();
            this.tabFinanzas.ResumeLayout(false);
            this.layoutFinanzas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartFacturadoMes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            this.tabOperaciones.ResumeLayout(false);
            this.layoutOperaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartBookingsEstado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperaciones)).EndInit();
            this.tabAlertas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlertas)).EndInit();
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
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblUltimaActualizacion;
        private System.Windows.Forms.TabControl tabDashboard;
        private System.Windows.Forms.TabPage tabResumen;
        private System.Windows.Forms.TabPage tabFinanzas;
        private System.Windows.Forms.TabPage tabOperaciones;
        private System.Windows.Forms.TabPage tabAlertas;
        private System.Windows.Forms.TableLayoutPanel layoutResumen;
        private System.Windows.Forms.Panel cardFacturado = new System.Windows.Forms.Panel();
        private System.Windows.Forms.Label lblFacturadoValor = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblFacturadoTitulo = new System.Windows.Forms.Label();
        private System.Windows.Forms.Panel cardCobrado = new System.Windows.Forms.Panel();
        private System.Windows.Forms.Label lblCobradoValor = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblCobradoTitulo = new System.Windows.Forms.Label();
        private System.Windows.Forms.Panel cardPendiente = new System.Windows.Forms.Panel();
        private System.Windows.Forms.Label lblPendienteValor = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblPendienteTitulo = new System.Windows.Forms.Label();
        private System.Windows.Forms.Panel cardFacturas = new System.Windows.Forms.Panel();
        private System.Windows.Forms.Label lblFacturasValor = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblFacturasTitulo = new System.Windows.Forms.Label();
        private System.Windows.Forms.Panel cardBookings = new System.Windows.Forms.Panel();
        private System.Windows.Forms.Label lblBookingsValor = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblBookingsTitulo = new System.Windows.Forms.Label();
        private System.Windows.Forms.Panel cardCotizaciones = new System.Windows.Forms.Panel();
        private System.Windows.Forms.Label lblCotizacionesValor = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label lblCotizacionesTitulo = new System.Windows.Forms.Label();
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFacturadoCobrado;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFacturasEstado;
        private System.Windows.Forms.TableLayoutPanel layoutFinanzas;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFacturadoMes;
        private System.Windows.Forms.DataGridView dgvFacturas;
        private System.Windows.Forms.TableLayoutPanel layoutOperaciones;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBookingsEstado;
        private System.Windows.Forms.DataGridView dgvOperaciones;
        private System.Windows.Forms.DataGridView dgvAlertas;
    }
}
