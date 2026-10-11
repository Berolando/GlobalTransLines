namespace GlobalTransLines.UI.Controles
{
    partial class UCDirecciones
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
            this.ucCalle = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucNumero = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucCiudad = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucProvincia = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucPais = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucCodigoPostal = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.ucPuerto = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.SuspendLayout();
            // 
            // ucCalle
            // 
            this.ucCalle.BackColor = System.Drawing.Color.Transparent;
            this.ucCalle.Location = new System.Drawing.Point(13, 21);
            this.ucCalle.MensajeError = "Formato inválido.";
            this.ucCalle.Name = "ucCalle";
            this.ucCalle.Size = new System.Drawing.Size(376, 43);
            this.ucCalle.TabIndex = 0;
            this.ucCalle.Texto = "";
            this.ucCalle.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucCalle.Titulo = "Calle";
            // 
            // ucNumero
            // 
            this.ucNumero.BackColor = System.Drawing.Color.Transparent;
            this.ucNumero.Location = new System.Drawing.Point(13, 67);
            this.ucNumero.MensajeError = "Error Validacion, Solo se permiten números.";
            this.ucNumero.Name = "ucNumero";
            this.ucNumero.Size = new System.Drawing.Size(376, 43);
            this.ucNumero.TabIndex = 1;
            this.ucNumero.Texto = "";
            this.ucNumero.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Numeros;
            this.ucNumero.Titulo = "Numero";
            // 
            // ucCiudad
            // 
            this.ucCiudad.BackColor = System.Drawing.Color.Transparent;
            this.ucCiudad.Location = new System.Drawing.Point(13, 107);
            this.ucCiudad.MensajeError = "Error Validacion, Formato inválido.";
            this.ucCiudad.Name = "ucCiudad";
            this.ucCiudad.Size = new System.Drawing.Size(376, 43);
            this.ucCiudad.TabIndex = 2;
            this.ucCiudad.Texto = "";
            this.ucCiudad.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucCiudad.Titulo = "Ciudad";
            // 
            // ucProvincia
            // 
            this.ucProvincia.BackColor = System.Drawing.Color.Transparent;
            this.ucProvincia.Location = new System.Drawing.Point(13, 150);
            this.ucProvincia.MensajeError = "Error Validacion, Formato inválido.";
            this.ucProvincia.Name = "ucProvincia";
            this.ucProvincia.Size = new System.Drawing.Size(376, 43);
            this.ucProvincia.TabIndex = 3;
            this.ucProvincia.Texto = "";
            this.ucProvincia.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucProvincia.Titulo = "Provincia";
            // 
            // ucPais
            // 
            this.ucPais.BackColor = System.Drawing.Color.Transparent;
            this.ucPais.Location = new System.Drawing.Point(13, 193);
            this.ucPais.MensajeError = "Error Validacion, Formato inválido.";
            this.ucPais.Name = "ucPais";
            this.ucPais.Size = new System.Drawing.Size(376, 43);
            this.ucPais.TabIndex = 4;
            this.ucPais.Texto = "";
            this.ucPais.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucPais.Titulo = "Pais";
            // 
            // ucCodigoPostal
            // 
            this.ucCodigoPostal.BackColor = System.Drawing.Color.Transparent;
            this.ucCodigoPostal.Location = new System.Drawing.Point(13, 236);
            this.ucCodigoPostal.MensajeError = "Error Validacion, Formato inválido.";
            this.ucCodigoPostal.Name = "ucCodigoPostal";
            this.ucCodigoPostal.Size = new System.Drawing.Size(376, 43);
            this.ucCodigoPostal.TabIndex = 5;
            this.ucCodigoPostal.Texto = "";
            this.ucCodigoPostal.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucCodigoPostal.Titulo = "Codigo Postal";
            // 
            // ucPuerto
            // 
            this.ucPuerto.BackColor = System.Drawing.Color.Transparent;
            this.ucPuerto.Location = new System.Drawing.Point(13, 279);
            this.ucPuerto.MensajeError = "Error Validacion, Formato inválido.";
            this.ucPuerto.Name = "ucPuerto";
            this.ucPuerto.Size = new System.Drawing.Size(376, 43);
            this.ucPuerto.TabIndex = 6;
            this.ucPuerto.Texto = "";
            this.ucPuerto.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucPuerto.Titulo = "Puerto";
            // 
            // UCDirecciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucPuerto);
            this.Controls.Add(this.ucCodigoPostal);
            this.Controls.Add(this.ucPais);
            this.Controls.Add(this.ucProvincia);
            this.Controls.Add(this.ucCiudad);
            this.Controls.Add(this.ucNumero);
            this.Controls.Add(this.ucCalle);
            this.Name = "UCDirecciones";
            this.Size = new System.Drawing.Size(402, 339);
            this.ResumeLayout(false);

        }

        #endregion

        private UCValidaciones ucCalle;
        private UCValidaciones ucNumero;
        private UCValidaciones ucCiudad;
        private UCValidaciones ucProvincia;
        private UCValidaciones ucPais;
        private UCValidaciones ucCodigoPostal;
        private UCValidaciones ucPuerto;
    }
}
