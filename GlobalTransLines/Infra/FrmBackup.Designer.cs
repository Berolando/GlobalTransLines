namespace GlobalTransLines.UI.Infra
{
    partial class FrmBackup
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
            this.grdBackups = new System.Windows.Forms.DataGridView();
            this.txtBuscarUsuario = new System.Windows.Forms.TextBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkFiltrarFecha = new System.Windows.Forms.CheckBox();
            this.btnGenerarBackup = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdBackups)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdBackups
            // 
            this.grdBackups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBackups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBackups.Location = new System.Drawing.Point(0, 125);
            this.grdBackups.Name = "grdBackups";
            this.grdBackups.RowHeadersWidth = 51;
            this.grdBackups.RowTemplate.Height = 24;
            this.grdBackups.Size = new System.Drawing.Size(1000, 475);
            this.grdBackups.TabIndex = 0;
            this.grdBackups.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdBackups_CellClick);
            this.grdBackups.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdBackups_CellDoubleClick);
            // 
            // txtBuscarUsuario
            // 
            this.txtBuscarUsuario.Location = new System.Drawing.Point(184, 32);
            this.txtBuscarUsuario.Name = "txtBuscarUsuario";
            this.txtBuscarUsuario.Size = new System.Drawing.Size(221, 22);
            this.txtBuscarUsuario.TabIndex = 1;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(18, 82);
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(130, 22);
            this.dtpFecha.TabIndex = 2;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Location = new System.Drawing.Point(527, 64);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(100, 43);
            this.btnRestaurar.TabIndex = 3;
            this.btnRestaurar.Text = "Restaurar";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(643, 64);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(100, 43);
            this.btnActualizar.TabIndex = 4;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(411, 34);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 16);
            this.lblEstado.TabIndex = 7;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(18, 28);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(160, 31);
            this.btnBuscar.TabIndex = 8;
            this.btnBuscar.Text = "Aplicar filtros";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblEstado);
            this.groupBox1.Controls.Add(this.btnGenerarBackup);
            this.groupBox1.Controls.Add(this.chkFiltrarFecha);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.btnActualizar);
            this.groupBox1.Controls.Add(this.btnRestaurar);
            this.groupBox1.Controls.Add(this.btnCerrar);
            this.groupBox1.Controls.Add(this.dtpFecha);
            this.groupBox1.Controls.Add(this.txtBuscarUsuario);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1000, 125);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Backup/Restore";
            // 
            // chkFiltrarFecha
            // 
            this.chkFiltrarFecha.AutoSize = true;
            this.chkFiltrarFecha.Location = new System.Drawing.Point(237, 82);
            this.chkFiltrarFecha.Name = "chkFiltrarFecha";
            this.chkFiltrarFecha.Size = new System.Drawing.Size(121, 20);
            this.chkFiltrarFecha.TabIndex = 9;
            this.chkFiltrarFecha.Text = "Filtrar por fecha";
            this.chkFiltrarFecha.UseVisualStyleBackColor = true;
            // 
            // btnGenerarBackup
            // 
            this.btnGenerarBackup.Location = new System.Drawing.Point(411, 64);
            this.btnGenerarBackup.Name = "btnGenerarBackup";
            this.btnGenerarBackup.Size = new System.Drawing.Size(100, 43);
            this.btnGenerarBackup.TabIndex = 10;
            this.btnGenerarBackup.Text = "Generar backup";
            this.btnGenerarBackup.UseVisualStyleBackColor = true;
            this.btnGenerarBackup.Click += new System.EventHandler(this.btnGenerarBackup_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(759, 64);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(100, 43);
            this.btnCerrar.TabIndex = 11;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FrmBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.grdBackups);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmBackup";
            this.Tag = "FrmBackup";
            this.Text = "Backup y Restore";
            this.Load += new System.EventHandler(this.FrmBackup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdBackups)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdBackups;
        private System.Windows.Forms.TextBox txtBuscarUsuario;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkFiltrarFecha;
        private System.Windows.Forms.Button btnGenerarBackup;
        private System.Windows.Forms.Button btnCerrar;
    }
}
