using BE.Personas;
using BE.Infra;
using BLL.Personas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Gestion1_Booking
{
    public partial class FrmCliente : Form
    {
        #region "Atributos y Constructor"

        private ClienteBLL gestorCliente = new ClienteBLL();
        public BECliente clienteSeleccionado { get; private set; }
        private BEDireccion direccionSeleccionada;
        public bool ModoSeleccion { get; set; }
        private string columnaOrdenCliente = "Id";
        private bool ordenClienteAscendente;

        public FrmCliente()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrilla(grdClientes);
                RefrescarGrilla();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el formulario: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                bool valido = ucDatosPersonales.EsValido()
                           && ucDirecciones.EsValido()
                           && ucDatosComerciales.EsValido();

                if (!valido)
                    return;

                BEDireccion direccion = ucDirecciones.Obtener();

                BECliente cliente = new BECliente
                {
                    Nombre = ucDatosPersonales.Nombre,
                    Apellido = ucDatosPersonales.Apellido,
                    DNI = ucDatosPersonales.DNI,
                    Telefono = ucDatosPersonales.Telefono,
                    Email = ucDatosPersonales.Email,
                    FechaNacimiento = ucDatosPersonales.FechaNacimiento,
                    Direccion = direccion,
                    Cuit = ucDatosComerciales.Cuit,
                    RazonSocial = ucDatosComerciales.RazonSocial,
                    CondicionFiscal = ucDatosComerciales.CondicionFiscal
                };

                gestorCliente.Guardar(cliente);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Cliente agregado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar cliente: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (clienteSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un cliente para modificar.");
                    return;
                }

                bool valido = ucDatosPersonales.EsValido()
                           && ucDirecciones.EsValido()
                           && ucDatosComerciales.EsValido();

                if (!valido)
                    return;

                clienteSeleccionado.Nombre = ucDatosPersonales.Nombre;
                clienteSeleccionado.Apellido = ucDatosPersonales.Apellido;
                clienteSeleccionado.DNI = ucDatosPersonales.DNI;
                clienteSeleccionado.Telefono = ucDatosPersonales.Telefono;
                clienteSeleccionado.Email = ucDatosPersonales.Email;
                clienteSeleccionado.FechaNacimiento = ucDatosPersonales.FechaNacimiento;
                clienteSeleccionado.Cuit = ucDatosComerciales.Cuit;
                clienteSeleccionado.RazonSocial = ucDatosComerciales.RazonSocial;
                clienteSeleccionado.CondicionFiscal = ucDatosComerciales.CondicionFiscal;

                clienteSeleccionado.Direccion = ucDirecciones.Obtener();

                gestorCliente.Guardar(clienteSeleccionado);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Cliente modificado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar cliente: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (clienteSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un cliente para eliminar.");
                    return;
                }

                DialogResult res = MessageBox.Show("¿Confirma la eliminación del cliente seleccionado?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.No)
                    return;

                gestorCliente.Eliminar(clienteSeleccionado);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Cliente eliminado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar cliente: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                RefrescarGrilla(txtBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar clientes: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region "Configuración Visual"

        private void ConfigurarGrilla(DataGridView grilla)
        {
            grilla.AutoGenerateColumns = false;
            grilla.Columns.Clear();
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.MultiSelect = false;
            grilla.ReadOnly = true;
            grilla.RowHeadersVisible = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.AllowUserToAddRows = false;
            grilla.AllowUserToDeleteRows = false;

            grilla.Columns.Add("RazonSocial", "Razón Social");
            grilla.Columns.Add("Cuit", "CUIT");
            grilla.Columns.Add("CondicionFiscal", "Condición Fiscal");
            grilla.Columns.Add("Referente", "Referente");
            grilla.Columns.Add("Telefono", "Teléfono");
            grilla.Columns.Add("Email", "Email");
            grilla.Columns.Add("Direccion", "Dirección");
            grilla.Columns.Add("Id", "Id");
            grilla.Columns.Add("Activo", "Activo");

            foreach (DataGridViewColumn columna in grilla.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            grilla.ColumnHeaderMouseClick -= grdClientes_ColumnHeaderMouseClick;
            grilla.ColumnHeaderMouseClick += grdClientes_ColumnHeaderMouseClick;

            grilla.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
        }

        #endregion

        #region "Eventos de grilla"

        private void grdClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                clienteSeleccionado = grdClientes.Rows[e.RowIndex].Tag as BECliente;
                if (clienteSeleccionado == null)
                    return;

                CargarClienteEnControles(clienteSeleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cliente seleccionado: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void grdClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                // Solo si el formulario está en modo selección 
                if (ModoSeleccion)
                {
                    clienteSeleccionado = grdClientes.Rows[e.RowIndex].Tag as BECliente;

                    if (clienteSeleccionado != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {

                    grdClientes_CellClick(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar cliente: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdClientes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = grdClientes.Columns[e.ColumnIndex].Name;

            // Orden manual: la grilla se carga con Rows.Add para conservar el objeto BECliente en el Tag.
            if (columnaOrdenCliente == columna)
                ordenClienteAscendente = !ordenClienteAscendente;
            else
            {
                columnaOrdenCliente = columna;
                ordenClienteAscendente = true;
            }

            RefrescarGrilla(txtBuscar.Text.Trim());
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        #endregion

        #region "Métodos auxiliares"

        private void RefrescarGrilla()
        {
            RefrescarGrilla(string.Empty);
        }

        private void RefrescarGrilla(string filtro)
        {
            var clientes = gestorCliente.ListarActivos();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.ToLower();
                clientes = clientes
                    .Where(c => (c.RazonSocial ?? "").ToLower().Contains(filtro)
                             || (c.Cuit ?? "").ToLower().Contains(filtro)
                             || (c.Nombre ?? "").ToLower().Contains(filtro)
                             || (c.Apellido ?? "").ToLower().Contains(filtro)
                             || (c.Email ?? "").ToLower().Contains(filtro)
                             || TextoDireccion(c.Direccion).ToLower().Contains(filtro))
                    .ToList();
            }

            grdClientes.Rows.Clear();

            foreach (var cliente in OrdenarClientes(clientes))
            {
                int fila = grdClientes.Rows.Add(
                    cliente.RazonSocial,
                    cliente.Cuit,
                    cliente.CondicionFiscal,
                    (cliente.Nombre + " " + cliente.Apellido).Trim(),
                    cliente.Telefono,
                    cliente.Email,
                    TextoDireccion(cliente.Direccion),
                    cliente.Id,
                    cliente.Activo ? "Sí" : "No"
                );

                grdClientes.Rows[fila].Tag = cliente;
            }
        }

        private List<BECliente> OrdenarClientes(List<BECliente> clientes)
        {
            Func<BECliente, object> criterio;

            switch (columnaOrdenCliente)
            {
                case "RazonSocial":
                    criterio = c => c.RazonSocial ?? string.Empty;
                    break;
                case "Cuit":
                    criterio = c => c.Cuit ?? string.Empty;
                    break;
                case "CondicionFiscal":
                    criterio = c => c.CondicionFiscal != null ? c.CondicionFiscal.ToString() : string.Empty;
                    break;
                case "Referente":
                    criterio = c => ((c.Nombre ?? string.Empty) + " " + (c.Apellido ?? string.Empty)).Trim();
                    break;
                case "Telefono":
                    criterio = c => c.Telefono ?? string.Empty;
                    break;
                case "Email":
                    criterio = c => c.Email ?? string.Empty;
                    break;
                case "Direccion":
                    criterio = c => TextoDireccion(c.Direccion);
                    break;
                case "Activo":
                    criterio = c => c.Activo;
                    break;
                default:
                    criterio = c => c.Id;
                    break;
            }

            return ordenClienteAscendente
                ? clientes.OrderBy(criterio).ToList()
                : clientes.OrderByDescending(criterio).ToList();
        }

        private void LimpiarControles()
        {
            ucDatosPersonales.Limpiar();
            ucDirecciones.Limpiar();
            ucDatosComerciales.Limpiar();

            clienteSeleccionado = null;
            direccionSeleccionada = null;
        }

        #endregion

        #region "Carga de datos"

        private void CargarClienteEnControles(BECliente cliente)
        {
            if (cliente == null)
                return;

            ucDatosPersonales.Cargar(cliente);

            direccionSeleccionada = cliente.Direccion;
            ucDirecciones.Cargar(direccionSeleccionada);

            ucDatosComerciales.Cargar(cliente);
        }

        #endregion

        #region "Formato"

        private string TextoDireccion(BEDireccion direccion)
        {
            if (direccion == null)
                return "-";

            var partes = new[]
            {
                direccion.Calle,
                direccion.Numero,
                direccion.Ciudad,
                direccion.Provincia,
                direccion.Pais
            }
            .Where(p => !string.IsNullOrWhiteSpace(p));

            return partes.Any() ? string.Join(", ", partes) : "-";
        }

        #endregion

    }
}

