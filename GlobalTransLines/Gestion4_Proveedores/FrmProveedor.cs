using BE.Personas;
using BLL.Personas;
using BE.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Gestion4_Proveedores
{
    public partial class FrmProveedor : Form
    {
        #region "Atributos y Constructor"

        private readonly ProveedorBLL gestorProveedor = new ProveedorBLL();
        public BEProveedor ProveedorSeleccionado { get; private set; }
        public bool ModoSeleccion { get; set; }
        private string columnaOrdenProveedor = "RazonSocial";
        private bool ordenProveedorAscendente = true;

        public FrmProveedor()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmProveedor_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrilla(dataGridView1);
                RefrescarGrilla();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool valido = ucDatosPersonales.EsValido()
                           && ucDirecciones.EsValido()
                           && ucDatosComerciales1.EsValido();

                if (!valido) return;

                BEDireccion direccion = ucDirecciones.Obtener();

                BEProveedor proveedor = new BEProveedor
                {
                    Nombre = ucDatosPersonales.Nombre,
                    Apellido = ucDatosPersonales.Apellido,
                    DNI = ucDatosPersonales.DNI,
                    Telefono = ucDatosPersonales.Telefono,
                    Email = ucDatosPersonales.Email,
                    Direccion = direccion,
                    Cuit = ucDatosComerciales1.Cuit,
                    RazonSocial = ucDatosComerciales1.RazonSocial,
                    CondicionFiscal = ucDatosComerciales1.CondicionFiscal
                };

                gestorProveedor.Guardar(proveedor);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Proveedor guardado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar proveedor: " + ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProveedorSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un proveedor para modificar.");
                    return;
                }

                bool valido = ucDatosPersonales.EsValido()
                           && ucDirecciones.EsValido()
                           && ucDatosComerciales1.EsValido();

                if (!valido) return;

                ProveedorSeleccionado.Nombre = ucDatosPersonales.Nombre;
                ProveedorSeleccionado.Apellido = ucDatosPersonales.Apellido;
                ProveedorSeleccionado.DNI = ucDatosPersonales.DNI;
                ProveedorSeleccionado.Telefono = ucDatosPersonales.Telefono;
                ProveedorSeleccionado.Email = ucDatosPersonales.Email;
                ProveedorSeleccionado.Cuit = ucDatosComerciales1.Cuit;
                ProveedorSeleccionado.RazonSocial = ucDatosComerciales1.RazonSocial;
                ProveedorSeleccionado.CondicionFiscal = ucDatosComerciales1.CondicionFiscal;
                ProveedorSeleccionado.Direccion = ucDirecciones.Obtener();

                gestorProveedor.Guardar(ProveedorSeleccionado);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Proveedor modificado correctamente.", "Éxito");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar proveedor: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProveedorSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un proveedor para eliminar.");
                    return;
                }

                DialogResult res = MessageBox.Show("¿Confirma la eliminación?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.No) return;

                gestorProveedor.Eliminar(ProveedorSeleccionado);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Proveedor eliminado correctamente.", "Éxito");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar proveedor: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();

            var listaFiltrada = gestorProveedor.ListarActivos()
                .Where(p =>
                    p.RazonSocial.ToLower().Contains(filtro) ||
                    p.Cuit.ToLower().Contains(filtro) ||
                    p.Nombre.ToLower().Contains(filtro))
                .ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaFiltrada;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
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
            grilla.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
            grilla.ColumnHeaderMouseClick -= dataGridView1_ColumnHeaderMouseClick;
            grilla.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;
        }

        #endregion

        #region "Eventos de grilla"

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                ProveedorSeleccionado = (BEProveedor)dataGridView1.CurrentRow.DataBoundItem;
                CargarProveedorEnControles(ProveedorSeleccionado);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedor: " + ex.Message);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                if (ModoSeleccion)
                {
                    ProveedorSeleccionado = (BEProveedor)dataGridView1.CurrentRow.DataBoundItem;
                    if (ProveedorSeleccionado != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    dataGridView1_CellClick(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar proveedor: " + ex.Message);
            }
        }

        #endregion

        #region "Orden de grilla"

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = dataGridView1.Columns[e.ColumnIndex].Name;
            ordenProveedorAscendente = columnaOrdenProveedor == columna ? !ordenProveedorAscendente : true;
            columnaOrdenProveedor = columna;
            OrdenarProveedores();
        }

        private void OrdenarProveedores()
        {
            var lista = (dataGridView1.DataSource as IEnumerable<BEProveedor>)?.ToList();
            if (lista == null)
                return;

            // Ordena la lista visible para respetar busquedas ya aplicadas.
            switch (columnaOrdenProveedor)
            {
                case "Id":
                    lista = ordenProveedorAscendente ? lista.OrderBy(p => p.Id).ToList() : lista.OrderByDescending(p => p.Id).ToList();
                    break;
                case "Cuit":
                    lista = ordenProveedorAscendente ? lista.OrderBy(p => p.Cuit).ToList() : lista.OrderByDescending(p => p.Cuit).ToList();
                    break;
                case "CondicionFiscal":
                    lista = ordenProveedorAscendente ? lista.OrderBy(p => p.CondicionFiscal?.ToString()).ToList() : lista.OrderByDescending(p => p.CondicionFiscal?.ToString()).ToList();
                    break;
                case "Nombre":
                    lista = ordenProveedorAscendente ? lista.OrderBy(p => p.Nombre).ToList() : lista.OrderByDescending(p => p.Nombre).ToList();
                    break;
                case "Apellido":
                    lista = ordenProveedorAscendente ? lista.OrderBy(p => p.Apellido).ToList() : lista.OrderByDescending(p => p.Apellido).ToList();
                    break;
                case "DNI":
                    lista = ordenProveedorAscendente ? lista.OrderBy(p => p.DNI).ToList() : lista.OrderByDescending(p => p.DNI).ToList();
                    break;
                case "Telefono":
                    lista = ordenProveedorAscendente ? lista.OrderBy(p => p.Telefono).ToList() : lista.OrderByDescending(p => p.Telefono).ToList();
                    break;
                case "Email":
                    lista = ordenProveedorAscendente ? lista.OrderBy(p => p.Email).ToList() : lista.OrderByDescending(p => p.Email).ToList();
                    break;
                case "Activo":
                    lista = ordenProveedorAscendente ? lista.OrderBy(p => p.Activo).ToList() : lista.OrderByDescending(p => p.Activo).ToList();
                    break;
                default:
                    lista = ordenProveedorAscendente ? lista.OrderBy(p => p.RazonSocial).ToList() : lista.OrderByDescending(p => p.RazonSocial).ToList();
                    break;
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;
        }

        #endregion

        #region "Métodos auxiliares"

        private void RefrescarGrilla()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = gestorProveedor.ListarActivos();
        }

        private void LimpiarControles()
        {
            ucDatosPersonales.Limpiar();
            ucDirecciones.Limpiar();
            ucDatosComerciales1.Limpiar();
            ProveedorSeleccionado = null;
        }

        private void CargarProveedorEnControles(BEProveedor proveedor)
        {
            if (proveedor == null) return;
            ucDatosPersonales.Cargar(proveedor);
            ucDirecciones.Cargar(proveedor.Direccion);
            ucDatosComerciales1.Cargar(proveedor);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        #endregion
    }
}
