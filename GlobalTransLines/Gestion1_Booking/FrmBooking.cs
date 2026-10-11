using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BE.Transporte;
using BE.Infra;
using BE.Dominio;
using BE.Finanzas;
using BE.ServiciosSubcontratados;
using BLL.Transporte;
using System.Collections.Generic;
using BLL.Finanzas;
using BLL.Infra;

namespace GlobalTransLines.UI.Gestion1_Booking
{
    public partial class FrmBooking : Form
    {
        #region "Atributos y Constructor"

        private BookingBLL gestorBooking = new BookingBLL();
        private BEUsuario usuarioActual;
        private BEBooking bookingActual;
        private BECotizacion cotizacionSeleccionada;
        public bool ModoSeleccion { get; set; }
        private string columnaOrdenCargas = "Descripcion";
        private bool ordenCargasAscendente = true;
        private string columnaOrdenServicios = "Nombre";
        private bool ordenServiciosAscendente = true;
        private string columnaOrdenBookings = "FechaGeneracion";
        private bool ordenBookingsAscendente;

        public FrmBooking()
        {
            InitializeComponent();
           
        }

        public FrmBooking(SeguridadBLL seguridad) : this()
        {
            usuarioActual = seguridad?.UsuarioActual;
        }

        public FrmBooking(BECotizacion cotizacion) : this(cotizacion, null)
        {
        }

