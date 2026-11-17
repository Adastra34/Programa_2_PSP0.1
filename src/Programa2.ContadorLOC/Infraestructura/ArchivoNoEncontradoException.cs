namespace Programa2.ContadorLOC.Infraestructura;

using System;

/// <summary>
/// Excepción que se produce cuando una ruta o un archivo solicitado no existe en el sistema de archivos.
/// </summary>
public class ArchivoNoEncontradoException : Exception
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ArchivoNoEncontradoException"/>.
    /// </summary>
    public ArchivoNoEncontradoException()
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ArchivoNoEncontradoException"/> con un mensaje especificado.
    /// </summary>
    /// <param name="mensaje">Mensaje descriptivo del error.</param>
    public ArchivoNoEncontradoException(string mensaje)
        : base(mensaje)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ArchivoNoEncontradoException"/> con un mensaje y una excepción interna.
    /// </summary>
    /// <param name="mensaje">Mensaje descriptivo del error.</param>
    /// <param name="excepcionInterna">Excepción que causó el error actual.</param>
    public ArchivoNoEncontradoException(string mensaje, Exception excepcionInterna)
        : base(mensaje, excepcionInterna)
    {
    }
}

