namespace GlobalTransLines.UI.Gestion2_Transporte
{
    partial class FrmAsignarBuqueContenedor
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
            this.gbBooking = new System.Windows.Forms.GroupBox();
            this.grdBookings = new System.Windows.Forms.DataGridView();
            this.gbContenedores = new System.Windows.Forms.GroupBox();
            this.grdContenedores = new System.Windows.Forms.DataGridView();
            this.gbBuques = new System.Windows.Forms.GroupBox();
            this.grdBuques = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.gbAsignaciones = new System.Windows.Forms.GroupBox();
            this.grdAsignaciones = new System.Windows.Forms.DataGridView();
            this.btnNuevoContenedor = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gbBooking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings)).BeginInit();
            this.gbContenedores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdContenedores)).BeginInit();
            this.gbBuques.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBuques)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbAsignaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAsignaciones)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbBooking
            // 
            this.gbBooking.Controls.Add(this.grdBookings);
            this.gbBooking.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbBooking.Location = new System.Drawing.Point(3, 3);
            this.gbBooking.Name = "gbBooking";
            this.gbBooking.Size = new System.Drawing.Size(1276, 146);
            this.gbBooking.TabIndex = 0;
            this.gbBooking.TabStop = false;
            this.gbBooking.Text = "Bookings Pendientes";
            // 
            // grdBookings
            // 
            this.grdBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBookings.Location = new System.Drawing.Point(3, 18);
            this.grdBookings.Name = "grdBookings";
            this.grdBookings.RowHeadersWidth = 51;
            this.grdBookings.RowTemplate.Height = 24;
            this.grdBookings.Size = new System.Drawing.Size(1270, 125);
            this.grdBookings.TabIndex = 0;
            this.grdBookings.SelectionChanged += new System.EventHandler(this.grdBookings_SelectionChanged);
            // 
            // gbContenedores
            // 
            this.gbContenedores.Controls.Add(this.tableLayoutPanel2);
            this.gbContenedores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbContenedores.Location = new System.Drawing.Point(3, 155);
            this.gbContenedores.Name = "gbContenedores";
            this.gbContenedores.Size = new System.Drawing.Size(1276, 176);
            this.gbContenedores.TabIndex = 1;
            this.gbContenedores.TabStop = false;
            this.gbContenedores.Text = "Contenedores";
            // 
            // grdContenedores
            // 
            this.grdContenedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdContenedores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdContenedores.Location = new System.Drawing.Point(3, 3);
            this.grdContenedores.Name = "grdContenedores";
            this.grdContenedores.RowHeadersWidth = 51;
            this.grdContenedores.RowTemplate.Height = 24;
            this.grdContenedores.Size = new System.Drawing.Size(1270, 104);
            this.grdContenedores.TabIndex = 0;
            this.grdContenedores.SelectionChanged += new System.EventHandler(this.grdContenedores_SelectionChanged);
            // 
            // gbBuques
            // 
            this.gbBuques.Controls.Add(this.grdBuques);
            this.gbBuques.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbBuques.Location = new System.Drawing.Point(3, 337);
            this.gbBuques.Name = "gbBuques";
            this.gbBuques.Size = new System.Drawing.Size(1276, 150);
            this.gbBuques.TabIndex = 1;
            this.gbBuques.TabStop = false;
            this.gbBuques.Text = "Buques";
            // 
            // grdBuques
            // 
            this.grdBuques.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBuques.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBuques.Location = new System.Drawing.Point(3, 18);
            this.grdBuques.Name = "grdBuques";
            this.grdBuques.RowHeadersWidth = 51;
            this.grdBuques.RowTemplate.Height = 24;
            this.grdBuques.Size = new System.Drawing.Size(1270, 129);
            this.grdBuques.TabIndex = 0;
            this.grdBuques.SelectionChanged += new System.EventHandler(this.grdBuques_SelectionChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.gbBooking, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbAsignaciones, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.gbBuques, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gbContenedores, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.23077F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.07692F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.07692F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.07692F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.53846F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1282, 791);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 701);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1276, 87);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(739, 9);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(9);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(150, 28);
            this.btnEliminar.TabIndex = 19;
            this.btnEliminar.Text = "✖️ Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(177, 9);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(9);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(150, 28);
            this.btnModificar.TabIndex = 18;
            this.btnModificar.Text = "✏️ Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(9, 9);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(9);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(150, 28);
            this.btnGuardar.TabIndex = 17;
            this.btnGuardar.Text = "📂 Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(571, 9);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(9);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(150, 28);
            this.btnBuscar.TabIndex = 20;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(345, 9);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(9);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(208, 22);
            this.txtBuscar.TabIndex = 21;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // gbAsignaciones
            // 
            this.gbAsignaciones.Controls.Add(this.grdAsignaciones);
            this.gbAsignaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAsignaciones.Location = new System.Drawing.Point(3, 519);
            this.gbAsignaciones.Name = "gbAsignaciones";
            this.gbAsignaciones.Size = new System.Drawing.Size(1276, 176);
            this.gbAsignaciones.TabIndex = 2;
            this.gbAsignaciones.TabStop = false;
            this.gbAsignaciones.Text = "Asignaciones";
            // 
            // grdAsignaciones
            // 
            this.grdAsignaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAsignaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAsignaciones.Location = new System.Drawing.Point(3, 18);
            this.grdAsignaciones.Name = "grdAsignaciones";
            this.grdAsignaciones.RowHeadersWidth = 51;
            this.grdAsignaciones.RowTemplate.Height = 24;
            this.grdAsignaciones.Size = new System.Drawing.Size(1270, 155);
            this.grdAsignaciones.TabIndex = 0;
            this.grdAsignaciones.SelectionChanged += new System.EventHandler(this.grdAsignaciones_SelectionChanged);
            // 
            // btnNuevoContenedor
            // 
            this.btnNuevoContenedor.Location = new System.Drawing.Point(3, 3);
            this.btnNuevoContenedor.Name = "btnNuevoContenedor";
            this.btnNuevoContenedor.Size = new System.Drawing.Size(168, 23);
            this.btnNuevoContenedor.TabIndex = 1;
            this.btnNuevoContenedor.Text = "Nuevo Contenedor";
            this.btnNuevoContenedor.UseVisualStyleBackColor = true;
            this.btnNuevoContenedor.Click += new System.EventHandler(this.btnNuevoContenedor_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnNuevoContenedor);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(9, 119);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(9);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1258, 27);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnGuardar);
            this.flowLayoutPanel2.Controls.Add(this.btnModificar);
            this.flowLayoutPanel2.Controls.Add(this.txtBuscar);
            this.flowLayoutPanel2.Controls.Add(this.btnBuscar);
            this.flowLayoutPanel2.Controls.Add(this.btnEliminar);
            this.flowLayoutPanel2.Controls.Add(this.btnCerrar);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 18);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(9);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1270, 66);
            this.flowLayoutPanel2.TabIndex = 22;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(0, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 32);
            this.btnCerrar.TabIndex = 23;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.grdContenedores, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1270, 155);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // FrmAsignarBuqueContenedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1282, 791);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmAsignarBuqueContenedor";
            this.Tag = "FrmAsignarBuqueContenedor";
            this.Text = "FrmAsignarBuqueContenedor";
            this.Load += new System.EventHandler(this.FrmAsignarBuqueContenedor_Load);
            this.gbBooking.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings)).EndInit();
            this.gbContenedores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdContenedores)).EndInit();
            this.gbBuques.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBuques)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gbAsignaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAsignaciones)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbBooking;
        private System.Windows.Forms.DataGridView grdBookings;
        private System.Windows.Forms.GroupBox gbContenedores;
        private System.Windows.Forms.DataGridView grdContenedores;
        private System.Windows.Forms.GroupBox gbBuques;
        private System.Windows.Forms.DataGridView grdBuques;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.GroupBox gbAsignaciones;
        private System.Windows.Forms.DataGridView grdAsignaciones;
        private System.Windows.Forms.Button btnNuevoContenedor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnCerrar;
    }
}
