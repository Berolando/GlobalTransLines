using BE;
using BLL.Personas;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GlobalTransLines.UI.Controles;

namespace GlobalTransLines.UI.Gestion1_Booking
{
    public partial class FrmCliente : Form
    {
        #region "Atributos Y Constructor"


        private ClienteBLL gestorCliente = new ClienteBLL();
        private List<BECliente> listaClientes = new List<BECliente>();
        private BECliente clienteSeleccionado;
        public FrmCliente()
        {
            InitializeComponent();
        }

        #endregion
        #region "Eventos del formulario"
        private void FrmCliente_Load(object sender, System.EventArgs e)
        {
            ConfiguracionGrilla(grdClientes);
            RefrescarGrilla();
        }

        #endregion
        #region "Eventos de botones"
        private void btnAlta_Click(object sender, System.EventArgs e)
        {
            try
            {
                BECliente cliente = new BECliente
                {
                    Nombre = ucDatosPersonales.Nombre,
                    Apellido = ucDatosPersonales.Apellido,
                    DNI = ucDatosPersonales.DNI,
                    Telefono = ucDatosPersonales.Telefono,
                    Email = ucDatosPersonales.Email,
                    FechaNacimiento = ucDatosPersonales.FechaNacimiento,
                    Direccion = ucDatosPersonales.Direccion,
                    Cuit = ucDatosComerciales.Cuit,
                    RazonSocial = ucDatosComerciales.RazonSocial,
                    CondicionFiscal = ucDatosComerciales.CondicionFiscal

                };
                gestorCliente.Guardar(cliente);
                RefrescarGrilla();
                LimpiarControles();
                MessageBox.Show("Cliente agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar cliente: " + ex.Message);
            }
        }
        private void btnModificar_Click(object sender, System.EventArgs e)
        {
            if (clienteSeleccionado == null)
            {
                MessageBox.Show("Seleccione un cliente para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                clienteSeleccionado.Nombre = ucDatosPersonales.Nombre;
                clienteSeleccionado.Apellido = ucDatosPersonales.Apellido;
                clienteSeleccionado.DNI = ucDatosPersonales.DNI;
                clienteSeleccionado.Telefono = ucDatosPersonales.Telefono;
                clienteSeleccionado.Email = ucDatosPersonales.Email;
                clienteSeleccionado.FechaNacimiento = ucDatosPersonales.FechaNacimiento;
                clienteSeleccionado.Direccion = ucDatosPersonales.Direccion;
                clienteSeleccionado.Cuit = ucDatosComerciales.Cuit;
                clienteSeleccionado.RazonSocial = ucDatosComerciales.RazonSocial;
                clienteSeleccionado.CondicionFiscal = ucDatosComerciales.CondicionFiscal;

                gestorCliente.Guardar(clienteSeleccionado);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Cliente modificado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar cliente: " + ex.Message);
            }

        }
        private void btnEliminar_Click(object sender, System.EventArgs e)
        {
            if (clienteSeleccionado == null)
            {
                MessageBox.Show("Seleccione un cliente para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmar = MessageBox.Show(
                "¿Está seguro de eliminar el cliente seleccionado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmar == DialogResult.Yes)
            {
                try
                {
                    gestorCliente.Eliminar(clienteSeleccionado);
                    RefrescarGrilla();
                    LimpiarControles();
                    MessageBox.Show("Cliente eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar cliente: " + ex.Message);
                }
            }

        }
        #endregion
        #region "Configuracion Visual"
        private void ConfiguracionGrilla(DataGridView grilla)
        {
            grilla.AutoGenerateColumns = true;
            grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grilla.MultiSelect = false;
            grilla.ReadOnly = true;
            grilla.RowHeadersVisible = false;
            grilla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grilla.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
        }



        #endregion
        #region"Eventos de Grilla"
        private void grdClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                clienteSeleccionado = (BECliente)grdClientes.Rows[e.RowIndex].DataBoundItem;
                CargarClienteEnControles(clienteSeleccionado);
            }

        }

        #endregion
        #region "Metodos Auxiliares"
        private void RefrescarGrilla()
        {
            listaClientes = gestorCliente.ListarTodo(); // o .Listar(), según tu BLL
            grdClientes.DataSource = null;
            grdClientes.DataSource = listaClientes;
        }
        private void LimpiarControles()
        {
            ucDatosPersonales.Limpiar();
            ucDatosComerciales.Limpiar();
        }
        #endregion
        #region "Carga de datos"

        private void CargarClienteEnControles(BECliente cliente)
        {
            if (cliente == null) return;

            ucDatosPersonales.Cargar(cliente);
            ucDatosComerciales.Cargar(cliente);
        }

        #endregion

    }
}

/*using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BLL;
using BLL.Transversal;
using BE;
using BE.Transversal;
using BE.Enums;

namespace GlobalTransLines.[ModuloCorrespondiente]
{
    public partial class [NombreDelFormulario] : Form
    {
        #region "Atributos y Constructor"

        // Atributos privados (BLL, listas, entidades, etc.)
        public [NombreDelFormulario]()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos del formulario"

        // Load, Shown, Closing, etc.

        #endregion

        #region "Eventos de botones"

        // Alta, Modificar, Eliminar, Buscar, etc.

        #endregion

        #region "Configuración Visual"

        // Métodos de configuración de grillas, combos, estilos, etc.

        #endregion

        #region "Eventos de grilla"

        // CellClick, CellDoubleClick, SelectionChanged, etc.

        #endregion

        #region "Métodos auxiliares"

        // RefrescarGrilla, LimpiarControles, Validar, etc.

        #endregion

        #region "Carga de datos"

        // CargarEntidadEnControles, MapearAObjeto, etc.

        #endregion
    }
}
*/