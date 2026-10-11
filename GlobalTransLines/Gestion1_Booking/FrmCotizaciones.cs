using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BE.Dominio;
using BE.Finanzas;
using BLL.Finanzas;
using BLL.Infra;

namespace GlobalTransLines.UI.Gestion1_Booking
{
    public partial class FrmCotizaciones : Form
    {
        #region "Atributos y Constructores"

        private  CotizacionBLL gestorCotizacion = new CotizacionBLL();
        private SeguridadBLL seguridadBLL;
        private List<BECotizacion> listaCotizaciones = new List<BECotizacion>();
        public BECotizacion cotizacionSeleccionada;
        public bool ModoSeleccion { get; set; }
        public bool esUsuarioAutorizado;
        private string columnaOrdenCotizacion = "Id";
        private bool ordenCotizacionAscendente;


        public FrmCotizaciones()
        {
            InitializeComponent();
            ModoSeleccion = false;
            if (seguridadBLL == null)
                seguridadBLL = new SeguridadBLL();
        }
        public FrmCotizaciones(SeguridadBLL seguridad) : this()
        {
            seguridadBLL = seguridad ?? new SeguridadBLL();
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmCotizaciones_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrilla();
                esUsuarioAutorizado = EsUsuarioAprobador();
                ConfigurarModoPorRol();

                RefrescarGrilla();
                btnAprobar.Enabled = false;
                btnRechazar.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar formulario de cotizaciones: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnNueva_Click(object sender, EventArgs e)
        {
            if (esUsuarioAutorizado)
            {
                MessageBox.Show("Este módulo está en modo aprobación. No puede realizar ABM.",
                    "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var frm = new FrmCotizacion(seguridadBLL);
            frm.ShowDialog();
            RefrescarGrilla();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (esUsuarioAutorizado)
            {
                MessageBox.Show("Este módulo está en modo aprobación. No puede realizar ABM.",
                    "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (cotizacionSeleccionada == null)
                {
                    MessageBox.Show("Seleccione una cotización para modificar.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var frm = new FrmCotizacion(cotizacionSeleccionada, seguridadBLL);
                frm.ShowDialog();
                RefrescarGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar cotización: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (esUsuarioAutorizado)
            {
                MessageBox.Show("Este módulo está en modo aprobación. No puede realizar ABM.",
                    "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (cotizacionSeleccionada == null)
                {
                    MessageBox.Show("Seleccione una cotización para eliminar.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult res = MessageBox.Show("¿Confirma la eliminación de la cotización seleccionada?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.No)
                    return;

                gestorCotizacion.Eliminar(cotizacionSeleccionada);
                RefrescarGrilla();

                MessageBox.Show("Cotización eliminada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar cotización: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            RefrescarGrilla(txtBuscar.Text.Trim().ToLower());
        }
        private void btnAprobar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!esUsuarioAutorizado)
                    throw new Exception("Acceso denegado.");

                if (cotizacionSeleccionada == null)
                {
                    MessageBox.Show("Seleccione una cotización para aprobar.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cotizacionSeleccionada.Estado == null || cotizacionSeleccionada.Estado.Codigo != EstadoCotizacion.PendienteAprobacion.Codigo)
                {
                    MessageBox.Show("Solo se pueden aprobar cotizaciones en Pendiente de Aprobación.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                gestorCotizacion.CambiarEstado(cotizacionSeleccionada, EstadoCotizacion.Aceptada);
                RefrescarGrilla();

                MessageBox.Show("Cotización aprobada.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al aprobar: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnRechazar_Click(object sender, EventArgs e)
        {

            try
            {
                if (!esUsuarioAutorizado)
                    throw new Exception("Acceso denegado.");

                if (cotizacionSeleccionada == null)
                {
                    MessageBox.Show("Seleccione una cotización para rechazar.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cotizacionSeleccionada.Estado == null || cotizacionSeleccionada.Estado.Codigo != EstadoCotizacion.PendienteAprobacion.Codigo)
                {
                    MessageBox.Show("Solo se pueden rechazar cotizaciones en Pendiente de Aprobación.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                gestorCotizacion.CambiarEstado(cotizacionSeleccionada, EstadoCotizacion.Rechazada);
                RefrescarGrilla();

                MessageBox.Show("Cotización rechazada.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al rechazar: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region "Configuración Visual"

        private void ConfigurarGrilla()
        {
            grdCotizaciones.Columns.Clear();
            grdCotizaciones.AutoGenerateColumns = false;
            grdCotizaciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdCotizaciones.MultiSelect = false;
            grdCotizaciones.ReadOnly = true;
            grdCotizaciones.RowHeadersVisible = false;
            grdCotizaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdCotizaciones.AllowUserToAddRows = false;
            grdCotizaciones.AllowUserToDeleteRows = false;

            grdCotizaciones.Columns.Add("Numero", "N° Cotización");
            grdCotizaciones.Columns.Add("Cliente", "Cliente");
            grdCotizaciones.Columns.Add("DescripcionCarga", "Descripción de Carga");
            grdCotizaciones.Columns.Add("TipoCarga", "Tipo de Carga");
            grdCotizaciones.Columns.Add("TipoContenedor", "Tipo de Contenedor");
            grdCotizaciones.Columns.Add("MontoTotal", "Monto Total (USD)");
            grdCotizaciones.Columns.Add("Estado", "Estado");
            grdCotizaciones.Columns.Add("FechaEmision", "Fecha Emisión");
            grdCotizaciones.Columns.Add("FechaVencimiento", "Fecha Vencimiento");

            foreach (DataGridViewColumn columna in grdCotizaciones.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            grdCotizaciones.ColumnHeaderMouseClick -= grdCotizaciones_ColumnHeaderMouseClick;
            grdCotizaciones.ColumnHeaderMouseClick += grdCotizaciones_ColumnHeaderMouseClick;

            grdCotizaciones.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            grdCotizaciones.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
        }

        #endregion

        #region "Eventos de grilla"

        private void grdCotizaciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            cotizacionSeleccionada = grdCotizaciones.Rows[e.RowIndex].Tag as BECotizacion;

            // Habilitar aprobación SOLO si es aprobador y estado pendiente 
            string codPend = EstadoCotizacion.PendienteAprobacion.Codigo;

            bool habilitar = esUsuarioAutorizado
                && cotizacionSeleccionada != null
                && cotizacionSeleccionada.Estado != null
                && cotizacionSeleccionada.Estado.Codigo == codPend;

            btnAprobar.Enabled = habilitar;
            btnRechazar.Enabled = habilitar;
        }


        private void grdCotizaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            cotizacionSeleccionada = grdCotizaciones.Rows[e.RowIndex].Tag as BECotizacion;
            if (cotizacionSeleccionada == null) return;

            if (ModoSeleccion)
            {
                if (cotizacionSeleccionada.Estado != null && cotizacionSeleccionada.Estado.Codigo == EstadoCotizacion.Aceptada.Codigo)

                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Solo se pueden seleccionar cotizaciones ACEPTADAS.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (esUsuarioAutorizado)
                {
                    MessageBox.Show(
                        "Modo aprobación activo.\nUse los botones Aprobar / Rechazar.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
                }
                btnModificar_Click(sender, e);
            }
        }

        private void grdCotizaciones_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = grdCotizaciones.Columns[e.ColumnIndex].Name;

            // Orden manual: la grilla se carga con Rows.Add, por eso ordenamos las cotizaciones antes de redibujar.
            if (columnaOrdenCotizacion == columna)
                ordenCotizacionAscendente = !ordenCotizacionAscendente;
            else
            {
                columnaOrdenCotizacion = columna;
                ordenCotizacionAscendente = true;
            }

            RefrescarGrilla(txtBuscar.Text.Trim().ToLower());
        }

        #endregion

        #region "Métodos auxiliares"

        private void RefrescarGrilla(string filtro = "")
        {
            try
            {
                grdCotizaciones.Rows.Clear();
                listaCotizaciones = gestorCotizacion.ListarTodo();

                var filtradas = listaCotizaciones
                    .Where(c => string.IsNullOrEmpty(filtro) ||
                                (c.Cliente?.RazonSocial ?? "").ToLower().Contains(filtro))
                    .ToList();

                filtradas = OrdenarCotizaciones(filtradas);

                foreach (var c in filtradas)
                {
                    string numero = !string.IsNullOrWhiteSpace(c.Numero) ? c.Numero : "-";
                    string cliente = c.Cliente?.ToString() ?? "(sin cliente)";
                    string tipoCarga = c.Cargas?.FirstOrDefault()?.Tipo?.Descripcion ?? "-";
                    string descripcionCarga = c.Cargas != null && c.Cargas.Count > 0
                        ? string.Join(", ", c.Cargas.Select(ca => ca.Descripcion))
                        : "-";
                    string tipoContenedor = c.TipoContenedor?.Descripcion ?? "-";
                    string monto = $"USD {c.MontoTotal:N2}";
                    string estado = c.Estado?.Descripcion ?? "-";
                    string fechaE = c.FechaEmision.ToString("dd/MM/yyyy");
                    string fechaV = c.FechaVencimiento.ToString("dd/MM/yyyy");

                    int fila = grdCotizaciones.Rows.Add(numero, cliente, descripcionCarga, tipoCarga, tipoContenedor,
                    monto, estado, fechaE, fechaV);

                    grdCotizaciones.Rows[fila].Tag = c;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al refrescar grilla: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<BECotizacion> OrdenarCotizaciones(List<BECotizacion> cotizaciones)
        {
            Func<BECotizacion, object> criterio;

            switch (columnaOrdenCotizacion)
            {
                case "Numero":
                    criterio = c => c.Numero ?? string.Empty;
                    break;
                case "Cliente":
                    criterio = c => c.Cliente?.RazonSocial ?? string.Empty;
                    break;
                case "DescripcionCarga":
                    criterio = c => c.Cargas != null && c.Cargas.Count > 0
                        ? string.Join(", ", c.Cargas.Select(ca => ca.Descripcion))
                        : string.Empty;
                    break;
                case "TipoCarga":
                    criterio = c => c.Cargas?.FirstOrDefault()?.Tipo?.Descripcion ?? string.Empty;
                    break;
                case "TipoContenedor":
                    criterio = c => c.TipoContenedor?.Descripcion ?? string.Empty;
                    break;
                case "MontoTotal":
                    criterio = c => c.MontoTotal;
                    break;
                case "Estado":
                    criterio = c => c.Estado?.Descripcion ?? string.Empty;
                    break;
                case "FechaEmision":
                    criterio = c => c.FechaEmision;
                    break;
                case "FechaVencimiento":
                    criterio = c => c.FechaVencimiento;
                    break;
                default:
                    criterio = c => c.Id;
                    break;
            }

            return ordenCotizacionAscendente
                ? cotizaciones.OrderBy(criterio).ToList()
                : cotizaciones.OrderByDescending(criterio).ToList();
        }
        private bool EsUsuarioAprobador()
        {
            if (seguridadBLL == null)
                return false;

            var usuario = seguridadBLL.UsuarioActual;
            if (usuario == null || usuario.Roles == null)
                return false;

            if (usuario.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                return true;

            return usuario.Roles.Any(r =>
                r != null &&
                (r.Nombre == "Jefe de Ventas y Cotizaciones"
                || r.Nombre == "Director General"));
        }

        private void ConfigurarModoPorRol()
        {
            // ABM disponible para ambos
            btnNueva.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;

            // Aprobación solo para jefe
            btnAprobar.Visible = esUsuarioAutorizado;
            btnRechazar.Visible = esUsuarioAutorizado;
        }





        #endregion


    }
}
