using BE.Infra;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Controles
{
    public partial class UCDirecciones : UserControl
    {
        #region "Inicialización"

        public UCDirecciones()
        {
            InitializeComponent();
        }

        #endregion

        #region "API pública"

        public void Cargar(BEDireccion d)
        {
            if (d == null) return;
            ucCalle.Texto = d.Calle;
            ucNumero.Texto = d.Numero;
            ucCiudad.Texto = d.Ciudad;
            ucProvincia.Texto = d.Provincia;
            ucPais.Texto = d.Pais;
            ucCodigoPostal.Texto = d.CodigoPostal;
            ucPuerto.Texto = d.Puerto;
        }

        public BEDireccion Obtener()
        {
            
            bool todosVacios =
                string.IsNullOrWhiteSpace(ucCalle.Texto) &&
                string.IsNullOrWhiteSpace(ucNumero.Texto) &&
                string.IsNullOrWhiteSpace(ucCiudad.Texto) &&
                string.IsNullOrWhiteSpace(ucProvincia.Texto) &&
                string.IsNullOrWhiteSpace(ucPais.Texto) &&
                string.IsNullOrWhiteSpace(ucCodigoPostal.Texto) &&
                string.IsNullOrWhiteSpace(ucPuerto.Texto);

            if (todosVacios)
                return null;

            //  Si solo hay puerto, devolvemos dirección con solo ese dato
            bool soloPuerto =
                !string.IsNullOrWhiteSpace(ucPuerto.Texto) &&
                string.IsNullOrWhiteSpace(ucCalle.Texto) &&
                string.IsNullOrWhiteSpace(ucNumero.Texto) &&
                string.IsNullOrWhiteSpace(ucCiudad.Texto) &&
                string.IsNullOrWhiteSpace(ucProvincia.Texto) &&
                string.IsNullOrWhiteSpace(ucPais.Texto) &&
                string.IsNullOrWhiteSpace(ucCodigoPostal.Texto);

            if (soloPuerto)
                return new BEDireccion { Puerto = ucPuerto.Texto?.Trim() };

            //  Dirección completa
            var d = new BEDireccion();
            d.Calle = ucCalle.Texto?.Trim();
            d.Numero = ucNumero.Texto?.Trim();
            d.Ciudad = ucCiudad.Texto?.Trim();
            d.Provincia = ucProvincia.Texto?.Trim();
            d.Pais = ucPais.Texto?.Trim();
            d.CodigoPostal = ucCodigoPostal.Texto?.Trim();
            d.Puerto = ucPuerto.Texto?.Trim();
            return d;
        }

        public bool EsValido()
        {
            bool todosVacios =
                string.IsNullOrWhiteSpace(ucCalle.Texto) &&
                string.IsNullOrWhiteSpace(ucNumero.Texto) &&
                string.IsNullOrWhiteSpace(ucCiudad.Texto) &&
                string.IsNullOrWhiteSpace(ucProvincia.Texto) &&
                string.IsNullOrWhiteSpace(ucPais.Texto) &&
                string.IsNullOrWhiteSpace(ucCodigoPostal.Texto) &&
                string.IsNullOrWhiteSpace(ucPuerto.Texto);

            if (todosVacios)
            {
                MessageBox.Show("Debe completar al menos una dirección o un puerto.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            bool soloPuerto =
                !string.IsNullOrWhiteSpace(ucPuerto.Texto) &&
                string.IsNullOrWhiteSpace(ucCalle.Texto) &&
                string.IsNullOrWhiteSpace(ucNumero.Texto) &&
                string.IsNullOrWhiteSpace(ucCiudad.Texto) &&
                string.IsNullOrWhiteSpace(ucProvincia.Texto) &&
                string.IsNullOrWhiteSpace(ucPais.Texto) &&
                string.IsNullOrWhiteSpace(ucCodigoPostal.Texto);

            if (soloPuerto)
                return true;

            // Valido dirección común.
            // No obligo puerto porque cliente, empleado y proveedor no deberían necesitarlo.
            bool valido = ucCalle.EsValido() &&
                          ucCiudad.EsValido() &&
                          ucPais.EsValido();

            if (!valido)
            {
                MessageBox.Show("Para una dirección común debe completar Calle, Ciudad y País.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return valido;
        }

        public void Limpiar()
        {
            ucCalle.Texto = string.Empty;
            ucNumero.Texto = string.Empty;
            ucCiudad.Texto = string.Empty;
            ucProvincia.Texto = string.Empty;
            ucPais.Texto = string.Empty;
            ucCodigoPostal.Texto = string.Empty;
            ucPuerto.Texto = string.Empty;
        }

        #endregion

        #region "Propiedades públicas"

        public string Calle => ucCalle.Texto.Trim();
        public string Numero => ucNumero.Texto.Trim();
        public string Ciudad => ucCiudad.Texto.Trim();
        public string Provincia => ucProvincia.Texto.Trim();
        public string Pais => ucPais.Texto.Trim();
        public string CodigoPostal => ucCodigoPostal.Texto?.Trim();
        public string Puerto => ucPuerto.Texto.Trim();

        #endregion
    }
}