        public FrmBooking(BECotizacion cotizacion, SeguridadBLL seguridad) : this(seguridad)
        {
            try
            {
                if (cotizacion == null)
                    throw new ArgumentNullException(nameof(cotizacion));

                
                var gestor = new CotizacionBLL();
                var cotiReal = gestor.ListarTodo().FirstOrDefault(c => c.Id == cotizacion.Id);
                if (cotiReal != null)
                    cotizacion = cotiReal;

                CargarDesdeCotizacion(cotizacion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cotización: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region "Eventos del formulario"

        private void FrmBooking_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrillas();

               
                txtNumero.Text = gestorBooking.ObtenerProximoNumeroPreview();

                cmbEstado.DataSource = EstadoBooking.ObtenerTodos().ToList();
                cmbEstado.SelectedIndex = 0;
                cmbEstado.DisplayMember = "Descripcion";
                cmbEstado.ValueMember = "Codigo";

                // Configurar preview
                rtbPreview.Font = new Font("Consolas", 11);
                rtbPreview.BackColor = Color.White;
                rtbPreview.ReadOnly = true;
                rtbPreview.BorderStyle = BorderStyle.FixedSingle;

                // Solo inicia un booking vacío cuando el formulario no recibió una cotización.
                if (bookingActual == null)
                {
                    bookingActual = new BEBooking
                    {
                        FechaGeneracion = DateTime.Now,
                        Estado = EstadoBooking.Confirmado
                    };
                    dtpFechaTentativaSalida.Value = DateTime.Today.AddDays(15);
                }

                RefrescarGrillas();
                ActualizarPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar formulario de Booking: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnBuscarCoti_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmCotizaciones { ModoSeleccion = true };

                if (frm.ShowDialog() == DialogResult.OK && frm.cotizacionSeleccionada != null)
                {
                    cotizacionSeleccionada = frm.cotizacionSeleccionada;

                    if (cotizacionSeleccionada.Estado != null && cotizacionSeleccionada.Estado.Codigo == EstadoCotizacion.Aceptada.Codigo)
                        CargarDesdeCotizacion(cotizacionSeleccionada);
                    else
                        MessageBox.Show("Solo se pueden generar bookings desde cotizaciones aceptadas.",
                            "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar cotización: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (bookingActual == null)
                {
                    bookingActual = new BEBooking();
                }

                bookingActual.FechaTentativaSalida = dtpFechaTentativaSalida.Value;

                gestorBooking.Guardar(bookingActual, usuarioActual);

                MessageBox.Show("Booking guardado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                RefrescarGrillas();
                ActualizarPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al guardar booking:\n" + ex.Message +
                    "\n\nDetalle interno:\n" +
                    (ex.InnerException != null ? ex.InnerException.Message : "(sin detalle interno)"),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void bookingExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region "Configuración Visual"

        private void ConfigurarGrillas()
        {
            ConfigurarGrilla(grdCargas);
            ConfigurarGrilla(grdServicios);
            ConfigurarGrilla(grdBookings);

            grdCargas.ColumnHeaderMouseClick -= grdCargas_ColumnHeaderMouseClick;
            grdCargas.ColumnHeaderMouseClick += grdCargas_ColumnHeaderMouseClick;

            grdServicios.ColumnHeaderMouseClick -= grdServicios_ColumnHeaderMouseClick;
            grdServicios.ColumnHeaderMouseClick += grdServicios_ColumnHeaderMouseClick;

            grdBookings.ColumnHeaderMouseClick -= grdBookings_ColumnHeaderMouseClick;
            grdBookings.ColumnHeaderMouseClick += grdBookings_ColumnHeaderMouseClick;
        }

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

            foreach (DataGridViewColumn columna in grilla.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            grilla.RowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 250, 240);
        }

        #endregion

        #region "Eventos de grilla"

        private void grdCargas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = grdCargas.Columns[e.ColumnIndex].Name;

            // Orden manual: la grilla muestra una proyección de las cargas del booking actual.
            if (columnaOrdenCargas == columna)
                ordenCargasAscendente = !ordenCargasAscendente;
            else
            {
                columnaOrdenCargas = columna;
                ordenCargasAscendente = true;
            }

            RefrescarGrillas();
        }

        private void grdServicios_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = grdServicios.Columns[e.ColumnIndex].Name;

            // Orden manual: la grilla muestra una proyección de los servicios del booking actual.
            if (columnaOrdenServicios == columna)
                ordenServiciosAscendente = !ordenServiciosAscendente;
            else
            {
                columnaOrdenServicios = columna;
                ordenServiciosAscendente = true;
            }

            RefrescarGrillas();
        }

        private void grdBookings_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = grdBookings.Columns[e.ColumnIndex].Name;

            // Orden manual: la grilla usa datos reconstruidos desde XML y se vuelve a cargar con el criterio elegido.
            if (columnaOrdenBookings == columna)
                ordenBookingsAscendente = !ordenBookingsAscendente;
            else
            {
                columnaOrdenBookings = columna;
                ordenBookingsAscendente = true;
            }

            RefrescarGrillas();
        }

        #endregion

        #region "Métodos auxiliares"

        private void RefrescarGrillas()
        {
            if (bookingActual == null) return;

            grdCargas.DataSource = null;
            grdServicios.DataSource = null;

            if (bookingActual.Cargas != null)
            {
                grdCargas.DataSource = OrdenarCargas(bookingActual.Cargas).Select(c => new
                {
                    c.Descripcion,
                    c.Peso,
                    c.Volumen,
                    Origen = c.Origen?.Puerto ?? "-",
                    Destino = c.Destino?.Puerto ?? "-"
                }).ToList();
            }

            if (bookingActual.Servicios != null)
            {
                grdServicios.DataSource = OrdenarServicios(bookingActual.Servicios).Select(s => new
                {
                    s.Nombre,
                    Proveedor = s.Proveedor?.RazonSocial ?? "-",
                    Costo = s.Costo.ToString("C")
                }).ToList();
            }
            grdBookings.DataSource = null;

            var bookings = gestorBooking.ListarTodo(); // <-- XML puro, vía BLL/MPP

            if (bookings != null && bookings.Count > 0)
            {
                grdBookings.DataSource = OrdenarBookings(bookings)
                    .Select(b => new
                    {
                        b.Numero,
                        Cliente = b.Cliente?.RazonSocial ?? "-",
                        Estado = b.Estado?.Descripcion ?? "-",
                        b.FechaGeneracion,
                        b.FechaTentativaSalida,
                        Cargas = b.Cargas?.Count ?? 0,
                        Servicios = b.Servicios?.Count ?? 0
                    })
                    .ToList();
            }

            grdCargas.Refresh();
            grdServicios.Refresh();
            grdBookings.Refresh();
        }

        private List<BECarga> OrdenarCargas(List<BECarga> cargas)
        {
            Func<BECarga, object> criterio;

            switch (columnaOrdenCargas)
            {
                case "Peso":
                    criterio = c => c.Peso;
                    break;
                case "Volumen":
                    criterio = c => c.Volumen;
                    break;
                case "Origen":
                    criterio = c => c.Origen?.Puerto ?? string.Empty;
                    break;
                case "Destino":
                    criterio = c => c.Destino?.Puerto ?? string.Empty;
                    break;
                default:
                    criterio = c => c.Descripcion ?? string.Empty;
                    break;
            }

            return ordenCargasAscendente
                ? cargas.OrderBy(criterio).ToList()
                : cargas.OrderByDescending(criterio).ToList();
        }

        private List<BEServicioSubcontratado> OrdenarServicios(List<BEServicioSubcontratado> servicios)
        {
            Func<BEServicioSubcontratado, object> criterio;

            switch (columnaOrdenServicios)
            {
                case "Proveedor":
                    criterio = s => s.Proveedor?.RazonSocial ?? string.Empty;
                    break;
                case "Costo":
                    criterio = s => s.Costo;
                    break;
                default:
                    criterio = s => s.Nombre ?? string.Empty;
                    break;
            }

            return ordenServiciosAscendente
                ? servicios.OrderBy(criterio).ToList()
                : servicios.OrderByDescending(criterio).ToList();
        }

        private List<BEBooking> OrdenarBookings(List<BEBooking> bookings)
        {
            Func<BEBooking, object> criterio;

            switch (columnaOrdenBookings)
            {
                case "Numero":
                    criterio = b => b.Numero ?? string.Empty;
                    break;
                case "Cliente":
                    criterio = b => b.Cliente?.RazonSocial ?? string.Empty;
                    break;
                case "Estado":
                    criterio = b => b.Estado?.Descripcion ?? string.Empty;
                    break;
                case "FechaTentativaSalida":
                    criterio = b => b.FechaTentativaSalida;
                    break;
                case "Cargas":
                    criterio = b => b.Cargas?.Count ?? 0;
                    break;
                case "Servicios":
                    criterio = b => b.Servicios?.Count ?? 0;
                    break;
                default:
                    criterio = b => b.FechaGeneracion;
                    break;
            }

            return ordenBookingsAscendente
                ? bookings.OrderBy(criterio).ToList()
                : bookings.OrderByDescending(criterio).ToList();
        }

        private void CargarDesdeCotizacion(BECotizacion cotizacion)
        {
            try
            {
                if (cotizacion == null)
                    throw new ArgumentNullException(nameof(cotizacion));

                

                // Filtrar solo las cargas aceptadas en cotización
                // Aceptamos cargas "DIS" si la cotización está ACEPTADA
                var cargasValidas = cotizacion.Cargas?
                    .Where(c => c.Estado != null &&
                               (c.Estado.Codigo == EstadoCarga.AceptadaCotizacion.Codigo ||
                                (c.Estado.Codigo == EstadoCarga.Disponible.Codigo &&
                                 cotizacion.Estado.Codigo == EstadoCotizacion.Aceptada.Codigo)))
                    .ToList() ?? new List<BECarga>();

                // Normalizamos el estado para mantener coherencia en el flujo
                foreach (var c in cargasValidas)
                {
                    if (c.Estado.Codigo == EstadoCarga.Disponible.Codigo)
                        c.Estado = EstadoCarga.AceptadaCotizacion;
                }

                // Creamos el booking con las cargas ahora válidas
                bookingActual = new BEBooking
                {
                    Cliente = cotizacion.Cliente,
                    Cotizacion = cotizacion,
                    Cargas = cargasValidas,
                    Servicios = cotizacion.Servicios,
                    TipoContenedorCodigo = cargasValidas
                        .Select(c => c.TipoContenedor?.Codigo)
                        .FirstOrDefault(codigo => !string.IsNullOrWhiteSpace(codigo)),
                    FechaGeneracion = DateTime.Now,
                    Estado = EstadoBooking.Confirmado
                };


                

                dtpFechaTentativaSalida.Value = DateTime.Today.AddDays(1);

                txtRazonSocial.Text = cotizacion.Cliente?.RazonSocial ?? "-";
                txtCuit.Text = cotizacion.Cliente?.Cuit ?? "-";

                RefrescarGrillas();
                ActualizarPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos desde cotización: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ActualizarPreview()
        {
            if (bookingActual == null) return;

            string tipo = TipoContenedor.ObtenerTodos()
                            .FirstOrDefault(t => t.Codigo == bookingActual.TipoContenedorCodigo)?
                            .Descripcion ?? "-";

            var texto = new System.Text.StringBuilder();
            texto.AppendLine("GLOBAL TRANS LINES");
            texto.AppendLine("CONFIRMACIÓN DE BOOKING");
            texto.AppendLine(new string('-', 40));
            texto.AppendLine($"N°: {bookingActual.Numero}");
            texto.AppendLine($"Cliente: {bookingActual.Cliente?.RazonSocial ?? "-"}");
            texto.AppendLine($"CUIT: {bookingActual.Cliente?.Cuit ?? "-"}");
            texto.AppendLine($"Fecha: {DateTime.Now:dd/MM/yyyy}");
            texto.AppendLine($"Estado: {bookingActual.Estado?.Descripcion ?? "Pendiente"}");
            texto.AppendLine($"Tipo de contenedor: {tipo}");
            texto.AppendLine($"Salida estimada: {bookingActual.FechaTentativaSalida:dd/MM/yyyy}");
            texto.AppendLine(new string('-', 40));
            texto.AppendLine("CARGAS ASOCIADAS:");
            if (bookingActual.Cargas?.Count > 0)
            {
                foreach (var c in bookingActual.Cargas)
                    texto.AppendLine($"- {c.Descripcion} | {c.Peso:N0} kg | {c.Volumen:N1} m³ | {c.Origen?.Puerto ?? "?"} → {c.Destino?.Ciudad ?? "?"}");
            }
            else
                texto.AppendLine("  (sin cargas)");

            texto.AppendLine(new string('-', 40));
            texto.AppendLine("SERVICIOS SUBCONTRATADOS:");
            if (bookingActual.Servicios?.Count > 0)
            {
                foreach (var s in bookingActual.Servicios)
                    texto.AppendLine($"- {s.Nombre} | {s.Proveedor?.RazonSocial ?? "?"} | {s.Costo:C}");
            }
            else
                texto.AppendLine("  (sin servicios)");

            rtbPreview.Text = texto.ToString();
        }


        #endregion

       
    }
}
