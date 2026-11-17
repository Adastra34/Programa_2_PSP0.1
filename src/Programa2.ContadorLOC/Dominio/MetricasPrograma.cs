namespace Programa2.ContadorLOC.Dominio;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Contenedor de las métricas consolidadas de un programa analizado según el estándar SEI PSP1.
/// </summary>
public sealed class MetricasPrograma
{
    private readonly List<MetricasParte> _partes = new();

    /// <summary>
    /// Obtiene la lista de métricas de partes registradas en el programa.
    /// </summary>
    public IReadOnlyList<MetricasParte> Partes => _partes.AsReadOnly();

    /// <summary>
    /// Obtiene o establece el tamaño total de líneas de código contables acumulado en todo el programa.
    /// </summary>
    public int TamanioTotalLOC { get; set; }

    /// <summary>
    /// Obtiene el tamaño total de líneas de código del programa.
    /// </summary>
    public int TamanioTotal => ObtenerTamanioTotal();

    /// <summary>
    /// Agrega la métrica de una parte a la colección del programa.
    /// </summary>
    /// <param name="parte">Métrica de la parte a incorporar.</param>
    /// <exception cref="ArgumentNullException">Se lanza si la métrica de parte es nula.</exception>
    public void AgregarParte(MetricasParte parte)
    {
        ArgumentNullException.ThrowIfNull(parte);
        _partes.Add(parte);
    }

    /// <summary>
    /// Agrega una secuencia de métricas de partes a la colección del programa.
    /// </summary>
    /// <param name="partes">Secuencia de métricas a registrar.</param>
    /// <exception cref="ArgumentNullException">Se lanza si la secuencia es nula.</exception>
    public void AgregarPartes(IEnumerable<MetricasParte> partes)
    {
        ArgumentNullException.ThrowIfNull(partes);

        foreach (MetricasParte parte in partes)
        {
            AgregarParte(parte);
        }
    }

    /// <summary>
    /// Calcula y devuelve el tamaño total de líneas de código contables (LOC) del programa.
    /// </summary>
    /// <returns>Número entero con el total de líneas de código.</returns>
    public int ObtenerTamanioTotal()
    {
        if (TamanioTotalLOC > 0)
        {
            return TamanioTotalLOC;
        }

        return _partes.Sum(p => p.TamanioLOC);
    }
}

