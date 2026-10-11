namespace GlobalTransLines.UI.Gestion2_Transporte
{
    partial class FrmBillOfLading
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabEmitir = new System.Windows.Forms.TabPage();
            this.layoutEmitir = new System.Windows.Forms.TableLayoutPanel();
            this.gbAsignaciones = new System.Windows.Forms.GroupBox();
            this.dgvAsignaciones = new System.Windows.Forms.DataGridView();
            this.panelDatos = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPuertoDestino = new System.Windows.Forms.Label();
            this.txtPuertoDestino = new System.Windows.Forms.TextBox();
            this.lblFechaEmision = new System.Windows.Forms.Label();
            this.dtpFechaEmision = new System.Windows.Forms.DateTimePicker();
            this.gbDetalle = new System.Windows.Forms.GroupBox();
            this.rtbDetalle = new System.Windows.Forms.RichTextBox();
            this.panelBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEmitir = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.tabEmitidos = new System.Windows.Forms.TabPage();
            this.layoutEmitidos = new System.Windows.Forms.TableLayoutPanel();
            this.gbBills = new System.Windows.Forms.GroupBox();
            this.dgvBills = new System.Windows.Forms.DataGridView();
            this.gbDetalleEmitido = new System.Windows.Forms.GroupBox();
            this.rtbDetalleEmitido = new System.Windows.Forms.RichTextBox();
            this.panelBotonesEmitidos = new System.Windows.Forms.FlowLayoutPanel();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnCerrarEmitidos = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabEmitir.SuspendLayout();
            this.layoutEmitir.SuspendLayout();
            this.gbAsignaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsignaciones)).BeginInit();
            this.panelDatos.SuspendLayout();
            this.gbDetalle.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.tabEmitidos.SuspendLayout();
            this.layoutEmitidos.SuspendLayout();
            this.gbBills.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBills)).BeginInit();
            this.gbDetalleEmitido.SuspendLayout();
            this.panelBotonesEmitidos.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabEmitir);
            this.tabControl.Controls.Add(this.tabEmitidos);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1200, 700);
            this.tabControl.TabIndex = 0;
            // 
            // tabEmitir
            // 
            this.tabEmitir.Controls.Add(this.layoutEmitir);
            this.tabEmitir.Location = new System.Drawing.Point(4, 25);
            this.tabEmitir.Name = "tabEmitir";
            this.tabEmitir.Padding = new System.Windows.Forms.Padding(3);
            this.tabEmitir.Size = new System.Drawing.Size(1192, 671);
            this.tabEmitir.TabIndex = 0;
            this.tabEmitir.Text = "Emitir Bill of Lading";
            this.tabEmitir.UseVisualStyleBackColor = true;
            // 
            // layoutEmitir
            // 
            this.layoutEmitir.ColumnCount = 1;
            this.layoutEmitir.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutEmitir.Controls.Add(this.gbAsignaciones, 0, 0);
            this.layoutEmitir.Controls.Add(this.panelDatos, 0, 1);
            this.layoutEmitir.Controls.Add(this.gbDetalle, 0, 2);
            this.layoutEmitir.Controls.Add(this.panelBotones, 0, 3);
            this.layoutEmitir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutEmitir.Location = new System.Drawing.Point(3, 3);
            this.layoutEmitir.Name = "layoutEmitir";
            this.layoutEmitir.RowCount = 4;
            this.layoutEmitir.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.layoutEmitir.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.layoutEmitir.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.layoutEmitir.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.layoutEmitir.Size = new System.Drawing.Size(1186, 665);
            this.layoutEmitir.TabIndex = 0;
            // 
            // gbAsignaciones
            // 
            this.gbAsignaciones.Controls.Add(this.dgvAsignaciones);
            this.gbAsignaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAsignaciones.Location = new System.Drawing.Point(3, 3);
            this.gbAsignaciones.Name = "gbAsignaciones";
            this.gbAsignaciones.Size = new System.Drawing.Size(1180, 282);
            this.gbAsignaciones.TabIndex = 0;
            this.gbAsignaciones.TabStop = false;
            this.gbAsignaciones.Text = "Bookings con buque y contenedor asignados";
            // 
            // dgvAsignaciones
            // 
            this.dgvAsignaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAsignaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAsignaciones.Location = new System.Drawing.Point(3, 18);
            this.dgvAsignaciones.Name = "dgvAsignaciones";
            this.dgvAsignaciones.RowHeadersWidth = 51;
            this.dgvAsignaciones.RowTemplate.Height = 24;
            this.dgvAsignaciones.Size = new System.Drawing.Size(1174, 261);
            this.dgvAsignaciones.TabIndex = 0;
            this.dgvAsignaciones.SelectionChanged += new System.EventHandler(this.dgvAsignaciones_SelectionChanged);
            // 
            // panelDatos
            // 
            this.panelDatos.Controls.Add(this.lblPuertoDestino);
            this.panelDatos.Controls.Add(this.txtPuertoDestino);
            this.panelDatos.Controls.Add(this.lblFechaEmision);
            this.panelDatos.Controls.Add(this.dtpFechaEmision);
            this.panelDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDatos.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelDatos.Location = new System.Drawing.Point(3, 291);
            this.panelDatos.Name = "panelDatos";
            this.panelDatos.Padding = new System.Windows.Forms.Padding(12);
            this.panelDatos.Size = new System.Drawing.Size(1180, 64);
            this.panelDatos.TabIndex = 1;
            // 
            // lblPuertoDestino
            // 
            this.lblPuertoDestino.AutoSize = true;
            this.lblPuertoDestino.Location = new System.Drawing.Point(15, 18);
            this.lblPuertoDestino.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblPuertoDestino.Name = "lblPuertoDestino";
            this.lblPuertoDestino.Size = new System.Drawing.Size(104, 22);
            this.lblPuertoDestino.TabIndex = 0;
            this.lblPuertoDestino.Text = "Puerto destino:";
            // 
            // txtPuertoDestino
            // 
            this.txtPuertoDestino.Location = new System.Drawing.Point(125, 15);
            this.txtPuertoDestino.Name = "txtPuertoDestino";
            this.txtPuertoDestino.Size = new System.Drawing.Size(320, 22);
            this.txtPuertoDestino.TabIndex = 1;
            // 
            // lblFechaEmision
            // 
            this.lblFechaEmision.AutoSize = true;
            this.lblFechaEmision.Location = new System.Drawing.Point(468, 18);
            this.lblFechaEmision.Margin = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.lblFechaEmision.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblFechaEmision.Name = "lblFechaEmision";
            this.lblFechaEmision.Size = new System.Drawing.Size(103, 22);
            this.lblFechaEmision.TabIndex = 2;
            this.lblFechaEmision.Text = "Fecha emision:";
            // 
            // dtpFechaEmision
            // 
            this.dtpFechaEmision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEmision.Location = new System.Drawing.Point(577, 15);
            this.dtpFechaEmision.Name = "dtpFechaEmision";
            this.dtpFechaEmision.Size = new System.Drawing.Size(140, 22);
            this.dtpFechaEmision.TabIndex = 3;
            // 
            // gbDetalle
            // 
            this.gbDetalle.Controls.Add(this.rtbDetalle);
            this.gbDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetalle.Location = new System.Drawing.Point(3, 361);
            this.gbDetalle.Name = "gbDetalle";
            this.gbDetalle.Size = new System.Drawing.Size(1180, 230);
            this.gbDetalle.TabIndex = 2;
            this.gbDetalle.TabStop = false;
            this.gbDetalle.Text = "Detalle a documentar";
            // 
            // rtbDetalle
            // 
            this.rtbDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDetalle.Location = new System.Drawing.Point(3, 18);
            this.rtbDetalle.Name = "rtbDetalle";
            this.rtbDetalle.ReadOnly = true;
            this.rtbDetalle.Size = new System.Drawing.Size(1174, 209);
            this.rtbDetalle.TabIndex = 0;
            this.rtbDetalle.Text = "";
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnEmitir);
            this.panelBotones.Controls.Add(this.btnActualizar);
            this.panelBotones.Controls.Add(this.btnCerrar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBotones.Location = new System.Drawing.Point(3, 597);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Padding = new System.Windows.Forms.Padding(12);
            this.panelBotones.Size = new System.Drawing.Size(1180, 65);
            this.panelBotones.TabIndex = 3;
            // 
            // btnEmitir
            // 
            this.btnEmitir.Location = new System.Drawing.Point(15, 15);
            this.btnEmitir.Name = "btnEmitir";
            this.btnEmitir.Size = new System.Drawing.Size(180, 34);
            this.btnEmitir.TabIndex = 0;
            this.btnEmitir.Text = "Emitir Bill of Lading";
            this.btnEmitir.UseVisualStyleBackColor = true;
            this.btnEmitir.Click += new System.EventHandler(this.btnEmitir_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(201, 15);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(130, 34);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(337, 15);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(130, 34);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // tabEmitidos
            // 
            this.tabEmitidos.Controls.Add(this.layoutEmitidos);
            this.tabEmitidos.Location = new System.Drawing.Point(4, 25);
            this.tabEmitidos.Name = "tabEmitidos";
            this.tabEmitidos.Padding = new System.Windows.Forms.Padding(3);
            this.tabEmitidos.Size = new System.Drawing.Size(1192, 671);
            this.tabEmitidos.TabIndex = 1;
            this.tabEmitidos.Text = "Bills emitidos";
            this.tabEmitidos.UseVisualStyleBackColor = true;
            // 
            // layoutEmitidos
            // 
            this.layoutEmitidos.ColumnCount = 1;
            this.layoutEmitidos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutEmitidos.Controls.Add(this.gbBills, 0, 0);
            this.layoutEmitidos.Controls.Add(this.gbDetalleEmitido, 0, 1);
            this.layoutEmitidos.Controls.Add(this.panelBotonesEmitidos, 0, 2);
            this.layoutEmitidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutEmitidos.Location = new System.Drawing.Point(3, 3);
            this.layoutEmitidos.Name = "layoutEmitidos";
            this.layoutEmitidos.RowCount = 3;
            this.layoutEmitidos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layoutEmitidos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layoutEmitidos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.layoutEmitidos.Size = new System.Drawing.Size(1186, 665);
            this.layoutEmitidos.TabIndex = 0;
            // 
            // gbBills
            // 
            this.gbBills.Controls.Add(this.dgvBills);
            this.gbBills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBills.Location = new System.Drawing.Point(3, 3);
            this.gbBills.Name = "gbBills";
            this.gbBills.Size = new System.Drawing.Size(1180, 351);
            this.gbBills.TabIndex = 0;
            this.gbBills.TabStop = false;
            this.gbBills.Text = "Bill of Lading emitidos";
            // 
            // dgvBills
            // 
            this.dgvBills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBills.Location = new System.Drawing.Point(3, 18);
            this.dgvBills.Name = "dgvBills";
            this.dgvBills.RowHeadersWidth = 51;
            this.dgvBills.RowTemplate.Height = 24;
            this.dgvBills.Size = new System.Drawing.Size(1174, 330);
            this.dgvBills.TabIndex = 0;
            this.dgvBills.SelectionChanged += new System.EventHandler(this.dgvBills_SelectionChanged);
            // 
            // gbDetalleEmitido
            // 
            this.gbDetalleEmitido.Controls.Add(this.rtbDetalleEmitido);
            this.gbDetalleEmitido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetalleEmitido.Location = new System.Drawing.Point(3, 360);
            this.gbDetalleEmitido.Name = "gbDetalleEmitido";
            this.gbDetalleEmitido.Size = new System.Drawing.Size(1180, 232);
            this.gbDetalleEmitido.TabIndex = 1;
            this.gbDetalleEmitido.TabStop = false;
            this.gbDetalleEmitido.Text = "Detalle del documento seleccionado";
            // 
            // rtbDetalleEmitido
            // 
            this.rtbDetalleEmitido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDetalleEmitido.Location = new System.Drawing.Point(3, 18);
            this.rtbDetalleEmitido.Name = "rtbDetalleEmitido";
            this.rtbDetalleEmitido.ReadOnly = true;
            this.rtbDetalleEmitido.Size = new System.Drawing.Size(1174, 211);
            this.rtbDetalleEmitido.TabIndex = 0;
            this.rtbDetalleEmitido.Text = "";
            // 
            // panelBotonesEmitidos
            // 
            this.panelBotonesEmitidos.Controls.Add(this.btnImprimir);
            this.panelBotonesEmitidos.Controls.Add(this.btnCerrarEmitidos);
            this.panelBotonesEmitidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBotonesEmitidos.Location = new System.Drawing.Point(3, 598);
            this.panelBotonesEmitidos.Name = "panelBotonesEmitidos";
            this.panelBotonesEmitidos.Padding = new System.Windows.Forms.Padding(12);
            this.panelBotonesEmitidos.Size = new System.Drawing.Size(1180, 64);
            this.panelBotonesEmitidos.TabIndex = 2;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(15, 15);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(140, 34);
            this.btnImprimir.TabIndex = 0;
            this.btnImprimir.Text = "Imprimir PDF";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCerrarEmitidos
            // 
            this.btnCerrarEmitidos.Location = new System.Drawing.Point(161, 15);
            this.btnCerrarEmitidos.Name = "btnCerrarEmitidos";
            this.btnCerrarEmitidos.Size = new System.Drawing.Size(130, 34);
            this.btnCerrarEmitidos.TabIndex = 1;
            this.btnCerrarEmitidos.Text = "Cerrar";
            this.btnCerrarEmitidos.UseVisualStyleBackColor = true;
            this.btnCerrarEmitidos.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FrmBillOfLading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tabControl);
            this.Name = "FrmBillOfLading";
            this.Tag = "FrmBillOfLading";
            this.Text = "Bill of Lading";
            this.Load += new System.EventHandler(this.FrmBillOfLading_Load);
            this.tabControl.ResumeLayout(false);
            this.tabEmitir.ResumeLayout(false);
            this.layoutEmitir.ResumeLayout(false);
            this.gbAsignaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsignaciones)).EndInit();
            this.panelDatos.ResumeLayout(false);
            this.panelDatos.PerformLayout();
            this.gbDetalle.ResumeLayout(false);
            this.panelBotones.ResumeLayout(false);
            this.tabEmitidos.ResumeLayout(false);
            this.layoutEmitidos.ResumeLayout(false);
            this.gbBills.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBills)).EndInit();
            this.gbDetalleEmitido.ResumeLayout(false);
            this.panelBotonesEmitidos.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabEmitir;
        private System.Windows.Forms.TableLayoutPanel layoutEmitir;
        private System.Windows.Forms.GroupBox gbAsignaciones;
        private System.Windows.Forms.DataGridView dgvAsignaciones;
        private System.Windows.Forms.FlowLayoutPanel panelDatos;
        private System.Windows.Forms.Label lblPuertoDestino;
        private System.Windows.Forms.TextBox txtPuertoDestino;
        private System.Windows.Forms.Label lblFechaEmision;
        private System.Windows.Forms.DateTimePicker dtpFechaEmision;
        private System.Windows.Forms.GroupBox gbDetalle;
        private System.Windows.Forms.RichTextBox rtbDetalle;
        private System.Windows.Forms.FlowLayoutPanel panelBotones;
        private System.Windows.Forms.Button btnEmitir;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TabPage tabEmitidos;
        private System.Windows.Forms.TableLayoutPanel layoutEmitidos;
        private System.Windows.Forms.GroupBox gbBills;
        private System.Windows.Forms.DataGridView dgvBills;
        private System.Windows.Forms.GroupBox gbDetalleEmitido;
        private System.Windows.Forms.RichTextBox rtbDetalleEmitido;
        private System.Windows.Forms.FlowLayoutPanel panelBotonesEmitidos;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnCerrarEmitidos;
    }
}
