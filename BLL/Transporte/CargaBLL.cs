using Abstraccion;
using BE.Dominio;
using BE.Transporte;
using BE.Infra;
using BLL.Infra;
using MPP.Cargas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Transporte
{
    public class CargaBLL : IGestor<BECarga>
    {
        private CargaMPP cargaMPP = new CargaMPP();

        #region "Constructor estático"

        static CargaBLL()
        {
            ReconstructorBLL.Registrar<BECarga>(
                id => new CargaBLL().ListarObjeto(new BECarga { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BECarga carga)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(carga, "Carga");
                ValidadorBLL.CampoObligatorio(carga.Descripcion, "Descripción");
                ValidadorBLL.CampoObligatorio(carga.Tipo?.ToString(), "Tipo de Carga");
                ValidadorBLL.ObjetoNoNulo(carga.TipoContenedor, "Tipo de Contenedor");
                ValidadorBLL.MayorQueCero(carga.Peso, "Peso");
                ValidadorBLL.MayorQueCero(carga.Volumen, "Volumen");
                ValidadorBLL.ValidarDireccion(carga.Origen, "Dirección de Origen");
                ValidadorBLL.ObjetoNoNulo(carga.PuertoEmbarque, "Puerto de embarque");
                ValidadorBLL.ValidarDireccion(carga.Destino, "Puerto de destino");

                if (!carga.PuertoEmbarque.EsPuerto ||
                    !string.Equals(carga.PuertoEmbarque.Pais, "Argentina", StringComparison.OrdinalIgnoreCase))
                    throw new Exception("Debe seleccionar un puerto de embarque argentino.");

                if (!carga.Destino.EsPuerto)
                    throw new Exception("El destino debe identificar un puerto.");

                if (carga.Tipo.Codigo == TipoCarga.FCL.Codigo ||
                    carga.Tipo.Codigo == TipoCarga.LCL.Codigo)
                {
                    throw new Exception(
                        "Debe indicar la naturaleza de la carga: general, refrigerada, peligrosa o granel. " +
                        "La modalidad operativa de esta versión es contenedor completo.");
                }

                var contenedorBLL = new ContenedorBLL();
                contenedorBLL.ValidarCompatibilidadTipos(carga.Tipo, carga.TipoContenedor);
                contenedorBLL.ValidarCapacidadPlanificada(
                    carga.TipoContenedor, carga.Peso, carga.Volumen);

                // Las direcciones se persisten primero porque la carga guarda sus relaciones por Id.
                if (carga.Origen != null)
                {
                    carga.Origen.Tipo = "DOMICILIO";
                    new DireccionBLL().Guardar(carga.Origen);
                }

                // El puerto se selecciona del catálogo y ya debe contar con un Id persistido.
                if (carga.PuertoEmbarque.Id <= 0)
                    throw new Exception("El puerto de embarque seleccionado no pertenece al catálogo.");

                if (carga.Destino.Id <= 0)
                    throw new Exception("El puerto de destino seleccionado no pertenece al catálogo.");

                // Una carga nueva queda disponible hasta que cotización, booking o asignación cambien su estado.
                if (carga.Estado == null)
                    carga.Estado = EstadoCarga.Disponible;
                
                if (carga.Id == 0)

                    cargaMPP.Alta(carga);
                else
                    cargaMPP.Modificar(carga);

                // Se limpia la cache para que las próximas reconstrucciones tomen peso, volumen y estado actualizados.
                ReconstructorBLL.LimpiarCache<BECarga>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar carga.", ex);
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BECarga carga)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(carga, "Carga");
                cargaMPP.Baja(carga);
                ReconstructorBLL.LimpiarCache<BECarga>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar carga.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BECarga> ListarTodo()
        {
            try
            {
                var lista = cargaMPP.Listar();

                foreach (var c in lista)
                {
                    if (c.Origen != null && c.Origen.Id > 0)
                        c.Origen = ReconstructorBLL.Reconstruir<BEDireccion>(c.Origen.Id);

                    if (c.PuertoEmbarque != null && c.PuertoEmbarque.Id > 0)
                        c.PuertoEmbarque = ReconstructorBLL.Reconstruir<BEDireccion>(c.PuertoEmbarque.Id);

                    if (c.Destino != null && c.Destino.Id > 0)
                        c.Destino = ReconstructorBLL.Reconstruir<BEDireccion>(c.Destino.Id);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar cargas.", ex);
            }
        }

        public BECarga ListarObjeto(BECarga carga)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(carga, "Carga");

                var cargaEncontrada = cargaMPP.BuscarPorId(carga.Id);

                if (cargaEncontrada != null)
                {
                    if (cargaEncontrada.Origen != null && cargaEncontrada.Origen.Id > 0)
                        cargaEncontrada.Origen = ReconstructorBLL.Reconstruir<BEDireccion>(cargaEncontrada.Origen.Id);

                    if (cargaEncontrada.PuertoEmbarque != null && cargaEncontrada.PuertoEmbarque.Id > 0)
                        cargaEncontrada.PuertoEmbarque = ReconstructorBLL.Reconstruir<BEDireccion>(cargaEncontrada.PuertoEmbarque.Id);

                    if (cargaEncontrada.Destino != null && cargaEncontrada.Destino.Id > 0)
                        cargaEncontrada.Destino = ReconstructorBLL.Reconstruir<BEDireccion>(cargaEncontrada.Destino.Id);

                   
                    if (!string.IsNullOrEmpty(cargaEncontrada.Tipo?.Codigo))
                        cargaEncontrada.Tipo = TipoCarga.ObtenerPorCodigo(cargaEncontrada.Tipo.Codigo);

                    if (!string.IsNullOrEmpty(cargaEncontrada.Estado?.Codigo))
                        cargaEncontrada.Estado = EstadoCarga.ObtenerPorCodigo(cargaEncontrada.Estado.Codigo);
                }

                return cargaEncontrada;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar carga.", ex);
            }
        }


        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return cargaMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de carga.", ex);
            }
        }

        #endregion

        #region "Reglas de negocio"

        // Una carga solo puede volver a cotizarse si todavía no fue tomada por otro flujo.
        public bool PuedeAgregarCarga(BECarga carga)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(carga, "Carga");

                var actual = ReconstructorBLL.Reconstruir<BECarga>(carga.Id);
                if (actual == null)
                    throw new Exception("No se pudo encontrar la carga especificada.");

                // Solo las cargas DISPONIBLES pueden usarse.
                return actual.Estado != null && actual.Estado.Codigo == EstadoCarga.Disponible.Codigo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar si la carga puede agregarse.", ex);
            }
        }
        public List<BECarga> ListarParaBooking()
        {
            try
            {
                var lista = cargaMPP.Listar();
                return lista.Where(c => c.Estado.Codigo == EstadoCarga.AceptadaCotizacion.Codigo).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar cargas disponibles para booking.", ex);
            }
        }
        public void ActualizarEstado(BECarga carga, EstadoCarga nuevoEstado)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(carga, "Carga");
                if (carga.Id <= 0) throw new Exception("Id de carga inválido.");

                // Se actualiza solamente el estado para no pisar datos propios de la carga.
                var actual = cargaMPP.BuscarPorId(carga.Id);
                if (actual == null) throw new Exception("No se encontró la carga.");

                actual.Estado = nuevoEstado ?? EstadoCarga.Disponible;
                cargaMPP.Modificar(actual);

                ReconstructorBLL.LimpiarCache<BECarga>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar estado de carga.", ex);
            }
        }

        #endregion
    }
}
