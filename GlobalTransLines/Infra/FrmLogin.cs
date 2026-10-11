using BE.Infra;
using BLL.Infra;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Infra
{
    public partial class FrmLogin : Form
    {
        private readonly SeguridadBLL seguridadBLL = new SeguridadBLL();

        public FrmLogin()
        {
            InitializeComponent();

            loginTxtBxPassword.UseSystemPasswordChar = true;
            AplicarEstiloVisual();
        }

        #region "Eventos del formulario"

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                usuarioBLL.InicializarSistema();
                this.AcceptButton = loginBtnLogin;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar el sistema: " + ex.Message,
                                "Error de inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ckBxLoginMostrarPassword_CheckedChanged(object sender, EventArgs e)
        {
            loginTxtBxPassword.UseSystemPasswordChar = !ckBxLoginMostrarPassword.Checked;
        }

        private void loginBtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = loginTxtBxUsuario.Text.Trim();
                string password = loginTxtBxPassword.Text.Trim();

                if (username == string.Empty || password == string.Empty)
                {
                    MessageBox.Show("Debe ingresar usuario y contraseña.",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BEUsuario usuarioAutenticado = seguridadBLL.Login(username, password);

                if (!seguridadBLL.EstaAutenticado || usuarioAutenticado == null)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.",
                                    "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    loginTxtBxPassword.Clear();
                    loginTxtBxPassword.Focus();
                    return;
                }

                MessageBox.Show("Bienvenido, " + usuarioAutenticado.Username + "!",
                                "Acceso concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                using (MenuPrincipal menu = new MenuPrincipal(usuarioAutenticado, seguridadBLL))
                {
                    this.Hide();
                    menu.ShowDialog();
                    this.Show();
                }

                // Limpieza al volver
                loginTxtBxPassword.Clear();
                loginTxtBxUsuario.Clear();
                loginTxtBxUsuario.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de autenticación",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loginBtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region "Estilo visual"

        private void AplicarEstiloVisual()
        {
            Color azulPrincipal = Color.FromArgb(8, 45, 91);
            Color azulSeleccion = Color.FromArgb(16, 92, 160);
            Color fondoPanel = Color.FromArgb(245, 248, 252);

            Text = "GlobalTransLines | Acceso al sistema";
            ClientSize = new Size(920, 560);
            MinimumSize = new Size(760, 500);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            BackColor = Color.White;

            string rutaFondo = Path.Combine(Application.StartupPath, "Resources", "Fondo.png");
            if (File.Exists(rutaFondo))
            {
                BackgroundImage = Image.FromFile(rutaFondo);
                BackgroundImageLayout = ImageLayout.Stretch;
            }

            groupBox1.Text = string.Empty;
            groupBox1.BackColor = fondoPanel;
            groupBox1.ForeColor = azulPrincipal;
            groupBox1.Size = new Size(360, 350);
            groupBox1.Location = new Point(48, 94);
            groupBox1.Padding = new Padding(18);

            pictureBox1.Visible = false;

            Label lblTitulo = new Label();
            lblTitulo.AutoSize = false;
            lblTitulo.Text = "GlobalTransLines";
            lblTitulo.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            lblTitulo.ForeColor = azulPrincipal;
            lblTitulo.Location = new Point(32, 30);
            lblTitulo.Size = new Size(295, 44);
            groupBox1.Controls.Add(lblTitulo);

            Label lblSubtitulo = new Label();
            lblSubtitulo.AutoSize = false;
            lblSubtitulo.Text = "Sistema de gestion logistica";
            lblSubtitulo.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lblSubtitulo.ForeColor = Color.DimGray;
            lblSubtitulo.Location = new Point(35, 74);
            lblSubtitulo.Size = new Size(285, 26);
            groupBox1.Controls.Add(lblSubtitulo);

            label1.Text = "Usuario";
            label1.ForeColor = azulPrincipal;
            label1.Location = new Point(35, 122);

            loginTxtBxUsuario.Location = new Point(35, 145);
            loginTxtBxUsuario.Size = new Size(285, 24);

            label2.Text = "Clave";
            label2.ForeColor = azulPrincipal;
            label2.Location = new Point(35, 182);

            loginTxtBxPassword.Location = new Point(35, 205);
            loginTxtBxPassword.Size = new Size(285, 24);

            ckBxLoginMostrarPassword.Text = "Mostrar clave";
            ckBxLoginMostrarPassword.ForeColor = azulPrincipal;
            ckBxLoginMostrarPassword.BackColor = fondoPanel;
            ckBxLoginMostrarPassword.Location = new Point(35, 238);

            loginBtnLogin.Text = "Ingresar";
            loginBtnLogin.Location = new Point(35, 285);
            loginBtnLogin.Size = new Size(135, 38);
            AplicarEstiloBotonPrincipal(loginBtnLogin, azulSeleccion);

            loginBtnExit.Text = "Salir";
            loginBtnExit.Location = new Point(185, 285);
            loginBtnExit.Size = new Size(135, 38);
            AplicarEstiloBotonSecundario(loginBtnExit, azulPrincipal);
        }

        private void AplicarEstiloBotonPrincipal(Button boton, Color color)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.BackColor = color;
            boton.ForeColor = Color.White;
            boton.FlatAppearance.BorderColor = color;
            boton.FlatAppearance.BorderSize = 1;
            boton.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        }

        private void AplicarEstiloBotonSecundario(Button boton, Color color)
        {
            boton.FlatStyle = FlatStyle.Flat;
            boton.BackColor = Color.White;
            boton.ForeColor = color;
            boton.FlatAppearance.BorderColor = Color.FromArgb(190, 205, 220);
            boton.FlatAppearance.BorderSize = 1;
            boton.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        }

        #endregion
    }
}
