using BE;
using System;
using System.Windows.Forms;

namespace GlobalTransLines.UI.Controles
{
    public partial class UcDatosPersonales : UserControl
    {
        private UCDireccion ucDireccion;
        public UcDatosPersonales()
        {
            InitializeComponent();
            InicializarDireccion();
        }
        #region "Inicialización"
        private void InicializarDireccion()
        {
            ucDireccion = new UCDireccion();
            ucDireccion.Dock = DockStyle.Fill;
            panelDireccion.Controls.Add(ucDireccion);
        }
        #endregion
        #region "API pública"

        // Carga los datos de una persona existente en los controles
        public void Cargar(BEPersona persona)
        {
            if (persona == null) return;

            ucNombre.Texto = persona.Nombre;
            ucApellido.Texto = persona.Apellido;
            ucDni.Texto = persona.DNI;
            ucTelefono.Texto = persona.Telefono;
            ucEmail.Texto = persona.Email;

            // Validar rango antes de asignar la fecha
            if (persona.FechaNacimiento >= dtpFechaNacimiento.MinDate &&
                persona.FechaNacimiento <= dtpFechaNacimiento.MaxDate)
            {
                dtpFechaNacimiento.Value = persona.FechaNacimiento;
            }
            else
            {
                dtpFechaNacimiento.Value = DateTime.Today; 
            }

            ucDireccion.Cargar(persona.Direccion);
        }

        // Valida todos los campos del control
        public bool EsValido()
        {
            bool valido = ucNombre.EsValido() &&
                          ucApellido.EsValido() &&
                          ucDni.EsValido() &&
                          ucTelefono.EsValido() &&
                          ucEmail.EsValido() &&
                          ucDireccion.EsValido();

            if (!valido)
            {
                MessageBox.Show("Existen campos con formato inválido.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return valido;
        }

        // Limpia los datos
        public void Limpiar()
        {
            ucNombre.Texto = string.Empty;
            ucApellido.Texto = string.Empty;
            ucDni.Texto = string.Empty;
            ucTelefono.Texto = string.Empty;
            ucEmail.Texto = string.Empty;
            dtpFechaNacimiento.Value = DateTime.Today;
            ucDireccion.Limpiar();
        }

        #endregion
        #region "Propiedades públicas"
        public string Nombre => ucNombre.Texto.Trim();
        public string Apellido => ucApellido.Texto.Trim();
        public string DNI => ucDni.Texto.Trim();
        public string Telefono => ucTelefono.Texto.Trim();
        public string Email => ucEmail.Texto.Trim();
        public DateTime FechaNacimiento => dtpFechaNacimiento.Value;
        public BEDireccion Direccion => ucDireccion.Obtener();
        #endregion
    }
}
