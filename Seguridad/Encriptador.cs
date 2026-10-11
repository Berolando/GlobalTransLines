using Seguridad;
using System;
using System.Text;

/// <summary>
/// Proporciona cifrado simetrico basico para evitar almacenar contrasenas en texto plano.
/// </summary>
public static class Encriptador
{
    /// <summary>
    /// Encripta un texto plano usando una clave numerica aleatoria.
    /// Devuelve una tupla con la clave y el texto cifrado.
    /// </summary>
    public static (byte Clave, string TextoCifrado) Encriptar(string textoOriginal)
    {
        try
        {
            if (string.IsNullOrEmpty(textoOriginal))
                return (0, string.Empty);

            byte clave = (byte)new Random().Next(1, 255);
            byte[] bytesOriginales = Encoding.UTF8.GetBytes(textoOriginal);
            byte[] bytesCifrados = new byte[bytesOriginales.Length];

            for (int indice = 0; indice < bytesOriginales.Length; indice++)
            {
                bytesCifrados[indice] = (byte)(bytesOriginales[indice] ^ clave);
            }

            return (clave, Convert.ToBase64String(bytesCifrados));
        }
        catch (Exception ex)
        {
            throw new EncriptacionException($"Error durante la encriptación del texto: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Desencripta el texto previamente cifrado usando la misma clave.
    /// </summary>
    public static string Desencriptar(string textoCifrado, byte clave)
    {
        try
        {
            if (string.IsNullOrEmpty(textoCifrado))
                return string.Empty;

            byte[] bytesCifrados = Convert.FromBase64String(textoCifrado);
            byte[] bytesOriginales = new byte[bytesCifrados.Length];

            for (int indice = 0; indice < bytesCifrados.Length; indice++)
            {
                bytesOriginales[indice] = (byte)(bytesCifrados[indice] ^ clave);
            }

            return Encoding.UTF8.GetString(bytesOriginales);
        }
        catch (FormatException ex)
        {
            throw new EncriptacionException("El texto cifrado no tiene un formato Base64 válido", ex);
        }
        catch (Exception ex)
        {
            throw new EncriptacionException($"Error durante la desencriptación del texto: {ex.Message}", ex);
        }
    }
}
