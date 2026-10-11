using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BE.Transporte;
using BE.Dominio;
using BLL.Transporte;
using System.Drawing;

namespace GlobalTransLines.UI.Gestion2_Transporte
{
    public partial class FrmAsignarBuqueContenedor : Form
    {
        #region "Atributos y Constructor"

        private readonly BookingBLL gestorBooking = new BookingBLL();
        private readonly ContenedorBLL gestorContenedor = new ContenedorBLL();
        private readonly BuqueBLL gestorBuque = new BuqueBLL();
        private readonly AsignacionBLL gestorAsignacion = new AsignacionBLL();

        private BEBooking bookingSel;
        private BEContenedor contenedorSel;
        private BEBuque buqueSel;
        private BEAsignacion asignacionSel;

        private string columnaOrdenBooking = "FechaTentativaSalida";
        private bool ordenBookingAscendente = false;
        private string columnaOrdenContenedor = "Codigo";
        private bool ordenContenedorAscendente = true;
        private string columnaOrdenBuque = "Nombre";
        private bool ordenBuqueAscendente = true;
        private string columnaOrdenAsignacion = "FechaAsignacion";
        private bool ordenAsignacionAscendente = false;

        private TabControl tabControlProceso;
        private TabPage tabPageBookings;
        private TabPage tabPageRecursos;
        private TabPage tabPageAsignaciones;

        public FrmAsignarBuqueContenedor()
        {
            InitializeComponent();
            ConfigurarSolapasOperativas();
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmAsignarBuqueContenedor_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrillas();
                RefrescarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar formulario: " + ex.Message);
            }
        }

        #endregion

        #region "Carga de datos"

        private void RefrescarTodo()
        {
            grdBookings.DataSource = gestorBooking.ListarTodo()
                .Where(b => b.Estado != null && b.Estado.Codigo == EstadoBooking.Confirmado.Codigo)
                .ToList();

            grdContenedores.DataSource = gestorContenedor.ListarTodo()
                .Where(c => c.Estado.EsDisponible()).ToList();



            grdBuques.DataSource = gestorBuque.ListarTodo()
                .Where(b => b.Estado != null && b.Estado.Codigo == EstadoBuque.Disponible.Codigo)
                .ToList();

            grdAsignaciones.DataSource = gestorAsignacion.ListarTodo();
        }

       



        #endregion

        #region "Selección de grillas"

        private void grdBookings_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                bookingSel = grdBookings.CurrentRow?.DataBoundItem as BEBooking;

                if (bookingSel == null)
                    return;

                // Solicita automaticamente los recursos compatibles
                var sugerencias = gestorAsignacion.SugerirRecursosPara(bookingSel);
                var compatibleContenedores = sugerencias.Item1;
                var buquesDisponibles = sugerencias.Item2;

                // Actualiza las grillas con las sugerencias
                grdContenedores.DataSource = compatibleContenedores;
                grdBuques.DataSource = buquesDisponibles;

                // Limpia selección previa
                contenedorSel = null;
                buqueSel = null;
                grdContenedores.ClearSelection();
                grdBuques.ClearSelection();

