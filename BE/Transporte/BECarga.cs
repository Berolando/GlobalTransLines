using BE.Dominio;
using BE.Infra;
using System;

namespace BE.Transporte
{
    public class BECarga : BEEntidad
    {
        #region Propiedades

        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public TipoCarga Tipo { get; set; }
        public TipoContenedor TipoContenedor { get; set; }
        public EstadoCarga Estado { get; set; }
        public decimal Peso { get; set; }      // En kilogramos
        public decimal Volumen { get; set; }   // En metros cúbicos
        public BEDireccion Origen { get; set; }
        public BEDireccion PuertoEmbarque { get; set; }
        public BEDireccion Destino { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaEntregaEstimada { get; set; }

        public decimal PesoKg{get { return Peso; } }

        public decimal VolumenM3 {get { return Volumen; } }

        #endregion

        #region Constructores
        public BECarga() { }

        public BECarga(string descripcion, TipoCarga tipo, decimal peso, decimal volumen,
                       BEDireccion origen, BEDireccion destino,
                       DateTime fechaRegistro, DateTime fechaEntregaEstimada)
        {
            Descripcion = descripcion;
            Tipo = tipo;
            Peso = peso;
            Volumen = volumen;
            Origen = origen;
            Destino = destino;
            FechaRegistro = fechaRegistro;
            FechaEntregaEstimada = fechaEntregaEstimada;
        }
        #endregion

        #region "Métodos"
        public override string ToString()
        {
            return $"Carga {Descripcion} - {Tipo} | {Peso} kg | {Volumen} m³";
        }
        #endregion
    }
}

