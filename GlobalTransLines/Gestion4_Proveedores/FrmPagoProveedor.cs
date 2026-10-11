using BE.Dominio;
using BE.Finanzas;
using BLL.Finanzas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Transversales.Documentos;

namespace GlobalTransLines.UI.Gestion4_Proveedores
{
    public partial class FrmPagoProveedor : Form
    {
        #region "Atributos y Constructor"

        private PagoProveedorBLL pagoProveedorBLL = new PagoProveedorBLL();
        private BEOrdenDeCompra ordenSeleccionada;
        private BEPagoProveedor pagoSeleccionado;
        private string columnaOrdenOrden = "FechaEmision";
        private bool ordenOrdenAscendente = false;
        private string columnaOrdenPago = "FechaPago";
        private bool ordenPagoAscendente = false;

        public FrmPagoProveedor()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmPagoProveedor_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrillas();
                ConfigurarComboMedioPago();
                ConfigurarControlesIniciales();
                RefrescarGrillaOrdenes();
                RefrescarGrillaPagos();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pagos a proveedores: " + ex.Message,
                                "Pago a proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (ordenSeleccionada == null)
                {
                    MessageBox.Show("Debe seleccionar una orden de compra recibida.",
                                    "Pago a proveedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BEPagoProveedor pago = ConstruirPagoDesdeFormulario();
                pagoProveedorBLL.RegistrarPago(pago);

                MessageBox.Show("Pago a proveedor registrado correctamente. La orden quedó marcada como pagada.",
                                "Pago a proveedores", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefrescarGrillaOrdenes();
                RefrescarGrillaPagos();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerMensajeRaiz(ex),
                                "Pago a proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            RefrescarGrillaOrdenes();
            RefrescarGrillaPagos();
            LimpiarControles();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnActualizar2_Click(object sender, EventArgs e)
        {
            RefrescarGrillaPagos();
            pagoSeleccionado = null;
            rtbDetallePago.Clear();
        }

        private void btnImprimirComprobante_Click(object sender, EventArgs e)
        {
            try
            {
                if (pagoSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un pago registrado.",
                                    "Pago a proveedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BEPagoProveedor pago = pagoProveedorBLL.ListarObjeto(new BEPagoProveedor { Id = pagoSeleccionado.Id });

                if (pago == null)
                {
                    MessageBox.Show("No se encontró el pago seleccionado.",
                                    "Pago a proveedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string numeroOrden = pago.OrdenDeCompra != null
                    ? pago.OrdenDeCompra.Numero
                    : pago.Id.ToString();

                string numeroComprobante = "PAGOPROV-" + numeroOrden;

                var rutas = new RutasDocumentos();
                var gestor = new Transversales.Docu.GestorDocumentos(rutas);

                var generador = new GeneradorPdfBasico<BEPagoProveedor>(
                    p => "Comprobante de Pago a Proveedor",
                    p => ObtenerContenidoComprobante(p),
                    null,
                    null,
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Logo.jpg"),
                    p => "RECIBO DE PAGO A PROVEEDOR\n" +
                         "Nro. " + numeroComprobante + "\n" +
                         "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")
                );

                string rutaPdf = gestor.ObtenerOGenerarPdf(
                    DocumentoTipo.PagoProveedor,
                    numeroComprobante,
                    ruta => generador.GenerarDocumento(ruta, pago)
                );

                System.Diagnostics.Process.Start(rutaPdf);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir comprobante de pago a proveedor: " + ObtenerMensajeRaiz(ex),
                                "Pago a proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de grilla"

        private void dgvOrdenes_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrdenes.CurrentRow == null)
                    return;

                ordenSeleccionada = dgvOrdenes.CurrentRow.DataBoundItem as BEOrdenDeCompra;
                CargarOrdenSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar orden de compra: " + ex.Message,
                                "Pago a proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPagos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvPagos.CurrentRow == null)
                    return;

                pagoSeleccionado = dgvPagos.CurrentRow.DataBoundItem as BEPagoProveedor;
                CargarPagoSeleccionado();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar pago: " + ex.Message,
                                "Pago a proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de controles"

        private void cmbMedioPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarMedioPagoSeleccionado();
        }

        private void txtMontoEntregado_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalcularVuelto();
            }
            catch
            {
                txtVuelto.Text = string.Empty;
            }
        }

        #endregion

        #region "Configuración Visual"

        private void ConfigurarGrillas()
        {
            ConfigurarGrilla(dgvOrdenes);
            ConfigurarGrilla(dgvPagos);

            dgvOrdenes.SelectionChanged -= dgvOrdenes_SelectionChanged;
            dgvOrdenes.SelectionChanged += dgvOrdenes_SelectionChanged;

            dgvPagos.SelectionChanged -= dgvPagos_SelectionChanged;
            dgvPagos.SelectionChanged += dgvPagos_SelectionChanged;

            dgvOrdenes.ColumnHeaderMouseClick -= dgvOrdenes_ColumnHeaderMouseClick;
            dgvOrdenes.ColumnHeaderMouseClick += dgvOrdenes_ColumnHeaderMouseClick;

            dgvPagos.ColumnHeaderMouseClick -= dgvPagos_ColumnHeaderMouseClick;
            dgvPagos.ColumnHeaderMouseClick += dgvPagos_ColumnHeaderMouseClick;

            dgvPagos.CellFormatting -= dgvPagos_CellFormatting;
            dgvPagos.CellFormatting += dgvPagos_CellFormatting;
        }

        private void ConfigurarGrilla(DataGridView grilla)
        {
            grilla.AutoGenerateColumns = true;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.MultiSelect = false;
            grilla.ReadOnly = true;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.RowHeadersVisible = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
        }

        private void ConfigurarComboMedioPago()
        {
            cmbMedioPago.Items.Clear();
            cmbMedioPago.Items.Add("Efectivo");
            cmbMedioPago.Items.Add("Transferencia");
            cmbMedioPago.Items.Add("Tarjeta");

            cmbMedioPago.SelectedIndexChanged -= cmbMedioPago_SelectedIndexChanged;
            cmbMedioPago.SelectedIndexChanged += cmbMedioPago_SelectedIndexChanged;

            if (cmbMedioPago.Items.Count > 0)
                cmbMedioPago.SelectedIndex = 0;
        }

        private void ConfigurarControlesIniciales()
        {
            txtOrden.ReadOnly = true;
            txtTotalOrden.ReadOnly = true;
            txtImportePago.ReadOnly = true;
            txtVuelto.ReadOnly = true;

            dtpFechaFactura.Format = DateTimePickerFormat.Short;
            dtpFechaPago.Format = DateTimePickerFormat.Short;
            dtpVencimiento.Format = DateTimePickerFormat.Short;

            dtpFechaFactura.MaxDate = DateTime.Today;
            dtpFechaPago.MaxDate = DateTime.Today;

            txtMontoEntregado.TextChanged -= txtMontoEntregado_TextChanged;
            txtMontoEntregado.TextChanged += txtMontoEntregado_TextChanged;
        }

        #endregion

        #region "Orden de grillas"

        private void dgvOrdenes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = dgvOrdenes.Columns[e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrWhiteSpace(columna))
                columna = dgvOrdenes.Columns[e.ColumnIndex].Name;

            ordenOrdenAscendente = columnaOrdenOrden == columna ? !ordenOrdenAscendente : true;
            columnaOrdenOrden = columna;

            dgvOrdenes.DataSource = null;
            dgvOrdenes.DataSource = OrdenarOrdenes(pagoProveedorBLL.ListarOrdenesRecibidas()).ToList();
            dgvOrdenes.ClearSelection();
            ordenSeleccionada = null;
        }

        private void dgvPagos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = dgvPagos.Columns[e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrWhiteSpace(columna))
                columna = dgvPagos.Columns[e.ColumnIndex].Name;

            ordenPagoAscendente = columnaOrdenPago == columna ? !ordenPagoAscendente : true;
            columnaOrdenPago = columna;

            dgvPagos.DataSource = null;
            dgvPagos.DataSource = OrdenarPagos(pagoProveedorBLL.ListarTodo()).ToList();
            dgvPagos.ClearSelection();
            pagoSeleccionado = null;
        }

        private void dgvPagos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            BEPagoProveedor pago = dgvPagos.Rows[e.RowIndex].DataBoundItem as BEPagoProveedor;

            if (pago == null)
                return;

            string columna = dgvPagos.Columns[e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrWhiteSpace(columna))
                columna = dgvPagos.Columns[e.ColumnIndex].Name;

            if (columna == "OrdenDeCompra")
            {
                e.Value = pago.OrdenDeCompra?.Numero ?? "-";
                e.FormattingApplied = true;
            }
            else if (columna == "MedioDePago")
            {
                e.Value = pago.MedioDePago?.Nombre ?? "-";
                e.FormattingApplied = true;
            }
            else if (columna == "Estado")
            {
                e.Value = pago.Estado?.Descripcion ?? "-";
                e.FormattingApplied = true;
            }
        }

