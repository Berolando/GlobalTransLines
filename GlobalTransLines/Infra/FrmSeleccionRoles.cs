using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BE.Infra;

namespace GlobalTransLines.UI.Infra
{
    public partial class FrmSeleccionRoles : Form
    {
        #region "Atributos"

        private List<BERol> rolesDisponibles = new List<BERol>();

        #endregion

        #region "Constructor"

        public FrmSeleccionRoles()
        {
            InitializeComponent();
        }

        #endregion

        #region "Carga"

        public void CargarRoles(List<BERol> roles)
        {
            rolesDisponibles = roles ?? new List<BERol>();

            chkRoles.Items.Clear();

            foreach (var r in rolesDisponibles.OrderBy(x => x.Nombre))
            {
                chkRoles.DisplayMember = "Nombre";
                chkRoles.ValueMember = "Id";
                chkRoles.Items.Add(r, false);
            }
        }

        #endregion

        #region "Obtención"

        public List<BERol> ObtenerRolesSeleccionados()
        {
            var seleccionados = new List<BERol>();

            foreach (var item in chkRoles.CheckedItems)
            {
                var rol = item as BERol;
                if (rol != null)
                    seleccionados.Add(rol);
            }

            return seleccionados;
        }

        #endregion

        #region "Eventos botones"

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}
