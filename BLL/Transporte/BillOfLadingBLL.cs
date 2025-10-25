using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE;
using MPP.Documentacion;
using BLL.Transversal;

namespace BLL.Transporte
{
    public class BillOfLadingBLL : IGestor<BEBillofLading>
    {
        private BillofLadingMPP billMPP = new BillofLadingMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir un Bill of Lading por Id
        static BillOfLadingBLL()
        {
            ReconstructorBLL.Registrar<BEBillofLading>(
                id => new BillOfLadingBLL().ListarObjeto(new BEBillofLading { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEBillofLading obj)
        {
            try
            {
                // Validaciones
                ValidadorBLL.ObjetoNoNulo(obj, "Bill of Lading");
                ValidadorBLL.ObjetoNoNulo(obj.Booking, "Booking asociado");
                ValidadorBLL.ObjetoNoNulo(obj.Cliente, "Cliente asociado");
                ValidadorBLL.ObjetoNoNulo(obj.Carga, "Carga asociada");
                ValidadorBLL.ObjetoNoNulo(obj.Contenedor, "Contenedor asociado");
                ValidadorBLL.ObjetoNoNulo(obj.Buque, "Buque asociado");
                ValidadorBLL.CampoObligatorio(obj.PuertoDestino, "Puerto de destino");

                if (obj.FechaEmision > DateTime.Now)
                    throw new Exception("La fecha de emisión no puede ser futura.");

                if (obj.Id == 0)
                    billMPP.Alta(obj);
                else
                    billMPP.Modificar(obj);

                // Limpieza de cache
                ReconstructorBLL.LimpiarCache<BEBillofLading>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar Bill of Lading.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEBillofLading obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Bill of Lading");
                billMPP.Baja(obj);

                ReconstructorBLL.LimpiarCache<BEBillofLading>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar Bill of Lading.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEBillofLading> ListarTodo()
        {
            try
            {
                var lista = billMPP.Listar();

                // Reconstrucción de relaciones por Id
                foreach (var b in lista)
                {
                    if (b.Booking != null && b.Booking.Id > 0)
                        b.Booking = ReconstructorBLL.Reconstruir<BEBooking>(b.Booking.Id);

                    if (b.Cliente != null && b.Cliente.Id > 0)
                        b.Cliente = ReconstructorBLL.Reconstruir<BECliente>(b.Cliente.Id);

                    if (b.Carga != null && b.Carga.Id > 0)
                        b.Carga = ReconstructorBLL.Reconstruir<BECarga>(b.Carga.Id);

                    if (b.Contenedor != null && b.Contenedor.Id > 0)
                        b.Contenedor = ReconstructorBLL.Reconstruir<BEContenedor>(b.Contenedor.Id);

                    if (b.Buque != null && b.Buque.Id > 0)
                        b.Buque = ReconstructorBLL.Reconstruir<BEBuque>(b.Buque.Id);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar Bill of Lading.", ex);
            }
        }

        public BEBillofLading ListarObjeto(BEBillofLading obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Bill of Lading");

                var bl = billMPP.BuscarPorId(obj.Id);

                if (bl != null)
                {
                    if (bl.Booking != null && bl.Booking.Id > 0)
                        bl.Booking = ReconstructorBLL.Reconstruir<BEBooking>(bl.Booking.Id);

                    if (bl.Cliente != null && bl.Cliente.Id > 0)
                        bl.Cliente = ReconstructorBLL.Reconstruir<BECliente>(bl.Cliente.Id);

                    if (bl.Carga != null && bl.Carga.Id > 0)
                        bl.Carga = ReconstructorBLL.Reconstruir<BECarga>(bl.Carga.Id);

                    if (bl.Contenedor != null && bl.Contenedor.Id > 0)
                        bl.Contenedor = ReconstructorBLL.Reconstruir<BEContenedor>(bl.Contenedor.Id);

                    if (bl.Buque != null && bl.Buque.Id > 0)
                        bl.Buque = ReconstructorBLL.Reconstruir<BEBuque>(bl.Buque.Id);
                }

                return bl;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar Bill of Lading.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return billMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de Bill of Lading.", ex);
            }
        }

        #endregion
    }
}
