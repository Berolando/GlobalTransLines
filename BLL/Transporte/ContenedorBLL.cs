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
    public class ContenedorBLL : IGestor<BEContenedor>
    {
        private ContenedorMPP contenedorMPP = new ContenedorMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir un contenedor por Id
        static ContenedorBLL()
        {
            ReconstructorBLL.Registrar<BEContenedor>(
                id => new ContenedorBLL().ListarObjeto(new BEContenedor { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BEContenedor obj)
        {
            try
            {
                // Validaciones básicas
                ValidadorBLL.ObjetoNoNulo(obj, "Contenedor");
                ValidadorBLL.CampoObligatorio(obj.Codigo, "Código del contenedor");
                ValidadorBLL.ObjetoNoNulo(obj.Buque, "Buque asociado");
                ValidadorBLL.ListaNoVacia(obj.Cargas, "Cargas asociadas");

                if (obj.CapacidadPeso <= 0 || obj.CapacidadVolumen <= 0)
                    throw new Exception("Las capacidades de peso y volumen deben ser mayores a cero.");

                if (obj.Id == 0)
                    contenedorMPP.Alta(obj);
                else
                    contenedorMPP.Modificar(obj);

                ReconstructorBLL.LimpiarCache<BEContenedor>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar contenedor.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEContenedor obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Contenedor");
                contenedorMPP.Baja(obj);

                ReconstructorBLL.LimpiarCache<BEContenedor>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar contenedor.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEContenedor> ListarTodo()
        {
            try
            {
                var lista = contenedorMPP.Listar();

                // Reconstrucción de relaciones
                foreach (var c in lista)
                {
                    if (c.Buque != null && c.Buque.Id > 0)
                        c.Buque = ReconstructorBLL.Reconstruir<BEBuque>(c.Buque.Id);

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
                throw new Exception("Error al listar contenedores.", ex);
            }
        }

        public BEContenedor ListarObjeto(BEContenedor obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Contenedor");

                var contenedor = contenedorMPP.BuscarPorId(obj.Id);

                if (contenedor != null)
                {
                    if (contenedor.Buque != null && contenedor.Buque.Id > 0)
                        contenedor.Buque = ReconstructorBLL.Reconstruir<BEBuque>(contenedor.Buque.Id);

                    if (contenedor.Cargas != null && contenedor.Cargas.Count > 0)
                    {
                        var ids = contenedor.Cargas.Select(x => x.Id).ToList();
                        contenedor.Cargas = ReconstructorBLL.ReconstruirLista<BECarga>(ids);
                    }
                }

                return contenedor;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar contenedor.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return contenedorMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de contenedor.", ex);
            }
        }

        #endregion
    }
}
