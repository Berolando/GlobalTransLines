using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using Abstraccion;
using BE.Dominio;
using BE.Finanzas;
using BE.Personas;
using BE.ServiciosSubcontratados;
using BE.Transporte;
using BLL.Transporte;
using BLL.Infra;
using MPP.Finanzas;
using Transversales;

namespace BLL.Finanzas
{
    public class CotizacionBLL : IGestor<BECotizacion>
    {
        private CotizacionMPP cotizacionMPP = new CotizacionMPP();

        #region "Constructor estático"

        static CotizacionBLL()
        {
            ReconstructorBLL.Registrar<BECotizacion>(
                id => new CotizacionBLL().ListarObjeto(new BECotizacion { Id = id })
            );
        }

        #endregion

        #region "Guardar"

        public void Guardar(BECotizacion obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Cotización");
                ValidadorBLL.ListaNoVacia(obj.Cargas, "Cargas asociadas");

                if (obj.Cliente == null || obj.Cliente.Id <= 0)
                    throw new Exception("Debe asignar un cliente válido a la cotización.");

                var cargasCompletas = obj.Cargas
                    .Select(c => c != null && c.Id > 0
                        ? ReconstructorBLL.Reconstruir<BECarga>(c.Id)
                        : c).Where(c => c != null).ToList();

                if (cargasCompletas.Count != obj.Cargas.Count)
                    throw new Exception("No se pudieron reconstruir todas las cargas de la cotización.");

                var tiposContenedor = cargasCompletas
                    .Select(c => c.TipoContenedor?.Codigo)
                    .Where(codigo => !string.IsNullOrWhiteSpace(codigo))
                    .Distinct()
                    .ToList();

                if (tiposContenedor.Count != 1)
                    throw new Exception("Las cargas de una cotización deben requerir el mismo tipo de contenedor.");

                obj.Cargas = cargasCompletas;
                obj.TipoContenedor = TipoContenedor.ObtenerPorCodigo(tiposContenedor[0]);

                if (obj.MontoTotal <= 0)
                    obj.MontoTotal = CalcularMontoTotal(obj);

                if (obj.FechaVencimiento <= obj.FechaEmision)
                    throw new Exception("La fecha de vencimiento debe ser posterior a la de emisión.");

                if (obj.Estado == null)
                    obj.Estado = EstadoCotizacion.Pendiente;

                if (obj.Id == 0)
                    cotizacionMPP.Alta(obj);
                else
                    cotizacionMPP.Modificar(obj);

                // La cotización se reconstruye desde varios flujos, por eso se limpia al guardar cambios.
                ReconstructorBLL.LimpiarCache<BECotizacion>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar cotización.", ex);
            }
        }

        #endregion

        #region "Cálculo de montos"

        public decimal CalcularMontoTotal(BECotizacion cotizacion)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(cotizacion, "Cotización");

                decimal totalFlete = 0;
                decimal totalServicios = 0;

                if (cotizacion.Cargas != null && cotizacion.Cargas.Count > 0)
                {
                    foreach (var carga in cotizacion.Cargas)
                        totalFlete += CalcularFlete(carga);
                }

                if (cotizacion.Servicios != null && cotizacion.Servicios.Count > 0)
                {
                    totalServicios = CalcularServicios(cotizacion.Servicios);
                }

