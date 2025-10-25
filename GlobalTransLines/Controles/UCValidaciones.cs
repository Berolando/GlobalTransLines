using System;
using System.ComponentModel;       // Permite usar [Category] y [Description] en propiedades
using System.Text.RegularExpressions; // Usamos Regex para validar formatos
using System.Windows.Forms;

namespace GlobalTransLines.UI.Controles
{
    public partial class UCValidaciones : UserControl
    {
        #region "Enum"
        // Define los distintos tipos de campo que este control puede validar automáticamente.
        
        public enum TipoCampoEnum
        {
            Texto,          // Sin restricción específica 
            SoloLetras,     // Acepta únicamente letras y espacios
            DNI,            // Valida un número de 7 u 8 dígitos
            CUIT,           // Valida el formato NN-NNNNNNNN-N
            Email,          // Valida el formato típico de correo electrónico
            Telefono        // Valida entre 7 y 15 dígitos, con prefijo opcional "+"
        }
        #endregion

        #region "Propiedades privadas"
        // Campo interno para recordar qué tipo de validación está activo
        private TipoCampoEnum _tipoCampo = TipoCampoEnum.Texto;

        // Guarda la expresión regular que se usará en la validación
        private string _regexActual = string.Empty;

        // Mensaje mostrado cuando la validación falla
        private string _mensajeError = "Formato inválido.";
        #endregion

        #region "Propiedades públicas"

        // Propiedad visible en el diseñador para elegir el tipo de campo (Texto, DNI, CUIT, etc.)
        [Category("Validación")]
        [Description("Selecciona el tipo de campo para aplicar validación automática.")]
        public TipoCampoEnum TipoCampo
        {
            get { return _tipoCampo; }
            set
            {
                _tipoCampo = value;
                ConfigurarRegex(); // Actualiza el patrón y mensaje según el tipo elegido
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
        // Constructor: inicializa los componentes del control
        public UCValidaciones()
        {
            InitializeComponent();
        }
        #endregion

        #region "Lógica interna"
        // Asigna la expresión regular y el mensaje de error según el tipo de campo seleccionado.
        // Esta lógica se ejecuta automáticamente cada vez que se cambia la propiedad TipoCampo.
        private void ConfigurarRegex()
        {
            switch (_tipoCampo)
            {
                case TipoCampoEnum.SoloLetras:
                    // Solo letras (mayúsculas, minúsculas, tildes y Ñ)
                    _regexActual = "^[A-Za-zÁÉÍÓÚáéíóúñÑ\\s]+$";
                    _mensajeError = "Solo se permiten letras.";
                    break;

                case TipoCampoEnum.DNI:
                    // Entre 7 y 8 dígitos numéricos
                    _regexActual = "^\\d{7,8}$";
                    _mensajeError = "El DNI debe tener 7 u 8 dígitos.";
                    break;

                case TipoCampoEnum.CUIT:
                    // Formato estándar NN-NNNNNNNN-N
                    _regexActual = "^\\d{2}-\\d{8}-\\d{1}$";
                    _mensajeError = "El CUIT debe tener el formato NN-NNNNNNNN-N.";
                    break;

                case TipoCampoEnum.Email:
                    // Formato de correo electrónico simple
                    _regexActual = "^[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,4}$";
                    _mensajeError = "El formato del email no es válido.";
                    break;

                case TipoCampoEnum.Telefono:
                    // Números con entre 7 y 15 dígitos, opcionalmente con "+"
                    _regexActual = "^\\+?\\d{7,15}$";
                    _mensajeError = "El teléfono no tiene un formato válido.";
                    break;

                default:
                    // Texto libre (no se valida con Regex)
                    _regexActual = string.Empty;
                    _mensajeError = "Formato inválido.";
                    break;
            }
        }
        #endregion

        #region "Métodos públicos"
        // Valida el texto actual según el patrón configurado.
        // Retorna true si el valor cumple con la expresión regular, false en caso contrario.
        // Si el tipo es "Texto" (sin regex), siempre devuelve true.
        public bool EsValido()
        {
            // Si no hay regex definida, no se realiza validación.
            if (string.IsNullOrWhiteSpace(_regexActual))
                return true;

            // Evalúa si el texto cumple con el patrón.
            bool valido = Regex.IsMatch(txtEntrada.Text, _regexActual);

            // Muestra u oculta el ícono de error en el control.
            errorProvider1.SetError(txtEntrada, valido ? "" : _mensajeError);
            txtEntrada.BackColor = valido ? System.Drawing.Color.White : System.Drawing.Color.MistyRose;

            return valido;
        }
        private void txtEntrada_Leave(object sender, EventArgs e)
        {
            if (!EsValido())
            {
                MessageBox.Show(_mensajeError, "Validación de campo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
        }
        #endregion

        #region "Eventos"
        private void UCValidaciones_Load(object sender, EventArgs e)
        {
            // Evento de carga del control (opcional)
            // Podrías usarlo para asignar valores por defecto si lo necesitás.
        }
        #endregion
    }
}
