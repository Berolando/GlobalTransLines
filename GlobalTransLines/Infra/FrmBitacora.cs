using BE.Dominio;
using BE.Infra;
using BLL.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;



namespace GlobalTransLines.UI.Infra
{
    public partial class FrmBitacora : Form
    {
        #region "Atributos y Constructor"

        private BitacoraBLL gestorBitacora = new BitacoraBLL();
        private UsuarioBLL gestorUsuario = new UsuarioBLL();
        private List<BEEvento> listaEventos = new List<BEEvento>();
        private List<BEEvento> listaFiltrada = new List<BEEvento>();
        private string columnaOrdenEvento = "Fecha";
        private bool ordenEventoAscendente = false;

        public FrmBitacora()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmBitacora_Load(object sender, EventArgs e)
        {
            ConfiguracionGrilla(dgvEventos);
            CargarCombos();
            RefrescarGrilla();
        }

        #endregion

        #region "Configuración Visual"

        private void ConfiguracionGrilla(DataGridView grilla)
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
                FillWeight = 16
            });

            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Usuario",
                HeaderText = "Usuario",
                FillWeight = 14
            });

            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Tipo",
                HeaderText = "Tipo",
                FillWeight = 18
            });

            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Origen",
                HeaderText = "Origen",
                FillWeight = 18
            });

            grilla.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Descripcion",
                HeaderText = "Descripción",
                FillWeight = 34
            });

            foreach (DataGridViewColumn columna in grilla.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            grilla.ColumnHeaderMouseClick -= dgvEventos_ColumnHeaderMouseClick;
            grilla.ColumnHeaderMouseClick += dgvEventos_ColumnHeaderMouseClick;
        }

        #endregion

        #region "Eventos de grilla"

        private void dgvEventos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = dgvEventos.Columns[e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrWhiteSpace(columna))
                columna = dgvEventos.Columns[e.ColumnIndex].Name;

            if (columnaOrdenEvento == columna)
                ordenEventoAscendente = !ordenEventoAscendente;
            else
            {
                columnaOrdenEvento = columna;
                ordenEventoAscendente = true;
            }

            List<BEEvento> origen = listaFiltrada != null && listaFiltrada.Count > 0
                ? listaFiltrada
                : listaEventos;

            dgvEventos.DataSource = null;
            dgvEventos.DataSource = MapearEventos(OrdenarEventos(origen));
        }

        #endregion

        #region "Carga de datos iniciales"

        private void CargarCombos()
        {
            try
            {
                // Tipo de operación
                cmbTipoOperacion.DataSource = TipoOperacion.ObtenerTodos().ToList();
                cmbTipoOperacion.SelectedIndex = -1;

                // Usuarios
                var usuarios = gestorUsuario.ListarTodo();
                cmbUsuario.DataSource = usuarios;
                cmbUsuario.DisplayMember = "Username";
                cmbUsuario.ValueMember = "Id";
                cmbUsuario.SelectedIndex = -1;

                // Fechas
                dtpDesde.Value = DateTime.Today.AddDays(-7);
                dtpHasta.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar combos: " + ex.Message);
            }
        }

        private void RefrescarGrilla()
        {
            try
            {
                listaEventos = gestorBitacora.ListarTodo();
                listaFiltrada = new List<BEEvento>();
                dgvEventos.DataSource = null;
                dgvEventos.DataSource = MapearEventos(OrdenarEventos(listaEventos));
                lblEstado.Text = "Eventos encontrados: " + listaEventos.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar eventos: " + ex.Message);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                listaFiltrada = listaEventos;

                // Filtro por tipo de operación
                if (cmbTipoOperacion.SelectedIndex != -1)
                {
                    var tipoSel = (TipoOperacion)cmbTipoOperacion.SelectedItem;
                    listaFiltrada = listaFiltrada
                        .Where(evn => evn.Tipo == tipoSel)
                        .ToList();
                }

                // Filtro por usuario
                if (cmbUsuario.SelectedIndex != -1)
                {
                    var usuarioSel = (BEUsuario)cmbUsuario.SelectedItem;
                    listaFiltrada = listaFiltrada
                        .Where(evn => evn.Usuario != null && evn.Usuario.Id == usuarioSel.Id)
                        .ToList();
                }

                // Filtro por rango de fechas
                var desde = dtpDesde.Value.Date;
                var hasta = dtpHasta.Value.Date.AddDays(1).AddTicks(-1);
                listaFiltrada = listaFiltrada
                    .Where(evn => evn.Fecha >= desde && evn.Fecha <= hasta)
                    .ToList();

                dgvEventos.DataSource = null;
                dgvEventos.DataSource = MapearEventos(OrdenarEventos(listaFiltrada));
                lblEstado.Text = "Eventos encontrados: " + listaFiltrada.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar eventos: " + ex.Message);
            }
        }

        private void btnRestablecer_Click(object sender, EventArgs e)
        {
            try
            {
                cmbTipoOperacion.SelectedIndex = -1;
                cmbUsuario.SelectedIndex = -1;
                dtpDesde.Value = DateTime.Today.AddDays(-7);
                dtpHasta.Value = DateTime.Today;
                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reestablecer filtros: " + ex.Message);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region "Métodos auxiliares"

        private List<EventoVista> MapearEventos(List<BEEvento> eventos)
        {
            return eventos
                .Select(e => new EventoVista
                {
                    Fecha = e.Fecha.ToString("dd/MM/yyyy HH:mm"),
                    Usuario = e.Usuario != null && !string.IsNullOrWhiteSpace(e.Usuario.Username)
                        ? e.Usuario.Username
                        : "Sistema",
                    Tipo = e.Tipo != null ? e.Tipo.Descripcion : string.Empty,
                    Origen = e.Origen ?? string.Empty,
                    Descripcion = e.Descripcion ?? string.Empty
                })
                .ToList();
        }

        private List<BEEvento> OrdenarEventos(List<BEEvento> eventos)
        {
            Func<BEEvento, object> criterio;

            switch (columnaOrdenEvento)
            {
                case "Usuario":
                    criterio = e => e.Usuario != null && !string.IsNullOrWhiteSpace(e.Usuario.Username)
                        ? e.Usuario.Username
                        : "Sistema";
                    break;
                case "Tipo":
                    criterio = e => e.Tipo != null ? e.Tipo.Descripcion : string.Empty;
                    break;
                case "Origen":
                    criterio = e => e.Origen ?? string.Empty;
                    break;
                case "Descripcion":
                    criterio = e => e.Descripcion ?? string.Empty;
                    break;
                default:
                    criterio = e => e.Fecha;
                    break;
            }

            return ordenEventoAscendente
                ? eventos.OrderBy(criterio).ToList()
                : eventos.OrderByDescending(criterio).ToList();
        }

        private class EventoVista
        {
            public string Fecha { get; set; }
            public string Usuario { get; set; }
            public string Tipo { get; set; }
            public string Origen { get; set; }
            public string Descripcion { get; set; }
        }

        #endregion
    }
}