        private IEnumerable<BEOrdenDeCompra> OrdenarOrdenes(IEnumerable<BEOrdenDeCompra> ordenes)
        {
            Func<BEOrdenDeCompra, object> selector;

            switch (columnaOrdenOrden)
            {
                case "Id":
                    selector = x => x.Id;
                    break;
                case "Numero":
                    selector = x => x.Numero;
                    break;
                case "Proveedor":
                    selector = x => x.Proveedor?.RazonSocial;
                    break;
                case "BookingOrigen":
                    selector = x => x.BookingOrigen?.Numero;
                    break;
                case "Estado":
                    selector = x => x.Estado?.Descripcion;
                    break;
                case "ImporteTotal":
                    selector = x => x.ImporteTotal;
                    break;
                case "Activo":
                    selector = x => x.Activo;
                    break;
                default:
                    selector = x => x.FechaEmision;
                    break;
            }

            return ordenOrdenAscendente
                ? ordenes.OrderBy(selector)
                : ordenes.OrderByDescending(selector);
        }

        private IEnumerable<BEPagoProveedor> OrdenarPagos(IEnumerable<BEPagoProveedor> pagos)
        {
            Func<BEPagoProveedor, object> selector;

            switch (columnaOrdenPago)
            {
                case "Id":
                    selector = x => x.Id;
                    break;
                case "OrdenDeCompra":
                    selector = x => x.OrdenDeCompra?.Numero;
                    break;
                case "MedioDePago":
                    selector = x => x.MedioDePago?.Nombre;
                    break;
                case "Importe":
                    selector = x => x.Importe;
                    break;
                case "NumeroFacturaProveedor":
                    selector = x => x.NumeroFacturaProveedor;
                    break;
                case "FechaFacturaProveedor":
                    selector = x => x.FechaFacturaProveedor;
                    break;
                case "ImporteFacturaProveedor":
                    selector = x => x.ImporteFacturaProveedor;
                    break;
                case "Estado":
                    selector = x => x.Estado?.Descripcion;
                    break;
                case "Activo":
                    selector = x => x.Activo;
                    break;
                default:
                    selector = x => x.FechaPago;
                    break;
            }

            return ordenPagoAscendente
                ? pagos.OrderBy(selector)
                : pagos.OrderByDescending(selector);
        }

