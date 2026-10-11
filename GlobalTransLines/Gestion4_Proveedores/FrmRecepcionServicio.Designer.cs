namespace GlobalTransLines.UI.Gestion4_Proveedores
{
    partial class FrmRecepcionServicio
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
            this.gbOrdenes = new System.Windows.Forms.GroupBox();
            this.dgvOrdenesEmitidas = new System.Windows.Forms.DataGridView();
            this.gbRecepcion = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnConfirmarRecepcion = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.dtpFechaRecepcion = new System.Windows.Forms.DateTimePicker();
            this.lblFecha = new System.Windows.Forms.Label();
            this.txtDetalleOrden = new System.Windows.Forms.TextBox();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.gbRecepciones = new System.Windows.Forms.GroupBox();
            this.dgvRecepciones = new System.Windows.Forms.DataGridView();
            this.gbOrdenes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesEmitidas)).BeginInit();
            this.gbRecepcion.SuspendLayout();
            this.gbRecepciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecepciones)).BeginInit();
            this.SuspendLayout();
            // 
            // gbOrdenes
            // 
            this.gbOrdenes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOrdenes.Controls.Add(this.dgvOrdenesEmitidas);
            this.gbOrdenes.Location = new System.Drawing.Point(12, 12);
            this.gbOrdenes.Name = "gbOrdenes";
            this.gbOrdenes.Size = new System.Drawing.Size(1172, 220);
            this.gbOrdenes.TabIndex = 0;
            this.gbOrdenes.TabStop = false;
            this.gbOrdenes.Text = "Órdenes de compra emitidas";
            // 
            // dgvOrdenesEmitidas
            // 
            this.dgvOrdenesEmitidas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrdenesEmitidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdenesEmitidas.Location = new System.Drawing.Point(12, 24);
            this.dgvOrdenesEmitidas.Name = "dgvOrdenesEmitidas";
            this.dgvOrdenesEmitidas.RowHeadersWidth = 51;
            this.dgvOrdenesEmitidas.RowTemplate.Height = 24;
            this.dgvOrdenesEmitidas.Size = new System.Drawing.Size(1148, 184);
            this.dgvOrdenesEmitidas.TabIndex = 0;
            // 
            // gbRecepcion
            // 
            this.gbRecepcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRecepcion.Controls.Add(this.btnLimpiar);
            this.gbRecepcion.Controls.Add(this.btnActualizar);
            this.gbRecepcion.Controls.Add(this.btnConfirmarRecepcion);
            this.gbRecepcion.Controls.Add(this.btnCerrar);
            this.gbRecepcion.Controls.Add(this.txtObservaciones);
            this.gbRecepcion.Controls.Add(this.lblObservaciones);
            this.gbRecepcion.Controls.Add(this.dtpFechaRecepcion);
            this.gbRecepcion.Controls.Add(this.lblFecha);
            this.gbRecepcion.Controls.Add(this.txtDetalleOrden);
            this.gbRecepcion.Controls.Add(this.lblDetalle);
            this.gbRecepcion.Location = new System.Drawing.Point(12, 238);
            this.gbRecepcion.Name = "gbRecepcion";
            this.gbRecepcion.Size = new System.Drawing.Size(1172, 230);
            this.gbRecepcion.TabIndex = 1;
            this.gbRecepcion.TabStop = false;
            this.gbRecepcion.Text = "Confirmación de recepción";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiar.Location = new System.Drawing.Point(1010, 169);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(150, 34);
            this.btnLimpiar.TabIndex = 8;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualizar.Location = new System.Drawing.Point(854, 169);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(150, 34);
            this.btnActualizar.TabIndex = 7;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnConfirmarRecepcion
            // 
            this.btnConfirmarRecepcion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmarRecepcion.Location = new System.Drawing.Point(635, 169);
            this.btnConfirmarRecepcion.Name = "btnConfirmarRecepcion";
            this.btnConfirmarRecepcion.Size = new System.Drawing.Size(213, 34);
            this.btnConfirmarRecepcion.TabIndex = 6;
            this.btnConfirmarRecepcion.Text = "Confirmar recepción";
            this.btnConfirmarRecepcion.UseVisualStyleBackColor = true;
            this.btnConfirmarRecepcion.Click += new System.EventHandler(this.btnConfirmarRecepcion_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Location = new System.Drawing.Point(479, 169);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(150, 34);
            this.btnCerrar.TabIndex = 9;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservaciones.Location = new System.Drawing.Point(635, 74);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(525, 76);
            this.txtObservaciones.TabIndex = 5;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Location = new System.Drawing.Point(632, 52);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(103, 16);
            this.lblObservaciones.TabIndex = 4;
            this.lblObservaciones.Text = "Observaciones";
            // 
            // dtpFechaRecepcion
            // 
            this.dtpFechaRecepcion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaRecepcion.Location = new System.Drawing.Point(894, 21);
            this.dtpFechaRecepcion.Name = "dtpFechaRecepcion";
            this.dtpFechaRecepcion.Size = new System.Drawing.Size(266, 22);
            this.dtpFechaRecepcion.TabIndex = 3;
            // 
            // lblFecha
            // 
            this.lblFecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(773, 24);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(115, 16);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha recepción";
            // 
            // txtDetalleOrden
            // 
            this.txtDetalleOrden.Location = new System.Drawing.Point(12, 46);
            this.txtDetalleOrden.Multiline = true;
            this.txtDetalleOrden.Name = "txtDetalleOrden";
            this.txtDetalleOrden.Size = new System.Drawing.Size(589, 157);
            this.txtDetalleOrden.TabIndex = 1;
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Location = new System.Drawing.Point(9, 24);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(111, 16);
            this.lblDetalle.TabIndex = 0;
            this.lblDetalle.Text = "Orden seleccionada";
            // 
            // gbRecepciones
            // 
            this.gbRecepciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRecepciones.Controls.Add(this.dgvRecepciones);
            this.gbRecepciones.Location = new System.Drawing.Point(12, 474);
            this.gbRecepciones.Name = "gbRecepciones";
            this.gbRecepciones.Size = new System.Drawing.Size(1172, 214);
            this.gbRecepciones.TabIndex = 2;
            this.gbRecepciones.TabStop = false;
            this.gbRecepciones.Text = "Recepciones registradas";
            // 
            // dgvRecepciones
            // 
            this.dgvRecepciones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecepciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecepciones.Location = new System.Drawing.Point(12, 24);
            this.dgvRecepciones.Name = "dgvRecepciones";
            this.dgvRecepciones.RowHeadersWidth = 51;
            this.dgvRecepciones.RowTemplate.Height = 24;
            this.dgvRecepciones.Size = new System.Drawing.Size(1148, 178);
            this.dgvRecepciones.TabIndex = 0;
            // 
            // FrmRecepcionServicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 700);
            this.Controls.Add(this.gbRecepciones);
            this.Controls.Add(this.gbRecepcion);
            this.Controls.Add(this.gbOrdenes);
            this.Name = "FrmRecepcionServicio";
            this.Tag = "FrmRecepcionServicio";
            this.Text = "Recepción de Servicios";
            this.Load += new System.EventHandler(this.FrmRecepcionServicio_Load);
            this.gbOrdenes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenesEmitidas)).EndInit();
            this.gbRecepcion.ResumeLayout(false);
            this.gbRecepcion.PerformLayout();
            this.gbRecepciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecepciones)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOrdenes;
        private System.Windows.Forms.DataGridView dgvOrdenesEmitidas;
        private System.Windows.Forms.GroupBox gbRecepcion;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnConfirmarRecepcion;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.DateTimePicker dtpFechaRecepcion;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox txtDetalleOrden;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.GroupBox gbRecepciones;
        private System.Windows.Forms.DataGridView dgvRecepciones;
    }
}
