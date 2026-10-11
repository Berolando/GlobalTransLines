using Abstraccion;
using BE.Dominio;
using BE.Transporte;
using BLL.Infra;
using MPP.Transporte;
using System;
using System.Collections.Generic;
using System.Linq;

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
                
                // El contenedor debe tener tipo y cargas compatibles antes de persistirse.
                ValidadorBLL.ObjetoNoNulo(obj, "Contenedor");
                ValidadorBLL.ObjetoNoNulo(obj.Tipo, "Tipo de contenedor");

                // Valida capacidad según tipo y cargas asignadas
                ValidarCapacidad(obj);

                if (obj.Id == 0)
                    contenedorMPP.Alta(obj);
                else
                    contenedorMPP.Modificar(obj);

                // Se limpia la cache porque la ocupación y disponibilidad pueden cambiar.
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

                // En XML se guardan relaciones por Id; la BLL reconstruye las cargas completas.
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

        #region "Validaciones de capacidad"

        // Verifica que el peso y volumen total de las cargas no excedan la capacidad máxima del tipo de contenedor
        public void ValidarCapacidad(BEContenedor contenedor)
        {
            ValidadorBLL.ObjetoNoNulo(contenedor, "Contenedor");
            ValidadorBLL.ObjetoNoNulo(contenedor.Tipo, "Tipo de contenedor");

            if (contenedor.Cargas == null || contenedor.Cargas.Count == 0)
                return; // No hay cargas para validar

            decimal pesoTotal = contenedor.Cargas.Sum(c => c.PesoKg);
            decimal volumenTotal = contenedor.Cargas.Sum(c => c.VolumenM3);

            decimal capacidadPeso = contenedor.Tipo.CapacidadPesoKg;
            decimal capacidadVolumen = contenedor.Tipo.CapacidadVolumenM3;

            if (pesoTotal > capacidadPeso)
                throw new Exception($"El peso total ({pesoTotal} kg) supera la capacidad del contenedor ({capacidadPeso} kg).");

            if (volumenTotal > capacidadVolumen)
                throw new Exception($"El volumen total ({volumenTotal} m³) supera la capacidad del contenedor ({capacidadVolumen} m³).");
        }

        #endregion

        #region "Validaciones de disponibilidad"
        public void ValidarDisponibilidad(BEContenedor contenedor)
        {
            ValidadorBLL.ObjetoNoNulo(contenedor, "Contenedor");
            ValidadorBLL.ObjetoNoNulo(contenedor.Estado, "Estado del contenedor");

            // Solo se puede asignar si está disponible
            if (contenedor.Estado != EstadoContenedor.Disponible)
                throw new Exception(
                    $"El contenedor {contenedor.Codigo} no está disponible para asignación (Estado actual: {contenedor.Estado.Descripcion}).");
        }
        #endregion

        #region "Validaciones de compatibilidad"

        // Valida las combinaciones de carga y tipo físico habilitadas para el alcance actual.
        public void ValidarCompatibilidadTipos(TipoCarga tipoCarga, TipoContenedor tipoContenedor)
        {
            ValidadorBLL.ObjetoNoNulo(tipoCarga, "Tipo de carga");
            ValidadorBLL.ObjetoNoNulo(tipoContenedor, "Tipo de contenedor");

            string carga = tipoCarga.Codigo;
            string contenedor = tipoContenedor.Codigo;
            bool compatible = false;

            switch (carga)
            {
                case "FCL":
                case "GEN":
                    compatible = contenedor == "20STD" || contenedor == "40STD" || contenedor == "40HC";
                    break;
                case "REF":
                    compatible = contenedor == "20REF" || contenedor == "40REF";
                    break;
                case "GRA":
                    compatible = contenedor == "TANK";
                    break;
                case "PEL":
                    compatible = contenedor == "20STD" || contenedor == "40STD" ||
                                  contenedor == "40HC" || contenedor == "TANK";
                    break;
                case "LCL":
                    throw new Exception("La modalidad LCL está definida como evolución futura y no se encuentra operativa en esta versión.");
            }

            if (!compatible)
                throw new Exception("La combinación " + tipoCarga.Codigo + " + " + tipoContenedor.Codigo + " no está habilitada.");
        }

        // Devuelve las alternativas físicas habilitadas para un tipo de carga.
        public List<TipoContenedor> ObtenerTiposCompatibles(TipoCarga tipoCarga)
        {
            ValidadorBLL.ObjetoNoNulo(tipoCarga, "Tipo de carga");

            return TipoContenedor.ObtenerTodos()
                .Where(tipo =>
                {
                    try
                    {
                        ValidarCompatibilidadTipos(tipoCarga, tipo);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                })
                .ToList();
        }
        // Calcula el porcentaje de capacidad utilizado por una carga en un tipo de contenedor.
        public decimal CalcularOcupacion(TipoContenedor tipoContenedor, decimal pesoKg, decimal volumenM3)
        {
            ValidadorBLL.ObjetoNoNulo(tipoContenedor, "Tipo de contenedor");

            if (pesoKg <= 0)
                throw new Exception("El peso debe ser mayor a cero.");
            if (volumenM3 <= 0)
                throw new Exception("El volumen debe ser mayor a cero.");

            decimal usoPeso = (pesoKg / tipoContenedor.CapacidadPesoKg) * 100;
            decimal usoVolumen = (volumenM3 / tipoContenedor.CapacidadVolumenM3) * 100;
            return Math.Max(usoPeso, usoVolumen);
        }
        // Valida el margen operativo definido para planificar una carga individual.
        public decimal ValidarCapacidadPlanificada(TipoContenedor tipoContenedor, decimal pesoKg, decimal volumenM3)
        {
            decimal ocupacion = CalcularOcupacion(tipoContenedor, pesoKg, volumenM3);

            if (ocupacion > 90)
            {
                throw new Exception(
                    "La carga utiliza " + ocupacion.ToString("N1") +
                    "% de la capacidad del contenedor (" + tipoContenedor.Codigo +
                    "). Excede el 90% permitido.");
            }

            return ocupacion;
        }
        // Verifica que la carga sea compatible con el contenedor según el tipo y su restricción.
        public void ValidarCompatibilidad(BECarga carga, BEContenedor contenedor)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(carga, "Carga");
                ValidadorBLL.ObjetoNoNulo(contenedor, "Contenedor");
                ValidadorBLL.ObjetoNoNulo(carga.TipoContenedor, "Tipo de contenedor requerido por la carga");
                ValidadorBLL.ObjetoNoNulo(contenedor.Tipo, "Tipo de contenedor");

                var tipoRequerido = carga.TipoContenedor;   // EJ: 20STD, 40HC, 20REF…
                var tipoContenedor = contenedor.Tipo;

                ValidarCompatibilidadTipos(carga.Tipo, tipoRequerido);

                // La compatibilidad se compara por código para evitar depender del texto visible.
                if (!string.Equals(tipoRequerido.Codigo, tipoContenedor.Codigo))
                {
                    throw new Exception(
                        "Incompatibilidad de tipo: la carga requiere un contenedor " +
                        tipoRequerido.Codigo +
                        " pero el contenedor seleccionado es " +
                        tipoContenedor.Codigo + "."
                    );
                }

                // Validación de RESTRICCIÓN     (Refrigerado, Líquidos, Sobredimensionado…)
                var restriccionRequerida = carga.Tipo.RestriccionRequerida;
                var restriccionContenedor = contenedor.Restriccion ?? RestriccionContenedor.Ninguna;

                if (restriccionRequerida.Codigo != RestriccionContenedor.Ninguna.Codigo &&
                    restriccionRequerida.Codigo != restriccionContenedor.Codigo)
                {
                    throw new Exception(
                        "Incompatibilidad de restricción: la carga requiere un contenedor con " +
                        restriccionRequerida.Descripcion +
                        " pero el contenedor seleccionado tiene " +
                        restriccionContenedor.Descripcion + "."
                    );
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar la compatibilidad entre carga y contenedor.", ex);
            }
        }

        #endregion


        #region "Cálculos y Evaluaciones"

        // Calcula la ocupación real del contenedor y clasifica su uso como vacío, FCL o LCL.
        public decimal EvaluarOcupacion(BEContenedor contenedor)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(contenedor, "Contenedor");
                ValidadorBLL.ObjetoNoNulo(contenedor.Tipo, "Tipo de contenedor");

                if (contenedor.Cargas == null || contenedor.Cargas.Count == 0)
                {
                    contenedor.Estado = EstadoContenedor.Disponible;
                    return 0;
                }

                // Totales de peso y volumen
                decimal pesoTotal = contenedor.Cargas.Sum(c => c.PesoKg);
                decimal volumenTotal = contenedor.Cargas.Sum(c => c.VolumenM3);

                // Capacidades del tipo
                decimal capacidadPeso = contenedor.Tipo.CapacidadPesoKg;
                decimal capacidadVolumen = contenedor.Tipo.CapacidadVolumenM3;

                // Porcentajes parciales
                decimal porcentajePeso = (pesoTotal / capacidadPeso) * 100;
                decimal porcentajeVolumen = (volumenTotal / capacidadVolumen) * 100;
                decimal porcentajeOcupacion = (porcentajePeso + porcentajeVolumen) / 2;

                // Determinación del tipo de uso
                if (cargasMultiples(contenedor))
                    contenedor.Estado = EstadoContenedor.Consolidado; // LCL
                else if (porcentajeOcupacion >= 90)
                    contenedor.Estado = EstadoContenedor.Asignado; // FCL
                else if (porcentajeOcupacion > 0)
                    contenedor.Estado = EstadoContenedor.Asignado; // Parcial
                else
                    contenedor.Estado = EstadoContenedor.Disponible; // Vacío

                return porcentajeOcupacion;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al evaluar la ocupación del contenedor.", ex);
            }
        }

        // Sobrecarga para evaluar ocupación con una lista externa de cargas
        public decimal EvaluarOcupacion(BEContenedor contenedor, List<BECarga> cargas)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(contenedor, "Contenedor");
                ValidadorBLL.ObjetoNoNulo(contenedor.Tipo, "Tipo de contenedor");
                ValidadorBLL.ListaNoVacia(cargas, "Cargas a evaluar");

                // Contenedor temporal para no alterar el objeto original
                var contenedorTemporal = new BEContenedor
                {
                    Id = contenedor.Id,
                    Codigo = contenedor.Codigo,
                    Tipo = contenedor.Tipo,
                    Buque = contenedor.Buque,
                    Estado = contenedor.Estado,
                    Cargas = cargas
                };

                return EvaluarOcupacion(contenedorTemporal);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al evaluar la ocupación del contenedor con lista externa.", ex);
            }
        }

        // Actualiza automáticamente el estado del contenedor según su nivel de ocupación y cantidad de cargas
        public void ActualizarEstadoAutomatico(BEContenedor contenedor)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(contenedor, "Contenedor");
                ValidadorBLL.ObjetoNoNulo(contenedor.Tipo, "Tipo de contenedor");

                if (contenedor.Cargas == null || contenedor.Cargas.Count == 0)
                {
                    contenedor.Estado = EstadoContenedor.Disponible;
                    return;
                }

                decimal ocupacion = EvaluarOcupacion(contenedor);

                if (contenedor.Cargas.Count > 1)
                    contenedor.Estado = EstadoContenedor.Consolidado;
                else if (ocupacion >= 90)
                    contenedor.Estado = EstadoContenedor.Asignado;
                else if (ocupacion > 0)
                    contenedor.Estado = EstadoContenedor.Asignado;
                else
                    contenedor.Estado = EstadoContenedor.Disponible;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el estado automático del contenedor.", ex);
            }
        }

        // Método auxiliar para verificar múltiples cargas (LCL)
        private bool cargasMultiples(BEContenedor contenedor)
        {
            return contenedor.Cargas != null && contenedor.Cargas.Count > 1;
        }

        #endregion




    }
}
