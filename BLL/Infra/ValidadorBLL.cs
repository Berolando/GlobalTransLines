using BE.Dominio;
using BE.Finanzas;
using BE.Transporte;
using BE.Infra;
using System;

namespace BLL.Infra
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
        #region "Validaciones numéricas"

        // Valida que un valor decimal sea mayor que cero
        public static void MayorQueCero(decimal valor, string nombreCampo)
        {
            if (valor <= 0)
                throw new Exception($"El campo {nombreCampo} debe ser mayor que cero.");
        }

        // Valida que un valor entero sea mayor que cero
        public static void MayorQueCero(int valor, string nombreCampo)
        {
            if (valor <= 0)
                throw new Exception($"El campo {nombreCampo} debe ser mayor que cero.");
        }

        // Valida que un valor double sea mayor que cero
        public static void MayorQueCero(double valor, string nombreCampo)
        {
            if (valor <= 0)
                throw new Exception($"El campo {nombreCampo} debe ser mayor que cero.");
        }

        #endregion
        #region "Validaciones de relaciones entre entidades"

        public static void ValidarCotizacionParaBooking(BECotizacion c, bool esPrevia = false)
        {
            if (c == null)
                throw new Exception("Debe seleccionar una cotización válida.");

            // Solo valida estado si no es un paso previo
            if (!esPrevia)
            {
                if (c.Estado != null &&
                   (c.Estado.Codigo == EstadoCotizacion.Rechazada.Codigo
                 || c.Estado.Codigo == EstadoCotizacion.Vencida.Codigo))
                    throw new Exception("No se puede generar un booking desde una cotización rechazada o vencida.");

                if (c.Estado == null || c.Estado.Codigo != EstadoCotizacion.Aceptada.Codigo)
                    throw new Exception("La cotización debe estar en estado Aceptada para generar un booking.");
            }

            if (c.Cargas == null || c.Cargas.Count == 0)
                throw new Exception("La cotización no tiene cargas asociadas.");
        }



        public static void ValidarCargaParaBooking(BECarga c)
        {
            if (c == null)
                throw new Exception("Carga inexistente o inválida.");

            if (c.Estado == null || c.Estado.Codigo != EstadoCarga.AceptadaCotizacion.Codigo)
                throw new Exception($"La carga {c.Codigo} ya no está disponible para asignar a un booking.");
        }

        #endregion
        #region "Validaciones de direcciones"
        public static void ValidarDireccion(BEDireccion direccion, string nombreCampo)
        {
            if (direccion == null)
                throw new Exception($"Debe indicar un valor válido para {nombreCampo}.");

            bool tienePuerto = !string.IsNullOrWhiteSpace(direccion.Puerto);

            bool tieneCalle = !string.IsNullOrWhiteSpace(direccion.Calle);
            bool tieneCiudad = !string.IsNullOrWhiteSpace(direccion.Ciudad);
            bool tienePais = !string.IsNullOrWhiteSpace(direccion.Pais);

            bool tieneNumero = !string.IsNullOrWhiteSpace(direccion.Numero);
            bool tieneProvincia = !string.IsNullOrWhiteSpace(direccion.Provincia);
            bool tieneCodigoPostal = !string.IsNullOrWhiteSpace(direccion.CodigoPostal);

            bool tieneOtrosCampos =
                tieneCalle ||
                tieneNumero ||
                tieneCiudad ||
                tieneProvincia ||
                tienePais ||
                tieneCodigoPostal;

            // Caso 1: no cargó nada.
            if (!tienePuerto && !tieneOtrosCampos)
                throw new Exception($"Debe completar al menos un dato de {nombreCampo}.");

            // Caso 2: puerto del catálogo.
            // Un puerto puede tener ciudad y país para mostrarse mejor, pero no requiere calle.
            if (direccion.EsPuerto && tienePuerto)
                return;

            // Caso 3: dirección común.
            // No obligo puerto, pero sí Calle + Ciudad + País.
            if (!tieneCalle || !tieneCiudad || !tienePais)
                throw new Exception($"Para {nombreCampo} debe completar Calle, Ciudad y País, o indicar solo un Puerto.");
        }
        #endregion
    }
}
