namespace GlobalTransLines.UI.Gestion1_Booking
{
    partial class FrmCarga
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
            this.grpDatosCarga = new System.Windows.Forms.GroupBox();
            this.cmbPuertoEmbarque = new System.Windows.Forms.ComboBox();
            this.lblPuertoEmbarque = new System.Windows.Forms.Label();
            this.cmbTipoContenedor = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpEntregaEstimada = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTipoCarga = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numVolumen = new System.Windows.Forms.NumericUpDown();
            this.numPeso = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.grdCargas = new System.Windows.Forms.DataGridView();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucOrigen = new GlobalTransLines.UI.Controles.UCDirecciones();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ucDestino = new GlobalTransLines.UI.Controles.UCDirecciones();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.grpDatosCarga.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVolumen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCargas)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDatosCarga
            // 
            this.grpDatosCarga.Controls.Add(this.cmbPuertoEmbarque);
            this.grpDatosCarga.Controls.Add(this.lblPuertoEmbarque);
            this.grpDatosCarga.Controls.Add(this.cmbTipoContenedor);
            this.grpDatosCarga.Controls.Add(this.label8);
            this.grpDatosCarga.Controls.Add(this.label7);
            this.grpDatosCarga.Controls.Add(this.label6);
            this.grpDatosCarga.Controls.Add(this.label5);
            this.grpDatosCarga.Controls.Add(this.dtpEntregaEstimada);
            this.grpDatosCarga.Controls.Add(this.label4);
            this.grpDatosCarga.Controls.Add(this.cmbTipoCarga);
            this.grpDatosCarga.Controls.Add(this.label3);
            this.grpDatosCarga.Controls.Add(this.label2);
            this.grpDatosCarga.Controls.Add(this.numVolumen);
            this.grpDatosCarga.Controls.Add(this.numPeso);
            this.grpDatosCarga.Controls.Add(this.label1);
            this.grpDatosCarga.Controls.Add(this.txtDescripcion);
            this.grpDatosCarga.Location = new System.Drawing.Point(0, 0);
            this.grpDatosCarga.Name = "grpDatosCarga";
            this.grpDatosCarga.Size = new System.Drawing.Size(668, 378);
            this.grpDatosCarga.TabIndex = 0;
            this.grpDatosCarga.TabStop = false;
            this.grpDatosCarga.Text = "Datos de la Carga";
            // 
            // cmbPuertoEmbarque
            // 
            this.cmbPuertoEmbarque.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPuertoEmbarque.FormattingEnabled = true;
            this.cmbPuertoEmbarque.Location = new System.Drawing.Point(148, 227);
            this.cmbPuertoEmbarque.Name = "cmbPuertoEmbarque";
            this.cmbPuertoEmbarque.Size = new System.Drawing.Size(495, 24);
            this.cmbPuertoEmbarque.TabIndex = 18;
            // 
            // lblPuertoEmbarque
            // 
            this.lblPuertoEmbarque.AutoSize = true;
            this.lblPuertoEmbarque.Location = new System.Drawing.Point(9, 235);
            this.lblPuertoEmbarque.Name = "lblPuertoEmbarque";
            this.lblPuertoEmbarque.Size = new System.Drawing.Size(133, 16);
            this.lblPuertoEmbarque.TabIndex = 17;
            this.lblPuertoEmbarque.Text = "Puerto de embarque";
            // 
            // cmbTipoContenedor
            // 
            this.cmbTipoContenedor.FormattingEnabled = true;
            this.cmbTipoContenedor.Location = new System.Drawing.Point(120, 267);
            this.cmbTipoContenedor.Name = "cmbTipoContenedor";
            this.cmbTipoContenedor.Size = new System.Drawing.Size(523, 24);
            this.cmbTipoContenedor.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 275);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 16);
            this.label8.TabIndex = 15;
            this.label8.Text = "TipoContenedor";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(258, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "m³";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(258, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Kg";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 326);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Entrega Estimada";
            // 
            // dtpEntregaEstimada
            // 
            this.dtpEntregaEstimada.Location = new System.Drawing.Point(148, 321);
            this.dtpEntregaEstimada.Name = "dtpEntregaEstimada";
            this.dtpEntregaEstimada.Size = new System.Drawing.Size(200, 22);
            this.dtpEntregaEstimada.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "TipoCarga";
            // 
            // cmbTipoCarga
            // 
            this.cmbTipoCarga.FormattingEnabled = true;
            this.cmbTipoCarga.Location = new System.Drawing.Point(120, 187);
            this.cmbTipoCarga.Name = "cmbTipoCarga";
            this.cmbTipoCarga.Size = new System.Drawing.Size(523, 24);
            this.cmbTipoCarga.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Volumen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Peso";
            // 
            // numVolumen
            // 
            this.numVolumen.DecimalPlaces = 3;
            this.numVolumen.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numVolumen.Location = new System.Drawing.Point(120, 129);
            this.numVolumen.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numVolumen.Name = "numVolumen";
            this.numVolumen.Size = new System.Drawing.Size(120, 22);
            this.numVolumen.TabIndex = 3;
            // 
            // numPeso
            // 
            this.numPeso.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPeso.Location = new System.Drawing.Point(120, 91);
            this.numPeso.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numPeso.Name = "numPeso";
            this.numPeso.Size = new System.Drawing.Size(120, 22);
            this.numPeso.TabIndex = 2;
            this.numPeso.ThousandsSeparator = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Descripcion";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(120, 51);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(243, 22);
            this.txtDescripcion.TabIndex = 0;
            // 
            // grdCargas
            // 
            this.grdCargas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCargas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCargas.Location = new System.Drawing.Point(0, 387);
            this.grdCargas.Name = "grdCargas";
            this.grdCargas.RowHeadersWidth = 51;
            this.grdCargas.RowTemplate.Height = 24;
            this.grdCargas.Size = new System.Drawing.Size(1527, 227);
            this.grdCargas.TabIndex = 3;
            this.grdCargas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdCargas_CellClick);
            this.grdCargas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdCargas_CellDoubleClick);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(15, 30);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(208, 22);
            this.txtBuscar.TabIndex = 34;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(235, 24);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(150, 28);
            this.btnBuscar.TabIndex = 33;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(954, 24);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(150, 28);
            this.btnEliminar.TabIndex = 31;
            this.btnEliminar.Text = "✖️ Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(697, 24);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(150, 28);
            this.btnModificar.TabIndex = 30;
            this.btnModificar.Text = "✏️ Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(437, 24);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(150, 28);
            this.btnGuardar.TabIndex = 35;
            this.btnGuardar.Text = "📂 Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucOrigen);
            this.groupBox2.Location = new System.Drawing.Point(674, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(428, 371);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Origen";
            // 
            // ucOrigen
            // 
            this.ucOrigen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOrigen.Location = new System.Drawing.Point(3, 18);
            this.ucOrigen.Name = "ucOrigen";
            this.ucOrigen.Size = new System.Drawing.Size(422, 350);
            this.ucOrigen.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ucDestino);
            this.groupBox3.Location = new System.Drawing.Point(1108, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(419, 371);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Destino";
            // 
            // ucDestino
            // 
            this.ucDestino.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDestino.Location = new System.Drawing.Point(3, 18);
            this.ucDestino.Name = "ucDestino";
            this.ucDestino.Size = new System.Drawing.Size(413, 350);
            this.ucDestino.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnBuscar);
            this.groupBox4.Controls.Add(this.txtBuscar);
            this.groupBox4.Controls.Add(this.btnModificar);
            this.groupBox4.Controls.Add(this.btnEliminar);
            this.groupBox4.Controls.Add(this.btnGuardar);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(0, 549);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1527, 65);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.grpDatosCarga);
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1527, 387);
            this.groupBox5.TabIndex = 37;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "";
            // 
            // FrmCarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1527, 614);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.grdCargas);
            this.Controls.Add(this.groupBox5);
            this.Name = "FrmCarga";
            this.Tag = "FrmCarga";
            this.Text = "Gestión de Cargas";
            this.Load += new System.EventHandler(this.FrmCarga_Load);
            this.grpDatosCarga.ResumeLayout(false);
            this.grpDatosCarga.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVolumen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCargas)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDatosCarga;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numVolumen;
        private System.Windows.Forms.NumericUpDown numPeso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grdCargas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbTipoCarga;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DateTimePicker dtpEntregaEstimada;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controles.UCDirecciones ucOrigen;
        private System.Windows.Forms.GroupBox groupBox3;
        private Controles.UCDirecciones ucDestino;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbTipoContenedor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbPuertoEmbarque;
        private System.Windows.Forms.Label lblPuertoEmbarque;
    }
}
