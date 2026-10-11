using System;
using System.Windows.Forms;
using BLL.Personas;
using BLL.Infra;
using BE.Personas;
using BE.Infra;
using Seguridad;
using System.Linq;

namespace GlobalTransLines.UI.Infra
{
    public partial class FrmUsuario : Form
    {
        #region "Atributos y Constructor"

        private readonly UsuarioBLL usuarioBLL = new UsuarioBLL();
        private readonly EmpleadoBLL empleadoBLL = new EmpleadoBLL();
        private BEEmpleado empleadoPendiente;
        private bool quitarEmpleadoPendiente;
        private BEUsuario usuarioSeleccionado;
        private string columnaOrdenUsuario = "Id";
        private bool ordenUsuarioAscendente = false;

        public FrmUsuario()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarVisual();
                ConfigurarGrilla();
                RefrescarGrilla();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void ABMUsuarioBtnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                var nuevo = MapearAEntidad();
                usuarioBLL.Guardar(nuevo);

                MessageBox.Show("Usuario creado correctamente.");

                RefrescarGrilla();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear usuario: " + ex.Message,
                    "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ABMUsuarioBtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un usuario de la lista.");
                    return;
                }

                var modificado = MapearAEntidad();

                long? empleadoIdSeleccionado = empleadoPendiente != null && empleadoPendiente.Id > 0
                    ? empleadoPendiente.Id
                    : (long?)null;

                usuarioBLL.GuardarDesdeFormulario(
                    modificado,
                    empleadoIdSeleccionado,
                    quitarEmpleadoPendiente);

                MessageBox.Show("Usuario modificado correctamente.");

                RefrescarGrilla();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar usuario: " + ex.Message);
            }
        }

        private void ABMUsuarioBtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un usuario.");
                    return;
                }

                usuarioBLL.Eliminar(usuarioSeleccionado);

                MessageBox.Show("Usuario eliminado correctamente.");

                RefrescarGrilla();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar usuario: " + ex.Message);
            }
        }

        private void ABMUsuarioBtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ABMUsuarioBtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        private void ABMUsuarioBtnBuscarEmpleado_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un usuario antes de asociar un empleado.");
                    return;
                }

                var frm = new FrmEmpleado();
                frm.ModoSeleccion = true;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.WindowState = FormWindowState.Maximized;

                var dr = frm.ShowDialog();

                if (dr == DialogResult.OK && frm.empleadoSeleccionado != null)
                {
                    empleadoPendiente = frm.empleadoSeleccionado;
                    quitarEmpleadoPendiente = false;
                    txtEmpleadoActual.Text = ObtenerDetalleEmpleado(empleadoPendiente);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al asociar empleado: " + ex.Message);
            }
        }
        private void btnQuitarEmpleado_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un usuario.");
                    return;
                }

                empleadoPendiente = null;
                quitarEmpleadoPendiente = true;
                txtEmpleadoActual.Text = "(sin empleado)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar empleado: " + ex.Message);
            }
        }



        private void chkDescifrarPassword_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Si no hay usuario seleccionado, no se descifra nada
                if (usuarioSeleccionado == null)
                {
                    chkDescifrarPassword.Checked = false;
                    txtPasswordCifrada.PasswordChar = '*';
                    txtPasswordCifrada.Text = string.Empty;
                    return;
                }

                // Si destilda: limpiar visor
                if (!chkDescifrarPassword.Checked)
                {
                    txtPasswordCifrada.PasswordChar = '*';
                    txtPasswordCifrada.Text = string.Empty;
                    return;
                }

                // Si tilda: descifrar SOLO visual
                string cifrada = usuarioSeleccionado.Password;

                if (string.IsNullOrWhiteSpace(cifrada))
                {
                    txtPasswordCifrada.PasswordChar = '*';
                    txtPasswordCifrada.Text = string.Empty;
                    return;
                }

                byte clave = usuarioSeleccionado.ClaveEncriptacion;

                // Usa tu clase real
                string plana = Encriptador.Desencriptar(cifrada, clave);

                txtPasswordCifrada.PasswordChar = '\0';
                txtPasswordCifrada.Text = plana;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo descifrar la clave: " + ex.Message,
                    "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Error);

                chkDescifrarPassword.Checked = false;
                txtPasswordCifrada.PasswordChar = '*';
                txtPasswordCifrada.Text = string.Empty;
            }
        }

        #endregion

        #region "Eventos de grilla"

        private void dgvABMUsuarioUsuarios_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = dgvABMUsuarioUsuarios.Columns[e.ColumnIndex].Name;

            if (columnaOrdenUsuario == columna)
                ordenUsuarioAscendente = !ordenUsuarioAscendente;
            else
            {
                columnaOrdenUsuario = columna;
                ordenUsuarioAscendente = true;
            }

            RefrescarGrilla();
        }

        private void dgvABMUsuarioUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                var item = dgvABMUsuarioUsuarios.Rows[e.RowIndex].Tag as BEUsuario;

                if (item == null)
                    return;

                usuarioSeleccionado = item;
                CargarEntidadEnControles(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar usuario: " + ex.Message);
            }
        }

        #endregion

        #region "Configuración Visual"

        private void ConfigurarVisual()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Resize -= FrmUsuario_Resize;
            this.Resize += FrmUsuario_Resize;

            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            ABMUsuarioBtnAlta.Anchor = AnchorStyles.Bottom;
            ABMUsuarioBtnModificar.Anchor = AnchorStyles.Bottom;
            ABMUsuarioBtnEliminar.Anchor = AnchorStyles.Bottom;
            ABMUsuarioBtnExit.Anchor = AnchorStyles.Bottom;

            txtPasswordCifrada.PasswordChar = '*';
            txtEmpleadoActual.Text = "(sin empleado)";

            AjustarDistribucion();
        }

        private void FrmUsuario_Resize(object sender, EventArgs e)
        {
            AjustarDistribucion();
        }

        private void AjustarDistribucion()
        {
            int margen = 13;
            int separacion = 12;
            int anchoPanel = 334;

            groupBox1.SetBounds(margen, margen, anchoPanel, Math.Max(360, ClientSize.Height - margen * 2));

            int xGrilla = groupBox1.Right + separacion;
            int anchoGrilla = Math.Max(500, ClientSize.Width - xGrilla - margen);
            int altoGrilla = Math.Max(240, ClientSize.Height - margen - 70);

            groupBox2.SetBounds(xGrilla, margen, anchoGrilla, altoGrilla);

            int anchoBoton = 110;
            int altoBoton = 33;
            int yBotones = groupBox2.Bottom + 10;
            int anchoBotonera = anchoBoton * 4 + separacion * 3;
            int xBotones = groupBox2.Left + Math.Max(0, (groupBox2.Width - anchoBotonera) / 2);

            ABMUsuarioBtnAlta.SetBounds(xBotones, yBotones, anchoBoton, altoBoton);
            ABMUsuarioBtnModificar.SetBounds(ABMUsuarioBtnAlta.Right + separacion, yBotones, anchoBoton, altoBoton);
            ABMUsuarioBtnEliminar.SetBounds(ABMUsuarioBtnModificar.Right + separacion, yBotones, anchoBoton, altoBoton);
            ABMUsuarioBtnExit.SetBounds(ABMUsuarioBtnEliminar.Right + separacion, yBotones, anchoBoton, altoBoton);
        }

        private void ConfigurarGrilla()
        {
            dgvABMUsuarioUsuarios.AutoGenerateColumns = false;
            dgvABMUsuarioUsuarios.Columns.Clear();
            dgvABMUsuarioUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvABMUsuarioUsuarios.MultiSelect = false;
            dgvABMUsuarioUsuarios.ReadOnly = true;
            dgvABMUsuarioUsuarios.RowHeadersVisible = false;
            dgvABMUsuarioUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvABMUsuarioUsuarios.AllowUserToAddRows = false;
            dgvABMUsuarioUsuarios.AllowUserToDeleteRows = false;

            dgvABMUsuarioUsuarios.Columns.Add("Id", "ID");
            dgvABMUsuarioUsuarios.Columns["Id"].Width = 55;
            dgvABMUsuarioUsuarios.Columns["Id"].MinimumWidth = 55;
            dgvABMUsuarioUsuarios.Columns["Id"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgvABMUsuarioUsuarios.Columns.Add("Username", "Usuario");
            dgvABMUsuarioUsuarios.Columns["Username"].Width = 190;
            dgvABMUsuarioUsuarios.Columns["Username"].MinimumWidth = 150;
            dgvABMUsuarioUsuarios.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgvABMUsuarioUsuarios.Columns.Add("Empleado", "Empleado");
            dgvABMUsuarioUsuarios.Columns["Empleado"].Width = 280;
            dgvABMUsuarioUsuarios.Columns["Empleado"].MinimumWidth = 220;
            dgvABMUsuarioUsuarios.Columns["Empleado"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgvABMUsuarioUsuarios.Columns.Add("Cargo", "Cargo");
            dgvABMUsuarioUsuarios.Columns["Cargo"].Width = 320;
            dgvABMUsuarioUsuarios.Columns["Cargo"].MinimumWidth = 220;
            dgvABMUsuarioUsuarios.Columns["Cargo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            dgvABMUsuarioUsuarios.Columns.Add("Email", "Email");
            dgvABMUsuarioUsuarios.Columns["Email"].Width = 420;
            dgvABMUsuarioUsuarios.Columns["Email"].MinimumWidth = 260;
            dgvABMUsuarioUsuarios.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            foreach (DataGridViewColumn columna in dgvABMUsuarioUsuarios.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            dgvABMUsuarioUsuarios.ColumnHeaderMouseClick -= dgvABMUsuarioUsuarios_ColumnHeaderMouseClick;
            dgvABMUsuarioUsuarios.ColumnHeaderMouseClick += dgvABMUsuarioUsuarios_ColumnHeaderMouseClick;

            dgvABMUsuarioUsuarios.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            dgvABMUsuarioUsuarios.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
        }

        #endregion

        #region "Métodos auxiliares"

        private void RefrescarGrilla()
        {
            dgvABMUsuarioUsuarios.Rows.Clear();

            var lista = OrdenarUsuarios(usuarioBLL.ListarTodo());

            foreach (var u in lista)
            {
                var empleado = u.Empleado;

                int fila = dgvABMUsuarioUsuarios.Rows.Add(
                    u.Id,
                    u.Username,
                    ObtenerNombreEmpleado(empleado),
                    ObtenerCargoEmpleado(empleado),
                    empleado != null ? empleado.Email : ""
                );

                dgvABMUsuarioUsuarios.Rows[fila].Tag = u;
            }
        }

        private System.Collections.Generic.List<BEUsuario> OrdenarUsuarios(System.Collections.Generic.List<BEUsuario> usuarios)
        {
            Func<BEUsuario, object> criterio;

            switch (columnaOrdenUsuario)
            {
                case "Username":
                    criterio = u => u.Username ?? string.Empty;
                    break;
                case "Empleado":
                    criterio = u => u.Empleado != null
                        ? (u.Empleado.Apellido + ", " + u.Empleado.Nombre)
                        : "(sin empleado)";
                    break;
                case "Cargo":
                    criterio = u => ObtenerCargoEmpleado(u.Empleado);
                    break;
                case "Email":
                    criterio = u => u.Empleado != null ? (u.Empleado.Email ?? string.Empty) : string.Empty;
                    break;
                default:
                    criterio = u => u.Id;
                    break;
            }

            return ordenUsuarioAscendente
                ? usuarios.OrderBy(criterio).ToList()
                : usuarios.OrderByDescending(criterio).ToList();
        }

        private void LimpiarControles()
        {
            usuarioSeleccionado = null;
            empleadoPendiente = null;
            quitarEmpleadoPendiente = false;

            ABMUsuarioTxtBxId.Text = "";
            ABMUsuarioTxtBxUsuario.Text = "";
            ABMUsuarioTxtBxPassword.Text = "";

            txtPasswordCifrada.Text = "";
            txtEmpleadoActual.Text = "(sin empleado)";

            chkDescifrarPassword.Checked = false;
        }

        private BEUsuario MapearAEntidad()
        {
            var u = new BEUsuario();

            if (usuarioSeleccionado != null)
            {
                u.Id = usuarioSeleccionado.Id;
            }
            else
            {
                u.Activo = true;
            }

            u.Username = ABMUsuarioTxtBxUsuario.Text.Trim();
            u.Password = ABMUsuarioTxtBxPassword.Text; 

            return u;
        }



        private void CargarEntidadEnControles(BEUsuario u)
        {
            ABMUsuarioTxtBxId.Text = u.Id.ToString();
            ABMUsuarioTxtBxUsuario.Text = u.Username;

            ABMUsuarioTxtBxPassword.Text = "";

            // Mostrar cifrada por defecto
            txtPasswordCifrada.PasswordChar = '*';
            txtPasswordCifrada.Text = u.Password ?? string.Empty;

            // El check siempre arranca destildado
            chkDescifrarPassword.Checked = false;

            empleadoPendiente = u.Empleado;
            quitarEmpleadoPendiente = false;
            txtEmpleadoActual.Text = ObtenerDetalleEmpleado(empleadoPendiente);
        }

        private string ObtenerNombreEmpleado(BEEmpleado empleado)
        {
            if (empleado == null || empleado.Id <= 0)
                return "(sin empleado)";

            string nombreCompleto = (empleado.Apellido + ", " + empleado.Nombre).Trim(' ', ',');

            return string.IsNullOrWhiteSpace(nombreCompleto)
                ? "Empleado ID: " + empleado.Id
                : nombreCompleto;
        }

        private string ObtenerCargoEmpleado(BEEmpleado empleado)
        {
            return empleado != null && empleado.cargoEmpleado != null
                ? empleado.cargoEmpleado.ToString().Trim()
                : "";
        }

        private string ObtenerDetalleEmpleado(BEEmpleado empleado)
        {
            if (empleado == null || empleado.Id <= 0)
                return "(sin empleado)";

            string detalle = ObtenerNombreEmpleado(empleado);
            string cargo = ObtenerCargoEmpleado(empleado);

            if (!string.IsNullOrWhiteSpace(cargo))
                detalle += " | " + cargo;

            return detalle;
        }



        #endregion


    }
}
