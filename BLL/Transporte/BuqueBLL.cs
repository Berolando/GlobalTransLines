using Abstraccion;
using BE.Transporte;
using BE.Infra;
using BLL.Infra;
using MPP.Flota;
using MPP.Transporte;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Transporte
{
    public class BuqueBLL : IGestor<BEBuque>
    {
        private readonly BuqueMPP buqueMPP = new BuqueMPP();

        #region "Constructor estático"

        // Registro para reconstrucción perezosa desde otras entidades
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
                // Se validan los datos propios del buque antes de evaluar capacidad o asignaciones.
                ValidadorBLL.ObjetoNoNulo(obj, "Buque");
                ValidadorBLL.CampoObligatorio(obj.Nombre, "Nombre del buque");
                ValidadorBLL.CampoObligatorio(obj.CodigoIMO, "Código IMO");

                if (obj.CapacidadTEU <= 0)
                    throw new Exception("La capacidad TEU debe ser mayor a cero.");

                if (obj.CapacidadPeso <= 0)
                    throw new Exception("La capacidad de peso debe ser mayor a cero.");

                if (obj.ItinerarioPuertoIds == null || obj.ItinerarioPuertoIds.Count == 0)
                    throw new Exception("El buque debe tener al menos un puerto en su itinerario.");

                if (obj.ItinerarioPuertoIds.Any(id => id <= 0))
                    throw new Exception("El itinerario contiene un puerto sin Id válido.");

                obj.ItinerarioPuertoIds = obj.ItinerarioPuertoIds.Distinct().ToList();
                
                if (obj.Id == 0)
                    buqueMPP.Alta(obj);
                else
                    buqueMPP.Modificar(obj);

                // Se limpia la cache porque disponibilidad y ocupación dependen del estado actualizado del buque.
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

                foreach (var buque in lista)
                {
                    ReconstruirItinerario(buque);

                    // Indicador derivado: disponibilidad real por peso
                    buque.Disponibilidad = CalcularDisponibilidadPesoToneladas(buque.Id);
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
                if (buque == null)
                    return null;

                ReconstruirItinerario(buque);

                // Indicador derivado: disponibilidad real por peso
                buque.Disponibilidad = CalcularDisponibilidadPesoToneladas(buque.Id);

                return buque;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar buque.", ex);
            }
        }

        #endregion

        #region "Itinerario"

        private void ReconstruirItinerario(BEBuque buque)
        {
            if (buque.ItinerarioPuertoIds == null)
                buque.ItinerarioPuertoIds = new List<long>();

            buque.Itinerario = ReconstructorBLL.ReconstruirLista<BEDireccion>(
                buque.ItinerarioPuertoIds
            );
        }

        public bool RecorrePuerto(BEBuque buque, BEDireccion puerto)
        {
            if (buque == null || puerto == null || puerto.Id <= 0)
                return false;

            var completo = buque.Id > 0 ? ListarObjeto(new BEBuque { Id = buque.Id }) : buque;
            return completo != null &&
                   completo.ItinerarioPuertoIds != null &&
                   completo.ItinerarioPuertoIds.Contains(puerto.Id);
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

        #region "Ocupación (TEU)"

        // Regla de capacidad TEU usada para medir ocupación naviera:
        // 20' => 1 TEU
        // 40' y el resto => 2 TEU
        public int CalcularTeuPorContenedor(BEContenedor contenedor)
        {
            if (contenedor == null || contenedor.Tipo == null || string.IsNullOrEmpty(contenedor.Tipo.Codigo))
                return 1;

            return contenedor.Tipo.Codigo.StartsWith("20") ? 1 : 2;
        }

        public int CalcularTeuAsignados(long buqueId)
        {
            try
            {
                
                var contenedorMPP = new ContenedorMPP();
                var contenedores = contenedorMPP.Listar();

                return contenedores
                    .Where(c => c.Buque != null && c.Buque.Id == buqueId)
                    .Sum(c => CalcularTeuPorContenedor(c));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular TEU asignados al buque.", ex);
            }
        }

        public decimal CalcularPorcentajeOcupado(long buqueId)
        {
            try
            {
                var buque = buqueMPP.BuscarPorId(buqueId);
                if (buque == null || buque.CapacidadTEU <= 0)
                    return 0m;

                int usados = CalcularTeuAsignados(buqueId);
                decimal porcentaje = usados * 100m / buque.CapacidadTEU;

                return Math.Min(100m, Math.Max(0m, porcentaje));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular porcentaje de ocupación del buque.", ex);
            }
        }

        #endregion

        #region "Ocupación (Peso)"

        // Peso real asignado al buque (en toneladas)
        public decimal CalcularPesoAsignadoToneladas(long buqueId)
        {
            try
            {
                // IMPORTANTE: NO usar ContenedorBLL acá, porque reconstruye Buque y genera recursión.
                var contenedorMPP = new ContenedorMPP();
                var contenedores = contenedorMPP.Listar();

                // 1) Tomo los IDs de cargas de contenedores asignados a ESTE buque
                var cargaIds = contenedores
                    .Where(c => c.Buque != null && c.Buque.Id == buqueId)
                    .SelectMany(c => (c.Cargas ?? new List<BECarga>()).Select(ca => ca.Id))
                    .Where(id => id > 0)
                    .Distinct()
                    .ToList();

                // 2) Reconstruyo cargas completas (para tener PesoKg real)
                var cargas = ReconstructorBLL.ReconstruirLista<BECarga>(cargaIds);

                // 3) Sumo Kg reales y convierto a toneladas
                decimal pesoKg = cargas.Sum(c => c != null ? c.PesoKg : 0m);

                return pesoKg / 1000m; // tn
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular peso asignado al buque.", ex);
            }
        }



        // Disponibilidad real por peso (en toneladas)
        public decimal CalcularDisponibilidadPesoToneladas(long buqueId)
        {
            try
            {
                var buque = buqueMPP.BuscarPorId(buqueId);
                if (buque == null)
                    return 0m;

                decimal usado = CalcularPesoAsignadoToneladas(buqueId);
                decimal disponible = buque.CapacidadPeso - usado;

                return disponible < 0m ? 0m : disponible;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular disponibilidad de peso del buque.", ex);
            }
        }

        #endregion
    }
}
