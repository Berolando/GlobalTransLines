using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE;
using BE.Transporte;
using BE.Enums;
using MPP.Transporte;
using BLL.Transversal;

namespace BLL.Transporte
{
    public class EstibaBLL : IGestor<BEEstiba>
    {
        private EstibaMPP estibaMPP = new EstibaMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir una estiba por Id
        static EstibaBLL()
        {
            ReconstructorBLL.Registrar<BEEstiba>(
                id => new EstibaBLL().ListarObjeto(new BEEstiba { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEEstiba obj)
        {
            try
            {
                // Validaciones
                ValidadorBLL.ObjetoNoNulo(obj, "Estiba");
                ValidadorBLL.ObjetoNoNulo(obj.Contenedor, "Contenedor asociado");
                ValidadorBLL.ObjetoNoNulo(obj.Buque, "Buque asociado");
                ValidadorBLL.ListaNoVacia(obj.Cargas, "Cargas asociadas");
                ValidadorBLL.CampoObligatorio(obj.UbicacionBodega, "Ubicación de bodega");

                if (obj.FechaPlan > DateTime.Now)
                    throw new Exception("La fecha del plan de estiba no puede ser futura.");

                if (obj.Id == 0)
                    estibaMPP.Alta(obj);
                else
                    estibaMPP.Modificar(obj);

                // Limpieza de cache
                ReconstructorBLL.LimpiarCache<BEEstiba>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar estiba.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEEstiba obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Estiba");
                estibaMPP.Baja(obj);

                ReconstructorBLL.LimpiarCache<BEEstiba>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar estiba.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEEstiba> ListarTodo()
        {
            try
            {
                var lista = estibaMPP.Listar();

                // Reconstrucción de relaciones por Id
                foreach (var e in lista)
                {
                    if (e.Contenedor != null && e.Contenedor.Id > 0)
                        e.Contenedor = ReconstructorBLL.Reconstruir<BEContenedor>(e.Contenedor.Id);

                    if (e.Buque != null && e.Buque.Id > 0)
                        e.Buque = ReconstructorBLL.Reconstruir<BEBuque>(e.Buque.Id);

                    if (e.Cargas != null && e.Cargas.Count > 0)
                    {
                        var ids = e.Cargas.Select(c => c.Id).ToList();
                        e.Cargas = ReconstructorBLL.ReconstruirLista<BECarga>(ids);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar estibas.", ex);
            }
        }

        public BEEstiba ListarObjeto(BEEstiba obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Estiba");

                var estiba = estibaMPP.BuscarPorId(obj.Id);

                if (estiba != null)
                {
                    if (estiba.Contenedor != null && estiba.Contenedor.Id > 0)
                        estiba.Contenedor = ReconstructorBLL.Reconstruir<BEContenedor>(estiba.Contenedor.Id);

                    if (estiba.Buque != null && estiba.Buque.Id > 0)
                        estiba.Buque = ReconstructorBLL.Reconstruir<BEBuque>(estiba.Buque.Id);

                    if (estiba.Cargas != null && estiba.Cargas.Count > 0)
                    {
                        var ids = estiba.Cargas.Select(c => c.Id).ToList();
                        estiba.Cargas = ReconstructorBLL.ReconstruirLista<BECarga>(ids);
                    }
                }

                return estiba;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar estiba.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return estibaMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de estiba.", ex);
            }
        }

        #endregion
    }
}
