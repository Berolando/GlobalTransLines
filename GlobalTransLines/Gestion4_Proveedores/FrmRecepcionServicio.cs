using BE.Finanzas;
using BE.ServiciosSubcontratados;
using BLL.Finanzas;
using BLL.ServiciosSubcontratados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Gestion4_Proveedores
{
    public partial class FrmRecepcionServicio : Form
    {
        #region "Atributos y Constructor"

        private OrdenCompraBLL ordenCompraBLL = new OrdenCompraBLL();
        private RecepcionServicioBLL recepcionBLL = new RecepcionServicioBLL();

        private List<BEOrdenDeCompra> ordenesEmitidas = new List<BEOrdenDeCompra>();
        private List<BERecepcionServicio> recepcionesRegistradas = new List<BERecepcionServicio>();
        private BEOrdenDeCompra ordenSeleccionada;
        private string columnaOrdenOrdenCompra = "Fecha";
        private bool ordenOrdenCompraAscendente = false;
        private string columnaOrdenRecepcion = "Fecha";
        private bool ordenRecepcionAscendente = false;

        public FrmRecepcionServicio()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmRecepcionServicio_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrillas();
                ConfigurarControlesIniciales();
                RefrescarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar recepción de servicios: " + ex.Message,
                                "Recepción de servicios",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnConfirmarRecepcion_Click(object sender, EventArgs e)
        {
            try
            {
                if (ordenSeleccionada == null)
                {
                    MessageBox.Show("Debe seleccionar una orden de compra emitida.",
                                    "Recepción de servicios",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                recepcionBLL.ConfirmarRecepcion(
                    ordenSeleccionada,
                    dtpFechaRecepcion.Value,
                    txtObservaciones.Text);

                MessageBox.Show("Recepción confirmada correctamente.",
                                "Recepción de servicios",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                RefrescarDatos();
                LimpiarSeleccion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al confirmar recepción: " + ex.Message,
                                "Recepción de servicios",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                RefrescarDatos();
                LimpiarSeleccion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar datos: " + ex.Message,
                                "Recepción de servicios",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarSeleccion();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region "Eventos de grillas"

        private void dgvOrdenesEmitidas_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrdenesEmitidas.CurrentRow == null)
                {
                    ordenSeleccionada = null;
                    txtDetalleOrden.Clear();
                    return;
                }

                long id = Convert.ToInt64(dgvOrdenesEmitidas.CurrentRow.Cells["Id"].Value);
                ordenSeleccionada = ordenesEmitidas.FirstOrDefault(x => x.Id == id);
                CargarOrdenSeleccionada();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar orden de compra: " + ex.Message,
                                "Recepción de servicios",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Configuración Visual"

        private void ConfigurarGrillas()
        {
            ConfigurarGrilla(dgvOrdenesEmitidas);
            ConfigurarGrilla(dgvRecepciones);

            dgvOrdenesEmitidas.SelectionChanged -= dgvOrdenesEmitidas_SelectionChanged;
            dgvOrdenesEmitidas.SelectionChanged += dgvOrdenesEmitidas_SelectionChanged;
            dgvOrdenesEmitidas.ColumnHeaderMouseClick -= dgvOrdenesEmitidas_ColumnHeaderMouseClick;
            dgvOrdenesEmitidas.ColumnHeaderMouseClick += dgvOrdenesEmitidas_ColumnHeaderMouseClick;

            dgvRecepciones.ColumnHeaderMouseClick -= dgvRecepciones_ColumnHeaderMouseClick;
            dgvRecepciones.ColumnHeaderMouseClick += dgvRecepciones_ColumnHeaderMouseClick;
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

        private void ConfigurarControlesIniciales()
        {
            dtpFechaRecepcion.Format = DateTimePickerFormat.Short;
            dtpFechaRecepcion.Value = DateTime.Now;
            txtDetalleOrden.ReadOnly = true;
        }

        #endregion

        #region "Orden de grillas"

        private void dgvOrdenesEmitidas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = dgvOrdenesEmitidas.Columns[e.ColumnIndex].Name;
            ordenOrdenCompraAscendente = columnaOrdenOrdenCompra == columna ? !ordenOrdenCompraAscendente : true;
            columnaOrdenOrdenCompra = columna;
            MostrarOrdenesEmitidas();
        }

        private void dgvRecepciones_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = dgvRecepciones.Columns[e.ColumnIndex].Name;
            ordenRecepcionAscendente = columnaOrdenRecepcion == columna ? !ordenRecepcionAscendente : true;
            columnaOrdenRecepcion = columna;
            MostrarRecepciones();
        }

        private List<BEOrdenDeCompra> OrdenarOrdenesEmitidas()
        {
            // Ordena las ordenes visibles sin cambiar el criterio de la BLL: solo emitidas.
            switch (columnaOrdenOrdenCompra)
            {
                case "Id":
                    return ordenOrdenCompraAscendente ? ordenesEmitidas.OrderBy(oc => oc.Id).ToList() : ordenesEmitidas.OrderByDescending(oc => oc.Id).ToList();
                case "Numero":
                    return ordenOrdenCompraAscendente ? ordenesEmitidas.OrderBy(oc => oc.Numero).ToList() : ordenesEmitidas.OrderByDescending(oc => oc.Numero).ToList();
                case "Proveedor":
                    return ordenOrdenCompraAscendente ? ordenesEmitidas.OrderBy(oc => oc.Proveedor?.RazonSocial).ToList() : ordenesEmitidas.OrderByDescending(oc => oc.Proveedor?.RazonSocial).ToList();
                case "Booking":
                    return ordenOrdenCompraAscendente ? ordenesEmitidas.OrderBy(oc => oc.BookingOrigen?.Numero).ToList() : ordenesEmitidas.OrderByDescending(oc => oc.BookingOrigen?.Numero).ToList();
                case "Total":
                    return ordenOrdenCompraAscendente ? ordenesEmitidas.OrderBy(oc => oc.ImporteTotal).ToList() : ordenesEmitidas.OrderByDescending(oc => oc.ImporteTotal).ToList();
                case "Estado":
                    return ordenOrdenCompraAscendente ? ordenesEmitidas.OrderBy(oc => oc.Estado?.Descripcion).ToList() : ordenesEmitidas.OrderByDescending(oc => oc.Estado?.Descripcion).ToList();
                default:
                    return ordenOrdenCompraAscendente ? ordenesEmitidas.OrderBy(oc => oc.FechaEmision).ToList() : ordenesEmitidas.OrderByDescending(oc => oc.FechaEmision).ToList();
            }
        }

        private List<BERecepcionServicio> OrdenarRecepciones()
        {
            // Ordena las recepciones registradas que ya fueron reconstruidas desde XML.
            switch (columnaOrdenRecepcion)
            {
                case "Id":
                    return ordenRecepcionAscendente ? recepcionesRegistradas.OrderBy(r => r.Id).ToList() : recepcionesRegistradas.OrderByDescending(r => r.Id).ToList();
                case "Orden":
                    return ordenRecepcionAscendente ? recepcionesRegistradas.OrderBy(r => r.OrdenDeCompra?.Numero).ToList() : recepcionesRegistradas.OrderByDescending(r => r.OrdenDeCompra?.Numero).ToList();
                case "Servicio":
                    return ordenRecepcionAscendente ? recepcionesRegistradas.OrderBy(r => r.Servicio?.Nombre).ToList() : recepcionesRegistradas.OrderByDescending(r => r.Servicio?.Nombre).ToList();
                case "Observaciones":
                    return ordenRecepcionAscendente ? recepcionesRegistradas.OrderBy(r => r.Observaciones).ToList() : recepcionesRegistradas.OrderByDescending(r => r.Observaciones).ToList();
                default:
                    return ordenRecepcionAscendente ? recepcionesRegistradas.OrderBy(r => r.FechaRecepcion).ToList() : recepcionesRegistradas.OrderByDescending(r => r.FechaRecepcion).ToList();
            }
        }

        #endregion

        #region "Métodos auxiliares"

        private void RefrescarDatos()
        {
            RefrescarOrdenesEmitidas();
            RefrescarRecepciones();
        }

        private void RefrescarOrdenesEmitidas()
        {
            ordenesEmitidas = ordenCompraBLL.ListarEmitidas();
            MostrarOrdenesEmitidas();

            dgvOrdenesEmitidas.ClearSelection();
            ordenSeleccionada = null;
        }

        private void MostrarOrdenesEmitidas()
        {
            dgvOrdenesEmitidas.DataSource = null;
            dgvOrdenesEmitidas.DataSource = OrdenarOrdenesEmitidas().Select(oc => new
            {
                oc.Id,
                oc.Numero,
                Proveedor = oc.Proveedor?.RazonSocial ?? "-",
                Booking = oc.BookingOrigen?.Numero ?? "-",
                Fecha = oc.FechaEmision.ToShortDateString(),
                Total = oc.ImporteTotal.ToString("C"),
                Estado = oc.Estado?.Descripcion ?? "-"
            }).ToList();
        }

        private void RefrescarRecepciones()
        {
            recepcionesRegistradas = recepcionBLL.ListarTodo();
            MostrarRecepciones();

            dgvRecepciones.ClearSelection();
        }

        private void MostrarRecepciones()
        {
            dgvRecepciones.DataSource = null;
            dgvRecepciones.DataSource = OrdenarRecepciones().Select(r => new
            {
                r.Id,
                Fecha = r.FechaRecepcion.ToShortDateString(),
                Orden = r.OrdenDeCompra?.Numero ?? "-",
                Servicio = r.Servicio?.Nombre ?? "-",
                r.Observaciones
            }).ToList();
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
                "Número: " + ordenSeleccionada.Numero + Environment.NewLine +
                "Booking: " + ordenSeleccionada.BookingOrigen?.Numero + Environment.NewLine +
                "Proveedor: " + ordenSeleccionada.Proveedor?.RazonSocial + Environment.NewLine +
                "Servicios: " + servicios + Environment.NewLine +
                "Total: " + ordenSeleccionada.ImporteTotal.ToString("C") + Environment.NewLine +
                "Estado: " + ordenSeleccionada.Estado?.Descripcion;
        }

        private void LimpiarSeleccion()
        {
            ordenSeleccionada = null;
            txtDetalleOrden.Clear();
            txtObservaciones.Clear();
            dtpFechaRecepcion.Value = DateTime.Now;
            dgvOrdenesEmitidas.ClearSelection();
        }

        #endregion
    }
}
