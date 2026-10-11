namespace BE.Finanzas
{
    public abstract class BEMedioDePago : BEEntidad
    {
        #region "Propiedades"
        public string Nombre { get; set; }
        #endregion

        #region "Constructores"
        protected BEMedioDePago() { }

        protected BEMedioDePago(string nombre)
        {
            Nombre = nombre;
        }
        #endregion

        #region "Métodos"
        public override string ToString()
        {
            return $"Medio de Pago: {Nombre}";
        }
        #endregion
    }
}
