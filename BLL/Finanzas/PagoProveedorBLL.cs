using Abstraccion;
using BE.Dominio;
using BE.Finanzas;
using BLL.Infra;
using MPP.Finanzas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Finanzas
{
    public class PagoProveedorBLL : IGestor<BEPagoProveedor>
    {
        private PagoProveedorMPP pagoProveedorMPP = new PagoProveedorMPP();
        private EfectivoMPP efectivoMPP = new EfectivoMPP();
        private TarjetaMPP tarjetaMPP = new TarjetaMPP();
        private TransferenciaMPP transferenciaMPP = new TransferenciaMPP();

        #region "Constructor estático"

        static PagoProveedorBLL()
        {
            ReconstructorBLL.Registrar<BEPagoProveedor>(
                id => new PagoProveedorBLL().ListarObjeto(new BEPagoProveedor { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEPagoProveedor obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Pago a proveedor");
                ValidadorBLL.ObjetoNoNulo(obj.OrdenDeCompra, "Orden de compra asociada");
                ValidadorBLL.ObjetoNoNulo(obj.MedioDePago, "Medio de pago");
                ValidadorBLL.CampoObligatorio(obj.NumeroFacturaProveedor, "Número de factura del proveedor");

                if (obj.Importe <= 0)
                    throw new Exception("El importe del pago debe ser mayor a cero.");

                if (obj.ImporteFacturaProveedor <= 0)
                    throw new Exception("El importe de la factura del proveedor debe ser mayor a cero.");

                if (obj.FechaFacturaProveedor > DateTime.Now)
                    throw new Exception("La fecha de factura del proveedor no puede ser futura.");

                if (obj.FechaPago > DateTime.Now)
                    throw new Exception("La fecha de pago no puede ser futura.");

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
                        throw new Exception("Monto insuficiente para cubrir el pago al proveedor.");

                    if (efectivo.Id == 0)
                        efectivoMPP.Alta(efectivo);
                    else
                        efectivoMPP.Modificar(efectivo);
                }

                if (obj.Id == 0)
                    pagoProveedorMPP.Alta(obj);
                else
                    pagoProveedorMPP.Modificar(obj);

                ReconstructorBLL.LimpiarCache<BEPagoProveedor>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar pago a proveedor.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEPagoProveedor obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Pago a proveedor");
                pagoProveedorMPP.Baja(obj);
                ReconstructorBLL.LimpiarCache<BEPagoProveedor>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar pago a proveedor.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEPagoProveedor> ListarTodo()
        {
            try
            {
                var lista = pagoProveedorMPP.Listar();

                foreach (var p in lista)
                {
                    if (p.OrdenDeCompra != null && p.OrdenDeCompra.Id > 0)
                        p.OrdenDeCompra = ReconstructorBLL.Reconstruir<BEOrdenDeCompra>(p.OrdenDeCompra.Id);

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
                throw new Exception("Error al listar pagos a proveedores.", ex);
            }
        }

        public BEPagoProveedor ListarObjeto(BEPagoProveedor obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Pago a proveedor");

                var pago = pagoProveedorMPP.BuscarPorId(obj.Id);

                if (pago != null)
                {
                    if (pago.OrdenDeCompra != null && pago.OrdenDeCompra.Id > 0)
                        pago.OrdenDeCompra = ReconstructorBLL.Reconstruir<BEOrdenDeCompra>(pago.OrdenDeCompra.Id);

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
                throw new Exception("Error al buscar pago a proveedor.", ex);
            }
        }

        public List<BEOrdenDeCompra> ListarOrdenesRecibidas()
        {
            try
            {
                return new OrdenCompraBLL().ListarTodo()
                    .Where(oc => oc.Estado != null &&
                                 oc.Estado.Codigo == EstadoOrdenCompra.Recibida.Codigo)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar órdenes de compra recibidas.", ex);
            }
        }

        #endregion

        #region "Flujo de compras"

        public void RegistrarPago(BEPagoProveedor pago)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(pago, "Pago a proveedor");
                ValidadorBLL.ObjetoNoNulo(pago.OrdenDeCompra, "Orden de compra asociada");

                var ordenBLL = new OrdenCompraBLL();
                var orden = ordenBLL.ListarObjeto(new BEOrdenDeCompra { Id = pago.OrdenDeCompra.Id });

                if (orden == null)
                    throw new Exception("No se encontró la orden de compra especificada.");

                if (orden.Estado == null || orden.Estado.Codigo != EstadoOrdenCompra.Recibida.Codigo)
                    throw new Exception("Solo se puede registrar el pago de una orden recibida.");

                // Se persiste la relación por Id para mantener el mismo criterio usado en los XML.
                pago.OrdenDeCompra = new BEOrdenDeCompra { Id = orden.Id };
                pago.Estado = EstadoPago.Pagado;

                Guardar(pago);
                // El pago cierra el circuito de compras: la OC pasa de REC a PAG.
                ordenBLL.MarcarPagada(orden);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el pago a proveedor.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return pagoProveedorMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de pago a proveedor.", ex);
            }
        }

        #endregion
    }
}
