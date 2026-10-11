using BE.Infra;
using BE.Dominio;
using BLL.Infra;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Infra
{
    public partial class FrmBackup : Form
    {
        #region "Atributos y Constructor"

        private readonly BackupBLL gestorBackup = new BackupBLL();
        private BEUsuario usuarioActivo;
        private BEBackup backupSeleccionado;
        private System.Collections.Generic.List<BEBackup> backupsMostrados = new System.Collections.Generic.List<BEBackup>();
        private string columnaOrdenBackup = "Fecha";
        private bool ordenBackupAscendente = false;
        public FrmBackup()
        {
            InitializeComponent();
        }

        public FrmBackup(BEUsuario usuario, SeguridadBLL seguridadBLL) : this()
        {
            usuarioActivo = usuario;
        }


        #endregion

        #region "Eventos del formulario"
        private void FrmBackup_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrilla(grdBackups);
                RefrescarGrilla();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el formulario: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region "Eventos de botones"
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarControles();
                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la lista: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerarBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string usuario = ObtenerUsuarioResponsable();

                var backup = new BEBackup
                {
                    UsuarioId = ObtenerUsuarioResponsableId(),
                    Usuario = usuario,
                    Descripcion = "Backup manual generado desde el módulo Backup/Restore"
                };

                gestorBackup.Guardar(backup);

                MessageBox.Show("Backup generado correctamente.",
                    "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar backup: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            try
            {
                if (backupSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un backup para restaurar.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult res = MessageBox.Show(
                    "¿Desea restaurar el sistema al estado del backup seleccionado?",
                    "Confirmar restauración", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.No)
                    return;

                gestorBackup.Restaurar(backupSeleccionado, backupSeleccionado.RutaArchivo);

                MessageBox.Show(
                    "Restauración completada correctamente. Cierre sesión y vuelva a ingresar para validar usuarios, roles y permisos restaurados.",
                    "Restauración completada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al restaurar el backup: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string filtroUsuario = txtBuscarUsuario.Text.Trim().ToLower();
                DateTime? filtroFecha = chkFiltrarFecha.Checked ? (DateTime?)dtpFecha.Value.Date : null;

                var listaFiltrada = gestorBackup.ListarTodo()
                    .Where(b =>
                        (string.IsNullOrEmpty(filtroUsuario) ||
                         (b.Usuario ?? string.Empty).ToLower().Contains(filtroUsuario)) &&
                        (!filtroFecha.HasValue || b.Fecha.Date == filtroFecha.Value))
                    .OrderByDescending(b => b.Fecha)
                    .ToList();

                CargarGrilla(listaFiltrada);
                backupSeleccionado = null;
                lblEstado.Text = "Backups encontrados: " + listaFiltrada.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar backups: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            chkFiltrarFecha.Checked = true;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
        #region "Configuración Visual"

        private void ConfigurarGrilla(DataGridView grilla)
        {
            grilla.AutoGenerateColumns = false;
            grilla.Columns.Clear();
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.MultiSelect = false;
            grilla.ReadOnly = true;
            grilla.RowHeadersVisible = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);

            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Fecha",
                HeaderText = "Fecha",
                FillWeight = 20
            });

            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Usuario",
                HeaderText = "Usuario",
                FillWeight = 16
            });

            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Operacion",
                HeaderText = "Operación",
                FillWeight = 18
            });

            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Descripcion",
                HeaderText = "Descripción",
                FillWeight = 34
            });

            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Carpeta",
                HeaderText = "Carpeta",
                FillWeight = 12
            });

            foreach (DataGridViewColumn columna in grilla.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            grilla.ColumnHeaderMouseClick -= grdBackups_ColumnHeaderMouseClick;
            grilla.ColumnHeaderMouseClick += grdBackups_ColumnHeaderMouseClick;
        }

        #endregion

        #region "Eventos de grilla"

        private void grdBackups_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = grdBackups.Columns[e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrWhiteSpace(columna))
                columna = grdBackups.Columns[e.ColumnIndex].Name;

            ordenBackupAscendente = columnaOrdenBackup == columna ? !ordenBackupAscendente : true;
            columnaOrdenBackup = columna;

            CargarGrilla(backupsMostrados);
            backupSeleccionado = null;
        }

        private void grdBackups_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                int indice = grdBackups.CurrentRow.Index;
                backupSeleccionado = indice >= 0 && indice < backupsMostrados.Count
                    ? backupsMostrados[indice]
                    : null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar backup: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdBackups_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                grdBackups_CellClick(sender, e);
                btnRestaurar_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al restaurar backup: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Métodos auxiliares"

        private void RefrescarGrilla()
        {
            var backups = gestorBackup.ListarTodo()
                .OrderByDescending(b => b.Fecha)
                .ToList();

            CargarGrilla(backups);
            backupSeleccionado = null;
            lblEstado.Text = "Backups encontrados: " + backups.Count;
        }

        private void CargarGrilla(System.Collections.Generic.List<BEBackup> backups)
        {
            backupsMostrados = OrdenarBackups(backups).ToList();
            grdBackups.DataSource = null;
            grdBackups.DataSource = MapearBackups(backupsMostrados);
            grdBackups.AutoResizeColumns();
        }

        private System.Collections.Generic.IEnumerable<BEBackup> OrdenarBackups(System.Collections.Generic.IEnumerable<BEBackup> backups)
        {
            Func<BEBackup, object> selector;

            switch (columnaOrdenBackup)
            {
                case "Usuario":
                    selector = x => x.Usuario;
                    break;
                case "Operacion":
                    selector = x => x.TipoOperacion != null ? x.TipoOperacion.Descripcion : string.Empty;
                    break;
                case "Descripcion":
                    selector = x => x.Descripcion;
                    break;
                case "Carpeta":
                    selector = x => string.IsNullOrWhiteSpace(x.RutaArchivo)
                        ? string.Empty
                        : System.IO.Path.GetFileName(x.RutaArchivo);
                    break;
                default:
                    selector = x => x.Fecha;
                    break;
            }

            return ordenBackupAscendente
                ? backups.OrderBy(selector)
                : backups.OrderByDescending(selector);
        }

        private System.Collections.Generic.List<BackupVista> MapearBackups(System.Collections.Generic.List<BEBackup> backups)
        {
            return backups
                .Select(b => new BackupVista
                {
                    Fecha = b.Fecha.ToString("dd/MM/yyyy HH:mm"),
                    Usuario = b.Usuario ?? string.Empty,
                    Operacion = b.TipoOperacion != null ? b.TipoOperacion.Descripcion : string.Empty,
                    Descripcion = b.Descripcion ?? string.Empty,
                    Carpeta = string.IsNullOrWhiteSpace(b.RutaArchivo)
                        ? string.Empty
                        : System.IO.Path.GetFileName(b.RutaArchivo)
                })
                .ToList();
        }

        private void LimpiarControles()
        {
            txtBuscarUsuario.Text = string.Empty;
            chkFiltrarFecha.Checked = false;
            backupSeleccionado = null;
            lblEstado.Text = string.Empty;
        }

        private string ObtenerUsuarioResponsable()
        {
            if (usuarioActivo != null && !string.IsNullOrWhiteSpace(usuarioActivo.Username))
                return usuarioActivo.Username;

            return "Sistema";
        }

        private long ObtenerUsuarioResponsableId()
        {
            return usuarioActivo != null ? usuarioActivo.Id : 0;
        }

        private class BackupVista
        {
            public string Fecha { get; set; }
            public string Usuario { get; set; }
            public string Operacion { get; set; }
            public string Descripcion { get; set; }
            public string Carpeta { get; set; }
        }

        #endregion


    }
}
