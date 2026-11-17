namespace Programa2.ContadorLOC.Dominio;

using System;

/// <summary>
/// Representa las métricas calculadas para una parte específica de código (clase, estructura, interfaz o registro).
/// </summary>
public sealed record MetricasParte
{
    /// <summary>
    /// Obtiene el nombre de la parte analizada.
    /// </summary>
    public string NombreParte { get; init; }

    /// <summary>
    /// Obtiene el número total de elementos (métodos y constructores) que pertenecen a la parte.
    /// </summary>
    public int NumeroElementos { get; init; }

    /// <summary>
    /// Obtiene el tamaño de la parte medido en líneas físicas de código contables (LOC).
    /// </summary>
    public int TamanioLOC { get; init; }

    /// <summary>
    /// Inicializa una nueva instancia del registro <see cref="MetricasParte"/>.
    /// </summary>
    /// <param name="nombreParte">Nombre de la parte analizada.</param>
    /// <param name="numeroElementos">Número de elementos detectados dentro de la parte.</param>
    /// <param name="tamanioLoc">Líneas de código contables correspondientes a la parte.</param>
    /// <exception cref="ArgumentException">Se lanza si el nombre de la parte es nulo o está en blanco.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Se lanza si el número de elementos o tamaño es negativo.</exception>
    public MetricasParte(string nombreParte, int numeroElementos, int tamanioLoc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nombreParte);
        ArgumentOutOfRangeException.ThrowIfNegative(numeroElementos);
        ArgumentOutOfRangeException.ThrowIfNegative(tamanioLoc);

        NombreParte = nombreParte.Trim();
        NumeroElementos = numeroElementos;
        TamanioLOC = tamanioLoc;
    }
}

