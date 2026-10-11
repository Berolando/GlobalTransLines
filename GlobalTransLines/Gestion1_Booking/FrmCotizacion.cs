using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BE.Personas;
using BE.ServiciosSubcontratados;
using BE.Transporte;
using BE.Finanzas;
using BE.Dominio;
using BLL.Finanzas;
using BLL.Transporte;
using BLL.ServiciosSubcontratados;
using GlobalTransLines.UI.Gestion4_Proveedores;
using BLL.Infra;

namespace GlobalTransLines.UI.Gestion1_Booking
{
    public partial class FrmCotizacion : Form
    {
        #region "Atributos y Constructor"

        private readonly CotizacionBLL gestorCotizacion = new CotizacionBLL();
        private readonly ServicioSubcontratadoBLL gestorServicios = new ServicioSubcontratadoBLL();

        private BECliente clienteSeleccionado;
        private List<BECarga> cargasSeleccionadas = new List<BECarga>();
        private List<BEServicioSubcontratado> serviciosSeleccionados = new List<BEServicioSubcontratado>();
        private BECotizacion cotizacionActual;
        private bool modoEdicion = false;
        private SeguridadBLL seguridadBLL;
        private bool esUsuarioAprobador;

        public FrmCotizacion() : this(null, null) { }

        public FrmCotizacion(SeguridadBLL seguridad) : this(null, seguridad) { }

        public FrmCotizacion(BECotizacion cotizacion) : this(cotizacion, null) { }

        public FrmCotizacion(BECotizacion cotizacion, SeguridadBLL seguridad)
        {
            InitializeComponent();
            cotizacionActual = cotizacion ?? new BECotizacion();
            modoEdicion = (cotizacion != null);

            seguridadBLL = seguridad ?? new SeguridadBLL();
        }


        #endregion

        #region "Eventos del formulario"