                if (tabControlProceso != null)
                    tabControlProceso.SelectedTab = tabPageRecursos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al sugerir recursos: " + ex.Message);
            }
        }


        private void grdContenedores_SelectionChanged(object sender, EventArgs e)
        {
            contenedorSel = grdContenedores.CurrentRow?.DataBoundItem as BEContenedor;
        }

        private void grdBuques_SelectionChanged(object sender, EventArgs e)
        {
            buqueSel = grdBuques.CurrentRow?.DataBoundItem as BEBuque;
        }

        private void grdAsignaciones_SelectionChanged(object sender, EventArgs e)
        {
            asignacionSel = grdAsignaciones.CurrentRow?.DataBoundItem as BEAsignacion;
        }

        #endregion

        #region "Botones CRUD"

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (bookingSel == null || contenedorSel == null || buqueSel == null)
                    throw new Exception("Debe seleccionar Booking, Contenedor y Buque.");
                bool yaAsignado = gestorAsignacion.ListarTodo().Any(a => a.BookingId == bookingSel.Id);

                if (yaAsignado)
                    throw new Exception("Ese Booking ya tiene una asignación. No se puede asignar dos veces.");
                
                gestorAsignacion.Asignar(bookingSel, contenedorSel, buqueSel);
              
                MessageBox.Show("Asignación creada correctamente.");
                RefrescarTodo();
                if (tabControlProceso != null)
                    tabControlProceso.SelectedTab = tabPageAsignaciones;
            }
            catch (Exception ex)
            {
                var detalle = ObtenerMensajeOriginal(ex);

                MessageBox.Show(
                    "No se pudo completar la asignación.\n\nDetalle:\n" + detalle,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string ObtenerMensajeOriginal(Exception ex)
        {
            var actual = ex;
            while (actual.InnerException != null)
                actual = actual.InnerException;

            return actual.Message;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (asignacionSel == null)
                    throw new Exception("Seleccione una asignación para modificar.");

                
                asignacionSel.Estado = EstadoAsignacion.Confirmada;
                gestorAsignacion.Guardar(asignacionSel);
                MessageBox.Show("Asignación modificada.");
                RefrescarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (asignacionSel == null)
                    throw new Exception("Seleccione una asignación para eliminar.");

                gestorAsignacion.Eliminar(asignacionSel);
                MessageBox.Show("Asignación eliminada.");
                RefrescarTodo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string filtro = txtBuscar.Text.ToLower().Trim();
                var lista = gestorAsignacion.ListarTodo()
                    .Where(a =>
                        a.BookingId.ToString().Contains(filtro) ||
                        a.BuqueId.ToString().Contains(filtro))
                    .ToList();
                grdAsignaciones.DataSource = lista;
                if (tabControlProceso != null)
                    tabControlProceso.SelectedTab = tabPageAsignaciones;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }
        private void btnNuevoContenedor_Click(object sender, EventArgs e)
        {

            var frm = new FrmContenedor(); // el nombre real de tu ABM
            frm.ShowDialog();

           
            RefrescarPostAltaContenedor();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region "Configuración Visual"

        private void ConfigurarSolapasOperativas()
        {
            if (tabControlProceso != null)
                return;

            SuspendLayout();

            var layoutPrincipal = new TableLayoutPanel();
            layoutPrincipal.ColumnCount = 1;
            layoutPrincipal.RowCount = 2;
            layoutPrincipal.Dock = DockStyle.Fill;
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));

            tabControlProceso = new TabControl();
            tabControlProceso.Dock = DockStyle.Fill;

            tabPageBookings = new TabPage("1. Bookings pendientes");
            tabPageRecursos = new TabPage("2. Recursos sugeridos");
            tabPageAsignaciones = new TabPage("3. Asignaciones realizadas");

            tabControlProceso.Controls.Add(tabPageBookings);
            tabControlProceso.Controls.Add(tabPageRecursos);
            tabControlProceso.Controls.Add(tabPageAsignaciones);

            gbBooking.Dock = DockStyle.Fill;
            gbBooking.Text = "Bookings confirmados pendientes de asignación";
            tabPageBookings.Controls.Add(gbBooking);

            var layoutRecursos = new TableLayoutPanel();
            layoutRecursos.ColumnCount = 1;
            layoutRecursos.RowCount = 2;
            layoutRecursos.Dock = DockStyle.Fill;
            layoutRecursos.Padding = new Padding(6);
            layoutRecursos.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            layoutRecursos.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));

            gbContenedores.Dock = DockStyle.Fill;
            gbContenedores.Text = "Contenedores compatibles";
            gbBuques.Dock = DockStyle.Fill;
            gbBuques.Text = "Buques disponibles";

            tableLayoutPanel2.RowStyles.Clear();
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            flowLayoutPanel1.Padding = new Padding(6, 4, 0, 0);
            btnNuevoContenedor.Size = new Size(170, 28);

            layoutRecursos.Controls.Add(gbContenedores, 0, 0);
            layoutRecursos.Controls.Add(gbBuques, 0, 1);
            tabPageRecursos.Controls.Add(layoutRecursos);

            gbAsignaciones.Dock = DockStyle.Fill;
            gbAsignaciones.Text = "Asignaciones registradas";
            tabPageAsignaciones.Controls.Add(gbAsignaciones);

            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Text = "Acciones";
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Padding = new Padding(8, 8, 0, 0);
            flowLayoutPanel2.WrapContents = false;
            flowLayoutPanel2.AutoScroll = true;

            btnGuardar.Text = "Guardar asignación";
            btnModificar.Text = "Modificar";
            btnBuscar.Text = "Buscar";
            btnEliminar.Text = "Eliminar";
            btnCerrar.Text = "Cerrar";

            layoutPrincipal.Controls.Add(tabControlProceso, 0, 0);
            layoutPrincipal.Controls.Add(groupBox2, 0, 1);

            Controls.Clear();
            Controls.Add(layoutPrincipal);

            ResumeLayout(false);
        }

        private void ConfigurarGrillas()
        {

            foreach (var g in new[] { grdBookings, grdContenedores, grdBuques, grdAsignaciones })
            {
                g.AutoGenerateColumns = true;
                g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                g.MultiSelect = false;
                g.ReadOnly = true;
                g.RowHeadersVisible = false;
                g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                g.AllowUserToAddRows = false;
                g.AllowUserToDeleteRows = false;
                g.BackgroundColor = SystemColors.Window;
                g.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
                g.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
            }

            // Personalización de cada grilla
            ConfigurarGrillaBookings();
            ConfigurarGrillaContenedores();
            ConfigurarGrillaBuques();
            ConfigurarGrillaAsignaciones();
        }
        private void ConfigurarGrillaBookings()
        {
            grdBookings.Columns.Clear();
            grdBookings.AutoGenerateColumns = false;

            var colNumero = new DataGridViewTextBoxColumn
            {
                HeaderText = "N° Booking",
                Name = "Numero",
                DataPropertyName = "Numero",
                FillWeight = 15
            };
            grdBookings.Columns.Add(colNumero);

            var colCliente = new DataGridViewTextBoxColumn
            {
                HeaderText = "Cliente",
                Name = "Cliente",
                FillWeight = 22
            };
            grdBookings.Columns.Add(colCliente);

            var colFecha = new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha Tentativa",
                Name = "FechaTentativaSalida",
                DataPropertyName = "FechaTentativaSalida",
                FillWeight = 14,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            };
            grdBookings.Columns.Add(colFecha);

            var colEstado = new DataGridViewTextBoxColumn
            {
                HeaderText = "Estado",
                Name = "Estado",
                
                DataPropertyName = "Estado",
                FillWeight = 12
            };
            grdBookings.Columns.Add(colEstado);

            var colDesc = new DataGridViewTextBoxColumn
            {
                HeaderText = "Descripción de Carga",
                Name = "DescripcionCarga",
                FillWeight = 37
            };
            grdBookings.Columns.Add(colDesc);

            
            grdBookings.CellFormatting += (s, e) =>
            {
                var b = grdBookings.Rows[e.RowIndex].DataBoundItem as BEBooking;
                if (b == null) return;

                switch (grdBookings.Columns[e.ColumnIndex].Name)
                {
                    case "Cliente":
                        e.Value = b.Cliente?.RazonSocial ?? "(sin cliente)";
                        break;
                    case "Estado":
                        e.Value = b.Estado?.Descripcion ?? "-";
                        break;
                    case "DescripcionCarga":
                        e.Value = b.Cargas?.FirstOrDefault()?.Descripcion ?? "(sin carga)";
                        break;
                }
            };

            grdBookings.ColumnHeaderMouseClick -= grdBookings_ColumnHeaderMouseClick;
            grdBookings.ColumnHeaderMouseClick += grdBookings_ColumnHeaderMouseClick;
        }


        private void ConfigurarGrillaContenedores()
        {
            grdContenedores.Columns.Clear();
            grdContenedores.AutoGenerateColumns = false;

            grdContenedores.Columns.Add("Codigo", "Código");
            grdContenedores.Columns["Codigo"].DataPropertyName = "Codigo";

            grdContenedores.Columns.Add("Tipo", "Tipo");
            grdContenedores.Columns.Add("Estado", "Estado");
            grdContenedores.Columns.Add("Restriccion", "Restricción");

            grdContenedores.CellFormatting += (s, e) =>
            {
                var c = grdContenedores.Rows[e.RowIndex].DataBoundItem as BEContenedor;
                if (c == null) return;

                switch (grdContenedores.Columns[e.ColumnIndex].Name)
                {
                    case "Tipo":
                        e.Value = c.Tipo?.Descripcion ?? "-";
                        grdContenedores.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = c.Tipo?.Descripcion; 
                       
                        break;
                    case "Estado":
                        e.Value = c.Estado?.Descripcion ?? "-";
                        break;
                    case "Restriccion":
                        e.Value = c.Restriccion?.Descripcion ?? "-";
                        break;
                }
            };

            grdContenedores.ColumnHeaderMouseClick -= grdContenedores_ColumnHeaderMouseClick;
            grdContenedores.ColumnHeaderMouseClick += grdContenedores_ColumnHeaderMouseClick;
        }

        private void ConfigurarGrillaBuques()
        {
            grdBuques.Columns.Clear();
            grdBuques.AutoGenerateColumns = false;

            grdBuques.Columns.Add("Nombre", "Nombre");
            grdBuques.Columns["Nombre"].DataPropertyName = "Nombre";

            grdBuques.Columns.Add("CapacidadTEU", "Capacidad TEU");
            grdBuques.Columns["CapacidadTEU"].DataPropertyName = "CapacidadTEU";

            grdBuques.Columns.Add("TEUUsados", "TEU usados");
            grdBuques.Columns.Add("Ocupacion", "% ocupado");
            grdBuques.Columns.Add("DisponiblePeso", "Disponible (tn)");

            grdBuques.Columns.Add("Estado", "Estado");

            grdBuques.CellFormatting += (s, e) =>
            {
                var b = grdBuques.Rows[e.RowIndex].DataBoundItem as BEBuque;
                if (b == null) return;

                string col = grdBuques.Columns[e.ColumnIndex].Name;

                if (col == "Estado")
                {
                    e.Value = b.Estado != null ? b.Estado.Descripcion : "-";
                    return;
                }

                if (col == "TEUUsados")
                {
                    int usados = gestorBuque.CalcularTeuAsignados(b.Id);
                    e.Value = usados + " / " + b.CapacidadTEU;
                    return;
                }
                if (col == "DisponiblePeso")
                {
                    decimal disp = gestorBuque.CalcularDisponibilidadPesoToneladas(b.Id);
                    e.Value = disp.ToString("0.##");
                    return;
                }

                if (col == "Ocupacion")
                {
                    decimal p = gestorBuque.CalcularPorcentajeOcupado(b.Id);
                    e.Value = p.ToString("0.##") + "%";
                    return;
                }
            };

            grdBuques.ColumnHeaderMouseClick -= grdBuques_ColumnHeaderMouseClick;
            grdBuques.ColumnHeaderMouseClick += grdBuques_ColumnHeaderMouseClick;
        }



        private void ConfigurarGrillaAsignaciones()
        {
            grdAsignaciones.Columns.Clear();
            grdAsignaciones.AutoGenerateColumns = false;

            grdAsignaciones.Columns.Add("Booking", "Booking");
            grdAsignaciones.Columns.Add("Contenedor", "Tipo Contenedor");
            grdAsignaciones.Columns.Add("Buque", "Buque");

            var colFecha = new DataGridViewTextBoxColumn
            {
                HeaderText = "Fecha Asignación",
                Name = "FechaAsignacion",
                DataPropertyName = "FechaAsignacion",
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            };
            grdAsignaciones.Columns.Add(colFecha);

            var colEstado = new DataGridViewTextBoxColumn
            {
                HeaderText = "Estado",
                Name = "Estado",
                DataPropertyName = "Estado"
            };
            grdAsignaciones.Columns.Add(colEstado);

            grdAsignaciones.CellFormatting += (s, e) =>
            {
                var a = grdAsignaciones.Rows[e.RowIndex].DataBoundItem as BEAsignacion;
                if (a == null) return;

                switch (grdAsignaciones.Columns[e.ColumnIndex].Name)
                {
                    case "Booking":
                        e.Value = a.Booking?.Numero ?? $"BK-{a.BookingId}";
                        break;
                    case "Contenedor":
                        var tipo = TipoContenedor.ObtenerPorCodigo(a.Booking?.TipoContenedorCodigo);
                        e.Value = tipo != null ? $"{tipo.Codigo} - {tipo.Descripcion.Split('|')[0].Trim()}" : "(sin tipo)";
                        grdAsignaciones.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = tipo?.Descripcion;
                        break;

                    case "Buque":
                        e.Value = a.Buque?.Nombre ?? $"BQ-{a.BuqueId}";
                        break;
                    case "Estado":
                        e.Value = a.Estado?.Descripcion ?? "-";
                        break;
                }
            };

            grdAsignaciones.ColumnHeaderMouseClick -= grdAsignaciones_ColumnHeaderMouseClick;
            grdAsignaciones.ColumnHeaderMouseClick += grdAsignaciones_ColumnHeaderMouseClick;
        }






        #endregion

        #region "Orden de grillas"

        private void grdBookings_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = grdBookings.Columns[e.ColumnIndex].Name;
            ordenBookingAscendente = columnaOrdenBooking == columna ? !ordenBookingAscendente : true;
            columnaOrdenBooking = columna;
            OrdenarBookings();
        }

        private void grdContenedores_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = grdContenedores.Columns[e.ColumnIndex].Name;
            ordenContenedorAscendente = columnaOrdenContenedor == columna ? !ordenContenedorAscendente : true;
            columnaOrdenContenedor = columna;
            OrdenarContenedores();
        }

        private void grdBuques_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = grdBuques.Columns[e.ColumnIndex].Name;
            ordenBuqueAscendente = columnaOrdenBuque == columna ? !ordenBuqueAscendente : true;
            columnaOrdenBuque = columna;
            OrdenarBuques();
        }

        private void grdAsignaciones_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = grdAsignaciones.Columns[e.ColumnIndex].Name;
            ordenAsignacionAscendente = columnaOrdenAsignacion == columna ? !ordenAsignacionAscendente : true;
            columnaOrdenAsignacion = columna;
            OrdenarAsignaciones();
        }

        private void OrdenarBookings()
        {
            var lista = (grdBookings.DataSource as IEnumerable<BEBooking>)?.ToList();
            if (lista == null) return;

            // La grilla muestra datos reconstruidos, por eso el orden se resuelve sobre la lista visible.
            switch (columnaOrdenBooking)
            {
                case "Numero":
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.Numero).ToList() : lista.OrderByDescending(b => b.Numero).ToList();
                    break;
                case "Cliente":
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.Cliente?.RazonSocial).ToList() : lista.OrderByDescending(b => b.Cliente?.RazonSocial).ToList();
                    break;
                case "Estado":
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.Estado?.Descripcion).ToList() : lista.OrderByDescending(b => b.Estado?.Descripcion).ToList();
                    break;
                case "DescripcionCarga":
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.Cargas?.FirstOrDefault()?.Descripcion).ToList() : lista.OrderByDescending(b => b.Cargas?.FirstOrDefault()?.Descripcion).ToList();
                    break;
                default:
                    lista = ordenBookingAscendente ? lista.OrderBy(b => b.FechaTentativaSalida).ToList() : lista.OrderByDescending(b => b.FechaTentativaSalida).ToList();
                    break;
            }

            grdBookings.DataSource = lista;
        }

        private void OrdenarContenedores()
        {
            var lista = (grdContenedores.DataSource as IEnumerable<BEContenedor>)?.ToList();
            if (lista == null) return;

            // Respeta el filtro actual de compatibilidad para no mostrar contenedores que la BLL descarto.
            switch (columnaOrdenContenedor)
            {
                case "Tipo":
                    lista = ordenContenedorAscendente ? lista.OrderBy(c => c.Tipo?.Descripcion).ToList() : lista.OrderByDescending(c => c.Tipo?.Descripcion).ToList();
                    break;
                case "Estado":
                    lista = ordenContenedorAscendente ? lista.OrderBy(c => c.Estado?.Descripcion).ToList() : lista.OrderByDescending(c => c.Estado?.Descripcion).ToList();
                    break;
                case "Restriccion":
                    lista = ordenContenedorAscendente ? lista.OrderBy(c => c.Restriccion?.Descripcion).ToList() : lista.OrderByDescending(c => c.Restriccion?.Descripcion).ToList();
                    break;
                default:
                    lista = ordenContenedorAscendente ? lista.OrderBy(c => c.Codigo).ToList() : lista.OrderByDescending(c => c.Codigo).ToList();
                    break;
            }

            grdContenedores.DataSource = lista;
        }

        private void OrdenarBuques()
        {
            var lista = (grdBuques.DataSource as IEnumerable<BEBuque>)?.ToList();
            if (lista == null) return;

            switch (columnaOrdenBuque)
            {
                case "CapacidadTEU":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => b.CapacidadTEU).ToList() : lista.OrderByDescending(b => b.CapacidadTEU).ToList();
                    break;
                case "TEUUsados":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => gestorBuque.CalcularTeuAsignados(b.Id)).ToList() : lista.OrderByDescending(b => gestorBuque.CalcularTeuAsignados(b.Id)).ToList();
                    break;
                case "Ocupacion":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => gestorBuque.CalcularPorcentajeOcupado(b.Id)).ToList() : lista.OrderByDescending(b => gestorBuque.CalcularPorcentajeOcupado(b.Id)).ToList();
                    break;
                case "DisponiblePeso":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => gestorBuque.CalcularDisponibilidadPesoToneladas(b.Id)).ToList() : lista.OrderByDescending(b => gestorBuque.CalcularDisponibilidadPesoToneladas(b.Id)).ToList();
                    break;
                case "Estado":
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => b.Estado?.Descripcion).ToList() : lista.OrderByDescending(b => b.Estado?.Descripcion).ToList();
                    break;
                default:
                    lista = ordenBuqueAscendente ? lista.OrderBy(b => b.Nombre).ToList() : lista.OrderByDescending(b => b.Nombre).ToList();
                    break;
            }

            grdBuques.DataSource = lista;
        }

        private void OrdenarAsignaciones()
        {
            var lista = (grdAsignaciones.DataSource as IEnumerable<BEAsignacion>)?.ToList();
            if (lista == null) return;

            switch (columnaOrdenAsignacion)
            {
                case "Booking":
                    lista = ordenAsignacionAscendente ? lista.OrderBy(a => a.Booking?.Numero ?? a.BookingId.ToString()).ToList() : lista.OrderByDescending(a => a.Booking?.Numero ?? a.BookingId.ToString()).ToList();
                    break;
                case "Contenedor":
                    lista = ordenAsignacionAscendente ? lista.OrderBy(a => a.Contenedor?.Codigo ?? a.ContenedorId.ToString()).ToList() : lista.OrderByDescending(a => a.Contenedor?.Codigo ?? a.ContenedorId.ToString()).ToList();
                    break;
                case "Buque":
                    lista = ordenAsignacionAscendente ? lista.OrderBy(a => a.Buque?.Nombre ?? a.BuqueId.ToString()).ToList() : lista.OrderByDescending(a => a.Buque?.Nombre ?? a.BuqueId.ToString()).ToList();
                    break;
                case "Estado":
                    lista = ordenAsignacionAscendente ? lista.OrderBy(a => a.Estado?.Descripcion).ToList() : lista.OrderByDescending(a => a.Estado?.Descripcion).ToList();
                    break;
                default:
                    lista = ordenAsignacionAscendente ? lista.OrderBy(a => a.FechaAsignacion).ToList() : lista.OrderByDescending(a => a.FechaAsignacion).ToList();
                    break;
            }

            grdAsignaciones.DataSource = lista;
        }

        #endregion

        #region "Otros métodos"
        private void RefrescarPostAltaContenedor()
        {
            // Siempre refrescar asignaciones y buques disponibles
            grdAsignaciones.DataSource = gestorAsignacion.ListarTodo();

            // Si hay booking seleccionado, mantené el modo compatibilidad
            if (bookingSel != null)
            {
                var sugerencias = gestorAsignacion.SugerirRecursosPara(bookingSel);
                grdContenedores.DataSource = sugerencias.Item1;
                grdBuques.DataSource = sugerencias.Item2;
            }
            else
            {
                // Si no hay booking, mostrar todos los disponibles
                grdContenedores.DataSource = gestorContenedor.ListarTodo()
                    .Where(c => c.Estado != null && c.Estado.EsDisponible()).ToList();

                grdBuques.DataSource = gestorBuque.ListarTodo()
                    .Where(b => b.Estado != null && b.Estado.Codigo == EstadoBuque.Disponible.Codigo)
                    .ToList();
            }

            grdContenedores.ClearSelection();
            grdBuques.ClearSelection();
            contenedorSel = null;
            buqueSel = null;
        }

        #endregion
    }
}



