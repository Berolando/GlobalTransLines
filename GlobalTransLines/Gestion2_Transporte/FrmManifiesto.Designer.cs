namespace GlobalTransLines.UI.Gestion2_Transporte
{
    partial class FrmManifiesto
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
            this.tabGenerar = new System.Windows.Forms.TabPage();
            this.layoutGenerar = new System.Windows.Forms.TableLayoutPanel();
            this.panelDatos = new System.Windows.Forms.FlowLayoutPanel();
            this.lblBuque = new System.Windows.Forms.Label();
            this.cmbBuques = new System.Windows.Forms.ComboBox();
            this.lblAutoridadDestino = new System.Windows.Forms.Label();
            this.txtAutoridadDestino = new System.Windows.Forms.TextBox();
            this.lblFechaEmision = new System.Windows.Forms.Label();
            this.dtpFechaEmision = new System.Windows.Forms.DateTimePicker();
            this.lblDestinoRapido = new System.Windows.Forms.Label();
            this.cmbDestinos = new System.Windows.Forms.ComboBox();
            this.btnSeleccionarDestino = new System.Windows.Forms.Button();
            this.lblCriterio = new System.Windows.Forms.Label();
            this.gbContenedores = new System.Windows.Forms.GroupBox();
            this.dgvContenedores = new System.Windows.Forms.DataGridView();
            this.gbDetalle = new System.Windows.Forms.GroupBox();
            this.rtbDetalle = new System.Windows.Forms.RichTextBox();
            this.panelBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.tabGenerados = new System.Windows.Forms.TabPage();
            this.layoutGenerados = new System.Windows.Forms.TableLayoutPanel();
            this.gbManifiestos = new System.Windows.Forms.GroupBox();
            this.dgvManifiestos = new System.Windows.Forms.DataGridView();
            this.gbDetalleGenerado = new System.Windows.Forms.GroupBox();
            this.rtbDetalleGenerado = new System.Windows.Forms.RichTextBox();
            this.panelBotonesGenerados = new System.Windows.Forms.FlowLayoutPanel();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnCerrarGenerados = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabGenerar.SuspendLayout();
            this.layoutGenerar.SuspendLayout();
            this.panelDatos.SuspendLayout();
            this.gbContenedores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContenedores)).BeginInit();
            this.gbDetalle.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.tabGenerados.SuspendLayout();
            this.layoutGenerados.SuspendLayout();
            this.gbManifiestos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvManifiestos)).BeginInit();
            this.gbDetalleGenerado.SuspendLayout();
            this.panelBotonesGenerados.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabGenerar);
            this.tabControl.Controls.Add(this.tabGenerados);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1200, 700);
            this.tabControl.TabIndex = 0;
            // 
            // tabGenerar
            // 
            this.tabGenerar.Controls.Add(this.layoutGenerar);
            this.tabGenerar.Location = new System.Drawing.Point(4, 25);
            this.tabGenerar.Name = "tabGenerar";
            this.tabGenerar.Padding = new System.Windows.Forms.Padding(3);
            this.tabGenerar.Size = new System.Drawing.Size(1192, 671);
            this.tabGenerar.TabIndex = 0;
            this.tabGenerar.Text = "Generar manifiesto";
            this.tabGenerar.UseVisualStyleBackColor = true;
            // 
            // layoutGenerar
            // 
            this.layoutGenerar.ColumnCount = 1;
            this.layoutGenerar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutGenerar.Controls.Add(this.panelDatos, 0, 0);
            this.layoutGenerar.Controls.Add(this.gbContenedores, 0, 1);
            this.layoutGenerar.Controls.Add(this.gbDetalle, 0, 2);
            this.layoutGenerar.Controls.Add(this.panelBotones, 0, 3);
            this.layoutGenerar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutGenerar.Location = new System.Drawing.Point(3, 3);
            this.layoutGenerar.Name = "layoutGenerar";
            this.layoutGenerar.RowCount = 4;
            this.layoutGenerar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.layoutGenerar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.layoutGenerar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.layoutGenerar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.layoutGenerar.Size = new System.Drawing.Size(1186, 665);
            this.layoutGenerar.TabIndex = 0;
            // 
            // panelDatos
            // 
            this.panelDatos.Controls.Add(this.lblBuque);
            this.panelDatos.Controls.Add(this.cmbBuques);
            this.panelDatos.Controls.Add(this.lblAutoridadDestino);
            this.panelDatos.Controls.Add(this.txtAutoridadDestino);
            this.panelDatos.Controls.Add(this.lblFechaEmision);
            this.panelDatos.Controls.Add(this.dtpFechaEmision);
            this.panelDatos.Controls.Add(this.lblDestinoRapido);
            this.panelDatos.Controls.Add(this.cmbDestinos);
            this.panelDatos.Controls.Add(this.btnSeleccionarDestino);
            this.panelDatos.Controls.Add(this.lblCriterio);
            this.panelDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDatos.Location = new System.Drawing.Point(3, 3);
            this.panelDatos.Name = "panelDatos";
            this.panelDatos.Padding = new System.Windows.Forms.Padding(12);
            this.panelDatos.Size = new System.Drawing.Size(1180, 74);
            this.panelDatos.TabIndex = 0;
            // 
            // lblBuque
            // 
            this.lblBuque.AutoSize = true;
            this.lblBuque.Location = new System.Drawing.Point(15, 18);
            this.lblBuque.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblBuque.Name = "lblBuque";
            this.lblBuque.Size = new System.Drawing.Size(50, 22);
            this.lblBuque.TabIndex = 0;
            this.lblBuque.Text = "Buque:";
            // 
            // cmbBuques
            // 
            this.cmbBuques.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBuques.FormattingEnabled = true;
            this.cmbBuques.Location = new System.Drawing.Point(71, 15);
            this.cmbBuques.Name = "cmbBuques";
            this.cmbBuques.Size = new System.Drawing.Size(260, 24);
            this.cmbBuques.TabIndex = 1;
            this.cmbBuques.SelectedIndexChanged += new System.EventHandler(this.cmbBuques_SelectedIndexChanged);
            // 
            // lblAutoridadDestino
            // 
            this.lblAutoridadDestino.AutoSize = true;
            this.lblAutoridadDestino.Location = new System.Drawing.Point(354, 18);
            this.lblAutoridadDestino.Margin = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.lblAutoridadDestino.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblAutoridadDestino.Name = "lblAutoridadDestino";
            this.lblAutoridadDestino.Size = new System.Drawing.Size(129, 22);
            this.lblAutoridadDestino.TabIndex = 2;
            this.lblAutoridadDestino.Text = "Autoridad/destino:";
            // 
            // txtAutoridadDestino
            // 
            this.txtAutoridadDestino.Location = new System.Drawing.Point(489, 15);
            this.txtAutoridadDestino.Name = "txtAutoridadDestino";
            this.txtAutoridadDestino.Size = new System.Drawing.Size(300, 22);
            this.txtAutoridadDestino.TabIndex = 3;
            // 
            // lblFechaEmision
            // 
            this.lblFechaEmision.AutoSize = true;
            this.lblFechaEmision.Location = new System.Drawing.Point(812, 18);
            this.lblFechaEmision.Margin = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.lblFechaEmision.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblFechaEmision.Name = "lblFechaEmision";
            this.lblFechaEmision.Size = new System.Drawing.Size(103, 22);
            this.lblFechaEmision.TabIndex = 4;
            this.lblFechaEmision.Text = "Fecha emision:";
            // 
            // dtpFechaEmision
            // 
            this.dtpFechaEmision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEmision.Location = new System.Drawing.Point(921, 15);
            this.dtpFechaEmision.Name = "dtpFechaEmision";
            this.dtpFechaEmision.Size = new System.Drawing.Size(140, 22);
            this.dtpFechaEmision.TabIndex = 5;
            // 
            // lblDestinoRapido
            // 
            this.lblDestinoRapido.AutoSize = true;
            this.lblDestinoRapido.Location = new System.Drawing.Point(15, 47);
            this.lblDestinoRapido.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblDestinoRapido.Name = "lblDestinoRapido";
            this.lblDestinoRapido.Size = new System.Drawing.Size(113, 22);
            this.lblDestinoRapido.TabIndex = 6;
            this.lblDestinoRapido.Text = "Filtrar destino:";
            // 
            // cmbDestinos
            // 
            this.cmbDestinos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDestinos.FormattingEnabled = true;
            this.cmbDestinos.Location = new System.Drawing.Point(134, 44);
            this.cmbDestinos.Name = "cmbDestinos";
            this.cmbDestinos.Size = new System.Drawing.Size(260, 24);
            this.cmbDestinos.TabIndex = 7;
            this.cmbDestinos.SelectedIndexChanged += new System.EventHandler(this.cmbDestinos_SelectedIndexChanged);
            // 
            // btnSeleccionarDestino
            // 
            this.btnSeleccionarDestino.Location = new System.Drawing.Point(400, 43);
            this.btnSeleccionarDestino.Name = "btnSeleccionarDestino";
            this.btnSeleccionarDestino.Size = new System.Drawing.Size(150, 27);
            this.btnSeleccionarDestino.TabIndex = 8;
            this.btnSeleccionarDestino.Text = "Seleccionar destino";
            this.btnSeleccionarDestino.UseVisualStyleBackColor = true;
            this.btnSeleccionarDestino.Click += new System.EventHandler(this.btnSeleccionarDestino_Click);
            // 
            // lblCriterio
            // 
            this.lblCriterio.AutoSize = true;
            this.lblCriterio.Location = new System.Drawing.Point(573, 47);
            this.lblCriterio.Margin = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.lblCriterio.Name = "lblCriterio";
            this.lblCriterio.Size = new System.Drawing.Size(666, 16);
            this.lblCriterio.TabIndex = 6;
            this.lblCriterio.Text = "Criterio: el manifiesto agrupa contenedores con carga del mismo buque y del mismo" +
    " destino/autoridad.";
            // 
            // gbContenedores
            // 
            this.gbContenedores.Controls.Add(this.dgvContenedores);
            this.gbContenedores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbContenedores.Location = new System.Drawing.Point(3, 83);
            this.gbContenedores.Name = "gbContenedores";
            this.gbContenedores.Size = new System.Drawing.Size(1180, 277);
            this.gbContenedores.TabIndex = 1;
            this.gbContenedores.TabStop = false;
            this.gbContenedores.Text = "Contenedores asignados al buque";
            // 
            // dgvContenedores
            // 
            this.dgvContenedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContenedores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvContenedores.Location = new System.Drawing.Point(3, 18);
            this.dgvContenedores.Name = "dgvContenedores";
            this.dgvContenedores.RowHeadersWidth = 51;
            this.dgvContenedores.RowTemplate.Height = 24;
            this.dgvContenedores.Size = new System.Drawing.Size(1174, 256);
            this.dgvContenedores.TabIndex = 0;
            this.dgvContenedores.SelectionChanged += new System.EventHandler(this.dgvContenedores_SelectionChanged);
            // 
            // gbDetalle
            // 
            this.gbDetalle.Controls.Add(this.rtbDetalle);
            this.gbDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetalle.Location = new System.Drawing.Point(3, 366);
            this.gbDetalle.Name = "gbDetalle";
            this.gbDetalle.Size = new System.Drawing.Size(1180, 225);
            this.gbDetalle.TabIndex = 2;
            this.gbDetalle.TabStop = false;
            this.gbDetalle.Text = "Detalle del manifiesto";
            // 
            // rtbDetalle
            // 
            this.rtbDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDetalle.Location = new System.Drawing.Point(3, 18);
            this.rtbDetalle.Name = "rtbDetalle";
            this.rtbDetalle.ReadOnly = true;
            this.rtbDetalle.Size = new System.Drawing.Size(1174, 204);
            this.rtbDetalle.TabIndex = 0;
            this.rtbDetalle.Text = "";
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnGenerar);
            this.panelBotones.Controls.Add(this.btnActualizar);
            this.panelBotones.Controls.Add(this.btnCerrar);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBotones.Location = new System.Drawing.Point(3, 597);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Padding = new System.Windows.Forms.Padding(12);
            this.panelBotones.Size = new System.Drawing.Size(1180, 65);
            this.panelBotones.TabIndex = 3;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(15, 15);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(170, 34);
            this.btnGenerar.TabIndex = 0;
            this.btnGenerar.Text = "Generar manifiesto";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(191, 15);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(120, 34);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(317, 15);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 34);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // tabGenerados
            // 
            this.tabGenerados.Controls.Add(this.layoutGenerados);
            this.tabGenerados.Location = new System.Drawing.Point(4, 25);
            this.tabGenerados.Name = "tabGenerados";
            this.tabGenerados.Padding = new System.Windows.Forms.Padding(3);
            this.tabGenerados.Size = new System.Drawing.Size(1192, 671);
            this.tabGenerados.TabIndex = 1;
            this.tabGenerados.Text = "Manifiestos generados";
            this.tabGenerados.UseVisualStyleBackColor = true;
            // 
            // layoutGenerados
            // 
            this.layoutGenerados.ColumnCount = 1;
            this.layoutGenerados.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutGenerados.Controls.Add(this.gbManifiestos, 0, 0);
            this.layoutGenerados.Controls.Add(this.gbDetalleGenerado, 0, 1);
            this.layoutGenerados.Controls.Add(this.panelBotonesGenerados, 0, 2);
            this.layoutGenerados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutGenerados.Location = new System.Drawing.Point(3, 3);
            this.layoutGenerados.Name = "layoutGenerados";
            this.layoutGenerados.RowCount = 3;
            this.layoutGenerados.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layoutGenerados.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.layoutGenerados.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.layoutGenerados.Size = new System.Drawing.Size(1186, 665);
            this.layoutGenerados.TabIndex = 0;
            // 
            // gbManifiestos
            // 
            this.gbManifiestos.Controls.Add(this.dgvManifiestos);
            this.gbManifiestos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbManifiestos.Location = new System.Drawing.Point(3, 3);
            this.gbManifiestos.Name = "gbManifiestos";
            this.gbManifiestos.Size = new System.Drawing.Size(1180, 351);
            this.gbManifiestos.TabIndex = 0;
            this.gbManifiestos.TabStop = false;
            this.gbManifiestos.Text = "Manifiestos generados";
            // 
            // dgvManifiestos
            // 
            this.dgvManifiestos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvManifiestos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvManifiestos.Location = new System.Drawing.Point(3, 18);
            this.dgvManifiestos.Name = "dgvManifiestos";
            this.dgvManifiestos.RowHeadersWidth = 51;
            this.dgvManifiestos.RowTemplate.Height = 24;
            this.dgvManifiestos.Size = new System.Drawing.Size(1174, 330);
            this.dgvManifiestos.TabIndex = 0;
            this.dgvManifiestos.SelectionChanged += new System.EventHandler(this.dgvManifiestos_SelectionChanged);
            // 
            // gbDetalleGenerado
            // 
            this.gbDetalleGenerado.Controls.Add(this.rtbDetalleGenerado);
            this.gbDetalleGenerado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetalleGenerado.Location = new System.Drawing.Point(3, 360);
            this.gbDetalleGenerado.Name = "gbDetalleGenerado";
            this.gbDetalleGenerado.Size = new System.Drawing.Size(1180, 232);
            this.gbDetalleGenerado.TabIndex = 1;
            this.gbDetalleGenerado.TabStop = false;
            this.gbDetalleGenerado.Text = "Detalle del documento seleccionado";
            // 
            // rtbDetalleGenerado
            // 
            this.rtbDetalleGenerado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDetalleGenerado.Location = new System.Drawing.Point(3, 18);
            this.rtbDetalleGenerado.Name = "rtbDetalleGenerado";
            this.rtbDetalleGenerado.ReadOnly = true;
            this.rtbDetalleGenerado.Size = new System.Drawing.Size(1174, 211);
            this.rtbDetalleGenerado.TabIndex = 0;
            this.rtbDetalleGenerado.Text = "";
            // 
            // panelBotonesGenerados
            // 
            this.panelBotonesGenerados.Controls.Add(this.btnImprimir);
            this.panelBotonesGenerados.Controls.Add(this.btnCerrarGenerados);
            this.panelBotonesGenerados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBotonesGenerados.Location = new System.Drawing.Point(3, 598);
            this.panelBotonesGenerados.Name = "panelBotonesGenerados";
            this.panelBotonesGenerados.Padding = new System.Windows.Forms.Padding(12);
            this.panelBotonesGenerados.Size = new System.Drawing.Size(1180, 64);
            this.panelBotonesGenerados.TabIndex = 2;
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
            // btnCerrarGenerados
            // 
            this.btnCerrarGenerados.Location = new System.Drawing.Point(161, 15);
            this.btnCerrarGenerados.Name = "btnCerrarGenerados";
            this.btnCerrarGenerados.Size = new System.Drawing.Size(120, 34);
            this.btnCerrarGenerados.TabIndex = 1;
            this.btnCerrarGenerados.Text = "Cerrar";
            this.btnCerrarGenerados.UseVisualStyleBackColor = true;
            this.btnCerrarGenerados.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FrmManifiesto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tabControl);
            this.Name = "FrmManifiesto";
            this.Tag = "FrmManifiesto";
            this.Text = "Manifiesto de carga";
            this.Load += new System.EventHandler(this.FrmManifiesto_Load);
            this.tabControl.ResumeLayout(false);
            this.tabGenerar.ResumeLayout(false);
            this.layoutGenerar.ResumeLayout(false);
            this.panelDatos.ResumeLayout(false);
            this.panelDatos.PerformLayout();
            this.gbContenedores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContenedores)).EndInit();
            this.gbDetalle.ResumeLayout(false);
            this.panelBotones.ResumeLayout(false);
            this.tabGenerados.ResumeLayout(false);
            this.layoutGenerados.ResumeLayout(false);
            this.gbManifiestos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvManifiestos)).EndInit();
            this.gbDetalleGenerado.ResumeLayout(false);
            this.panelBotonesGenerados.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGenerar;
        private System.Windows.Forms.TableLayoutPanel layoutGenerar;
        private System.Windows.Forms.FlowLayoutPanel panelDatos;
        private System.Windows.Forms.Label lblBuque;
        private System.Windows.Forms.ComboBox cmbBuques;
        private System.Windows.Forms.Label lblAutoridadDestino;
        private System.Windows.Forms.TextBox txtAutoridadDestino;
        private System.Windows.Forms.Label lblFechaEmision;
        private System.Windows.Forms.DateTimePicker dtpFechaEmision;
        private System.Windows.Forms.Label lblDestinoRapido;
        private System.Windows.Forms.ComboBox cmbDestinos;
        private System.Windows.Forms.Button btnSeleccionarDestino;
        private System.Windows.Forms.Label lblCriterio;
        private System.Windows.Forms.GroupBox gbContenedores;
        private System.Windows.Forms.DataGridView dgvContenedores;
        private System.Windows.Forms.GroupBox gbDetalle;
        private System.Windows.Forms.RichTextBox rtbDetalle;
        private System.Windows.Forms.FlowLayoutPanel panelBotones;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TabPage tabGenerados;
        private System.Windows.Forms.TableLayoutPanel layoutGenerados;
        private System.Windows.Forms.GroupBox gbManifiestos;
        private System.Windows.Forms.DataGridView dgvManifiestos;
        private System.Windows.Forms.GroupBox gbDetalleGenerado;
        private System.Windows.Forms.RichTextBox rtbDetalleGenerado;
        private System.Windows.Forms.FlowLayoutPanel panelBotonesGenerados;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnCerrarGenerados;
    }
}
