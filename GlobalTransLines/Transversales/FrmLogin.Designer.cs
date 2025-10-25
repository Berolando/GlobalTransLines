namespace GlobalTransLines.Transversales
{
    partial class FrmLogin
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ckBxLoginMostrarPassword = new System.Windows.Forms.CheckBox();
            this.loginBtnExit = new System.Windows.Forms.Button();
            this.loginBtnLogin = new System.Windows.Forms.Button();
            this.loginTxtBxPassword = new System.Windows.Forms.TextBox();
            this.loginTxtBxUsuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckBxLoginMostrarPassword);
            this.groupBox1.Controls.Add(this.loginBtnExit);
            this.groupBox1.Controls.Add(this.loginBtnLogin);
            this.groupBox1.Controls.Add(this.loginTxtBxPassword);
            this.groupBox1.Controls.Add(this.loginTxtBxUsuario);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(383, 433);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GlobalTransLines.UI.Properties.Resources.Logo_1;
            this.pictureBox1.Location = new System.Drawing.Point(7, 8);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(372, 409);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // ckBxLoginMostrarPassword
            // 
            this.ckBxLoginMostrarPassword.AutoSize = true;
            this.ckBxLoginMostrarPassword.Location = new System.Drawing.Point(147, 84);
            this.ckBxLoginMostrarPassword.Margin = new System.Windows.Forms.Padding(4);
            this.ckBxLoginMostrarPassword.Name = "ckBxLoginMostrarPassword";
            this.ckBxLoginMostrarPassword.Size = new System.Drawing.Size(146, 20);
            this.ckBxLoginMostrarPassword.TabIndex = 6;
            this.ckBxLoginMostrarPassword.Text = "Mostrar Contraseña";
            this.ckBxLoginMostrarPassword.UseVisualStyleBackColor = true;
            // 
            // loginBtnExit
            // 
            this.loginBtnExit.Location = new System.Drawing.Point(246, 348);
            this.loginBtnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loginBtnExit.Name = "loginBtnExit";
            this.loginBtnExit.Size = new System.Drawing.Size(104, 48);
            this.loginBtnExit.TabIndex = 5;
            this.loginBtnExit.Text = "Exit";
            this.loginBtnExit.UseVisualStyleBackColor = true;
            this.loginBtnExit.Click += new System.EventHandler(this.loginBtnExit_Click);
            // 
            // loginBtnLogin
            // 
            this.loginBtnLogin.Location = new System.Drawing.Point(73, 348);
            this.loginBtnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loginBtnLogin.Name = "loginBtnLogin";
            this.loginBtnLogin.Size = new System.Drawing.Size(104, 48);
            this.loginBtnLogin.TabIndex = 4;
            this.loginBtnLogin.Text = "Login";
            this.loginBtnLogin.UseVisualStyleBackColor = true;
            this.loginBtnLogin.Click += new System.EventHandler(this.loginBtnLogin_Click);
            // 
            // loginTxtBxPassword
            // 
            this.loginTxtBxPassword.Location = new System.Drawing.Point(124, 56);
            this.loginTxtBxPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loginTxtBxPassword.Name = "loginTxtBxPassword";
            this.loginTxtBxPassword.Size = new System.Drawing.Size(169, 22);
            this.loginTxtBxPassword.TabIndex = 3;
            this.loginTxtBxPassword.UseSystemPasswordChar = true;
            // 
            // loginTxtBxUsuario
            // 
            this.loginTxtBxUsuario.Location = new System.Drawing.Point(124, 30);
            this.loginTxtBxUsuario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loginTxtBxUsuario.Name = "loginTxtBxUsuario";
            this.loginTxtBxUsuario.Size = new System.Drawing.Size(169, 22);
            this.loginTxtBxUsuario.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario:";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 454);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmLogin";
            this.Text = "FrmLogin";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox ckBxLoginMostrarPassword;
        private System.Windows.Forms.Button loginBtnExit;
        private System.Windows.Forms.Button loginBtnLogin;
        private System.Windows.Forms.TextBox loginTxtBxPassword;
        private System.Windows.Forms.TextBox loginTxtBxUsuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}