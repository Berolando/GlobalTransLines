using System;
using System.Collections.Generic;

namespace BLL.Infra
{
    /// <summary>
    /// Servicio transversal de reconstrucción de entidades por Id.
    /// Permite obtener objetos completos sin generar dependencias directas entre BLLs.
    /// Funciona registrando funciones genéricas que saben cómo reconstruir cada tipo.
    /// </summary>
    public static class ReconstructorBLL
    {
        #region "Diccionarios internos"

        // Guarda, por cada tipo de entidad, la función que sabe reconstruirla desde su Id.
        private static Dictionary<Type, Func<long, object>> resolutores = new Dictionary<Type, Func<long, object>>();


        // Evita reconstruir varias veces el mismo objeto durante una misma ejecución.
        private static Dictionary<Type, Dictionary<long, object>> cache = new Dictionary<Type, Dictionary<long, object>>();

        #endregion

        #region "Registro de funciones"

        /// <summary>
        /// Registra la función que indica cómo reconstruir un tipo T a partir de su Id.
        /// Ejemplo de uso:
        /// ReconstructorBLL.Registrar<BECliente>(id => new ClienteBLL().ListarObjeto(new BECliente { Id = id }));
        /// </summary>
        public static void Registrar<T>(Func<long, T> funcionReconstruccion)
        {
            if (funcionReconstruccion == null)
                throw new ArgumentNullException(nameof(funcionReconstruccion));

            resolutores[typeof(T)] = id => funcionReconstruccion(id);
        }

        #endregion

        #region "Reconstrucción de entidades"

        /// <summary>
        /// Devuelve una entidad reconstruida por Id.
        /// Usa cache si el objeto ya fue cargado anteriormente.
        /// </summary>
        // Método central: recibe un Id y devuelve el objeto completo si existe un resolutor registrado.
        public static T Reconstruir<T>(long id) where T : class
        {
            if (id <= 0)
                return null;

            var tipo = typeof(T);

            // Si ya está en cache, lo devuelve sin volver a leer XML.
            if (cache.ContainsKey(tipo) && cache[tipo].ContainsKey(id))
                return cache[tipo][id] as T;

            // Si no hay una función registrada para este tipo, no se puede reconstruir
            if (!resolutores.ContainsKey(tipo))
                return null;

            // Ejecuta el resolutor registrado para este tipo de entidad.
            var objeto = resolutores[tipo].Invoke(id) as T;

            // Si el objeto es válido, lo guarda en cache
            if (objeto != null)
            {
                if (!cache.ContainsKey(tipo))
                    cache[tipo] = new Dictionary<long, object>();

                cache[tipo][id] = objeto;
            }

            return objeto;
        }

        /// <summary>
        /// Devuelve una lista de entidades reconstruidas a partir de una lista de Ids.
        /// </summary>
        public static List<T> ReconstruirLista<T>(IEnumerable<long> ids) where T : class
        {
            if (ids == null)
                return new List<T>();

            var resultado = new List<T>();

            foreach (var id in ids)
            {
                var obj = Reconstruir<T>(id);
                if (obj != null)
                    resultado.Add(obj);
            }

            return resultado;
        }

        #endregion

        #region "Mantenimiento de cache"

        /// <summary>
        /// Limpia el cache de un tipo específico.
        /// Se usa luego de  guardar o modificar una entidad.
        /// </summary>
        public static void LimpiarCache<T>()
        {
            var tipo = typeof(T);
            if (cache.ContainsKey(tipo))
                cache.Remove(tipo);
        }

        /// <summary>
        /// Limpia completamente todo el cache de reconstrucciones en memoria.
        /// Útil si se quiere forzar una recarga total.
        /// </summary>
        public static void LimpiarTodo()
        {
            cache.Clear();
        }

        #endregion

        #region "Explicación funcionamiento "

        /*
            ¿Para qué existe esta clase?

            En los XML las relaciones se guardan solo por Id.
            Ejemplo: una Cotización guarda ClienteId = 3, no todo el cliente completo.

            El Reconstructor permite convertir ese Id en el objeto completo cuando la BLL lo necesita,
            sin obligar a que todas las BLL dependan directamente entre sí.

            Flujo:
            1. Al iniciar el sistema, cada módulo registra cómo reconstruir su entidad.
            2. Cuando otra clase necesita el objeto completo, llama a Reconstruir<T>(id).
            3. El Reconstructor busca la función registrada para ese tipo.
            4. Si encuentra el objeto, lo guarda en cache para no leer XML repetidas veces.

            Ejemplo:
            ReconstructorBLL.Registrar<BECliente>(
                id => new ClienteBLL().ListarObjeto(new BECliente { Id = id })
            );

            Uso:
            var cliente = ReconstructorBLL.Reconstruir<BECliente>(3);

            Importante:
            Si una entidad se modifica, su BLL debe limpiar la cache de ese tipo.
  
         */

        #endregion
    }
}
