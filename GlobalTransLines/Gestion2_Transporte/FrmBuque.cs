using BE.Dominio;
using BE.Transporte;
using BE.Infra;
using BLL.Infra;
using BLL.Transporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Gestion2_Transporte
{
    public partial class FrmBuque : Form
    {
        #region "Atributos y Constructor"

        private readonly BuqueBLL gestorBuque = new BuqueBLL();
        private readonly DireccionBLL gestorDireccion = new DireccionBLL();
        private BEBuque buqueSeleccionado;
        private List<BEDireccion> catalogoPuertos;
        private Button btnGestionarPuertos;
        private ComboBox cmbPuertoItinerario;
        public bool ModoSeleccion { get; set; }
        private string columnaOrdenBuque = "Nombre";
        private bool ordenBuqueAscendente = true;

        public FrmBuque()
        {
            InitializeComponent();
            ModoSeleccion = false;
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmBuque_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text += " | " + AppDomain.CurrentDomain.BaseDirectory;

                txtDisponibilidad.ReadOnly = true;
                txtDisponibilidad.TabStop = false;
                txtDisponibilidad.BackColor = System.Drawing.Color.Gainsboro; // opcional visual

                txtNumero.ReadOnly = true;
                txtNumero.TabStop = false;

                CargarCombos();
                ConfigurarSelectorPuertoItinerario();
                CargarCatalogoPuertos();
                ConfigurarBotonGestionarPuertos();
                ConfigurarGrilla(grdBuques);
                RefrescarGrilla();

                cmbEstado.SelectedIndex = -1;

                if (ModoSeleccion)
                {
                    btnGuardar.Visible = false;
                    btnModificar.Visible = false;
                    btnEliminar.Visible = false;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar formulario de buques: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese el nombre del buque.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCodigoIMO.Text) || txtCodigoIMO.Text.Length != 7 || !txtCodigoIMO.Text.All(char.IsDigit))
                {
                    MessageBox.Show("El Código IMO debe tener exactamente 7 dígitos numéricos.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var estadoSel = cmbEstado.SelectedItem as EstadoBuque;
                if (estadoSel == null)
                {
                    MessageBox.Show("Seleccione un estado válido para el buque.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nuevo = new BEBuque
                {
                    Nombre = txtNombre.Text.Trim(),
                    CodigoIMO = txtCodigoIMO.Text.Trim(),
                    CapacidadTEU = (int)numCapacidadTEU.Value,
                    CapacidadPeso = numCapacidadPeso.Value,
                    Estado = estadoSel,
                    ItinerarioPuertoIds = lstItinerarios.Items
                        .Cast<BEDireccion>()
                        .Select(p => p.Id)
                        .ToList()
                };

                gestorBuque.Guardar(nuevo);
                RefrescarGrilla();

                MessageBox.Show("Buque agregado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar buque: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (buqueSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un buque para modificar.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("Ingrese el nombre del buque.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var estadoSel = cmbEstado.SelectedItem as EstadoBuque;
                if (estadoSel == null)
                {
                    MessageBox.Show("Seleccione un estado válido para el buque.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Actualizar propiedades
                buqueSeleccionado.Nombre = txtNombre.Text.Trim();
                buqueSeleccionado.CodigoIMO = txtCodigoIMO.Text.Trim();
                buqueSeleccionado.CapacidadTEU = (int)numCapacidadTEU.Value;
                buqueSeleccionado.CapacidadPeso = numCapacidadPeso.Value;
                buqueSeleccionado.Estado = estadoSel;
                buqueSeleccionado.ItinerarioPuertoIds = lstItinerarios.Items
                    .Cast<BEDireccion>()
                    .Select(p => p.Id)
                    .ToList();

                gestorBuque.Guardar(buqueSeleccionado);
                RefrescarGrilla();

                MessageBox.Show("Buque modificado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar buque: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (buqueSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un buque para eliminar.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult res = MessageBox.Show("¿Confirma la eliminación del buque seleccionado?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.No)
                    return;

                gestorBuque.Eliminar(buqueSeleccionado);
                RefrescarGrilla();

                MessageBox.Show("Buque eliminado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar buque: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string filtro = txtBuscar.Text.Trim().ToLower();
                var lista = gestorBuque.ListarTodo();

                var estadoSel = cmbEstado.SelectedItem as EstadoBuque;
                if (estadoSel != null)
                    lista = lista.Where(b => b.Estado.Codigo == estadoSel.Codigo).ToList();

                if (!string.IsNullOrEmpty(filtro))
                    lista = lista.Where(b =>
                        (b.Nombre ?? "").ToLower().Contains(filtro) ||
                        (b.CodigoIMO ?? "").ToLower().Contains(filtro))
                        .ToList();

                if (lista.Count == 0)
                {
                    MessageBox.Show("No se encontraron buques que coincidan con el criterio de búsqueda.",
                        "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    grdBuques.DataSource = null;
                    return;
                }

                grdBuques.DataSource = null;
                grdBuques.DataSource = lista;

                PintarFilasPorEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar buques: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region "Eventos de botones - Itinerario"

        private void btnAgregarItinerario_Click(object sender, EventArgs e)
        {
            var puertoSeleccionado = cmbPuertoItinerario.SelectedItem as BEDireccion;

            if (puertoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un puerto del catálogo antes de agregar.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lstItinerarios.Items.Cast<BEDireccion>().Any(p => p.Id == puertoSeleccionado.Id))
            {
                MessageBox.Show("Ese puerto ya fue agregado al itinerario.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lstItinerarios.Items.Add(puertoSeleccionado);
            cmbPuertoItinerario.SelectedIndex = -1;
            cmbPuertoItinerario.Focus();
        }

        private void btnEliminarItinerario_Click(object sender, EventArgs e)
        {
            if (lstItinerarios.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un puerto para eliminar.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lstItinerarios.Items.RemoveAt(lstItinerarios.SelectedIndex);
        }

        private void btnGuardarItinerario_Click(object sender, EventArgs e)
        {
            if (lstItinerarios.Items.Count == 0)
            {
                MessageBox.Show("No hay puertos en el itinerario para guardar.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (buqueSeleccionado != null)
            {
                buqueSeleccionado.ItinerarioPuertoIds = lstItinerarios.Items
                    .Cast<BEDireccion>()
                    .Select(p => p.Id)
                    .ToList();
                gestorBuque.Guardar(buqueSeleccionado);
                RefrescarGrilla();
                MessageBox.Show("Itinerario guardado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Primero debe guardar o seleccionar un buque.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        private void btnGestionarPuertos_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmPuerto())
            {
                frm.ShowDialog();
            }

            CargarCatalogoPuertos();
        }
        #endregion

        #region "Eventos de grilla"

        private void grdBuques_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            buqueSeleccionado = (BEBuque)grdBuques.CurrentRow.DataBoundItem;
            if (buqueSeleccionado == null) return;

            txtNumero.Text = buqueSeleccionado.Codigo;
            txtNombre.Text = buqueSeleccionado.Nombre;
            txtCodigoIMO.Text = buqueSeleccionado.CodigoIMO;
            numCapacidadTEU.Value = buqueSeleccionado.CapacidadTEU;
            numCapacidadPeso.Value = buqueSeleccionado.CapacidadPeso;
            txtDisponibilidad.Text = buqueSeleccionado.Disponibilidad.ToString("0.##");
            cmbEstado.SelectedItem = buqueSeleccionado.Estado;

            lstItinerarios.Items.Clear();
            foreach (var puerto in buqueSeleccionado.Itinerario ?? new List<BEDireccion>())
                lstItinerarios.Items.Add(puerto);
        }

        #endregion

        #region "Configuración Visual"

        private void ConfigurarGrilla(DataGridView grilla)
        {
            grilla.AutoGenerateColumns = true;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.MultiSelect = false;
            grilla.ReadOnly = true;
            grilla.RowHeadersVisible = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);

            grilla.ColumnHeaderMouseClick -= grdBuques_ColumnHeaderMouseClick;
            grilla.ColumnHeaderMouseClick += grdBuques_ColumnHeaderMouseClick;
        }

        private void ConfigurarBotonGestionarPuertos()
        {
            if (btnGestionarPuertos != null)
                return;

            btnGestionarPuertos = new Button
            {
                Name = "btnGestionarPuertos",
                Text = "Gestionar puertos",
                Location = new System.Drawing.Point(334, 27),
                Size = new System.Drawing.Size(125, 28),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                UseVisualStyleBackColor = true
            };

            btnGestionarPuertos.Click += btnGestionarPuertos_Click;
            groupBox3.Controls.Add(btnGestionarPuertos);
        }

        private void ConfigurarSelectorPuertoItinerario()
        {
            if (cmbPuertoItinerario != null)
                return;

            txtPuerto.Visible = false;

            cmbPuertoItinerario = new ComboBox
            {
                Name = "cmbPuertoItinerario",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = txtPuerto.Location,
                Size = txtPuerto.Size,
                DropDownWidth = 360,
                Anchor = txtPuerto.Anchor
            };

            groupBox3.Controls.Add(cmbPuertoItinerario);
            cmbPuertoItinerario.BringToFront();
        }

        #endregion

        #region "Orden de grilla"

        private void grdBuques_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = grdBuques.Columns[e.ColumnIndex].Name;
            ordenBuqueAscendente = columnaOrdenBuque == columna ? !ordenBuqueAscendente : true;
            columnaOrdenBuque = columna;
            OrdenarBuques();
        }

        private void OrdenarBuques()
        {
            var lista = (grdBuques.DataSource as IEnumerable<BEBuque>)?.ToList();
            if (lista == null)
                return;

            // Ordena la lista visible para conservar filtros aplicados por busqueda o estado.
            switch (columnaOrdenBuque)
            {
                case "Id":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => b.Id).ToList() : lista.OrderByDescending(b => b.Id).ToList();
                    break;
                case "Codigo":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => b.Codigo).ToList() : lista.OrderByDescending(b => b.Codigo).ToList();
                    break;
                case "CodigoIMO":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => b.CodigoIMO).ToList() : lista.OrderByDescending(b => b.CodigoIMO).ToList();
                    break;
                case "CapacidadTEU":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => b.CapacidadTEU).ToList() : lista.OrderByDescending(b => b.CapacidadTEU).ToList();
                    break;
                case "CapacidadPeso":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => b.CapacidadPeso).ToList() : lista.OrderByDescending(b => b.CapacidadPeso).ToList();
                    break;
                case "Disponibilidad":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => b.Disponibilidad).ToList() : lista.OrderByDescending(b => b.Disponibilidad).ToList();
                    break;
                case "Estado":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => b.Estado?.Descripcion).ToList() : lista.OrderByDescending(b => b.Estado?.Descripcion).ToList();
                    break;
                case "Activo":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => b.Activo).ToList() : lista.OrderByDescending(b => b.Activo).ToList();
                    break;
                default:
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => b.Nombre).ToList() : lista.OrderByDescending(b => b.Nombre).ToList();
                    break;
            }

            grdBuques.DataSource = null;
            grdBuques.DataSource = lista;
            PintarFilasPorEstado();
        }

        #endregion

        #region "Métodos auxiliares"

        private void RefrescarGrilla()
        {
            var lista = gestorBuque.ListarTodo();
            grdBuques.DataSource = null;
            grdBuques.DataSource = lista;
            grdBuques.AutoResizeColumns();
            PintarFilasPorEstado();
        }

        private void PintarFilasPorEstado()
        {
            // El color acompaña el estado operativo del buque sin cambiar la lógica de negocio.
            foreach (DataGridViewRow fila in grdBuques.Rows)
            {
                var b = fila.DataBoundItem as BEBuque;
                if (b != null)
                {
                    if (b.Estado == EstadoBuque.EnRuta)
                        fila.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                    else if (b.Estado == EstadoBuque.Mantenimiento)
                        fila.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                    else
                        fila.DefaultCellStyle.BackColor = System.Drawing.Color.Honeydew;
                }
            }
        }

        private void CargarCombos()
        {
            // EstadoBuque debe exponer ObtenerTodos() en Dominio
            cmbEstado.DataSource = EstadoBuque.ObtenerTodos().ToList();
            cmbEstado.DisplayMember = "Descripcion";
            cmbEstado.ValueMember = "Codigo";
        }

        private void CargarCatalogoPuertos()
        {
            catalogoPuertos = gestorDireccion.ListarPuertos()
                .GroupBy(p => p.Id)
                .Select(g => g.First())
                .OrderBy(p => p.Pais)
                .ThenBy(p => p.Puerto)
                .ThenBy(p => p.Ciudad)
                .ToList();

            var sugerencias = new AutoCompleteStringCollection();
            sugerencias.AddRange(catalogoPuertos
                .Select(p => p.Puerto)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(p => p)
                .ToArray());

            txtPuerto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtPuerto.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtPuerto.AutoCompleteCustomSource = sugerencias;

            if (cmbPuertoItinerario != null)
            {
                cmbPuertoItinerario.DataSource = null;
                cmbPuertoItinerario.DataSource = catalogoPuertos;
                cmbPuertoItinerario.SelectedIndex = -1;
            }
        }

        private void LimpiarCampos()
        {
            txtNumero.Clear();
            txtNombre.Clear();
            txtCodigoIMO.Clear();
            numCapacidadTEU.Value = 0;
            numCapacidadPeso.Value = 0;
            txtDisponibilidad.Clear();
            cmbEstado.SelectedIndex = -1;
            lstItinerarios.Items.Clear();
            buqueSeleccionado = null;
        }

        #endregion

       
    }
}


