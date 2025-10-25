using System;
using System.Windows.Forms;
using BLL.Transversal;
using BE.Transversal;

namespace GlobalTransLines.Transversales
{
    public partial class FrmLogin : Form
    {
        private SeguridadBLL seguridadBLL = new SeguridadBLL();

        public FrmLogin()
        {
            InitializeComponent();
        }

        #region "Eventos del formulario"

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                usuarioBLL.InicializarSistema();

                ckBxLoginMostrarPassword.CheckedChanged += (s, ev) =>
                {
                    loginTxtBxPassword.UseSystemPasswordChar = !ckBxLoginMostrarPassword.Checked;
                };

                loginBtnLogin.Click += loginBtnLogin_Click;
                loginBtnExit.Click += loginBtnExit_Click;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar el sistema: " + ex.Message,
                                "Error de inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                if (seguridadBLL.EstaAutenticado)
                {
                    MessageBox.Show("Bienvenido, " + usuarioAutenticado.Username + "!",
                                    "Acceso concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MenuPrincipal menu = new MenuPrincipal(usuarioAutenticado, seguridadBLL);
                    this.Hide();
                    menu.ShowDialog();
                    this.Show();
                }
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
    }
}
