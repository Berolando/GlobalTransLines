using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Transversal
{
    /// <summary>
    /// Servicio transversal de reconstrucción de entidades por Id.
    /// Permite obtener objetos completos sin generar dependencias directas entre BLLs.
    /// Funciona registrando funciones genéricas que saben cómo reconstruir cada tipo.
    /// </summary>
    public static class ReconstructorBLL
    {
        #region "Diccionarios internos"

        // Diccionario que guarda las funciones de reconstrucción registradas por tipo
        private static Dictionary<Type, Func<long, object>> resolutores = new Dictionary<Type, Func<long, object>>();

        // Cache de objetos ya reconstruidos (por tipo e Id)
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
        public static T Reconstruir<T>(long id) where T : class
        {
            if (id <= 0)
                return null;

            var tipo = typeof(T);

            // Si ya está en cache, se devuelve directamente (más rápido)
            if (cache.ContainsKey(tipo) && cache[tipo].ContainsKey(id))
                return cache[tipo][id] as T;

            // Si no hay una función registrada para este tipo, no se puede reconstruir
            if (!resolutores.ContainsKey(tipo))
                return null;

            // Ejecutamos la función registrada para este tipo
            var objeto = resolutores[tipo].Invoke(id) as T;

            // Si el objeto es válido, lo guardamos en cache
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
        /// Se recomienda llamarlo luego de guardar o modificar una entidad.
        /// Ejemplo:
        /// ReconstructorBLL.LimpiarCache<BECliente>();
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

        #region "Explicación del flujo (documentación interna)"

        /*
          Cómo funciona el ReconstructorBLL:

         1️ Registro de funciones
             - Cada módulo le enseña al Reconstructor cómo reconstruir sus entidades.
               Ejemplo:
                 ReconstructorBLL.Registrar<BECliente>(
                     id => new ClienteBLL().ListarObjeto(new BECliente { Id = id })
                 );

             - Esto solo se hace una vez, por tipo.

         2️ Uso normal
             - Si desde otra parte del sistema tenés un objeto con solo el Id (por ejemplo, una Cotización con Cliente.Id = 3),
               podés reconstruirlo así:
                 var cliente = ReconstructorBLL.Reconstruir<BECliente>(3);

         3️ Internamente
             - El Reconstructor busca si existe un "resolutor" registrado para ese tipo.
             - Si lo encuentra, ejecuta la función (por ejemplo, ClienteBLL.ListarObjeto).
             - Si el objeto no está en memoria, lo carga y lo guarda en el cache.

         4️ Cache
             - Si pedís el mismo objeto otra vez, lo devuelve desde memoria (sin volver a leer XML).
             - Esto mejora el rendimiento general del sistema.

         5️ Limpieza
             - Cuando una entidad cambia (por ejemplo, se guarda o modifica),
               la BLL correspondiente puede limpiar su cache:
                 ReconstructorBLL.LimpiarCache<BECliente>();

          Ventajas:
             - No hay dependencias directas entre BLLs.
             - Todas las entidades se reconstruyen de forma uniforme.
             - El sistema es más rápido, más limpio y más modular.
         */

        #endregion
    }
}
