using System;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

namespace BE.Dominio
{
    public static class EmpresaConfig
    {
        #region "Propiedades"

        public static string RazonSocial { get; private set; }
        public static string NombreFantasia { get; private set; }
        public static string CUIT { get; private set; }

        // CondicionFiscal (Tax Status / Condición Fiscal)
        public static global::BE.Dominio.CondicionFiscal CondicionFiscal { get; private set; }

        public static string DomicilioFiscal { get; private set; }
        public static string Localidad { get; private set; }
        public static string Provincia { get; private set; }
        public static string CodigoPostal { get; private set; }
        public static string Pais { get; private set; }

        public static string IngresosBrutos { get; private set; }
        public static DateTime InicioActividades { get; private set; }

        public static string Email { get; private set; }
        public static string SitioWeb { get; private set; }
        public static string Telefono { get; private set; }

        public static string PuntoVenta { get; private set; }

        #endregion

        #region "Ruta"
        private static readonly string ruta = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Config",
            "EmpresaConfig.xml"
        );
        #endregion

        #region "Inicialización"

        static EmpresaConfig()
        {
            try
            {
                AsegurarArchivo();

                var doc = XDocument.Load(ruta);
                var root = doc.Element("EmpresaConfig");
                if (root == null) throw new Exception("No se encontró el nodo raíz <EmpresaConfig>.");

                RazonSocial = ValStr(root.Element("RazonSocial"), "Empresa Desconocida");
                NombreFantasia = ValStr(root.Element("NombreFantasia"), RazonSocial);

                CUIT = ValStr(root.Element("CUIT"), "00-00000000-0");

                // Guardamos/Leemos CODIGO (RI/MT/EX/CF), no descripción
                string codigoCF = ValStr(
                    root.Element("CondicionFiscal"),
                    global::BE.Dominio.CondicionFiscal.ResponsableInscripto.Codigo
                );

                CondicionFiscal =
                    global::BE.Dominio.CondicionFiscal.ObtenerPorCodigo(codigoCF)
                    ?? global::BE.Dominio.CondicionFiscal.ResponsableInscripto;

                DomicilioFiscal = ValStr(root.Element("DomicilioFiscal"), "Domicilio no informado");
                Localidad = ValStr(root.Element("Localidad"), "CABA");
                Provincia = ValStr(root.Element("Provincia"), "Buenos Aires");
                CodigoPostal = ValStr(root.Element("CodigoPostal"), "0000");
                Pais = ValStr(root.Element("Pais"), "Argentina");

                IngresosBrutos = ValStr(root.Element("IngresosBrutos"), "901-123456-7");
                InicioActividades = ParseFecha(ValStr(root.Element("InicioActividades"), "2019-01-01"));

                Email = ValStr(root.Element("Email"), "facturacion@globaltranslines.com");
                SitioWeb = ValStr(root.Element("SitioWeb"), "www.globaltranslines.com");
                Telefono = ValStr(root.Element("Telefono"), "+54 11 5278-4400");

                PuntoVenta = ValStr(root.Element("PuntoVenta"), "0001");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar la configuración de empresa.", ex);
            }
        }

        private static void AsegurarArchivo()
        {
            if (File.Exists(ruta)) return;

            var defecto = new XElement("EmpresaConfig",
                new XElement("RazonSocial", "GlobalTrans Lines SRL"),
                new XElement("NombreFantasia", "GlobalTrans Lines"),

                new XElement("CUIT", "30-71583942-7"),

                // CODIGO: RI/MT/EX/CF
                new XElement("CondicionFiscal", global::BE.Dominio.CondicionFiscal.ResponsableInscripto.Codigo),

                new XElement("DomicilioFiscal", "Av. Alicia Moreau de Justo 740, Piso 9"),
                new XElement("Localidad", "CABA"),
                new XElement("Provincia", "Buenos Aires"),
                new XElement("CodigoPostal", "C1107AAB"),
                new XElement("Pais", "Argentina"),

                new XElement("IngresosBrutos", "901-123456-7"),
                new XElement("InicioActividades", "2019-01-01"),

                new XElement("Email", "facturacion@globaltranslines.com"),
                new XElement("SitioWeb", "www.globaltranslines.com"),
                new XElement("Telefono", "+54 11 5278-4400"),

                new XElement("PuntoVenta", "0001")
            );

            Directory.CreateDirectory(Path.GetDirectoryName(ruta));
            defecto.Save(ruta);
        }

        #endregion

        #region "Helpers"

        private static string ValStr(XElement el, string defecto)
        {
            var v = el != null ? (string)el : null;
            return string.IsNullOrWhiteSpace(v) ? defecto : v.Trim();
        }

        private static DateTime ParseFecha(string valor)
        {
            DateTime dt;

            if (DateTime.TryParseExact(valor, "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                return dt;

            if (DateTime.TryParse(valor, out dt))
                return dt;

            return new DateTime(2019, 1, 1);
        }

        #endregion
    }
}
