namespace GlobalTransLines.UI.Infra
{
    partial class FrmBitacora
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region "Inicialización del formulario"

        private void InitializeComponent()
        {
            this.dgvEventos = new System.Windows.Forms.DataGridView();
            this.grpFiltrosPrincipales = new System.Windows.Forms.GroupBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblTipoOperacion = new System.Windows.Forms.Label();
            this.cmbUsuario = new System.Windows.Forms.ComboBox();
            this.cmbTipoOperacion = new System.Windows.Forms.ComboBox();
            this.grpRangoFechas = new System.Windows.Forms.GroupBox();
            this.lblDesde = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.btnRestablecer = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).BeginInit();
            this.grpFiltrosPrincipales.SuspendLayout();
            this.grpRangoFechas.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvEventos
            // 
            this.dgvEventos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEventos.Location = new System.Drawing.Point(12, 248);
            this.dgvEventos.Name = "dgvEventos";
            this.dgvEventos.RowHeadersWidth = 51;
            this.dgvEventos.RowTemplate.Height = 24;
            this.dgvEventos.Size = new System.Drawing.Size(976, 400);
            this.dgvEventos.TabIndex = 0;
            // 
            // grpFiltrosPrincipales
            // 
            this.grpFiltrosPrincipales.Controls.Add(this.lblUsuario);
            this.grpFiltrosPrincipales.Controls.Add(this.lblTipoOperacion);
            this.grpFiltrosPrincipales.Controls.Add(this.cmbUsuario);
            this.grpFiltrosPrincipales.Controls.Add(this.cmbTipoOperacion);
            this.grpFiltrosPrincipales.Location = new System.Drawing.Point(12, 12);
            this.grpFiltrosPrincipales.Name = "grpFiltrosPrincipales";
            this.grpFiltrosPrincipales.Size = new System.Drawing.Size(360, 150);
            this.grpFiltrosPrincipales.TabIndex = 1;
            this.grpFiltrosPrincipales.TabStop = false;
            this.grpFiltrosPrincipales.Text = "Filtros principales";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(10, 90);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(57, 16);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario:";
            // 
            // lblTipoOperacion
            // 
            this.lblTipoOperacion.AutoSize = true;
            this.lblTipoOperacion.Location = new System.Drawing.Point(10, 35);
            this.lblTipoOperacion.Name = "lblTipoOperacion";
            this.lblTipoOperacion.Size = new System.Drawing.Size(121, 16);
            this.lblTipoOperacion.TabIndex = 1;
            this.lblTipoOperacion.Text = "Tipo de operación:";
            // 
            // cmbUsuario
            // 
            this.cmbUsuario.FormattingEnabled = true;
            this.cmbUsuario.Location = new System.Drawing.Point(150, 85);
            this.cmbUsuario.Name = "cmbUsuario";
            this.cmbUsuario.Size = new System.Drawing.Size(180, 24);
            this.cmbUsuario.TabIndex = 2;
            // 
            // cmbTipoOperacion
            // 
            this.cmbTipoOperacion.FormattingEnabled = true;
            this.cmbTipoOperacion.Location = new System.Drawing.Point(150, 30);
            this.cmbTipoOperacion.Name = "cmbTipoOperacion";
            this.cmbTipoOperacion.Size = new System.Drawing.Size(180, 24);
            this.cmbTipoOperacion.TabIndex = 3;
            // 
            // grpRangoFechas
            // 
            this.grpRangoFechas.Controls.Add(this.lblDesde);
            this.grpRangoFechas.Controls.Add(this.lblHasta);
            this.grpRangoFechas.Controls.Add(this.dtpDesde);
            this.grpRangoFechas.Controls.Add(this.dtpHasta);
            this.grpRangoFechas.Location = new System.Drawing.Point(388, 12);
            this.grpRangoFechas.Name = "grpRangoFechas";
            this.grpRangoFechas.Size = new System.Drawing.Size(330, 150);
            this.grpRangoFechas.TabIndex = 2;
            this.grpRangoFechas.TabStop = false;
            this.grpRangoFechas.Text = "Rango de fechas";
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(15, 35);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(51, 16);
            this.lblDesde.TabIndex = 0;
            this.lblDesde.Text = "Desde:";
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(15, 90);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(46, 16);
            this.lblHasta.TabIndex = 1;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(85, 30);
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(130, 22);
            this.dtpDesde.TabIndex = 2;
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(85, 85);
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(130, 22);
            this.dtpHasta.TabIndex = 3;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(12, 178);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(100, 43);
            this.btnFiltrar.TabIndex = 3;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // btnRestablecer
            // 
            this.btnRestablecer.Location = new System.Drawing.Point(132, 178);
            this.btnRestablecer.Name = "btnRestablecer";
            this.btnRestablecer.Size = new System.Drawing.Size(120, 43);
            this.btnRestablecer.TabIndex = 4;
            this.btnRestablecer.Text = "Restablecer";
            this.btnRestablecer.UseVisualStyleBackColor = true;
            this.btnRestablecer.Click += new System.EventHandler(this.btnRestablecer_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Location = new System.Drawing.Point(860, 178);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 43);
            this.btnCerrar.TabIndex = 6;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(388, 192);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 16);
            this.lblEstado.TabIndex = 5;
            // 
            // FrmBitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 660);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.dgvEventos);
            this.Controls.Add(this.grpFiltrosPrincipales);
            this.Controls.Add(this.grpRangoFechas);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.btnRestablecer);
            this.Controls.Add(this.btnCerrar);
            this.Name = "FrmBitacora";
            this.Tag = "FrmBitacora";
            this.Text = "Bitácora del Sistema";
            this.Load += new System.EventHandler(this.FrmBitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).EndInit();
            this.grpFiltrosPrincipales.ResumeLayout(false);
            this.grpFiltrosPrincipales.PerformLayout();
            this.grpRangoFechas.ResumeLayout(false);
            this.grpRangoFechas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEventos;
        private System.Windows.Forms.GroupBox grpFiltrosPrincipales;
        private System.Windows.Forms.GroupBox grpRangoFechas;
        private System.Windows.Forms.ComboBox cmbTipoOperacion;
        private System.Windows.Forms.ComboBox cmbUsuario;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label lblTipoOperacion;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Button btnRestablecer;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblEstado;
    }
}
