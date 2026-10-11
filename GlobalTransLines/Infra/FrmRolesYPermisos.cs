using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL.Infra;
using BLL.Personas;
using BE.Infra;

namespace GlobalTransLines.UI.Infra
{
    public partial class FrmRolesYPermisos : Form
    {
        #region "Atributos y Constructor"

        private readonly RolBLL gestorRol = new RolBLL();
        private readonly PermisoBLL gestorPermiso = new PermisoBLL();
        private readonly ComponentBLL gestorComposite = new ComponentBLL();
        private readonly UsuarioBLL gestorUsuario = new UsuarioBLL();

        private List<BERol> rolesBase = new List<BERol>();
        private List<BEPermiso> permisosBase = new List<BEPermiso>();
        private List<BERol> rolesJerarquia = new List<BERol>();
        private List<BEUsuario> usuariosBase = new List<BEUsuario>();

        private BERol rolSeleccionado;
        private BEPermiso permisoSeleccionado;
        private BEUsuario usuarioSeleccionado;
        private Label lblGuia;
        private TableLayoutPanel layoutPrincipal;

        public FrmRolesYPermisos()
        {
            InitializeComponent();
            ConfigurarPermisosComoCatalogo();
            ConfigurarGuiaInteractiva();
        }

        private void ConfigurarPermisosComoCatalogo()
        {
            Text = "Roles y permisos";

            groupBox2.Text = "Roles disponibles";
            grpRolPermisos.Text = "Permisos asignados al rol";
            grpRolRoles.Text = "Subroles y permisos heredados";
            tabPermisos.Text = "Catálogo de permisos";
            groupBox3.Text = "Permisos del sistema";
            grpPermisoDetalle.Text = "Detalle del permiso";
            grpAccesosDelUsuario.Text = "Accesos efectivos del usuario";

            btnPermisosDelRolAsociar.Text = "Asociar permisos";
            btnPermisosDelRolQuitar.Text = "Quitar permiso";
            btnAsociarRolARol.Text = "Asociar subrol";
        }

        private void ConfigurarGuiaInteractiva()
        {
            lblGuia = new Label();

            layoutPrincipal = new TableLayoutPanel();
            layoutPrincipal.ColumnCount = 1;
            layoutPrincipal.RowCount = 2;
            layoutPrincipal.Dock = DockStyle.Fill;
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            lblGuia.Height = 26;
            lblGuia.Dock = DockStyle.Fill;
            lblGuia.Padding = new Padding(8, 5, 8, 4);
            lblGuia.BackColor = SystemColors.Info;
            lblGuia.ForeColor = SystemColors.ControlText;
            lblGuia.TextAlign = ContentAlignment.MiddleLeft;

            Controls.Remove(tabControl1);
            layoutPrincipal.Controls.Add(lblGuia, 0, 0);
            layoutPrincipal.Controls.Add(tabControl1, 0, 1);
            Controls.Add(layoutPrincipal);
            layoutPrincipal.BringToFront();

            tabControl1.SelectedIndexChanged += (s, e) => ActualizarGuiaPorPestania();

            AjustarCuadriculaRoles();

            AsociarGuia(treeRoles, "Seleccioná un rol para ver sus permisos directos, subroles y permisos heredados.");
            AsociarGuia(treePermisos, "Catálogo de permisos del sistema. Se usan para armar roles.");
            AsociarGuia(treePermisosDelRol, "Permisos directos del rol seleccionado.");
            AsociarGuia(treeRolesJerarquia, "Subroles y permisos heredados por el rol seleccionado.");
            AsociarGuia(treeUsuarios, "Seleccioná un usuario para ver sus roles y accesos efectivos.");
            AsociarGuia(treeRolesDelUsuario, "Roles asignados directamente al usuario seleccionado.");
            AsociarGuia(treeAccesosDelUsuario, "Accesos finales que obtiene el usuario por sus roles y subroles.");

            AsociarGuia(btnPermisosDelRolAsociar, "Asocia varios permisos del catálogo al rol seleccionado.");
            AsociarGuia(btnPermisosDelRolQuitar, "Quita del rol el permiso seleccionado en la lista de permisos directos.");
            AsociarGuia(btnAsociarRolARol, "Agrega subroles al rol seleccionado para heredar permisos.");
            AsociarGuia(btnDesasociarRolARol, "Quita un subrol de la jerarquía del rol seleccionado.");
            AsociarGuia(btnAsignarRol, "Asigna uno o varios roles al usuario seleccionado.");
            AsociarGuia(btnQuitarRol, "Quita del usuario el rol seleccionado.");

            ActualizarGuiaPorPestania();
        }