        #endregion

        #region "Métodos auxiliares"

        private void CargarOrdenSeleccionada()
        {
            if (ordenSeleccionada == null)
            {
                txtOrden.Clear();
                txtTotalOrden.Clear();
                txtImporteFactura.Clear();
                txtImportePago.Clear();
                return;
            }

            txtOrden.Text = ordenSeleccionada.Numero;
            txtTotalOrden.Text = ordenSeleccionada.ImporteTotal.ToString("N2");
            txtImporteFactura.Text = ordenSeleccionada.ImporteTotal.ToString("N2");
            txtImportePago.Text = ordenSeleccionada.ImporteTotal.ToString("N2");
            CalcularVuelto();
        }

        private void CargarPagoSeleccionado()
        {
            rtbDetallePago.Clear();

            if (pagoSeleccionado == null)
                return;

            rtbDetallePago.AppendText("COMPROBANTE DE PAGO A PROVEEDOR" + Environment.NewLine);
            rtbDetallePago.AppendText("----------------------------------------" + Environment.NewLine);
            rtbDetallePago.AppendText("OC: " + (pagoSeleccionado.OrdenDeCompra?.Numero ?? "-") + Environment.NewLine);
            rtbDetallePago.AppendText("Factura proveedor: " + pagoSeleccionado.NumeroFacturaProveedor + Environment.NewLine);
            rtbDetallePago.AppendText("Fecha factura: " + FechaTexto(pagoSeleccionado.FechaFacturaProveedor) + Environment.NewLine);
            rtbDetallePago.AppendText("Importe factura: " + pagoSeleccionado.ImporteFacturaProveedor.ToString("C") + Environment.NewLine);
            rtbDetallePago.AppendText("Fecha pago: " + FechaTexto(pagoSeleccionado.FechaPago) + Environment.NewLine);
            rtbDetallePago.AppendText("Importe pago: " + pagoSeleccionado.Importe.ToString("C") + Environment.NewLine);
            rtbDetallePago.AppendText("Estado: " + (pagoSeleccionado.Estado?.Descripcion ?? "-") + Environment.NewLine);
            rtbDetallePago.AppendText(Environment.NewLine);

            if (pagoSeleccionado.MedioDePago is BEEfectivo efectivo)
            {
                rtbDetallePago.AppendText("Medio de pago: Efectivo" + Environment.NewLine);
                rtbDetallePago.AppendText("Monto entregado: " + efectivo.MontoEntregado.ToString("C") + Environment.NewLine);
                rtbDetallePago.AppendText("Vuelto: " + efectivo.Vuelto.ToString("C") + Environment.NewLine);
            }
            else if (pagoSeleccionado.MedioDePago is BETransferencia transferencia)
            {
                rtbDetallePago.AppendText("Medio de pago: Transferencia" + Environment.NewLine);
                rtbDetallePago.AppendText("Banco: " + transferencia.Banco + Environment.NewLine);
                rtbDetallePago.AppendText("CBU/Alias: " + transferencia.CBU + Environment.NewLine);
            }
            else if (pagoSeleccionado.MedioDePago is BETarjeta tarjeta)
            {
                rtbDetallePago.AppendText("Medio de pago: Tarjeta" + Environment.NewLine);
                rtbDetallePago.AppendText("Marca: " + tarjeta.Marca + Environment.NewLine);
                rtbDetallePago.AppendText("Titular: " + tarjeta.Titular + Environment.NewLine);
            }
        }

