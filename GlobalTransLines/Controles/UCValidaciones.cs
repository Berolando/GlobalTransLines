using System;
using System.ComponentModel;       // Permite usar [Category] y [Description] en propiedades
using System.Windows.Forms;

namespace GlobalTransLines.UI.Controles
{
    public partial class UCValidaciones : UserControl
    {
        #region "Enum"

        public enum TipoCampoEnum
        {
            Texto,          // Sin restricción específica 
            SoloLetras,     // Acepta únicamente letras y espacios
            Numeros,        // Acepta únicamente dígitos numéricos
            DNI,            // Valida un número de 7 u 8 dígitos
            CUIT,           // Valida CUIT con 11 dígitos, con o sin guiones
            Email,          // Valida formato básico de correo electrónico
            Telefono        // Valida teléfono con dígitos y caracteres habituales
        }

        #endregion

        #region "Propiedades privadas"

        // Campo interno para recordar qué tipo de validación está activo
        private TipoCampoEnum _tipoCampo = TipoCampoEnum.Texto;

        // Mensaje mostrado cuando la validación falla
        private string _mensajeError = "Formato inválido.";

        #endregion

        #region "Propiedades públicas"

        [Category("Validación")]
        [Description("Selecciona el tipo de campo para aplicar validación automática.")]
        public TipoCampoEnum TipoCampo
        {
            get { return _tipoCampo; }
            set
            {
                _tipoCampo = value;
                ConfigurarMensaje();
            }
        }

        // Propiedad para cambiar el texto del label que actúa como título del campo
        [Category("Datos")]
        public string Titulo
        {
            get { return lblTitulo.Text; }
            set { lblTitulo.Text = value; }
        }

        // Propiedad que expone el texto ingresado en la caja de texto interna
        [Category("Datos")]
        public string Texto
        {
            get { return txtEntrada.Text; }
            set { txtEntrada.Text = value; }
        }

        // Permite personalizar el mensaje de error que se muestra en el errorProvider
        [Category("Validación")]
        public string MensajeError
        {
            get { return _mensajeError; }
            set { _mensajeError = value; }
        }

        #endregion

        #region "Inicialización"

        public UCValidaciones()
        {
            InitializeComponent();
            ConfigurarMensaje();
        }

        #endregion

        #region "Lógica interna"

        private void ConfigurarMensaje()
        {
            switch (_tipoCampo)
            {
                case TipoCampoEnum.SoloLetras:
                    _mensajeError = "Error Validacion, Solo se permiten letras y espacios. UC";
                    break;

                case TipoCampoEnum.DNI:
                    _mensajeError = "Error Validacion, El DNI debe tener 7 u 8 dígitos UC.";
                    break;

                case TipoCampoEnum.CUIT:
                    _mensajeError = "Error Validación, el CUIT debe tener 11 dígitos numéricos UC.";
                    break;

                case TipoCampoEnum.Email:
                    _mensajeError = "Error Validacion, El formato del email no es válido UC.";
                    break;

                case TipoCampoEnum.Telefono:
                    _mensajeError = "Error Validacion, El teléfono no tiene un formato válido UC.";
                    break;

                case TipoCampoEnum.Numeros:
                    _mensajeError = "Error Validacion, Solo se permiten números UC.";
                    break;

                default:
                    _mensajeError = "Error Validacion, Formato inválido UC.";
                    break;
            }
        }

        private bool ValidarSoloLetras(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            foreach (char c in valor)
            {
                if (!char.IsLetter(c) && c != ' ')
                    return false;
            }

            return true;
        }

        private bool ValidarSoloNumeros(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            foreach (char c in valor)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        private bool ValidarDNI(string valor)
        {
            if (!ValidarSoloNumeros(valor))
                return false;

            return valor.Length == 7 || valor.Length == 8;
        }

        private bool ValidarCUIT(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            // Permite CUIT con o sin guiones:
            // 20123456789
            // 20-12345678-9
            string limpio = valor.Replace("-", "").Replace(" ", "");

            if (!ValidarSoloNumeros(limpio))
                return false;

            return limpio.Length == 11;
        }

        private bool ValidarEmail(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            int posicionArroba = valor.IndexOf("@");
            int ultimaArroba = valor.LastIndexOf("@");

            // Debe tener una sola arroba y no puede estar al principio
            if (posicionArroba <= 0)
                return false;

            if (posicionArroba != ultimaArroba)
                return false;

            string usuario = valor.Substring(0, posicionArroba);
            string dominio = valor.Substring(posicionArroba + 1);

            if (string.IsNullOrWhiteSpace(usuario))
                return false;

            if (string.IsNullOrWhiteSpace(dominio))
                return false;

            if (!dominio.Contains("."))
                return false;

            if (dominio.StartsWith(".") || dominio.EndsWith("."))
                return false;

            return true;
        }

        private bool ValidarTelefono(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            int cantidadDigitos = 0;

            foreach (char c in valor)
            {
                if (char.IsDigit(c))
                {
                    cantidadDigitos++;
                }
                else
                {
                    bool caracterPermitido =
                        c == '+' ||
                        c == '-' ||
                        c == ' ' ||
                        c == '(' ||
                        c == ')';

                    if (!caracterPermitido)
                        return false;
                }
            }

            return cantidadDigitos >= 7 && cantidadDigitos <= 15;
        }

        #endregion

        #region "Métodos públicos"

        public bool EsValido()
        {
            string valor = txtEntrada.Text.Trim();
            bool valido = true;

            switch (_tipoCampo)
            {
                case TipoCampoEnum.Texto:
                    valido = true;
                    break;

                case TipoCampoEnum.SoloLetras:
                    valido = ValidarSoloLetras(valor);
                    break;

                case TipoCampoEnum.Numeros:
                    valido = ValidarSoloNumeros(valor);
                    break;

                case TipoCampoEnum.DNI:
                    valido = ValidarDNI(valor);
                    break;

                case TipoCampoEnum.CUIT:
                    valido = ValidarCUIT(valor);
                    break;

                case TipoCampoEnum.Email:
                    valido = ValidarEmail(valor);
                    break;

                case TipoCampoEnum.Telefono:
                    valido = ValidarTelefono(valor);
                    break;
            }

            errorProvider1.SetError(txtEntrada, valido ? "" : _mensajeError);
            txtEntrada.BackColor = valido ? System.Drawing.Color.White : System.Drawing.Color.MistyRose;

            return valido;
        }

        private void txtEntrada_Leave(object sender, EventArgs e)
        {
            if (!EsValido())
            {
                MessageBox.Show(_mensajeError,
                                "Validación de campo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region "Eventos"

        private void UCValidaciones_Load(object sender, EventArgs e)
        {

        }

        #endregion
    }
}