namespace GlobalTransLines.UI.Infra
{
    partial class FrmUsuario
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkDescifrarPassword = new System.Windows.Forms.CheckBox();
            this.txtPasswordCifrada = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvABMUsuarioUsuarios = new System.Windows.Forms.DataGridView();
            this.ABMUsuarioBtnExit = new System.Windows.Forms.Button();
            this.ABMUsuarioBtnEliminar = new System.Windows.Forms.Button();
            this.ABMUsuarioBtnModificar = new System.Windows.Forms.Button();
            this.ABMUsuarioBtnAlta = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEmpleadoActual = new System.Windows.Forms.TextBox();
            this.ABMUsuarioBtnLimpiar = new System.Windows.Forms.Button();
            this.ABMUsuarioTxtBxPassword = new System.Windows.Forms.TextBox();
            this.btnAsociarEmpleado = new System.Windows.Forms.Button();
            this.ABMUsuarioTxtBxUsuario = new System.Windows.Forms.TextBox();
            this.ABMUsuarioTxtBxId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnQuitarEmpleado = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvABMUsuarioUsuarios)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkDescifrarPassword);
            this.groupBox3.Controls.Add(this.txtPasswordCifrada);
            this.groupBox3.Location = new System.Drawing.Point(22, 136);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(259, 90);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Descrifrar Clave:";
            // 
            // chkDescifrarPassword
            // 
            this.chkDescifrarPassword.AutoSize = true;
            this.chkDescifrarPassword.Location = new System.Drawing.Point(17, 28);
            this.chkDescifrarPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkDescifrarPassword.Name = "chkDescifrarPassword";
            this.chkDescifrarPassword.Size = new System.Drawing.Size(146, 20);
            this.chkDescifrarPassword.TabIndex = 4;
            this.chkDescifrarPassword.Text = "Descifrar Password";
            this.chkDescifrarPassword.UseVisualStyleBackColor = true;
            this.chkDescifrarPassword.CheckedChanged += new System.EventHandler(this.chkDescifrarPassword_CheckedChanged);
            // 
            // txtPasswordCifrada
            // 
            this.txtPasswordCifrada.Location = new System.Drawing.Point(8, 54);
            this.txtPasswordCifrada.Margin = new System.Windows.Forms.Padding(4);
            this.txtPasswordCifrada.Name = "txtPasswordCifrada";
            this.txtPasswordCifrada.ReadOnly = true;
            this.txtPasswordCifrada.Size = new System.Drawing.Size(241, 22);
            this.txtPasswordCifrada.TabIndex = 2;
            this.txtPasswordCifrada.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvABMUsuarioUsuarios);
            this.groupBox2.Location = new System.Drawing.Point(355, 40);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(688, 273);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Usuarios:";
            // 
            // dgvABMUsuarioUsuarios
            // 
            this.dgvABMUsuarioUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvABMUsuarioUsuarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvABMUsuarioUsuarios.Location = new System.Drawing.Point(4, 19);
            this.dgvABMUsuarioUsuarios.Margin = new System.Windows.Forms.Padding(4);
            this.dgvABMUsuarioUsuarios.Name = "dgvABMUsuarioUsuarios";
            this.dgvABMUsuarioUsuarios.RowHeadersWidth = 51;
            this.dgvABMUsuarioUsuarios.Size = new System.Drawing.Size(680, 250);
            this.dgvABMUsuarioUsuarios.TabIndex = 0;
            this.dgvABMUsuarioUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvABMUsuarioUsuarios_CellClick);
            // 
            // ABMUsuarioBtnExit
            // 
            this.ABMUsuarioBtnExit.Location = new System.Drawing.Point(943, 320);
            this.ABMUsuarioBtnExit.Margin = new System.Windows.Forms.Padding(4);
            this.ABMUsuarioBtnExit.Name = "ABMUsuarioBtnExit";
            this.ABMUsuarioBtnExit.Size = new System.Drawing.Size(100, 36);
            this.ABMUsuarioBtnExit.TabIndex = 11;
            this.ABMUsuarioBtnExit.Text = "Cerrar";
            this.ABMUsuarioBtnExit.UseVisualStyleBackColor = true;
            this.ABMUsuarioBtnExit.Click += new System.EventHandler(this.ABMUsuarioBtnExit_Click);
            // 
            // ABMUsuarioBtnEliminar
            // 
            this.ABMUsuarioBtnEliminar.Location = new System.Drawing.Point(837, 320);
            this.ABMUsuarioBtnEliminar.Margin = new System.Windows.Forms.Padding(4);
            this.ABMUsuarioBtnEliminar.Name = "ABMUsuarioBtnEliminar";
            this.ABMUsuarioBtnEliminar.Size = new System.Drawing.Size(100, 36);
            this.ABMUsuarioBtnEliminar.TabIndex = 10;
            this.ABMUsuarioBtnEliminar.Text = "Eliminar";
            this.ABMUsuarioBtnEliminar.UseVisualStyleBackColor = true;
            this.ABMUsuarioBtnEliminar.Click += new System.EventHandler(this.ABMUsuarioBtnEliminar_Click);
            // 
            // ABMUsuarioBtnModificar
            // 
            this.ABMUsuarioBtnModificar.Location = new System.Drawing.Point(729, 320);
            this.ABMUsuarioBtnModificar.Margin = new System.Windows.Forms.Padding(4);
            this.ABMUsuarioBtnModificar.Name = "ABMUsuarioBtnModificar";
            this.ABMUsuarioBtnModificar.Size = new System.Drawing.Size(100, 36);
            this.ABMUsuarioBtnModificar.TabIndex = 9;
            this.ABMUsuarioBtnModificar.Text = "Modificar";
            this.ABMUsuarioBtnModificar.UseVisualStyleBackColor = true;
            this.ABMUsuarioBtnModificar.Click += new System.EventHandler(this.ABMUsuarioBtnModificar_Click);
            // 
            // ABMUsuarioBtnAlta
            // 
            this.ABMUsuarioBtnAlta.Location = new System.Drawing.Point(621, 320);
            this.ABMUsuarioBtnAlta.Margin = new System.Windows.Forms.Padding(4);
            this.ABMUsuarioBtnAlta.Name = "ABMUsuarioBtnAlta";
            this.ABMUsuarioBtnAlta.Size = new System.Drawing.Size(100, 36);
            this.ABMUsuarioBtnAlta.TabIndex = 8;
            this.ABMUsuarioBtnAlta.Text = "Alta";
            this.ABMUsuarioBtnAlta.UseVisualStyleBackColor = true;
            this.ABMUsuarioBtnAlta.Click += new System.EventHandler(this.ABMUsuarioBtnAlta_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btnQuitarEmpleado);
            this.groupBox1.Controls.Add(this.txtEmpleadoActual);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.ABMUsuarioBtnLimpiar);
            this.groupBox1.Controls.Add(this.ABMUsuarioTxtBxPassword);
            this.groupBox1.Controls.Add(this.btnAsociarEmpleado);
            this.groupBox1.Controls.Add(this.ABMUsuarioTxtBxUsuario);
            this.groupBox1.Controls.Add(this.ABMUsuarioTxtBxId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(334, 364);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Usuario:";
            // 
            // txtEmpleadoActual
            // 
            this.txtEmpleadoActual.Location = new System.Drawing.Point(22, 294);
            this.txtEmpleadoActual.Name = "txtEmpleadoActual";
            this.txtEmpleadoActual.ReadOnly = true;
            this.txtEmpleadoActual.Size = new System.Drawing.Size(259, 22);
            this.txtEmpleadoActual.TabIndex = 15;
            // 
            // ABMUsuarioBtnLimpiar
            // 
            this.ABMUsuarioBtnLimpiar.Location = new System.Drawing.Point(22, 323);
            this.ABMUsuarioBtnLimpiar.Margin = new System.Windows.Forms.Padding(4);
            this.ABMUsuarioBtnLimpiar.Name = "ABMUsuarioBtnLimpiar";
            this.ABMUsuarioBtnLimpiar.Size = new System.Drawing.Size(149, 33);
            this.ABMUsuarioBtnLimpiar.TabIndex = 10;
            this.ABMUsuarioBtnLimpiar.Text = "Limpiar Campos";
            this.ABMUsuarioBtnLimpiar.UseVisualStyleBackColor = true;
            this.ABMUsuarioBtnLimpiar.Click += new System.EventHandler(this.ABMUsuarioBtnLimpiar_Click);
            // 
            // ABMUsuarioTxtBxPassword
            // 
            this.ABMUsuarioTxtBxPassword.Location = new System.Drawing.Point(109, 82);
            this.ABMUsuarioTxtBxPassword.Margin = new System.Windows.Forms.Padding(4);
            this.ABMUsuarioTxtBxPassword.Name = "ABMUsuarioTxtBxPassword";
            this.ABMUsuarioTxtBxPassword.Size = new System.Drawing.Size(132, 22);
            this.ABMUsuarioTxtBxPassword.TabIndex = 7;
            // 
            // btnAsociarEmpleado
            // 
            this.btnAsociarEmpleado.Location = new System.Drawing.Point(22, 234);
            this.btnAsociarEmpleado.Margin = new System.Windows.Forms.Padding(4);
            this.btnAsociarEmpleado.Name = "btnAsociarEmpleado";
            this.btnAsociarEmpleado.Size = new System.Drawing.Size(100, 48);
            this.btnAsociarEmpleado.TabIndex = 14;
            this.btnAsociarEmpleado.Text = "Asociar Empleado";
            this.btnAsociarEmpleado.UseVisualStyleBackColor = true;
            this.btnAsociarEmpleado.Click += new System.EventHandler(this.ABMUsuarioBtnBuscarEmpleado_Click);
            // 
            // ABMUsuarioTxtBxUsuario
            // 
            this.ABMUsuarioTxtBxUsuario.Location = new System.Drawing.Point(109, 53);
            this.ABMUsuarioTxtBxUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.ABMUsuarioTxtBxUsuario.Name = "ABMUsuarioTxtBxUsuario";
            this.ABMUsuarioTxtBxUsuario.Size = new System.Drawing.Size(132, 22);
            this.ABMUsuarioTxtBxUsuario.TabIndex = 6;
            // 
            // ABMUsuarioTxtBxId
            // 
            this.ABMUsuarioTxtBxId.Enabled = false;
            this.ABMUsuarioTxtBxId.Location = new System.Drawing.Point(109, 23);
            this.ABMUsuarioTxtBxId.Margin = new System.Windows.Forms.Padding(4);
            this.ABMUsuarioTxtBxId.Name = "ABMUsuarioTxtBxId";
            this.ABMUsuarioTxtBxId.Size = new System.Drawing.Size(132, 22);
            this.ABMUsuarioTxtBxId.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Usuario:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // btnQuitarEmpleado
            // 
            this.btnQuitarEmpleado.Location = new System.Drawing.Point(141, 234);
            this.btnQuitarEmpleado.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuitarEmpleado.Name = "btnQuitarEmpleado";
            this.btnQuitarEmpleado.Size = new System.Drawing.Size(100, 48);
            this.btnQuitarEmpleado.TabIndex = 16;
            this.btnQuitarEmpleado.Text = "Desasociar Empleado";
            this.btnQuitarEmpleado.UseVisualStyleBackColor = true;
            this.btnQuitarEmpleado.Click += new System.EventHandler(this.btnQuitarEmpleado_Click);
            // 
            // FrmUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 390);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ABMUsuarioBtnExit);
            this.Controls.Add(this.ABMUsuarioBtnEliminar);
            this.Controls.Add(this.ABMUsuarioBtnModificar);
            this.Controls.Add(this.ABMUsuarioBtnAlta);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmUsuario";
            this.Tag = "FrmUsuario";
            this.Text = "FrmUsuario";
            this.Load += new System.EventHandler(this.FrmUsuario_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvABMUsuarioUsuarios)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPasswordCifrada;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvABMUsuarioUsuarios;
        private System.Windows.Forms.Button ABMUsuarioBtnExit;
        private System.Windows.Forms.Button ABMUsuarioBtnEliminar;
        private System.Windows.Forms.Button ABMUsuarioBtnModificar;
        private System.Windows.Forms.Button ABMUsuarioBtnAlta;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAsociarEmpleado;
        private System.Windows.Forms.Button ABMUsuarioBtnLimpiar;
        private System.Windows.Forms.TextBox ABMUsuarioTxtBxPassword;
        private System.Windows.Forms.TextBox ABMUsuarioTxtBxUsuario;
        private System.Windows.Forms.TextBox ABMUsuarioTxtBxId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkDescifrarPassword;
        private System.Windows.Forms.TextBox txtEmpleadoActual;
        private System.Windows.Forms.Button btnQuitarEmpleado;
    }
}