        private BEPagoProveedor ConstruirPagoDesdeFormulario()
        {
            decimal importeFactura = ObtenerDecimal(txtImporteFactura.Text, "Importe factura");
            decimal importePago = ObtenerDecimal(txtImportePago.Text, "Importe pago");

            return new BEPagoProveedor
            {
                OrdenDeCompra = ordenSeleccionada,
                NumeroFacturaProveedor = txtNumeroFactura.Text,
                FechaFacturaProveedor = dtpFechaFactura.Value,
                ImporteFacturaProveedor = importeFactura,
                FechaPago = dtpFechaPago.Value,
                Importe = importePago,
                Estado = EstadoPago.Pagado,
                MedioDePago = ConstruirMedioDePago(importePago)
            };
        }

        private BEMedioDePago ConstruirMedioDePago(decimal importe)
        {
            if (cmbMedioPago.SelectedItem == null)
                throw new Exception("Debe seleccionar un medio de pago.");

            string medio = cmbMedioPago.SelectedItem.ToString();

            if (medio == "Efectivo")
                return ConstruirEfectivo(importe);

            if (medio == "Transferencia")
                return ConstruirTransferencia();

            if (medio == "Tarjeta")
                return ConstruirTarjeta();

            throw new Exception("El medio de pago seleccionado no es válido.");
        }

        private BEEfectivo ConstruirEfectivo(decimal importe)
        {
            decimal montoEntregado = ObtenerDecimal(txtMontoEntregado.Text, "Monto entregado");

            if (montoEntregado < importe)
                throw new Exception("El monto entregado no puede ser menor al importe del pago.");

            return new BEEfectivo
            {
                Nombre = "Efectivo",
                MontoEntregado = Convert.ToDouble(montoEntregado),
                Moneda = "ARS",
                FechaPago = dtpFechaPago.Value,
                Vuelto = montoEntregado - importe
            };
        }

