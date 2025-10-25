using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE;
using BE.Enums;
using BE.Transporte;
using MPP.Transporte;
using BLL.Transversal;

namespace BLL.Transporte
{
    public class ConsolidacionBLL : IGestor<BEConsolidacion>
    {
        private ConsolidacionMPP consolidacionMPP = new ConsolidacionMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir una consolidación por Id
        static ConsolidacionBLL()
        {
            ReconstructorBLL.Registrar<BEConsolidacion>(
                id => new ConsolidacionBLL().ListarObjeto(new BEConsolidacion { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEConsolidacion obj)
        {
            try
            {
                // Validaciones básicas
                ValidadorBLL.ObjetoNoNulo(obj, "Consolidación");
                ValidadorBLL.ObjetoNoNulo(obj.Contenedor, "Contenedor asociado");
                ValidadorBLL.ListaNoVacia(obj.Cargas, "Cargas a consolidar");

                if (obj.CapacidadTotal <= 0)
                    throw new Exception("La capacidad total debe ser mayor a cero.");

                if (obj.CapacidadOcupada < 0)
                    throw new Exception("La capacidad ocupada no puede ser negativa.");

                if (obj.CapacidadOcupada > obj.CapacidadTotal)
                    throw new Exception("La capacidad ocupada no puede superar la capacidad total.");

                if (obj.FechaConsolidacion > DateTime.Now)
                    throw new Exception("La fecha de consolidación no puede ser futura.");

                if (obj.Id == 0)
                    consolidacionMPP.Alta(obj);
                else
                    consolidacionMPP.Modificar(obj);

                // Limpieza del cache
                ReconstructorBLL.LimpiarCache<BEConsolidacion>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar consolidación.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEConsolidacion obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Consolidación");
                consolidacionMPP.Baja(obj);

                ReconstructorBLL.LimpiarCache<BEConsolidacion>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar consolidación.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEConsolidacion> ListarTodo()
        {
            try
            {
                var lista = consolidacionMPP.Listar();

                // Reconstrucción de relaciones
                foreach (var c in lista)
                {
                    if (c.Contenedor != null && c.Contenedor.Id > 0)
                        c.Contenedor = ReconstructorBLL.Reconstruir<BEContenedor>(c.Contenedor.Id);

                    if (c.Cargas != null && c.Cargas.Count > 0)
                    {
                        var ids = c.Cargas.Select(x => x.Id).ToList();
                        c.Cargas = ReconstructorBLL.ReconstruirLista<BECarga>(ids);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar consolidaciones.", ex);
            }
        }

        public BEConsolidacion ListarObjeto(BEConsolidacion obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Consolidación");

                var consolidacion = consolidacionMPP.BuscarPorId(obj.Id);

                if (consolidacion != null)
                {
                    if (consolidacion.Contenedor != null && consolidacion.Contenedor.Id > 0)
                        consolidacion.Contenedor = ReconstructorBLL.Reconstruir<BEContenedor>(consolidacion.Contenedor.Id);

                    if (consolidacion.Cargas != null && consolidacion.Cargas.Count > 0)
                    {
                        var ids = consolidacion.Cargas.Select(x => x.Id).ToList();
                        consolidacion.Cargas = ReconstructorBLL.ReconstruirLista<BECarga>(ids);
                    }
                }

                return consolidacion;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar consolidación.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return consolidacionMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de consolidación.", ex);
            }
        }

        #endregion
    }
}
