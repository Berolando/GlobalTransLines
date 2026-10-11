using BE.Dominio;
using BE.Finanzas;
using BLL.Finanzas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Transversales.Documentos;

namespace GlobalTransLines.UI.Gestion3_Facturacion
{
    public partial class FrmPago : Form
    {
        #region "Atributos y Constructor"

        private FacturaBLL facturaBLL = new FacturaBLL();
        private PagoBLL pagoBLL = new PagoBLL();

        private BEFactura facturaSeleccionada;
        private BEPago pagoSeleccionado;
        private string columnaOrdenFactura = "Fecha";
        private bool ordenFacturaAscendente = false;
        private string columnaOrdenPago = "FechaPago";
        private bool ordenPagoAscendente = false;

        public FrmPago()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmPago_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrillas();
                ConfigurarGrillaPagos();
                ConfigurarComboMedioPago();
                ConfigurarControlesIniciales();
                RefrescarGrilla();
                RefrescarGrillaPagos();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar formulario de pagos: " + ex.Message,
                                "Pagos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (facturaSeleccionada == null)
                {
                    MessageBox.Show("Debe seleccionar una factura pendiente de pago.",
                                    "Pagos",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                BEPago pago = ConstruirPagoDesdeFormulario();

                pagoBLL.Guardar(pago);

                MessageBox.Show("Pago registrado correctamente. La factura quedó marcada como pagada.",
                                "Pagos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                RefrescarGrilla();
                LimpiarControles();
                RefrescarGrillaPagos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar pago: " + ex.Message,
                                "Pagos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar controles: " + ex.Message,
                                "Pagos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                RefrescarGrilla();
                LimpiarControles();
                RefrescarGrillaPagos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar facturas: " + ex.Message,
                                "Pagos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        private void btnImprimirComprobante_Click(object sender, EventArgs e)
        {
            try
            {
                if (pagoSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un pago registrado.",
                                    "Pagos",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                BEPago pago = pagoBLL.ListarObjeto(new BEPago { Id = pagoSeleccionado.Id });

                if (pago == null)
                {
                    MessageBox.Show("No se encontró el pago seleccionado.",
                                    "Pagos",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                string numeroFactura = pago.Factura != null
                    ? pago.Factura.NumeroFactura
                    : pago.Id.ToString();

                string numeroComprobante = "PAGO-" + numeroFactura;

                var rutas = new RutasDocumentos();
                var gestor = new Transversales.Docu.GestorDocumentos(rutas);

                var generador = new GeneradorPdfBasico<BEPago>(
                    p => "Comprobante de Pago",
                    p =>
                    {
                        string factura = p.Factura != null ? p.Factura.NumeroFactura : "-";

                        string contenido =
                            "Fecha de pago: " + p.FechaPago.ToString("dd/MM/yyyy HH:mm") + "\n" +
                            "Factura: " + factura + "\n" +
                            "Importe: " + p.Importe.ToString("C") + "\n" +
                            "Estado: " + (p.Estado != null ? p.Estado.ToString() : "-") + "\n\n" +
                            "--- Medio de Pago ---\n";

                        if (p.MedioDePago is BEEfectivo efectivo)
                        {
                            contenido +=
                                "Tipo: Efectivo\n" +
                                "Monto entregado: " + efectivo.MontoEntregado.ToString("C") + "\n" +
                                "Vuelto: " + efectivo.Vuelto.ToString("C") + "\n" +
                                "Moneda: " + efectivo.Moneda + "\n";
                        }
                        else if (p.MedioDePago is BETransferencia transferencia)
                        {
                            contenido +=
                                "Tipo: Transferencia\n" +
                                "Banco: " + transferencia.Banco + "\n" +
                                "CBU/Alias: " + transferencia.CBU + "\n";
                        }
                        else if (p.MedioDePago is BETarjeta tarjeta)
                        {
                            string ultimos4 = "-";

                            if (!string.IsNullOrWhiteSpace(tarjeta.Numero) && tarjeta.Numero.Length >= 4)
                                ultimos4 = tarjeta.Numero.Substring(tarjeta.Numero.Length - 4);

                            contenido +=
                                "Tipo: Tarjeta\n" +
                                "Marca: " + tarjeta.Marca + "\n" +
                                "Titular: " + tarjeta.Titular + "\n" +
                                "Banco emisor: " + tarjeta.BancoEmisor + "\n" +
                                "Últimos 4 dígitos: " + ultimos4 + "\n";
                        }
                        else
                        {
                            contenido += "Tipo: -\n";
                        }

                        return contenido;
                    },
                    null, null, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Logo.jpg"),
                    p =>"RECIBO DE PAGO\n" +
                        "N° " + numeroComprobante + "\n" +
                        "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")
                );

                string rutaPdf = gestor.ObtenerOGenerarPdf(
                    DocumentoTipo.PagoCliente,
                    numeroComprobante,
                    ruta => generador.GenerarDocumento(ruta, pago)
                );

                System.Diagnostics.Process.Start(rutaPdf);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir comprobante de pago: " + ex.Message,
                                "Pagos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnActualizar2_Click(object sender, EventArgs e)
        {
            try
            {
                RefrescarGrilla();
                LimpiarControles();
                RefrescarGrillaPagos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar facturas: " + ex.Message,
                                "Pagos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar formulario: " + ex.Message,
                                "Pagos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        #endregion
        #region "Eventos de grilla"

        private void dgvFacturas_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvFacturas.CurrentRow == null)
                    return;

                facturaSeleccionada = dgvFacturas.CurrentRow.DataBoundItem as BEFactura;

                CargarFacturaSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar factura: " + ex.Message,
                                "Pagos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void dgvPagos_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvPagos.CurrentRow == null)
                    return;
                pagoSeleccionado = dgvPagos.CurrentRow.DataBoundItem as BEPago;
                CargarPagoSeleccionado();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar pago: " + ex.Message,
                                "Pagos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de controles"

        private void cmbMedioPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MostrarMedioPagoSeleccionado();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar medio de pago: " + ex.Message,
                                "Pagos",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
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
            dgvFacturas.AutoGenerateColumns = true;
            dgvFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFacturas.MultiSelect = false;
            dgvFacturas.ReadOnly = true;
            dgvFacturas.RowHeadersVisible = false;
            dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFacturas.AllowUserToAddRows = false;
            dgvFacturas.AllowUserToDeleteRows = false;
            dgvFacturas.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            dgvFacturas.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);

            dgvFacturas.SelectionChanged -= dgvFacturas_SelectionChanged;
            dgvFacturas.SelectionChanged += dgvFacturas_SelectionChanged;
            dgvFacturas.ColumnHeaderMouseClick -= dgvFacturas_ColumnHeaderMouseClick;
            dgvFacturas.ColumnHeaderMouseClick += dgvFacturas_ColumnHeaderMouseClick;
        }
        private void ConfigurarGrillaPagos()
        {
            dgvPagos.AutoGenerateColumns = true;
            dgvPagos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPagos.MultiSelect = false;
            dgvPagos.ReadOnly = true;
            dgvPagos.RowHeadersVisible = false;
            dgvPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPagos.AllowUserToAddRows = false;
            dgvPagos.AllowUserToDeleteRows = false;
            dgvPagos.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            dgvPagos.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
            dgvPagos.SelectionChanged -= dgvPagos_SelectionChanged;
            dgvPagos.SelectionChanged += dgvPagos_SelectionChanged;
            dgvPagos.ColumnHeaderMouseClick -= dgvPagos_ColumnHeaderMouseClick;
            dgvPagos.ColumnHeaderMouseClick += dgvPagos_ColumnHeaderMouseClick;
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
            txtFactura.ReadOnly = true;
            txtTotalFactura.ReadOnly = true;
            txtImportePago.ReadOnly = true;
            txtVuelto.ReadOnly = true;

            dtpFechaPago.Format = DateTimePickerFormat.Short;
            dtpVencimiento.Format = DateTimePickerFormat.Short;

            dtpFechaPago.Value = DateTime.Now;
            dtpVencimiento.Value = DateTime.Now.AddYears(1);

            txtMontoEntregado.TextChanged -= txtMontoEntregado_TextChanged;
            txtMontoEntregado.TextChanged += txtMontoEntregado_TextChanged;
        }

        #endregion

        #region "Orden de grillas"

        private void dgvFacturas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = dgvFacturas.Columns[e.ColumnIndex].Name;
            ordenFacturaAscendente = columnaOrdenFactura == columna ? !ordenFacturaAscendente : true;
            columnaOrdenFactura = columna;
            OrdenarFacturas();
        }

        private void dgvPagos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = dgvPagos.Columns[e.ColumnIndex].Name;
            ordenPagoAscendente = columnaOrdenPago == columna ? !ordenPagoAscendente : true;
            columnaOrdenPago = columna;
            OrdenarPagos();
        }

        private void OrdenarFacturas()
        {
            var lista = (dgvFacturas.DataSource as IEnumerable<BEFactura>)?.ToList();
            if (lista == null)
                return;

            // Ordena la lista visible para respetar que esta grilla solo muestra facturas pendientes.
            switch (columnaOrdenFactura)
            {
                case "Id":
                    lista = ordenFacturaAscendente ? lista.OrderBy(f => f.Id).ToList() : lista.OrderByDescending(f => f.Id).ToList();
                    break;
                case "NumeroFactura":
                    lista = ordenFacturaAscendente ? lista.OrderBy(f => f.NumeroFactura).ToList() : lista.OrderByDescending(f => f.NumeroFactura).ToList();
                    break;
                case "Booking":
                    lista = ordenFacturaAscendente ? lista.OrderBy(f => f.Booking?.Numero).ToList() : lista.OrderByDescending(f => f.Booking?.Numero).ToList();
                    break;
                case "Estado":
                    lista = ordenFacturaAscendente ? lista.OrderBy(f => f.Estado?.Descripcion).ToList() : lista.OrderByDescending(f => f.Estado?.Descripcion).ToList();
                    break;
                case "Impuesto":
                    lista = ordenFacturaAscendente ? lista.OrderBy(f => f.Impuesto).ToList() : lista.OrderByDescending(f => f.Impuesto).ToList();
                    break;
                case "Subtotal":
                    lista = ordenFacturaAscendente ? lista.OrderBy(f => f.Subtotal).ToList() : lista.OrderByDescending(f => f.Subtotal).ToList();
                    break;
                case "Tipo":
                    lista = ordenFacturaAscendente ? lista.OrderBy(f => f.Tipo?.Descripcion).ToList() : lista.OrderByDescending(f => f.Tipo?.Descripcion).ToList();
                    break;
                case "Total":
                    lista = ordenFacturaAscendente ? lista.OrderBy(f => f.Total).ToList() : lista.OrderByDescending(f => f.Total).ToList();
                    break;
                case "Activo":
                    lista = ordenFacturaAscendente ? lista.OrderBy(f => f.Activo).ToList() : lista.OrderByDescending(f => f.Activo).ToList();
                    break;
                default:
                    lista = ordenFacturaAscendente ? lista.OrderBy(f => f.Fecha).ToList() : lista.OrderByDescending(f => f.Fecha).ToList();
                    break;
            }

            dgvFacturas.DataSource = null;
            dgvFacturas.DataSource = lista;
        }

        private void OrdenarPagos()
        {
            var lista = (dgvPagos.DataSource as IEnumerable<BEPago>)?.ToList();
            if (lista == null)
                return;

            // Ordena los pagos registrados sin volver a leer el XML.
            switch (columnaOrdenPago)
            {
                case "Id":
                    lista = ordenPagoAscendente ? lista.OrderBy(p => p.Id).ToList() : lista.OrderByDescending(p => p.Id).ToList();
                    break;
                case "Factura":
                    lista = ordenPagoAscendente ? lista.OrderBy(p => p.Factura?.NumeroFactura).ToList() : lista.OrderByDescending(p => p.Factura?.NumeroFactura).ToList();
                    break;
                case "Fecha":
                    lista = ordenPagoAscendente ? lista.OrderBy(p => p.Fecha).ToList() : lista.OrderByDescending(p => p.Fecha).ToList();
                    break;
                case "MedioDePago":
                    lista = ordenPagoAscendente ? lista.OrderBy(p => p.MedioDePago?.Nombre).ToList() : lista.OrderByDescending(p => p.MedioDePago?.Nombre).ToList();
                    break;
                case "Importe":
                    lista = ordenPagoAscendente ? lista.OrderBy(p => p.Importe).ToList() : lista.OrderByDescending(p => p.Importe).ToList();
                    break;
                case "Estado":
                    lista = ordenPagoAscendente ? lista.OrderBy(p => p.Estado?.Descripcion).ToList() : lista.OrderByDescending(p => p.Estado?.Descripcion).ToList();
                    break;
                case "Activo":
                    lista = ordenPagoAscendente ? lista.OrderBy(p => p.Activo).ToList() : lista.OrderByDescending(p => p.Activo).ToList();
                    break;
                default:
                    lista = ordenPagoAscendente ? lista.OrderBy(p => p.FechaPago).ToList() : lista.OrderByDescending(p => p.FechaPago).ToList();
                    break;
            }

            dgvPagos.DataSource = null;
            dgvPagos.DataSource = lista;
        }

        #endregion

        #region "Métodos auxiliares"

        private void CargarFacturaSeleccionada()
        {
            if (facturaSeleccionada == null)
            {
                txtFactura.Text = string.Empty;
                txtTotalFactura.Text = string.Empty;
                txtImportePago.Text = string.Empty;
                return;
            }

            txtFactura.Text = facturaSeleccionada.NumeroFactura;
            txtTotalFactura.Text = facturaSeleccionada.Total.ToString("N2");
            txtImportePago.Text = facturaSeleccionada.Total.ToString("N2");

            CalcularVuelto();
        }
        private void CargarPagoSeleccionado()
        {
            rtbDetallePago.Clear();

            if (pagoSeleccionado == null)
                return;

            string numeroFactura = pagoSeleccionado.Factura != null
                ? pagoSeleccionado.Factura.NumeroFactura
                : "-";

            string estado = pagoSeleccionado.Estado != null
                ? pagoSeleccionado.Estado.ToString()
                : "-";

            rtbDetallePago.AppendText("COMPROBANTE DE PAGO" + Environment.NewLine);
            rtbDetallePago.AppendText("----------------------------------------" + Environment.NewLine);
            rtbDetallePago.AppendText("Factura: " + numeroFactura + Environment.NewLine);
            rtbDetallePago.AppendText("Fecha de pago: " + pagoSeleccionado.FechaPago.ToString("dd/MM/yyyy HH:mm") + Environment.NewLine);
            rtbDetallePago.AppendText("Importe: " + pagoSeleccionado.Importe.ToString("C") + Environment.NewLine);
            rtbDetallePago.AppendText("Estado: " + estado + Environment.NewLine);
            rtbDetallePago.AppendText(Environment.NewLine);

            if (pagoSeleccionado.MedioDePago is BEEfectivo efectivo)
            {
                rtbDetallePago.AppendText("Medio de pago: Efectivo" + Environment.NewLine);
                rtbDetallePago.AppendText("Monto entregado: " + efectivo.MontoEntregado.ToString("C") + Environment.NewLine);
                rtbDetallePago.AppendText("Vuelto: " + efectivo.Vuelto.ToString("C") + Environment.NewLine);
                rtbDetallePago.AppendText("Moneda: " + efectivo.Moneda + Environment.NewLine);
            }
            else if (pagoSeleccionado.MedioDePago is BETransferencia transferencia)
            {
                rtbDetallePago.AppendText("Medio de pago: Transferencia" + Environment.NewLine);
                rtbDetallePago.AppendText("Banco: " + transferencia.Banco + Environment.NewLine);
                rtbDetallePago.AppendText("CBU/Alias: " + transferencia.CBU + Environment.NewLine);
            }
            else if (pagoSeleccionado.MedioDePago is BETarjeta tarjeta)
            {
                string ultimos4 = "-";

                if (!string.IsNullOrWhiteSpace(tarjeta.Numero) && tarjeta.Numero.Length >= 4)
                    ultimos4 = tarjeta.Numero.Substring(tarjeta.Numero.Length - 4);

                rtbDetallePago.AppendText("Medio de pago: Tarjeta" + Environment.NewLine);
                rtbDetallePago.AppendText("Marca: " + tarjeta.Marca + Environment.NewLine);
                rtbDetallePago.AppendText("Titular: " + tarjeta.Titular + Environment.NewLine);
                rtbDetallePago.AppendText("Banco emisor: " + tarjeta.BancoEmisor + Environment.NewLine);
                rtbDetallePago.AppendText("Últimos 4 dígitos: " + ultimos4 + Environment.NewLine);
            }
            else
            {
                rtbDetallePago.AppendText("Medio de pago: -" + Environment.NewLine);
            }
        }
        private BEPago ConstruirPagoDesdeFormulario()
        {
            decimal importe = facturaSeleccionada.Total;

            BEPago pago = new BEPago();
            pago.Factura = facturaSeleccionada;
            pago.Fecha = DateTime.Now;
            pago.FechaPago = dtpFechaPago.Value;
            pago.Importe = importe;
            pago.Estado = EstadoPago.Pagado;
            pago.MedioDePago = ConstruirMedioDePago(importe);

            return pago;
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
                throw new Exception("El monto entregado no puede ser menor al total de la factura.");

            BEEfectivo efectivo = new BEEfectivo();
            efectivo.Nombre = "Efectivo";
            efectivo.MontoEntregado = Convert.ToDouble(montoEntregado);
            efectivo.Moneda = "ARS";
            efectivo.FechaPago = dtpFechaPago.Value;
            efectivo.Vuelto = montoEntregado - importe;

            return efectivo;
        }

        private BETransferencia ConstruirTransferencia()
        {
            if (string.IsNullOrWhiteSpace(txtBanco.Text))
                throw new Exception("Debe ingresar el banco de la transferencia.");

            if (string.IsNullOrWhiteSpace(txtCBU.Text))
                throw new Exception("Debe ingresar el CBU o Alias de la transferencia.");

            BETransferencia transferencia = new BETransferencia();
            transferencia.Nombre = "Transferencia";
            transferencia.Banco = txtBanco.Text.Trim();

            // La clase BETransferencia actual solo persiste Banco y CBU.
            // El número de operación queda visual por ahora; no se guarda hasta agregar propiedad en BE/MPP.
            transferencia.CBU = txtCBU.Text.Trim();

            return transferencia;
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

            BETarjeta tarjeta = new BETarjeta();
            tarjeta.Nombre = "Tarjeta";
            tarjeta.Numero = txtNumeroTarjeta.Text.Trim();
            tarjeta.Titular = txtTitular.Text.Trim();
            tarjeta.CVV = txtCVV.Text.Trim();
            tarjeta.Marca = txtMarca.Text.Trim();
            tarjeta.BancoEmisor = txtBancoEmisor.Text.Trim();
            tarjeta.Vencimiento = dtpVencimiento.Value;

            return tarjeta;
        }

        private decimal ObtenerDecimal(string texto, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new Exception("Debe ingresar " + nombreCampo + ".");

            decimal valor;
            string normalizado = texto.Replace(".", ",");

            if (!decimal.TryParse(normalizado, NumberStyles.Any, CultureInfo.CurrentCulture, out valor))
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
            if (facturaSeleccionada == null)
            {
                txtVuelto.Text = string.Empty;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMontoEntregado.Text))
            {
                txtVuelto.Text = string.Empty;
                return;
            }

            decimal montoEntregado;
            string normalizado = txtMontoEntregado.Text.Replace(".", ",");

            if (!decimal.TryParse(normalizado, NumberStyles.Any, CultureInfo.CurrentCulture, out montoEntregado))
            {
                txtVuelto.Text = string.Empty;
                return;
            }

            decimal vuelto = montoEntregado - facturaSeleccionada.Total;

            txtVuelto.Text = vuelto >= 0 ? vuelto.ToString("N2") : "0,00";
        }

        private void LimpiarControles()
        {
            facturaSeleccionada = null;

            txtFactura.Text = string.Empty;
            txtTotalFactura.Text = string.Empty;
            txtImportePago.Text = string.Empty;

            txtMontoEntregado.Text = string.Empty;
            txtVuelto.Text = string.Empty;

            txtBanco.Text = string.Empty;
            txtCBU.Text = string.Empty;
            txtNumeroOperacion.Text = string.Empty;

            txtNumeroTarjeta.Text = string.Empty;
            txtTitular.Text = string.Empty;
            txtCVV.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtBancoEmisor.Text = string.Empty;

            dtpFechaPago.Value = DateTime.Now;
            dtpVencimiento.Value = DateTime.Now.AddYears(1);

            if (cmbMedioPago.Items.Count > 0)
                cmbMedioPago.SelectedIndex = 0;

            dgvFacturas.ClearSelection();
            rtbDetallePago.Clear();
            pagoSeleccionado = null;
        }

        #endregion

        #region "Carga de datos"

        private void RefrescarGrilla()
        {
            List<BEFactura> facturas = facturaBLL.ListarTodo();

            var pendientes = facturas
                .Where(f => f.Estado != null &&
                            f.Estado.Codigo == EstadoFactura.Emitida.Codigo)
                .ToList();

            dgvFacturas.DataSource = null;
            dgvFacturas.DataSource = pendientes;

            dgvFacturas.ClearSelection();
        }

        private void RefrescarGrillaPagos()
        {
            List<BEPago> pagos = pagoBLL.ListarTodo();
            dgvPagos.DataSource = null;
            dgvPagos.DataSource = pagos;
            dgvPagos.ClearSelection();
            pagoSeleccionado = null;
        }



        #endregion

    }
}
