using BE.Dominio;
using BE.Transporte;
using BLL.Transporte;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Transversales.Documentos;

namespace GlobalTransLines.UI.Gestion2_Transporte
{
    public partial class FrmBillOfLading : Form
    {
        #region "Atributos"

        private readonly BillOfLadingBLL billBLL = new BillOfLadingBLL();
        private readonly AsignacionBLL asignacionBLL = new AsignacionBLL();

        private BEAsignacion asignacionSeleccionada;
        private BEBillofLading billSeleccionado;
        private string columnaOrdenAsignacion = "Booking";
        private bool ordenAsignacionAscendente = true;
        private string columnaOrdenBill = "Numero";
        private bool ordenBillAscendente = true;

        #endregion

        #region "Constructor"

        public FrmBillOfLading()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos"

        private void FrmBillOfLading_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrillas();
                RefrescarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar Bill of Lading: " + ObtenerMensajeRaiz(ex),
                                "Bill of Lading", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAsignaciones_SelectionChanged(object sender, EventArgs e)
        {
            asignacionSeleccionada = dgvAsignaciones.CurrentRow != null
                ? dgvAsignaciones.CurrentRow.DataBoundItem as BEAsignacion
                : null;

            CargarAsignacionSeleccionada();
        }

        private void dgvBills_SelectionChanged(object sender, EventArgs e)
        {
            billSeleccionado = dgvBills.CurrentRow != null
                ? dgvBills.CurrentRow.DataBoundItem as BEBillofLading
                : null;

            CargarBillSeleccionado();
        }

