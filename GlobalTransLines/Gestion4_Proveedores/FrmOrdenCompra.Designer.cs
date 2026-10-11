namespace GlobalTransLines.UI.Gestion4_Proveedores
{
    partial class FrmOrdenCompra
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControlOrdenCompra = new System.Windows.Forms.TabControl();
            this.tabGenerarOrdenCompra = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelGenerarOC = new System.Windows.Forms.TableLayoutPanel();
            this.gbRequerimientosPendientes = new System.Windows.Forms.GroupBox();
            this.dgvRequerimientos = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanelDatosOC = new System.Windows.Forms.TableLayoutPanel();
            this.gbDatosOrdenCompra = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelFormularioOC = new System.Windows.Forms.TableLayoutPanel();
            this.lblNumeroOrden = new System.Windows.Forms.Label();
            this.txtNumeroOrden = new System.Windows.Forms.TextBox();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.cmbProveedor = new System.Windows.Forms.ComboBox();
            this.lblFechaEmision = new System.Windows.Forms.Label();
            this.dtpFechaEmision = new System.Windows.Forms.DateTimePicker();
            this.lblImporteTotal = new System.Windows.Forms.Label();
            this.txtImporteTotal = new System.Windows.Forms.TextBox();
            this.gbDetalleRequerimiento = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelDetalleReq = new System.Windows.Forms.TableLayoutPanel();
            this.lblBooking = new System.Windows.Forms.Label();
            this.txtBooking = new System.Windows.Forms.TextBox();
            this.lblServicio = new System.Windows.Forms.Label();
            this.txtServicio = new System.Windows.Forms.TextBox();
            this.lblEstadoRequerimiento = new System.Windows.Forms.Label();
            this.txtEstadoRequerimiento = new System.Windows.Forms.TextBox();
            this.lblFechaSolicitud = new System.Windows.Forms.Label();
            this.txtFechaSolicitud = new System.Windows.Forms.TextBox();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnAnularRequerimiento = new System.Windows.Forms.Button();
            this.btnGenerarOC = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.tabOrdenesRegistradas = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelOrdenesRegistradas = new System.Windows.Forms.TableLayoutPanel();
            this.gbOrdenesCompra = new System.Windows.Forms.GroupBox();
            this.dgvOrdenesCompra = new System.Windows.Forms.DataGridView();
            this.gbDetalleOrdenCompra = new System.Windows.Forms.GroupBox();
            this.txtDetalleOrden = new System.Windows.Forms.TextBox();
            this.gbBotonesOrdenes = new System.Windows.Forms.GroupBox();
            this.btnRegistrarPago = new System.Windows.Forms.Button();
            this.btnConfirmarRecepcion = new System.Windows.Forms.Button();
            this.btnCerrarRegistradas = new System.Windows.Forms.Button();
            this.tabControlOrdenCompra.SuspendLayout();
            this.tabGenerarOrdenCompra.SuspendLayout();
            this.tableLayoutPanelGenerarOC.SuspendLayout();
            this.gbRequerimientosPendientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequerimientos)).BeginInit();
            this.tableLayoutPanelDatosOC.SuspendLayout();
            this.gbDatosOrdenCompra.SuspendLayout();
            this.tableLayoutPanelFormularioOC.SuspendLayout();
            this.gbDetalleRequerimiento.SuspendLayout();
            this.tableLayoutPanelDetalleReq.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.tabOrdenesRegistradas.SuspendLayout();
            this.tableLayoutPanelOrdenesRegistradas.SuspendLayout();
            this.gbOrdenesCompra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesCompra)).BeginInit();
            this.gbDetalleOrdenCompra.SuspendLayout();
            this.gbBotonesOrdenes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlOrdenCompra
            // 
            this.tabControlOrdenCompra.Controls.Add(this.tabGenerarOrdenCompra);
            this.tabControlOrdenCompra.Controls.Add(this.tabOrdenesRegistradas);
            this.tabControlOrdenCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlOrdenCompra.Location = new System.Drawing.Point(0, 0);
            this.tabControlOrdenCompra.Name = "tabControlOrdenCompra";
            this.tabControlOrdenCompra.SelectedIndex = 0;
            this.tabControlOrdenCompra.Size = new System.Drawing.Size(1269, 662);
            this.tabControlOrdenCompra.TabIndex = 0;
            // 
            // tabGenerarOrdenCompra
            // 
            this.tabGenerarOrdenCompra.Controls.Add(this.tableLayoutPanelGenerarOC);
            this.tabGenerarOrdenCompra.Location = new System.Drawing.Point(4, 25);
            this.tabGenerarOrdenCompra.Name = "tabGenerarOrdenCompra";
            this.tabGenerarOrdenCompra.Padding = new System.Windows.Forms.Padding(3);
            this.tabGenerarOrdenCompra.Size = new System.Drawing.Size(1261, 633);
            this.tabGenerarOrdenCompra.TabIndex = 0;
            this.tabGenerarOrdenCompra.Text = "Generar Orden de Compra";
            this.tabGenerarOrdenCompra.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelGenerarOC
            // 
            this.tableLayoutPanelGenerarOC.ColumnCount = 1;
            this.tableLayoutPanelGenerarOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelGenerarOC.Controls.Add(this.gbRequerimientosPendientes, 0, 0);
            this.tableLayoutPanelGenerarOC.Controls.Add(this.tableLayoutPanelDatosOC, 0, 1);
            this.tableLayoutPanelGenerarOC.Controls.Add(this.panelBotones, 0, 2);
            this.tableLayoutPanelGenerarOC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGenerarOC.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelGenerarOC.Name = "tableLayoutPanelGenerarOC";
            this.tableLayoutPanelGenerarOC.RowCount = 3;
            this.tableLayoutPanelGenerarOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGenerarOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelGenerarOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanelGenerarOC.Size = new System.Drawing.Size(1255, 627);
            this.tableLayoutPanelGenerarOC.TabIndex = 0;
            // 
            // gbRequerimientosPendientes
            // 
            this.gbRequerimientosPendientes.Controls.Add(this.dgvRequerimientos);
            this.gbRequerimientosPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRequerimientosPendientes.Location = new System.Drawing.Point(3, 3);
            this.gbRequerimientosPendientes.Name = "gbRequerimientosPendientes";
            this.gbRequerimientosPendientes.Size = new System.Drawing.Size(1249, 272);
            this.gbRequerimientosPendientes.TabIndex = 0;
            this.gbRequerimientosPendientes.TabStop = false;
            this.gbRequerimientosPendientes.Text = "Requerimientos pendientes desde Booking";
            // 
            // dgvRequerimientos
            // 
            this.dgvRequerimientos.AllowUserToAddRows = false;
            this.dgvRequerimientos.AllowUserToDeleteRows = false;
            this.dgvRequerimientos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRequerimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequerimientos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRequerimientos.Location = new System.Drawing.Point(3, 18);
            this.dgvRequerimientos.MultiSelect = false;
            this.dgvRequerimientos.Name = "dgvRequerimientos";
            this.dgvRequerimientos.ReadOnly = true;
            this.dgvRequerimientos.RowHeadersWidth = 51;
            this.dgvRequerimientos.RowTemplate.Height = 24;
            this.dgvRequerimientos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRequerimientos.Size = new System.Drawing.Size(1243, 251);
            this.dgvRequerimientos.TabIndex = 0;
            this.dgvRequerimientos.SelectionChanged += new System.EventHandler(this.dgvRequerimientos_SelectionChanged);
            // 
            // tableLayoutPanelDatosOC
            // 
            this.tableLayoutPanelDatosOC.ColumnCount = 2;
            this.tableLayoutPanelDatosOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelDatosOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanelDatosOC.Controls.Add(this.gbDatosOrdenCompra, 0, 0);
            this.tableLayoutPanelDatosOC.Controls.Add(this.gbDetalleRequerimiento, 1, 0);
            this.tableLayoutPanelDatosOC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDatosOC.Location = new System.Drawing.Point(3, 281);
            this.tableLayoutPanelDatosOC.Name = "tableLayoutPanelDatosOC";
            this.tableLayoutPanelDatosOC.RowCount = 1;
            this.tableLayoutPanelDatosOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDatosOC.Size = new System.Drawing.Size(1249, 272);
            this.tableLayoutPanelDatosOC.TabIndex = 1;
            // 
            // gbDatosOrdenCompra
            // 
            this.gbDatosOrdenCompra.Controls.Add(this.tableLayoutPanelFormularioOC);
            this.gbDatosOrdenCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDatosOrdenCompra.Location = new System.Drawing.Point(3, 3);
            this.gbDatosOrdenCompra.Name = "gbDatosOrdenCompra";
            this.gbDatosOrdenCompra.Size = new System.Drawing.Size(493, 266);
            this.gbDatosOrdenCompra.TabIndex = 0;
            this.gbDatosOrdenCompra.TabStop = false;
            this.gbDatosOrdenCompra.Text = "Datos de la Orden de Compra";
            // 
            // tableLayoutPanelFormularioOC
            // 
            this.tableLayoutPanelFormularioOC.ColumnCount = 2;
            this.tableLayoutPanelFormularioOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanelFormularioOC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFormularioOC.Controls.Add(this.lblNumeroOrden, 0, 0);
            this.tableLayoutPanelFormularioOC.Controls.Add(this.txtNumeroOrden, 1, 0);
            this.tableLayoutPanelFormularioOC.Controls.Add(this.lblProveedor, 0, 1);
            this.tableLayoutPanelFormularioOC.Controls.Add(this.cmbProveedor, 1, 1);
            this.tableLayoutPanelFormularioOC.Controls.Add(this.lblFechaEmision, 0, 2);
            this.tableLayoutPanelFormularioOC.Controls.Add(this.dtpFechaEmision, 1, 2);
            this.tableLayoutPanelFormularioOC.Controls.Add(this.lblImporteTotal, 0, 3);
            this.tableLayoutPanelFormularioOC.Controls.Add(this.txtImporteTotal, 1, 3);
            this.tableLayoutPanelFormularioOC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFormularioOC.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanelFormularioOC.Name = "tableLayoutPanelFormularioOC";
            this.tableLayoutPanelFormularioOC.RowCount = 5;
            this.tableLayoutPanelFormularioOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanelFormularioOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanelFormularioOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanelFormularioOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanelFormularioOC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFormularioOC.Size = new System.Drawing.Size(487, 245);
            this.tableLayoutPanelFormularioOC.TabIndex = 0;
            // 
            // lblNumeroOrden
            // 
            this.lblNumeroOrden.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNumeroOrden.AutoSize = true;
            this.lblNumeroOrden.Location = new System.Drawing.Point(3, 13);
            this.lblNumeroOrden.Name = "lblNumeroOrden";
            this.lblNumeroOrden.Size = new System.Drawing.Size(78, 16);
            this.lblNumeroOrden.TabIndex = 0;
            this.lblNumeroOrden.Text = "Numero OC";
            // 
            // txtNumeroOrden
            // 
            this.txtNumeroOrden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumeroOrden.Location = new System.Drawing.Point(138, 10);
            this.txtNumeroOrden.Name = "txtNumeroOrden";
            this.txtNumeroOrden.Size = new System.Drawing.Size(346, 22);
            this.txtNumeroOrden.TabIndex = 1;
            // 
            // lblProveedor
            // 
            this.lblProveedor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(3, 55);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(72, 16);
            this.lblProveedor.TabIndex = 2;
            this.lblProveedor.Text = "Proveedor";
            // 
            // cmbProveedor
            // 
            this.cmbProveedor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProveedor.FormattingEnabled = true;
            this.cmbProveedor.Location = new System.Drawing.Point(138, 51);
            this.cmbProveedor.Name = "cmbProveedor";
            this.cmbProveedor.Size = new System.Drawing.Size(346, 24);
            this.cmbProveedor.TabIndex = 3;
            // 
            // lblFechaEmision
            // 
            this.lblFechaEmision.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFechaEmision.AutoSize = true;
            this.lblFechaEmision.Location = new System.Drawing.Point(3, 97);
            this.lblFechaEmision.Name = "lblFechaEmision";
            this.lblFechaEmision.Size = new System.Drawing.Size(94, 16);
            this.lblFechaEmision.TabIndex = 4;
            this.lblFechaEmision.Text = "Fecha emision";
            // 
            // dtpFechaEmision
            // 
            this.dtpFechaEmision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaEmision.Location = new System.Drawing.Point(138, 94);
            this.dtpFechaEmision.Name = "dtpFechaEmision";
            this.dtpFechaEmision.Size = new System.Drawing.Size(346, 22);
            this.dtpFechaEmision.TabIndex = 5;
            // 
            // lblImporteTotal
            // 
            this.lblImporteTotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblImporteTotal.AutoSize = true;
            this.lblImporteTotal.Location = new System.Drawing.Point(3, 139);
            this.lblImporteTotal.Name = "lblImporteTotal";
            this.lblImporteTotal.Size = new System.Drawing.Size(82, 16);
            this.lblImporteTotal.TabIndex = 6;
            this.lblImporteTotal.Text = "Importe total";
            // 
            // txtImporteTotal
            // 
            this.txtImporteTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImporteTotal.Location = new System.Drawing.Point(138, 136);
            this.txtImporteTotal.Name = "txtImporteTotal";
            this.txtImporteTotal.Size = new System.Drawing.Size(346, 22);
            this.txtImporteTotal.TabIndex = 7;
            // 
            // gbDetalleRequerimiento
            // 
            this.gbDetalleRequerimiento.Controls.Add(this.tableLayoutPanelDetalleReq);
            this.gbDetalleRequerimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetalleRequerimiento.Location = new System.Drawing.Point(502, 3);
            this.gbDetalleRequerimiento.Name = "gbDetalleRequerimiento";
            this.gbDetalleRequerimiento.Size = new System.Drawing.Size(744, 266);
            this.gbDetalleRequerimiento.TabIndex = 1;
            this.gbDetalleRequerimiento.TabStop = false;
            this.gbDetalleRequerimiento.Text = "Detalle del requerimiento";
            // 
            // tableLayoutPanelDetalleReq
            // 
            this.tableLayoutPanelDetalleReq.ColumnCount = 2;
            this.tableLayoutPanelDetalleReq.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanelDetalleReq.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDetalleReq.Controls.Add(this.lblBooking, 0, 0);
            this.tableLayoutPanelDetalleReq.Controls.Add(this.txtBooking, 1, 0);
            this.tableLayoutPanelDetalleReq.Controls.Add(this.lblServicio, 0, 1);
            this.tableLayoutPanelDetalleReq.Controls.Add(this.txtServicio, 1, 1);
            this.tableLayoutPanelDetalleReq.Controls.Add(this.lblEstadoRequerimiento, 0, 2);
            this.tableLayoutPanelDetalleReq.Controls.Add(this.txtEstadoRequerimiento, 1, 2);
            this.tableLayoutPanelDetalleReq.Controls.Add(this.lblFechaSolicitud, 0, 3);
            this.tableLayoutPanelDetalleReq.Controls.Add(this.txtFechaSolicitud, 1, 3);
            this.tableLayoutPanelDetalleReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDetalleReq.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanelDetalleReq.Name = "tableLayoutPanelDetalleReq";
            this.tableLayoutPanelDetalleReq.RowCount = 5;
            this.tableLayoutPanelDetalleReq.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanelDetalleReq.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanelDetalleReq.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanelDetalleReq.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanelDetalleReq.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDetalleReq.Size = new System.Drawing.Size(738, 245);
            this.tableLayoutPanelDetalleReq.TabIndex = 0;
            // 
            // lblBooking
            // 
            this.lblBooking.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBooking.AutoSize = true;
            this.lblBooking.Location = new System.Drawing.Point(3, 13);
            this.lblBooking.Name = "lblBooking";
            this.lblBooking.Size = new System.Drawing.Size(55, 16);
            this.lblBooking.TabIndex = 0;
            this.lblBooking.Text = "Booking";
            // 
            // txtBooking
            // 
            this.txtBooking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBooking.Location = new System.Drawing.Point(153, 10);
            this.txtBooking.Name = "txtBooking";
            this.txtBooking.Size = new System.Drawing.Size(582, 22);
            this.txtBooking.TabIndex = 1;
            // 
            // lblServicio
            // 
            this.lblServicio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblServicio.AutoSize = true;
            this.lblServicio.Location = new System.Drawing.Point(3, 55);
            this.lblServicio.Name = "lblServicio";
            this.lblServicio.Size = new System.Drawing.Size(56, 16);
            this.lblServicio.TabIndex = 2;
            this.lblServicio.Text = "Servicio";
            // 
            // txtServicio
            // 
            this.txtServicio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtServicio.Location = new System.Drawing.Point(153, 52);
            this.txtServicio.Name = "txtServicio";
            this.txtServicio.Size = new System.Drawing.Size(582, 22);
            this.txtServicio.TabIndex = 3;
            // 
            // lblEstadoRequerimiento
            // 
            this.lblEstadoRequerimiento.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEstadoRequerimiento.AutoSize = true;
            this.lblEstadoRequerimiento.Location = new System.Drawing.Point(3, 97);
            this.lblEstadoRequerimiento.Name = "lblEstadoRequerimiento";
            this.lblEstadoRequerimiento.Size = new System.Drawing.Size(50, 16);
            this.lblEstadoRequerimiento.TabIndex = 4;
            this.lblEstadoRequerimiento.Text = "Estado";
            // 
            // txtEstadoRequerimiento
            // 
            this.txtEstadoRequerimiento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEstadoRequerimiento.Location = new System.Drawing.Point(153, 94);
            this.txtEstadoRequerimiento.Name = "txtEstadoRequerimiento";
            this.txtEstadoRequerimiento.Size = new System.Drawing.Size(582, 22);
            this.txtEstadoRequerimiento.TabIndex = 5;
            // 
            // lblFechaSolicitud
            // 
            this.lblFechaSolicitud.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFechaSolicitud.AutoSize = true;
            this.lblFechaSolicitud.Location = new System.Drawing.Point(3, 139);
            this.lblFechaSolicitud.Name = "lblFechaSolicitud";
            this.lblFechaSolicitud.Size = new System.Drawing.Size(98, 16);
            this.lblFechaSolicitud.TabIndex = 6;
            this.lblFechaSolicitud.Text = "Fecha solicitud";
            // 
            // txtFechaSolicitud
            // 
            this.txtFechaSolicitud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFechaSolicitud.Location = new System.Drawing.Point(153, 136);
            this.txtFechaSolicitud.Name = "txtFechaSolicitud";
            this.txtFechaSolicitud.Size = new System.Drawing.Size(582, 22);
            this.txtFechaSolicitud.TabIndex = 7;
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnLimpiar);
            this.panelBotones.Controls.Add(this.btnActualizar);
            this.panelBotones.Controls.Add(this.btnAnularRequerimiento);
            this.panelBotones.Controls.Add(this.btnGenerarOC);
            this.panelBotones.Controls.Add(this.btnCerrar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBotones.Location = new System.Drawing.Point(3, 559);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1249, 65);
            this.panelBotones.TabIndex = 2;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiar.Location = new System.Drawing.Point(1125, 16);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(110, 34);
            this.btnLimpiar.TabIndex = 3;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualizar.Location = new System.Drawing.Point(1009, 16);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(110, 34);
            this.btnActualizar.TabIndex = 2;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnAnularRequerimiento
            // 
            this.btnAnularRequerimiento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnularRequerimiento.Location = new System.Drawing.Point(811, 16);
            this.btnAnularRequerimiento.Name = "btnAnularRequerimiento";
            this.btnAnularRequerimiento.Size = new System.Drawing.Size(192, 34);
            this.btnAnularRequerimiento.TabIndex = 1;
            this.btnAnularRequerimiento.Text = "Anular requerimiento";
            this.btnAnularRequerimiento.UseVisualStyleBackColor = true;
            this.btnAnularRequerimiento.Click += new System.EventHandler(this.btnAnularRequerimiento_Click);
            // 
            // btnGenerarOC
            // 
            this.btnGenerarOC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarOC.Location = new System.Drawing.Point(622, 16);
            this.btnGenerarOC.Name = "btnGenerarOC";
            this.btnGenerarOC.Size = new System.Drawing.Size(183, 34);
            this.btnGenerarOC.TabIndex = 0;
            this.btnGenerarOC.Text = "Generar OC";
            this.btnGenerarOC.UseVisualStyleBackColor = true;
            this.btnGenerarOC.Click += new System.EventHandler(this.btnGenerarOC_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Location = new System.Drawing.Point(506, 16);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(110, 34);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // tabOrdenesRegistradas
            // 
            this.tabOrdenesRegistradas.Controls.Add(this.tableLayoutPanelOrdenesRegistradas);
            this.tabOrdenesRegistradas.Location = new System.Drawing.Point(4, 25);
            this.tabOrdenesRegistradas.Name = "tabOrdenesRegistradas";
            this.tabOrdenesRegistradas.Padding = new System.Windows.Forms.Padding(3);
            this.tabOrdenesRegistradas.Size = new System.Drawing.Size(1261, 633);
            this.tabOrdenesRegistradas.TabIndex = 1;
            this.tabOrdenesRegistradas.Text = "Ordenes registradas";
            this.tabOrdenesRegistradas.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelOrdenesRegistradas
            // 
            this.tableLayoutPanelOrdenesRegistradas.ColumnCount = 1;
            this.tableLayoutPanelOrdenesRegistradas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelOrdenesRegistradas.Controls.Add(this.gbOrdenesCompra, 0, 0);
            this.tableLayoutPanelOrdenesRegistradas.Controls.Add(this.gbDetalleOrdenCompra, 0, 1);
            this.tableLayoutPanelOrdenesRegistradas.Controls.Add(this.gbBotonesOrdenes, 0, 2);
            this.tableLayoutPanelOrdenesRegistradas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelOrdenesRegistradas.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelOrdenesRegistradas.Name = "tableLayoutPanelOrdenesRegistradas";
            this.tableLayoutPanelOrdenesRegistradas.RowCount = 3;
            this.tableLayoutPanelOrdenesRegistradas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanelOrdenesRegistradas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanelOrdenesRegistradas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanelOrdenesRegistradas.Size = new System.Drawing.Size(1255, 627);
            this.tableLayoutPanelOrdenesRegistradas.TabIndex = 0;
            // 
            // gbOrdenesCompra
            // 
            this.gbOrdenesCompra.Controls.Add(this.dgvOrdenesCompra);
            this.gbOrdenesCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOrdenesCompra.Location = new System.Drawing.Point(3, 3);
            this.gbOrdenesCompra.Name = "gbOrdenesCompra";
            this.gbOrdenesCompra.Size = new System.Drawing.Size(1249, 298);
            this.gbOrdenesCompra.TabIndex = 0;
            this.gbOrdenesCompra.TabStop = false;
            this.gbOrdenesCompra.Text = "Ordenes de compra registradas";
            // 
            // dgvOrdenesCompra
            // 
            this.dgvOrdenesCompra.AllowUserToAddRows = false;
            this.dgvOrdenesCompra.AllowUserToDeleteRows = false;
            this.dgvOrdenesCompra.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrdenesCompra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenesCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenesCompra.Location = new System.Drawing.Point(3, 18);
            this.dgvOrdenesCompra.MultiSelect = false;
            this.dgvOrdenesCompra.Name = "dgvOrdenesCompra";
            this.dgvOrdenesCompra.ReadOnly = true;
            this.dgvOrdenesCompra.RowHeadersWidth = 51;
            this.dgvOrdenesCompra.RowTemplate.Height = 24;
            this.dgvOrdenesCompra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenesCompra.Size = new System.Drawing.Size(1243, 277);
            this.dgvOrdenesCompra.TabIndex = 0;
            this.dgvOrdenesCompra.SelectionChanged += new System.EventHandler(this.dgvOrdenesCompra_SelectionChanged);
            // 
            // gbDetalleOrdenCompra
            // 
            this.gbDetalleOrdenCompra.Controls.Add(this.txtDetalleOrden);
            this.gbDetalleOrdenCompra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetalleOrdenCompra.Location = new System.Drawing.Point(3, 307);
            this.gbDetalleOrdenCompra.Name = "gbDetalleOrdenCompra";
            this.gbDetalleOrdenCompra.Size = new System.Drawing.Size(1249, 243);
            this.gbDetalleOrdenCompra.TabIndex = 1;
            this.gbDetalleOrdenCompra.TabStop = false;
            this.gbDetalleOrdenCompra.Text = "Detalle de la OC seleccionada";
            // 
            // txtDetalleOrden
            // 
            this.txtDetalleOrden.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetalleOrden.Location = new System.Drawing.Point(3, 18);
            this.txtDetalleOrden.Multiline = true;
            this.txtDetalleOrden.Name = "txtDetalleOrden";
            this.txtDetalleOrden.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetalleOrden.Size = new System.Drawing.Size(1243, 222);
            this.txtDetalleOrden.TabIndex = 0;
            // 
            // gbBotonesOrdenes
            // 
            this.gbBotonesOrdenes.Controls.Add(this.btnRegistrarPago);
            this.gbBotonesOrdenes.Controls.Add(this.btnConfirmarRecepcion);
            this.gbBotonesOrdenes.Controls.Add(this.btnCerrarRegistradas);
            this.gbBotonesOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBotonesOrdenes.Location = new System.Drawing.Point(3, 556);
            this.gbBotonesOrdenes.Name = "gbBotonesOrdenes";
            this.gbBotonesOrdenes.Size = new System.Drawing.Size(1249, 68);
            this.gbBotonesOrdenes.TabIndex = 2;
            this.gbBotonesOrdenes.TabStop = false;
            // 
            // btnRegistrarPago
            // 
            this.btnRegistrarPago.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegistrarPago.Location = new System.Drawing.Point(1041, 21);
            this.btnRegistrarPago.Name = "btnRegistrarPago";
            this.btnRegistrarPago.Size = new System.Drawing.Size(195, 34);
            this.btnRegistrarPago.TabIndex = 1;
            this.btnRegistrarPago.Text = "Registrar pago";
            this.btnRegistrarPago.UseVisualStyleBackColor = true;
            this.btnRegistrarPago.Click += new System.EventHandler(this.btnRegistrarPago_Click);
            // 
            // btnConfirmarRecepcion
            // 
            this.btnConfirmarRecepcion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmarRecepcion.Location = new System.Drawing.Point(822, 21);
            this.btnConfirmarRecepcion.Name = "btnConfirmarRecepcion";
            this.btnConfirmarRecepcion.Size = new System.Drawing.Size(213, 34);
            this.btnConfirmarRecepcion.TabIndex = 0;
            this.btnConfirmarRecepcion.Text = "Confirmar recepcion";
            this.btnConfirmarRecepcion.UseVisualStyleBackColor = true;
            this.btnConfirmarRecepcion.Click += new System.EventHandler(this.btnConfirmarRecepcion_Click);
            // 
            // btnCerrarRegistradas
            // 
            this.btnCerrarRegistradas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrarRegistradas.Location = new System.Drawing.Point(706, 21);
            this.btnCerrarRegistradas.Name = "btnCerrarRegistradas";
            this.btnCerrarRegistradas.Size = new System.Drawing.Size(110, 34);
            this.btnCerrarRegistradas.TabIndex = 2;
            this.btnCerrarRegistradas.Text = "Cerrar";
            this.btnCerrarRegistradas.UseVisualStyleBackColor = true;
            this.btnCerrarRegistradas.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FrmOrdenCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 662);
            this.Controls.Add(this.tabControlOrdenCompra);
            this.Name = "FrmOrdenCompra";
            this.Tag = "FrmOrdenCompra";
            this.Text = "Ordenes de compra";
            this.Load += new System.EventHandler(this.FrmOrdenCompra_Load);
            this.tabControlOrdenCompra.ResumeLayout(false);
            this.tabGenerarOrdenCompra.ResumeLayout(false);
            this.tableLayoutPanelGenerarOC.ResumeLayout(false);
            this.gbRequerimientosPendientes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequerimientos)).EndInit();
            this.tableLayoutPanelDatosOC.ResumeLayout(false);
            this.gbDatosOrdenCompra.ResumeLayout(false);
            this.tableLayoutPanelFormularioOC.ResumeLayout(false);
            this.tableLayoutPanelFormularioOC.PerformLayout();
            this.gbDetalleRequerimiento.ResumeLayout(false);
            this.tableLayoutPanelDetalleReq.ResumeLayout(false);
            this.tableLayoutPanelDetalleReq.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.tabOrdenesRegistradas.ResumeLayout(false);
            this.tableLayoutPanelOrdenesRegistradas.ResumeLayout(false);
            this.gbOrdenesCompra.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesCompra)).EndInit();
            this.gbDetalleOrdenCompra.ResumeLayout(false);
            this.gbDetalleOrdenCompra.PerformLayout();
            this.gbBotonesOrdenes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlOrdenCompra;
        private System.Windows.Forms.TabPage tabGenerarOrdenCompra;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGenerarOC;
        private System.Windows.Forms.GroupBox gbRequerimientosPendientes;
        private System.Windows.Forms.DataGridView dgvRequerimientos;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDatosOC;
        private System.Windows.Forms.GroupBox gbDatosOrdenCompra;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFormularioOC;
        private System.Windows.Forms.Label lblNumeroOrden;
        private System.Windows.Forms.TextBox txtNumeroOrden;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.ComboBox cmbProveedor;
        private System.Windows.Forms.Label lblFechaEmision;
        private System.Windows.Forms.DateTimePicker dtpFechaEmision;
        private System.Windows.Forms.Label lblImporteTotal;
        private System.Windows.Forms.TextBox txtImporteTotal;
        private System.Windows.Forms.GroupBox gbDetalleRequerimiento;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDetalleReq;
        private System.Windows.Forms.Label lblBooking;
        private System.Windows.Forms.TextBox txtBooking;
        private System.Windows.Forms.Label lblServicio;
        private System.Windows.Forms.TextBox txtServicio;
        private System.Windows.Forms.Label lblEstadoRequerimiento;
        private System.Windows.Forms.TextBox txtEstadoRequerimiento;
        private System.Windows.Forms.Label lblFechaSolicitud;
        private System.Windows.Forms.TextBox txtFechaSolicitud;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnAnularRequerimiento;
        private System.Windows.Forms.Button btnGenerarOC;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TabPage tabOrdenesRegistradas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOrdenesRegistradas;
        private System.Windows.Forms.GroupBox gbOrdenesCompra;
        private System.Windows.Forms.DataGridView dgvOrdenesCompra;
        private System.Windows.Forms.GroupBox gbDetalleOrdenCompra;
        private System.Windows.Forms.TextBox txtDetalleOrden;
        private System.Windows.Forms.GroupBox gbBotonesOrdenes;
        private System.Windows.Forms.Button btnRegistrarPago;
        private System.Windows.Forms.Button btnConfirmarRecepcion;
        private System.Windows.Forms.Button btnCerrarRegistradas;
    }
}