        private BETransferencia ConstruirTransferencia()
        {
            if (string.IsNullOrWhiteSpace(txtBanco.Text))
                throw new Exception("Debe ingresar el banco de la transferencia.");

            if (string.IsNullOrWhiteSpace(txtCBU.Text))
                throw new Exception("Debe ingresar el CBU o Alias de la transferencia.");

            return new BETransferencia
            {
                Nombre = "Transferencia",
                Banco = txtBanco.Text.Trim(),
                CBU = txtCBU.Text.Trim()
            };
        }

        private BETarjeta ConstruirTarjeta()
        {
            if (string.IsNullOrWhiteSpace(txtNumeroTarjeta.Text))
                throw new Exception("Debe ingresar el número de tarjeta.");

            if (string.IsNullOrWhiteSpace(txtTitular.Text))
                throw new Exception("Debe ingresar el titular de la tarjeta.");

            if (string.IsNullOrWhiteSpace(txtCVV.Text))
                throw new Exception("Debe ingresar el CVV.");

            if (string.IsNullOrWhiteSpace(txtMarca.Text))
                throw new Exception("Debe ingresar la marca de la tarjeta.");

            if (dtpVencimiento.Value <= DateTime.Now)
                throw new Exception("La tarjeta indicada se encuentra vencida.");

            return new BETarjeta
            {
                Nombre = "Tarjeta",
                Numero = txtNumeroTarjeta.Text.Trim(),
                Titular = txtTitular.Text.Trim(),
                CVV = txtCVV.Text.Trim(),
                Marca = txtMarca.Text.Trim(),
                BancoEmisor = txtBancoEmisor.Text.Trim(),
                Vencimiento = dtpVencimiento.Value
            };
        }

        private decimal ObtenerDecimal(string texto, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new Exception("Debe ingresar " + nombreCampo + ".");

            decimal valor;
            string normalizado = texto.Trim();

            if (!decimal.TryParse(normalizado, NumberStyles.Any, CultureInfo.CurrentCulture, out valor) &&
                !decimal.TryParse(normalizado, NumberStyles.Any, new CultureInfo("es-AR"), out valor))
            {
                normalizado = normalizado.Replace(".", "").Replace(",", ".");

                if (!decimal.TryParse(normalizado, NumberStyles.Any, CultureInfo.InvariantCulture, out valor))
                    throw new Exception("El campo " + nombreCampo + " debe ser numérico.");
            }

            if (valor <= 0)
                throw new Exception("El campo " + nombreCampo + " debe ser numérico.");

            return valor;
        }

        private void MostrarMedioPagoSeleccionado()
        {
            if (cmbMedioPago.SelectedItem == null)
                return;

            string medio = cmbMedioPago.SelectedItem.ToString();

            gbEfectivo.Visible = medio == "Efectivo";
            gbTransferencia.Visible = medio == "Transferencia";
            gbTarjeta.Visible = medio == "Tarjeta";
        }

        private void CalcularVuelto()
        {
            if (ordenSeleccionada == null || string.IsNullOrWhiteSpace(txtMontoEntregado.Text))
            {
                txtVuelto.Text = string.Empty;
                return;
            }

            decimal montoEntregado;
            decimal importePago;

            if (!decimal.TryParse(txtMontoEntregado.Text.Replace(".", ","), NumberStyles.Any, CultureInfo.CurrentCulture, out montoEntregado) ||
                !decimal.TryParse(txtImportePago.Text.Replace(".", ","), NumberStyles.Any, CultureInfo.CurrentCulture, out importePago))
            {
                txtVuelto.Text = string.Empty;
                return;
            }

            decimal vuelto = montoEntregado - importePago;
            txtVuelto.Text = vuelto >= 0 ? vuelto.ToString("N2") : "0,00";
        }

        private void LimpiarControles()
        {
            ordenSeleccionada = null;
            pagoSeleccionado = null;

            txtOrden.Clear();
            txtTotalOrden.Clear();
            txtNumeroFactura.Clear();
            txtImporteFactura.Clear();
            txtImportePago.Clear();
            txtMontoEntregado.Clear();
            txtVuelto.Clear();
            txtBanco.Clear();
            txtCBU.Clear();
            txtNumeroOperacion.Clear();
            txtNumeroTarjeta.Clear();
            txtTitular.Clear();
            txtCVV.Clear();
            txtMarca.Clear();
            txtBancoEmisor.Clear();
            rtbDetallePago.Clear();

            dtpFechaFactura.Value = DateTime.Today;
            dtpFechaPago.Value = DateTime.Today;
            dtpVencimiento.Value = DateTime.Today.AddYears(1);

            if (cmbMedioPago.Items.Count > 0)
                cmbMedioPago.SelectedIndex = 0;

            dgvOrdenes.ClearSelection();
        }