        private void btnEmitir_Click(object sender, EventArgs e)
        {
            try
            {
                if (asignacionSeleccionada == null)
                {
                    MessageBox.Show("Debe seleccionar una asignacion de booking, contenedor y buque.",
                                    "Bill of Lading", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BEBillofLading bill = ConstruirBillDesdeAsignacion();
                billBLL.Guardar(bill);

                MessageBox.Show("Bill of Lading emitido correctamente.",
                                "Bill of Lading", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefrescarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerMensajeRaiz(ex),
                                "Bill of Lading", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            RefrescarTodo();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (billSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un Bill of Lading emitido.",
                                    "Bill of Lading", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BEBillofLading bill = billBLL.ListarObjeto(new BEBillofLading { Id = billSeleccionado.Id });
                ImprimirBill(bill);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir Bill of Lading: " + ObtenerMensajeRaiz(ex),
                                "Bill of Lading", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region "Interfaz"

        private void ConfigurarGrillas()
        {
            ConfigurarGrillaBase(dgvAsignaciones);
            ConfigurarGrillaBase(dgvBills);

            dgvAsignaciones.Columns.Add("Booking", "Booking");
            dgvAsignaciones.Columns.Add("Cliente", "Cliente");
            dgvAsignaciones.Columns.Add("Contenedor", "Contenedor");
            dgvAsignaciones.Columns.Add("Buque", "Buque");
            dgvAsignaciones.Columns.Add("Estado", "Estado");

            dgvAsignaciones.CellFormatting += (s, e) =>
            {
                var a = dgvAsignaciones.Rows[e.RowIndex].DataBoundItem as BEAsignacion;
                if (a == null) return;

                switch (dgvAsignaciones.Columns[e.ColumnIndex].Name)
                {
                    case "Booking":
                        e.Value = a.Booking != null ? a.Booking.Numero : "-";
                        break;
                    case "Cliente":
                        e.Value = a.Booking != null && a.Booking.Cliente != null ? a.Booking.Cliente.RazonSocial : "-";
                        break;
                    case "Contenedor":
                        e.Value = a.Contenedor != null ? a.Contenedor.Codigo : "-";
                        break;
                    case "Buque":
                        e.Value = a.Buque != null ? a.Buque.Nombre : "-";
                        break;
                    case "Estado":
                        e.Value = a.Estado != null ? a.Estado.Descripcion : "-";
                        break;
                }
            };

            foreach (DataGridViewColumn columna in dgvAsignaciones.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            dgvAsignaciones.ColumnHeaderMouseClick -= dgvAsignaciones_ColumnHeaderMouseClick;
            dgvAsignaciones.ColumnHeaderMouseClick += dgvAsignaciones_ColumnHeaderMouseClick;

            dgvBills.Columns.Add("Numero", "Numero");
            dgvBills.Columns["Numero"].DataPropertyName = "Numero";
            dgvBills.Columns.Add("Booking", "Booking");
            dgvBills.Columns.Add("Cliente", "Cliente");
            dgvBills.Columns.Add("Buque", "Buque");
            dgvBills.Columns.Add("PuertoDestino", "Puerto destino");
            dgvBills.Columns["PuertoDestino"].DataPropertyName = "PuertoDestino";

            dgvBills.CellFormatting += (s, e) =>
            {
                var b = dgvBills.Rows[e.RowIndex].DataBoundItem as BEBillofLading;
                if (b == null) return;

                switch (dgvBills.Columns[e.ColumnIndex].Name)
                {
                    case "Booking":
                        e.Value = b.Booking != null ? b.Booking.Numero : "-";
                        break;
                    case "Cliente":
                        e.Value = b.Cliente != null ? b.Cliente.RazonSocial : "-";
                        break;
                    case "Buque":
                        e.Value = b.Buque != null ? b.Buque.Nombre : "-";
                        break;
                }
            };

            foreach (DataGridViewColumn columna in dgvBills.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            dgvBills.ColumnHeaderMouseClick -= dgvBills_ColumnHeaderMouseClick;
            dgvBills.ColumnHeaderMouseClick += dgvBills_ColumnHeaderMouseClick;
        }

        private void ConfigurarGrillaBase(DataGridView grilla)
        {
            grilla.Dock = DockStyle.Fill;
            grilla.AutoGenerateColumns = false;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;
            grilla.ReadOnly = true;
            grilla.MultiSelect = false;
            grilla.RowHeadersVisible = false;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.BackgroundColor = SystemColors.Window;
            grilla.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
        }

        #endregion

        #region "Orden de grillas"

        private void dgvAsignaciones_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = dgvAsignaciones.Columns[e.ColumnIndex].Name;

            if (columnaOrdenAsignacion == columna)
                ordenAsignacionAscendente = !ordenAsignacionAscendente;
            else
            {
                columnaOrdenAsignacion = columna;
                ordenAsignacionAscendente = true;
            }

            RefrescarTodo();
        }

        private void dgvBills_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = dgvBills.Columns[e.ColumnIndex].Name;

            if (columnaOrdenBill == columna)
                ordenBillAscendente = !ordenBillAscendente;
            else
            {
                columnaOrdenBill = columna;
                ordenBillAscendente = true;
            }

            RefrescarTodo();
        }

        private List<BEAsignacion> OrdenarAsignaciones(List<BEAsignacion> asignaciones)
        {
            Func<BEAsignacion, object> criterio;

            switch (columnaOrdenAsignacion)
            {
                case "Cliente":
                    criterio = a => a.Booking != null && a.Booking.Cliente != null ? a.Booking.Cliente.RazonSocial : string.Empty;
                    break;
                case "Contenedor":
                    criterio = a => a.Contenedor != null ? a.Contenedor.Codigo : string.Empty;
                    break;
                case "Buque":
                    criterio = a => a.Buque != null ? a.Buque.Nombre : string.Empty;
                    break;
                case "Estado":
                    criterio = a => a.Estado != null ? a.Estado.Descripcion : string.Empty;
                    break;
                default:
                    criterio = a => a.Booking != null ? a.Booking.Numero : string.Empty;
                    break;
            }

            return ordenAsignacionAscendente
                ? asignaciones.OrderBy(criterio).ToList()
                : asignaciones.OrderByDescending(criterio).ToList();
        }

        private List<BEBillofLading> OrdenarBills(List<BEBillofLading> bills)
        {
            Func<BEBillofLading, object> criterio;

            switch (columnaOrdenBill)
            {
                case "Booking":
                    criterio = b => b.Booking != null ? b.Booking.Numero : string.Empty;
                    break;
                case "Cliente":
                    criterio = b => b.Cliente != null ? b.Cliente.RazonSocial : string.Empty;
                    break;
                case "Buque":
                    criterio = b => b.Buque != null ? b.Buque.Nombre : string.Empty;
                    break;
                case "PuertoDestino":
                    criterio = b => b.PuertoDestino ?? string.Empty;
                    break;
                default:
                    criterio = b => b.Numero ?? string.Empty;
                    break;
            }

            return ordenBillAscendente
                ? bills.OrderBy(criterio).ToList()
                : bills.OrderByDescending(criterio).ToList();
        }

        #endregion

        #region "Carga y armado"

        private void RefrescarTodo()
        {
            var bills = billBLL.ListarTodo();
            var bookingsConBill = bills.Where(b => b.Booking != null).Select(b => b.Booking.Id).ToList();

            dgvAsignaciones.DataSource = null;
            var asignaciones = asignacionBLL.ListarTodo()
                .Where(a => a.Booking != null &&
                            a.Contenedor != null &&
                            a.Buque != null &&
                            !bookingsConBill.Contains(a.Booking.Id))
                .ToList();
            dgvAsignaciones.DataSource = OrdenarAsignaciones(asignaciones);

            dgvBills.DataSource = null;
            dgvBills.DataSource = OrdenarBills(bills);

            asignacionSeleccionada = null;
            billSeleccionado = null;
            txtPuertoDestino.Clear();
            rtbDetalle.Clear();
            rtbDetalleEmitido.Clear();
            dgvAsignaciones.ClearSelection();
            dgvBills.ClearSelection();
        }

        private void CargarAsignacionSeleccionada()
        {
            rtbDetalle.Clear();
            txtPuertoDestino.Clear();

            if (asignacionSeleccionada == null)
                return;

            var booking = asignacionSeleccionada.Booking;
            var carga = booking != null ? booking.Cargas.FirstOrDefault() : null;
            string destino = carga != null && carga.Destino != null ? carga.Destino.ToString() : string.Empty;

            txtPuertoDestino.Text = destino;
            rtbDetalle.Text = ObtenerContenidoDesdeAsignacion(asignacionSeleccionada);
        }

        private void CargarBillSeleccionado()
        {
            rtbDetalleEmitido.Clear();

            if (billSeleccionado == null)
                return;

            rtbDetalleEmitido.Text = ObtenerContenidoBill(billSeleccionado);
        }

        private BEBillofLading ConstruirBillDesdeAsignacion()
        {
            var booking = asignacionSeleccionada.Booking;
            var carga = booking != null ? booking.Cargas.FirstOrDefault() : null;

            return new BEBillofLading
            {
                Booking = booking,
                Cliente = booking != null ? booking.Cliente : null,
                Carga = carga,
                Contenedor = asignacionSeleccionada.Contenedor,
                Buque = asignacionSeleccionada.Buque,
                FechaEmision = dtpFechaEmision.Value.Date,
                PuertoDestino = txtPuertoDestino.Text.Trim()
            };
        }

        #endregion

        #region "PDF y textos"

        private void ImprimirBill(BEBillofLading bill)
        {
            string numeroDocumento = !string.IsNullOrWhiteSpace(bill.Numero)
                ? bill.Numero
                : "BL-" + bill.Id.ToString("00000");

            var rutas = new RutasDocumentos();
            var gestor = new Transversales.Docu.GestorDocumentos(rutas);
            var generador = new GeneradorPdfBasico<BEBillofLading>(
                b => "Bill of Lading",
                b => ObtenerContenidoBill(b),
                null,
                null,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "FacturaEncabezadoFondo.png"),
                b => "BILL OF LADING\n" +
                     "Nro. " + numeroDocumento + "\n" +
                     "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")
            );

            string rutaPdf = gestor.ObtenerOGenerarPdf(
                DocumentoTipo.BillOfLading,
                numeroDocumento,
                ruta => generador.GenerarBillOfLading(ruta, bill)
            );

            System.Diagnostics.Process.Start(rutaPdf);
        }

        private string ObtenerContenidoDesdeAsignacion(BEAsignacion asignacion)
        {
            var booking = asignacion.Booking;
            var carga = booking != null ? booking.Cargas.FirstOrDefault() : null;

            return
                "--- Origen documental ---\n" +
                "Booking: " + (booking != null ? booking.Numero : "-") + "\n" +
                "Cliente: " + (booking != null && booking.Cliente != null ? booking.Cliente.RazonSocial : "-") + "\n" +
                "Contenedor: " + (asignacion.Contenedor != null ? asignacion.Contenedor.Codigo : "-") + "\n" +
                "Buque: " + (asignacion.Buque != null ? asignacion.Buque.Nombre : "-") + "\n" +
                "--- Carga ---\n" +
                "Descripcion: " + (carga != null ? carga.Descripcion : "-") + "\n" +
                "Peso: " + (carga != null ? carga.PesoKg.ToString("N2") : "0") + " kg\n" +
                "Volumen: " + (carga != null ? carga.VolumenM3.ToString("N2") : "0") + " m3\n" +
                "Destino: " + txtPuertoDestino.Text + "\n";
        }

        private string ObtenerContenidoBill(BEBillofLading bill)
        {
            return
                "--- Datos principales ---\n" +
                "Numero: " + bill.Numero + "\n" +
                "Fecha emision: " + bill.FechaEmision.ToString("dd/MM/yyyy") + "\n" +
                "Booking: " + (bill.Booking != null ? bill.Booking.Numero : "-") + "\n" +
                "Cliente: " + (bill.Cliente != null ? bill.Cliente.RazonSocial : "-") + "\n" +
                "Buque: " + (bill.Buque != null ? bill.Buque.Nombre : "-") + "\n" +
                "Contenedor: " + (bill.Contenedor != null ? bill.Contenedor.Codigo : "-") + "\n" +
                "Puerto destino: " + bill.PuertoDestino + "\n" +
                "--- Carga ---\n" +
                "Descripcion: " + (bill.Carga != null ? bill.Carga.Descripcion : "-") + "\n" +
                "Peso: " + (bill.Carga != null ? bill.Carga.PesoKg.ToString("N2") : "0") + " kg\n" +
                "Volumen: " + (bill.Carga != null ? bill.Carga.VolumenM3.ToString("N2") : "0") + " m3\n";
        }

        private string ObtenerMensajeRaiz(Exception ex)
        {
            Exception actual = ex;
            while (actual.InnerException != null)
                actual = actual.InnerException;
            return actual.Message;
        }

        #endregion
    }
}
