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
    public partial class FrmManifiesto : Form
    {
        #region "Atributos"

        private readonly ManifiestoBLL manifiestoBLL = new ManifiestoBLL();
        private readonly BuqueBLL buqueBLL = new BuqueBLL();
        private readonly ContenedorBLL contenedorBLL = new ContenedorBLL();
        private readonly BookingBLL bookingBLL = new BookingBLL();

        private BEBuque buqueSeleccionado;
        private BEManifiesto manifiestoSeleccionado;
        private string columnaOrdenContenedor = "Codigo";
        private bool ordenContenedorAscendente = true;
        private string columnaOrdenManifiesto = "Numero";
        private bool ordenManifiestoAscendente = true;

        #endregion

        #region "Constructor"

        public FrmManifiesto()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos"

        private void FrmManifiesto_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrillas();
                RefrescarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar manifiestos: " + ObtenerMensajeRaiz(ex),
                                "Manifiesto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBuques_SelectedIndexChanged(object sender, EventArgs e)
        {
            buqueSeleccionado = cmbBuques.SelectedItem as BEBuque;
            RefrescarContenedoresDelBuque();
            CargarDestinosDisponibles();
        }

        private void dgvManifiestos_SelectionChanged(object sender, EventArgs e)
        {
            manifiestoSeleccionado = dgvManifiestos.CurrentRow != null
                ? dgvManifiestos.CurrentRow.DataBoundItem as BEManifiesto
                : null;

            CargarManifiestoSeleccionado();
        }

        private void dgvContenedores_SelectionChanged(object sender, EventArgs e)
        {
            CargarDetalleSeleccion();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (buqueSeleccionado == null)
                    throw new Exception("Debe seleccionar un buque.");

                var contenedores = ObtenerContenedoresSeleccionados();
                if (contenedores.Count == 0)
                    throw new Exception("Debe seleccionar al menos un contenedor.");

                BEManifiesto manifiesto = new BEManifiesto
                {
                    Buque = buqueSeleccionado,
                    FechaEmision = dtpFechaEmision.Value.Date,
                    AutoridadDestino = txtAutoridadDestino.Text.Trim(),
                    Contenedores = contenedores,
                    PesoTotal = contenedores.Sum(c => c.Cargas != null ? c.Cargas.Sum(ca => ca.PesoKg) : 0m)
                };

                manifiestoBLL.Guardar(manifiesto);

                MessageBox.Show("Manifiesto generado correctamente.",
                                "Manifiesto", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefrescarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ObtenerMensajeRaiz(ex),
                                "Manifiesto", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (manifiestoSeleccionado == null)
                {
                    MessageBox.Show("Debe seleccionar un manifiesto generado.",
                                    "Manifiesto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                BEManifiesto manifiesto = manifiestoBLL.ListarObjeto(new BEManifiesto { Id = manifiestoSeleccionado.Id });
                ImprimirManifiesto(manifiesto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir manifiesto: " + ObtenerMensajeRaiz(ex),
                                "Manifiesto", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ConfigurarGrillaBase(dgvContenedores);
            dgvContenedores.MultiSelect = true;
            dgvContenedores.Columns.Add("Codigo", "Contenedor");
            dgvContenedores.Columns["Codigo"].DataPropertyName = "Codigo";
            dgvContenedores.Columns.Add("Tipo", "Tipo");
            dgvContenedores.Columns.Add("Peso", "Peso kg");
            dgvContenedores.Columns.Add("Cargas", "Cargas");
            dgvContenedores.Columns.Add("Bookings", "Bookings");
            dgvContenedores.Columns.Add("Clientes", "Clientes");
            dgvContenedores.Columns.Add("Destinos", "Destinos");
            foreach (DataGridViewColumn columna in dgvContenedores.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            dgvContenedores.ColumnHeaderMouseClick -= dgvContenedores_ColumnHeaderMouseClick;
            dgvContenedores.ColumnHeaderMouseClick += dgvContenedores_ColumnHeaderMouseClick;

            dgvContenedores.CellFormatting += (s, e) =>
            {
                var c = dgvContenedores.Rows[e.RowIndex].DataBoundItem as BEContenedor;
                if (c == null) return;

                switch (dgvContenedores.Columns[e.ColumnIndex].Name)
                {
                    case "Tipo":
                        e.Value = c.Tipo != null ? c.Tipo.Codigo : "-";
                        break;
                    case "Peso":
                        e.Value = (c.Cargas != null ? c.Cargas.Sum(ca => ca.PesoKg) : 0m).ToString("N2");
                        break;
                    case "Cargas":
                        e.Value = c.Cargas != null ? c.Cargas.Count.ToString() : "0";
                        break;
                    case "Bookings":
                        e.Value = ObtenerBookings(c);
                        break;
                    case "Clientes":
                        e.Value = ObtenerClientes(c);
                        break;
                    case "Destinos":
                        e.Value = ObtenerDestinos(c);
                        break;
                }
            };

            ConfigurarGrillaBase(dgvManifiestos);
            dgvManifiestos.Columns.Add("Numero", "Numero");
            dgvManifiestos.Columns["Numero"].DataPropertyName = "Numero";
            dgvManifiestos.Columns.Add("Buque", "Buque");
            dgvManifiestos.Columns.Add("FechaEmision", "Fecha");
            dgvManifiestos.Columns["FechaEmision"].DataPropertyName = "FechaEmision";
            dgvManifiestos.Columns["FechaEmision"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvManifiestos.Columns.Add("AutoridadDestino", "Autoridad / destino");
            dgvManifiestos.Columns["AutoridadDestino"].DataPropertyName = "AutoridadDestino";
            dgvManifiestos.Columns.Add("PesoTotal", "Peso total");
            dgvManifiestos.Columns["PesoTotal"].DataPropertyName = "PesoTotal";
            dgvManifiestos.CellFormatting += (s, e) =>
            {
                var m = dgvManifiestos.Rows[e.RowIndex].DataBoundItem as BEManifiesto;
                if (m == null) return;

                if (dgvManifiestos.Columns[e.ColumnIndex].Name == "Buque")
                    e.Value = m.Buque != null ? m.Buque.Nombre : "-";
            };

            foreach (DataGridViewColumn columna in dgvManifiestos.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            dgvManifiestos.ColumnHeaderMouseClick -= dgvManifiestos_ColumnHeaderMouseClick;
            dgvManifiestos.ColumnHeaderMouseClick += dgvManifiestos_ColumnHeaderMouseClick;
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

        private void dgvContenedores_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = dgvContenedores.Columns[e.ColumnIndex].Name;

            if (columnaOrdenContenedor == columna)
                ordenContenedorAscendente = !ordenContenedorAscendente;
            else
            {
                columnaOrdenContenedor = columna;
                ordenContenedorAscendente = true;
            }

            RefrescarContenedoresDelBuque();
        }

        private void dgvManifiestos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = dgvManifiestos.Columns[e.ColumnIndex].Name;

            if (columnaOrdenManifiesto == columna)
                ordenManifiestoAscendente = !ordenManifiestoAscendente;
            else
            {
                columnaOrdenManifiesto = columna;
                ordenManifiestoAscendente = true;
            }

            RefrescarManifiestos();
        }

        private List<BEContenedor> OrdenarContenedores(List<BEContenedor> contenedores)
        {
            Func<BEContenedor, object> criterio;

            switch (columnaOrdenContenedor)
            {
                case "Tipo":
                    criterio = c => c.Tipo != null ? c.Tipo.Codigo : string.Empty;
                    break;
                case "Peso":
                    criterio = c => c.Cargas != null ? c.Cargas.Sum(ca => ca.PesoKg) : 0m;
                    break;
                case "Cargas":
                    criterio = c => c.Cargas != null ? c.Cargas.Count : 0;
                    break;
                case "Bookings":
                    criterio = c => ObtenerBookings(c);
                    break;
                case "Clientes":
                    criterio = c => ObtenerClientes(c);
                    break;
                case "Destinos":
                    criterio = c => ObtenerDestinos(c);
                    break;
                default:
                    criterio = c => c.Codigo ?? string.Empty;
                    break;
            }

            return ordenContenedorAscendente
                ? contenedores.OrderBy(criterio).ToList()
                : contenedores.OrderByDescending(criterio).ToList();
        }

        private List<BEManifiesto> OrdenarManifiestos(List<BEManifiesto> manifiestos)
        {
            Func<BEManifiesto, object> criterio;

            switch (columnaOrdenManifiesto)
            {
                case "Buque":
                    criterio = m => m.Buque != null ? m.Buque.Nombre : string.Empty;
                    break;
                case "FechaEmision":
                    criterio = m => m.FechaEmision;
                    break;
                case "AutoridadDestino":
                    criterio = m => m.AutoridadDestino ?? string.Empty;
                    break;
                case "PesoTotal":
                    criterio = m => m.PesoTotal;
                    break;
                default:
                    criterio = m => m.Numero ?? string.Empty;
                    break;
            }

            return ordenManifiestoAscendente
                ? manifiestos.OrderBy(criterio).ToList()
                : manifiestos.OrderByDescending(criterio).ToList();
        }

        #endregion

        #region "Carga y armado"

        private void RefrescarTodo()
        {
            cmbBuques.SelectedIndexChanged -= cmbBuques_SelectedIndexChanged;
            cmbBuques.DataSource = null;
            cmbBuques.DisplayMember = "Nombre";
            cmbBuques.DataSource = buqueBLL.ListarTodo()
                .Where(b => b.Estado != null && b.Estado.Codigo == EstadoBuque.Disponible.Codigo)
                .ToList();
            cmbBuques.SelectedIndexChanged += cmbBuques_SelectedIndexChanged;

            buqueSeleccionado = cmbBuques.SelectedItem as BEBuque;
            txtAutoridadDestino.Clear();
            rtbDetalle.Clear();
            rtbDetalleGenerado.Clear();

            RefrescarContenedoresDelBuque();
            RefrescarManifiestos();
        }

        private void RefrescarContenedoresDelBuque()
        {
            dgvContenedores.DataSource = null;

            if (buqueSeleccionado == null)
                return;

            var contenedoresManifestados = manifiestoBLL.ListarTodo()
                .SelectMany(m => m.Contenedores ?? new List<BEContenedor>())
                .Select(c => c.Id)
                .Distinct()
                .ToList();

            var contenedores = contenedorBLL.ListarTodo()
                .Where(c => c.Buque != null &&
                            c.Buque.Id == buqueSeleccionado.Id &&
                            c.Cargas != null &&
                            c.Cargas.Count > 0 &&
                            !contenedoresManifestados.Contains(c.Id))
                .ToList();
            dgvContenedores.DataSource = OrdenarContenedores(contenedores);

            dgvContenedores.ClearSelection();
            CargarDestinosDisponibles();
            CargarDetalleSeleccion();
        }

        private void btnSeleccionarDestino_Click(object sender, EventArgs e)
        {
            if (cmbDestinos.SelectedItem == null)
                return;

            string destinoSeleccionado = cmbDestinos.SelectedItem.ToString();

            foreach (DataGridViewRow row in dgvContenedores.Rows)
            {
                var contenedor = row.DataBoundItem as BEContenedor;
                row.Selected = contenedor != null && ObtenerDestinos(contenedor) == destinoSeleccionado;
            }

            txtAutoridadDestino.Text = destinoSeleccionado;
            CargarDetalleSeleccion();
        }

        private void CargarDestinosDisponibles()
        {
            if (cmbDestinos == null || dgvContenedores == null)
                return;

            var destinos = new List<string>();

            foreach (DataGridViewRow row in dgvContenedores.Rows)
            {
                var contenedor = row.DataBoundItem as BEContenedor;
                if (contenedor == null)
                    continue;

                string destino = ObtenerDestinos(contenedor);
                if (!string.IsNullOrWhiteSpace(destino) && destino != "-" && !destinos.Contains(destino))
                    destinos.Add(destino);
            }

            destinos.Sort();

            cmbDestinos.SelectedIndexChanged -= cmbDestinos_SelectedIndexChanged;
            cmbDestinos.DataSource = null;
            cmbDestinos.DataSource = destinos;
            cmbDestinos.SelectedIndex = destinos.Count > 0 ? 0 : -1;
            cmbDestinos.SelectedIndexChanged += cmbDestinos_SelectedIndexChanged;
        }

        private void cmbDestinos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDestinos.SelectedItem != null && string.IsNullOrWhiteSpace(txtAutoridadDestino.Text))
                txtAutoridadDestino.Text = cmbDestinos.SelectedItem.ToString();
        }

        private void RefrescarManifiestos()
        {
            dgvManifiestos.DataSource = null;
            dgvManifiestos.DataSource = OrdenarManifiestos(manifiestoBLL.ListarTodo());
            dgvManifiestos.ClearSelection();
            manifiestoSeleccionado = null;
        }

        private List<BEContenedor> ObtenerContenedoresSeleccionados()
        {
            var lista = new List<BEContenedor>();

            foreach (DataGridViewRow row in dgvContenedores.SelectedRows)
            {
                var contenedor = row.DataBoundItem as BEContenedor;
                if (contenedor != null)
                    lista.Add(contenedor);
            }

            return lista;
        }

        private void CargarManifiestoSeleccionado()
        {
            rtbDetalleGenerado.Clear();

            if (manifiestoSeleccionado == null)
                return;

            rtbDetalleGenerado.Text = ObtenerContenidoManifiesto(manifiestoSeleccionado);
        }

        private void CargarDetalleSeleccion()
        {
            var seleccionados = ObtenerContenedoresSeleccionados();

            if (seleccionados.Count == 0)
            {
                rtbDetalle.Text =
                    "Seleccione uno o mas contenedores del mismo buque.\n" +
                    "Para esta version, no se mezclan destinos distintos dentro del mismo manifiesto.\n" +
                    "Los contenedores sin carga no se muestran porque no corresponden al manifiesto de carga.";
                return;
            }

            var destinos = seleccionados
                .SelectMany(c => c.Cargas ?? new List<BECarga>())
                .Where(c => c.Destino != null)
                .Select(c => c.Destino.ToString())
                .Where(d => !string.IsNullOrWhiteSpace(d))
                .Distinct()
                .ToList();

            if (destinos.Count == 1 && string.IsNullOrWhiteSpace(txtAutoridadDestino.Text))
                txtAutoridadDestino.Text = destinos[0];

            decimal pesoTotal = seleccionados.Sum(c => c.Cargas != null ? c.Cargas.Sum(ca => ca.PesoKg) : 0m);
            int cantidadCargas = seleccionados.Sum(c => c.Cargas != null ? c.Cargas.Count : 0);

            rtbDetalle.Text =
                "--- Seleccion actual ---\n" +
                "Buque: " + (buqueSeleccionado != null ? buqueSeleccionado.Nombre : "-") + "\n" +
                "Contenedores seleccionados: " + seleccionados.Count + "\n" +
                "Cargas incluidas: " + cantidadCargas + "\n" +
                "Peso total: " + pesoTotal.ToString("N2") + " kg\n" +
                "Destinos detectados: " + (destinos.Count > 0 ? string.Join(" | ", destinos) : "-") + "\n\n" +
                (destinos.Count > 1
                    ? "Atencion: hay destinos distintos. Genere manifiestos separados por destino/autoridad.\n\n"
                    : "") +
                ObtenerDetalleContenedores(seleccionados);
        }

        #endregion

        #region "PDF y textos"

        private void ImprimirManifiesto(BEManifiesto manifiesto)
        {
            string numeroDocumento = !string.IsNullOrWhiteSpace(manifiesto.Numero)
                ? manifiesto.Numero
                : "MAN-" + manifiesto.Id.ToString("00000");

            var rutas = new RutasDocumentos();
            var gestor = new Transversales.Docu.GestorDocumentos(rutas);
            var generador = new GeneradorPdfBasico<BEManifiesto>(
                m => "Manifiesto de carga",
                m => ObtenerContenidoManifiesto(m),
                null,
                null,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "FacturaEncabezadoFondo.png"),
                m => "MANIFIESTO DE CARGA\n" +
                     "Nro. " + numeroDocumento + "\n" +
                     "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")
            );

            string rutaPdf = gestor.ObtenerOGenerarPdf(
                DocumentoTipo.Manifiesto,
                numeroDocumento,
                ruta => generador.GenerarManifiesto(ruta, manifiesto)
            );

            System.Diagnostics.Process.Start(rutaPdf);
        }

        private string ObtenerContenidoManifiesto(BEManifiesto manifiesto)
        {
            string contenido =
                "--- Datos principales ---\n" +
                "Numero: " + manifiesto.Numero + "\n" +
                "Fecha emision: " + manifiesto.FechaEmision.ToString("dd/MM/yyyy") + "\n" +
                "Buque: " + (manifiesto.Buque != null ? manifiesto.Buque.Nombre : "-") + "\n" +
                "Autoridad destino: " + manifiesto.AutoridadDestino + "\n" +
                "Total contenedores: " + (manifiesto.Contenedores != null ? manifiesto.Contenedores.Count.ToString() : "0") + "\n" +
                "Total: " + manifiesto.PesoTotal.ToString("N2") + " kg\n" +
                "--- Contenedores ---\n";

            if (manifiesto.Contenedores != null)
            {
                foreach (var c in manifiesto.Contenedores)
                {
                    decimal peso = c.Cargas != null ? c.Cargas.Sum(ca => ca.PesoKg) : 0m;
                    contenido += "- " + c.Codigo + ": " + (c.Tipo != null ? c.Tipo.Codigo : "-") + " / " + peso.ToString("N2") + " kg\n";
                }
            }

            return contenido;
        }

        private string ObtenerDetalleContenedores(List<BEContenedor> contenedores)
        {
            string contenido = "--- Contenedores seleccionados ---\n";

            foreach (var contenedor in contenedores)
            {
                decimal peso = contenedor.Cargas != null ? contenedor.Cargas.Sum(c => c.PesoKg) : 0m;
                contenido +=
                    "- " + contenedor.Codigo +
                    " | Tipo: " + (contenedor.Tipo != null ? contenedor.Tipo.Codigo : "-") +
                    " | Booking: " + ObtenerBookings(contenedor) +
                    " | Cliente: " + ObtenerClientes(contenedor) +
                    " | Destino: " + ObtenerDestinos(contenedor) +
                    " | Peso: " + peso.ToString("N2") + " kg\n";
            }

            return contenido;
        }

        private string ObtenerBookings(BEContenedor contenedor)
        {
            var bookings = ObtenerBookingsDelContenedor(contenedor)
                .Select(b => b.Numero)
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct()
                .ToList();

            return bookings.Count > 0 ? string.Join(" / ", bookings) : "-";
        }

        private string ObtenerClientes(BEContenedor contenedor)
        {
            var clientes = ObtenerBookingsDelContenedor(contenedor)
                .Where(b => b.Cliente != null)
                .Select(b => b.Cliente.RazonSocial)
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct()
                .ToList();

            return clientes.Count > 0 ? string.Join(" / ", clientes) : "-";
        }

        private string ObtenerDestinos(BEContenedor contenedor)
        {
            return ObtenerTextoCargas(contenedor, c => c.Destino != null ? c.Destino.ToString() : "-");
        }

        private string ObtenerTextoCargas(BEContenedor contenedor, Func<BECarga, string> selector)
        {
            if (contenedor == null || contenedor.Cargas == null || contenedor.Cargas.Count == 0)
                return "-";

            var valores = contenedor.Cargas
                .Select(selector)
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct()
                .ToList();

            return valores.Count > 0 ? string.Join(" / ", valores) : "-";
        }

        private List<BEBooking> ObtenerBookingsDelContenedor(BEContenedor contenedor)
        {
            if (contenedor == null || contenedor.Cargas == null || contenedor.Cargas.Count == 0)
                return new List<BEBooking>();

            var cargaIds = contenedor.Cargas.Select(c => c.Id).ToList();

            return bookingBLL.ListarTodo()
                .Where(b => b.Cargas != null &&
                            b.Cargas.Any(c => cargaIds.Contains(c.Id)))
                .ToList();
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
