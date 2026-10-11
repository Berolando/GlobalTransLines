namespace GlobalTransLines.UI.Gestion1_Booking
{
    partial class FrmCotizaciones
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
            this.grdCotizaciones = new System.Windows.Forms.DataGridView();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnNueva = new System.Windows.Forms.Button();
            this.gprAbm = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRechazar = new System.Windows.Forms.Button();
            this.btnAprobar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gprGrilla = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdCotizaciones)).BeginInit();
            this.gprAbm.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gprGrilla.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdCotizaciones
            // 
            this.grdCotizaciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdCotizaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCotizaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCotizaciones.Location = new System.Drawing.Point(3, 18);
            this.grdCotizaciones.Name = "grdCotizaciones";
            this.grdCotizaciones.RowHeadersWidth = 51;
            this.grdCotizaciones.RowTemplate.Height = 24;
            this.grdCotizaciones.Size = new System.Drawing.Size(824, 301);
            this.grdCotizaciones.TabIndex = 0;
            this.grdCotizaciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdCotizaciones_CellClick);
            this.grdCotizaciones.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdCotizaciones_CellDoubleClick);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(439, 21);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(150, 28);
            this.btnEliminar.TabIndex = 26;
            this.btnEliminar.Text = "✖️ Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(250, 21);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(150, 28);
            this.btnModificar.TabIndex = 24;
            this.btnModificar.Text = "✏️ Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnNueva
            // 
            this.btnNueva.Location = new System.Drawing.Point(17, 21);
            this.btnNueva.Name = "btnNueva";
            this.btnNueva.Size = new System.Drawing.Size(150, 28);
            this.btnNueva.TabIndex = 27;
            this.btnNueva.Text = "➕ Nueva Cotizacion";
            this.btnNueva.UseVisualStyleBackColor = true;
            this.btnNueva.Click += new System.EventHandler(this.btnNueva_Click);
            // 
            // gprAbm
            // 
            this.gprAbm.Controls.Add(this.btnExit);
            this.gprAbm.Controls.Add(this.btnRechazar);
            this.gprAbm.Controls.Add(this.btnAprobar);
            this.gprAbm.Controls.Add(this.txtBuscar);
            this.gprAbm.Controls.Add(this.btnBuscar);
            this.gprAbm.Controls.Add(this.btnEliminar);
            this.gprAbm.Controls.Add(this.btnNueva);
            this.gprAbm.Controls.Add(this.btnModificar);
            this.gprAbm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gprAbm.Location = new System.Drawing.Point(3, 3);
            this.gprAbm.Name = "gprAbm";
            this.gprAbm.Size = new System.Drawing.Size(830, 157);
            this.gprAbm.TabIndex = 28;
            this.gprAbm.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(634, 21);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 28);
            this.btnExit.TabIndex = 32;
            this.btnExit.Text = "Cerrar";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRechazar
            // 
            this.btnRechazar.Location = new System.Drawing.Point(634, 67);
            this.btnRechazar.Name = "btnRechazar";
            this.btnRechazar.Size = new System.Drawing.Size(150, 28);
            this.btnRechazar.TabIndex = 31;
            this.btnRechazar.Text = "Rechazar";
            this.btnRechazar.UseVisualStyleBackColor = true;
            this.btnRechazar.Visible = false;
            this.btnRechazar.Click += new System.EventHandler(this.btnRechazar_Click);
            // 
            // btnAprobar
            // 
            this.btnAprobar.Location = new System.Drawing.Point(453, 67);
            this.btnAprobar.Name = "btnAprobar";
            this.btnAprobar.Size = new System.Drawing.Size(150, 28);
            this.btnAprobar.TabIndex = 30;
            this.btnAprobar.Text = "Aprobar";
            this.btnAprobar.UseVisualStyleBackColor = true;
            this.btnAprobar.Visible = false;
            this.btnAprobar.Click += new System.EventHandler(this.btnAprobar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(31, 70);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(208, 22);
            this.txtBuscar.TabIndex = 29;
            this.txtBuscar.TextChanged += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(264, 64);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(150, 28);
            this.btnBuscar.TabIndex = 28;
            this.btnBuscar.Text = "🔍 Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gprAbm, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gprGrilla, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(836, 491);
            this.tableLayoutPanel1.TabIndex = 29;
            // 
            // gprGrilla
            // 
            this.gprGrilla.Controls.Add(this.grdCotizaciones);
            this.gprGrilla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gprGrilla.Location = new System.Drawing.Point(3, 166);
            this.gprGrilla.Name = "gprGrilla";
            this.gprGrilla.Size = new System.Drawing.Size(830, 322);
            this.gprGrilla.TabIndex = 29;
            this.gprGrilla.TabStop = false;
            // 
            // FrmCotizaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 491);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCotizaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "FrmCotizaciones";
            this.Text = "Cotizaciones";
            this.Load += new System.EventHandler(this.FrmCotizaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdCotizaciones)).EndInit();
            this.gprAbm.ResumeLayout(false);
            this.gprAbm.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gprGrilla.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdCotizaciones;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnNueva;
        private System.Windows.Forms.GroupBox gprAbm;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnRechazar;
        private System.Windows.Forms.Button btnAprobar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gprGrilla;
        private System.Windows.Forms.Button btnExit;
    }
}
