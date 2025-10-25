using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE.Transversal;
using BLL.Transversal;
using BE.Enums;
using GlobalTransLines.UI.Gestion1_Booking;
using GlobalTransLines.UI.Gestion2_Transporte;
using GlobalTransLines.UI.Gestion3_Facturacion;
using GlobalTransLines.UI.Gestion4_Proveedores;

namespace GlobalTransLines
{
    public partial class MenuPrincipal : Form
    {
        #region "Atributos y Constructor"

        private BEUsuario usuarioActivo;
        private SeguridadBLL seguridadBLL;
        private BitacoraBLL bitacoraBLL = new BitacoraBLL();

        public MenuPrincipal(BEUsuario usuario, SeguridadBLL seguridad)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            usuarioActivo = usuario;
            seguridadBLL = seguridad;

            Text = "GlobalTransLines ▪ Usuario: " + usuario.Username + " ▪ Rol: " + usuario.Rol.Nombre;

            logoutToolStripMenuItem.Click += logoutToolStripMenuItem_Click;
            salirToolStripMenuItem.Click += salirToolStripMenuItem_Click;
            this.Load += MenuPrincipal_Load;
        }

        #endregion

        #region "Eventos del formulario"

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                // 🧠 Registrar en bitácora el inicio de sesión visual
                bitacoraBLL.RegistrarEvento(
                    usuarioActivo,
                    TipoOperacionEnum.Informativo,
                    "Inicio de sesión visual. Aplicando permisos al menú principal.",
                    "Transversal/UI");

                // Mensaje opcional una sola vez
                if (usuarioActivo.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    MessageBox.Show("Usuario administrador detectado. Permisos totales habilitados.");

                // Aplicar permisos dinámicamente
                var visitados = new HashSet<ToolStripMenuItem>();
                AplicarPermisosMenu(menuStrip1, visitados);

                // Registrar éxito en Bitácora
                bitacoraBLL.RegistrarEvento(
                    usuarioActivo,
                    TipoOperacionEnum.Informativo,
                    "Permisos aplicados correctamente al menú principal.",
                    "Transversal/UI");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar menú principal: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Aplicar permisos dinámicos"

        private void AplicarPermisosMenu(MenuStrip menu, HashSet<ToolStripMenuItem> visitados)
        {
            foreach (ToolStripMenuItem item in menu.Items)
                ValidarItem(item, visitados);
        }

        private void ValidarItem(ToolStripMenuItem item, HashSet<ToolStripMenuItem> visitados)
        {
            if (visitados.Contains(item))
                return;

            visitados.Add(item);
            bool visible = false;

            // Recorrer submenús
            foreach (ToolStripMenuItem subItem in item.DropDownItems)
            {
                if (object.ReferenceEquals(subItem, item))
                    continue;

                ValidarItem(subItem, visitados);
                if (subItem.Visible)
                    visible = true;
            }

            // Validar permiso propio
            if (item.Tag != null)
            {
                bool permisoPropio = false;
                try
                {
                    permisoPropio = seguridadBLL.TienePermiso(item.Tag.ToString());
                }
                catch (Exception ex)
                {
                    bitacoraBLL.RegistrarEvento(
                        usuarioActivo,
                        TipoOperacionEnum.Error,
                        "Error validando permiso para " + item.Tag + ": " + ex.Message,
                        "Transversal/UI");
                }

                visible = visible || permisoPropio;
            }

            // Superpermiso admin
            if (usuarioActivo.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                visible = true;

            item.Visible = visible;
        }

        #endregion

        #region "Abrir formularios"

        private void AbrirFormulario(Form form)
        {
            foreach (Form f in MdiChildren)
            {
                if (f.GetType() == form.GetType())
                {
                    f.Activate();
                    return;
                }
            }

            form.MdiParent = this;
            form.Show();

            // Registrar apertura en Bitácora
            bitacoraBLL.RegistrarEvento(
                usuarioActivo,
                TipoOperacionEnum.Operacion,
                "Apertura de formulario: " + form.GetType().Name,
                "Transversal/UI");
        }

        #endregion

        #region "Eventos de menú"

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                seguridadBLL.Logout();
                bitacoraBLL.RegistrarEvento(
                    usuarioActivo,
                    TipoOperacionEnum.Logout,
                    "Cierre de sesión desde menú principal.",
                    "Transversal/UI");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar sesión: " + ex.Message);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Ejemplos de eventos para abrir formularios
        private void frmClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!seguridadBLL.TienePermiso("FrmCliente"))
            {
                MessageBox.Show("Acceso denegado a Clientes.", "Permiso insuficiente");
                bitacoraBLL.RegistrarEvento(
                    usuarioActivo,
                    TipoOperacionEnum.Error,
                    "Intento de acceso sin permiso al formulario Clientes.",
                    "Transversal/UI");
                return;
            }

            AbrirFormulario(new FrmCliente());
        }

        private void frmBookingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!seguridadBLL.TienePermiso("FrmBooking"))
            {
                MessageBox.Show("Acceso denegado a Booking.", "Permiso insuficiente");
                bitacoraBLL.RegistrarEvento(
                    usuarioActivo,
                    TipoOperacionEnum.Error,
                    "Intento de acceso sin permiso al formulario Booking.",
                    "Transversal/UI");
                return;
            }

           AbrirFormulario(new FrmBooking());
        }

        private void frmFacturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!seguridadBLL.TienePermiso("FrmFactura"))
            {
                MessageBox.Show("Acceso denegado a Facturación.", "Permiso insuficiente");
                bitacoraBLL.RegistrarEvento(
                    usuarioActivo,
                    TipoOperacionEnum.Error,
                    "Intento de acceso sin permiso al formulario Factura.",
                    "Transversal/UI");
                return;
            }

            AbrirFormulario(new FrmFactura());
        }

        private void frmPagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!seguridadBLL.TienePermiso("FrmPago"))
            {
                MessageBox.Show("Acceso denegado a Pagos.", "Permiso insuficiente");
                bitacoraBLL.RegistrarEvento(
                    usuarioActivo,
                    TipoOperacionEnum.Error,
                    "Intento de acceso sin permiso al formulario Pago.",
                    "Transversal/UI");
                return;
            }

           // AbrirFormulario(new FmrPago());
        }

        #endregion
    }
}
