namespace GlobalTransLines.UI.Gestion3_Facturacion
{
    partial class FrmPago
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
            this.tabRegistrarPago = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbFacturasPendientes = new System.Windows.Forms.GroupBox();
            this.dgvFacturas = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gbDatosPago = new System.Windows.Forms.GroupBox();
            this.cmbMedioPago = new System.Windows.Forms.ComboBox();
            this.lblMedioPago = new System.Windows.Forms.Label();
            this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
            this.lblFechaPago = new System.Windows.Forms.Label();
            this.txtImportePago = new System.Windows.Forms.TextBox();
            this.lblImporte = new System.Windows.Forms.Label();
            this.txtTotalFactura = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.lblFactura = new System.Windows.Forms.Label();
            this.gbDetalleMedioPago = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.gbTarjeta = new System.Windows.Forms.GroupBox();
            this.txtBancoEmisor = new System.Windows.Forms.TextBox();
            this.dtpVencimiento = new System.Windows.Forms.DateTimePicker();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.txtCVV = new System.Windows.Forms.TextBox();
            this.txtTitular = new System.Windows.Forms.TextBox();
            this.txtNumeroTarjeta = new System.Windows.Forms.TextBox();
            this.lblBancoEmisor = new System.Windows.Forms.Label();
            this.lblVencimiento = new System.Windows.Forms.Label();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblCVV = new System.Windows.Forms.Label();
            this.lblTitular = new System.Windows.Forms.Label();
            this.lblNumeroTarjeta = new System.Windows.Forms.Label();
            this.gbTransferencia = new System.Windows.Forms.GroupBox();
            this.txtNumeroOperacion = new System.Windows.Forms.TextBox();
            this.lblNumeroOperacion = new System.Windows.Forms.Label();
            this.txtCBU = new System.Windows.Forms.TextBox();
            this.lblCBU = new System.Windows.Forms.Label();
            this.txtBanco = new System.Windows.Forms.TextBox();
            this.lblBanco = new System.Windows.Forms.Label();
            this.gbEfectivo = new System.Windows.Forms.GroupBox();
            this.txtVuelto = new System.Windows.Forms.TextBox();
            this.lblVuelto = new System.Windows.Forms.Label();
            this.txtMontoEntregado = new System.Windows.Forms.TextBox();
            this.lblMontoEntregado = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnRegistrarPago = new System.Windows.Forms.Button();
            this.tabControlPagos = new System.Windows.Forms.TabControl();
            this.tabPagosRegistrados = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelPagosRegistrados = new System.Windows.Forms.TableLayoutPanel();
            this.gbPagosRegistrados = new System.Windows.Forms.GroupBox();
            this.dgvPagos = new System.Windows.Forms.DataGridView();
            this.gbDetallePago = new System.Windows.Forms.GroupBox();
            this.rtbDetallePago = new System.Windows.Forms.RichTextBox();
            this.gbBotonesPagosRegistrados = new System.Windows.Forms.GroupBox();
            this.btnActualizar2 = new System.Windows.Forms.Button();
            this.btnImprimirComprobante = new System.Windows.Forms.Button();
            this.tabRegistrarPago.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbFacturasPendientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.gbDatosPago.SuspendLayout();
            this.gbDetalleMedioPago.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.gbTarjeta.SuspendLayout();
            this.gbTransferencia.SuspendLayout();
            this.gbEfectivo.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.tabControlPagos.SuspendLayout();
            this.tabPagosRegistrados.SuspendLayout();
            this.tableLayoutPanelPagosRegistrados.SuspendLayout();
            this.gbPagosRegistrados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).BeginInit();
            this.gbDetallePago.SuspendLayout();
            this.gbBotonesPagosRegistrados.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabRegistrarPago
            // 
            this.tabRegistrarPago.Controls.Add(this.tableLayoutPanel1);
            this.tabRegistrarPago.Location = new System.Drawing.Point(4, 25);
            this.tabRegistrarPago.Name = "tabRegistrarPago";
            this.tabRegistrarPago.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegistrarPago.Size = new System.Drawing.Size(1261, 633);
            this.tabRegistrarPago.TabIndex = 0;
            this.tabRegistrarPago.Text = "Registrar Pago";
            this.tabRegistrarPago.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gbFacturasPendientes, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelBotones, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1255, 627);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gbFacturasPendientes
            // 
            this.gbFacturasPendientes.Controls.Add(this.dgvFacturas);
            this.gbFacturasPendientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFacturasPendientes.Location = new System.Drawing.Point(3, 3);
            this.gbFacturasPendientes.Name = "gbFacturasPendientes";
            this.gbFacturasPendientes.Size = new System.Drawing.Size(1249, 272);
            this.gbFacturasPendientes.TabIndex = 0;
            this.gbFacturasPendientes.TabStop = false;
            this.gbFacturasPendientes.Text = "Facturas pendientes de pago";
            // 
            // dgvFacturas
            // 
            this.dgvFacturas.AllowUserToAddRows = false;
            this.dgvFacturas.AllowUserToDeleteRows = false;
            this.dgvFacturas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFacturas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFacturas.Location = new System.Drawing.Point(3, 18);
            this.dgvFacturas.MultiSelect = false;
            this.dgvFacturas.Name = "dgvFacturas";
            this.dgvFacturas.ReadOnly = true;
            this.dgvFacturas.RowHeadersWidth = 51;
            this.dgvFacturas.RowTemplate.Height = 24;
            this.dgvFacturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFacturas.Size = new System.Drawing.Size(1243, 251);
            this.dgvFacturas.TabIndex = 0;
            this.dgvFacturas.SelectionChanged += new System.EventHandler(this.dgvFacturas_SelectionChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.Controls.Add(this.gbDatosPago, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.gbDetalleMedioPago, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 281);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1249, 272);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // gbDatosPago
            // 
            this.gbDatosPago.AutoSize = true;
            this.gbDatosPago.Controls.Add(this.cmbMedioPago);
            this.gbDatosPago.Controls.Add(this.lblMedioPago);
            this.gbDatosPago.Controls.Add(this.dtpFechaPago);
            this.gbDatosPago.Controls.Add(this.lblFechaPago);
            this.gbDatosPago.Controls.Add(this.txtImportePago);
            this.gbDatosPago.Controls.Add(this.lblImporte);
            this.gbDatosPago.Controls.Add(this.txtTotalFactura);
            this.gbDatosPago.Controls.Add(this.lblTotal);
            this.gbDatosPago.Controls.Add(this.txtFactura);
            this.gbDatosPago.Controls.Add(this.lblFactura);
            this.gbDatosPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDatosPago.Location = new System.Drawing.Point(3, 3);
            this.gbDatosPago.Name = "gbDatosPago";
            this.gbDatosPago.Size = new System.Drawing.Size(493, 266);
            this.gbDatosPago.TabIndex = 0;
            this.gbDatosPago.TabStop = false;
            this.gbDatosPago.Text = "Datos del pago";
            // 
            // cmbMedioPago
            // 
            this.cmbMedioPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedioPago.FormattingEnabled = true;
            this.cmbMedioPago.Location = new System.Drawing.Point(114, 200);
            this.cmbMedioPago.Name = "cmbMedioPago";
            this.cmbMedioPago.Size = new System.Drawing.Size(210, 24);
            this.cmbMedioPago.TabIndex = 9;
            this.cmbMedioPago.SelectedIndexChanged += new System.EventHandler(this.cmbMedioPago_SelectedIndexChanged);
            // 
            // lblMedioPago
            // 
            this.lblMedioPago.AutoSize = true;
            this.lblMedioPago.Location = new System.Drawing.Point(6, 203);
            this.lblMedioPago.Name = "lblMedioPago";
            this.lblMedioPago.Size = new System.Drawing.Size(102, 16);
            this.lblMedioPago.TabIndex = 8;
            this.lblMedioPago.Text = "Medio de pago:";
            // 
            // dtpFechaPago
            // 
            this.dtpFechaPago.Location = new System.Drawing.Point(114, 161);
            this.dtpFechaPago.Name = "dtpFechaPago";
            this.dtpFechaPago.Size = new System.Drawing.Size(210, 22);
            this.dtpFechaPago.TabIndex = 7;
            // 
            // lblFechaPago
            // 
            this.lblFechaPago.AutoSize = true;
            this.lblFechaPago.Location = new System.Drawing.Point(6, 161);
            this.lblFechaPago.Name = "lblFechaPago";
            this.lblFechaPago.Size = new System.Drawing.Size(83, 16);
            this.lblFechaPago.TabIndex = 6;
            this.lblFechaPago.Text = "Fecha pago:";
            // 
            // txtImportePago
            // 
            this.txtImportePago.Location = new System.Drawing.Point(114, 118);
            this.txtImportePago.Name = "txtImportePago";
            this.txtImportePago.Size = new System.Drawing.Size(210, 22);
            this.txtImportePago.TabIndex = 5;
            // 
            // lblImporte
            // 
            this.lblImporte.AutoSize = true;
            this.lblImporte.Location = new System.Drawing.Point(6, 118);
            this.lblImporte.Name = "lblImporte";
            this.lblImporte.Size = new System.Drawing.Size(90, 16);
            this.lblImporte.TabIndex = 4;
            this.lblImporte.Text = "Importe pago:";
            // 
            // txtTotalFactura
            // 
            this.txtTotalFactura.Location = new System.Drawing.Point(114, 79);
            this.txtTotalFactura.Name = "txtTotalFactura";
            this.txtTotalFactura.ReadOnly = true;
            this.txtTotalFactura.Size = new System.Drawing.Size(210, 22);
            this.txtTotalFactura.TabIndex = 3;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(6, 82);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(84, 16);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "Total factura:";
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(114, 38);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.ReadOnly = true;
            this.txtFactura.Size = new System.Drawing.Size(210, 22);
            this.txtFactura.TabIndex = 1;
            // 
            // lblFactura
            // 
            this.lblFactura.AutoSize = true;
            this.lblFactura.Location = new System.Drawing.Point(6, 38);
            this.lblFactura.Name = "lblFactura";
            this.lblFactura.Size = new System.Drawing.Size(55, 16);
            this.lblFactura.TabIndex = 0;
            this.lblFactura.Text = "Factura:";
            // 
            // gbDetalleMedioPago
            // 
            this.gbDetalleMedioPago.Controls.Add(this.tableLayoutPanel3);
            this.gbDetalleMedioPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetalleMedioPago.Location = new System.Drawing.Point(502, 3);
            this.gbDetalleMedioPago.Name = "gbDetalleMedioPago";
            this.gbDetalleMedioPago.Size = new System.Drawing.Size(744, 266);
            this.gbDetalleMedioPago.TabIndex = 1;
            this.gbDetalleMedioPago.TabStop = false;
            this.gbDetalleMedioPago.Text = "Detalle del medio de pago";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.gbTarjeta, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.gbTransferencia, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.gbEfectivo, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(738, 245);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // gbTarjeta
            // 
            this.gbTarjeta.Controls.Add(this.txtBancoEmisor);
            this.gbTarjeta.Controls.Add(this.dtpVencimiento);
            this.gbTarjeta.Controls.Add(this.txtMarca);
            this.gbTarjeta.Controls.Add(this.txtCVV);
            this.gbTarjeta.Controls.Add(this.txtTitular);
            this.gbTarjeta.Controls.Add(this.txtNumeroTarjeta);
            this.gbTarjeta.Controls.Add(this.lblBancoEmisor);
            this.gbTarjeta.Controls.Add(this.lblVencimiento);
            this.gbTarjeta.Controls.Add(this.lblMarca);
            this.gbTarjeta.Controls.Add(this.lblCVV);
            this.gbTarjeta.Controls.Add(this.lblTitular);
            this.gbTarjeta.Controls.Add(this.lblNumeroTarjeta);
            this.gbTarjeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTarjeta.Location = new System.Drawing.Point(3, 149);
            this.gbTarjeta.Name = "gbTarjeta";
            this.gbTarjeta.Size = new System.Drawing.Size(732, 93);
            this.gbTarjeta.TabIndex = 1;
            this.gbTarjeta.TabStop = false;
            this.gbTarjeta.Text = "Tarjeta";
            // 
            // txtBancoEmisor
            // 
            this.txtBancoEmisor.Location = new System.Drawing.Point(448, 82);
            this.txtBancoEmisor.Name = "txtBancoEmisor";
            this.txtBancoEmisor.Size = new System.Drawing.Size(150, 22);
            this.txtBancoEmisor.TabIndex = 22;
            // 
            // dtpVencimiento
            // 
            this.dtpVencimiento.Location = new System.Drawing.Point(448, 54);
            this.dtpVencimiento.Name = "dtpVencimiento";
            this.dtpVencimiento.Size = new System.Drawing.Size(210, 22);
            this.dtpVencimiento.TabIndex = 10;
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(448, 17);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(150, 22);
            this.txtMarca.TabIndex = 21;
            // 
            // txtCVV
            // 
            this.txtCVV.Location = new System.Drawing.Point(142, 76);
            this.txtCVV.Name = "txtCVV";
            this.txtCVV.Size = new System.Drawing.Size(150, 22);
            this.txtCVV.TabIndex = 20;
            // 
            // txtTitular
            // 
            this.txtTitular.Location = new System.Drawing.Point(142, 48);
            this.txtTitular.Name = "txtTitular";
            this.txtTitular.Size = new System.Drawing.Size(150, 22);
            this.txtTitular.TabIndex = 19;
            // 
            // txtNumeroTarjeta
            // 
            this.txtNumeroTarjeta.Location = new System.Drawing.Point(142, 17);
            this.txtNumeroTarjeta.Name = "txtNumeroTarjeta";
            this.txtNumeroTarjeta.Size = new System.Drawing.Size(150, 22);
            this.txtNumeroTarjeta.TabIndex = 13;
            // 
            // lblBancoEmisor
            // 
            this.lblBancoEmisor.AutoSize = true;
            this.lblBancoEmisor.Location = new System.Drawing.Point(328, 82);
            this.lblBancoEmisor.Name = "lblBancoEmisor";
            this.lblBancoEmisor.Size = new System.Drawing.Size(93, 16);
            this.lblBancoEmisor.TabIndex = 18;
            this.lblBancoEmisor.Text = "Banco emisor:";
            // 
            // lblVencimiento
            // 
            this.lblVencimiento.AutoSize = true;
            this.lblVencimiento.Location = new System.Drawing.Point(328, 54);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.Size = new System.Drawing.Size(84, 16);
            this.lblVencimiento.TabIndex = 17;
            this.lblVencimiento.Text = "Vencimiento:";
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(328, 23);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(48, 16);
            this.lblMarca.TabIndex = 16;
            this.lblMarca.Text = "Marca:";
            // 
            // lblCVV
            // 
            this.lblCVV.AutoSize = true;
            this.lblCVV.Location = new System.Drawing.Point(24, 82);
            this.lblCVV.Name = "lblCVV";
            this.lblCVV.Size = new System.Drawing.Size(37, 16);
            this.lblCVV.TabIndex = 15;
            this.lblCVV.Text = "CVV:";
            // 
            // lblTitular
            // 
            this.lblTitular.AutoSize = true;
            this.lblTitular.Location = new System.Drawing.Point(24, 54);
            this.lblTitular.Name = "lblTitular";
            this.lblTitular.Size = new System.Drawing.Size(47, 16);
            this.lblTitular.TabIndex = 14;
            this.lblTitular.Text = "Titular:";
            // 
            // lblNumeroTarjeta
            // 
            this.lblNumeroTarjeta.AutoSize = true;
            this.lblNumeroTarjeta.Location = new System.Drawing.Point(24, 23);
            this.lblNumeroTarjeta.Name = "lblNumeroTarjeta";
            this.lblNumeroTarjeta.Size = new System.Drawing.Size(58, 16);
            this.lblNumeroTarjeta.TabIndex = 13;
            this.lblNumeroTarjeta.Text = "Número:";
            // 
            // gbTransferencia
            // 
            this.gbTransferencia.Controls.Add(this.txtNumeroOperacion);
            this.gbTransferencia.Controls.Add(this.lblNumeroOperacion);
            this.gbTransferencia.Controls.Add(this.txtCBU);
            this.gbTransferencia.Controls.Add(this.lblCBU);
            this.gbTransferencia.Controls.Add(this.txtBanco);
            this.gbTransferencia.Controls.Add(this.lblBanco);
            this.gbTransferencia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTransferencia.Location = new System.Drawing.Point(3, 76);
            this.gbTransferencia.Name = "gbTransferencia";
            this.gbTransferencia.Size = new System.Drawing.Size(732, 67);
            this.gbTransferencia.TabIndex = 1;
            this.gbTransferencia.TabStop = false;
            this.gbTransferencia.Text = "Transferencia";
            // 
            // txtNumeroOperacion
            // 
            this.txtNumeroOperacion.Location = new System.Drawing.Point(448, 12);
            this.txtNumeroOperacion.Name = "txtNumeroOperacion";
            this.txtNumeroOperacion.Size = new System.Drawing.Size(150, 22);
            this.txtNumeroOperacion.TabIndex = 16;
            // 
            // lblNumeroOperacion
            // 
            this.lblNumeroOperacion.AutoSize = true;
            this.lblNumeroOperacion.Location = new System.Drawing.Point(353, 18);
            this.lblNumeroOperacion.Name = "lblNumeroOperacion";
            this.lblNumeroOperacion.Size = new System.Drawing.Size(89, 16);
            this.lblNumeroOperacion.TabIndex = 15;
            this.lblNumeroOperacion.Text = "Nº operación:";
            // 
            // txtCBU
            // 
            this.txtCBU.Location = new System.Drawing.Point(151, 44);
            this.txtCBU.Name = "txtCBU";
            this.txtCBU.Size = new System.Drawing.Size(325, 22);
            this.txtCBU.TabIndex = 14;
            // 
            // lblCBU
            // 
            this.lblCBU.AutoSize = true;
            this.lblCBU.Location = new System.Drawing.Point(24, 53);
            this.lblCBU.Name = "lblCBU";
            this.lblCBU.Size = new System.Drawing.Size(72, 16);
            this.lblCBU.TabIndex = 13;
            this.lblCBU.Text = "CBU/Alias:";
            // 
            // txtBanco
            // 
            this.txtBanco.Location = new System.Drawing.Point(151, 19);
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.Size = new System.Drawing.Size(150, 22);
            this.txtBanco.TabIndex = 13;
            // 
            // lblBanco
            // 
            this.lblBanco.AutoSize = true;
            this.lblBanco.Location = new System.Drawing.Point(24, 25);
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.Size = new System.Drawing.Size(49, 16);
            this.lblBanco.TabIndex = 2;
            this.lblBanco.Text = "Banco:";
            // 
            // gbEfectivo
            // 
            this.gbEfectivo.Controls.Add(this.txtVuelto);
            this.gbEfectivo.Controls.Add(this.lblVuelto);
            this.gbEfectivo.Controls.Add(this.txtMontoEntregado);
            this.gbEfectivo.Controls.Add(this.lblMontoEntregado);
            this.gbEfectivo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbEfectivo.Location = new System.Drawing.Point(3, 3);
            this.gbEfectivo.Name = "gbEfectivo";
            this.gbEfectivo.Size = new System.Drawing.Size(732, 67);
            this.gbEfectivo.TabIndex = 0;
            this.gbEfectivo.TabStop = false;
            this.gbEfectivo.Text = "Efectivo";
            // 
            // txtVuelto
            // 
            this.txtVuelto.Location = new System.Drawing.Point(414, 25);
            this.txtVuelto.Name = "txtVuelto";
            this.txtVuelto.ReadOnly = true;
            this.txtVuelto.Size = new System.Drawing.Size(150, 22);
            this.txtVuelto.TabIndex = 12;
            // 
            // lblVuelto
            // 
            this.lblVuelto.AutoSize = true;
            this.lblVuelto.Location = new System.Drawing.Point(353, 28);
            this.lblVuelto.Name = "lblVuelto";
            this.lblVuelto.Size = new System.Drawing.Size(48, 16);
            this.lblVuelto.TabIndex = 11;
            this.lblVuelto.Text = "Vuelto:";
            // 
            // txtMontoEntregado
            // 
            this.txtMontoEntregado.Location = new System.Drawing.Point(142, 25);
            this.txtMontoEntregado.Name = "txtMontoEntregado";
            this.txtMontoEntregado.Size = new System.Drawing.Size(150, 22);
            this.txtMontoEntregado.TabIndex = 10;
            this.txtMontoEntregado.TextChanged += new System.EventHandler(this.txtMontoEntregado_TextChanged);
            // 
            // lblMontoEntregado
            // 
            this.lblMontoEntregado.AutoSize = true;
            this.lblMontoEntregado.Location = new System.Drawing.Point(24, 31);
            this.lblMontoEntregado.Name = "lblMontoEntregado";
            this.lblMontoEntregado.Size = new System.Drawing.Size(112, 16);
            this.lblMontoEntregado.TabIndex = 1;
            this.lblMontoEntregado.Text = "Monto entregado:";
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnCerrar);
            this.panelBotones.Controls.Add(this.btnActualizar);
            this.panelBotones.Controls.Add(this.btnLimpiar);
            this.panelBotones.Controls.Add(this.btnRegistrarPago);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBotones.Location = new System.Drawing.Point(3, 559);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1249, 65);
            this.panelBotones.TabIndex = 2;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(1000, 20);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(130, 35);
            this.btnCerrar.TabIndex = 44;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(650, 20);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(130, 35);
            this.btnActualizar.TabIndex = 43;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(350, 20);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(130, 35);
            this.btnLimpiar.TabIndex = 42;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnRegistrarPago
            // 
            this.btnRegistrarPago.Location = new System.Drawing.Point(50, 20);
            this.btnRegistrarPago.Name = "btnRegistrarPago";
            this.btnRegistrarPago.Size = new System.Drawing.Size(130, 35);
            this.btnRegistrarPago.TabIndex = 41;
            this.btnRegistrarPago.Text = "Registrar Pago";
            this.btnRegistrarPago.UseVisualStyleBackColor = true;
            this.btnRegistrarPago.Click += new System.EventHandler(this.btnRegistrarPago_Click);
            // 
            // tabControlPagos
            // 
            this.tabControlPagos.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.tabControlPagos.Controls.Add(this.tabRegistrarPago);
            this.tabControlPagos.Controls.Add(this.tabPagosRegistrados);
            this.tabControlPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPagos.Location = new System.Drawing.Point(0, 0);
            this.tabControlPagos.Name = "tabControlPagos";
            this.tabControlPagos.SelectedIndex = 0;
            this.tabControlPagos.Size = new System.Drawing.Size(1269, 662);
            this.tabControlPagos.TabIndex = 1;
            // 
            // tabPagosRegistrados
            // 
            this.tabPagosRegistrados.Controls.Add(this.tableLayoutPanelPagosRegistrados);
            this.tabPagosRegistrados.Location = new System.Drawing.Point(4, 25);
            this.tabPagosRegistrados.Name = "tabPagosRegistrados";
            this.tabPagosRegistrados.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagosRegistrados.Size = new System.Drawing.Size(1261, 633);
            this.tabPagosRegistrados.TabIndex = 1;
            this.tabPagosRegistrados.Text = "Pagos Registrados";
            this.tabPagosRegistrados.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelPagosRegistrados
            // 
            this.tableLayoutPanelPagosRegistrados.ColumnCount = 1;
            this.tableLayoutPanelPagosRegistrados.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPagosRegistrados.Controls.Add(this.gbPagosRegistrados, 0, 0);
            this.tableLayoutPanelPagosRegistrados.Controls.Add(this.gbDetallePago, 0, 1);
            this.tableLayoutPanelPagosRegistrados.Controls.Add(this.gbBotonesPagosRegistrados, 0, 2);
            this.tableLayoutPanelPagosRegistrados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelPagosRegistrados.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelPagosRegistrados.Name = "tableLayoutPanelPagosRegistrados";
            this.tableLayoutPanelPagosRegistrados.RowCount = 3;
            this.tableLayoutPanelPagosRegistrados.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.7179F));
            this.tableLayoutPanelPagosRegistrados.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.2821F));
            this.tableLayoutPanelPagosRegistrados.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanelPagosRegistrados.Size = new System.Drawing.Size(1255, 627);
            this.tableLayoutPanelPagosRegistrados.TabIndex = 0;
            // 
            // gbPagosRegistrados
            // 
            this.gbPagosRegistrados.Controls.Add(this.dgvPagos);
            this.gbPagosRegistrados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPagosRegistrados.Location = new System.Drawing.Point(3, 3);
            this.gbPagosRegistrados.Name = "gbPagosRegistrados";
            this.gbPagosRegistrados.Size = new System.Drawing.Size(1249, 280);
            this.gbPagosRegistrados.TabIndex = 1;
            this.gbPagosRegistrados.TabStop = false;
            this.gbPagosRegistrados.Text = "Pagos Registrados";
            // 
            // dgvPagos
            // 
            this.dgvPagos.AllowUserToAddRows = false;
            this.dgvPagos.AllowUserToDeleteRows = false;
            this.dgvPagos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPagos.Location = new System.Drawing.Point(3, 18);
            this.dgvPagos.MultiSelect = false;
            this.dgvPagos.Name = "dgvPagos";
            this.dgvPagos.ReadOnly = true;
            this.dgvPagos.RowHeadersWidth = 51;
            this.dgvPagos.RowTemplate.Height = 24;
            this.dgvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPagos.Size = new System.Drawing.Size(1243, 259);
            this.dgvPagos.TabIndex = 0;
            // 
            // gbDetallePago
            // 
            this.gbDetallePago.Controls.Add(this.rtbDetallePago);
            this.gbDetallePago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetallePago.Location = new System.Drawing.Point(3, 289);
            this.gbDetallePago.Name = "gbDetallePago";
            this.gbDetallePago.Size = new System.Drawing.Size(1249, 261);
            this.gbDetallePago.TabIndex = 2;
            this.gbDetallePago.TabStop = false;
            this.gbDetallePago.Text = "Detalle Pago";
            // 
            // rtbDetallePago
            // 
            this.rtbDetallePago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbDetallePago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDetallePago.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDetallePago.Location = new System.Drawing.Point(3, 18);
            this.rtbDetallePago.Name = "rtbDetallePago";
            this.rtbDetallePago.ReadOnly = true;
            this.rtbDetallePago.Size = new System.Drawing.Size(1243, 240);
            this.rtbDetallePago.TabIndex = 0;
            this.rtbDetallePago.Text = "";
            // 
            // gbBotonesPagosRegistrados
            // 
            this.gbBotonesPagosRegistrados.Controls.Add(this.btnActualizar2);
            this.gbBotonesPagosRegistrados.Controls.Add(this.btnImprimirComprobante);
            this.gbBotonesPagosRegistrados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBotonesPagosRegistrados.Location = new System.Drawing.Point(3, 556);
            this.gbBotonesPagosRegistrados.Name = "gbBotonesPagosRegistrados";
            this.gbBotonesPagosRegistrados.Size = new System.Drawing.Size(1249, 68);
            this.gbBotonesPagosRegistrados.TabIndex = 3;
            this.gbBotonesPagosRegistrados.TabStop = false;
            // 
            // btnActualizar2
            // 
            this.btnActualizar2.Location = new System.Drawing.Point(489, 17);
            this.btnActualizar2.Name = "btnActualizar2";
            this.btnActualizar2.Size = new System.Drawing.Size(270, 35);
            this.btnActualizar2.TabIndex = 47;
            this.btnActualizar2.Text = "Actualizar";
            this.btnActualizar2.UseVisualStyleBackColor = true;
            this.btnActualizar2.Click += new System.EventHandler(this.btnActualizar2_Click);
            // 
            // btnImprimirComprobante
            // 
            this.btnImprimirComprobante.Location = new System.Drawing.Point(87, 21);
            this.btnImprimirComprobante.Name = "btnImprimirComprobante";
            this.btnImprimirComprobante.Size = new System.Drawing.Size(270, 35);
            this.btnImprimirComprobante.TabIndex = 46;
            this.btnImprimirComprobante.Text = "Imprimir Compribante ";
            this.btnImprimirComprobante.UseVisualStyleBackColor = true;
            this.btnImprimirComprobante.Click += new System.EventHandler(this.btnImprimirComprobante_Click);
            // 
            // FrmPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 662);
            this.Controls.Add(this.tabControlPagos);
            this.Name = "FrmPago";
            this.Tag = "FrmPago";
            this.Text = "FrmPago";
            this.Load += new System.EventHandler(this.FrmPago_Load);
            this.tabRegistrarPago.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gbFacturasPendientes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacturas)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.gbDatosPago.ResumeLayout(false);
            this.gbDatosPago.PerformLayout();
            this.gbDetalleMedioPago.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.gbTarjeta.ResumeLayout(false);
            this.gbTarjeta.PerformLayout();
            this.gbTransferencia.ResumeLayout(false);
            this.gbTransferencia.PerformLayout();
            this.gbEfectivo.ResumeLayout(false);
            this.gbEfectivo.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.tabControlPagos.ResumeLayout(false);
            this.tabPagosRegistrados.ResumeLayout(false);
            this.tableLayoutPanelPagosRegistrados.ResumeLayout(false);
            this.gbPagosRegistrados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).EndInit();
            this.gbDetallePago.ResumeLayout(false);
            this.gbBotonesPagosRegistrados.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabRegistrarPago;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gbFacturasPendientes;
        private System.Windows.Forms.DataGridView dgvFacturas;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox gbDatosPago;
        private System.Windows.Forms.ComboBox cmbMedioPago;
        private System.Windows.Forms.Label lblMedioPago;
        private System.Windows.Forms.DateTimePicker dtpFechaPago;
        private System.Windows.Forms.Label lblFechaPago;
        private System.Windows.Forms.TextBox txtImportePago;
        private System.Windows.Forms.Label lblImporte;
        private System.Windows.Forms.TextBox txtTotalFactura;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.Label lblFactura;
        private System.Windows.Forms.GroupBox gbDetalleMedioPago;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox gbTarjeta;
        private System.Windows.Forms.TextBox txtBancoEmisor;
        private System.Windows.Forms.DateTimePicker dtpVencimiento;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.TextBox txtCVV;
        private System.Windows.Forms.TextBox txtTitular;
        private System.Windows.Forms.TextBox txtNumeroTarjeta;
        private System.Windows.Forms.Label lblBancoEmisor;
        private System.Windows.Forms.Label lblVencimiento;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblCVV;
        private System.Windows.Forms.Label lblTitular;
        private System.Windows.Forms.Label lblNumeroTarjeta;
        private System.Windows.Forms.GroupBox gbTransferencia;
        private System.Windows.Forms.TextBox txtNumeroOperacion;
        private System.Windows.Forms.Label lblNumeroOperacion;
        private System.Windows.Forms.TextBox txtCBU;
        private System.Windows.Forms.Label lblCBU;
        private System.Windows.Forms.TextBox txtBanco;
        private System.Windows.Forms.Label lblBanco;
        private System.Windows.Forms.GroupBox gbEfectivo;
        private System.Windows.Forms.TextBox txtVuelto;
        private System.Windows.Forms.Label lblVuelto;
        private System.Windows.Forms.TextBox txtMontoEntregado;
        private System.Windows.Forms.Label lblMontoEntregado;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnRegistrarPago;
        private System.Windows.Forms.TabControl tabControlPagos;
        private System.Windows.Forms.TabPage tabPagosRegistrados;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPagosRegistrados;
        private System.Windows.Forms.GroupBox gbPagosRegistrados;
        private System.Windows.Forms.DataGridView dgvPagos;
        private System.Windows.Forms.GroupBox gbDetallePago;
        private System.Windows.Forms.GroupBox gbBotonesPagosRegistrados;
        private System.Windows.Forms.Button btnActualizar2;
        private System.Windows.Forms.Button btnImprimirComprobante;
        private System.Windows.Forms.RichTextBox rtbDetallePago;
    }
}