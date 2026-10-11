namespace GlobalTransLines.UI.Infra
{
    partial class FrmEmpleado
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
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            this.grpDatos = new System.Windows.Forms.GroupBox();
            this.ucDatosPersonales = new GlobalTransLines.UI.Controles.UCDatosPersonales();
            this.grpDireccion = new System.Windows.Forms.GroupBox();
            this.ucDirecciones = new GlobalTransLines.UI.Controles.UCDirecciones();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.grpCabecera = new System.Windows.Forms.GroupBox();
            this.numSueldo = new System.Windows.Forms.NumericUpDown();
            this.lblSueldo = new System.Windows.Forms.Label();
            this.btnSeleccionarFoto = new System.Windows.Forms.Button();
            this.btnQuitarFoto = new System.Windows.Forms.Button();
            this.cmbCargo = new System.Windows.Forms.ComboBox();
            this.lblCargo = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.grpDatos.SuspendLayout();
            this.grpDireccion.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpCabecera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSueldo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmpleados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmpleados.Location = new System.Drawing.Point(0, 386);
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.RowHeadersWidth = 51;
            this.dgvEmpleados.RowTemplate.Height = 24;
            this.dgvEmpleados.Size = new System.Drawing.Size(1271, 235);
            this.dgvEmpleados.TabIndex = 13;
            this.dgvEmpleados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmpleados_CellClick);
            this.dgvEmpleados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmpleados_CellDoubleClick);
            // 
            // grpDatos
            // 
            this.grpDatos.Controls.Add(this.ucDatosPersonales);
            this.grpDatos.Location = new System.Drawing.Point(6, 18);
            this.grpDatos.Name = "grpDatos";
            this.grpDatos.Size = new System.Drawing.Size(400, 343);
            this.grpDatos.TabIndex = 17;
            this.grpDatos.TabStop = false;
            this.grpDatos.Text = "Datos Personales";
            // 
            // ucDatosPersonales
            // 
            this.ucDatosPersonales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDatosPersonales.Location = new System.Drawing.Point(3, 18);
            this.ucDatosPersonales.Name = "ucDatosPersonales";
            this.ucDatosPersonales.Size = new System.Drawing.Size(394, 322);
            this.ucDatosPersonales.TabIndex = 11;
            // 
            // grpDireccion
            // 
            this.grpDireccion.Controls.Add(this.ucDirecciones);
            this.grpDireccion.Location = new System.Drawing.Point(412, 21);
            this.grpDireccion.Name = "grpDireccion";
            this.grpDireccion.Size = new System.Drawing.Size(400, 343);
            this.grpDireccion.TabIndex = 18;
            this.grpDireccion.TabStop = false;
            this.grpDireccion.Text = "Direccion";
            // 
            // ucDirecciones
            // 
            this.ucDirecciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDirecciones.Location = new System.Drawing.Point(3, 18);
            this.ucDirecciones.Name = "ucDirecciones";
            this.ucDirecciones.Size = new System.Drawing.Size(394, 322);
            this.ucDirecciones.TabIndex = 12;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnGuardar);
            this.groupBox3.Controls.Add(this.txtBuscar);
            this.groupBox3.Controls.Add(this.btnBuscar);
            this.groupBox3.Controls.Add(this.btnEliminar);
            this.groupBox3.Controls.Add(this.btnModificar);
            this.groupBox3.Controls.Add(this.btnLimpiar);
            this.groupBox3.Controls.Add(this.btnCerrar);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 621);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1271, 60);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(500, 16);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 28);
            this.btnGuardar.TabIndex = 40;
            this.btnGuardar.Text = "📂 Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(78, 16);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(208, 22);
            this.txtBuscar.TabIndex = 39;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(310, 16);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(150, 28);
            this.btnBuscar.TabIndex = 38;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(780, 16);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(120, 28);
            this.btnEliminar.TabIndex = 37;
            this.btnEliminar.Text = "✖️ Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(640, 16);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(120, 28);
            this.btnModificar.TabIndex = 36;
            this.btnModificar.Text = "✏️ Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(920, 16);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(120, 28);
            this.btnLimpiar.TabIndex = 42;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Location = new System.Drawing.Point(1105, 16);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(130, 28);
            this.btnCerrar.TabIndex = 41;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // grpCabecera
            // 
            this.grpCabecera.Controls.Add(this.numSueldo);
            this.grpCabecera.Controls.Add(this.lblSueldo);
            this.grpCabecera.Controls.Add(this.btnSeleccionarFoto);
            this.grpCabecera.Controls.Add(this.btnQuitarFoto);
            this.grpCabecera.Controls.Add(this.cmbCargo);
            this.grpCabecera.Controls.Add(this.lblCargo);
            this.grpCabecera.Controls.Add(this.grpDatos);
            this.grpCabecera.Controls.Add(this.pictureBox1);
            this.grpCabecera.Controls.Add(this.grpDireccion);
            this.grpCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpCabecera.Location = new System.Drawing.Point(0, 0);
            this.grpCabecera.Name = "grpCabecera";
            this.grpCabecera.Size = new System.Drawing.Size(1271, 386);
            this.grpCabecera.TabIndex = 21;
            this.grpCabecera.TabStop = false;
            // 
            // numSueldo
            // 
            this.numSueldo.DecimalPlaces = 2;
            this.numSueldo.Location = new System.Drawing.Point(970, 294);
            this.numSueldo.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numSueldo.Name = "numSueldo";
            this.numSueldo.Size = new System.Drawing.Size(289, 22);
            this.numSueldo.TabIndex = 25;
            this.numSueldo.ThousandsSeparator = true;
            // 
            // lblSueldo
            // 
            this.lblSueldo.AutoSize = true;
            this.lblSueldo.Location = new System.Drawing.Point(909, 296);
            this.lblSueldo.Name = "lblSueldo";
            this.lblSueldo.Size = new System.Drawing.Size(49, 16);
            this.lblSueldo.TabIndex = 24;
            this.lblSueldo.Text = "Sueldo";
            // 
            // btnSeleccionarFoto
            // 
            this.btnSeleccionarFoto.Location = new System.Drawing.Point(912, 156);
            this.btnSeleccionarFoto.Name = "btnSeleccionarFoto";
            this.btnSeleccionarFoto.Size = new System.Drawing.Size(150, 28);
            this.btnSeleccionarFoto.TabIndex = 23;
            this.btnSeleccionarFoto.Text = "Seleccionar foto";
            this.btnSeleccionarFoto.UseVisualStyleBackColor = true;
            this.btnSeleccionarFoto.Click += new System.EventHandler(this.btnSeleccionarFoto_Click);
            // 
            // btnQuitarFoto
            // 
            this.btnQuitarFoto.Location = new System.Drawing.Point(1081, 156);
            this.btnQuitarFoto.Name = "btnQuitarFoto";
            this.btnQuitarFoto.Size = new System.Drawing.Size(150, 28);
            this.btnQuitarFoto.TabIndex = 22;
            this.btnQuitarFoto.Text = "Quitar foto";
            this.btnQuitarFoto.UseVisualStyleBackColor = true;
            this.btnQuitarFoto.Click += new System.EventHandler(this.btnQuitarFoto_Click);
            // 
            // cmbCargo
            // 
            this.cmbCargo.FormattingEnabled = true;
            this.cmbCargo.Location = new System.Drawing.Point(970, 248);
            this.cmbCargo.Name = "cmbCargo";
            this.cmbCargo.Size = new System.Drawing.Size(289, 24);
            this.cmbCargo.TabIndex = 21;
            // 
            // lblCargo
            // 
            this.lblCargo.AutoSize = true;
            this.lblCargo.Location = new System.Drawing.Point(909, 256);
            this.lblCargo.Name = "lblCargo";
            this.lblCargo.Size = new System.Drawing.Size(44, 16);
            this.lblCargo.TabIndex = 19;
            this.lblCargo.Text = "Cargo";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = global::GlobalTransLines.UI.Properties.Resources._554846;
            this.pictureBox1.Location = new System.Drawing.Point(1096, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(135, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // FrmEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 681);
            this.Controls.Add(this.dgvEmpleados);
            this.Controls.Add(this.grpCabecera);
            this.Controls.Add(this.groupBox3);
            this.Name = "FrmEmpleado";
            this.Tag = "FrmEmpleado";
            this.Text = "Empleados";
            this.Load += new System.EventHandler(this.FrmEmpleado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.grpDatos.ResumeLayout(false);
            this.grpDireccion.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpCabecera.ResumeLayout(false);
            this.grpCabecera.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSueldo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvEmpleados;
        private System.Windows.Forms.GroupBox grpDatos;
        private Controles.UCDatosPersonales ucDatosPersonales;
        private Controles.UCDirecciones ucDirecciones;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox grpDireccion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.GroupBox grpCabecera;
        private System.Windows.Forms.NumericUpDown numSueldo;
        private System.Windows.Forms.Label lblSueldo;
        private System.Windows.Forms.Button btnSeleccionarFoto;
        private System.Windows.Forms.Button btnQuitarFoto;
        private System.Windows.Forms.ComboBox cmbCargo;
        private System.Windows.Forms.Label lblCargo;
    }
}
