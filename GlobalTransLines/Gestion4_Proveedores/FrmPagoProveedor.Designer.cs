namespace GlobalTransLines.UI.Gestion4_Proveedores
{
    partial class FrmPagoProveedor
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControlPagos = new System.Windows.Forms.TabControl();
            this.tabRegistrarPago = new System.Windows.Forms.TabPage();
            this.tableLayoutPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.gbOrdenes = new System.Windows.Forms.GroupBox();
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.tableLayoutDatos = new System.Windows.Forms.TableLayoutPanel();
            this.gbDatosPago = new System.Windows.Forms.GroupBox();
            this.txtImporteFactura = new System.Windows.Forms.TextBox();
            this.lblImporteFactura = new System.Windows.Forms.Label();
            this.dtpFechaFactura = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFactura = new System.Windows.Forms.Label();
            this.txtNumeroFactura = new System.Windows.Forms.TextBox();
            this.lblNumeroFactura = new System.Windows.Forms.Label();
            this.cmbMedioPago = new System.Windows.Forms.ComboBox();
            this.lblMedioPago = new System.Windows.Forms.Label();
            this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
            this.lblFechaPago = new System.Windows.Forms.Label();
            this.txtImportePago = new System.Windows.Forms.TextBox();
            this.lblImportePago = new System.Windows.Forms.Label();
            this.txtTotalOrden = new System.Windows.Forms.TextBox();
            this.lblTotalOrden = new System.Windows.Forms.Label();
            this.txtOrden = new System.Windows.Forms.TextBox();
            this.lblOrden = new System.Windows.Forms.Label();
            this.gbDetalleMedioPago = new System.Windows.Forms.GroupBox();
            this.panelMedios = new System.Windows.Forms.Panel();
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
            this.tabPagosRegistrados = new System.Windows.Forms.TabPage();
            this.tableLayoutPagos = new System.Windows.Forms.TableLayoutPanel();
            this.gbPagos = new System.Windows.Forms.GroupBox();
            this.dgvPagos = new System.Windows.Forms.DataGridView();
            this.gbDetallePago = new System.Windows.Forms.GroupBox();
            this.rtbDetallePago = new System.Windows.Forms.RichTextBox();
            this.panelBotonesPagos = new System.Windows.Forms.Panel();
            this.btnActualizar2 = new System.Windows.Forms.Button();
            this.btnImprimirComprobante = new System.Windows.Forms.Button();
            this.tabControlPagos.SuspendLayout();
            this.tabRegistrarPago.SuspendLayout();
            this.tableLayoutPrincipal.SuspendLayout();
            this.gbOrdenes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            this.tableLayoutDatos.SuspendLayout();
            this.gbDatosPago.SuspendLayout();
            this.gbDetalleMedioPago.SuspendLayout();
            this.panelMedios.SuspendLayout();
            this.gbTarjeta.SuspendLayout();
            this.gbTransferencia.SuspendLayout();
            this.gbEfectivo.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.tabPagosRegistrados.SuspendLayout();
            this.tableLayoutPagos.SuspendLayout();
            this.gbPagos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).BeginInit();
            this.gbDetallePago.SuspendLayout();
            this.panelBotonesPagos.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlPagos
            // 
            this.tabControlPagos.Controls.Add(this.tabRegistrarPago);
            this.tabControlPagos.Controls.Add(this.tabPagosRegistrados);
            this.tabControlPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPagos.Location = new System.Drawing.Point(0, 0);
            this.tabControlPagos.Name = "tabControlPagos";
            this.tabControlPagos.SelectedIndex = 0;
            this.tabControlPagos.Size = new System.Drawing.Size(1269, 662);
            this.tabControlPagos.TabIndex = 0;
            // 
            // tabRegistrarPago
            // 
            this.tabRegistrarPago.Controls.Add(this.tableLayoutPrincipal);
            this.tabRegistrarPago.Location = new System.Drawing.Point(4, 25);
            this.tabRegistrarPago.Name = "tabRegistrarPago";
            this.tabRegistrarPago.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegistrarPago.Size = new System.Drawing.Size(1261, 633);
            this.tabRegistrarPago.TabIndex = 0;
            this.tabRegistrarPago.Text = "Registrar pago";
            this.tabRegistrarPago.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPrincipal
            // 
            this.tableLayoutPrincipal.ColumnCount = 1;
            this.tableLayoutPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Controls.Add(this.gbOrdenes, 0, 0);
            this.tableLayoutPrincipal.Controls.Add(this.tableLayoutDatos, 0, 1);
            this.tableLayoutPrincipal.Controls.Add(this.panelBotones, 0, 2);
            this.tableLayoutPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPrincipal.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPrincipal.Name = "tableLayoutPrincipal";
            this.tableLayoutPrincipal.RowCount = 3;
            this.tableLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPrincipal.Size = new System.Drawing.Size(1255, 627);
            this.tableLayoutPrincipal.TabIndex = 0;
            // 
            // gbOrdenes
            // 
            this.gbOrdenes.Controls.Add(this.dgvOrdenes);
            this.gbOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOrdenes.Location = new System.Drawing.Point(3, 3);
            this.gbOrdenes.Name = "gbOrdenes";
            this.gbOrdenes.Size = new System.Drawing.Size(1249, 244);
            this.gbOrdenes.TabIndex = 0;
            this.gbOrdenes.TabStop = false;
            this.gbOrdenes.Text = "Órdenes de compra recibidas";
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenes.Location = new System.Drawing.Point(3, 18);
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.RowHeadersWidth = 51;
            this.dgvOrdenes.RowTemplate.Height = 24;
            this.dgvOrdenes.Size = new System.Drawing.Size(1243, 223);
            this.dgvOrdenes.TabIndex = 0;
            // 
            // tableLayoutDatos
            // 
            this.tableLayoutDatos.ColumnCount = 2;
            this.tableLayoutDatos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutDatos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutDatos.Controls.Add(this.gbDatosPago, 0, 0);
            this.tableLayoutDatos.Controls.Add(this.gbDetalleMedioPago, 1, 0);
            this.tableLayoutDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutDatos.Location = new System.Drawing.Point(3, 253);
            this.tableLayoutDatos.Name = "tableLayoutDatos";
            this.tableLayoutDatos.RowCount = 1;
            this.tableLayoutDatos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDatos.Size = new System.Drawing.Size(1249, 300);
            this.tableLayoutDatos.TabIndex = 1;
            // 
            // gbDatosPago
            // 
            this.gbDatosPago.Controls.Add(this.txtImporteFactura);
            this.gbDatosPago.Controls.Add(this.lblImporteFactura);
            this.gbDatosPago.Controls.Add(this.dtpFechaFactura);
            this.gbDatosPago.Controls.Add(this.lblFechaFactura);
            this.gbDatosPago.Controls.Add(this.txtNumeroFactura);
            this.gbDatosPago.Controls.Add(this.lblNumeroFactura);
            this.gbDatosPago.Controls.Add(this.cmbMedioPago);
            this.gbDatosPago.Controls.Add(this.lblMedioPago);
            this.gbDatosPago.Controls.Add(this.dtpFechaPago);
            this.gbDatosPago.Controls.Add(this.lblFechaPago);
            this.gbDatosPago.Controls.Add(this.txtImportePago);
            this.gbDatosPago.Controls.Add(this.lblImportePago);
            this.gbDatosPago.Controls.Add(this.txtTotalOrden);
            this.gbDatosPago.Controls.Add(this.lblTotalOrden);
            this.gbDatosPago.Controls.Add(this.txtOrden);
            this.gbDatosPago.Controls.Add(this.lblOrden);
            this.gbDatosPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDatosPago.Location = new System.Drawing.Point(3, 3);
            this.gbDatosPago.Name = "gbDatosPago";
            this.gbDatosPago.Size = new System.Drawing.Size(493, 294);
            this.gbDatosPago.TabIndex = 0;
            this.gbDatosPago.TabStop = false;
            this.gbDatosPago.Text = "Datos del pago";
            // 
            // labels and fields
            // 
            this.lblOrden.Location = new System.Drawing.Point(10, 30); this.lblOrden.Size = new System.Drawing.Size(120, 20); this.lblOrden.Text = "Orden:";
            this.txtOrden.Location = new System.Drawing.Point(155, 27); this.txtOrden.Size = new System.Drawing.Size(250, 22);
            this.lblTotalOrden.Location = new System.Drawing.Point(10, 60); this.lblTotalOrden.Size = new System.Drawing.Size(120, 20); this.lblTotalOrden.Text = "Total OC:";
            this.txtTotalOrden.Location = new System.Drawing.Point(155, 57); this.txtTotalOrden.Size = new System.Drawing.Size(250, 22);
            this.lblNumeroFactura.Location = new System.Drawing.Point(10, 90); this.lblNumeroFactura.Size = new System.Drawing.Size(140, 20); this.lblNumeroFactura.Text = "Factura proveedor:";
            this.txtNumeroFactura.Location = new System.Drawing.Point(155, 87); this.txtNumeroFactura.Size = new System.Drawing.Size(250, 22);
            this.lblFechaFactura.Location = new System.Drawing.Point(10, 120); this.lblFechaFactura.Size = new System.Drawing.Size(140, 20); this.lblFechaFactura.Text = "Fecha factura:";
            this.dtpFechaFactura.Location = new System.Drawing.Point(155, 117); this.dtpFechaFactura.Size = new System.Drawing.Size(250, 22);
            this.lblImporteFactura.Location = new System.Drawing.Point(10, 150); this.lblImporteFactura.Size = new System.Drawing.Size(140, 20); this.lblImporteFactura.Text = "Importe factura:";
            this.txtImporteFactura.Location = new System.Drawing.Point(155, 147); this.txtImporteFactura.Size = new System.Drawing.Size(250, 22);
            this.lblFechaPago.Location = new System.Drawing.Point(10, 180); this.lblFechaPago.Size = new System.Drawing.Size(120, 20); this.lblFechaPago.Text = "Fecha pago:";
            this.dtpFechaPago.Location = new System.Drawing.Point(155, 177); this.dtpFechaPago.Size = new System.Drawing.Size(250, 22);
            this.lblImportePago.Location = new System.Drawing.Point(10, 210); this.lblImportePago.Size = new System.Drawing.Size(120, 20); this.lblImportePago.Text = "Importe pago:";
            this.txtImportePago.Location = new System.Drawing.Point(155, 207); this.txtImportePago.Size = new System.Drawing.Size(250, 22);
            this.lblMedioPago.Location = new System.Drawing.Point(10, 240); this.lblMedioPago.Size = new System.Drawing.Size(120, 20); this.lblMedioPago.Text = "Medio de pago:";
            this.cmbMedioPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedioPago.Location = new System.Drawing.Point(155, 237); this.cmbMedioPago.Size = new System.Drawing.Size(250, 24);
            // 
            // gbDetalleMedioPago
            // 
            this.gbDetalleMedioPago.Controls.Add(this.panelMedios);
            this.gbDetalleMedioPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetalleMedioPago.Location = new System.Drawing.Point(502, 3);
            this.gbDetalleMedioPago.Name = "gbDetalleMedioPago";
            this.gbDetalleMedioPago.Size = new System.Drawing.Size(744, 294);
            this.gbDetalleMedioPago.TabIndex = 1;
            this.gbDetalleMedioPago.TabStop = false;
            this.gbDetalleMedioPago.Text = "Detalle del medio de pago";
            // 
            // panelMedios
            // 
            this.panelMedios.Controls.Add(this.gbTarjeta);
            this.panelMedios.Controls.Add(this.gbTransferencia);
            this.panelMedios.Controls.Add(this.gbEfectivo);
            this.panelMedios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMedios.Location = new System.Drawing.Point(3, 18);
            this.panelMedios.Name = "panelMedios";
            this.panelMedios.Size = new System.Drawing.Size(738, 273);
            this.panelMedios.TabIndex = 0;
            // 
            // gbEfectivo
            // 
            this.gbEfectivo.Controls.Add(this.txtVuelto);
            this.gbEfectivo.Controls.Add(this.lblVuelto);
            this.gbEfectivo.Controls.Add(this.txtMontoEntregado);
            this.gbEfectivo.Controls.Add(this.lblMontoEntregado);
            this.gbEfectivo.Location = new System.Drawing.Point(8, 8);
            this.gbEfectivo.Name = "gbEfectivo";
            this.gbEfectivo.Size = new System.Drawing.Size(720, 70);
            this.gbEfectivo.TabIndex = 0;
            this.gbEfectivo.TabStop = false;
            this.gbEfectivo.Text = "Efectivo";
            this.lblMontoEntregado.Location = new System.Drawing.Point(20, 30); this.lblMontoEntregado.Text = "Monto entregado:"; this.lblMontoEntregado.Size = new System.Drawing.Size(130, 20);
            this.txtMontoEntregado.Location = new System.Drawing.Point(155, 27); this.txtMontoEntregado.Size = new System.Drawing.Size(150, 22);
            this.lblVuelto.Location = new System.Drawing.Point(340, 30); this.lblVuelto.Text = "Vuelto:"; this.lblVuelto.Size = new System.Drawing.Size(60, 20);
            this.txtVuelto.Location = new System.Drawing.Point(405, 27); this.txtVuelto.Size = new System.Drawing.Size(150, 22);
            // 
            // gbTransferencia
            // 
            this.gbTransferencia.Controls.Add(this.txtNumeroOperacion);
            this.gbTransferencia.Controls.Add(this.lblNumeroOperacion);
            this.gbTransferencia.Controls.Add(this.txtCBU);
            this.gbTransferencia.Controls.Add(this.lblCBU);
            this.gbTransferencia.Controls.Add(this.txtBanco);
            this.gbTransferencia.Controls.Add(this.lblBanco);
            this.gbTransferencia.Location = new System.Drawing.Point(8, 86);
            this.gbTransferencia.Name = "gbTransferencia";
            this.gbTransferencia.Size = new System.Drawing.Size(720, 80);
            this.gbTransferencia.TabIndex = 1;
            this.gbTransferencia.TabStop = false;
            this.gbTransferencia.Text = "Transferencia";
            this.lblBanco.Location = new System.Drawing.Point(20, 25); this.lblBanco.Text = "Banco:"; this.lblBanco.Size = new System.Drawing.Size(80, 20);
            this.txtBanco.Location = new System.Drawing.Point(105, 22); this.txtBanco.Size = new System.Drawing.Size(170, 22);
            this.lblCBU.Location = new System.Drawing.Point(20, 52); this.lblCBU.Text = "CBU/Alias:"; this.lblCBU.Size = new System.Drawing.Size(80, 20);
            this.txtCBU.Location = new System.Drawing.Point(105, 49); this.txtCBU.Size = new System.Drawing.Size(270, 22);
            this.lblNumeroOperacion.Location = new System.Drawing.Point(400, 25); this.lblNumeroOperacion.Text = "Nro op.:"; this.lblNumeroOperacion.Size = new System.Drawing.Size(70, 20);
            this.txtNumeroOperacion.Location = new System.Drawing.Point(475, 22); this.txtNumeroOperacion.Size = new System.Drawing.Size(170, 22);
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
            this.gbTarjeta.Location = new System.Drawing.Point(8, 174);
            this.gbTarjeta.Name = "gbTarjeta";
            this.gbTarjeta.Size = new System.Drawing.Size(720, 92);
            this.gbTarjeta.TabIndex = 2;
            this.gbTarjeta.TabStop = false;
            this.gbTarjeta.Text = "Tarjeta";
            this.lblNumeroTarjeta.Location = new System.Drawing.Point(20, 25); this.lblNumeroTarjeta.Text = "Número:"; this.lblNumeroTarjeta.Size = new System.Drawing.Size(70, 20);
            this.txtNumeroTarjeta.Location = new System.Drawing.Point(95, 22); this.txtNumeroTarjeta.Size = new System.Drawing.Size(140, 22);
            this.lblTitular.Location = new System.Drawing.Point(250, 25); this.lblTitular.Text = "Titular:"; this.lblTitular.Size = new System.Drawing.Size(55, 20);
            this.txtTitular.Location = new System.Drawing.Point(310, 22); this.txtTitular.Size = new System.Drawing.Size(140, 22);
            this.lblCVV.Location = new System.Drawing.Point(465, 25); this.lblCVV.Text = "CVV:"; this.lblCVV.Size = new System.Drawing.Size(45, 20);
            this.txtCVV.Location = new System.Drawing.Point(515, 22); this.txtCVV.Size = new System.Drawing.Size(70, 22);
            this.lblMarca.Location = new System.Drawing.Point(20, 58); this.lblMarca.Text = "Marca:"; this.lblMarca.Size = new System.Drawing.Size(70, 20);
            this.txtMarca.Location = new System.Drawing.Point(95, 55); this.txtMarca.Size = new System.Drawing.Size(140, 22);
            this.lblVencimiento.Location = new System.Drawing.Point(250, 58); this.lblVencimiento.Text = "Vencimiento:"; this.lblVencimiento.Size = new System.Drawing.Size(90, 20);
            this.dtpVencimiento.Location = new System.Drawing.Point(345, 55); this.dtpVencimiento.Size = new System.Drawing.Size(150, 22);
            this.lblBancoEmisor.Location = new System.Drawing.Point(510, 58); this.lblBancoEmisor.Text = "Banco:"; this.lblBancoEmisor.Size = new System.Drawing.Size(55, 20);
            this.txtBancoEmisor.Location = new System.Drawing.Point(570, 55); this.txtBancoEmisor.Size = new System.Drawing.Size(120, 22);
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
            this.btnRegistrarPago.Location = new System.Drawing.Point(50, 17); this.btnRegistrarPago.Size = new System.Drawing.Size(150, 35); this.btnRegistrarPago.Text = "Registrar pago"; this.btnRegistrarPago.Click += new System.EventHandler(this.btnRegistrarPago_Click);
            this.btnLimpiar.Location = new System.Drawing.Point(240, 17); this.btnLimpiar.Size = new System.Drawing.Size(150, 35); this.btnLimpiar.Text = "Limpiar"; this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            this.btnActualizar.Location = new System.Drawing.Point(430, 17); this.btnActualizar.Size = new System.Drawing.Size(150, 35); this.btnActualizar.Text = "Actualizar"; this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            this.btnCerrar.Location = new System.Drawing.Point(620, 17); this.btnCerrar.Size = new System.Drawing.Size(150, 35); this.btnCerrar.Text = "Cerrar"; this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // tabPagosRegistrados
            // 
            this.tabPagosRegistrados.Controls.Add(this.tableLayoutPagos);
            this.tabPagosRegistrados.Location = new System.Drawing.Point(4, 25);
            this.tabPagosRegistrados.Name = "tabPagosRegistrados";
            this.tabPagosRegistrados.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagosRegistrados.Size = new System.Drawing.Size(1261, 633);
            this.tabPagosRegistrados.TabIndex = 1;
            this.tabPagosRegistrados.Text = "Pagos registrados";
            this.tabPagosRegistrados.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPagos
            // 
            this.tableLayoutPagos.ColumnCount = 1;
            this.tableLayoutPagos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPagos.Controls.Add(this.gbPagos, 0, 0);
            this.tableLayoutPagos.Controls.Add(this.gbDetallePago, 0, 1);
            this.tableLayoutPagos.Controls.Add(this.panelBotonesPagos, 0, 2);
            this.tableLayoutPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPagos.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPagos.Name = "tableLayoutPagos";
            this.tableLayoutPagos.RowCount = 3;
            this.tableLayoutPagos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPagos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPagos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPagos.Size = new System.Drawing.Size(1255, 627);
            this.tableLayoutPagos.TabIndex = 0;
            this.gbPagos.Controls.Add(this.dgvPagos); this.gbPagos.Dock = System.Windows.Forms.DockStyle.Fill; this.gbPagos.Text = "Pagos registrados";
            this.dgvPagos.Dock = System.Windows.Forms.DockStyle.Fill; this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gbDetallePago.Controls.Add(this.rtbDetallePago); this.gbDetallePago.Dock = System.Windows.Forms.DockStyle.Fill; this.gbDetallePago.Text = "Detalle pago";
            this.rtbDetallePago.Dock = System.Windows.Forms.DockStyle.Fill; this.rtbDetallePago.ReadOnly = true;
            this.panelBotonesPagos.Controls.Add(this.btnImprimirComprobante); this.panelBotonesPagos.Controls.Add(this.btnActualizar2); this.panelBotonesPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnActualizar2.Location = new System.Drawing.Point(50, 17); this.btnActualizar2.Size = new System.Drawing.Size(150, 35); this.btnActualizar2.Text = "Actualizar"; this.btnActualizar2.Click += new System.EventHandler(this.btnActualizar2_Click);
            this.btnImprimirComprobante.Location = new System.Drawing.Point(230, 17); this.btnImprimirComprobante.Size = new System.Drawing.Size(190, 35); this.btnImprimirComprobante.Text = "Imprimir comprobante"; this.btnImprimirComprobante.Click += new System.EventHandler(this.btnImprimirComprobante_Click);
            // 
            // FrmPagoProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 662);
            this.Controls.Add(this.tabControlPagos);
            this.Name = "FrmPagoProveedor";
            this.Tag = "FrmPagoProveedor";
            this.Text = "Pago a Proveedores";
            this.Load += new System.EventHandler(this.FrmPagoProveedor_Load);
            this.tabControlPagos.ResumeLayout(false);
            this.tabRegistrarPago.ResumeLayout(false);
            this.tableLayoutPrincipal.ResumeLayout(false);
            this.gbOrdenes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            this.tableLayoutDatos.ResumeLayout(false);
            this.gbDatosPago.ResumeLayout(false);
            this.gbDatosPago.PerformLayout();
            this.gbDetalleMedioPago.ResumeLayout(false);
            this.panelMedios.ResumeLayout(false);
            this.gbTarjeta.ResumeLayout(false);
            this.gbTarjeta.PerformLayout();
            this.gbTransferencia.ResumeLayout(false);
            this.gbTransferencia.PerformLayout();
            this.gbEfectivo.ResumeLayout(false);
            this.gbEfectivo.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.tabPagosRegistrados.ResumeLayout(false);
            this.tableLayoutPagos.ResumeLayout(false);
            this.gbPagos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).EndInit();
            this.gbDetallePago.ResumeLayout(false);
            this.panelBotonesPagos.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPagos;
        private System.Windows.Forms.TabPage tabRegistrarPago;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPrincipal;
        private System.Windows.Forms.GroupBox gbOrdenes;
        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutDatos;
        private System.Windows.Forms.GroupBox gbDatosPago;
        private System.Windows.Forms.TextBox txtImporteFactura;
        private System.Windows.Forms.Label lblImporteFactura;
        private System.Windows.Forms.DateTimePicker dtpFechaFactura;
        private System.Windows.Forms.Label lblFechaFactura;
        private System.Windows.Forms.TextBox txtNumeroFactura;
        private System.Windows.Forms.Label lblNumeroFactura;
        private System.Windows.Forms.ComboBox cmbMedioPago;
        private System.Windows.Forms.Label lblMedioPago;
        private System.Windows.Forms.DateTimePicker dtpFechaPago;
        private System.Windows.Forms.Label lblFechaPago;
        private System.Windows.Forms.TextBox txtImportePago;
        private System.Windows.Forms.Label lblImportePago;
        private System.Windows.Forms.TextBox txtTotalOrden;
        private System.Windows.Forms.Label lblTotalOrden;
        private System.Windows.Forms.TextBox txtOrden;
        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.GroupBox gbDetalleMedioPago;
        private System.Windows.Forms.Panel panelMedios;
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
        private System.Windows.Forms.TabPage tabPagosRegistrados;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPagos;
        private System.Windows.Forms.GroupBox gbPagos = new System.Windows.Forms.GroupBox();
        private System.Windows.Forms.DataGridView dgvPagos = new System.Windows.Forms.DataGridView();
        private System.Windows.Forms.GroupBox gbDetallePago = new System.Windows.Forms.GroupBox();
        private System.Windows.Forms.RichTextBox rtbDetallePago = new System.Windows.Forms.RichTextBox();
        private System.Windows.Forms.Panel panelBotonesPagos = new System.Windows.Forms.Panel();
        private System.Windows.Forms.Button btnActualizar2;
        private System.Windows.Forms.Button btnImprimirComprobante;
    }
}
