using Seguridad;
using System.Text;

using System;

/// <summary>
/// Proporciona métodos para encriptar y desencriptar contraseñas usando XOR con clave dinámica.
/// </summary>
public static class Encriptador
{
    /// <summary>
    /// Encripta un texto plano usando una clave numérica aleatoria.
    /// Devuelve una tupla con la clave y el texto cifrado.
    /// </summary>
    public static (byte Clave, string TextoCifrado) Encriptar(string textoOriginal)
    {
        try
        {
            // Verifica si el texto de entrada es nulo o vacío
            if (string.IsNullOrEmpty(textoOriginal))
                // Retorna valores por defecto si no hay texto para encriptar
                return (0, string.Empty);

            // Genera una clave aleatoria en el rango de 1 a 255
            byte clave = (byte)new Random().Next(1, 255);

            // Convierte el texto original a un array de bytes usando codificación UTF-8
            byte[] bytesOriginales = Encoding.UTF8.GetBytes(textoOriginal);

            // Crea un array para almacenar el resultado de la encriptación
            byte[] bytesCifrados = new byte[bytesOriginales.Length];

            // Recorre cada byte del texto original para aplicar la operación XOR
            for (int indice = 0; indice < bytesOriginales.Length; indice++)
            {
                // Aplica operación XOR entre el byte original y la clave
                bytesCifrados[indice] = (byte)(bytesOriginales[indice] ^ clave);
            }

            // Convierte el array de bytes cifrados a una cadena Base64 y retorna con la clave
            return (clave, Convert.ToBase64String(bytesCifrados));
        }
        catch (Exception ex)
        {
            // Lanza excepción personalizada con contexto específico de encriptación
            throw new EncriptacionException($"Error durante la encriptación del texto: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Desencripta un texto previamente cifrado con XOR usando la misma clave.
    /// </summary>
    public static string Desencriptar(string textoCifrado, byte clave)
    {
        try
        {
            // Verifica si el texto cifrado es nulo o vacío
            if (string.IsNullOrEmpty(textoCifrado))
                // Retorna cadena vacía si no hay texto para desencriptar
                return string.Empty;

            // Convierte la cadena Base64 de vuelta a un array de bytes
            byte[] bytesCifrados = Convert.FromBase64String(textoCifrado);

            // Crea un array para almacenar el resultado de la desencriptación
            byte[] bytesOriginales = new byte[bytesCifrados.Length];

            // Recorre cada byte del texto cifrado para aplicar la operación XOR inversa
            for (int indice = 0; indice < bytesCifrados.Length; indice++)
            {
                // Aplica operación XOR entre el byte cifrado y la clave (misma operación que encriptación)
                bytesOriginales[indice] = (byte)(bytesCifrados[indice] ^ clave);
            }

            // Convierte el array de bytes desencriptados de vuelta a texto UTF-8
            return Encoding.UTF8.GetString(bytesOriginales);
        }
        catch (FormatException ex)
        {
            // Lanza excepción personalizada para formato Base64 inválido
            throw new EncriptacionException("El texto cifrado no tiene un formato Base64 válido", ex);
        }
        catch (Exception ex)
        {
            // Lanza excepción personalizada con contexto específico de desencriptación
            throw new EncriptacionException($"Error durante la desencriptación del texto: {ex.Message}", ex);
        }
    }
}