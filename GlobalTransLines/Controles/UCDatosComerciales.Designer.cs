namespace GlobalTransLines.UI.Controles
{
    partial class UCDatosComerciales
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
            this.grbDatosComerciales = new System.Windows.Forms.GroupBox();
            this.ucRazonSocial = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.cmbCondicionFiscal = new System.Windows.Forms.ComboBox();
            this.lblCondicionFiscal = new System.Windows.Forms.Label();
            this.ucCuit = new GlobalTransLines.UI.Controles.UCValidaciones();
            this.grbDatosComerciales.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbDatosComerciales
            // 
            this.grbDatosComerciales.Controls.Add(this.ucRazonSocial);
            this.grbDatosComerciales.Controls.Add(this.cmbCondicionFiscal);
            this.grbDatosComerciales.Controls.Add(this.lblCondicionFiscal);
            this.grbDatosComerciales.Controls.Add(this.ucCuit);
            this.grbDatosComerciales.Location = new System.Drawing.Point(13, 14);
            this.grbDatosComerciales.Name = "grbDatosComerciales";
            this.grbDatosComerciales.Size = new System.Drawing.Size(377, 135);
            this.grbDatosComerciales.TabIndex = 0;
            this.grbDatosComerciales.TabStop = false;
            this.grbDatosComerciales.Text = "Datos Comerciales";
            // 
            // ucRazonSocial
            // 
            this.ucRazonSocial.BackColor = System.Drawing.Color.Transparent;
            this.ucRazonSocial.Location = new System.Drawing.Point(6, 90);
            this.ucRazonSocial.MensajeError = "Formato inválido.";
            this.ucRazonSocial.Name = "ucRazonSocial";
            this.ucRazonSocial.Size = new System.Drawing.Size(368, 40);
            this.ucRazonSocial.TabIndex = 3;
            this.ucRazonSocial.Texto = "";
            this.ucRazonSocial.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.Texto;
            this.ucRazonSocial.Titulo = "Razon Social";
            // 
            // cmbCondicionFiscal
            // 
            this.cmbCondicionFiscal.FormattingEnabled = true;
            this.cmbCondicionFiscal.Location = new System.Drawing.Point(167, 60);
            this.cmbCondicionFiscal.Name = "cmbCondicionFiscal";
            this.cmbCondicionFiscal.Size = new System.Drawing.Size(121, 24);
            this.cmbCondicionFiscal.TabIndex = 2;
            // 
            // lblCondicionFiscal
            // 
            this.lblCondicionFiscal.AutoSize = true;
            this.lblCondicionFiscal.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lblCondicionFiscal.Location = new System.Drawing.Point(21, 68);
            this.lblCondicionFiscal.Name = "lblCondicionFiscal";
            this.lblCondicionFiscal.Size = new System.Drawing.Size(106, 16);
            this.lblCondicionFiscal.TabIndex = 1;
            this.lblCondicionFiscal.Text = "Condicion Fiscal";
            // 
            // ucCuit
            // 
            this.ucCuit.BackColor = System.Drawing.Color.Transparent;
            this.ucCuit.Location = new System.Drawing.Point(6, 21);
            this.ucCuit.MensajeError = "El CUIT debe tener el formato NN-NNNNNNNN-N.";
            this.ucCuit.Name = "ucCuit";
            this.ucCuit.Size = new System.Drawing.Size(368, 40);
            this.ucCuit.TabIndex = 0;
            this.ucCuit.Texto = "";
            this.ucCuit.TipoCampo = GlobalTransLines.UI.Controles.UCValidaciones.TipoCampoEnum.CUIT;
            this.ucCuit.Titulo = "CUIT";
            // 
            // UCDatosComerciales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grbDatosComerciales);
            this.Name = "UCDatosComerciales";
            this.Size = new System.Drawing.Size(399, 153);
            this.grbDatosComerciales.ResumeLayout(false);
            this.grbDatosComerciales.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbDatosComerciales;
        private UI.Controles.UCValidaciones ucRazonSocial;
        private System.Windows.Forms.ComboBox cmbCondicionFiscal;
        private System.Windows.Forms.Label lblCondicionFiscal;
        private UI.Controles.UCValidaciones ucCuit;
    }
}
