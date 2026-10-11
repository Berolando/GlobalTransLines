namespace GlobalTransLines.UI.Controles
{
    partial class UCDatosPersonales
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
            this.ucNombre = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucApellido = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucDNI = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucTelefono = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucEmail = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ucNombre
            // 
            this.ucNombre.BackColor = System.Drawing.Color.Transparent;
            this.ucNombre.Location = new System.Drawing.Point(12, 20);
            this.ucNombre.MensajeError = "Formato inválido.";
            this.ucNombre.Name = "ucNombre";
            this.ucNombre.Size = new System.Drawing.Size(368, 40);
            this.ucNombre.TabIndex = 0;
            this.ucNombre.Texto = "";
            this.ucNombre.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucNombre.Titulo = "Nombre";
            // 
            // ucApellido
            // 
            this.ucApellido.BackColor = System.Drawing.Color.Transparent;
            this.ucApellido.Location = new System.Drawing.Point(12, 66);
            this.ucApellido.MensajeError = "Error Validacion, Formato inválido.";
            this.ucApellido.Name = "ucApellido";
            this.ucApellido.Size = new System.Drawing.Size(368, 40);
            this.ucApellido.TabIndex = 1;
            this.ucApellido.Texto = "";
            this.ucApellido.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucApellido.Titulo = "Apellido";
            // 
            // ucDNI
            // 
            this.ucDNI.BackColor = System.Drawing.Color.Transparent;
            this.ucDNI.Location = new System.Drawing.Point(12, 112);
            this.ucDNI.MensajeError = "Error Validacion,Error Validacion,El DNI debe tener 7 u 8 dígitos.";
            this.ucDNI.Name = "ucDNI";
            this.ucDNI.Size = new System.Drawing.Size(368, 40);
            this.ucDNI.TabIndex = 2;
            this.ucDNI.Texto = "";
            this.ucDNI.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.DNI;
            this.ucDNI.Titulo = "DNI";
            // 
            // ucTelefono
            // 
            this.ucTelefono.BackColor = System.Drawing.Color.Transparent;
            this.ucTelefono.Location = new System.Drawing.Point(12, 158);
            this.ucTelefono.MensajeError = "Error Validacion, El teléfono no tiene un formato válido.";
            this.ucTelefono.Name = "ucTelefono";
            this.ucTelefono.Size = new System.Drawing.Size(368, 40);
            this.ucTelefono.TabIndex = 3;
            this.ucTelefono.Texto = "";
            this.ucTelefono.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Telefono;
            this.ucTelefono.Titulo = "Telefono";
            // 
            // ucEmail
            // 
            this.ucEmail.BackColor = System.Drawing.Color.Transparent;
            this.ucEmail.Location = new System.Drawing.Point(12, 214);
            this.ucEmail.MensajeError = "Error Validacion, El formato del email no es válido.";
            this.ucEmail.Name = "ucEmail";
            this.ucEmail.Size = new System.Drawing.Size(368, 40);
            this.ucEmail.TabIndex = 4;
            this.ucEmail.Texto = "";
            this.ucEmail.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Email;
            this.ucEmail.Titulo = "Email";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(170, 281);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(200, 22);
            this.dtpFechaNacimiento.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Location = new System.Drawing.Point(20, 286);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Fecha de Nacimiento";
            // 
            // UCDatosPersonales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFechaNacimiento);
            this.Controls.Add(this.ucEmail);
            this.Controls.Add(this.ucTelefono);
            this.Controls.Add(this.ucDNI);
            this.Controls.Add(this.ucApellido);
            this.Controls.Add(this.ucNombre);
            this.Name = "UCDatosPersonales";
            this.Size = new System.Drawing.Size(442, 362);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCValidaciones ucNombre;
        private UCValidaciones ucApellido;
        private UCValidaciones ucDNI;
        private UCValidaciones ucTelefono;
        private UCValidaciones ucEmail;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label label1;
    }
}
