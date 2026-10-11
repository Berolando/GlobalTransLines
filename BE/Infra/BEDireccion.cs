namespace BE.Infra
{
    public class BEDireccion : BEEntidad
    {
        #region Propiedades
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public string CodigoPostal { get; set; }
        public string Puerto { get; set; }
        public string Tipo { get; set; }
        public bool EsPuerto
        {
            get
            {
                return string.Equals(Tipo, "PUERTO", System.StringComparison.OrdinalIgnoreCase) ||
                       (string.IsNullOrWhiteSpace(Tipo) && !string.IsNullOrWhiteSpace(Puerto));
            }
        }
        #endregion

        #region Constructores
        public BEDireccion() { }

        public BEDireccion(string calle, string numero, string ciudad, string provincia, string pais, string codigoPostal, string puerto = null)
        {
            Calle = calle;
            Numero = numero;
            Ciudad = ciudad;
            Provincia = provincia;
            Pais = pais;
            CodigoPostal = codigoPostal;
            Puerto = puerto;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            // Si representa un puerto, se muestra como ubicación logística; si no, como domicilio.
            if (EsPuerto)
            {
                string ubicacion = string.Join(", ",
                    System.Linq.Enumerable.Where(
                        new[] { Puerto, Ciudad, Pais },
                        valor => !string.IsNullOrWhiteSpace(valor)));

                return ubicacion;
            }
            return $"{Calle} {Numero}, {Ciudad}, {Provincia}, {Pais}";
        }
        #endregion
    }
}
