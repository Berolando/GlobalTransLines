namespace GlobalTransLines.UI.Controles
{
    partial class UCDireccion
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
            this.ucCalle.AutoSize = true;
            this.ucCalle.Location = new System.Drawing.Point(0, -6);
            this.ucCalle.MensajeError = "Formato inválido.";
            this.ucCalle.Name = "ucCalle";
            this.ucCalle.Size = new System.Drawing.Size(348, 37);
            this.ucCalle.TabIndex = 0;
            this.ucCalle.Texto = "";
            this.ucCalle.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucCalle.Titulo = "Calle";
            // 
            // ucNumero
            // 
            this.ucNumero.Location = new System.Drawing.Point(0, 32);
            this.ucNumero.MensajeError = "Formato inválido.";
            this.ucNumero.Name = "ucNumero";
            this.ucNumero.Size = new System.Drawing.Size(348, 40);
            this.ucNumero.TabIndex = 1;
            this.ucNumero.Texto = "";
            this.ucNumero.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucNumero.Titulo = "Numero";
            // 
            // ucCiudad
            // 
            this.ucCiudad.Location = new System.Drawing.Point(0, 65);
            this.ucCiudad.MensajeError = "Formato inválido.";
            this.ucCiudad.Name = "ucCiudad";
            this.ucCiudad.Size = new System.Drawing.Size(348, 41);
            this.ucCiudad.TabIndex = 2;
            this.ucCiudad.Texto = "";
            this.ucCiudad.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucCiudad.Titulo = "Ciudad";
            // 
            // ucProvincia
            // 
            this.ucProvincia.Location = new System.Drawing.Point(0, 99);
            this.ucProvincia.MensajeError = "Formato inválido.";
            this.ucProvincia.Name = "ucProvincia";
            this.ucProvincia.Size = new System.Drawing.Size(348, 37);
            this.ucProvincia.TabIndex = 3;
            this.ucProvincia.Texto = "";
            this.ucProvincia.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucProvincia.Titulo = "Provincia";
            // 
            // ucPais
            // 
            this.ucPais.Location = new System.Drawing.Point(0, 128);
            this.ucPais.MensajeError = "Formato inválido.";
            this.ucPais.Name = "ucPais";
            this.ucPais.Size = new System.Drawing.Size(348, 39);
            this.ucPais.TabIndex = 4;
            this.ucPais.Texto = "";
            this.ucPais.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucPais.Titulo = "Pais";
            // 
            // ucCodigoPostal
            // 
            this.ucCodigoPostal.Location = new System.Drawing.Point(0, 162);
            this.ucCodigoPostal.MensajeError = "Formato inválido.";
            this.ucCodigoPostal.Name = "ucCodigoPostal";
            this.ucCodigoPostal.Size = new System.Drawing.Size(348, 36);
            this.ucCodigoPostal.TabIndex = 5;
            this.ucCodigoPostal.Texto = "";
            this.ucCodigoPostal.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucCodigoPostal.Titulo = "Codigo Postal";
            // 
            // ucPuerto
            // 
            this.ucPuerto.BackColor = System.Drawing.Color.Transparent;
            this.ucPuerto.Location = new System.Drawing.Point(0, 200);
            this.ucPuerto.MensajeError = "Formato inválido.";
            this.ucPuerto.Name = "ucPuerto";
            this.ucPuerto.Size = new System.Drawing.Size(348, 34);
            this.ucPuerto.TabIndex = 6;
            this.ucPuerto.Texto = "";
            this.ucPuerto.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucPuerto.Titulo = "Puerto";
            // 
            // UCDireccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucPuerto);
            this.Controls.Add(this.ucCalle);
            this.Controls.Add(this.ucCodigoPostal);
            this.Controls.Add(this.ucProvincia);
            this.Controls.Add(this.ucNumero);
            this.Controls.Add(this.ucCiudad);
            this.Controls.Add(this.ucPais);
            this.Name = "UCDireccion";
            this.Size = new System.Drawing.Size(434, 253);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Controles.UCValidaciones ucCalle;
        private UI.Controles.UCValidaciones ucNumero;
        private UI.Controles.UCValidaciones ucCiudad;
        private UI.Controles.UCValidaciones ucProvincia;
        private UI.Controles.UCValidaciones ucPais;
        private UI.Controles.UCValidaciones ucCodigoPostal;
        private UI.Controles.UCValidaciones ucPuerto;
    }
}
