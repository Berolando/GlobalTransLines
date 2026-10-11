namespace GlobalTransLines.UI.Gestion1_Booking
{
    partial class FrmCliente
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
            this.grdClientes = new System.Windows.Forms.DataGridView();
            this.btnAlta = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ucDatosComerciales = new GlobalTransLines.UI.Controles.UCDatosComerciales();
            this.ucDatosPersonales = new GlobalTransLines.UI.Controles.UCDatosPersonales();
            this.ucDirecciones = new GlobalTransLines.UI.Controles.UCDirecciones();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdClientes)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdClientes
            // 
            this.grdClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClientes.Location = new System.Drawing.Point(0, 375);
            this.grdClientes.Name = "grdClientes";
            this.grdClientes.RowHeadersWidth = 51;
            this.grdClientes.RowTemplate.Height = 24;
            this.grdClientes.Size = new System.Drawing.Size(1231, 193);
            this.grdClientes.TabIndex = 2;
            this.grdClientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdClientes_CellClick);
            this.grdClientes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdClientes_CellDoubleClick);
            // 
            // btnAlta
            // 
            this.btnAlta.Location = new System.Drawing.Point(542, 15);
            this.btnAlta.Margin = new System.Windows.Forms.Padding(4);
            this.btnAlta.Name = "btnAlta";
            this.btnAlta.Size = new System.Drawing.Size(150, 28);
            this.btnAlta.TabIndex = 8;
            this.btnAlta.Text = "📂 Guardar";
            this.btnAlta.UseVisualStyleBackColor = true;
            this.btnAlta.Click += new System.EventHandler(this.btnAlta_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(729, 16);
            this.btnModificar.Margin = new System.Windows.Forms.Padding(4);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(150, 28);
            this.btnModificar.TabIndex = 9;
            this.btnModificar.Text = "✏️ Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(911, 15);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(150, 28);
            this.btnEliminar.TabIndex = 10;
            this.btnEliminar.Text = "✖️ Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(312, 15);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(150, 28);
            this.btnBuscar.TabIndex = 15;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(56, 16);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(208, 22);
            this.txtBuscar.TabIndex = 16;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnCerrar);
            this.groupBox3.Controls.Add(this.btnBuscar);
            this.groupBox3.Controls.Add(this.btnEliminar);
            this.groupBox3.Controls.Add(this.txtBuscar);
            this.groupBox3.Controls.Add(this.btnModificar);
            this.groupBox3.Controls.Add(this.btnAlta);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 568);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1231, 56);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ucDatosPersonales);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 356);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Referente";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ucDirecciones);
            this.groupBox2.Location = new System.Drawing.Point(415, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(399, 356);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Direccion";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ucDatosComerciales);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1231, 375);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Location = new System.Drawing.Point(1068, 15);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(150, 28);
            this.btnCerrar.TabIndex = 17;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // ucDatosComerciales
            // 
            this.ucDatosComerciales.Location = new System.Drawing.Point(820, 0);
            this.ucDatosComerciales.Name = "ucDatosComerciales";
            this.ucDatosComerciales.Size = new System.Drawing.Size(399, 153);
            this.ucDatosComerciales.TabIndex = 13;
            // 
            // ucDatosPersonales
            // 
            this.ucDatosPersonales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDatosPersonales.Location = new System.Drawing.Point(3, 18);
            this.ucDatosPersonales.Name = "ucDatosPersonales";
            this.ucDatosPersonales.Size = new System.Drawing.Size(390, 335);
            this.ucDatosPersonales.TabIndex = 11;
            // 
            // ucDirecciones
            // 
            this.ucDirecciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDirecciones.Location = new System.Drawing.Point(3, 18);
            this.ucDirecciones.Name = "ucDirecciones";
            this.ucDirecciones.Size = new System.Drawing.Size(393, 335);
            this.ucDirecciones.TabIndex = 12;
            // 
            // FrmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 624);
            this.Controls.Add(this.grdClientes);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmCliente";
            this.Tag = "FrmCliente";
            this.Text = "👤Cliente";
            this.Load += new System.EventHandler(this.FrmCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdClientes)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        
        private System.Windows.Forms.DataGridView grdClientes;
        private System.Windows.Forms.Button btnAlta;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controles.UCDatosPersonales ucDatosPersonales;
        private Controles.UCDatosComerciales ucDatosComerciales;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controles.UCDirecciones ucDirecciones;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnCerrar;
    }
}