        private void AsociarGuia(Control control, string mensaje)
        {
            control.MouseEnter += (s, e) => ActualizarGuia(mensaje);
        }

        private void ActualizarGuiaPorPestania()
        {
            if (tabControl1.SelectedTab == tabRoles)
                ActualizarGuia("Roles: creá o seleccioná un rol, luego asociá permisos del catálogo o subroles.");
            else if (tabControl1.SelectedTab == tabPermisos)
                ActualizarGuia("Catálogo de permisos: representa funcionalidades reales del sistema. Se asignan desde la pestaña Roles.");
            else if (tabControl1.SelectedTab == tabAsignaciones)
                ActualizarGuia("Asignaciones: seleccioná un usuario para administrar sus roles y revisar sus accesos efectivos.");
        }

        private void ActualizarGuia(string mensaje)
        {
            if (lblGuia != null)
                lblGuia.Text = mensaje;
        }

        private void AjustarCuadriculaRoles()
        {
            tlpRolesRight.RowCount = 3;
            tlpRolesRight.RowStyles.Clear();
            tlpRolesRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 159F));
            tlpRolesRight.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
            tlpRolesRight.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));

            scRolPermisos.FixedPanel = FixedPanel.Panel2;
            scRolPermisos.Panel2MinSize = 52;
            scRolPermisos.SplitterDistance = Math.Max(80, scRolPermisos.Height - 56);

            scJerarquiaRoles.FixedPanel = FixedPanel.Panel2;
            scJerarquiaRoles.Panel2MinSize = 52;
            scJerarquiaRoles.SplitterDistance = Math.Max(80, scJerarquiaRoles.Height - 56);
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmRolesYPermisos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarTodo();
                LimpiarRoles();
                LimpiarPermisos();
                LimpiarAsignaciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el formulario: " + ex.Message,
                    "Roles y Permisos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos TreeView"

        private void treeRoles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node == null) return;

                var rolPlano = e.Node.Tag as BERol;
                if (rolPlano == null) return;

                rolSeleccionado = rolPlano;
                ActualizarGuia("Rol seleccionado: revisá sus permisos directos, su jerarquía o asociá nuevos permisos.");

                MostrarDetalleRol(rolSeleccionado);

                var rolReal = BuscarRolEnJerarquia(rolesJerarquia, rolSeleccionado.Id);
                if (rolReal == null)
                    rolReal = rolSeleccionado;

                PintarPermisosDirectosDelRol(rolReal);
                PintarJerarquiaDelRol(rolReal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar rol: " + ex.Message,
                    "Roles", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treePermisos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node == null) return;

                var permiso = e.Node.Tag as BEPermiso;
                if (permiso == null) return;

                permisoSeleccionado = permiso;
                ActualizarGuia("Permiso seleccionado del catálogo. Para asignarlo, volvé a Roles y usá Asociar Permiso al Rol.");
                MostrarDetallePermiso(permisoSeleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar permiso: " + ex.Message,
                    "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeUsuarios_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node == null) return;

                var usuario = e.Node.Tag as BEUsuario;
                if (usuario == null) return;

                usuarioSeleccionado = usuario;
                ActualizarGuia("Usuario seleccionado: revisá sus roles y los accesos efectivos que recibe.");

                PintarRolesDelUsuario(usuarioSeleccionado);
                PintarAccesosDelUsuario(usuarioSeleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar usuario: " + ex.Message,
                    "Asignaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones - Roles (CRUD)"

        private void btnAltaRol_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = (txtRolNombre.Text ?? "").Trim();
                string descripcion = (txtRolDescripcion.Text ?? "").Trim();

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("Ingresá el nombre del rol.", "Alta Rol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    MessageBox.Show("Ingresá la descripción del rol.", "Alta Rol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var rol = new BERol
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Activo = true
                };

                gestorRol.Guardar(rol);

                CargarTodo();
                RefrescarRolesManteniendoSeleccion(rol.Id);
                MessageBox.Show("Rol agregado correctamente.", "Alta Rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar rol: " + ex.Message, "Alta Rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (rolSeleccionado == null)
                {
                    MessageBox.Show("Seleccioná un rol para modificar.", "Modificar Rol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nombre = (txtRolNombre.Text ?? "").Trim();
                string descripcion = (txtRolDescripcion.Text ?? "").Trim();

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    MessageBox.Show("Ingresá el nombre del rol.", "Modificar Rol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    MessageBox.Show("Ingresá la descripción del rol.", "Modificar Rol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                rolSeleccionado.Nombre = nombre;
                rolSeleccionado.Descripcion = descripcion;

                gestorRol.Guardar(rolSeleccionado);

                long id = rolSeleccionado.Id;
                CargarTodo();
                RefrescarRolesManteniendoSeleccion(id);

                MessageBox.Show("Rol modificado correctamente.", "Modificar Rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar rol: " + ex.Message, "Modificar Rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (rolSeleccionado == null)
                {
                    MessageBox.Show("Seleccioná un rol para eliminar (baja lógica).", "Eliminar Rol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult res = MessageBox.Show("¿Confirma la baja del rol seleccionado?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.No) return;

                gestorRol.Eliminar(rolSeleccionado);

                CargarTodo();
                LimpiarRoles();

                MessageBox.Show("Rol dado de baja correctamente.", "Eliminar Rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar rol: " + ex.Message, "Eliminar Rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try { LimpiarRoles(); }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar: " + ex.Message, "Roles",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones - Permisos (CRUD)"

        private void btnlimpia_Click(object sender, EventArgs e)
        {
            try { LimpiarPermisos(); }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar: " + ex.Message, "Permisos",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones - Permisos del Rol"

        private void btnPermisosDelRolAsociar_Click(object sender, EventArgs e)
        {
            try
            {
                if (rolSeleccionado == null)
                {
                    MessageBox.Show("Seleccioná un rol.", "Asociar Permiso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var frm = new FrmSeleccionPermisos())
                {
                    frm.CargarPermisos(permisosBase.ToList());

                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                    var permisosElegidos = frm.ObtenerPermisosSeleccionados();
                    if (permisosElegidos == null || permisosElegidos.Count == 0)
                        return;

                    foreach (var permiso in permisosElegidos)
                        gestorComposite.AsociarPermiso(rolSeleccionado.Id, permiso.Id);
                }

                RefrescarRolesManteniendoSeleccion(rolSeleccionado.Id);

                MessageBox.Show("Permiso(s) asociado(s) correctamente.", "Asociar Permiso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al asociar permiso: " + ex.Message, "Asociar Permiso",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPermisosDelRolQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (rolSeleccionado == null)
                {
                    MessageBox.Show("Seleccioná un rol.", "Quitar Permiso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (treePermisosDelRol.SelectedNode == null || !(treePermisosDelRol.SelectedNode.Tag is BEPermiso))
                {
                    MessageBox.Show("Seleccioná un permiso dentro de 'Permisos del Rol'.", "Quitar Permiso",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var permiso = (BEPermiso)treePermisosDelRol.SelectedNode.Tag;

                gestorComposite.DesasociarPermiso(rolSeleccionado.Id, permiso.Id);

                RefrescarRolesManteniendoSeleccion(rolSeleccionado.Id);

                MessageBox.Show("Permiso quitado correctamente.", "Quitar Permiso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar permiso: " + ex.Message, "Quitar Permiso",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones - Jerarquía de Roles (Rol-Rol)"

        private void btnAsociarRolARol_Click(object sender, EventArgs e)
        {
            try
            {
                if (rolSeleccionado == null)
                {
                    MessageBox.Show("Seleccioná el rol padre.", "Asociar Rol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var frm = new FrmSeleccionRoles())
                {
                    var disponibles = rolesBase.Where(r => r.Id != rolSeleccionado.Id).ToList();

                    frm.CargarRoles(disponibles);

                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                    var rolesElegidos = frm.ObtenerRolesSeleccionados();
                    if (rolesElegidos == null || rolesElegidos.Count == 0)
                        return;

                    foreach (var sub in rolesElegidos)
                        gestorComposite.AsociarSubRol(rolSeleccionado.Id, sub.Id);
                }

                RefrescarRolesManteniendoSeleccion(rolSeleccionado.Id);

                MessageBox.Show("Subrol(es) asociado(s) correctamente.", "Asociar Rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al asociar subrol: " + ex.Message, "Asociar Rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesasociarRolARol_Click(object sender, EventArgs e)
        {
            try
            {
                if (rolSeleccionado == null)
                {
                    MessageBox.Show("Seleccioná el rol padre.", "Quitar Subrol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (treeRolesJerarquia.SelectedNode == null || !(treeRolesJerarquia.SelectedNode.Tag is BERol))
                {
                    MessageBox.Show("Seleccioná un subrol en 'Jerarquía de Roles'.", "Quitar Subrol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var sub = (BERol)treeRolesJerarquia.SelectedNode.Tag;

                if (sub.Id == rolSeleccionado.Id)
                {
                    MessageBox.Show("Seleccioná un subrol (no el rol raíz).", "Quitar Subrol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                gestorComposite.QuitarSubRol(rolSeleccionado.Id, sub.Id);

                RefrescarRolesManteniendoSeleccion(rolSeleccionado.Id);

                MessageBox.Show("Subrol quitado correctamente.", "Quitar Subrol",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar subrol: " + ex.Message, "Quitar Subrol",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones - Asignaciones (Usuario-Rol)"

        private void btnAsignarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioSeleccionado == null)
                {
                    MessageBox.Show("Seleccioná un usuario.", "Asignar Rol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                long usuarioId = usuarioSeleccionado.Id;

                using (var frm = new FrmSeleccionRoles())
                {
                    frm.CargarRoles(rolesBase.ToList());

                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;

                    var rolesElegidos = frm.ObtenerRolesSeleccionados();
                    if (rolesElegidos == null || rolesElegidos.Count == 0)
                        return;

                    gestorUsuario.AsignarRoles(usuarioId, rolesElegidos.Select(r => r.Id).ToList());

                    usuarioSeleccionado = gestorUsuario.ListarObjeto(new BEUsuario { Id = usuarioId });

                    usuariosBase = gestorUsuario.ListarTodo();
                    PintarTreeUsuarios();

                    ReSeleccionarUsuarioEnTree(usuarioId);

                    MessageBox.Show("Rol(es) asignado(s) correctamente.", "Asignar Rol",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al asignar rol: " + ex.Message, "Asignar Rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitarRol_Click(object sender, EventArgs e)
        {
            try
            {
                if (usuarioSeleccionado == null)
                {
                    MessageBox.Show("Seleccioná un usuario.", "Quitar Rol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (treeRolesDelUsuario.SelectedNode == null || !(treeRolesDelUsuario.SelectedNode.Tag is BERol))
                {
                    MessageBox.Show("Seleccioná un rol en 'Roles del Usuario'.", "Quitar Rol",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                long usuarioId = usuarioSeleccionado.Id;
                var rol = (BERol)treeRolesDelUsuario.SelectedNode.Tag;

                gestorUsuario.QuitarRol(usuarioId, rol.Id);

                usuarioSeleccionado = gestorUsuario.ListarObjeto(new BEUsuario { Id = usuarioId });

                usuariosBase = gestorUsuario.ListarTodo();
                PintarTreeUsuarios();

                ReSeleccionarUsuarioEnTree(usuarioId);

                MessageBox.Show("Rol quitado correctamente.", "Quitar Rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar rol: " + ex.Message, "Quitar Rol",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReSeleccionarUsuarioEnTree(long usuarioId)
        {
            foreach (TreeNode n in treeUsuarios.Nodes)
            {
                var u = n.Tag as BEUsuario;
                if (u != null && u.Id == usuarioId)
                {
                    treeUsuarios.SelectedNode = n;
                    n.EnsureVisible();
                    break;
                }
            }
        }

        #endregion

        #region "Carga de datos"

        private void CargarTodo()
        {
            rolesBase = gestorRol.ListarTodo();
            permisosBase = gestorPermiso.ListarTodo();
            rolesJerarquia = gestorComposite.ConstruirJerarquia();
            usuariosBase = gestorUsuario.ListarTodo();

            PintarTreeRoles();
            PintarTreePermisos();
            PintarTreeUsuarios();
        }

        #endregion

        #region "Pintado de Trees"

        private void PintarTreeRoles()
        {
            treeRoles.BeginUpdate();
            treeRoles.Nodes.Clear();

            foreach (var rol in rolesBase.OrderBy(r => r.Nombre))
            {
                var nodo = new TreeNode(rol.Nombre);
                nodo.Tag = rol;
                treeRoles.Nodes.Add(nodo);
            }

            treeRoles.EndUpdate();
            treeRoles.ExpandAll();
        }

        private void PintarTreePermisos()
        {
            treePermisos.BeginUpdate();
            treePermisos.Nodes.Clear();

            foreach (var p in permisosBase.OrderBy(x => x.Nombre))
            {
                var nodo = new TreeNode(p.Nombre);
                nodo.Tag = p;
                treePermisos.Nodes.Add(nodo);
            }

            treePermisos.EndUpdate();
            treePermisos.ExpandAll();
        }

        private void PintarTreeUsuarios()
        {
            treeUsuarios.BeginUpdate();
            treeUsuarios.Nodes.Clear();

            foreach (var u in usuariosBase.OrderBy(x => x.Username))
            {
                var nodo = new TreeNode(u.Username);
                nodo.Tag = u;
                treeUsuarios.Nodes.Add(nodo);
            }

            treeUsuarios.EndUpdate();
            treeUsuarios.ExpandAll();
        }

        #endregion

        #region "Detalle / Pintado (Roles)"

        private void MostrarDetalleRol(BERol rol)
        {
            txtRolId.Text = rol.Id.ToString();
            txtRolNombre.Text = rol.Nombre;
            txtRolDescripcion.Text = rol.Descripcion;
        }

        private void PintarPermisosDirectosDelRol(BERol rolReal)
        {
            treePermisosDelRol.BeginUpdate();
            treePermisosDelRol.Nodes.Clear();

            foreach (var p in rolReal.ObtenerHijos().OfType<BEPermiso>().OrderBy(x => x.Nombre))
            {
                var nodo = new TreeNode(p.Nombre);
                nodo.Tag = p;
                treePermisosDelRol.Nodes.Add(nodo);
            }

            treePermisosDelRol.EndUpdate();
            treePermisosDelRol.ExpandAll();
        }

        private void PintarJerarquiaDelRol(BERol rolReal)
        {
            treeRolesJerarquia.BeginUpdate();
            treeRolesJerarquia.Nodes.Clear();

            var raiz = new TreeNode(rolReal.Nombre);
            raiz.Tag = rolReal;
            treeRolesJerarquia.Nodes.Add(raiz);

            PintarJerarquiaRecursivo(raiz, rolReal);

            treeRolesJerarquia.EndUpdate();
            treeRolesJerarquia.ExpandAll();
        }

        private void PintarJerarquiaRecursivo(TreeNode nodoPadre, BERol rol)
        {
            foreach (var hijo in rol.ObtenerHijos())
            {
                if (hijo is BERol)
                {
                    var r = (BERol)hijo;

                    var n = new TreeNode(r.Nombre);
                    n.Tag = r;
                    n.ForeColor = Color.MidnightBlue;
                    n.NodeFont = new Font(treeRolesJerarquia.Font, FontStyle.Bold);

                    nodoPadre.Nodes.Add(n);
                    PintarJerarquiaRecursivo(n, r);
                }
                else if (hijo is BEPermiso)
                {
                    var p = (BEPermiso)hijo;
                    var n = new TreeNode(p.Nombre);
                    n.Tag = p;
                    nodoPadre.Nodes.Add(n);
                }
            }
        }

        private BERol BuscarRolEnJerarquia(List<BERol> jerarquia, long rolId)
        {
            if (jerarquia == null) return null;

            foreach (var r in jerarquia)
            {
                if (r.Id == rolId) return r;

                var subRoles = r.ObtenerHijos().OfType<BERol>().ToList();
                var encontrado = BuscarRolEnJerarquia(subRoles, rolId);
                if (encontrado != null) return encontrado;
            }

            return null;
        }

        #endregion

        #region "Detalle (Permisos)"

        private void MostrarDetallePermiso(BEPermiso permiso)
        {
            txtPermisoId.Text = permiso.Id.ToString();
            txtPermisoNombre.Text = permiso.Nombre;
            txtPermisoDescripcion.Text = permiso.Descripcion;
        }

        #endregion

        #region "Asignaciones"

        private void PintarRolesDelUsuario(BEUsuario usuario)
        {
            treeRolesDelUsuario.BeginUpdate();
            treeRolesDelUsuario.Nodes.Clear();

            if (usuario != null && usuario.Roles != null)
            {
                foreach (var r in usuario.Roles.OrderBy(x => x.Nombre))
                {
                    var nodo = new TreeNode(r.Nombre);
                    nodo.Tag = r;
                    treeRolesDelUsuario.Nodes.Add(nodo);
                }
            }

            treeRolesDelUsuario.EndUpdate();
            treeRolesDelUsuario.ExpandAll();
        }

        private void PintarAccesosDelUsuario(BEUsuario usuario)
        {
            treeAccesosDelUsuario.BeginUpdate();
            treeAccesosDelUsuario.Nodes.Clear();

            if (usuario == null || usuario.Roles == null || usuario.Roles.Count == 0)
            {
                treeAccesosDelUsuario.EndUpdate();
                return;
            }

            var jerarquia = rolesJerarquia;
            if (jerarquia == null || jerarquia.Count == 0)
                jerarquia = gestorComposite.ConstruirJerarquia();

            foreach (var rolAsignado in usuario.Roles)
            {
                if (rolAsignado == null || rolAsignado.Id <= 0) continue;

                var rolReal = BuscarRolEnJerarquia(jerarquia, rolAsignado.Id);
                if (rolReal == null) continue;

                var nodoRol = new TreeNode("ROL: " + rolReal.Nombre);
                nodoRol.Tag = rolReal;
                nodoRol.ForeColor = Color.MidnightBlue;
                nodoRol.NodeFont = new Font(treeAccesosDelUsuario.Font, FontStyle.Bold);

                treeAccesosDelUsuario.Nodes.Add(nodoRol);

                PintarAccesosRecursivo(nodoRol, rolReal);
            }

            treeAccesosDelUsuario.EndUpdate();
            treeAccesosDelUsuario.ExpandAll();
        }

        private void PintarAccesosRecursivo(TreeNode nodoPadre, BERol rol)
        {
            foreach (var hijo in rol.ObtenerHijos())
            {
                if (hijo is BERol)
                {
                    var r = (BERol)hijo;

                    var n = new TreeNode("SUBROL: " + r.Nombre);
                    n.Tag = r;
                    n.ForeColor = Color.DarkSlateBlue;

                    nodoPadre.Nodes.Add(n);

                    PintarAccesosRecursivo(n, r);
                }
                else if (hijo is BEPermiso)
                {
                    var p = (BEPermiso)hijo;

                    var n = new TreeNode("PERMISO: " + p.Nombre);
                    n.Tag = p;
                    n.ForeColor = Color.DarkGreen;

                    nodoPadre.Nodes.Add(n);
                }
            }
        }

        #endregion

        #region "Refrescar manteniendo selección"

        private void RefrescarRolesManteniendoSeleccion(long rolId)
        {
            rolesBase = gestorRol.ListarTodo();
            rolesJerarquia = gestorComposite.ConstruirJerarquia();

            PintarTreeRoles();

            foreach (TreeNode n in treeRoles.Nodes)
            {
                var r = n.Tag as BERol;
                if (r != null && r.Id == rolId)
                {
                    treeRoles.SelectedNode = n;
                    n.EnsureVisible();
                    break;
                }
            }
        }

        #endregion

        #region "Limpieza"

        private void LimpiarRoles()
        {
            rolSeleccionado = null;

            txtRolId.Text = "";
            txtRolNombre.Text = "";
            txtRolDescripcion.Text = "";

            treePermisosDelRol.Nodes.Clear();
            treeRolesJerarquia.Nodes.Clear();
        }

        private void LimpiarPermisos()
        {
            permisoSeleccionado = null;

            txtPermisoId.Text = "";
            txtPermisoNombre.Text = "";
            txtPermisoDescripcion.Text = "";
        }

        private void LimpiarAsignaciones()
        {
            usuarioSeleccionado = null;
            treeRolesDelUsuario.Nodes.Clear();
            treeAccesosDelUsuario.Nodes.Clear();
        }

        #endregion
    }
}
