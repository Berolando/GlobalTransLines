using BE.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Transversal;

namespace BE
{
    public class BECarga : BEEntidad
    {
        #region Propiedades
        public string Descripcion { get; set; }
        public TipoCargaEnum Tipo { get; set; }
        public decimal Peso { get; set; }      // en toneladas o kg
        public decimal Volumen { get; set; }   // en m3
        public BEDireccion Origen { get; set; }
        public BEDireccion Destino { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaEntregaEstimada { get; set; }
        #endregion

        #region Constructores
        public BECarga() { }

        public BECarga(string descripcion, TipoCargaEnum tipo, decimal peso, decimal volumen,
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

        #region Métodos
        public override string ToString()
        {
            return $"Carga {Descripcion} ({Tipo}) - {Peso} tn / {Volumen} m3 - {Origen?.Ciudad} → {Destino?.Ciudad}";
        }
        #endregion
    }
}
