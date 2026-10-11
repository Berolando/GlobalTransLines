namespace GlobalTransLines.UI.Gestion1_Booking
{
    partial class FrmTarifario
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Codigo generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.pnlCabecera = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpTarifas = new System.Windows.Forms.GroupBox();
            this.dgvTarifas = new System.Windows.Forms.DataGridView();
            this.grpDetalle = new System.Windows.Forms.GroupBox();
            this.btnEstado = new System.Windows.Forms.Button();
            this.btnNueva = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.txtCoefVolumen = new System.Windows.Forms.TextBox();
            this.lblCoefVolumen = new System.Windows.Forms.Label();
            this.txtCoefPeso = new System.Windows.Forms.TextBox();
            this.lblCoefPeso = new System.Windows.Forms.Label();
            this.txtBaseUSD = new System.Windows.Forms.TextBox();
            this.lblBaseUSD = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.cmbTipoContenedor = new System.Windows.Forms.ComboBox();
            this.lblTipoContenedor = new System.Windows.Forms.Label();
            this.cmbTipoCarga = new System.Windows.Forms.ComboBox();
            this.lblTipoCarga = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.pnlCabecera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpTarifas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarifas)).BeginInit();
            this.grpDetalle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCabecera
            // 
            this.pnlCabecera.Controls.Add(this.lblTitulo);
            this.pnlCabecera.Controls.Add(this.lblAyuda);
            this.pnlCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCabecera.Location = new System.Drawing.Point(0, 0);
            this.pnlCabecera.Name = "pnlCabecera";
            this.pnlCabecera.Padding = new System.Windows.Forms.Padding(14, 10, 14, 6);
            this.pnlCabecera.Size = new System.Drawing.Size(1100, 76);
            this.pnlCabecera.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(14, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(256, 31);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Tarifario de cotizacion";
            // 
            // lblAyuda
            // 
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.Location = new System.Drawing.Point(17, 48);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(723, 17);
            this.lblAyuda.TabIndex = 1;
            this.lblAyuda.Text = "Seleccione una tarifa para modificar los valores que usa la BLL al calcular el flete por carga y contenedor.";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 76);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpTarifas);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.grpDetalle);
            this.splitContainer.Size = new System.Drawing.Size(1100, 624);
            this.splitContainer.SplitterDistance = 690;
            this.splitContainer.TabIndex = 1;
            // 
            // grpTarifas
            // 
            this.grpTarifas.Controls.Add(this.dgvTarifas);
            this.grpTarifas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTarifas.Location = new System.Drawing.Point(0, 0);
            this.grpTarifas.Name = "grpTarifas";
            this.grpTarifas.Padding = new System.Windows.Forms.Padding(10);
            this.grpTarifas.Size = new System.Drawing.Size(690, 624);
            this.grpTarifas.TabIndex = 0;
            this.grpTarifas.TabStop = false;
            this.grpTarifas.Text = "Tarifas configuradas";
            // 
            // dgvTarifas
            // 
            this.dgvTarifas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTarifas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTarifas.Location = new System.Drawing.Point(10, 25);
            this.dgvTarifas.Name = "dgvTarifas";
            this.dgvTarifas.RowHeadersWidth = 35;
            this.dgvTarifas.RowTemplate.Height = 24;
            this.dgvTarifas.Size = new System.Drawing.Size(670, 589);
            this.dgvTarifas.TabIndex = 0;
            this.dgvTarifas.SelectionChanged += new System.EventHandler(this.dgvTarifas_SelectionChanged);
            // 
            // grpDetalle
            // 
            this.grpDetalle.Controls.Add(this.btnLimpiar);
            this.grpDetalle.Controls.Add(this.btnActualizar);
            this.grpDetalle.Controls.Add(this.btnEstado);
            this.grpDetalle.Controls.Add(this.btnNueva);
            this.grpDetalle.Controls.Add(this.btnGuardar);
            this.grpDetalle.Controls.Add(this.btnCerrar);
            this.grpDetalle.Controls.Add(this.txtCoefVolumen);
            this.grpDetalle.Controls.Add(this.lblCoefVolumen);
            this.grpDetalle.Controls.Add(this.txtCoefPeso);
            this.grpDetalle.Controls.Add(this.lblCoefPeso);
            this.grpDetalle.Controls.Add(this.txtBaseUSD);
            this.grpDetalle.Controls.Add(this.lblBaseUSD);
            this.grpDetalle.Controls.Add(this.txtDescripcion);
            this.grpDetalle.Controls.Add(this.lblDescripcion);
            this.grpDetalle.Controls.Add(this.cmbTipoContenedor);
            this.grpDetalle.Controls.Add(this.lblTipoContenedor);
            this.grpDetalle.Controls.Add(this.cmbTipoCarga);
            this.grpDetalle.Controls.Add(this.lblTipoCarga);
            this.grpDetalle.Controls.Add(this.txtId);
            this.grpDetalle.Controls.Add(this.lblId);
            this.grpDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDetalle.Location = new System.Drawing.Point(0, 0);
            this.grpDetalle.Name = "grpDetalle";
            this.grpDetalle.Padding = new System.Windows.Forms.Padding(14);
            this.grpDetalle.Size = new System.Drawing.Size(406, 624);
            this.grpDetalle.TabIndex = 0;
            this.grpDetalle.TabStop = false;
            this.grpDetalle.Text = "Detalle de tarifa";
            // 
            // btnEstado
            // 
            this.btnEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEstado.Location = new System.Drawing.Point(273, 535);
            this.btnEstado.Name = "btnEstado";
            this.btnEstado.Size = new System.Drawing.Size(116, 32);
            this.btnEstado.TabIndex = 16;
            this.btnEstado.Text = "Desactivar";
            this.btnEstado.UseVisualStyleBackColor = true;
            this.btnEstado.Click += new System.EventHandler(this.btnEstado_Click);
            // 
            // btnNueva
            // 
            this.btnNueva.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNueva.Location = new System.Drawing.Point(17, 535);
            this.btnNueva.Name = "btnNueva";
            this.btnNueva.Size = new System.Drawing.Size(120, 32);
            this.btnNueva.TabIndex = 14;
            this.btnNueva.Text = "Nueva";
            this.btnNueva.UseVisualStyleBackColor = true;
            this.btnNueva.Click += new System.EventHandler(this.btnNueva_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiar.Location = new System.Drawing.Point(281, 575);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(108, 32);
            this.btnLimpiar.TabIndex = 19;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualizar.Location = new System.Drawing.Point(167, 575);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(108, 32);
            this.btnActualizar.TabIndex = 18;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGuardar.Location = new System.Drawing.Point(145, 535);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 32);
            this.btnGuardar.TabIndex = 15;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCerrar.Location = new System.Drawing.Point(17, 575);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 32);
            this.btnCerrar.TabIndex = 20;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // txtCoefVolumen
            // 
            this.txtCoefVolumen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCoefVolumen.Location = new System.Drawing.Point(17, 366);
            this.txtCoefVolumen.Name = "txtCoefVolumen";
            this.txtCoefVolumen.Size = new System.Drawing.Size(372, 22);
            this.txtCoefVolumen.TabIndex = 13;
            // 
            // lblCoefVolumen
            // 
            this.lblCoefVolumen.AutoSize = true;
            this.lblCoefVolumen.Location = new System.Drawing.Point(17, 346);
            this.lblCoefVolumen.Name = "lblCoefVolumen";
            this.lblCoefVolumen.Size = new System.Drawing.Size(118, 17);
            this.lblCoefVolumen.TabIndex = 12;
            this.lblCoefVolumen.Text = "USD por m³";
            // 
            // txtCoefPeso
            // 
            this.txtCoefPeso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCoefPeso.Location = new System.Drawing.Point(17, 309);
            this.txtCoefPeso.Name = "txtCoefPeso";
            this.txtCoefPeso.Size = new System.Drawing.Size(372, 22);
            this.txtCoefPeso.TabIndex = 11;
            // 
            // lblCoefPeso
            // 
            this.lblCoefPeso.AutoSize = true;
            this.lblCoefPeso.Location = new System.Drawing.Point(17, 289);
            this.lblCoefPeso.Name = "lblCoefPeso";
            this.lblCoefPeso.Size = new System.Drawing.Size(97, 17);
            this.lblCoefPeso.TabIndex = 10;
            this.lblCoefPeso.Text = "USD por tonelada";
            // 
            // txtBaseUSD
            // 
            this.txtBaseUSD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBaseUSD.Location = new System.Drawing.Point(17, 252);
            this.txtBaseUSD.Name = "txtBaseUSD";
            this.txtBaseUSD.Size = new System.Drawing.Size(372, 22);
            this.txtBaseUSD.TabIndex = 9;
            // 
            // lblBaseUSD
            // 
            this.lblBaseUSD.AutoSize = true;
            this.lblBaseUSD.Location = new System.Drawing.Point(17, 232);
            this.lblBaseUSD.Name = "lblBaseUSD";
            this.lblBaseUSD.Size = new System.Drawing.Size(67, 17);
            this.lblBaseUSD.TabIndex = 8;
            this.lblBaseUSD.Text = "Base USD";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescripcion.Location = new System.Drawing.Point(17, 195);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(372, 22);
            this.txtDescripcion.TabIndex = 7;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(17, 175);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(82, 17);
            this.lblDescripcion.TabIndex = 6;
            this.lblDescripcion.Text = "Descripcion";
            // 
            // cmbTipoContenedor
            // 
            this.cmbTipoContenedor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTipoContenedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoContenedor.FormattingEnabled = true;
            this.cmbTipoContenedor.Location = new System.Drawing.Point(17, 138);
            this.cmbTipoContenedor.Name = "cmbTipoContenedor";
            this.cmbTipoContenedor.Size = new System.Drawing.Size(372, 24);
            this.cmbTipoContenedor.TabIndex = 5;
            // 
            // lblTipoContenedor
            // 
            this.lblTipoContenedor.AutoSize = true;
            this.lblTipoContenedor.Location = new System.Drawing.Point(17, 118);
            this.lblTipoContenedor.Name = "lblTipoContenedor";
            this.lblTipoContenedor.Size = new System.Drawing.Size(122, 17);
            this.lblTipoContenedor.TabIndex = 4;
            this.lblTipoContenedor.Text = "Tipo de contenedor";
            // 
            // cmbTipoCarga
            // 
            this.cmbTipoCarga.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTipoCarga.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoCarga.FormattingEnabled = true;
            this.cmbTipoCarga.Location = new System.Drawing.Point(17, 81);
            this.cmbTipoCarga.Name = "cmbTipoCarga";
            this.cmbTipoCarga.Size = new System.Drawing.Size(372, 24);
            this.cmbTipoCarga.TabIndex = 3;
            this.cmbTipoCarga.SelectedIndexChanged += new System.EventHandler(this.cmbTipoCarga_SelectedIndexChanged);
            // 
            // lblTipoCarga
            // 
            this.lblTipoCarga.AutoSize = true;
            this.lblTipoCarga.Location = new System.Drawing.Point(17, 61);
            this.lblTipoCarga.Name = "lblTipoCarga";
            this.lblTipoCarga.Size = new System.Drawing.Size(93, 17);
            this.lblTipoCarga.TabIndex = 2;
            this.lblTipoCarga.Text = "Tipo de carga";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(17, 36);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(100, 22);
            this.txtId.TabIndex = 1;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(17, 18);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(19, 17);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Id";
            // 
            // FrmTarifario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlCabecera);
            this.Name = "FrmTarifario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tarifario";
            this.Load += new System.EventHandler(this.FrmTarifario_Load);
            this.pnlCabecera.ResumeLayout(false);
            this.pnlCabecera.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpTarifas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTarifas)).EndInit();
            this.grpDetalle.ResumeLayout(false);
            this.grpDetalle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCabecera;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox grpTarifas;
        private System.Windows.Forms.DataGridView dgvTarifas;
        private System.Windows.Forms.GroupBox grpDetalle;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.ComboBox cmbTipoCarga;
        private System.Windows.Forms.Label lblTipoCarga;
        private System.Windows.Forms.ComboBox cmbTipoContenedor;
        private System.Windows.Forms.Label lblTipoContenedor;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtBaseUSD;
        private System.Windows.Forms.Label lblBaseUSD;
        private System.Windows.Forms.TextBox txtCoefPeso;
        private System.Windows.Forms.Label lblCoefPeso;
        private System.Windows.Forms.TextBox txtCoefVolumen;
        private System.Windows.Forms.Label lblCoefVolumen;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnNueva;
        private System.Windows.Forms.Button btnEstado;
        private System.Windows.Forms.Button btnCerrar;
    }
}
