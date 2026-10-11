using BE.Personas;
using BE.Infra;
using BLL.Personas;
using BE.Dominio;
using GlobalTransLines.UI.Controles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Infra
{
    public partial class FrmEmpleado : Form
    {
        #region "Atributos y Constructor"

        private EmpleadoBLL gestorEmpleado = new EmpleadoBLL();
        public BEEmpleado empleadoSeleccionado {get; private set; }

        private BEDireccion direccionSeleccionada;
        private string rutaFotoSeleccionada;
        private bool quitarFotoSeleccionada;
        private string columnaOrdenEmpleado = "Id";
        private bool ordenEmpleadoAscendente = false;

        public bool ModoSeleccion { get; set; }


        public FrmEmpleado()
        {
            InitializeComponent();
        }
        #endregion

        #region "Eventos del formulario"
        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrilla(dgvEmpleados);
                cmbCargo.DataSource = CargoEmpleado.ObtenerTodos().ToList();
                cmbCargo.DisplayMember = "Descripcion";
                cmbCargo.ValueMember = "Codigo";
                cmbCargo.SelectedIndex = -1;
                RefrescarGrilla();
                LimpiarControles();
                if (ModoSeleccion)
                {
                   
                    btnGuardar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnEliminar.Enabled = false;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el formulario: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region "Eventos de botones"

       

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (empleadoSeleccionado != null)
                {
                    MessageBox.Show("Ya hay un empleado seleccionado. Use Modificar para actualizar sus datos o limpie el formulario para cargar uno nuevo.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool valido = ucDatosPersonales.EsValido()
                          && ucDirecciones.EsValido();
                if (!valido) return;
                BEDireccion direccion = ucDirecciones.Obtener();
                BEEmpleado empleado = new BEEmpleado
                {
                    Nombre = ucDatosPersonales.Nombre,
                    Apellido = ucDatosPersonales.Apellido,
                    DNI = ucDatosPersonales.DNI,
                    Telefono = ucDatosPersonales.Telefono,
                    Email = ucDatosPersonales.Email,
                    FechaNacimiento = ucDatosPersonales.FechaNacimiento,
                    cargoEmpleado = ObtenerCargoSeleccionado(),
                    Sueldo = (double)numSueldo.Value,
                    Direccion = direccion
                };
                gestorEmpleado.Guardar(empleado);
                if (GuardarFotoEmpleado(empleado))
                    gestorEmpleado.Guardar(empleado);

                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Empleado agregado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al agregar empleado: " + ex.Message,
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (empleadoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un empleado para modificar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool valido = ucDatosPersonales.EsValido()
                          && ucDirecciones.EsValido();
                if (!valido)
                    return;

                long idEmpleadoModificado = empleadoSeleccionado.Id;

                var empleadoPersistido = gestorEmpleado.ListarObjeto(new BEEmpleado { Id = idEmpleadoModificado });
                if (empleadoPersistido == null)
                    throw new Exception("No se encontró el empleado seleccionado.");

                empleadoPersistido.Nombre = ucDatosPersonales.Nombre;
                empleadoPersistido.Apellido = ucDatosPersonales.Apellido;
                empleadoPersistido.DNI = ucDatosPersonales.DNI;
                empleadoPersistido.Telefono = ucDatosPersonales.Telefono;
                empleadoPersistido.Email = ucDatosPersonales.Email;
                empleadoPersistido.FechaNacimiento = ucDatosPersonales.FechaNacimiento;
                empleadoPersistido.cargoEmpleado = ObtenerCargoSeleccionado();
                empleadoPersistido.Sueldo = (double)numSueldo.Value;
                empleadoPersistido.Direccion = ucDirecciones.Obtener();

                if (quitarFotoSeleccionada)
                    empleadoPersistido.RutaFoto = string.Empty;

                GuardarFotoEmpleado(empleadoPersistido);
                gestorEmpleado.Guardar(empleadoPersistido);

                RefrescarGrilla(idEmpleadoModificado);
                LimpiarControles();

                MessageBox.Show("Empleado modificado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al modificar empleado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (empleadoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un empleado para eliminar.", "Atención",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult res = MessageBox.Show("¿Confirma la eliminación del empleado seleccionado?",
                        "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.No)
                    return;

                gestorEmpleado.Eliminar(empleadoSeleccionado);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Empleado eliminado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar empleado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            var listaFiltrada = gestorEmpleado.ListarActivos()
                .Where(c =>
                    c.Nombre.ToLower().Contains(filtro) ||
                    c.Apellido.ToLower().Contains(filtro))
                .ToList();

            dgvEmpleados.DataSource = null;
            dgvEmpleados.DataSource = OrdenarEmpleados(listaFiltrada);
            FormatearGrillaEmpleados();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        #endregion

        #region "Configuración Visual"

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
            grilla.ColumnHeaderMouseClick -= dgvEmpleados_ColumnHeaderMouseClick;
            grilla.ColumnHeaderMouseClick += dgvEmpleados_ColumnHeaderMouseClick;
            grilla.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
        }

        #endregion

        #region "Eventos de grilla"
        private void dgvEmpleados_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = dgvEmpleados.Columns[e.ColumnIndex].DataPropertyName;

            if (string.IsNullOrWhiteSpace(columna))
                columna = dgvEmpleados.Columns[e.ColumnIndex].Name;

            if (columnaOrdenEmpleado == columna)
                ordenEmpleadoAscendente = !ordenEmpleadoAscendente;
            else
            {
                columnaOrdenEmpleado = columna;
                ordenEmpleadoAscendente = true;
            }

            RefrescarGrilla();
        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            empleadoSeleccionado = (BEEmpleado)dgvEmpleados.Rows[e.RowIndex].DataBoundItem;
            CargarEmpleadoEnControles(empleadoSeleccionado);

        }
        private void dgvEmpleados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (ModoSeleccion)
                {
                    var emp = dgvEmpleados.Rows[e.RowIndex].DataBoundItem as BEEmpleado;

                    if (emp != null)
                    {
                        empleadoSeleccionado = emp; // propiedad pública (recomendado)
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }

                    return;
                }

                // Modo normal: reutilizás tu click (si querés)
                dgvEmpleados_CellClick(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar empleado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region "Métodos auxiliares"

        private void RefrescarGrilla(long? idSeleccionar = null)
        {
            dgvEmpleados.DataSource = null;
            var empleados = gestorEmpleado.ListarActivos()
                .ToList();
            dgvEmpleados.DataSource = OrdenarEmpleados(empleados);
            FormatearGrillaEmpleados();
            dgvEmpleados.AutoResizeColumns();

            if (!idSeleccionar.HasValue)
            {
                dgvEmpleados.ClearSelection();
                return;
            }

            foreach (DataGridViewRow fila in dgvEmpleados.Rows)
            {
                var empleado = fila.DataBoundItem as BEEmpleado;

                if (empleado == null || empleado.Id != idSeleccionar.Value)
                    continue;

                fila.Selected = true;
                dgvEmpleados.CurrentCell = fila.Cells[0];
                empleadoSeleccionado = empleado;
                CargarEmpleadoEnControles(empleadoSeleccionado);
                return;
            }
        }

        private void LimpiarControles()
        {
            ucDatosPersonales.Limpiar();
            ucDirecciones.Limpiar();
            cmbCargo.SelectedIndex = -1;
            numSueldo.Value = 0;
            CargarFoto(null);
           

            empleadoSeleccionado = null;
            direccionSeleccionada = null;
            rutaFotoSeleccionada = null;
            quitarFotoSeleccionada = false;

            dgvEmpleados.ClearSelection();
        }

        private List<BEEmpleado> OrdenarEmpleados(List<BEEmpleado> empleados)
        {
            Func<BEEmpleado, object> criterio;

            switch (columnaOrdenEmpleado)
            {
                case "Nombre":
                    criterio = e => e.Nombre ?? string.Empty;
                    break;
                case "Apellido":
                    criterio = e => e.Apellido ?? string.Empty;
                    break;
                case "DNI":
                    criterio = e => e.DNI ?? string.Empty;
                    break;
                case "Telefono":
                    criterio = e => e.Telefono ?? string.Empty;
                    break;
                case "Email":
                    criterio = e => e.Email ?? string.Empty;
                    break;
                case "FechaNacimiento":
                    criterio = e => e.FechaNacimiento;
                    break;
                case "cargoEmpleado":
                    criterio = e => e.cargoEmpleado != null ? e.cargoEmpleado.Descripcion : string.Empty;
                    break;
                case "Sueldo":
                    criterio = e => e.Sueldo;
                    break;
                case "Activo":
                    criterio = e => e.Activo;
                    break;
                default:
                    criterio = e => e.Id;
                    break;
            }

            return ordenEmpleadoAscendente
                ? empleados.OrderBy(criterio).ToList()
                : empleados.OrderByDescending(criterio).ToList();
        }

        #endregion

        #region "Carga de datos"

        private void CargarEmpleadoEnControles(BEEmpleado empleado)
        {
            if (empleado == null)
                return;

            ucDatosPersonales.Cargar(empleado);

            direccionSeleccionada = empleado.Direccion;
            ucDirecciones.Cargar(direccionSeleccionada);
            cmbCargo.SelectedValue = empleado.cargoEmpleado?.Codigo ?? string.Empty;
            numSueldo.Value = empleado.Sueldo > (double)numSueldo.Maximum
                ? numSueldo.Maximum
                : Convert.ToDecimal(empleado.Sueldo);
            CargarFoto(empleado.RutaFoto);

            rutaFotoSeleccionada = null;
            quitarFotoSeleccionada = false;

        }

        private CargoEmpleado ObtenerCargoSeleccionado()
        {
            string codigo = cmbCargo.SelectedValue?.ToString();
            var cargo = CargoEmpleado.ObtenerPorCodigo(codigo);

            if (cargo == null)
                throw new Exception("Debe seleccionar un cargo válido.");

            return cargo;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        private void btnSeleccionarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Title = "Seleccionar foto del empleado";
                dialogo.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialogo.ShowDialog() != DialogResult.OK)
                    return;

                rutaFotoSeleccionada = dialogo.FileName;
                quitarFotoSeleccionada = false;
                CargarFoto(rutaFotoSeleccionada);
            }
        }

        private void btnQuitarFoto_Click(object sender, EventArgs e)
        {
            rutaFotoSeleccionada = null;
            quitarFotoSeleccionada = true;
            CargarFoto(null);
        }

        private bool GuardarFotoEmpleado(BEEmpleado empleado)
        {
            if (string.IsNullOrWhiteSpace(rutaFotoSeleccionada) || empleado == null || empleado.Id <= 0)
                return false;

            string carpetaFotos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Personas", "Fotos");
            Directory.CreateDirectory(carpetaFotos);

            string extension = Path.GetExtension(rutaFotoSeleccionada);
            string nombreArchivo = "Empleado_" + empleado.Id + extension;
            string destino = Path.Combine(carpetaFotos, nombreArchivo);

            File.Copy(rutaFotoSeleccionada, destino, true);
            empleado.RutaFoto = Path.Combine("Data", "Personas", "Fotos", nombreArchivo);
            return true;
        }

        private void CargarFoto(string ruta)
        {
            pictureBox1.Image = Properties.Resources._554846;

            string rutaCompleta = ObtenerRutaFotoCompleta(ruta);
            if (string.IsNullOrWhiteSpace(rutaCompleta) || !File.Exists(rutaCompleta))
                return;

            using (FileStream stream = new FileStream(rutaCompleta, FileMode.Open, FileAccess.Read))
            {
                pictureBox1.Image = new Bitmap(stream);
            }
        }

        private string ObtenerRutaFotoCompleta(string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                return string.Empty;

            if (Path.IsPathRooted(ruta))
                return ruta;

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ruta);
        }

        private void FormatearGrillaEmpleados()
        {
            foreach (DataGridViewColumn columna in dgvEmpleados.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            if (dgvEmpleados.Columns["Sueldo"] != null)
            {
                dgvEmpleados.Columns["Sueldo"].DefaultCellStyle.Format = "C2";
                dgvEmpleados.Columns["Sueldo"].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvEmpleados.Columns["RutaFoto"] != null)
                dgvEmpleados.Columns["RutaFoto"].Visible = false;
        }

        #endregion


    }
}