        private void FrmCotizacion_Load(object sender, EventArgs e)
        {
            try
            {
                InicializarFormulario();
                ConfigurarCombos();
                esUsuarioAprobador = EsUsuarioAprobador();
                ConfigurarModoPorRolEnEdicion();

                ConfigurarRichTextBox();
                ActualizarPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el formulario: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Inicialización y Configuración Visual"

        private void InicializarFormulario()
        {
            textBox1.Text = gestorCotizacion.ObtenerProximoNumeroPreview();
            lblFechaE.Text = (cotizacionActual.FechaEmision == DateTime.MinValue)
                ? DateTime.Now.ToShortDateString()
                : cotizacionActual.FechaEmision.ToShortDateString();

            dtpVencimiento.Value = (cotizacionActual.FechaVencimiento == DateTime.MinValue)
                ? DateTime.Now.AddDays(7)
                : cotizacionActual.FechaVencimiento;

            if (modoEdicion)
            {
                clienteSeleccionado = cotizacionActual.Cliente;
                cargasSeleccionadas = cotizacionActual.Cargas ?? new List<BECarga>();
                serviciosSeleccionados = cotizacionActual.Servicios ?? new List<BEServicioSubcontratado>();
                lblMonto.Text = cotizacionActual.MontoTotal.ToString("C");
            }

            btnAceptarCliente.Enabled = false;
            lblCompleja.Visible = false;
            btnGenerarBooking.Enabled = false;
        }

        private void ConfigurarCombos()
        {
            cmbEstado.DisplayMember = "Descripcion";
            cmbEstado.ValueMember = "Codigo";
            cmbEstado.DataSource = new List<EstadoCotizacion>(EstadoCotizacion.Listar());
            cmbEstado.SelectedItem = EstadoCotizacion.Pendiente;
            cotizacionActual.Estado = EstadoCotizacion.Pendiente;
        }

        private void ConfigurarRichTextBox()
        {
            rtbPreview.Font = new System.Drawing.Font("Consolas", 11);
            rtbPreview.BackColor = System.Drawing.Color.White;
            rtbPreview.ReadOnly = true;
            rtbPreview.BorderStyle = BorderStyle.FixedSingle;
        }

        #endregion

        #region "Eventos de botones"

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            var frm = new FrmCliente { ModoSeleccion = true };
            if (frm.ShowDialog() == DialogResult.OK)
            {
                clienteSeleccionado = frm.clienteSeleccionado;
                cotizacionActual.Cliente = clienteSeleccionado;
                ActualizarPreview();
            }
        }

        private void btnAgregarCarga_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new FrmCarga
                {
                    ModoSeleccion = true,
                    WindowState = FormWindowState.Maximized,  // 👈 se abre maximizado
                    StartPosition = FormStartPosition.CenterScreen
                };
                if (frm.ShowDialog() == DialogResult.OK && frm.cargaSeleccionada != null)
                {
                    var bll = new CotizacionBLL();

                    if (!bll.PuedeAgregarCarga(frm.cargaSeleccionada))
                    {
                        MessageBox.Show("Esta carga ya pertenece a una cotización aceptada y no puede volver a utilizarse.",
                            "Validación de negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    
                    cargasSeleccionadas.Add(frm.cargaSeleccionada);
                    cotizacionActual.Cargas = cargasSeleccionadas;
                    ActualizarPreview();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar carga: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarServicio_Click(object sender, EventArgs e)
        {
            var frm = new FrmServicioSubcontratado { ModoSeleccion = true };
            if (frm.ShowDialog() == DialogResult.OK && frm.servicioSeleccionado != null)
            {
                serviciosSeleccionados.Add(frm.servicioSeleccionado);
                cotizacionActual.Servicios = serviciosSeleccionados;
                ActualizarPreview();
            }
        }

        private void btnCalcularTotal_Click(object sender, EventArgs e)
        {
            try
            {
                decimal total = gestorCotizacion.CalcularMontoTotal(cotizacionActual);
                cotizacionActual.MontoTotal = total;
                lblMonto.Text = total.ToString("C");
                lblCalc.Text = "Calculado";

                bool compleja = gestorCotizacion.EsCompleja(cotizacionActual);
                btnAceptarCliente.Enabled = !compleja;
                lblCompleja.Visible = compleja;

                if (compleja)
                {
                    cmbEstado.SelectedItem = EstadoCotizacion.PendienteAprobacion;
                    MessageBox.Show(
                        "La cotización se ha marcado como COMPLEJA debido a las características de las cargas o servicios seleccionados. " +
                        "Será necesario un análisis adicional por parte del jefe de ventas.",
                        "Cotización Compleja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    cmbEstado.SelectedItem = EstadoCotizacion.Pendiente;
                    MessageBox.Show("El total de la cotización se ha calculado correctamente.",
                        "Cálculo Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ActualizarPreview();
            }
            catch (Exception ex)
            {
                // Recorre excepciones internas
                Exception actual = ex;
                string detalle = "";
                while (actual != null)
                {
                    detalle += $" {actual.Message}\n";
                    actual = actual.InnerException;
                }

                MessageBox.Show("DEBUG (detalle interno):\n" + detalle,
                    "Detalle de excepción", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show("Error al calcular total: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAceptarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                if (cotizacionActual == null)
                    throw new Exception("No hay una cotización seleccionada.");

                // Validaciones mínimas (evitan aceptar “en el aire”)
                if (clienteSeleccionado == null)
                    throw new Exception("Debe seleccionar un cliente antes de confirmar.");

                if (cargasSeleccionadas == null || cargasSeleccionadas.Count == 0)
                    throw new Exception("Debe agregar al menos una carga antes de confirmar.");

                if (cotizacionActual.MontoTotal <= 0)
                    throw new Exception("Debe calcular el total antes de confirmar.");

                // Sincronizar objeto (por si el usuario tocó cosas y no guardó)
                cotizacionActual.Cliente = clienteSeleccionado;
                cotizacionActual.Cargas = cargasSeleccionadas;
                cotizacionActual.Servicios = serviciosSeleccionados;
                cotizacionActual.FechaEmision = (cotizacionActual.FechaEmision == DateTime.MinValue)
                    ? DateTime.Now
                    : cotizacionActual.FechaEmision;
                cotizacionActual.FechaVencimiento = dtpVencimiento.Value;

                bool esCompleja = gestorCotizacion.EsCompleja(cotizacionActual);

                // Asegurar persistencia: si es nueva, primero Guardar en estado base
                if (cotizacionActual.Id == 0)
                {
                    // El asesor NO define estados finales al guardar
                    cotizacionActual.Estado = esCompleja
                        ? EstadoCotizacion.PendienteAprobacion
                        : EstadoCotizacion.Pendiente;

                    gestorCotizacion.Guardar(cotizacionActual);
                }

                //  Compleja: solo mandar a aprobación (no aceptar)
                if (esCompleja)
                {
                    // si ya estaba PendienteAprobacion, no hace falta tocar nada, pero es ok dejarlo idempotente
                    gestorCotizacion.CambiarEstado(cotizacionActual, EstadoCotizacion.PendienteAprobacion);
                    cmbEstado.SelectedItem = EstadoCotizacion.PendienteAprobacion;

                    btnAceptarCliente.Enabled = false;
                    btnGenerarBooking.Enabled = false;

                    MessageBox.Show("Cotización compleja enviada a aprobación del Jefe de Ventas.",
                        "Aprobación requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ActualizarPreview();
                    return;
                }

                //  Simple: aceptar
                gestorCotizacion.CambiarEstado(cotizacionActual, EstadoCotizacion.Aceptada);
                cmbEstado.SelectedItem = EstadoCotizacion.Aceptada;

                btnAceptarCliente.Enabled = false;
                btnGenerarBooking.Enabled = true;

                MessageBox.Show("Cotización marcada como aceptada por el asesor comercial.",
                    "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ActualizarPreview();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al aceptar cotización: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                cotizacionActual.FechaEmision = DateTime.Now;
                cotizacionActual.FechaVencimiento = dtpVencimiento.Value;
                cotizacionActual.Cliente = clienteSeleccionado;
                cotizacionActual.Cargas = cargasSeleccionadas;
                cotizacionActual.Servicios = serviciosSeleccionados;
                bool esCompleja = gestorCotizacion.EsCompleja(cotizacionActual);

                // Estado lo define el sistema (asesor o aprobador)
                if (!esUsuarioAprobador)
                {
                    // Asesor: NO puede guardar Aceptada desde el combo.
                    // Si es compleja: PendienteAprobacion. Si no: Pendiente (hasta aceptar).
                    cotizacionActual.Estado = esCompleja ? EstadoCotizacion.PendienteAprobacion : EstadoCotizacion.Pendiente;

                    // Sincronizar UI para que no mienta el preview
                    cmbEstado.SelectedItem = cotizacionActual.Estado;
                }
                else
                {
                    // Aprobador: puede decidir estados desde combo (pero igual validamos)
                    cotizacionActual.Estado = (EstadoCotizacion)cmbEstado.SelectedItem ?? EstadoCotizacion.Pendiente;

                    // Si es compleja, jamás debería quedar "Pendiente" si el jefe está actuando:
                    // (opcional) lo podés forzar a PendienteAprobacion o dejarlo libre.
                }


                if (cotizacionActual.MontoTotal <= 0)
                {
                    MessageBox.Show("La cotización debe tener un monto válido.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gestorCotizacion.Guardar(cotizacionActual);

                MessageBox.Show("Cotización guardada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                Exception raiz = ex;
                while (raiz.InnerException != null)
                    raiz = raiz.InnerException;

                MessageBox.Show("DEBUG root: " + raiz.Message, "Debug interno");
                MessageBox.Show("Error al guardar cotización: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGenerarBooking_Click(object sender, EventArgs e)
        {
            try
            {
                if (cotizacionActual.Estado != EstadoCotizacion.Aceptada)
                {
                    MessageBox.Show("Solo pueden generarse bookings desde cotizaciones aceptadas.",
                        "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var frm = new FrmBooking(cotizacionActual, seguridadBLL);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar booking: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Vista previa"

        private void ActualizarPreview()
        {
            rtbPreview.Clear();
            rtbPreview.AppendText("=== COTIZACIÓN DE SERVICIO ===\n\n");

            if (clienteSeleccionado != null)
            {
                rtbPreview.AppendText($"Cliente: {clienteSeleccionado.RazonSocial}\n");
                rtbPreview.AppendText($"CUIT: {clienteSeleccionado.Cuit}\n\n");
            }

            if (cargasSeleccionadas.Count > 0)
            {
                rtbPreview.AppendText("Cargas:\n");
                foreach (var c in cargasSeleccionadas)
                    rtbPreview.AppendText($"- {c.Tipo} ({c.Peso} kg / {c.Volumen} m³)\n");
                rtbPreview.AppendText("\n");
            }

            if (serviciosSeleccionados.Count > 0)
            {
                rtbPreview.AppendText("Servicios seleccionados:\n");
                foreach (var s in serviciosSeleccionados)
                    rtbPreview.AppendText($"- {s.Nombre} (${s.Costo})\n");
                rtbPreview.AppendText("\n");
            }

            if (cotizacionActual.MontoTotal > 0)
                rtbPreview.AppendText($"TOTAL ESTIMADO: {cotizacionActual.MontoTotal:C}\n");

            rtbPreview.AppendText("\nFecha emisión: " + lblFechaE.Text);
            rtbPreview.AppendText("\nVencimiento: " + dtpVencimiento.Value.ToShortDateString());

            var estado = cmbEstado.SelectedItem as EstadoCotizacion;
            string estadoDesc = estado != null ? estado.Descripcion : "(sin estado)";
            rtbPreview.AppendText("\nEstado: " + estadoDesc);
            rtbPreview.AppendText("\n===============================");
        }

        #endregion

        #region "Lógica por rol de usuario"
        private bool EsUsuarioAprobador()
        {
            if (seguridadBLL == null) return false;

            var usuario = seguridadBLL.UsuarioActual;
            if (usuario == null || usuario.Roles == null) return false;

            return usuario.Roles.Any(r =>
                r != null &&
                (r.Nombre == "Jefe de Ventas y Cotizaciones" || r.Nombre == "Administrador"));
        }

        private void ConfigurarModoPorRolEnEdicion()
        {
            // El asesor NO decide estados desde combo
            if (!esUsuarioAprobador)
            {
                cmbEstado.Enabled = false;
                // si querés, también:
                // cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }
        #endregion

    }
}
