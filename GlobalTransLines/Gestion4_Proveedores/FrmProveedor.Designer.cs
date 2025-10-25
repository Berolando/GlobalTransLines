namespace GlobalTransLines.UI.Gestion4_Proveedores
{
    partial class FrmProveedor
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
            this.btnABMClienteEliminar = new System.Windows.Forms.Button();
            this.btnABMClienteModificar = new System.Windows.Forms.Button();
            this.btnABMClienteAlta = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ucDatosComerciales1 = new GlobalTransLines.UI.Controles.UCDatosComerciales();
            this.ucPer1 = new GlobalTransLines.UI.Controles.UcDatosPersonales();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnABMClienteEliminar
            // 
            this.btnABMClienteEliminar.Location = new System.Drawing.Point(1100, 298);
            this.btnABMClienteEliminar.Margin = new System.Windows.Forms.Padding(4);
            this.btnABMClienteEliminar.Name = "btnABMClienteEliminar";
            this.btnABMClienteEliminar.Size = new System.Drawing.Size(100, 43);
            this.btnABMClienteEliminar.TabIndex = 16;
            this.btnABMClienteEliminar.Text = "Eliminar";
            this.btnABMClienteEliminar.UseVisualStyleBackColor = true;
            // 
            // btnABMClienteModificar
            // 
            this.btnABMClienteModificar.Location = new System.Drawing.Point(992, 298);
            this.btnABMClienteModificar.Margin = new System.Windows.Forms.Padding(4);
            this.btnABMClienteModificar.Name = "btnABMClienteModificar";
            this.btnABMClienteModificar.Size = new System.Drawing.Size(100, 43);
            this.btnABMClienteModificar.TabIndex = 15;
            this.btnABMClienteModificar.Text = "Modificar";
            this.btnABMClienteModificar.UseVisualStyleBackColor = true;
            // 
            // btnABMClienteAlta
            // 
            this.btnABMClienteAlta.Location = new System.Drawing.Point(884, 298);
            this.btnABMClienteAlta.Margin = new System.Windows.Forms.Padding(4);
            this.btnABMClienteAlta.Name = "btnABMClienteAlta";
            this.btnABMClienteAlta.Size = new System.Drawing.Size(100, 43);
            this.btnABMClienteAlta.TabIndex = 14;
            this.btnABMClienteAlta.Text = "Alta";
            this.btnABMClienteAlta.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 373);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1246, 225);
            this.dataGridView1.TabIndex = 13;
            // 
            // ucDatosComerciales1
            // 
            this.ucDatosComerciales1.Location = new System.Drawing.Point(848, 12);
            this.ucDatosComerciales1.Name = "ucDatosComerciales1";
            this.ucDatosComerciales1.Size = new System.Drawing.Size(399, 153);
            this.ucDatosComerciales1.TabIndex = 12;
            // 
            // ucPer1
            // 
            this.ucPer1.Location = new System.Drawing.Point(12, 12);
            this.ucPer1.Name = "ucPer1";
            this.ucPer1.Size = new System.Drawing.Size(795, 317);
            this.ucPer1.TabIndex = 11;
            // 
            // FrmProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 659);
            this.Controls.Add(this.btnABMClienteEliminar);
            this.Controls.Add(this.btnABMClienteModificar);
            this.Controls.Add(this.btnABMClienteAlta);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ucDatosComerciales1);
            this.Controls.Add(this.ucPer1);
            this.Name = "FrmProveedor";
            this.Text = "FrmProveedor";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnABMClienteEliminar;
        private System.Windows.Forms.Button btnABMClienteModificar;
        private System.Windows.Forms.Button btnABMClienteAlta;
        private System.Windows.Forms.DataGridView dataGridView1;
        private UI.Controles.UCDatosComerciales ucDatosComerciales1;
        private UI.Controles.UcDatosPersonales ucPer1;
    }
}