        private string ObtenerMensajeRaiz(Exception ex)
        {
            Exception actual = ex;
            while (actual.InnerException != null)
                actual = actual.InnerException;

            return actual.Message;
        }

        private string FechaTexto(DateTime fecha)
        {
            return fecha == DateTime.MinValue ? "-" : fecha.ToString("dd/MM/yyyy");
        }

        private string ObtenerContenidoComprobante(BEPagoProveedor pago)
        {
            string orden = pago.OrdenDeCompra != null ? pago.OrdenDeCompra.Numero : "-";
            string proveedor = pago.OrdenDeCompra != null && pago.OrdenDeCompra.Proveedor != null
                ? pago.OrdenDeCompra.Proveedor.RazonSocial
                : "-";
            string booking = pago.OrdenDeCompra != null && pago.OrdenDeCompra.BookingOrigen != null
                ? pago.OrdenDeCompra.BookingOrigen.Numero
                : "-";

            string contenido =
                "--- Datos del comprobante ---\n" +
                "Orden de compra: " + orden + "\n" +
                "Proveedor: " + proveedor + "\n" +
                "Booking: " + booking + "\n" +
                "Factura proveedor: " + pago.NumeroFacturaProveedor + "\n" +
                "Fecha factura: " + FechaTexto(pago.FechaFacturaProveedor) + "\n" +
                "Importe factura: " + pago.ImporteFacturaProveedor.ToString("C") + "\n" +
                "Fecha pago: " + FechaTexto(pago.FechaPago) + "\n" +
                "Total: " + pago.Importe.ToString("C") + "\n" +
                "Estado: " + (pago.Estado != null ? pago.Estado.Descripcion : "-") + "\n" +
                "--- Medio de pago ---\n";

            if (pago.MedioDePago is BEEfectivo efectivo)
            {
                contenido +=
                    "Tipo: Efectivo\n" +
                    "Monto entregado: " + efectivo.MontoEntregado.ToString("C") + "\n" +
                    "Vuelto: " + efectivo.Vuelto.ToString("C") + "\n" +
                    "Moneda: " + efectivo.Moneda + "\n";
            }
            else if (pago.MedioDePago is BETransferencia transferencia)
            {
                contenido +=
                    "Tipo: Transferencia\n" +
                    "Banco: " + transferencia.Banco + "\n" +
                    "CBU/Alias: " + transferencia.CBU + "\n";
            }
            else if (pago.MedioDePago is BETarjeta tarjeta)
            {
                string ultimos4 = "-";

                if (!string.IsNullOrWhiteSpace(tarjeta.Numero) && tarjeta.Numero.Length >= 4)
                    ultimos4 = tarjeta.Numero.Substring(tarjeta.Numero.Length - 4);

                contenido +=
                    "Tipo: Tarjeta\n" +
                    "Marca: " + tarjeta.Marca + "\n" +
                    "Titular: " + tarjeta.Titular + "\n" +
                    "Banco emisor: " + tarjeta.BancoEmisor + "\n" +
                    "Ultimos 4 digitos: " + ultimos4 + "\n";
            }
            else
            {
                contenido += "Tipo: -\n";
            }

            return contenido;
        }

        #endregion

        #region "Carga de datos"

        private void RefrescarGrillaOrdenes()
        {
            dgvOrdenes.DataSource = null;
            dgvOrdenes.DataSource = OrdenarOrdenes(pagoProveedorBLL.ListarOrdenesRecibidas()).ToList();
            dgvOrdenes.ClearSelection();
            ordenSeleccionada = null;
        }

        private void RefrescarGrillaPagos()
        {
            dgvPagos.DataSource = null;
            dgvPagos.DataSource = OrdenarPagos(pagoProveedorBLL.ListarTodo()).ToList();
            dgvPagos.ClearSelection();
            pagoSeleccionado = null;
        }

        #endregion
    }
}


