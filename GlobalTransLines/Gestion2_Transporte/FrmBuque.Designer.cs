namespace GlobalTransLines.UI.Gestion2_Transporte
{
    partial class FrmBuque
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
            this.grdBuques = new System.Windows.Forms.DataGridView();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGuardarItinerario = new System.Windows.Forms.Button();
            this.btnEliminarPuerto = new System.Windows.Forms.Button();
            this.btnAgregarPuerto = new System.Windows.Forms.Button();
            this.txtPuerto = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lstItinerarios = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDisponibilidad = new System.Windows.Forms.TextBox();
            this.numCapacidadPeso = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numCapacidadTEU = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodigoIMO = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblNumero = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdBuques)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidadPeso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidadTEU)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdBuques
            // 
            this.grdBuques.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBuques.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBuques.Location = new System.Drawing.Point(0, 215);
            this.grdBuques.Name = "grdBuques";
            this.grdBuques.RowHeadersWidth = 51;
            this.grdBuques.RowTemplate.Height = 24;
            this.grdBuques.Size = new System.Drawing.Size(1208, 169);
            this.grdBuques.TabIndex = 24;
            this.grdBuques.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdBuques_CellClick);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(503, 22);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(150, 28);
            this.btnEliminar.TabIndex = 19;
            this.btnEliminar.Text = "✖️ Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(324, 22);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(150, 28);
            this.btnModificar.TabIndex = 18;
            this.btnModificar.Text = "✏️ Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(135, 22);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(150, 28);
            this.btnGuardar.TabIndex = 17;
            this.btnGuardar.Text = "📂 Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(909, 22);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(150, 28);
            this.btnBuscar.TabIndex = 20;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(1066, 25);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 28);
            this.btnCerrar.TabIndex = 27;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(678, 25);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(208, 22);
            this.txtBuscar.TabIndex = 21;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Estado";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Numero";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNumero);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtDisponibilidad);
            this.groupBox1.Controls.Add(this.numCapacidadPeso);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numCapacidadTEU);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCodigoIMO);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.cmbEstado);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblNumero);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1208, 215);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(117, 18);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(220, 22);
            this.txtNumero.TabIndex = 38;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnGuardarItinerario);
            this.groupBox3.Controls.Add(this.btnEliminarPuerto);
            this.groupBox3.Controls.Add(this.btnAgregarPuerto);
            this.groupBox3.Controls.Add(this.txtPuerto);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.lstItinerarios);
            this.groupBox3.Location = new System.Drawing.Point(735, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(473, 215);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Itinerario Buque";
            // 
            // btnGuardarItinerario
            // 
            this.btnGuardarItinerario.Location = new System.Drawing.Point(174, 171);
            this.btnGuardarItinerario.Margin = new System.Windows.Forms.Padding(4);
            this.btnGuardarItinerario.Name = "btnGuardarItinerario";
            this.btnGuardarItinerario.Size = new System.Drawing.Size(150, 28);
            this.btnGuardarItinerario.TabIndex = 22;
            this.btnGuardarItinerario.Text = "📂 Guardar Itinerario";
            this.btnGuardarItinerario.UseVisualStyleBackColor = true;
            this.btnGuardarItinerario.Click += new System.EventHandler(this.btnGuardarItinerario_Click);
            // 
            // btnEliminarPuerto
            // 
            this.btnEliminarPuerto.Location = new System.Drawing.Point(17, 171);
            this.btnEliminarPuerto.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminarPuerto.Name = "btnEliminarPuerto";
            this.btnEliminarPuerto.Size = new System.Drawing.Size(134, 28);
            this.btnEliminarPuerto.TabIndex = 39;
            this.btnEliminarPuerto.Text = "➖  Eliminar puerto";
            this.btnEliminarPuerto.UseVisualStyleBackColor = true;
            this.btnEliminarPuerto.Click += new System.EventHandler(this.btnEliminarItinerario_Click);
            // 
            // btnAgregarPuerto
            // 
            this.btnAgregarPuerto.Location = new System.Drawing.Point(201, 27);
            this.btnAgregarPuerto.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregarPuerto.Name = "btnAgregarPuerto";
            this.btnAgregarPuerto.Size = new System.Drawing.Size(123, 28);
            this.btnAgregarPuerto.TabIndex = 22;
            this.btnAgregarPuerto.Text = "➕ Agregar";
            this.btnAgregarPuerto.UseVisualStyleBackColor = true;
            this.btnAgregarPuerto.Click += new System.EventHandler(this.btnAgregarItinerario_Click);
            // 
            // txtPuerto
            // 
            this.txtPuerto.Location = new System.Drawing.Point(70, 27);
            this.txtPuerto.Name = "txtPuerto";
            this.txtPuerto.Size = new System.Drawing.Size(119, 22);
            this.txtPuerto.TabIndex = 38;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 16);
            this.label8.TabIndex = 38;
            this.label8.Text = "Puerto";
            // 
            // lstItinerarios
            // 
            this.lstItinerarios.FormattingEnabled = true;
            this.lstItinerarios.ItemHeight = 16;
            this.lstItinerarios.Location = new System.Drawing.Point(17, 68);
            this.lstItinerarios.Name = "lstItinerarios";
            this.lstItinerarios.Size = new System.Drawing.Size(307, 68);
            this.lstItinerarios.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 16);
            this.label7.TabIndex = 36;
            this.label7.Text = "Disponibilidad ";
            // 
            // txtDisponibilidad
            // 
            this.txtDisponibilidad.Location = new System.Drawing.Point(117, 130);
            this.txtDisponibilidad.Name = "txtDisponibilidad";
            this.txtDisponibilidad.Size = new System.Drawing.Size(220, 22);
            this.txtDisponibilidad.TabIndex = 35;
            // 
            // numCapacidadPeso
            // 
            this.numCapacidadPeso.Location = new System.Drawing.Point(495, 89);
            this.numCapacidadPeso.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numCapacidadPeso.Name = "numCapacidadPeso";
            this.numCapacidadPeso.Size = new System.Drawing.Size(220, 22);
            this.numCapacidadPeso.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(360, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 16);
            this.label6.TabIndex = 33;
            this.label6.Text = "Capacidad TEU";
            // 
            // numCapacidadTEU
            // 
            this.numCapacidadTEU.Location = new System.Drawing.Point(495, 55);
            this.numCapacidadTEU.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numCapacidadTEU.Name = "numCapacidadTEU";
            this.numCapacidadTEU.Size = new System.Drawing.Size(220, 22);
            this.numCapacidadTEU.TabIndex = 32;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(360, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 32);
            this.label5.TabIndex = 31;
            this.label5.Text = "Capacidad Peso \r\n(toneladas)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 30;
            this.label3.Text = "CodigoIMO";
            // 
            // txtCodigoIMO
            // 
            this.txtCodigoIMO.Location = new System.Drawing.Point(117, 91);
            this.txtCodigoIMO.Name = "txtCodigoIMO";
            this.txtCodigoIMO.Size = new System.Drawing.Size(220, 22);
            this.txtCodigoIMO.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(117, 53);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(220, 22);
            this.txtNombre.TabIndex = 24;
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(117, 171);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(220, 24);
            this.cmbEstado.TabIndex = 11;
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(74, 21);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(0, 16);
            this.lblNumero.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnEliminar);
            this.groupBox2.Controls.Add(this.btnModificar);
            this.groupBox2.Controls.Add(this.btnGuardar);
            this.groupBox2.Controls.Add(this.btnBuscar);
            this.groupBox2.Controls.Add(this.btnCerrar);
            this.groupBox2.Controls.Add(this.txtBuscar);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 384);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1208, 69);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // FrmBuque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 453);
            this.Controls.Add(this.grdBuques);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmBuque";
            this.Tag = "FrmBuque";
            this.Text = "FrmBuque";
            this.Load += new System.EventHandler(this.FrmBuque_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdBuques)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidadPeso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCapacidadTEU)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdBuques;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodigoIMO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDisponibilidad;
        private System.Windows.Forms.NumericUpDown numCapacidadPeso;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numCapacidadTEU;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox lstItinerarios;
        private System.Windows.Forms.Button btnEliminarPuerto;
        private System.Windows.Forms.Button btnAgregarPuerto;
        private System.Windows.Forms.TextBox txtPuerto;
        private System.Windows.Forms.Button btnGuardarItinerario;
        private System.Windows.Forms.TextBox txtNumero;
    }
}