                return totalFlete + totalServicios;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular el monto total de la cotización.", ex);
            }
        }

        public decimal CalcularFlete(BECarga carga)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(carga, "Carga");
                ValidadorBLL.ObjetoNoNulo(carga.Tipo, "Tipo de carga");
                ValidadorBLL.ObjetoNoNulo(carga.TipoContenedor, "Tipo de contenedor");

                return new TarifarioBLL().CalcularTarifa(
                    carga.Tipo,
                    carga.TipoContenedor,
                    carga.Peso,
                    carga.Volumen);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular el flete de la carga.", ex);
            }
        }

        public decimal CalcularServicios(List<BEServicioSubcontratado> servicios)
        {
            try
            {
                ValidadorBLL.ListaNoVacia(servicios, "Servicios subcontratados");
                return servicios.Sum(s => s.Costo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular el total de servicios subcontratados.", ex);
            }
        }

        #endregion

        #region "Modificar"

        public void CambiarEstado(BECotizacion c, EstadoCotizacion nuevo)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(c, "Cotización");
                ValidadorBLL.ObjetoNoNulo(nuevo, "Estado de cotización");

                var cotizacionActual = ListarObjeto(new BECotizacion { Id = c.Id });
                if (cotizacionActual == null)
                    throw new Exception("No se encontró la cotización seleccionada.");

                cotizacionActual.Estado = nuevo;

                // Al aceptar la cotización, sus cargas dejan de estar disponibles para otras cotizaciones.
                if (nuevo.Codigo == EstadoCotizacion.Aceptada.Codigo &&
                    cotizacionActual.Cargas != null &&
                    cotizacionActual.Cargas.Count > 0)
                {
                    var cargaBLL = new CargaBLL();

                    foreach (var carga in cotizacionActual.Cargas)
                    {
                        if (carga == null || carga.Id <= 0)
                            continue;

                        // Solo cambia el estado: no corresponde revalidar ni regrabar toda la carga.
                        cargaBLL.ActualizarEstado(carga, EstadoCarga.AceptadaCotizacion);
                    }
                }

                cotizacionMPP.Modificar(cotizacionActual);

                ReconstructorBLL.LimpiarCache<BECotizacion>();
                ReconstructorBLL.LimpiarCache<BECarga>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado de la cotización.", ex);
            }
        }

        #endregion

        #region "Reglas de negocio"

        // Criterio simple para distinguir cotizaciones que requieren mayor atención comercial.
        public bool EsCompleja(BECotizacion c)
        {
            return c.MontoTotal > 10000;
        }

        // Una carga aceptada en una cotización no debe reutilizarse en otra cotización aceptada.
        public bool PuedeAgregarCarga(BECarga carga)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(carga, "Carga");

                var todasLasCotizaciones = ListarTodo();
                bool estaSiendoUsada = todasLasCotizaciones.Any(c =>
                    c.Estado == EstadoCotizacion.Aceptada &&
                    c.Cargas.Any(x => x.Id == carga.Id));

                // Devuelve true solo si la carga no aparece en otra cotización aceptada.
                return !estaSiendoUsada;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar si la carga puede agregarse a una cotización.", ex);
            }
        }
        // La cotización solo puede generar booking cuando cumple las reglas definidas para ese flujo.
        public bool PuedeGenerarBooking(BECotizacion c)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(c, "Cotización");
              
                ValidadorBLL.ValidarCotizacionParaBooking(c);

                return true; 
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region "Eliminar"

        public void Eliminar(BECotizacion obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Cotización");
                cotizacionMPP.Baja(obj);

                ReconstructorBLL.LimpiarCache<BECotizacion>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cotización.", ex);
            }
        }

        #endregion

        #region "Listar / Buscar"

        public List<BECotizacion> ListarTodo()
        {
            try
            {
                var lista = cotizacionMPP.Listar();

                foreach (var c in lista)
                {
                    if (c.Cliente != null && c.Cliente.Id > 0)
                        c.Cliente = ReconstructorBLL.Reconstruir<BECliente>(c.Cliente.Id);

                    if (c.Cargas != null && c.Cargas.Count > 0)
                        c.Cargas = c.Cargas
                            .Select(x => ReconstructorBLL.Reconstruir<BECarga>(x.Id))
                            .Where(x => x != null)
                            .ToList();

                    if (c.Servicios != null && c.Servicios.Count > 0)
                        c.Servicios = c.Servicios
                            .Select(x => ReconstructorBLL.Reconstruir<BEServicioSubcontratado>(x.Id))
                            .Where(x => x != null)
                            .ToList();

                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar cotizaciones.", ex);
            }
        }

        public BECotizacion ListarObjeto(BECotizacion obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Cotización");

                var c = cotizacionMPP.BuscarPorId(obj.Id);
                if (c == null) return null;

                // En XML se guarda el Id; la BLL reconstruye el cliente completo para trabajar con datos reales.
                if (c.Cliente != null && c.Cliente.Id > 0)
                    c.Cliente = ReconstructorBLL.Reconstruir<BECliente>(c.Cliente.Id);

                // Las cargas también se reconstruyen por Id para respetar el criterio de persistencia XML.
                if (c.Cargas != null && c.Cargas.Count > 0)
                {
                    var ids = c.Cargas.Select(x => x.Id).ToList();
                    c.Cargas = ReconstructorBLL.ReconstruirLista<BECarga>(ids);
                }

                // Los servicios subcontratados se recuperan completos antes de usar la cotización.
                if (c.Servicios != null && c.Servicios.Count > 0)
                {
                    var ids = c.Servicios.Select(x => x.Id).ToList();
                    c.Servicios = ReconstructorBLL.ReconstruirLista<BEServicioSubcontratado>(ids);
                }

                // Los estados se guardan por código para reconstruir siempre la instancia correcta del dominio.
                if (!string.IsNullOrEmpty(c.Estado?.Codigo))
                    c.Estado = EstadoCotizacion.ObtenerPorCodigo(c.Estado.Codigo);

                // El tipo de contenedor también se normaliza desde su código.
                if (!string.IsNullOrEmpty(c.TipoContenedor?.Codigo))
                    c.TipoContenedor = TipoContenedor.ObtenerPorCodigo(c.TipoContenedor.Codigo);

                return c;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cotización.", ex);
            }
        }


        #endregion

        #region "Utilitarios"

        public long ObtenerUltimoId()
        {
            try
            {
                return cotizacionMPP.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de cotización.", ex);
            }
        }
        public string ObtenerProximoNumeroPreview()
        {
            try
            {
                long proximoId = ObtenerUltimoId() + 1;
                return GeneradorNumeros.GenerarNumeroCotizacion(proximoId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar número de vista previa de Cotizacion.", ex);
            }
        }

        #endregion
    }
}
