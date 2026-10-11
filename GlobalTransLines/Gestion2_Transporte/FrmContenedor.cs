using BE.Dominio;
using BE.Transporte;
using BLL.Transporte;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Gestion2_Transporte
{
    public partial class FrmContenedor : Form
    {
        #region "Atributos y Constructor"

        private readonly ContenedorBLL gestorContenedor = new ContenedorBLL();
        public BEContenedor contenedorSeleccionado { get; private set; }
        public bool ModoSeleccion { get; set; }
        private string columnaOrdenContenedor = "Codigo";
        private bool ordenContenedorAscendente = true;

        public FrmContenedor()
        {
            InitializeComponent();
            ModoSeleccion = false;
        }

        #endregion

        #region "Eventos del formulario"

        private void FrmContenedor_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text += " | " + AppDomain.CurrentDomain.BaseDirectory;
                ConfigurarBotoneraInferior();
                CargarCombos();
                ConfigurarGrilla(dataGridView1);
                RefrescarGrilla();
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                cmbRestriccion.SelectedIndex = -1;

                // Si se abre en modo selección 
                if (ModoSeleccion)
                {
                    btnAlta.Visible = false;
                    btnModificar.Visible = false;
                    btnEliminar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar formulario de contenedores: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Eventos de botones"

        private void btnAlta_Click(object sender, EventArgs e)
        {
            try
            {
                var tipoSel = comboBox1.SelectedItem as TipoContenedor;
                var estadoSel = comboBox2.SelectedItem as EstadoContenedor;

                if (tipoSel == null)
                {
                    MessageBox.Show("Seleccione un tipo de contenedor.");
                    return;
                }
                var restricSel = cmbRestriccion.SelectedItem as RestriccionContenedor;
                BEContenedor nuevo = new BEContenedor
                {
                    Tipo = tipoSel,
                    Estado = estadoSel ?? EstadoContenedor.Disponible,
                    Restriccion = restricSel ?? RestriccionContenedor.Ninguna,
                    Cargas = new List<BECarga>(),
                    Buque = null
                };

                gestorContenedor.Guardar(nuevo);
                RefrescarGrilla();

                MessageBox.Show("Contenedor agregado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar contenedor: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (contenedorSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un contenedor para modificar.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var tipoSel = comboBox1.SelectedItem as TipoContenedor;
                var estadoSel = comboBox2.SelectedItem as EstadoContenedor;
                var restriccionSel = cmbRestriccion.SelectedItem as RestriccionContenedor;

                //  Actualizar tipo (si se seleccionó)
                if (tipoSel != null)
                    contenedorSeleccionado.Tipo = tipoSel;

                //  Validar y actualizar estado
                if (estadoSel != null)
                {
                    if (estadoSel == EstadoContenedor.Disponible || estadoSel == EstadoContenedor.Mantenimiento)
                    {
                        contenedorSeleccionado.Estado = estadoSel;
                    }
                    else
                    {
                        MessageBox.Show("Solo se permite cambiar el estado a 'Disponible' o 'Mantenimiento'.",
                            "Cambio de estado restringido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Validar y actualizar restricción
                if (restriccionSel != null)
                {
                    if (contenedorSeleccionado.Estado == EstadoContenedor.Disponible ||
                        contenedorSeleccionado.Estado == EstadoContenedor.Mantenimiento)
                    {
                        contenedorSeleccionado.Restriccion = restriccionSel;
                    }
                    else
                    {
                        MessageBox.Show("Solo se puede cambiar la restricción cuando el contenedor está Disponible o en Mantenimiento.",
                            "Restricción no editable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                //  Guardar cambios y refrescar
                gestorContenedor.Guardar(contenedorSeleccionado);
                RefrescarGrilla();

                MessageBox.Show("Contenedor modificado correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar contenedor: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (contenedorSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un contenedor para eliminar.");
                    return;
                }

                DialogResult res = MessageBox.Show("¿Confirma la eliminación del contenedor seleccionado?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.No)
                    return;

                gestorContenedor.Eliminar(contenedorSeleccionado);
                RefrescarGrilla();

                MessageBox.Show("Contenedor eliminado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar contenedor: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string filtro = txtBuscar.Text.Trim().ToLower();
                var lista = gestorContenedor.ListarTodo();

                var tipoSel = comboBox1.SelectedItem as TipoContenedor;
                var estadoSel = comboBox2.SelectedItem as EstadoContenedor;

                // Filtros combinados
                if (comboBox1.SelectedIndex > -1 && tipoSel != null)
                    lista = lista.Where(c => c.Tipo.Codigo == tipoSel.Codigo).ToList();

                if (comboBox2.SelectedIndex > -1 && estadoSel != null)
                    lista = lista.Where(c => c.Estado.Codigo == estadoSel.Codigo).ToList();

                if (!string.IsNullOrEmpty(filtro))
                    lista = lista.Where(c => c.Codigo.ToLower().Contains(filtro)).ToList();

                //  Si no hay resultados, avisar y salir
                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show("No se encontraron contenedores que coincidan con el criterio de búsqueda.",
                        "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = null;
                    return;
                }

                // Mostrar resultados en grilla
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = lista;

                //  Colorear filas con contenedores en mantenimiento
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    var c = fila.DataBoundItem as BEContenedor;
                    if (c != null && c.Estado == EstadoContenedor.Mantenimiento)
                        fila.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar contenedores: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }


        #endregion

        #region "Eventos de grilla"

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            contenedorSeleccionado = (BEContenedor)dataGridView1.CurrentRow.DataBoundItem;
            if (contenedorSeleccionado == null)
                return;

            lblNumero.Text = contenedorSeleccionado.Codigo;
            comboBox1.SelectedItem = contenedorSeleccionado.Tipo;
            comboBox2.SelectedItem = contenedorSeleccionado.Estado;
            cmbRestriccion.SelectedItem = RestriccionContenedor.ObtenerPorCodigo(contenedorSeleccionado.Restriccion?.Codigo);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (ModoSeleccion)
                {
                    contenedorSeleccionado = (BEContenedor)dataGridView1.CurrentRow.DataBoundItem;
                    if (contenedorSeleccionado != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar contenedor: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region "Configuración Visual"

        private void ConfigurarBotoneraInferior()
        {
            groupBox2.Resize -= groupBox2_Resize;
            groupBox2.Resize += groupBox2_Resize;
            UbicarBotoneraInferior();
        }

        private void groupBox2_Resize(object sender, EventArgs e)
        {
            UbicarBotoneraInferior();
        }

        private void UbicarBotoneraInferior()
        {
            int margen = 18;
            int separacion = 12;
            int yBoton = 22;

            btnAlta.Location = new System.Drawing.Point(margen, yBoton);
            btnModificar.Location = new System.Drawing.Point(btnAlta.Right + separacion, yBoton);
            btnEliminar.Location = new System.Drawing.Point(btnModificar.Right + separacion, yBoton);

            btnCerrar.Location = new System.Drawing.Point(
                Math.Max(margen, groupBox2.ClientSize.Width - btnCerrar.Width - margen),
                yBoton);

            btnBuscar.Location = new System.Drawing.Point(btnCerrar.Left - btnBuscar.Width - separacion, yBoton);
            txtBuscar.Location = new System.Drawing.Point(btnBuscar.Left - txtBuscar.Width - separacion, yBoton + 3);

            btnCerrar.BringToFront();
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
            grilla.RowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 248, 255);
            grilla.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 250, 240);

            grilla.ColumnHeaderMouseClick -= dataGridView1_ColumnHeaderMouseClick;
            grilla.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;
        }

        #endregion

        #region "Orden de grilla"

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columna = dataGridView1.Columns[e.ColumnIndex].Name;
            ordenContenedorAscendente = columnaOrdenContenedor == columna ? !ordenContenedorAscendente : true;
            columnaOrdenContenedor = columna;
            OrdenarContenedores();
        }

        private void OrdenarContenedores()
        {
            var lista = (dataGridView1.DataSource as IEnumerable<BEContenedor>)?.ToList();
            if (lista == null)
                return;

            // Ordena la lista visible para respetar busquedas y filtros ya aplicados en pantalla.
            switch (columnaOrdenContenedor)
            {
                case "Id":
                    lista = ordenContenedorAscendente ? lista.OrderBy(c => c.Id).ToList() : lista.OrderByDescending(c => c.Id).ToList();
                    break;
                case "Tipo":
                    lista = ordenContenedorAscendente ? lista.OrderBy(c => c.Tipo?.Descripcion).ToList() : lista.OrderByDescending(c => c.Tipo?.Descripcion).ToList();
                    break;
                case "Estado":
                    lista = ordenContenedorAscendente ? lista.OrderBy(c => c.Estado?.Descripcion).ToList() : lista.OrderByDescending(c => c.Estado?.Descripcion).ToList();
                    break;
                case "Restriccion":
                    lista = ordenContenedorAscendente ? lista.OrderBy(c => c.Restriccion?.Descripcion).ToList() : lista.OrderByDescending(c => c.Restriccion?.Descripcion).ToList();
                    break;
                case "Buque":
                    lista = ordenContenedorAscendente ? lista.OrderBy(c => c.Buque?.Nombre).ToList() : lista.OrderByDescending(c => c.Buque?.Nombre).ToList();
                    break;
                case "Activo":
                    lista = ordenContenedorAscendente ? lista.OrderBy(c => c.Activo).ToList() : lista.OrderByDescending(c => c.Activo).ToList();
                    break;
                default:
                    lista = ordenContenedorAscendente ? lista.OrderBy(c => c.Codigo).ToList() : lista.OrderByDescending(c => c.Codigo).ToList();
                    break;
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;
            MarcarContenedoresEnMantenimiento();
        }

        #endregion

        #region "Métodos auxiliares"

        private void RefrescarGrilla()
        {
            var lista = gestorContenedor.ListarTodo();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;
            dataGridView1.AutoResizeColumns();  
            MarcarContenedoresEnMantenimiento();
        }

        private void MarcarContenedoresEnMantenimiento()
        {
            // Marcar visualmente los contenedores en mantenimiento
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                var c = fila.DataBoundItem as BEContenedor;
                if (c != null && c.Estado == EstadoContenedor.Mantenimiento)
                    fila.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            }
        }

        private void CargarCombos()
        {
            comboBox1.DataSource = TipoContenedor.ObtenerTodos().ToList();
            comboBox1.DisplayMember = "Descripcion";
            comboBox1.ValueMember = "Codigo";
            
            cmbRestriccion.DataSource = RestriccionContenedor.ObtenerTodos().ToList();
            cmbRestriccion.DisplayMember = "Descripcion";
            cmbRestriccion.ValueMember = "Codigo";
            
            //  Solo estados modificables manualmente
            comboBox2.DataSource = EstadoContenedor.ObtenerTodos()
                .Where(e => e == EstadoContenedor.Disponible || e == EstadoContenedor.Mantenimiento)
                .ToList();
            comboBox2.DisplayMember = "Descripcion";
            comboBox2.ValueMember = "Codigo";
           
        }

        #endregion
    }
}


