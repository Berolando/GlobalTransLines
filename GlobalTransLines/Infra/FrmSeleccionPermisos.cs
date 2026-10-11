using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BE.Infra;

namespace GlobalTransLines.UI.Infra
{
    public partial class FrmSeleccionPermisos : Form
    {
        #region "Atributos"

        private List<BEPermiso> permisosDisponibles = new List<BEPermiso>();

        #endregion

        #region "Constructor"

        public FrmSeleccionPermisos()
        {
            InitializeComponent();
        }

        #endregion

        #region "Carga"

        public void CargarPermisos(List<BEPermiso> permisos)
        {
            permisosDisponibles = permisos ?? new List<BEPermiso>();

            chkPermisos.Items.Clear();
            chkPermisos.DisplayMember = "Descripcion";
            chkPermisos.ValueMember = "Id";

            foreach (var permiso in permisosDisponibles.OrderBy(x => x.Descripcion))
            {
                chkPermisos.Items.Add(permiso, false);
            }
        }

        #endregion

        #region "Obtención"

        public List<BEPermiso> ObtenerPermisosSeleccionados()
        {
            var seleccionados = new List<BEPermiso>();

            foreach (var item in chkPermisos.CheckedItems)
            {
                var permiso = item as BEPermiso;
                if (permiso != null)
                    seleccionados.Add(permiso);
            }

            return seleccionados;
        }

        #endregion

        #region "Eventos botones"

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion
    }
}
