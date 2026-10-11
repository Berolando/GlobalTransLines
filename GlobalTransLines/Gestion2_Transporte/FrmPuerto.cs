using BE.Infra;
using BLL.Infra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Gestion2_Transporte
{
    public class FrmPuerto : Form
    {
        #region "Atributos"

        private readonly DireccionBLL gestorDireccion = new DireccionBLL();
        private BEDireccion puertoSeleccionado;
        private TextBox txtPuerto;
        private TextBox txtPais;
        private TextBox txtCiudad;
        private TextBox txtBuscar;
        private DataGridView grdPuertos;
        private Button btnGuardar;
        private Button btnModificar;
        private Button btnLimpiar;
        private Button btnCerrar;
        private string columnaOrdenPuerto = "Pais";
        private bool ordenPuertoAscendente = true;

        #endregion

        #region "Constructor"

        public FrmPuerto()
        {
            ConfigurarFormulario();
            ConfigurarControles();
            ConfigurarGrilla();
            RefrescarGrilla();
        }

        #endregion

        #region "Configuración Visual"

        private void ConfigurarFormulario()
        {
            Text = "Catálogo de puertos";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(820, 520);
            MinimumSize = new Size(760, 460);
        }

        private void ConfigurarControles()
        {
            var grpDatos = new GroupBox
            {
                Text = "Datos del puerto",
                Location = new Point(12, 12),
                Size = new Size(780, 120),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblPuerto = new Label { Text = "Puerto", Location = new Point(18, 32), AutoSize = true };
            var lblPais = new Label { Text = "País", Location = new Point(285, 32), AutoSize = true };
            var lblCiudad = new Label { Text = "Ciudad", Location = new Point(530, 32), AutoSize = true };

            txtPuerto = new TextBox { Location = new Point(18, 55), Size = new Size(230, 22) };
            txtPais = new TextBox { Location = new Point(285, 55), Size = new Size(200, 22) };
            txtCiudad = new TextBox { Location = new Point(530, 55), Size = new Size(220, 22), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };

            grpDatos.Controls.Add(lblPuerto);
            grpDatos.Controls.Add(lblPais);
            grpDatos.Controls.Add(lblCiudad);
            grpDatos.Controls.Add(txtPuerto);
            grpDatos.Controls.Add(txtPais);
            grpDatos.Controls.Add(txtCiudad);

            txtBuscar = new TextBox
            {
                Location = new Point(12, 145),
                Size = new Size(220, 22)
            };
            txtBuscar.TextChanged += txtBuscar_TextChanged;

            var lblBuscar = new Label
            {
                Text = "Buscar",
                Location = new Point(12, 125),
                AutoSize = true
            };

            btnGuardar = new Button { Text = "Guardar", Location = new Point(260, 140), Size = new Size(115, 30) };
            btnModificar = new Button { Text = "Modificar", Location = new Point(385, 140), Size = new Size(115, 30) };
            btnLimpiar = new Button { Text = "Limpiar", Location = new Point(510, 140), Size = new Size(115, 30) };
            btnCerrar = new Button { Text = "Cerrar", Location = new Point(635, 140), Size = new Size(115, 30), Anchor = AnchorStyles.Top | AnchorStyles.Right };

            btnGuardar.Click += btnGuardar_Click;
            btnModificar.Click += btnModificar_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnCerrar.Click += btnCerrar_Click;

            grdPuertos = new DataGridView
            {
                Location = new Point(12, 185),
                Size = new Size(780, 285),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            grdPuertos.CellClick += grdPuertos_CellClick;
            grdPuertos.ColumnHeaderMouseClick += grdPuertos_ColumnHeaderMouseClick;

            Controls.Add(grpDatos);
            Controls.Add(lblBuscar);
            Controls.Add(txtBuscar);
            Controls.Add(btnGuardar);
            Controls.Add(btnModificar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnCerrar);
            Controls.Add(grdPuertos);
        }

        private void ConfigurarGrilla()
        {
            grdPuertos.AutoGenerateColumns = false;
            grdPuertos.Columns.Clear();
            grdPuertos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdPuertos.MultiSelect = false;
            grdPuertos.ReadOnly = true;
            grdPuertos.RowHeadersVisible = false;
            grdPuertos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grdPuertos.AllowUserToAddRows = false;
            grdPuertos.AllowUserToDeleteRows = false;

            grdPuertos.Columns.Add("Puerto", "Puerto");
            grdPuertos.Columns.Add("Pais", "País");
            grdPuertos.Columns.Add("Ciudad", "Ciudad");
            grdPuertos.Columns.Add("Id", "Id");

            foreach (DataGridViewColumn columna in grdPuertos.Columns)
                columna.SortMode = DataGridViewColumnSortMode.Automatic;

            grdPuertos.RowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
            grdPuertos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 250, 240);
        }

        #endregion

        #region "Eventos"

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var puerto = MapearDesdeControles();
                gestorDireccion.GuardarPuerto(puerto);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Puerto registrado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar puerto: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (puertoSeleccionado == null)
                {
                    MessageBox.Show("Seleccione un puerto para modificar.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var puerto = MapearDesdeControles();
                puerto.Id = puertoSeleccionado.Id;
                gestorDireccion.GuardarPuerto(puerto);
                RefrescarGrilla();
                LimpiarControles();

                MessageBox.Show("Puerto modificado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar puerto: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            RefrescarGrilla(txtBuscar.Text.Trim());
        }

        private void grdPuertos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            puertoSeleccionado = grdPuertos.Rows[e.RowIndex].Tag as BEDireccion;
            if (puertoSeleccionado == null)
                return;

            txtPuerto.Text = puertoSeleccionado.Puerto;
            txtPais.Text = puertoSeleccionado.Pais;
            txtCiudad.Text = puertoSeleccionado.Ciudad;
        }

        private void grdPuertos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            string columna = grdPuertos.Columns[e.ColumnIndex].Name;

            // Orden manual: la grilla se carga con Rows.Add para conservar el puerto completo en el Tag.
            if (columnaOrdenPuerto == columna)
                ordenPuertoAscendente = !ordenPuertoAscendente;
            else
            {
                columnaOrdenPuerto = columna;
                ordenPuertoAscendente = true;
            }

            RefrescarGrilla(txtBuscar.Text.Trim());
        }

        #endregion

        #region "Métodos auxiliares"

        private void RefrescarGrilla(string filtro = "")
        {
            var puertos = gestorDireccion.ListarPuertos();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                filtro = filtro.ToLower();
                puertos = puertos
                    .Where(p => (p.Puerto ?? "").ToLower().Contains(filtro)
                             || (p.Pais ?? "").ToLower().Contains(filtro)
                             || (p.Ciudad ?? "").ToLower().Contains(filtro))
                    .ToList();
            }

            grdPuertos.Rows.Clear();
            foreach (var puerto in OrdenarPuertos(puertos))
            {
                int fila = grdPuertos.Rows.Add(
                    puerto.Puerto,
                    puerto.Pais,
                    puerto.Ciudad,
                    puerto.Id);

                grdPuertos.Rows[fila].Tag = puerto;
            }
        }

        private List<BEDireccion> OrdenarPuertos(List<BEDireccion> puertos)
        {
            Func<BEDireccion, object> criterio;

            switch (columnaOrdenPuerto)
            {
                case "Puerto":
                    criterio = p => p.Puerto ?? string.Empty;
                    break;
                case "Pais":
                    criterio = p => p.Pais ?? string.Empty;
                    break;
                case "Ciudad":
                    criterio = p => p.Ciudad ?? string.Empty;
                    break;
                case "Id":
                    criterio = p => p.Id;
                    break;
                default:
                    criterio = p => p.Pais ?? string.Empty;
                    break;
            }

            return ordenPuertoAscendente
                ? puertos.OrderBy(criterio).ThenBy(p => p.Ciudad).ThenBy(p => p.Puerto).ToList()
                : puertos.OrderByDescending(criterio).ThenBy(p => p.Ciudad).ThenBy(p => p.Puerto).ToList();
        }

        private BEDireccion MapearDesdeControles()
        {
            return new BEDireccion
            {
                Puerto = txtPuerto.Text.Trim(),
                Pais = txtPais.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim(),
                Tipo = "PUERTO"
            };
        }

        private void LimpiarControles()
        {
            txtPuerto.Clear();
            txtPais.Clear();
            txtCiudad.Clear();
            puertoSeleccionado = null;
            txtPuerto.Focus();
        }

        #endregion
    }
}


