using Abstraccion;
using BE.Transporte;
using BLL.Infra;
using MPP.Transporte;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Transporte
{
    public class ManifiestoBLL : IGestor<BEManifiesto>
    {
        private ManifiestoMPP manifiestoMPP = new ManifiestoMPP();

        #region "Constructor estático"

        // Registra cómo reconstruir un manifiesto por Id
        static ManifiestoBLL()
        {
            ReconstructorBLL.Registrar<BEManifiesto>(
                id => new ManifiestoBLL().ListarObjeto(new BEManifiesto { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        // El manifiesto agrupa contenedores ya asignados a un buque y destino.
        public void Guardar(BEManifiesto obj)
        {
            try
            {
                // Se validan datos mínimos del documento antes de persistirlo.
                ValidadorBLL.ObjetoNoNulo(obj, "Manifiesto");
                ValidadorBLL.ObjetoNoNulo(obj.Buque, "Buque asociado");
                ValidadorBLL.CampoObligatorio(obj.AutoridadDestino, "Autoridad de destino");
                ValidadorBLL.ListaNoVacia(obj.Contenedores, "Contenedores incluidos");

                if (obj.FechaEmision > DateTime.Now)
                    throw new Exception("La fecha de emisión no puede ser futura.");

                foreach (var contenedor in obj.Contenedores)
                {
                    ValidadorBLL.ObjetoNoNulo(contenedor, "Contenedor incluido");

                    if (contenedor.Buque == null || contenedor.Buque.Id != obj.Buque.Id)
                        throw new Exception("Todos los contenedores del manifiesto deben pertenecer al buque seleccionado.");

                    if (contenedor.Cargas == null || contenedor.Cargas.Count == 0)
                        throw new Exception("No se pueden incluir contenedores sin carga en el manifiesto.");
                }

                var destinos = obj.Contenedores
                    .SelectMany(c => c.Cargas ?? new List<BECarga>())
                    .Where(c => c.Destino != null)
                    .Select(c => c.Destino.ToString())
                    .Where(d => !string.IsNullOrWhiteSpace(d))
                    .Distinct()
                    .ToList();

                if (destinos.Count > 1)
                    throw new Exception("Para este manifiesto simplificado, todos los contenedores deben compartir el mismo destino.");

                if (obj.Id == 0)
                    manifiestoMPP.Alta(obj);
                else
                    manifiestoMPP.Modificar(obj);

                // Se limpia la cache para que los documentos emitidos se reconstruyan actualizados.
                ReconstructorBLL.LimpiarCache<BEManifiesto>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar manifiesto.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BEManifiesto obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Manifiesto");
                manifiestoMPP.Baja(obj);

                ReconstructorBLL.LimpiarCache<BEManifiesto>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar manifiesto.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BEManifiesto> ListarTodo()
        {
            try
            {
                var lista = manifiestoMPP.Listar();

                // En XML se guardan referencias por Id; la BLL reconstruye buque y contenedores completos.
                foreach (var m in lista)
                {
                    if (m.Buque != null && m.Buque.Id > 0)
                        m.Buque = ReconstructorBLL.Reconstruir<BEBuque>(m.Buque.Id);

                    if (m.Contenedores != null && m.Contenedores.Count > 0)
                    {
                        var ids = m.Contenedores.Select(c => c.Id).ToList();
                        m.Contenedores = ReconstructorBLL.ReconstruirLista<BEContenedor>(ids);
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar manifiestos.", ex);
            }
        }

        public BEManifiesto ListarObjeto(BEManifiesto obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Manifiesto");

                var manifiesto = manifiestoMPP.BuscarPorId(obj.Id);

                if (manifiesto != null)
                {
                    if (manifiesto.Buque != null && manifiesto.Buque.Id > 0)
                        manifiesto.Buque = ReconstructorBLL.Reconstruir<BEBuque>(manifiesto.Buque.Id);

                    if (manifiesto.Contenedores != null && manifiesto.Contenedores.Count > 0)
                    {
                        var ids = manifiesto.Contenedores.Select(c => c.Id).ToList();
                        manifiesto.Contenedores = ReconstructorBLL.ReconstruirLista<BEContenedor>(ids);
                    }
                }

                return manifiesto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar manifiesto.", ex);
            }
        }

        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return manifiestoMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de manifiesto.", ex);
            }
        }

        #endregion
    }
}
