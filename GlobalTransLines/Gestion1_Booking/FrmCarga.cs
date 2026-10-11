using BE.Dominio;
using BE.Infra;
using BE.Transporte;
using BLL.Infra;
using BLL.Transporte;
using GlobalTransLines.UI.Gestion2_Transporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Gestion1_Booking
{
    public partial class FrmCarga : Form
    {
        #region "Atributos y Constructor"

        private readonly CargaBLL gestorCarga = new CargaBLL();
        private readonly ContenedorBLL gestorContenedor = new ContenedorBLL();
        private readonly DireccionBLL gestorDireccion = new DireccionBLL();
        public BECarga cargaSeleccionada { get; private set; }
        private List<BECarga> listaCargas;
        public bool ModoSeleccion { get; set; }
        private Button btnCerrar;
        private Button btnGestionarPuertos;
        private ComboBox cmbPuertoDestino;
        private bool volumenEditadoManual;
        private bool actualizandoVolumenAutomatico;
        private string columnaOrdenCarga = "Id";
        private bool ordenCargaAscendente;

        public FrmCarga()
        {
            InitializeComponent();
        }

        public FrmCarga(bool modoSeleccion) : this()
        {
            ModoSeleccion = modoSeleccion;
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmCarga_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrilla(grdCargas);
                ConfigurarBotonCerrar();
                CargarTiposCombos();
                CargarPuertosEmbarque();
                ConfigurarPuertoDestino();
                ConfigurarBotonGestionarPuertos();
                RefrescarGrilla();
                LimpiarControles();
                ConfigurarModoSeleccion();
                ConfigurarValidaciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el formulario: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                var carga = MapearDesdeControles();
                gestorCarga.Guardar(carga);

                RefrescarGrilla();
                LimpiarControles();
                numPeso.Focus(); 
                decimal peso = numPeso.Value;
                decimal volumen = numVolumen.Value;

                MessageBox.Show("Carga registrada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar carga: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cargaSeleccionada == null)
                {
                    MessageBox.Show("Seleccione una carga para modificar.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos())
                    return;

                var cargaModificada = MapearDesdeControles();
                cargaModificada.Id = cargaSeleccionada.Id;
                gestorCarga.Guardar(cargaModificada);

                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Carga modificada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar carga: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cargaSeleccionada == null)
                {
                    MessageBox.Show("Seleccione una carga para eliminar.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show("¿Confirma la eliminación de la carga seleccionada?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.No)
                    return;

                gestorCarga.Eliminar(cargaSeleccionada);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Carga eliminada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar carga: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string filtro = txtBuscar.Text.Trim().ToLower();
                RefrescarGrilla(filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar cargas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            grilla.Columns.Add("Codigo", "Código");
            grilla.Columns.Add("Descripcion", "Descripción");
            grilla.Columns.Add("Tipo", "Tipo");
            grilla.Columns.Add("TipoContenedor", "Contenedor");
            grilla.Columns.Add("Estado", "Estado");
            grilla.Columns.Add("Peso", "Peso kg");
            grilla.Columns.Add("Volumen", "Volumen m3");
            grilla.Columns.Add("Origen", "Origen terrestre");
            grilla.Columns.Add("PuertoEmbarque", "Puerto de embarque");
            grilla.Columns.Add("Destino", "Destino");
            grilla.Columns.Add("Activo", "Activo");

            foreach (DataGridViewColumn columna in grilla.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            grilla.ColumnHeaderMouseClick -= grdCargas_ColumnHeaderMouseClick;
            grilla.ColumnHeaderMouseClick += grdCargas_ColumnHeaderMouseClick;

            grilla.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
        }

        private void ConfigurarBotonCerrar()
        {
            if (btnCerrar != null)
                return;

            btnCerrar = new Button
            {
                Name = "btnCerrar",
                Text = "Cerrar",
                Size = new System.Drawing.Size(150, 28),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                UseVisualStyleBackColor = true
            };

            btnCerrar.Click += btnCerrar_Click;
            groupBox4.Controls.Add(btnCerrar);
            groupBox4.Resize += groupBox4_Resize;
            UbicarBotonCerrar();
        }

        private void groupBox4_Resize(object sender, EventArgs e)
        {
            UbicarBotonCerrar();
        }

        private void UbicarBotonCerrar()
        {
            if (btnCerrar == null)
                return;

            int margenDerecho = 18;
            int margenSuperior = 24;
            int x = Math.Max(margenDerecho, groupBox4.ClientSize.Width - btnCerrar.Width - margenDerecho);

            btnCerrar.Location = new System.Drawing.Point(x, margenSuperior);
            btnCerrar.BringToFront();
        }

        private void ConfigurarBotonGestionarPuertos()
        {
            if (btnGestionarPuertos != null)
                return;

            btnGestionarPuertos = new Button
            {
                Name = "btnGestionarPuertos",
                Text = "Gestionar puertos",
                Size = new System.Drawing.Size(150, 28),
                Location = new System.Drawing.Point(18, 76),
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                UseVisualStyleBackColor = true
            };

            btnGestionarPuertos.Click += btnGestionarPuertos_Click;
            groupBox3.Controls.Add(btnGestionarPuertos);
        }

        private void ConfigurarModoSeleccion()
        {
            if (ModoSeleccion)
            {
                btnGuardar.Visible = true;
                btnModificar.Visible = true;
                btnEliminar.Visible = true;
            }
        }

        private void CargarTiposCombos()
        {
            cmbTipoCarga.DataSource = TipoCarga.ObtenerTodos()
                .Where(t => t.Codigo != TipoCarga.FCL.Codigo &&
                            t.Codigo != TipoCarga.LCL.Codigo)
                .ToList();
            cmbTipoCarga.DisplayMember = "Descripcion";
            cmbTipoCarga.ValueMember = "Codigo";
            cmbTipoContenedor.DisplayMember = "Descripcion";
            cmbTipoContenedor.ValueMember = "Codigo";
            CargarContenedoresCompatibles();
        }

        #endregion

        #region "Eventos de grilla"

        private void grdCargas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                cargaSeleccionada = grdCargas.Rows[e.RowIndex].Tag as BECarga;
                if (cargaSeleccionada == null)
                    return;

                CargarCargaEnControles(cargaSeleccionada);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la carga seleccionada: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdCargas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (ModoSeleccion && e.RowIndex >= 0)
                {
                    cargaSeleccionada = grdCargas.Rows[e.RowIndex].Tag as BECarga;
                    if (cargaSeleccionada != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    grdCargas_CellClick(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar carga: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdCargas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = grdCargas.Columns[e.ColumnIndex].Name;

            // Orden manual: la grilla se carga con Rows.Add, por eso ordenamos la lista y la volvemos a dibujar.
            if (columnaOrdenCarga == columna)
                ordenCargaAscendente = !ordenCargaAscendente;
            else
            {
                columnaOrdenCarga = columna;
                ordenCargaAscendente = true;
            }

            RefrescarGrilla(txtBuscar.Text.Trim().ToLower());
        }

        #endregion

        #region "Métodos auxiliares"

        private void RefrescarGrilla(string filtro = "")
        {
            listaCargas = gestorCarga.ListarTodo();
            grdCargas.Rows.Clear();

            var cargas = listaCargas
                .Where(c => string.IsNullOrWhiteSpace(filtro)
                            || (c.Codigo ?? "").ToLower().Contains(filtro)
                            || (c.Descripcion ?? "").ToLower().Contains(filtro)
                            || (c.Tipo?.Descripcion ?? "").ToLower().Contains(filtro)
                            || (c.TipoContenedor?.Descripcion ?? "").ToLower().Contains(filtro)
                            || (c.Estado?.Descripcion ?? "").ToLower().Contains(filtro)
                            || TextoDireccion(c.Origen).ToLower().Contains(filtro)
                            || TextoDireccion(c.PuertoEmbarque).ToLower().Contains(filtro)
                            || TextoDireccion(c.Destino).ToLower().Contains(filtro))
                .ToList();

            cargas = OrdenarCargas(cargas);

            foreach (var carga in cargas)
            {
                int fila = grdCargas.Rows.Add(
                    string.IsNullOrWhiteSpace(carga.Codigo) ? "-" : carga.Codigo,
                    string.IsNullOrWhiteSpace(carga.Descripcion) ? "-" : carga.Descripcion,
                    carga.Tipo?.Descripcion ?? "-",
                    carga.TipoContenedor?.Descripcion ?? "-",
                    carga.Estado?.Descripcion ?? "-",
                    carga.Peso.ToString("N0"),
                    carga.Volumen.ToString("N2"),
                    TextoDireccion(carga.Origen),
                    TextoDireccion(carga.PuertoEmbarque),
                    TextoDireccion(carga.Destino),
                    carga.Activo ? "Sí" : "No"
                );

                grdCargas.Rows[fila].Tag = carga;
            }
        }

        private List<BECarga> OrdenarCargas(List<BECarga> cargas)
        {
            Func<BECarga, object> criterio;

            switch (columnaOrdenCarga)
            {
                case "Codigo":
                    criterio = c => c.Codigo ?? string.Empty;
                    break;
                case "Descripcion":
                    criterio = c => c.Descripcion ?? string.Empty;
                    break;
                case "Tipo":
                    criterio = c => c.Tipo?.Descripcion ?? string.Empty;
                    break;
                case "TipoContenedor":
                    criterio = c => c.TipoContenedor?.Descripcion ?? string.Empty;
                    break;
                case "Estado":
                    criterio = c => c.Estado?.Descripcion ?? string.Empty;
                    break;
                case "Peso":
                    criterio = c => c.Peso;
                    break;
                case "Volumen":
                    criterio = c => c.Volumen;
                    break;
                case "Origen":
                    criterio = c => TextoDireccion(c.Origen);
                    break;
                case "PuertoEmbarque":
                    criterio = c => TextoDireccion(c.PuertoEmbarque);
                    break;
                case "Destino":
                    criterio = c => TextoDireccion(c.Destino);
                    break;
                case "Activo":
                    criterio = c => c.Activo;
                    break;
                default:
                    criterio = c => c.Id;
                    break;
            }

            return ordenCargaAscendente
                ? cargas.OrderBy(criterio).ToList()
                : cargas.OrderByDescending(criterio).ToList();
        }

        private void LimpiarControles()
        {
            txtDescripcion.Clear();
            numPeso.Value = 0;
            numVolumen.Value = 0;
            volumenEditadoManual = false;
            cmbTipoCarga.SelectedIndex = 0;
            ucOrigen.Limpiar();
            cmbPuertoEmbarque.SelectedIndex = -1;
            cmbPuertoDestino.SelectedIndex = -1;
            dtpEntregaEstimada.Value = DateTime.Today;
            cargaSeleccionada = null;
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Debe ingresar una descripción.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (numPeso.Value <= 0 || numVolumen.Value <= 0)
            {
                MessageBox.Show("El peso y volumen deben ser mayores a cero.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!ucOrigen.EsValido())
                return false;

            if (cmbPuertoEmbarque.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un puerto de embarque argentino.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbPuertoDestino.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un puerto de destino.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        #endregion

        #region "Carga de datos"

        private void CargarCargaEnControles(BECarga carga)
        {
            if (carga == null)
                return;

            txtDescripcion.Text = carga.Descripcion;
            numPeso.Value = carga.Peso;
            numVolumen.Value = carga.Volumen;
            volumenEditadoManual = carga.Volumen > 0;
            cmbTipoCarga.SelectedItem = carga.Tipo;
            CargarContenedoresCompatibles();
            SeleccionarContenedor(carga.TipoContenedor?.Codigo);
            ucOrigen.Cargar(carga.Origen);
            cmbPuertoEmbarque.SelectedValue = carga.PuertoEmbarque?.Id ?? 0;
            cmbPuertoDestino.SelectedValue = carga.Destino?.Id ?? 0;
            dtpEntregaEstimada.Value = carga.FechaEntregaEstimada;
        }

        private BECarga MapearDesdeControles()
        {
            var carga = new BECarga();

           
            if (cargaSeleccionada != null)
            {
                carga.Id = cargaSeleccionada.Id;
                carga.Codigo = cargaSeleccionada.Codigo;
            }
                       
            carga.Descripcion = txtDescripcion.Text.Trim();
            carga.Tipo = (TipoCarga)cmbTipoCarga.SelectedItem;
            carga.TipoContenedor = (TipoContenedor)cmbTipoContenedor.SelectedItem;
            carga.Peso = numPeso.Value;
            carga.Volumen = numVolumen.Value;
            carga.Origen = ucOrigen.Obtener();
            carga.PuertoEmbarque = cmbPuertoEmbarque.SelectedItem as BEDireccion;
            carga.Destino = cmbPuertoDestino.SelectedItem as BEDireccion;

            carga.FechaRegistro = cargaSeleccionada?.FechaRegistro ?? DateTime.Now;

            carga.FechaEntregaEstimada = dtpEntregaEstimada.Value;

            carga.Estado = cargaSeleccionada?.Estado ?? EstadoCarga.Disponible;

            return carga;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGestionarPuertos_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmPuerto())
            {
                frm.ShowDialog();
            }

            CargarPuertosEmbarque();
            ConfigurarPuertoDestino();
        }

        private string TextoDireccion(BEDireccion direccion)
        {
            if (direccion == null)
                return "-";

            if (!string.IsNullOrWhiteSpace(direccion.Puerto))
                return direccion.Puerto;

            if (!string.IsNullOrWhiteSpace(direccion.Ciudad))
                return direccion.Ciudad;

            return "-";
        }
        #endregion

        #region "Validaciones de compatibilidad Carga–Contenedor"

        private Label lblCompatibilidad;

        private void ConfigurarValidaciones()
        {
            // Tooltips
            ToolTip tt = new ToolTip();
            tt.SetToolTip(numPeso, "Ingrese el peso en kilogramos (kg)");
            tt.SetToolTip(numVolumen, "Se calcula automáticamente según el peso y tipo de carga (puede ajustarse manualmente).");

            // Label visual dentro del grupo
            lblCompatibilidad = new Label
            {
                AutoSize = true,
                ForeColor = System.Drawing.Color.Black,
                Location = new System.Drawing.Point(numVolumen.Left, numVolumen.Bottom + 15)
            };
            grpDatosCarga.Controls.Add(lblCompatibilidad); // asegúrate del nombre del groupbox

            // Eventos automáticos
            numPeso.ValueChanged += (s, e) =>
            {
                ActualizarVolumenAutomatico();
                ValidarCompatibilidad();
            };

            numVolumen.ValueChanged += (s, e) =>
            {
                if (!actualizandoVolumenAutomatico)
                    volumenEditadoManual = numVolumen.Value > 0;

                ValidarCompatibilidad();
            };

            cmbTipoCarga.SelectedIndexChanged += (s, e) =>
            {
                CargarContenedoresCompatibles();
                ActualizarVolumenAutomatico();
                ValidarCompatibilidad();
            };

            cmbTipoContenedor.SelectedIndexChanged += (s, e) => ValidarCompatibilidad();
        }

        private void CargarContenedoresCompatibles()
        {
            var tipoCarga = cmbTipoCarga.SelectedItem as TipoCarga;
            var tipos = tipoCarga == null
                ? new List<TipoContenedor>()
                : gestorContenedor.ObtenerTiposCompatibles(tipoCarga);

            cmbTipoContenedor.DataSource = tipos;
        }

        private void SeleccionarContenedor(string codigo)
        {
            foreach (var item in cmbTipoContenedor.Items)
            {
                var tipo = item as TipoContenedor;
                if (tipo != null && tipo.Codigo == codigo)
                {
                    cmbTipoContenedor.SelectedItem = item;
                    return;
                }
            }
        }

        private void CargarPuertosEmbarque()
        {
            cmbPuertoEmbarque.DataSource = gestorDireccion.ListarPuertosArgentinos();
            cmbPuertoEmbarque.DisplayMember = "Puerto";
            cmbPuertoEmbarque.ValueMember = "Id";
            cmbPuertoEmbarque.SelectedIndex = -1;
        }

        private void ConfigurarPuertoDestino()
        {
            ucDestino.Visible = false;

            if (cmbPuertoDestino == null)
            {
                cmbPuertoDestino = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    DisplayMember = "Puerto",
                    ValueMember = "Id",
                    Location = new System.Drawing.Point(18, 42),
                    Size = new System.Drawing.Size(370, 24),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                groupBox3.Controls.Add(cmbPuertoDestino);
            }

            cmbPuertoDestino.DataSource = gestorDireccion.ListarPuertos()
                .Where(p => !string.Equals(p.Pais, "Argentina", StringComparison.OrdinalIgnoreCase))
                .ToList();
            cmbPuertoDestino.SelectedIndex = -1;
        }

        private void ActualizarVolumenAutomatico()
        {
            if (volumenEditadoManual)
                return;

            if (numPeso.Value <= 0 || cmbTipoCarga.SelectedItem == null)
                return;

            string tipoCarga = ((TipoCarga)cmbTipoCarga.SelectedItem)?.Descripcion ?? "General";
            decimal volumen = CalcularVolumenEstimado(numPeso.Value, tipoCarga);

            actualizandoVolumenAutomatico = true;
            numVolumen.Value = Math.Min(volumen, numVolumen.Maximum);
            actualizandoVolumenAutomatico = false;
        }

        private void ValidarCompatibilidad()
        {
            try
            {
                var tipoContenedor = (TipoContenedor)cmbTipoContenedor.SelectedItem ?? TipoContenedor.Standard20;
                var tipoCarga = (TipoCarga)cmbTipoCarga.SelectedItem;

                decimal peso = numPeso.Value;
                decimal volumen = numVolumen.Value;

                // Si el volumen queda en cero, vuelve al cálculo automático.
                if (volumen <= 0 && peso > 0)
                {
                    volumenEditadoManual = false;
                    volumen = CalcularVolumenEstimado(peso, tipoCarga?.Descripcion ?? "General");
                    actualizandoVolumenAutomatico = true;
                    numVolumen.Value = Math.Min(volumen, numVolumen.Maximum);
                    actualizandoVolumenAutomatico = false;
                }

                gestorContenedor.ValidarCompatibilidadTipos(tipoCarga, tipoContenedor);
                decimal ocupacion = gestorContenedor.ValidarCapacidadPlanificada(
                    tipoContenedor, peso, volumen);

                lblCompatibilidad.Text = $"Compatible | Ocupación: {ocupacion:N1}% del {tipoContenedor.Codigo}";
                lblCompatibilidad.ForeColor = System.Drawing.Color.Green;
                btnGuardar.Enabled = true;
            }
            catch (Exception ex)
            {
                lblCompatibilidad.Text = ex.Message;
                lblCompatibilidad.ForeColor = System.Drawing.Color.Red;
                btnGuardar.Enabled = false;
            }
        }


        private decimal CalcularVolumenEstimado(decimal peso, string tipoCarga)
        {
            decimal densidad = 1000m; // kg/m³ general
            if (tipoCarga.Contains("Refrigerada")) densidad = 900m;
            else if (tipoCarga.Contains("Peligrosa")) densidad = 1100m;
            return peso / densidad;
        }

        #endregion

    }
}
