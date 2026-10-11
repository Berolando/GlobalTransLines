namespace GlobalTransLines.UI.Gestion1_Booking
{
    partial class FrmCotizacion
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
            this.btnCliente = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpVencimiento = new System.Windows.Forms.DateTimePicker();
            this.btnServicio = new System.Windows.Forms.Button();
            this.btnCarga = new System.Windows.Forms.Button();
            this.grpTotales = new System.Windows.Forms.GroupBox();
            this.lblCalc = new System.Windows.Forms.Label();
            this.btnCalcularTotal = new System.Windows.Forms.Button();
            this.lblMonto = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGenerarBooking = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.rtbPreview = new System.Windows.Forms.RichTextBox();
            this.lblFechaE = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.btnAceptarCliente = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCompleja = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.grpTotales.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCliente
            // 
            this.btnCliente.Location = new System.Drawing.Point(34, 34);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(150, 28);
            this.btnCliente.TabIndex = 10;
            this.btnCliente.Text = "➕ Agregar Cliente";
            this.btnCliente.UseVisualStyleBackColor = true;
            this.btnCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 460);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 16);
            this.label7.TabIndex = 18;
            this.label7.Text = " Vencimiento";
            // 
            // dtpVencimiento
            // 
            this.dtpVencimiento.Location = new System.Drawing.Point(97, 454);
            this.dtpVencimiento.Name = "dtpVencimiento";
            this.dtpVencimiento.Size = new System.Drawing.Size(200, 22);
            this.dtpVencimiento.TabIndex = 16;
            // 
            // btnServicio
            // 
            this.btnServicio.Location = new System.Drawing.Point(34, 148);
            this.btnServicio.Name = "btnServicio";
            this.btnServicio.Size = new System.Drawing.Size(185, 28);
            this.btnServicio.TabIndex = 14;
            this.btnServicio.Text = "🔎 Seleccionar Servicios";
            this.btnServicio.UseVisualStyleBackColor = true;
            this.btnServicio.Click += new System.EventHandler(this.btnAgregarServicio_Click);
            // 
            // btnCarga
            // 
            this.btnCarga.Location = new System.Drawing.Point(34, 93);
            this.btnCarga.Name = "btnCarga";
            this.btnCarga.Size = new System.Drawing.Size(150, 28);
            this.btnCarga.TabIndex = 12;
            this.btnCarga.Text = "➕ Agregar Carga";
            this.btnCarga.UseVisualStyleBackColor = true;
            this.btnCarga.Click += new System.EventHandler(this.btnAgregarCarga_Click);
            // 
            // grpTotales
            // 
            this.grpTotales.Controls.Add(this.lblCalc);
            this.grpTotales.Controls.Add(this.btnCalcularTotal);
            this.grpTotales.Controls.Add(this.lblMonto);
            this.grpTotales.Controls.Add(this.label4);
            this.grpTotales.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTotales.Location = new System.Drawing.Point(7, 265);
            this.grpTotales.Name = "grpTotales";
            this.grpTotales.Size = new System.Drawing.Size(412, 116);
            this.grpTotales.TabIndex = 2;
            this.grpTotales.TabStop = false;
            this.grpTotales.Text = "Totales";
            // 
            // lblCalc
            // 
            this.lblCalc.AutoSize = true;
            this.lblCalc.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalc.Location = new System.Drawing.Point(215, 74);
            this.lblCalc.Name = "lblCalc";
            this.lblCalc.Size = new System.Drawing.Size(0, 23);
            this.lblCalc.TabIndex = 17;
            // 
            // btnCalcularTotal
            // 
            this.btnCalcularTotal.Location = new System.Drawing.Point(31, 69);
            this.btnCalcularTotal.Name = "btnCalcularTotal";
            this.btnCalcularTotal.Size = new System.Drawing.Size(150, 28);
            this.btnCalcularTotal.TabIndex = 15;
            this.btnCalcularTotal.Text = "Calcular Total";
            this.btnCalcularTotal.UseVisualStyleBackColor = true;
            this.btnCalcularTotal.Click += new System.EventHandler(this.btnCalcularTotal_Click);
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonto.Location = new System.Drawing.Point(215, 32);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(0, 23);
            this.lblMonto.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 16);
            this.label4.TabIndex = 15;
            this.label4.Text = "Monto total estimado";
            // 
            // btnGenerarBooking
            // 
            this.btnGenerarBooking.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarBooking.Location = new System.Drawing.Point(174, 542);
            this.btnGenerarBooking.Name = "btnGenerarBooking";
            this.btnGenerarBooking.Size = new System.Drawing.Size(150, 28);
            this.btnGenerarBooking.TabIndex = 17;
            this.btnGenerarBooking.Text = "Generar Booking";
            this.btnGenerarBooking.UseVisualStyleBackColor = true;
            this.btnGenerarBooking.Click += new System.EventHandler(this.btnGenerarBooking_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Numero";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(87, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(113, 22);
            this.textBox1.TabIndex = 12;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(29, 542);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(150, 28);
            this.btnGuardar.TabIndex = 22;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(319, 542);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(105, 28);
            this.btnCerrar.TabIndex = 29;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // rtbPreview
            // 
            this.rtbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbPreview.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbPreview.Location = new System.Drawing.Point(3, 18);
            this.rtbPreview.Name = "rtbPreview";
            this.rtbPreview.ReadOnly = true;
            this.rtbPreview.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbPreview.Size = new System.Drawing.Size(632, 569);
            this.rtbPreview.TabIndex = 3;
            this.rtbPreview.Text = "";
            // 
            // lblFechaE
            // 
            this.lblFechaE.AutoSize = true;
            this.lblFechaE.Location = new System.Drawing.Point(229, 6);
            this.lblFechaE.Name = "lblFechaE";
            this.lblFechaE.Size = new System.Drawing.Size(0, 16);
            this.lblFechaE.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 420);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Estado";
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(87, 412);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(121, 24);
            this.cmbEstado.TabIndex = 24;
            // 
            // btnAceptarCliente
            // 
            this.btnAceptarCliente.Location = new System.Drawing.Point(29, 497);
            this.btnAceptarCliente.Name = "btnAceptarCliente";
            this.btnAceptarCliente.Size = new System.Drawing.Size(184, 28);
            this.btnAceptarCliente.TabIndex = 18;
            this.btnAceptarCliente.Tag = "FrmCotizacion";
            this.btnAceptarCliente.Text = "✔ Confirmar aceptación";
            this.btnAceptarCliente.UseVisualStyleBackColor = true;
            this.btnAceptarCliente.Click += new System.EventHandler(this.btnAceptarCliente_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCliente);
            this.groupBox1.Controls.Add(this.btnServicio);
            this.groupBox1.Controls.Add(this.btnCarga);
            this.groupBox1.Location = new System.Drawing.Point(7, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 201);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Carga";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblCompleja);
            this.groupBox2.Location = new System.Drawing.Point(7, 388);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(423, 148);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            // 
            // lblCompleja
            // 
            this.lblCompleja.AutoSize = true;
            this.lblCompleja.Location = new System.Drawing.Point(219, 120);
            this.lblCompleja.Name = "lblCompleja";
            this.lblCompleja.Size = new System.Drawing.Size(0, 16);
            this.lblCompleja.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.btnAceptarCliente);
            this.groupBox3.Controls.Add(this.cmbEstado);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.lblFechaE);
            this.groupBox3.Controls.Add(this.dtpVencimiento);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.btnGuardar);
            this.groupBox3.Controls.Add(this.btnGenerarBooking);
            this.groupBox3.Controls.Add(this.btnCerrar);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.grpTotales);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1089, 596);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rtbPreview);
            this.groupBox4.Location = new System.Drawing.Point(455, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(638, 590);
            this.groupBox4.TabIndex = 28;
            this.groupBox4.TabStop = false;
            // 
            // FrmCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 596);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmCotizacion";
            this.Tag = "FrmCotizacion";
            this.Text = "FrmCotizacion";
            this.Load += new System.EventHandler(this.FrmCotizacion_Load);
            this.grpTotales.ResumeLayout(false);
            this.grpTotales.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grpTotales;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Button btnCarga;
        private System.Windows.Forms.Button btnServicio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGenerarBooking;
        private System.Windows.Forms.Button btnCalcularTotal;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpVencimiento;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblCalc;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.RichTextBox rtbPreview;
        private System.Windows.Forms.Label lblFechaE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Button btnAceptarCliente;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblCompleja;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}
