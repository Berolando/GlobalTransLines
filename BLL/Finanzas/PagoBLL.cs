using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE.Finanzas;
using BE.Enums;
using MPP.Finanzas;
using BLL.Transversal;

namespace BLL.Finanzas
{
    public class PagoBLL : IGestor<BEPago>
    {
        private PagoMPP pagoMPP = new PagoMPP();
        private EfectivoMPP efectivoMPP = new EfectivoMPP();
        private TarjetaMPP tarjetaMPP = new TarjetaMPP();
        private TransferenciaMPP transferenciaMPP = new TransferenciaMPP();

        #region "Constructor estático"

        static PagoBLL()
        {
            ReconstructorBLL.Registrar<BEPago>(
                id => new PagoBLL().ListarObjeto(new BEPago { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEPago obj)
        {
            try
            {
                // --- Validaciones básicas ---
                ValidadorBLL.ObjetoNoNulo(obj, "Pago");
                ValidadorBLL.ObjetoNoNulo(obj.Factura, "Factura asociada");
                ValidadorBLL.ObjetoNoNulo(obj.MedioDePago, "Medio de pago");

                if (obj.Importe <= 0)
                    throw new Exception("El importe del pago debe ser mayor a cero.");

                // --- Validaciones de negocio según el tipo de medio ---
                if (obj.MedioDePago is BETarjeta tarjeta)
                {
                    if (tarjeta.Vencimiento <= DateTime.Now)
                        throw new Exception("La tarjeta indicada se encuentra vencida.");

                    if (string.IsNullOrWhiteSpace(tarjeta.CVV))
                        throw new Exception("Debe indicar el código CVV de la tarjeta.");

                    if (tarjeta.Id == 0)
                        tarjetaMPP.Alta(tarjeta);
                    else
                        tarjetaMPP.Modificar(tarjeta);
                }
                else if (obj.MedioDePago is BETransferencia transferencia)
                {
                    ValidadorBLL.CampoObligatorio(transferencia.Banco, "Banco");
                    ValidadorBLL.CampoObligatorio(transferencia.CBU, "CBU");

                    if (transferencia.Id == 0)
                        transferenciaMPP.Alta(transferencia);
                    else
                        transferenciaMPP.Modificar(transferencia);
                }
                else if (obj.MedioDePago is BEEfectivo efectivo)
                {
                    if (efectivo.MontoEntregado <= 0)
                        throw new Exception("Debe indicar el monto entregado.");

                    efectivo.Vuelto = (decimal)(efectivo.MontoEntregado - (double)obj.Importe);
                    if (efectivo.Vuelto < 0)
                        throw new Exception("Monto insuficiente para cubrir el pago.");

                    if (efectivo.Id == 0)
                        efectivoMPP.Alta(efectivo);
                    else
                        efectivoMPP.Modificar(efectivo);
                }

                // --- Persistencia del pago principal ---
                if (obj.Id == 0)
                    pagoMPP.Alta(obj);
                else
                    pagoMPP.Modificar(obj);

                ReconstructorBLL.LimpiarCache<BEPago>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar pago.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEPago obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Pago");
                pagoMPP.Baja(obj);
                ReconstructorBLL.LimpiarCache<BEPago>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar pago.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEPago> ListarTodo()
        {
            try
            {
                var lista = pagoMPP.Listar();

                foreach (var p in lista)
                {
                    if (p.Factura != null && p.Factura.Id > 0)
                        p.Factura = ReconstructorBLL.Reconstruir<BEFactura>(p.Factura.Id);

                    if (p.MedioDePago != null && p.MedioDePago.Id > 0)
                    {
                        if (p.MedioDePago is BETarjeta)
                            p.MedioDePago = ReconstructorBLL.Reconstruir<BETarjeta>(p.MedioDePago.Id);
                        else if (p.MedioDePago is BETransferencia)
                            p.MedioDePago = ReconstructorBLL.Reconstruir<BETransferencia>(p.MedioDePago.Id);
                        else if (p.MedioDePago is BEEfectivo)
                            p.MedioDePago = ReconstructorBLL.Reconstruir<BEEfectivo>(p.MedioDePago.Id);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar pagos.", ex);
            }
        }

        public BEPago ListarObjeto(BEPago obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Pago");
                var pago = pagoMPP.BuscarPorId(obj.Id);

                if (pago != null)
                {
                    if (pago.Factura != null && pago.Factura.Id > 0)
                        pago.Factura = ReconstructorBLL.Reconstruir<BEFactura>(pago.Factura.Id);

                    if (pago.MedioDePago != null && pago.MedioDePago.Id > 0)
                    {
                        if (pago.MedioDePago is BETarjeta)
                            pago.MedioDePago = ReconstructorBLL.Reconstruir<BETarjeta>(pago.MedioDePago.Id);
                        else if (pago.MedioDePago is BETransferencia)
                            pago.MedioDePago = ReconstructorBLL.Reconstruir<BETransferencia>(pago.MedioDePago.Id);
                        else if (pago.MedioDePago is BEEfectivo)
                            pago.MedioDePago = ReconstructorBLL.Reconstruir<BEEfectivo>(pago.MedioDePago.Id);
                    }
                }

                return pago;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar pago.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return pagoMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de pago.", ex);
            }
        }

        #endregion
    }
}
