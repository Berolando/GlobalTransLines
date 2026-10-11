using System;
using System.Collections.Generic;
using System.Linq;
using Abstraccion;
using BE.Dominio;
using BE.Finanzas;
using BLL.Infra;
using BLL.Transporte;
using MPP.Finanzas;

namespace BLL.Finanzas
{
    public class TarifarioBLL : IGestor<BETarifa>
    {
        private readonly TarifarioMPP mpp = new TarifarioMPP();
        private readonly ContenedorBLL contenedorBLL = new ContenedorBLL();

        #region "Guardar"

        // Crea una combinación tarifaria o modifica únicamente sus valores comerciales.
        public void Guardar(BETarifa obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Tarifa");
                ValidadorBLL.ObjetoNoNulo(obj.TipoCarga, "Tipo de carga");
                ValidadorBLL.ObjetoNoNulo(obj.TipoContenedor, "Tipo de contenedor");
                ValidadorBLL.CampoObligatorio(obj.Descripcion, "Descripción");

                ValidarValores(obj);
                contenedorBLL.ValidarCompatibilidadTipos(obj.TipoCarga, obj.TipoContenedor);

                var tarifas = mpp.Listar();
                var mismaCombinacion = tarifas.FirstOrDefault(t =>
                    t.TipoCarga != null &&
                    t.TipoContenedor != null &&
                    t.TipoCarga.Codigo == obj.TipoCarga.Codigo &&
                    t.TipoContenedor.Codigo == obj.TipoContenedor.Codigo);

                if (obj.Id == 0)
                {
                    if (mismaCombinacion != null)
                    {
                        string accion = mismaCombinacion.Activo ? "modificarla" : "reactivarla";
                        throw new Exception("Ya existe una tarifa para esa combinación. Debe seleccionarla para " + accion + ".");
                    }

                    obj.Activo = true;
                    mpp.Alta(obj);
                    return;
                }

                var persistida = tarifas.FirstOrDefault(t => t.Id == obj.Id);
                if (persistida == null)
                    throw new Exception("La tarifa seleccionada no existe.");

                // La combinación identifica la regla y no puede cambiarse durante una modificación.
                if (persistida.TipoCarga.Codigo != obj.TipoCarga.Codigo ||
                    persistida.TipoContenedor.Codigo != obj.TipoContenedor.Codigo)
                {
                    throw new Exception("No se puede cambiar la combinación de una tarifa existente. Debe crear una nueva tarifa.");
                }

                mpp.Modificar(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar tarifa en BLL.", ex);
            }
        }

        private void ValidarValores(BETarifa tarifa)
        {
            if (tarifa.BaseUSD < 0 || tarifa.CoefPeso < 0 || tarifa.CoefVolumen < 0)
                throw new Exception("Los valores tarifarios no pueden ser negativos.");

            if (tarifa.BaseUSD == 0 && tarifa.CoefPeso == 0 && tarifa.CoefVolumen == 0)
                throw new Exception("Al menos un valor tarifario debe ser mayor que cero.");
        }

        #endregion

        #region "Desactivar y reactivar"

        // Realiza la baja lógica para conservar la trazabilidad de las tarifas utilizadas.
        public void Eliminar(BETarifa obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Tarifa");
                var persistida = ListarObjeto(obj);

                if (persistida == null)
                    throw new Exception("La tarifa seleccionada no existe.");

                mpp.Baja(persistida);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desactivar tarifa en BLL.", ex);
            }
        }

        public void Reactivar(BETarifa obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Tarifa");
                var persistida = ListarObjeto(obj);

                if (persistida == null)
                    throw new Exception("La tarifa seleccionada no existe.");

                persistida.Activo = true;
                mpp.Modificar(persistida);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al reactivar tarifa en BLL.", ex);
            }
        }

        #endregion

        #region "Listar"

        // La administración muestra activas e inactivas para permitir reactivación.
        public List<BETarifa> ListarTodo()
        {
            try
            {
                return mpp.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar tarifas en BLL.", ex);
            }
        }

        public List<BETarifa> ListarActivas()
        {
            return ListarTodo().Where(t => t.Activo).ToList();
        }

        public BETarifa ListarObjeto(BETarifa obj)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(obj, "Tarifa");
                return ListarTodo().Find(t => t.Id == obj.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar tarifa en BLL.", ex);
            }
        }

        public long ObtenerUltimoId()
        {
            try
            {
                return mpp.ObtenerUltimoId();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener último Id de tarifa.", ex);
            }
        }

        #endregion

        #region "Cálculo de tarifa"

        // Calcula base + toneladas por valor unitario + metros cúbicos por valor unitario.
        public decimal CalcularTarifa(TipoCarga tipoCarga, TipoContenedor tipoContenedor, decimal pesoKg, decimal volumenM3)
        {
            try
            {
                if (pesoKg < 0 || volumenM3 < 0)
                    throw new Exception("El peso y el volumen no pueden ser negativos.");

                var tarifa = ObtenerTarifaPara(tipoCarga, tipoContenedor);
                decimal pesoToneladas = pesoKg / 1000m;

                return tarifa.BaseUSD +
                       (pesoToneladas * tarifa.CoefPeso) +
                       (volumenM3 * tarifa.CoefVolumen);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular tarifa en BLL.", ex);
            }
        }

        public BETarifa ObtenerTarifaPara(TipoCarga tipoCarga, TipoContenedor tipoContenedor)
        {
            try
            {
                ValidadorBLL.ObjetoNoNulo(tipoCarga, "Tipo de carga");
                ValidadorBLL.ObjetoNoNulo(tipoContenedor, "Tipo de contenedor");
                contenedorBLL.ValidarCompatibilidadTipos(tipoCarga, tipoContenedor);

                var tarifa = mpp.Listar().FirstOrDefault(t =>
                    t.Activo &&
                    t.TipoCarga != null &&
                    t.TipoContenedor != null &&
                    t.TipoCarga.Codigo == tipoCarga.Codigo &&
                    t.TipoContenedor.Codigo == tipoContenedor.Codigo);

                if (tarifa == null)
                    throw new Exception("No existe una tarifa activa para la combinación solicitada.");

                return tarifa;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener tarifa vigente.", ex);
            }
        }

        #endregion
    }
}