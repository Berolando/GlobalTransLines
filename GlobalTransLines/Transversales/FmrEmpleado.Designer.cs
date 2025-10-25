namespace GlobalTransLines.Transversales
{
    partial class FmrEmpleado
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
            this.ucDatosPersonales = new GlobalTransLines.UI.Controles.UcDatosPersonales();
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
            // ucDatosPersonales
            // 
            this.ucDatosPersonales.Location = new System.Drawing.Point(12, 12);
            this.ucDatosPersonales.Name = "ucDatosPersonales";
            this.ucDatosPersonales.Size = new System.Drawing.Size(795, 317);
            this.ucDatosPersonales.TabIndex = 11;
            // 
            // FmrEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 624);
            this.Controls.Add(this.btnABMClienteEliminar);
            this.Controls.Add(this.btnABMClienteModificar);
            this.Controls.Add(this.btnABMClienteAlta);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ucDatosPersonales);
            this.Name = "FmrEmpleado";
            this.Text = "FmrEmpleado";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnABMClienteEliminar;
        private System.Windows.Forms.Button btnABMClienteModificar;
        private System.Windows.Forms.Button btnABMClienteAlta;
        private System.Windows.Forms.DataGridView dataGridView1;
        private UI.Controles.UcDatosPersonales ucDatosPersonales;
    }
}