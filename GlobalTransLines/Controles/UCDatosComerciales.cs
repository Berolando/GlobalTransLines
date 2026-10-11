using BE.Dominio;
using System.Windows.Forms;


namespace GlobalTransLines.UI.Controles
{
    public partial class UCDatosComerciales : UserControl
    {
        #region "Constructor"

        public UCDatosComerciales()
        {
            InitializeComponent();
            CargarCondicionesFiscales(); 
        }

        #endregion

        #region "Métodos privados"

        private void CargarCondicionesFiscales()
        {
            cmbCondicionFiscal.Items.Clear();

            foreach (var condicion in CondicionFiscal.ObtenerTodos())
                cmbCondicionFiscal.Items.Add(condicion);

            if (cmbCondicionFiscal.Items.Count > 0)
                cmbCondicionFiscal.SelectedIndex = 0;
        }

        #endregion

        #region "Validación"

        // Valida los campos obligatorios del control antes de guardar.
        
        public bool EsValido()
        {
            bool ok = ucCuit.EsValido() && ucRazonSocial.EsValido();

            if (cmbCondicionFiscal.SelectedItem == null)
            {
                MessageBox.Show("Seleccione la condición fiscal.",
                                "Validación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                ok = false;
            }

            return ok;
        }

        #endregion

        #region "Propiedades públicas"

        
        public string Cuit => ucCuit.Texto.Trim();

        
        public string RazonSocial => ucRazonSocial.Texto.Trim();

        
        public CondicionFiscal CondicionFiscal =>
            cmbCondicionFiscal.SelectedItem != null
                ? (CondicionFiscal)cmbCondicionFiscal.SelectedItem
                : CondicionFiscal.ConsumidorFinal;

        #endregion

        #region "Utilitarios"

        
        public void Limpiar()
        {
            ucCuit.Texto = string.Empty;
            ucRazonSocial.Texto = string.Empty;
            cmbCondicionFiscal.SelectedIndex = 0;
        }

        
        public void Cargar(object entidad)
        {
            if (entidad == null) return;

            var tipo = entidad.GetType();

            var propCuit = tipo.GetProperty("Cuit");
            var propRazonSocial = tipo.GetProperty("RazonSocial");
            var propCondicion = tipo.GetProperty("CondicionFiscal");

            if (propCuit != null)
                ucCuit.Texto = propCuit.GetValue(entidad, null)?.ToString() ?? string.Empty;

            if (propRazonSocial != null)
                ucRazonSocial.Texto = propRazonSocial.GetValue(entidad, null)?.ToString() ?? string.Empty;

            if (propCondicion != null)
                cmbCondicionFiscal.SelectedItem = (CondicionFiscal)propCondicion.GetValue(entidad, null);
        }

        #endregion
    }
}
