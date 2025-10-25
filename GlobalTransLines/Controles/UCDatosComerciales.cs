using System;
using System.Windows.Forms;
using BE.Enums; 

namespace GlobalTransLines.UI.Controles
{
    public partial class UCDatosComerciales : UserControl
    {
        #region "Constructor"

        public UCDatosComerciales()
        {
            InitializeComponent();
            CargarCondicionesFiscales(); // Al iniciar, llena el combo con los valores del enum
        }

        #endregion

        #region "Métodos privados"

        // Carga las opciones del Enum CondicionFiscalEnum en el ComboBox.
        // Esto permite mantener la lista actualizada sin escribir los valores a mano.
        private void CargarCondicionesFiscales()
        {
            cmbCondicionFiscal.Items.Clear();

            foreach (var v in Enum.GetValues(typeof(CondicionFiscalEnum)))
                cmbCondicionFiscal.Items.Add(v);

            if (cmbCondicionFiscal.Items.Count > 0)
                cmbCondicionFiscal.SelectedIndex = 0; // Selecciona el primer valor por defecto
        }

        #endregion

        #region "Validación"

        // Valida los campos obligatorios del control antes de guardar.
        // Este método se llama desde el formulario padre (ej. FmrCliente o FmrProveedor)
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

        // Devuelve el texto del control ucCuit sin espacios extra.
        public string Cuit => ucCuit.Texto.Trim();

        // Devuelve el texto del control ucRazonSocial sin espacios extra.
        public string RazonSocial => ucRazonSocial.Texto.Trim();

        // Devuelve el valor seleccionado del combo como Enum (no como string).
        // Si no hay selección válida, retorna ConsumidorFinal por defecto.
        public CondicionFiscalEnum CondicionFiscal =>
            cmbCondicionFiscal.SelectedItem != null
                ? (CondicionFiscalEnum)cmbCondicionFiscal.SelectedItem
                : CondicionFiscalEnum.ConsumidorFinal;

        #endregion

        #region "Utilitarios"

        // Limpia todos los campos del control.
        // Se usa en operaciones de "Nuevo" o después de guardar un registro.
        public void Limpiar()
        {
            ucCuit.Texto = string.Empty;
            ucRazonSocial.Texto = string.Empty;
            cmbCondicionFiscal.SelectedIndex = 0;
        }

        //Cargar generico para usar en cliente o proveedor
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
                cmbCondicionFiscal.SelectedItem = (CondicionFiscalEnum)propCondicion.GetValue(entidad, null);
        }

        #endregion
    }
}
