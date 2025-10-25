using System;

namespace BLL.Transversal
{
    public static class ValidadorBLL
    {
        #region "Validaciones básicas"

        // Valida que un texto no sea nulo ni vacío
        public static void CampoObligatorio(string valor, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new Exception($"El campo {nombreCampo} es obligatorio.");
        }

        // Valida que un objeto no sea nulo
        public static void ObjetoNoNulo(object objeto, string nombreObjeto)
        {
            if (objeto == null)
                throw new Exception($"Debe indicar un valor válido para {nombreObjeto}.");
        }

        // Valida que una lista tenga al menos un elemento
        public static void ListaNoVacia<T>(System.Collections.Generic.List<T> lista, string nombreLista)
        {
            if (lista == null || lista.Count == 0)
                throw new Exception($"La lista {nombreLista} no puede estar vacía.");
        }

        #endregion
    }
}
