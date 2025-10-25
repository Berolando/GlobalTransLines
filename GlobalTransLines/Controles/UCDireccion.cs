using System.Windows.Forms;
using BE.Transversal;
using BE;

namespace GlobalTransLines.UI.Controles
{
    public partial class UCDireccion : UserControl
    {
        
        public UCDireccion()
        {
            InitializeComponent();
        }

        public void Cargar(BEDireccion d)
        {
            if (d == null) return;
            ucCalle.Texto = d.Calle;
            ucNumero.Text = d.Numero;
            ucCiudad.Texto = d.Ciudad;
            ucProvincia.Text = d.Provincia;
            ucPais.Text = d.Pais;
            ucCodigoPostal.Text = d.CodigoPostal;
            ucPuerto.Text = d.Puerto;
        }
        public BEDireccion Obtener()
        {
            var d = new BEDireccion();
            d.Calle = ucCalle.Texto;
            d.Numero = ucNumero.Text;
            d.Ciudad = ucCiudad.Texto;
            d.Provincia = ucProvincia.Text;
            d.Pais  = ucPais.Text;
            d.CodigoPostal = ucCodigoPostal.Text;
            d.Puerto = ucPuerto.Text;
            return d;
        }

        public bool EsValido()
        {
            bool valido = ucCalle.EsValido() &
                ucNumero.EsValido() &
                ucCiudad.EsValido() &
                ucProvincia.EsValido() &
                ucPais.EsValido() &
                ucCodigoPostal.EsValido() &
                ucPuerto.EsValido();
            if( !valido)
            {
                MessageBox.Show("Hay errores en la dirección. Revise los campos marcados.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
