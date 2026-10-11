using BE.Personas;
using BE.ServiciosSubcontratados;
using BLL.Personas;
using BLL.ServiciosSubcontratados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Gestion4_Proveedores
{
    public partial class FrmServicioSubcontratado : Form
    {
        #region "Atributos y Constructor"

        private readonly ServicioSubcontratadoBLL gestorServicio = new ServicioSubcontratadoBLL();
        public BEServicioSubcontratado servicioSeleccionado;
        private BEProveedor proveedorSeleccionado;
        public bool ModoSeleccion { get; set; }
        private string columnaOrdenServicio = "Id";
        private bool ordenServicioAscendente;

        public FrmServicioSubcontratado()
        {
            InitializeComponent();
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmServicioSubcontratado_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarGrilla(grdServicios);
                RefrescarGrilla();
                LimpiarControles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar formulario de servicios: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = textBox2.Text.Trim();
                string descripcion = textBox1.Text.Trim();
                decimal costo;

                if (!decimal.TryParse(textBox3.Text, out costo))
                    throw new Exception("Debe ingresar un costo válido.");

                if (proveedorSeleccionado == null)
                    throw new Exception("Debe seleccionar un proveedor.");

                BEServicioSubcontratado servicio = new BEServicioSubcontratado
                {
                    Id = servicioSeleccionado?.Id ?? 0,
                    Nombre = nombre,
                    Descripcion = descripcion,
                    Costo = costo,
                    Proveedor = proveedorSeleccionado
                };

                gestorServicio.Guardar(servicio);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Servicio guardado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar servicio: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (servicioSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un servicio para modificar.");
                    return;
                }

                textBox2.Text = servicioSeleccionado.Nombre;
                textBox1.Text = servicioSeleccionado.Descripcion;
                textBox3.Text = servicioSeleccionado.Costo.ToString();
                proveedorSeleccionado = servicioSeleccionado.Proveedor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar servicio: " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (servicioSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un servicio para eliminar.");
                    return;
                }

                DialogResult r = MessageBox.Show("¿Confirma eliminar el servicio seleccionado?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.No) return;

                gestorServicio.Eliminar(servicioSeleccionado);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Servicio eliminado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar servicio: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            RefrescarGrilla(txtBuscar.Text.Trim().ToLower());
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            using (var frm = new FrmProveedor { ModoSeleccion = true })
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    proveedorSeleccionado = frm.ProveedorSeleccionado;
                    MessageBox.Show("Proveedor seleccionado: " + proveedorSeleccionado.RazonSocial);
                }
            }
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

            grilla.ColumnHeaderMouseClick -= grdServicios_ColumnHeaderMouseClick;
            grilla.ColumnHeaderMouseClick += grdServicios_ColumnHeaderMouseClick;

            grilla.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);
        }

        #endregion

        #region "Eventos de grilla"

        private void grdServicios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                servicioSeleccionado = (BEServicioSubcontratado)grdServicios.CurrentRow.DataBoundItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar servicio: " + ex.Message);
            }
        }

        private void grdServicios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                if (ModoSeleccion)
                {
                    servicioSeleccionado = (BEServicioSubcontratado)grdServicios.CurrentRow.DataBoundItem;
                    if (servicioSeleccionado != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    grdServicios_CellClick(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar servicio: " + ex.Message);
            }
        }

        private void grdServicios_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = grdServicios.Columns[e.ColumnIndex].Name;

            // Orden manual: la grilla usa una lista de servicios reconstruida desde XML.
            if (columnaOrdenServicio == columna)
                ordenServicioAscendente = !ordenServicioAscendente;
            else
            {
                columnaOrdenServicio = columna;
                ordenServicioAscendente = true;
            }

            RefrescarGrilla(txtBuscar.Text.Trim().ToLower());
        }

        #endregion

        #region "Métodos auxiliares"

        private void RefrescarGrilla()
        {
            RefrescarGrilla(string.Empty);
        }

        private void RefrescarGrilla(string filtro)
        {
            var servicios = gestorServicio.ListarTodo();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                servicios = servicios
                    .Where(s =>
                        (s.Nombre ?? "").ToLower().Contains(filtro) ||
                        (s.Descripcion ?? "").ToLower().Contains(filtro) ||
                        (s.Proveedor != null && (s.Proveedor.RazonSocial ?? "").ToLower().Contains(filtro)))
                    .ToList();
            }

            grdServicios.DataSource = null;
            grdServicios.DataSource = OrdenarServicios(servicios);
        }

        private List<BEServicioSubcontratado> OrdenarServicios(List<BEServicioSubcontratado> servicios)
        {
            Func<BEServicioSubcontratado, object> criterio;

            switch (columnaOrdenServicio)
            {
                case "Nombre":
                    criterio = s => s.Nombre ?? string.Empty;
                    break;
                case "Descripcion":
                    criterio = s => s.Descripcion ?? string.Empty;
                    break;
                case "Costo":
                    criterio = s => s.Costo;
                    break;
                case "Proveedor":
                    criterio = s => s.Proveedor != null ? s.Proveedor.RazonSocial ?? string.Empty : string.Empty;
                    break;
                case "Activo":
                    criterio = s => s.Activo;
                    break;
                default:
                    criterio = s => s.Id;
                    break;
            }

            return ordenServicioAscendente
                ? servicios.OrderBy(criterio).ToList()
                : servicios.OrderByDescending(criterio).ToList();
        }

        private void LimpiarControles()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            proveedorSeleccionado = null;
            servicioSeleccionado = null;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        #endregion
    }
}
