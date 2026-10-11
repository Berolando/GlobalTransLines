using BE.Dominio;
using BE.Finanzas;
using BE.Personas;
using BE.ServiciosSubcontratados;
using BLL.Finanzas;
using BLL.Personas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Gestion4_Proveedores
{
    public partial class FrmOrdenCompra : Form
    {
        #region "Atributos y Constructor"

        private RequerimientoCompraBLL requerimientoBLL = new RequerimientoCompraBLL();
        private OrdenCompraBLL ordenCompraBLL = new OrdenCompraBLL();
        private ProveedorBLL proveedorBLL = new ProveedorBLL();

        private BERequerimientoCompra requerimientoSeleccionado;
        private BEOrdenDeCompra ordenSeleccionada;
        private string columnaOrdenRequerimiento = "FechaSolicitud";
        private bool ordenRequerimientoAscendente = false;
        private string columnaOrdenCompra = "FechaEmision";
        private bool ordenCompraAscendente = false;

        public FrmOrdenCompra()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmOrdenCompra_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrillas();
                ConfigurarCombos();
                ConfigurarControlesIniciales();
                RefrescarGrillaRequerimientos();
                RefrescarGrillaOrdenesCompra();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ordenes de compra: " + ex.Message,
                                "Ordenes de compra",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnGenerarOC_Click(object sender, EventArgs e)
        {
            try
            {
                if (requerimientoSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un requerimiento pendiente.",
                                    "Ordenes de compra",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                BEProveedor proveedor = cmbProveedor.SelectedItem as BEProveedor;
                if (proveedor == null)
                {
                    MessageBox.Show("Debe seleccionar un proveedor.",
                                    "Ordenes de compra",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                decimal importeTotal;
                if (!decimal.TryParse(txtImporteTotal.Text, out importeTotal) || importeTotal <= 0)
                {
                    MessageBox.Show("Debe ingresar un importe total mayor a cero.",
                                    "Ordenes de compra",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                BEOrdenDeCompra orden = ConstruirOrdenDesdeFormulario(proveedor, importeTotal);
                ordenCompraBLL.Guardar(orden);
                requerimientoBLL.MarcarProcesado(requerimientoSeleccionado);

                MessageBox.Show("Orden de compra generada correctamente.",
                                "Ordenes de compra",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                RefrescarGrillaRequerimientos();
                RefrescarGrillaOrdenesCompra();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar orden de compra: " + ex.Message,
                                "Ordenes de compra",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnAnularRequerimiento_Click(object sender, EventArgs e)
        {
            try
            {
                if (requerimientoSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un requerimiento pendiente.",
                                    "Ordenes de compra",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                requerimientoBLL.MarcarAnulado(requerimientoSeleccionado);

                MessageBox.Show("Requerimiento anulado correctamente.",
                                "Ordenes de compra",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                RefrescarGrillaRequerimientos();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al anular requerimiento: " + ex.Message,
                                "Ordenes de compra",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                RefrescarGrillaRequerimientos();
                RefrescarGrillaOrdenesCompra();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar datos: " + ex.Message,
                                "Ordenes de compra",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        private void btnConfirmarRecepcion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("La recepcion de servicios se gestiona desde su formulario correspondiente.",
                            "Ordenes de compra",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El pago a proveedores se gestiona desde su formulario correspondiente.",
                            "Ordenes de compra",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region "Eventos de grillas"

        private void dgvRequerimientos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                requerimientoSeleccionado = dgvRequerimientos.CurrentRow?.DataBoundItem as BERequerimientoCompra;
                CargarRequerimientoSeleccionado();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar requerimiento: " + ex.Message,
                                "Ordenes de compra",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void dgvOrdenesCompra_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ordenSeleccionada = dgvOrdenesCompra.CurrentRow?.DataBoundItem as BEOrdenDeCompra;
                CargarOrdenSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar orden de compra: " + ex.Message,
                                "Ordenes de compra",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Configuración Visual"

        private void ConfigurarGrillas()
        {
            dgvRequerimientos.AutoGenerateColumns = true;
            dgvRequerimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRequerimientos.MultiSelect = false;
            dgvRequerimientos.ReadOnly = true;
            dgvRequerimientos.RowHeadersVisible = false;
            dgvRequerimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRequerimientos.AllowUserToAddRows = false;
            dgvRequerimientos.AllowUserToDeleteRows = false;
            dgvRequerimientos.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            dgvRequerimientos.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);

            dgvRequerimientos.SelectionChanged -= dgvRequerimientos_SelectionChanged;
            dgvRequerimientos.SelectionChanged += dgvRequerimientos_SelectionChanged;
            dgvRequerimientos.ColumnHeaderMouseClick -= dgvRequerimientos_ColumnHeaderMouseClick;
            dgvRequerimientos.ColumnHeaderMouseClick += dgvRequerimientos_ColumnHeaderMouseClick;

            dgvOrdenesCompra.AutoGenerateColumns = true;
            dgvOrdenesCompra.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrdenesCompra.MultiSelect = false;
            dgvOrdenesCompra.ReadOnly = true;
            dgvOrdenesCompra.RowHeadersVisible = false;
            dgvOrdenesCompra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrdenesCompra.AllowUserToAddRows = false;
            dgvOrdenesCompra.AllowUserToDeleteRows = false;
            dgvOrdenesCompra.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            dgvOrdenesCompra.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);

            dgvOrdenesCompra.SelectionChanged -= dgvOrdenesCompra_SelectionChanged;
            dgvOrdenesCompra.SelectionChanged += dgvOrdenesCompra_SelectionChanged;
            dgvOrdenesCompra.ColumnHeaderMouseClick -= dgvOrdenesCompra_ColumnHeaderMouseClick;
            dgvOrdenesCompra.ColumnHeaderMouseClick += dgvOrdenesCompra_ColumnHeaderMouseClick;
        }

        private void ConfigurarCombos()
        {
            cmbProveedor.DataSource = null;
            cmbProveedor.DataSource = proveedorBLL.ListarActivos();
            cmbProveedor.DisplayMember = "RazonSocial";
            cmbProveedor.ValueMember = "Id";
            cmbProveedor.SelectedIndex = -1;
        }

        private void ConfigurarControlesIniciales()
        {
            txtNumeroOrden.ReadOnly = true;
            txtBooking.ReadOnly = true;
            txtServicio.ReadOnly = true;
            txtEstadoRequerimiento.ReadOnly = true;
            txtFechaSolicitud.ReadOnly = true;
            txtDetalleOrden.ReadOnly = true;

            dtpFechaEmision.Format = DateTimePickerFormat.Short;
            dtpFechaEmision.Value = DateTime.Now;
        }

        #endregion

        #region "Metodos auxiliares"

        private void CargarRequerimientoSeleccionado()
        {
            if (requerimientoSeleccionado == null)
            {
                txtBooking.Clear();
                txtServicio.Clear();
                txtEstadoRequerimiento.Clear();
                txtFechaSolicitud.Clear();
                return;
            }

            txtBooking.Text = requerimientoSeleccionado.BookingOrigen?.Numero;
            txtServicio.Text = requerimientoSeleccionado.Servicio?.Nombre;
            txtEstadoRequerimiento.Text = requerimientoSeleccionado.Estado?.Descripcion;
            txtFechaSolicitud.Text = requerimientoSeleccionado.FechaSolicitud.ToShortDateString();

            if (requerimientoSeleccionado.Servicio != null && requerimientoSeleccionado.Servicio.Costo > 0)
                txtImporteTotal.Text = requerimientoSeleccionado.Servicio.Costo.ToString("0.00");
        }

        private void CargarOrdenSeleccionada()
        {
            if (ordenSeleccionada == null)
            {
                txtDetalleOrden.Clear();
                return;
            }

            string servicios = ordenSeleccionada.Servicios == null
                ? string.Empty
                : string.Join(", ", ordenSeleccionada.Servicios.Select(s => s.Nombre));

            txtDetalleOrden.Text =
                "Numero: " + ordenSeleccionada.Numero + Environment.NewLine +
                "Booking: " + ordenSeleccionada.BookingOrigen?.Numero + Environment.NewLine +
                "Proveedor: " + ordenSeleccionada.Proveedor?.RazonSocial + Environment.NewLine +
                "Servicios: " + servicios + Environment.NewLine +
                "Total: " + ordenSeleccionada.ImporteTotal.ToString("C") + Environment.NewLine +
                "Estado: " + ordenSeleccionada.Estado;
        }

        private BEOrdenDeCompra ConstruirOrdenDesdeFormulario(BEProveedor proveedor, decimal importeTotal)
        {
            var servicio = requerimientoSeleccionado.Servicio;
            var servicios = new List<BEServicioSubcontratado>();
            if (servicio != null)
                servicios.Add(servicio);

            return new BEOrdenDeCompra
            {
                Numero = txtNumeroOrden.Text,
                Proveedor = proveedor,
                BookingOrigen = requerimientoSeleccionado.BookingOrigen,
                Servicios = servicios,
                FechaEmision = dtpFechaEmision.Value,
                Estado = EstadoOrdenCompra.Emitida,
                ImporteTotal = importeTotal
            };
        }

        private void LimpiarControles()
        {
            requerimientoSeleccionado = null;
            ordenSeleccionada = null;

            txtNumeroOrden.Text = GenerarNumeroOrden();
            txtBooking.Clear();
            txtServicio.Clear();
            txtEstadoRequerimiento.Clear();
            txtFechaSolicitud.Clear();
            txtImporteTotal.Clear();
            txtDetalleOrden.Clear();
            dtpFechaEmision.Value = DateTime.Now;

            if (cmbProveedor.Items.Count > 0)
                cmbProveedor.SelectedIndex = -1;

            dgvRequerimientos.ClearSelection();
            dgvOrdenesCompra.ClearSelection();
        }

        private string GenerarNumeroOrden()
        {
            long proximoId = ordenCompraBLL.ObtenerUltimoId() + 1;
            return "OC-" + proximoId.ToString("0000");
        }

        #endregion

        #region "Orden de grillas"

        private void dgvRequerimientos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = dgvRequerimientos.Columns[e.ColumnIndex].Name;
            ordenRequerimientoAscendente = columnaOrdenRequerimiento == columna ? !ordenRequerimientoAscendente : true;
            columnaOrdenRequerimiento = columna;
            OrdenarRequerimientos();
        }

        private void dgvOrdenesCompra_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = dgvOrdenesCompra.Columns[e.ColumnIndex].Name;
            ordenCompraAscendente = columnaOrdenCompra == columna ? !ordenCompraAscendente : true;
            columnaOrdenCompra = columna;
            OrdenarOrdenesCompra();
        }

        private void OrdenarRequerimientos()
        {
            var lista = (dgvRequerimientos.DataSource as IEnumerable<BERequerimientoCompra>)?.ToList();
            if (lista == null)
                return;

            // Ordena la lista visible para mantener el criterio de requerimientos pendientes.
            switch (columnaOrdenRequerimiento)
            {
                case "Id":
                    lista = ordenRequerimientoAscendente ? lista.OrderBy(r => r.Id).ToList() : lista.OrderByDescending(r => r.Id).ToList();
                    break;
                case "BookingOrigen":
                    lista = ordenRequerimientoAscendente ? lista.OrderBy(r => r.BookingOrigen?.Numero).ToList() : lista.OrderByDescending(r => r.BookingOrigen?.Numero).ToList();
                    break;
                case "Servicio":
                    lista = ordenRequerimientoAscendente ? lista.OrderBy(r => r.Servicio?.Nombre).ToList() : lista.OrderByDescending(r => r.Servicio?.Nombre).ToList();
                    break;
                case "Estado":
                    lista = ordenRequerimientoAscendente ? lista.OrderBy(r => r.Estado?.Descripcion).ToList() : lista.OrderByDescending(r => r.Estado?.Descripcion).ToList();
                    break;
                case "Activo":
                    lista = ordenRequerimientoAscendente ? lista.OrderBy(r => r.Activo).ToList() : lista.OrderByDescending(r => r.Activo).ToList();
                    break;
                default:
                    lista = ordenRequerimientoAscendente ? lista.OrderBy(r => r.FechaSolicitud).ToList() : lista.OrderByDescending(r => r.FechaSolicitud).ToList();
                    break;
            }

            dgvRequerimientos.DataSource = null;
            dgvRequerimientos.DataSource = lista;
        }

        private void OrdenarOrdenesCompra()
        {
            var lista = (dgvOrdenesCompra.DataSource as IEnumerable<BEOrdenDeCompra>)?.ToList();
            if (lista == null)
                return;

            // Ordena la lista visible de ordenes sin alterar el flujo de estados de compra.
            switch (columnaOrdenCompra)
            {
                case "Id":
                    lista = ordenCompraAscendente ? lista.OrderBy(o => o.Id).ToList() : lista.OrderByDescending(o => o.Id).ToList();
                    break;
                case "Numero":
                    lista = ordenCompraAscendente ? lista.OrderBy(o => o.Numero).ToList() : lista.OrderByDescending(o => o.Numero).ToList();
                    break;
                case "Proveedor":
                    lista = ordenCompraAscendente ? lista.OrderBy(o => o.Proveedor?.RazonSocial).ToList() : lista.OrderByDescending(o => o.Proveedor?.RazonSocial).ToList();
                    break;
                case "BookingOrigen":
                    lista = ordenCompraAscendente ? lista.OrderBy(o => o.BookingOrigen?.Numero).ToList() : lista.OrderByDescending(o => o.BookingOrigen?.Numero).ToList();
                    break;
                case "Estado":
                    lista = ordenCompraAscendente ? lista.OrderBy(o => o.Estado?.Descripcion).ToList() : lista.OrderByDescending(o => o.Estado?.Descripcion).ToList();
                    break;
                case "ImporteTotal":
                    lista = ordenCompraAscendente ? lista.OrderBy(o => o.ImporteTotal).ToList() : lista.OrderByDescending(o => o.ImporteTotal).ToList();
                    break;
                case "Activo":
                    lista = ordenCompraAscendente ? lista.OrderBy(o => o.Activo).ToList() : lista.OrderByDescending(o => o.Activo).ToList();
                    break;
                default:
                    lista = ordenCompraAscendente ? lista.OrderBy(o => o.FechaEmision).ToList() : lista.OrderByDescending(o => o.FechaEmision).ToList();
                    break;
            }

            dgvOrdenesCompra.DataSource = null;
            dgvOrdenesCompra.DataSource = lista;
        }

        #endregion

        #region "Carga de datos"

        private void RefrescarGrillaRequerimientos()
        {
            dgvRequerimientos.DataSource = null;
            dgvRequerimientos.DataSource = requerimientoBLL.ListarPendientes();
            dgvRequerimientos.ClearSelection();
            requerimientoSeleccionado = null;
        }

        private void RefrescarGrillaOrdenesCompra()
        {
            dgvOrdenesCompra.DataSource = null;
            dgvOrdenesCompra.DataSource = ordenCompraBLL.ListarTodo();
            dgvOrdenesCompra.ClearSelection();
            ordenSeleccionada = null;
        }

        #endregion
    }
}


