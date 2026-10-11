using BE.Dominio;
using BE.Infra;
using BLL.Infra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Infra
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
            InicializarTagsMenu();

            // Optimiza el repintado del formulario MDI principal.
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            usuarioActivo = usuario;
            seguridadBLL = seguridad;

            string rolesTxt = (usuario.Roles != null && usuario.Roles.Count > 0)
                ? string.Join(", ", usuario.Roles.Where(r => r != null).Select(r => r.Nombre))
                : "(sin roles)";

            this.Text = "GlobalTransLines | Usuario: " + usuario.Username +
                        " | Roles: " + rolesTxt;
        }

        #endregion

        #region "Eventos del formulario"

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                InicializadorSistema.InicializarReconstructores();
                AplicarEstiloVisual();

                // Fondo corporativo del escritorio principal.
                string ruta = Path.Combine(Application.StartupPath, "Resources", "Fondo.png");
                if (File.Exists(ruta))
                {
                    this.BackgroundImage = Image.FromFile(ruta);
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }

                bitacoraBLL.RegistrarEvento(
                    usuarioActivo,
                    TipoOperacion.Informativo,
                    "Inicio de sesion visual. Aplicando permisos al menu.",
                    "Infra/UI");

                AplicarPermisosMenu(menuStrip1);

                bitacoraBLL.RegistrarEvento(
                    usuarioActivo,
                    TipoOperacion.Informativo,
                    "Permisos aplicados correctamente al menu principal.",
                    "Infra/UI");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar menu: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Estilo visual"

        private void AplicarEstiloVisual()
        {
            Color azulPrincipal = Color.FromArgb(8, 45, 91);
            Color azulSeleccion = Color.FromArgb(16, 92, 160);
            Color fondoMenu = Color.FromArgb(245, 248, 252);

            BackColor = Color.White;
            menuStrip1.BackColor = fondoMenu;
            menuStrip1.ForeColor = azulPrincipal;
            menuStrip1.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Renderer = new MenuGlobalRenderer(azulPrincipal, azulSeleccion, fondoMenu);

            foreach (ToolStripMenuItem item in menuStrip1.Items)
                AplicarEstiloItemMenu(item, azulPrincipal);
        }

        private void AplicarEstiloItemMenu(ToolStripMenuItem item, Color colorTexto)
        {
            item.ForeColor = colorTexto;
            item.BackColor = Color.Transparent;

            foreach (ToolStripItem child in item.DropDownItems)
            {
                child.ForeColor = colorTexto;
                child.BackColor = Color.White;

                ToolStripMenuItem childMenu = child as ToolStripMenuItem;
                if (childMenu != null)
                    AplicarEstiloItemMenu(childMenu, colorTexto);
            }
        }

        private class MenuGlobalRenderer : ToolStripProfessionalRenderer
        {
            public MenuGlobalRenderer(Color azulPrincipal, Color azulSeleccion, Color fondoMenu)
                : base(new MenuGlobalColorTable(azulPrincipal, azulSeleccion, fondoMenu))
            {
            }
        }

        private class MenuGlobalColorTable : ProfessionalColorTable
        {
            private readonly Color azulPrincipal;
            private readonly Color azulSeleccion;
            private readonly Color fondoMenu;

            public MenuGlobalColorTable(Color azulPrincipal, Color azulSeleccion, Color fondoMenu)
            {
                this.azulPrincipal = azulPrincipal;
                this.azulSeleccion = azulSeleccion;
                this.fondoMenu = fondoMenu;
                UseSystemColors = false;
            }

            public override Color MenuStripGradientBegin => fondoMenu;
            public override Color MenuStripGradientEnd => fondoMenu;
            public override Color ToolStripDropDownBackground => Color.White;
            public override Color ImageMarginGradientBegin => Color.White;
            public override Color ImageMarginGradientMiddle => Color.White;
            public override Color ImageMarginGradientEnd => Color.White;
            public override Color MenuItemSelected => Color.FromArgb(225, 238, 249);
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(225, 238, 249);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(225, 238, 249);
            public override Color MenuItemBorder => azulSeleccion;
            public override Color MenuItemPressedGradientBegin => azulPrincipal;
            public override Color MenuItemPressedGradientEnd => azulPrincipal;
        }

        #endregion

        #region "Permisos dinamicos"

        private void AplicarPermisosMenu(MenuStrip menu)
        {
            foreach (ToolStripItem tsi in menu.Items)
            {
                var item = tsi as ToolStripMenuItem;
                if (item == null) continue;

                ValidarItem(item);
            }
        }

        private bool ValidarItem(ToolStripMenuItem item)
        {
            // Los comandos base del sistema se mantienen siempre visibles.
            if (item.Name == "inicioToolStripMenuItem" ||
                item.Name == "logoutToolStripMenuItem" ||
                item.Name == "salirToolStripMenuItem")
            {
                item.Visible = true;
                return true;
            }

            bool permisoPropio = false;

            // El permiso del item se evalua solo cuando tiene una clave Tag asignada.
            string tag = item.Tag == null ? null : item.Tag.ToString();
            if (!string.IsNullOrWhiteSpace(tag))
                permisoPropio = seguridadBLL.TienePermiso(tag);

            bool algunHijoHabilitado = false;

            foreach (ToolStripItem tsi in item.DropDownItems)
            {
                var sub = tsi as ToolStripMenuItem;
                if (sub == null) continue;

                // El resultado logico evita ocultar un menu padre con hijos permitidos.
                if (ValidarItem(sub))
                    algunHijoHabilitado = true;
            }

            bool habilitado = permisoPropio || algunHijoHabilitado;

            item.Visible = habilitado;

            return habilitado;
        }

        #endregion

        #region "Apertura MDI generica"

        private void AbrirFormularioMDI(string nombreFormulario)
        {
            try
            {
                var ensamblado = typeof(MenuPrincipal).Assembly;

                // Si el formulario ya esta abierto, se reutiliza la instancia existente.
                foreach (Form f in MdiChildren)
                {
                    if (f.GetType().Name == nombreFormulario)
                    {
                        f.WindowState = FormWindowState.Maximized;
                        f.BringToFront();
                        f.Activate();
                        return;
                    }
                }

                Type tipo = null;

                tipo = ensamblado.GetType("GlobalTransLines.UI.Infra." + nombreFormulario);
                if (tipo == null) tipo = ensamblado.GetType("GlobalTransLines.UI.Gestion1_Booking." + nombreFormulario);
                if (tipo == null) tipo = ensamblado.GetType("GlobalTransLines.UI.Gestion2_Transporte." + nombreFormulario);
                if (tipo == null) tipo = ensamblado.GetType("GlobalTransLines.UI.Gestion3_Facturacion." + nombreFormulario);
                if (tipo == null) tipo = ensamblado.GetType("GlobalTransLines.UI.Gestion4_Proveedores." + nombreFormulario);
                if (tipo == null) tipo = ensamblado.GetType("GlobalTransLines.UI.Reportes." + nombreFormulario);
                if (tipo == null) tipo = ensamblado.GetType("GlobalTransLines.UI." + nombreFormulario);

                if (tipo == null)
                    throw new Exception("No se encontro el formulario: " + nombreFormulario);

                Form instancia = null;

                var ctor2 = tipo.GetConstructor(new Type[] { typeof(BEUsuario), typeof(SeguridadBLL) });
                if (ctor2 != null)
                {
                    instancia = (Form)ctor2.Invoke(new object[] { usuarioActivo, seguridadBLL });
                }
                else
                {
                    var ctor1 = tipo.GetConstructor(new Type[] { typeof(SeguridadBLL) });
                    if (ctor1 != null)
                    {
                        instancia = (Form)ctor1.Invoke(new object[] { seguridadBLL });
                    }
                    else
                    {
                        instancia = Activator.CreateInstance(tipo) as Form;
                    }
                }

                instancia.MdiParent = this;
                instancia.StartPosition = FormStartPosition.Manual;
                instancia.WindowState = FormWindowState.Normal;
                instancia.Show();
                MaximizarHijoMdi(instancia);
                instancia.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MaximizarHijoMdi(Form formulario)
        {
            if (formulario == null)
                return;

            formulario.WindowState = FormWindowState.Maximized;

            BeginInvoke(new Action(() =>
            {
                if (!formulario.IsDisposed)
                {
                    formulario.WindowState = FormWindowState.Maximized;
                    formulario.BringToFront();
                    formulario.Activate();
                }
            }));
        }

        #endregion

        #region "Eventos del menu"

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                seguridadBLL.Logout();

                bitacoraBLL.RegistrarEvento(
                    usuarioActivo,
                    TipoOperacion.Logout,
                    "Cierre de sesion desde menu principal.",
                    "Infra/UI");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar sesion: " + ex.Message);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Gestion 1
        private void frmClienteToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void cargaToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void frmBookingToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void listarCotizacionesToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void listarCotizacionToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void tarifarioToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        // Gestion 2
        private void asignacionBuqueToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void aBMContenedoresToolStripMenuItem1_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void aBMBuquesToolStripMenuItem1_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());
        private void billOfLadToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void manifiestoToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        // Gestion 3
        private void frmFacturaToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void frmPagoToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        // Gestion 4
        private void proveedorToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void ServiciosToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void ordenDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void recepcionDeServiciosToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void pagoAProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        // Dashboards
        private void dashBoardPrincipalToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void reportesPorGestionToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        // Transversales
        private void empleadosToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        // Seguridad
        private void rolesYPermisosToolStripMenuItem_Click(object sender, EventArgs e)
             => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());


        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
            => AbrirFormularioMDI(((ToolStripMenuItem)sender).Tag.ToString());

        #endregion

        #region "Inicializar Tags"
        private void InicializarTagsMenu()
        {
            aMBClienteToolStripMenuItem.Tag = "FrmCliente";
            cargaToolStripMenuItem.Tag = "FrmCarga";
            NuevaCotizacionToolStripMenuItem.Tag = "FrmCotizacion";
            listarCotizacionToolStripMenuItem.Tag = "FrmCotizaciones";
            tarifarioToolStripMenuItem.Tag = "FrmTarifario";
            bookingToolStripMenuItem.Tag = "FrmBooking";

            asignacionBuqueToolStripMenuItem.Tag = "FrmAsignarBuqueContenedor";
            billOfLadToolStripMenuItem.Tag = "FrmBillOfLading";
            manifiestoToolStripMenuItem.Tag = "FrmManifiesto";
            aBMContenedoresToolStripMenuItem1.Tag = "FrmContenedor";
            aBMBuquesToolStripMenuItem1.Tag = "FrmBuque";

            facturacionToolStripMenuItem1.Tag = "FrmFactura";
            pagosToolStripMenuItem.Tag = "FrmPago";

            proveedorToolStripMenuItem.Tag = "FrmProveedor";
            ServiciosToolStripMenuItem.Tag = "FrmServicioSubcontratado";
            ordenDeCompraToolStripMenuItem1.Tag = "FrmOrdenCompra";
            recepcionDeServicioToolStripMenuItem.Tag = "FrmRecepcionServicio";
            pagoAProveedoresToolStripMenuItem.Tag = "FrmPagoProveedor";

            dashBoardPrincipalToolStripMenuItem.Tag = "FrmDashboard"; // 
            reportesPorGestionToolStripMenuItem.Tag = "FrmReportes";

            empleadosToolStripMenuItem.Tag = "FrmEmpleado";
            usuariosToolStripMenuItem.Tag = "FrmUsuario";
            rolesYPermisosToolStripMenuItem.Tag = "FrmRolesYPermisos";
            backupToolStripMenuItem.Tag = "FrmBackup";
            bitacoraToolStripMenuItem.Tag = "FrmBitacora";

            
        }


        #endregion
    }
}
