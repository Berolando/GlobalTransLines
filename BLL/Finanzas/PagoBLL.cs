using Abstraccion;
using BE.Finanzas;
using BLL.Infra;
using MPP.Finanzas;
using System;
using System.Collections.Generic;

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
                ValidadorBLL.ObjetoNoNulo(obj, "Pago");
                ValidadorBLL.ObjetoNoNulo(obj.Factura, "Factura asociada");
                ValidadorBLL.ObjetoNoNulo(obj.MedioDePago, "Medio de pago");

                if (obj.Importe <= 0)
                    throw new Exception("El importe del pago debe ser mayor a cero.");

                var facturaBLL = new FacturaBLL();
                var facturaActual = facturaBLL.ListarObjeto(new BEFactura { Id = obj.Factura.Id });

                if (facturaActual == null)
                    throw new Exception("No se encontró la factura asociada al pago.");

                if (facturaActual.Estado != null && facturaActual.Estado.Codigo == BE.Dominio.EstadoFactura.Anulada.Codigo)
                    throw new Exception("No se puede registrar un pago sobre una factura anulada.");

                if (facturaActual.Estado != null && facturaActual.Estado.Codigo == BE.Dominio.EstadoFactura.Pagada.Codigo)
                    throw new Exception("La factura seleccionada ya se encuentra pagada.");

                if (obj.Importe != facturaActual.Total)
                    throw new Exception("El importe del pago debe coincidir con el total de la factura.");

                bool yaExistePago = false;

                if (obj.Id == 0)
                {
                    yaExistePago = ListarTodo().Exists(p =>
                        p.Factura != null &&
                        p.Factura.Id == facturaActual.Id &&
                        p.Estado != null &&
                        p.Estado.Codigo != BE.Dominio.EstadoPago.Rechazado.Codigo
                    );
                }

                if (yaExistePago)
                    throw new Exception("La factura seleccionada ya tiene un pago registrado.");

                obj.Factura = facturaActual;

                if (obj.Fecha == default(DateTime))
                    obj.Fecha = DateTime.Now;

                if (obj.FechaPago == default(DateTime))
                    obj.FechaPago = DateTime.Now;

                obj.Estado = BE.Dominio.EstadoPago.Pagado;

                // Cada medio de pago tiene sus propias reglas antes de persistirse.
                if (obj.MedioDePago is BETarjeta tarjeta)
                {
                    ValidadorBLL.CampoObligatorio(tarjeta.Numero, "Número de tarjeta");
                    ValidadorBLL.CampoObligatorio(tarjeta.Titular, "Titular de tarjeta");
                    ValidadorBLL.CampoObligatorio(tarjeta.CVV, "CVV");
                    ValidadorBLL.CampoObligatorio(tarjeta.Marca, "Marca de tarjeta");

                    if (tarjeta.Vencimiento <= DateTime.Now)
                        throw new Exception("La tarjeta indicada se encuentra vencida.");

                    tarjeta.Nombre = "Tarjeta";

                    if (tarjeta.Id == 0)
                        tarjetaMPP.Alta(tarjeta);
                    else
                        tarjetaMPP.Modificar(tarjeta);
                }
                else if (obj.MedioDePago is BETransferencia transferencia)
                {
                    ValidadorBLL.CampoObligatorio(transferencia.Banco, "Banco");
                    ValidadorBLL.CampoObligatorio(transferencia.CBU, "CBU");

                    transferencia.Nombre = "Transferencia";

                    if (transferencia.Id == 0)
                        transferenciaMPP.Alta(transferencia);
                    else
                        transferenciaMPP.Modificar(transferencia);
                }
                else if (obj.MedioDePago is BEEfectivo efectivo)
                {
                    if (efectivo.MontoEntregado <= 0)
                        throw new Exception("Debe indicar el monto entregado.");

                    efectivo.Nombre = "Efectivo";
                    efectivo.Moneda = string.IsNullOrWhiteSpace(efectivo.Moneda) ? "ARS" : efectivo.Moneda;
                    efectivo.FechaPago = obj.FechaPago;

                    efectivo.Vuelto = (decimal)efectivo.MontoEntregado - obj.Importe;

                    if (efectivo.Vuelto < 0)
                        throw new Exception("Monto insuficiente para cubrir el pago.");

                    if (efectivo.Id == 0)
                        efectivoMPP.Alta(efectivo);
                    else
                        efectivoMPP.Modificar(efectivo);
                }
                else
                {
                    throw new Exception("El medio de pago indicado no es válido.");
                }

                if (obj.Id == 0)
                    pagoMPP.Alta(obj);
                else
                    pagoMPP.Modificar(obj);

                // Si el pago se registra correctamente, la factura pasa a estado Pagada.
                facturaBLL.MarcarComoPagada(facturaActual);

                ReconstructorBLL.LimpiarCache<BEPago>();
                ReconstructorBLL.LimpiarCache<BEFactura>();
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
                        p.MedioDePago = ReconstruirMedioDePago(p.MedioDePago);
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
                        pago.MedioDePago = ReconstruirMedioDePago(pago.MedioDePago);
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
        private BEMedioDePago ReconstruirMedioDePago(BEMedioDePago medio)
        {
            if (medio == null || medio.Id <= 0)
                return null;

            if (medio is BETarjeta)
            {
                BETarjeta tarjeta = tarjetaMPP.BuscarPorId(medio.Id);

                if (tarjeta != null)
                    tarjeta.Nombre = "Tarjeta";

                return tarjeta;
            }

            if (medio is BETransferencia)
            {
                BETransferencia transferencia = transferenciaMPP.BuscarPorId(medio.Id);

                if (transferencia != null)
                    transferencia.Nombre = "Transferencia";

                return transferencia;
            }

            if (medio is BEEfectivo)
            {
                BEEfectivo efectivo = efectivoMPP.BuscarPorId(medio.Id);

                if (efectivo != null)
                    efectivo.Nombre = "Efectivo";

                return efectivo;
            }

            return medio;
        }
        #endregion
    }
}
