using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BE.Dominio;


namespace Transversales
{
    public static class GeneradorNumeros
    {
        #region "Generar número genérico"
        public static string GenerarNumero(Type tipoEntidad, long id)
        {
            string prefijo = tipoEntidad.Name.Replace("BE", "").ToUpper();
            string numero = id.ToString().PadLeft(5, '0');
            return $"{prefijo}-{numero}";
        }

        #endregion

        #region "Método auxiliar reutilizable"

        // Actualiza o crea la numeración en un XML y devuelve el nuevo número correlativo
        private static long ActualizarNumeracion(string ruta, string codigoTipo, string puntoVenta)
        {
            var doc = XDocument.Load(ruta);
            var nodo = doc.Descendants("Numeracion")
                          .FirstOrDefault(x =>
                              (string)x.Attribute("Tipo") == codigoTipo &&
                              (string)x.Attribute("PuntoVenta") == puntoVenta);

            long ultimo = 0;
            if (nodo != null)
                long.TryParse((string)nodo.Attribute("UltimoNumero"), out ultimo);

            long nuevo = ultimo + 1;

            if (nodo == null)
            {
                nodo = new XElement("Numeracion",
                    new XAttribute("Tipo", codigoTipo),
                    new XAttribute("PuntoVenta", puntoVenta),
                    new XAttribute("UltimoNumero", nuevo));
                doc.Root.Add(nodo);
            }
            else
            {
                nodo.SetAttributeValue("UltimoNumero", nuevo);
            }

            doc.Save(ruta);
            return nuevo;
        }

        #endregion

        #region "Generar número de factura por tipo"

        public static string GenerarNumeroFactura(TipoFactura tipoFactura, long id)
        {
            try
            {
                string codigoTipo = tipoFactura?.Codigo ?? "FA";
                string puntoVenta = "0001";
                string ruta = GeneradorRutas.UbicacionNumeracionFacturas();

                long nuevoNumero = ActualizarNumeracion(ruta, codigoTipo, puntoVenta);
                return $"{codigoTipo}{puntoVenta}-{nuevoNumero.ToString().PadLeft(8, '0')}";
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de numeraciones de facturas.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de numeraciones de facturas. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar número de factura en XML.", ex);
            }
        }

        #endregion

        #region "Verificar próximo número Factura"

        // Devuelve cuál será el próximo número sin incrementarlo realmente
        public static string ObtenerProximoNumeroFactura(TipoFactura tipoFactura)
        {
            try
            {
                string codigoTipo = tipoFactura?.Codigo ?? "FA";
                string puntoVenta = "0001";
                string ruta = GeneradorRutas.UbicacionNumeracionFacturas();

                var doc = XDocument.Load(ruta);
                var nodo = doc.Descendants("Numeracion")
                              .FirstOrDefault(x =>
                                  (string)x.Attribute("Tipo") == codigoTipo &&
                                  (string)x.Attribute("PuntoVenta") == puntoVenta);

                long ultimoNumero = 0;
                if (nodo != null)
                    long.TryParse((string)nodo.Attribute("UltimoNumero"), out ultimoNumero);

                long proximo = ultimoNumero + 1;
                return $"{codigoTipo}{puntoVenta}-{proximo.ToString().PadLeft(8, '0')}";
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de numeraciones de facturas (vista previa).", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de numeraciones de facturas (vista previa). Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener número de vista previa de factura.", ex);
            }
        }

        #endregion

        #region "Generar número de orden de compra"

        public static string GenerarNumeroOrdenCompra(long id)
        {
            try
            {
                string codigoTipo = "OC";
                string puntoVenta = "0001";
                string ruta = GeneradorRutas.UbicacionNumeracionOrdenCompra();

                long nuevoNumero = ActualizarNumeracion(ruta, codigoTipo, puntoVenta);
                return $"{codigoTipo}{puntoVenta}-{nuevoNumero.ToString().PadLeft(8, '0')}";
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML de numeraciones de órdenes de compra.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML de numeraciones de órdenes de compra. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar número de orden de compra en XML.", ex);
            }
        }

        #endregion

        #region "Generar número de booking"

        // Estructura: BK-{AÑO}-{CORRELATIVO}
        public static string GenerarNumeroBooking(long id)
        {
            try
            {
                string anio = DateTime.Now.Year.ToString();
                string correlativo = id.ToString().PadLeft(5, '0');
                return $"BK-{anio}-{correlativo}";
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML al generar número de booking.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML al generar número de booking. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar número de booking en XML.", ex);
            }
        }

        #endregion

        #region "Generar número de cotizacion"

        // Estructura: CTZ-{AÑO}-{CORRELATIVO}
        public static string GenerarNumeroCotizacion(long id)
        {
            try
            {
                string anio = DateTime.Now.Year.ToString();
                string correlativo = id.ToString().PadLeft(5, '0');
                return $"CTZ-{anio}-{correlativo}";
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML al generar número de booking.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML al generar número de booking. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar número de booking en XML.", ex);
            }
        }

        #endregion

        #region "Generar número de contenedor"

        // Estructura: GTLU + CódigoTipo + correlativo 
        public static string GenerarNumeroContenedor(string codigoTipo, long id)
        {
            try
            {
                string prefijoEmpresa = "GTLU";
                string tipo = !string.IsNullOrWhiteSpace(codigoTipo) ? codigoTipo.ToUpper() : "ST";
                string correlativo = id.ToString().PadLeft(5, '0');
                return $"{prefijoEmpresa}{tipo}{correlativo}";
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML al generar número de contenedor.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML al generar número de contenedor. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar número de contenedor en XML.", ex);
            }
        }

        #endregion

        #region "Generar número de buque"

        // Estructura: GTLB + correlativo
        public static string GenerarNumeroBuque(long id)
        {
            try
            {
                string prefijoEmpresa = "GTLB"; // B de Buque
                string correlativo = id.ToString().PadLeft(5, '0');
                return $"{prefijoEmpresa}{correlativo}";
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML al generar número de buque.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML al generar número de buque. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar número de buque en XML.", ex);
            }
        }

        #endregion

        #region "Generar número de carga"

        // Estructura: GTLC + correlativo
        public static string GenerarNumeroCarga(long id)
        {
            try
            {
                string prefijoEmpresa = "GTLC"; // C de Carga
                string correlativo = id.ToString().PadLeft(5, '0');
                return $"{prefijoEmpresa}{correlativo}";
            }
            catch (IOException ex)
            {
                throw new Exception("Error de acceso al archivo XML al generar número de carga.", ex);
            }
            catch (System.Xml.XmlException ex)
            {
                throw new Exception("Error en la estructura del archivo XML al generar número de carga. Línea "
                    + ex.LineNumber + ", posición " + ex.LinePosition + ". Detalle: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar número de carga en XML.", ex);
            }
        }

        #endregion
    }
}
