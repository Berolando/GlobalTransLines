using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BE.Transporte;
using BE.Finanzas;
using BLL.Transporte;
using BLL.Finanzas;
using BE.Dominio;
using System.IO;
using Transversales.Documentos;


namespace GlobalTransLines.UI.Gestion3_Facturacion
{
    public partial class FrmFactura : Form
    {
        #region "Atributos"
        private readonly BookingBLL gestorBooking = new BookingBLL();
        private readonly FacturaBLL gestorFactura = new FacturaBLL();

        private BEBooking bookingSeleccionado;
        private BEFactura facturaSeleccionada;
        private string columnaOrdenBooking = "FechaGeneracion";
        private bool ordenBookingAscendente = false;
        private string columnaOrdenFactura = "Fecha";
        private bool ordenFacturaAscendente = false;
        #endregion

        #region "Constructor"
        public FrmFactura()
        {
            InitializeComponent();
        }
        #endregion

        #region "Eventos del formulario"
        private void FrmFactura_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrillas();
                RefrescarGrillas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar facturación: " + ex.Message);
            }
        }
        #endregion

        #region "Configuración Visual"
        private void ConfigurarGrillas()
        {
            // BOOKING
            dgvBookings.AutoGenerateColumns = true;
            dgvBookings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBookings.MultiSelect = false;
            dgvBookings.ReadOnly = true;
            dgvBookings.RowHeadersVisible = false;
            dgvBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBookings.AllowUserToAddRows = false;
            dgvBookings.AllowUserToDeleteRows = false;
            dgvBookings.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            dgvBookings.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
            dgvBookings.SelectionChanged += dgvBookings_SelectionChanged;
            dgvBookings.ColumnHeaderMouseClick -= dgvBookings_ColumnHeaderMouseClick;
            dgvBookings.ColumnHeaderMouseClick += dgvBookings_ColumnHeaderMouseClick;

            // FACTURAS
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
            dgvFacturas.SelectionChanged += dgvFacturas_SelectionChanged;
            dgvFacturas.ColumnHeaderMouseClick -= dgvFacturas_ColumnHeaderMouseClick;
            dgvFacturas.ColumnHeaderMouseClick += dgvFacturas_ColumnHeaderMouseClick;
        }

        private void RefrescarGrillas()
        {

            var facturas = gestorFactura.ListarTodo();

            var idsBookingFacturados = facturas
                .Where(f => f.Booking != null)
                .Select(f => f.Booking.Id)
                .Distinct()
                .ToHashSet();

            var bookingsAsignados = gestorBooking.ListarTodo()
                                    .Where(b => b.Estado != null && b.Estado.Codigo == EstadoBooking.Asignado.Codigo)
                                    .Where(b => !idsBookingFacturados.Contains(b.Id))
                                    .ToList();

            dgvBookings.DataSource = bookingsAsignados;
            dgvFacturas.DataSource = facturas;

            dgvBookings.ClearSelection();
            dgvFacturas.ClearSelection();

            bookingSeleccionado = null;
            facturaSeleccionada = null;

            LimpiarCampos();
        }
        #endregion

        #region "Cargar Booking en formulario"
        private void CargarBookingEnFormulario(BEBooking booking)
        {
            bookingSeleccionado = booking;
            var facturaBLL = new FacturaBLL();

            decimal subtotal = booking.Cotizacion?.MontoTotal ?? 0;
            decimal impuesto = subtotal * 0.21m;
            decimal total = subtotal + impuesto;

            txtNumero.Text = facturaBLL.ObtenerProximoNumeroPreview(TipoFactura.FA);
            txtMonto.Text = subtotal.ToString("N2");
            txtImpuestos.Text = impuesto.ToString("N2");
            txtSubtotal.Text = subtotal.ToString("N2");
            txtTotal.Text = total.ToString("N2");

            ordenDeCompraDtPkFechaEmision.Value = DateTime.Now;
        }
        #endregion

        #region "Selección Booking"
        private void dgvBookings_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                bookingSeleccionado = dgvBookings.CurrentRow?.DataBoundItem as BEBooking;

                if (bookingSeleccionado == null)
                {
                    LimpiarCampos();
                    return;
                }

                // Subtotal = monto total cotizado
                decimal subtotal = bookingSeleccionado.Cotizacion?.MontoTotal ?? 0;

                // Impuestos (IVA 21%)
                decimal impuesto = subtotal * 0.21m;

                // Total
                decimal total = subtotal + impuesto;

                
                txtNumero.Text = "(automatico al guardar)";
                txtMonto.Text = subtotal.ToString("N2");
                txtImpuestos.Text = impuesto.ToString("N2");
                txtSubtotal.Text = subtotal.ToString("N2");
                txtTotal.Text = total.ToString("N2");

                ordenDeCompraDtPkFechaEmision.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar booking: " + ex.Message);
            }
        }
        private void dgvBookings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var bk = dgvBookings.Rows[e.RowIndex].DataBoundItem as BEBooking;
            if (bk == null) return;

            CargarBookingEnFormulario(bk);
        }


        #endregion

        #region "Selección Factura"
        private void dgvFacturas_SelectionChanged(object sender, EventArgs e)
        {
            facturaSeleccionada = dgvFacturas.CurrentRow?.DataBoundItem as BEFactura;
        }
        #endregion

        #region "Orden de grillas"

        private void dgvBookings_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = dgvBookings.Columns[e.ColumnIndex].Name;
            ordenBookingAscendente = columnaOrdenBooking == columna ? !ordenBookingAscendente : true;
            columnaOrdenBooking = columna;
            OrdenarBookings();
        }

        private void dgvFacturas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = dgvFacturas.Columns[e.ColumnIndex].Name;
            ordenFacturaAscendente = columnaOrdenFactura == columna ? !ordenFacturaAscendente : true;
            columnaOrdenFactura = columna;
            OrdenarFacturas();
        }

        private void OrdenarBookings()
        {
            var lista = (dgvBookings.DataSource as IEnumerable<BEBooking>)?.ToList();
            if (lista == null)
                return;

            // Ordena la lista visible para respetar el filtro actual de bookings pendientes de facturacion.
            switch (columnaOrdenBooking)
            {
                case "Id":
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.Id).ToList() : lista.OrderByDescending(b => b.Id).ToList();
                    break;
                case "Numero":
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.Numero).ToList() : lista.OrderByDescending(b => b.Numero).ToList();
                    break;
                case "Cliente":
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.Cliente?.RazonSocial).ToList() : lista.OrderByDescending(b => b.Cliente?.RazonSocial).ToList();
                    break;
                case "Cotizacion":
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.Cotizacion?.Numero).ToList() : lista.OrderByDescending(b => b.Cotizacion?.Numero).ToList();
                    break;
                case "TipoContenedorCodigo":
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.TipoContenedorCodigo).ToList() : lista.OrderByDescending(b => b.TipoContenedorCodigo).ToList();
                    break;
                case "FechaTentativaSalida":
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.FechaTentativaSalida).ToList() : lista.OrderByDescending(b => b.FechaTentativaSalida).ToList();
                    break;
                case "Estado":
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.Estado?.Descripcion).ToList() : lista.OrderByDescending(b => b.Estado?.Descripcion).ToList();
                    break;
                case "Activo":
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.Activo).ToList() : lista.OrderByDescending(b => b.Activo).ToList();
                    break;
                default:
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.FechaGeneracion).ToList() : lista.OrderByDescending(b => b.FechaGeneracion).ToList();
                    break;
            }

            dgvBookings.DataSource = null;
            dgvBookings.DataSource = lista;
        }

        private void OrdenarFacturas()
        {
            var lista = (dgvFacturas.DataSource as IEnumerable<BEFactura>)?.ToList();
            if (lista == null)
                return;

            // Ordena la lista visible para mantener busquedas y no volver a cargar todo desde XML.
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

        #endregion

        #region "Botón Guardar"
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (bookingSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un booking para facturar.");
                    return;
                }
                    decimal subtotal = decimal.Parse(txtSubtotal.Text);
                    decimal impuesto = decimal.Parse(txtImpuestos.Text);

                    // Crear factura
                    BEFactura factura = new BEFactura
                    {
                        Booking = bookingSeleccionado,
                        Subtotal = subtotal,
                        Impuesto = impuesto,
                        Fecha = ordenDeCompraDtPkFechaEmision.Value,
                        Tipo = TipoFactura.FA,
                        Estado = EstadoFactura.Emitida
                    };

                    gestorFactura.Guardar(factura);

                    MessageBox.Show("Factura emitida correctamente.");

                    RefrescarGrillas();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al emitir factura: " + ex.Message);
            }
        }
           
        #endregion

        #region "Botón Anular"
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (facturaSeleccionada == null)
                {
                    MessageBox.Show("Seleccione una factura.");
                    return;
                }
                gestorFactura.Eliminar(facturaSeleccionada);

                MessageBox.Show("Factura anulada correctamente.");

                RefrescarGrillas();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al anular la  factura: " + ex.Message);
            }
        }
        #endregion

        #region "Botón Imprimir "
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (facturaSeleccionada == null)
                {
                    MessageBox.Show("Seleccione una factura.");
                    return;
                }

                // Reconstruye la factura COMPLETA desde la BLL
                var factura = gestorFactura.ListarObjeto(
                    new BEFactura { Id = facturaSeleccionada.Id }
                );

                var rutas = new RutasDocumentos();
                var gestor = new Transversales.Docu.GestorDocumentos(rutas);

                var generador = new GeneradorPdfBasico<BEFactura>(
                    f => "Factura",
                    f => string.Empty,
                    rutaLogo: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "FacturaEncabezadoFondo.png")
                );
                string rutaPdf = gestor.ObtenerRutaPdfParaEscritura(
                    DocumentoTipo.Factura,
                    factura.NumeroFactura
                );

                // La factura se regenera desde XML para mostrar siempre el formato vigente del comprobante.
                generador.GenerarFactura(rutaPdf, factura);

                System.Diagnostics.Process.Start(rutaPdf);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir factura: " + ex.Message);
            }
        }





        #endregion

        #region "Búsqueda"
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string filtro = txtBuscar.Text.Trim().ToLower();

                var lista = gestorFactura.ListarTodo()
                    .Where(f =>
                        f.NumeroFactura.ToLower().Contains(filtro) ||
                        (f.Booking?.Numero ?? "").ToLower().Contains(filtro))
                    .ToList();

                dgvFacturas.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }
        private void btnBuscarBooking_Click(object sender, EventArgs e)
        {
            try
            {
                string filtro = txtBuscarBooking.Text.Trim().ToLower();

                var facturas = gestorFactura.ListarTodo();
                var idsBookingFacturados = facturas
                    .Where(f => f.Booking != null)
                    .Select(f => f.Booking.Id)
                    .Distinct()
                    .ToHashSet();

                var lista = gestorBooking.ListarTodo()
                    .Where(b =>
                        b.Numero.ToLower().Contains(filtro) ||
                        (b.Cliente?.RazonSocial ?? "").ToLower().Contains(filtro))
                    .Where(b => b.Estado != null && b.Estado.Codigo == EstadoBooking.Asignado.Codigo)
                    .Where(b => !idsBookingFacturados.Contains(b.Id))
                    .ToList();

                dgvBookings.DataSource = lista;
                dgvBookings.ClearSelection();
                bookingSeleccionado = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar booking: " + ex.Message);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        private void txtBuscarBooking_TextChanged(object sender, EventArgs e)
        {
            btnBuscarBooking_Click(sender, e);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        
        #endregion

        #region "Limpiar campos"
        private void LimpiarCampos()
        {
            txtNumero.Text = "";
            txtMonto.Text = "";
            txtImpuestos.Text = "";
            txtSubtotal.Text = "";
            txtTotal.Text = "";
        }

        #endregion

        
    }
}





