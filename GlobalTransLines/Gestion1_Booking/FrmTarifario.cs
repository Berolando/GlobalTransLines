using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BE.Dominio;
using BE.Finanzas;
using BLL.Finanzas;
using BLL.Infra;
using BLL.Transporte;

namespace GlobalTransLines.UI.Gestion1_Booking
{
    public partial class FrmTarifario : Form
    {
        #region "Atributos y constructor"

        private readonly TarifarioBLL tarifarioBLL = new TarifarioBLL();
        private readonly ContenedorBLL contenedorBLL = new ContenedorBLL();
        private readonly BitacoraBLL bitacoraBLL = new BitacoraBLL();
        private readonly SeguridadBLL seguridadBLL;
        private List<BETarifa> tarifas = new List<BETarifa>();
        private BETarifa tarifaSeleccionada;
        private bool configurandoCombos;
        private string columnaOrdenTarifa = "TipoCarga";
        private bool ordenTarifaAscendente = true;

        public FrmTarifario()
        {
            InitializeComponent();
        }

        public FrmTarifario(SeguridadBLL seguridad) : this()
        {
            seguridadBLL = seguridad;
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmTarifario_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarCombos();
                ConfigurarGrilla();
                RefrescarGrilla();
                PrepararNuevaTarifa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar tarifario: " + ex.Message,
                    "Tarifario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de grilla y botones"

        private void dgvTarifas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTarifas.CurrentRow == null || dgvTarifas.CurrentRow.Cells["Id"].Value == null)
                return;

            long id = Convert.ToInt64(dgvTarifas.CurrentRow.Cells["Id"].Value);
            tarifaSeleccionada = tarifas.FirstOrDefault(t => t.Id == id);
            CargarTarifaSeleccionada();
        }

        private void dgvTarifas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = dgvTarifas.Columns[e.ColumnIndex].Name;

            // Orden manual: el DataSource es una lista proyectada, por eso se reordena desde la lista real de tarifas.
            if (columnaOrdenTarifa == columna)
                ordenTarifaAscendente = !ordenTarifaAscendente;
            else
            {
                columnaOrdenTarifa = columna;
                ordenTarifaAscendente = true;
            }

