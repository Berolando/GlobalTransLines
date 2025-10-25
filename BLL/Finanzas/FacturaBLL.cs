using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE.Finanzas;
using BE.Enums;
using BLL.Transversal;
using MPP.Finanzas;
using BE;

namespace BLL.Finanzas
{
    public class FacturaBLL : IGestor<BEFactura>
    {
        private FacturaMPP facturaMPP = new FacturaMPP();

        #region "Constructor estático"

        static FacturaBLL()
        {
            ReconstructorBLL.Registrar<BEFactura>(
                id => new FacturaBLL().ListarObjeto(new BEFactura { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEFactura obj)
        {
            try
            {
                // --- Validaciones estructurales ---
                ValidadorBLL.ObjetoNoNulo(obj, "Factura");
                ValidadorBLL.ObjetoNoNulo(obj.Booking, "Booking asociado");

                ValidadorBLL.CampoObligatorio(obj.NumeroFactura, "Número de factura");
                if (obj.Subtotal <= 0)
                    throw new Exception("El subtotal debe ser mayor a cero.");

                if (obj.Impuesto < 0)
                    throw new Exception("El impuesto no puede ser negativo.");

                if (obj.Fecha > DateTime.Now)
                    throw new Exception("La fecha de la factura no puede ser futura.");

                // --- Reglas de negocio ---
                if (obj.Estado == EstadoFacturaEnum.Anulada)
                    throw new Exception("No se puede guardar una factura anulada.");

                // Cálculo total
                obj.Total = obj.Subtotal + obj.Impuesto;

                // --- Persistencia ---
                if (obj.Id == 0)
                    facturaMPP.Alta(obj);
                else
                    facturaMPP.Modificar(obj);

                // Limpiar caché del reconstructor
                ReconstructorBLL.LimpiarCache<BEFactura>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar factura.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEFactura obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Factura");

                if (obj.Estado == EstadoFacturaEnum.Pagada)
                    throw new Exception("No se puede eliminar una factura ya pagada.");

                facturaMPP.Baja(obj);
                ReconstructorBLL.LimpiarCache<BEFactura>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar factura.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEFactura> ListarTodo()
        {
            try
            {
                var lista = facturaMPP.Listar();

                foreach (var f in lista)
                {
                    if (f.Booking != null && f.Booking.Id > 0)
                        f.Booking = ReconstructorBLL.Reconstruir<BEBooking>(f.Booking.Id);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar facturas.", ex);
            }
        }

        public BEFactura ListarObjeto(BEFactura obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Factura");

                var factura = facturaMPP.BuscarPorId(obj.Id);
                if (factura != null && factura.Booking != null && factura.Booking.Id > 0)
                    factura.Booking = ReconstructorBLL.Reconstruir<BEBooking>(factura.Booking.Id);

                return factura;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar factura.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return facturaMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de factura.", ex);
            }
        }

        #endregion
    }
}
