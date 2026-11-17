namespace Programa2.ContadorLOC.Dominio;

using System;

/// <summary>
/// Excepción que se produce cuando el archivo analizado no cumple con el formato o estándar esperado.
/// </summary>
public class ArchivoInvalidoException : Exception
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ArchivoInvalidoException"/>.
    /// </summary>
    public ArchivoInvalidoException()
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ArchivoInvalidoException"/> con un mensaje especificado.
    /// </summary>
    /// <param name="mensaje">Mensaje descriptivo del error.</param>
    public ArchivoInvalidoException(string mensaje)
        : base(mensaje)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ArchivoInvalidoException"/> con un mensaje y una excepción interna.
    /// </summary>
    /// <param name="mensaje">Mensaje descriptivo del error.</param>
    /// <param name="excepcionInterna">Excepción que causó el error actual.</param>
    public ArchivoInvalidoException(string mensaje, Exception excepcionInterna)
        : base(mensaje, excepcionInterna)
    {
    }
}