            CargarTarifasEnGrilla();
        }

        private void cmbTipoCarga_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!configurandoCombos && tarifaSeleccionada == null)
                CargarContenedoresCompatibles(cmbTipoCarga.SelectedItem as TipoCarga);
        }

        private void btnNueva_Click(object sender, EventArgs e)
        {
            PrepararNuevaTarifa();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool esAlta = tarifaSeleccionada == null;
                var tarifa = ConstruirTarifaDesdeFormulario();
                tarifarioBLL.Guardar(tarifa);

                RegistrarEvento(
                    esAlta ? TipoOperacion.Alta : TipoOperacion.Modificacion,
                    (esAlta ? "Alta" : "Modificación") + " de tarifa " +
                    tarifa.TipoCarga.Codigo + " + " + tarifa.TipoContenedor.Codigo + ".");

                MessageBox.Show(esAlta
                        ? "Tarifa registrada correctamente."
                        : "Tarifa actualizada correctamente.",
                    "Tarifario", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefrescarGrilla();
                SeleccionarTarifa(tarifa.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar tarifa: " + ObtenerMensaje(ex),
                    "Tarifario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            try
            {
                if (tarifaSeleccionada == null)
                {
                    MessageBox.Show("Seleccione una tarifa.",
                        "Tarifario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool activar = !tarifaSeleccionada.Activo;
                string accion = activar ? "reactivar" : "desactivar";
                var confirmacion = MessageBox.Show(
                    "¿Confirma " + accion + " la tarifa seleccionada?",
                    "Tarifario", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes)
                    return;

                if (activar)
                    tarifarioBLL.Reactivar(tarifaSeleccionada);
                else
                    tarifarioBLL.Eliminar(tarifaSeleccionada);

                RegistrarEvento(
                    activar ? TipoOperacion.Modificacion : TipoOperacion.Baja,
                    (activar ? "Reactivación" : "Desactivación") + " de tarifa " +
                    tarifaSeleccionada.TipoCarga.Codigo + " + " +
                    tarifaSeleccionada.TipoContenedor.Codigo + ".");

                long id = tarifaSeleccionada.Id;
                RefrescarGrilla();
                SeleccionarTarifa(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar el estado de la tarifa: " + ObtenerMensaje(ex),
                    "Tarifario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            RefrescarGrilla();
            PrepararNuevaTarifa();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            PrepararNuevaTarifa();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region "Carga visual"

        private void ConfigurarCombos()
        {
            configurandoCombos = true;
            cmbTipoCarga.DataSource = TipoCarga.ObtenerTodos()
                .Where(t => t.Codigo != TipoCarga.LCL.Codigo)
                .ToList();
            configurandoCombos = false;

            CargarContenedoresCompatibles(cmbTipoCarga.SelectedItem as TipoCarga);
        }

        private void CargarContenedoresCompatibles(TipoCarga tipoCarga)
        {
            configurandoCombos = true;
            cmbTipoContenedor.DataSource = tipoCarga == null
                ? new List<TipoContenedor>()
                : contenedorBLL.ObtenerTiposCompatibles(tipoCarga);
            configurandoCombos = false;
        }

        private void ConfigurarGrilla()
        {
            dgvTarifas.AutoGenerateColumns = false;
            dgvTarifas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTarifas.MultiSelect = false;
            dgvTarifas.AllowUserToAddRows = false;
            dgvTarifas.AllowUserToDeleteRows = false;
            dgvTarifas.ReadOnly = true;
            dgvTarifas.RowHeadersVisible = false;
            dgvTarifas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvTarifas.Columns.Clear();
            dgvTarifas.Columns.Add(CrearColumna("Id", "Id", 50));
            dgvTarifas.Columns.Add(CrearColumna("TipoCarga", "Tipo carga", 120));
            dgvTarifas.Columns.Add(CrearColumna("TipoContenedor", "Contenedor", 110));
            dgvTarifas.Columns.Add(CrearColumna("Descripcion", "Descripción", 240));
            dgvTarifas.Columns.Add(CrearColumna("BaseUSD", "Base USD", 95));
            dgvTarifas.Columns.Add(CrearColumna("CoefPeso", "USD/ton", 85));
            dgvTarifas.Columns.Add(CrearColumna("CoefVolumen", "USD/m³", 85));
            dgvTarifas.Columns.Add(CrearColumna("Estado", "Estado", 75));

            foreach (DataGridViewColumn columna in dgvTarifas.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            dgvTarifas.ColumnHeaderMouseClick -= dgvTarifas_ColumnHeaderMouseClick;
            dgvTarifas.ColumnHeaderMouseClick += dgvTarifas_ColumnHeaderMouseClick;

            dgvTarifas.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            dgvTarifas.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
        }

        private DataGridViewTextBoxColumn CrearColumna(string nombre, string texto, int ancho)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = nombre,
                HeaderText = texto,
                DataPropertyName = nombre,
                MinimumWidth = ancho
            };
        }

        private void RefrescarGrilla()
        {
            tarifas = tarifarioBLL.ListarTodo()
                .ToList();

            CargarTarifasEnGrilla();
        }

        private void CargarTarifasEnGrilla()
        {
            var tarifasOrdenadas = OrdenarTarifas(tarifas);

            dgvTarifas.DataSource = tarifasOrdenadas
                .Select(t => new
                {
                    t.Id,
                    TipoCarga = t.TipoCarga != null ? t.TipoCarga.Codigo : string.Empty,
                    TipoContenedor = t.TipoContenedor != null ? t.TipoContenedor.Codigo : string.Empty,
                    t.Descripcion,
                    BaseUSD = t.BaseUSD.ToString("N2"),
                    CoefPeso = t.CoefPeso.ToString("N2"),
                    CoefVolumen = t.CoefVolumen.ToString("N2"),
                    Estado = t.Activo ? "Activa" : "Inactiva"
                })
                .ToList();

            dgvTarifas.ClearSelection();
        }

        private List<BETarifa> OrdenarTarifas(List<BETarifa> lista)
        {
            Func<BETarifa, object> criterio;

            switch (columnaOrdenTarifa)
            {
                case "Id":
                    criterio = t => t.Id;
                    break;
                case "TipoCarga":
                    criterio = t => t.TipoCarga != null ? t.TipoCarga.Codigo : string.Empty;
                    break;
                case "TipoContenedor":
                    criterio = t => t.TipoContenedor != null ? t.TipoContenedor.Codigo : string.Empty;
                    break;
                case "Descripcion":
                    criterio = t => t.Descripcion ?? string.Empty;
                    break;
                case "BaseUSD":
                    criterio = t => t.BaseUSD;
                    break;
                case "CoefPeso":
                    criterio = t => t.CoefPeso;
                    break;
                case "CoefVolumen":
                    criterio = t => t.CoefVolumen;
                    break;
                case "Estado":
                    criterio = t => t.Activo;
                    break;
                default:
                    criterio = t => t.TipoCarga != null ? t.TipoCarga.Codigo : string.Empty;
                    break;
            }

            return ordenTarifaAscendente
                ? lista.OrderBy(criterio).ToList()
                : lista.OrderByDescending(criterio).ToList();
        }

        private void CargarTarifaSeleccionada()
        {
            if (tarifaSeleccionada == null)
                return;

            configurandoCombos = true;
            txtId.Text = tarifaSeleccionada.Id.ToString();
            SeleccionarComboPorCodigo(cmbTipoCarga,
                tarifaSeleccionada.TipoCarga != null ? tarifaSeleccionada.TipoCarga.Codigo : string.Empty);
            CargarContenedoresCompatibles(tarifaSeleccionada.TipoCarga);
            SeleccionarComboPorCodigo(cmbTipoContenedor,
                tarifaSeleccionada.TipoContenedor != null ? tarifaSeleccionada.TipoContenedor.Codigo : string.Empty);
            configurandoCombos = false;

            txtDescripcion.Text = tarifaSeleccionada.Descripcion;
            txtBaseUSD.Text = tarifaSeleccionada.BaseUSD.ToString("N2");
            txtCoefPeso.Text = tarifaSeleccionada.CoefPeso.ToString("N2");
            txtCoefVolumen.Text = tarifaSeleccionada.CoefVolumen.ToString("N2");

            cmbTipoCarga.Enabled = false;
            cmbTipoContenedor.Enabled = false;
            btnGuardar.Text = "Modificar";
            btnGuardar.Enabled = tarifaSeleccionada.Activo;
            btnEstado.Enabled = true;
            btnEstado.Text = tarifaSeleccionada.Activo ? "Desactivar" : "Reactivar";
        }

        private void PrepararNuevaTarifa()
        {
            tarifaSeleccionada = null;
            txtId.Clear();
            txtDescripcion.Clear();
            txtBaseUSD.Clear();
            txtCoefPeso.Clear();
            txtCoefVolumen.Clear();
            cmbTipoCarga.Enabled = true;
            cmbTipoContenedor.Enabled = true;
            btnGuardar.Text = "Guardar";
            btnGuardar.Enabled = true;
            btnEstado.Text = "Desactivar";
            btnEstado.Enabled = false;
            dgvTarifas.ClearSelection();

            if (cmbTipoCarga.Items.Count > 0)
                cmbTipoCarga.SelectedIndex = 0;

            CargarContenedoresCompatibles(cmbTipoCarga.SelectedItem as TipoCarga);
        }

        private void SeleccionarTarifa(long id)
        {
            foreach (DataGridViewRow row in dgvTarifas.Rows)
            {
                if (Convert.ToInt64(row.Cells["Id"].Value) == id)
                {
                    row.Selected = true;
                    dgvTarifas.CurrentCell = row.Cells["Descripcion"];
                    break;
                }
            }
        }

        private void SeleccionarComboPorCodigo(ComboBox combo, string codigo)
        {
            foreach (var item in combo.Items)
            {
                var tipoCarga = item as TipoCarga;
                if (tipoCarga != null && tipoCarga.Codigo == codigo)
                {
                    combo.SelectedItem = item;
                    return;
                }

                var tipoContenedor = item as TipoContenedor;
                if (tipoContenedor != null && tipoContenedor.Codigo == codigo)
                {
                    combo.SelectedItem = item;
                    return;
                }
            }
        }

        #endregion

        #region "Construcción y auditoría"

        private BETarifa ConstruirTarifaDesdeFormulario()
        {
            return new BETarifa
            {
                Id = tarifaSeleccionada != null ? tarifaSeleccionada.Id : 0,
                TipoCarga = cmbTipoCarga.SelectedItem as TipoCarga,
                TipoContenedor = cmbTipoContenedor.SelectedItem as TipoContenedor,
                Descripcion = txtDescripcion.Text.Trim(),
                BaseUSD = ObtenerDecimal(txtBaseUSD.Text, "Base USD"),
                CoefPeso = ObtenerDecimal(txtCoefPeso.Text, "USD por tonelada"),
                CoefVolumen = ObtenerDecimal(txtCoefVolumen.Text, "USD por m³"),
                Activo = tarifaSeleccionada == null || tarifaSeleccionada.Activo
            };
        }

        private decimal ObtenerDecimal(string valor, string campo)
        {
            decimal numero;

            if (decimal.TryParse(valor, NumberStyles.Any, CultureInfo.CurrentCulture, out numero))
                return numero;

            if (decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out numero))
                return numero;

            throw new Exception("El campo " + campo + " debe ser numérico.");
        }

        private void RegistrarEvento(TipoOperacion tipo, string descripcion)
        {
            if (seguridadBLL == null || seguridadBLL.UsuarioActual == null)
                return;

            bitacoraBLL.RegistrarEvento(
                seguridadBLL.UsuarioActual,
                tipo,
                descripcion,
                "Finanzas/Tarifario");
        }

        private string ObtenerMensaje(Exception ex)
        {
            Exception actual = ex;
            while (actual.InnerException != null)
                actual = actual.InnerException;

            return actual.Message;
        }

        #endregion
    }
}



