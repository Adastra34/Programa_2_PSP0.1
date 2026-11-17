namespace Programa2.ContadorLOC.Infraestructura;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Programa2.ContadorLOC.Dominio;

/// <summary>
/// Proporciona operaciones para la búsqueda y lectura de archivos de código fuente C#.
/// </summary>
public static class LectorArchivosCodigo
{
    private const string ExtensionCodigoCs = ".cs";

    /// <summary>
    /// Busca y retorna la lista de rutas de archivos de código fuente (.cs) en una ruta especificada.
    /// </summary>
    /// <param name="ruta">Ruta al archivo individual o al directorio a explorar.</param>
    /// <returns>Colección de rutas ordenadas de los archivos encontrados.</returns>
    /// <exception cref="ArgumentException">Se lanza si la ruta proporcionada es nula o vacía.</exception>
    /// <exception cref="ArchivoNoEncontradoException">Se lanza si la ruta no existe en el sistema de archivos.</exception>
    /// <exception cref="ArchivoInvalidoException">Se lanza si el archivo o carpeta no contiene código C# válido.</exception>
    public static IReadOnlyList<string> ObtenerArchivosFuente(string ruta)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ruta);

        string rutaNormalizada = ruta.Trim().Trim('"', '\'');

        if (File.Exists(rutaNormalizada))
        {
            return ProcesarArchivoIndividual(rutaNormalizada);
        }

        if (Directory.Exists(rutaNormalizada))
        {
            return ProcesarDirectorio(rutaNormalizada);
        }

        throw new ArchivoNoEncontradoException($"No se encontró la ruta o archivo: '{rutaNormalizada}'.");
    }

    /// <summary>
    /// Lee todas las líneas de un archivo de texto utilizando codificación UTF-8 de forma segura.
    /// </summary>
    /// <param name="rutaArchivo">Ruta del archivo a leer.</param>
    /// <returns>Arreglo con todas las líneas de texto del archivo.</returns>
    /// <exception cref="ArgumentException">Se lanza si la ruta del archivo es nula o vacía.</exception>
    /// <exception cref="ArchivoNoEncontradoException">Se lanza si el archivo físico no existe.</exception>
    /// <exception cref="ArchivoInvalidoException">Se lanza si ocurre un error de E/S o permisos al leer.</exception>
    public static IReadOnlyList<string> LeerLineas(string rutaArchivo)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(rutaArchivo);

        string rutaNormalizada = rutaArchivo.Trim().Trim('"', '\'');
        if (!File.Exists(rutaNormalizada))
        {
            throw new ArchivoNoEncontradoException($"No se pudo abrir el archivo porque no existe: '{rutaNormalizada}'.");
        }

        try
        {
            return File.ReadAllLines(rutaNormalizada, Encoding.UTF8);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            throw new ArchivoInvalidoException($"Error de lectura en el archivo '{rutaNormalizada}': {ex.Message}", ex);
        }
    }

    private static IReadOnlyList<string> ProcesarArchivoIndividual(string rutaNormalizada)
    {
        if (!rutaNormalizada.EndsWith(ExtensionCodigoCs, StringComparison.OrdinalIgnoreCase))
        {
            throw new ArchivoInvalidoException($"El archivo '{rutaNormalizada}' no tiene la extensión esperada ({ExtensionCodigoCs}).");
        }

        return new[] { Path.GetFullPath(rutaNormalizada) };
    }

    private static IReadOnlyList<string> ProcesarDirectorio(string rutaNormalizada)
    {
        string[] archivos = Directory.GetFiles(rutaNormalizada, "*" + ExtensionCodigoCs, SearchOption.AllDirectories);
        if (archivos.Length == 0)
        {
            throw new ArchivoInvalidoException($"No se encontraron archivos con extensión '{ExtensionCodigoCs}' en '{rutaNormalizada}'.");
        }

        Array.Sort(archivos, StringComparer.OrdinalIgnoreCase);
        return archivos;
    }
}

