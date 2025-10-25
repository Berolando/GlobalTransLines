using System;
using System.Collections.Generic;
using BE.Enums;
using BE.Finanzas;

namespace MPP.Utilidades
{
    public static class GeneradorNumeros
    {
        public class ConfiguracionNumeracion
        {
            public string Prefijo { get; set; }
            public int Longitud { get; set; } = 3;
            public bool UsarCerosIzquierda { get; set; } = true;
        }

        private static readonly Dictionary<Type, ConfiguracionNumeracion> configuraciones =
            new Dictionary<Type, ConfiguracionNumeracion>
        {
            // Entidades existentes
            {
                typeof(BE.Transporte.BEManifiesto),
                new ConfiguracionNumeracion { Prefijo = "MF", Longitud = 4 }
            },
            {
                typeof(BE.BEBuque),
                new ConfiguracionNumeracion { Prefijo = "BQ", Longitud = 5 }
            },
            {
                typeof(BE.BEContenedor),
                new ConfiguracionNumeracion { Prefijo = "CT", Longitud = 6 }
            },
            
            // Nuevas entidades de Finanzas
            {
                typeof(BEFactura),
                new ConfiguracionNumeracion { Prefijo = "FC", Longitud = 6 }
            },
            {
                typeof(BEOrdenDeCompra),
                new ConfiguracionNumeracion { Prefijo = "OC", Longitud = 5 }
            },
            
            // Otras entidades
            {
                typeof(BE.BEBillofLading),
                new ConfiguracionNumeracion { Prefijo = "BL", Longitud = 5 }
            },
            {
                typeof(BE.BEBooking),
                new ConfiguracionNumeracion { Prefijo = "BK", Longitud = 4 }
            }
        };

        public static string GenerarNumero(Type tipoEntidad, long id)
        {
            if (configuraciones.TryGetValue(tipoEntidad, out ConfiguracionNumeracion config))
            {
                string numero = config.UsarCerosIzquierda
                    ? id.ToString().PadLeft(config.Longitud, '0')
                    : id.ToString();

                return $"{config.Prefijo}-{numero}";
            }

            // Configuración por defecto si no se encuentra
            string prefijoDefault = tipoEntidad.Name.Replace("BE", "").ToUpper();
            return $"{prefijoDefault}-{id.ToString().PadLeft(3, '0')}";
        }

        // **MÉTODO ESPECÍFICO PARA FACTURAS CON TIPO - COMPATIBLE C# 7.3**
        public static string GenerarNumeroFactura(TipoFacturaEnum tipoFactura, long id)
        {
            string prefijo;

            // Usando switch tradicional en lugar de switch expression
            switch (tipoFactura)
            {
                case TipoFacturaEnum.A:
                    prefijo = "FCA";
                    break;
                case TipoFacturaEnum.B:
                    prefijo = "FCB";
                    break;
                case TipoFacturaEnum.C:
                    prefijo = "FCC";
                    break;
                case TipoFacturaEnum.E:
                    prefijo = "FCE";
                    break;
                default:
                    prefijo = "FC";
                    break;
            }

            return $"{prefijo}-{id.ToString().PadLeft(6, '0')}";
        }

        // Método alternativo usando if-else (también compatible)
        public static string GenerarNumeroFacturaAlternativo(TipoFacturaEnum tipoFactura, long id)
        {
            string prefijo = "FC"; // Valor por defecto

            if (tipoFactura == TipoFacturaEnum.A)
                prefijo = "FCA";
            else if (tipoFactura == TipoFacturaEnum.B)
                prefijo = "FCB";
            else if (tipoFactura == TipoFacturaEnum.C)
                prefijo = "FCC";
            else if (tipoFactura == TipoFacturaEnum.E)
                prefijo = "FCE";

            return $"{prefijo}-{id.ToString().PadLeft(6, '0')}";
        }

        // Método para agregar configuraciones dinámicamente
        public static void ConfigurarEntidad(Type tipoEntidad, string prefijo, int longitud = 3)
        {
            configuraciones[tipoEntidad] = new ConfiguracionNumeracion
            {
                Prefijo = prefijo,
                Longitud = longitud
            };
        }

        // Método para obtener el próximo número (útil para vistas previas)
        public static string ObtenerProximoNumero(Type tipoEntidad, long proximoId)
        {
            return GenerarNumero(tipoEntidad, proximoId);
        }
    }
}