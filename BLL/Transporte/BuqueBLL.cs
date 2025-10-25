using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE;
using BE.Enums;
using MPP.Flota;
using BLL.Transversal;

namespace BLL.Transporte
{
    public class BuqueBLL : IGestor<BEBuque>
    {
        private BuqueMPP buqueMPP = new BuqueMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir un buque por Id
        static BuqueBLL()
        {
            ReconstructorBLL.Registrar<BEBuque>(
                id => new BuqueBLL().ListarObjeto(new BEBuque { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEBuque obj)
        {
            try
            {
                // Validaciones
                ValidadorBLL.ObjetoNoNulo(obj, "Buque");
                ValidadorBLL.CampoObligatorio(obj.Nombre, "Nombre del buque");
                ValidadorBLL.CampoObligatorio(obj.CodigoIMO, "Código IMO");

                if (obj.CapacidadTEU <= 0)
                    throw new Exception("La capacidad TEU debe ser mayor a cero.");

                if (obj.CapacidadPeso <= 0)
                    throw new Exception("La capacidad de peso debe ser mayor a cero.");

                if (obj.Disponibilidad < 0)
                    throw new Exception("La disponibilidad no puede ser negativa.");

                // Alta o modificación
                if (obj.Id == 0)
                    buqueMPP.Alta(obj);
                else
                    buqueMPP.Modificar(obj);

                // Limpieza de cache
                ReconstructorBLL.LimpiarCache<BEBuque>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar buque.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEBuque obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Buque");
                buqueMPP.Baja(obj);

                ReconstructorBLL.LimpiarCache<BEBuque>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar buque.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEBuque> ListarTodo()
        {
            try
            {
                var lista = buqueMPP.Listar();

                // Validación adicional: asegurar consistencia de itinerarios
                foreach (var b in lista)
                {
                    if (b.Itinerarios == null)
                        b.Itinerarios = new List<string>();
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar buques.", ex);
            }
        }

        public BEBuque ListarObjeto(BEBuque obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Buque");

                var buque = buqueMPP.BuscarPorId(obj.Id);

                if (buque != null && buque.Itinerarios == null)
                    buque.Itinerarios = new List<string>();

                return buque;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar buque.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return buqueMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de buque.", ex);
            }
        }

        #endregion
    }
}
