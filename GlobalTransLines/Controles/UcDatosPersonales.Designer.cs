namespace GlobalTransLines.UI.Controles
{
    partial class UcDatosPersonales
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.ucEmail = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucTelefono = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucDni = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucApellido = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucNombre = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.panelDireccion = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFechaNacimiento);
            this.groupBox1.Controls.Add(this.ucEmail);
            this.groupBox1.Controls.Add(this.ucTelefono);
            this.groupBox1.Controls.Add(this.ucDni);
            this.groupBox1.Controls.Add(this.ucApellido);
            this.groupBox1.Controls.Add(this.ucNombre);
            this.groupBox1.Location = new System.Drawing.Point(8, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 318);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos Personales";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Location = new System.Drawing.Point(16, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Fecha de nacimiento";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(164, 273);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(200, 22);
            this.dtpFechaNacimiento.TabIndex = 5;
            // 
            // ucEmail
            // 
            this.ucEmail.BackColor = System.Drawing.Color.Transparent;
            this.ucEmail.Location = new System.Drawing.Point(7, 217);
            this.ucEmail.MensajeError = "El formato del email no es válido.";
            this.ucEmail.Name = "ucEmail";
            this.ucEmail.Size = new System.Drawing.Size(368, 40);
            this.ucEmail.TabIndex = 4;
            this.ucEmail.Texto = "";
            this.ucEmail.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Email;
            this.ucEmail.Titulo = "Email";
            // 
            // ucTelefono
            // 
            this.ucTelefono.BackColor = System.Drawing.Color.Transparent;
            this.ucTelefono.Location = new System.Drawing.Point(7, 171);
            this.ucTelefono.MensajeError = "El teléfono no tiene un formato válido.";
            this.ucTelefono.Name = "ucTelefono";
            this.ucTelefono.Size = new System.Drawing.Size(368, 40);
            this.ucTelefono.TabIndex = 3;
            this.ucTelefono.Texto = "";
            this.ucTelefono.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Telefono;
            this.ucTelefono.Titulo = "Telefono";
            // 
            // ucDni
            // 
            this.ucDni.BackColor = System.Drawing.Color.Transparent;
            this.ucDni.Location = new System.Drawing.Point(7, 116);
            this.ucDni.MensajeError = "El DNI debe tener 7 u 8 dígitos.";
            this.ucDni.Name = "ucDni";
            this.ucDni.Size = new System.Drawing.Size(368, 40);
            this.ucDni.TabIndex = 2;
            this.ucDni.Texto = "";
            this.ucDni.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.DNI;
            this.ucDni.Titulo = "DNI";
            // 
            // ucApellido
            // 
            this.ucApellido.BackColor = System.Drawing.Color.Transparent;
            this.ucApellido.Location = new System.Drawing.Point(7, 70);
            this.ucApellido.MensajeError = "Formato inválido.";
            this.ucApellido.Name = "ucApellido";
            this.ucApellido.Size = new System.Drawing.Size(368, 40);
            this.ucApellido.TabIndex = 1;
            this.ucApellido.Texto = "";
            this.ucApellido.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucApellido.Titulo = "Apellido";
            // 
            // ucNombre
            // 
            this.ucNombre.BackColor = System.Drawing.Color.Transparent;
            this.ucNombre.Location = new System.Drawing.Point(6, 24);
            this.ucNombre.MensajeError = "Formato inválido.";
            this.ucNombre.Name = "ucNombre";
            this.ucNombre.Size = new System.Drawing.Size(368, 40);
            this.ucNombre.TabIndex = 0;
            this.ucNombre.Texto = "";
            this.ucNombre.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucNombre.Titulo = "Nombre";
            // 
            // panelDireccion
            // 
            this.panelDireccion.Location = new System.Drawing.Point(431, 17);
            this.panelDireccion.Name = "panelDireccion";
            this.panelDireccion.Size = new System.Drawing.Size(417, 318);
            this.panelDireccion.TabIndex = 7;
            this.panelDireccion.TabStop = false;
            this.panelDireccion.Text = "Direccion";
            // 
            // UcDatosPersonales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelDireccion);
            this.Controls.Add(this.groupBox1);
            this.Name = "UcDatosPersonales";
            this.Size = new System.Drawing.Size(882, 368);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UI.Controles.UCValidaciones ucEmail;
        private UI.Controles.UCValidaciones ucTelefono;
        private UI.Controles.UCValidaciones ucDni;
        private UI.Controles.UCValidaciones ucApellido;
        private UI.Controles.UCValidaciones ucNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.GroupBox panelDireccion;
    }
}
