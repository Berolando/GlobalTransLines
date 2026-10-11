using BE.Finanzas;
using System;

namespace BE.ServiciosSubcontratados
{
    public class BERecepcionServicio : BEEntidad
    {

        #region Propiedades
        public DateTime FechaRecepcion { get; set; }
        public string Observaciones { get; set; }
        public BEServicioSubcontratado Servicio { get; set; }
        public BEOrdenDeCompra OrdenDeCompra { get; set; }
        #endregion

        #region Constructores
        public BERecepcionServicio() { }

        public BERecepcionServicio(DateTime fecha, string observaciones, BEServicioSubcontratado servicio, BEOrdenDeCompra orden)
        {

            FechaRecepcion = fecha;
            Observaciones = observaciones;
            Servicio = servicio;
            OrdenDeCompra = orden;
        }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return $"Recepción {Id} - Servicio: {Servicio?.Nombre} - Fecha: {FechaRecepcion.ToShortDateString()}";
        }
        #endregion
    }
}
