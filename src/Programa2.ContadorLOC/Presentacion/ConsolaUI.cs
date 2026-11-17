namespace Programa2.ContadorLOC.Presentacion;

using System;
using System.IO;
using Programa2.ContadorLOC.Dominio;

/// <summary>
/// Gestiona la interacción con el usuario y la presentación visual en consola utilizando flujos inyectados.
/// </summary>
public sealed class ConsolaUI
{
    private const int AnchoNombreParte = 30;
    private const int AnchoNumeroItems = 18;
    private const int AnchoTamanioParte = 15;
    private const int AnchoTamanioTotal = 15;
    private const int LongitudLineaSeparador = 80;

    private readonly TextReader _entrada;
    private readonly TextWriter _salida;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ConsolaUI"/>.
    /// </summary>
    /// <param name="entrada">Flujo de entrada para lectura de datos.</param>
    /// <param name="salida">Flujo de salida para impresión de resultados y mensajes.</param>
    /// <exception cref="ArgumentNullException">Se lanza si alguno de los flujos es nulo.</exception>
    public ConsolaUI(TextReader entrada, TextWriter salida)
    {
        ArgumentNullException.ThrowIfNull(entrada);
        ArgumentNullException.ThrowIfNull(salida);

        _entrada = entrada;
        _salida = salida;
    }

    /// <summary>
    /// Muestra el encabezado estándar de 3 líneas que identifica al programa PSP1.
    /// </summary>
    public void MostrarEncabezado()
    {
        _salida.WriteLine(new string('=', LongitudLineaSeparador));
        _salida.WriteLine("PROGRAMA 2 - CONTADOR DE LÍNEAS DE CÓDIGO (LOC) - PSP1");
        _salida.WriteLine("Proceso de Software Personal (SEI PSP1)");
        _salida.WriteLine(new string('=', LongitudLineaSeparador));
    }

    /// <summary>
    /// Solicita de forma interactiva la ruta del archivo o directorio a analizar.
    /// </summary>
    /// <returns>Cadena con la ruta provista por el usuario.</returns>
    public string PedirRuta()
    {
        _salida.Write("Ingrese la ruta del archivo o directorio a analizar: ");
        string? texto = _entrada.ReadLine();
        return texto?.Trim() ?? string.Empty;
    }

    /// <summary>
    /// Dibuja la tabla de resultados exacta requerida por el SEI PSP1 con las columnas solicitadas.
    /// </summary>
    /// <param name="metricas">Objeto con las métricas acumuladas del programa.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el contenedor de métricas es nulo.</exception>
    public void MostrarReporte(MetricasPrograma metricas)
    {
        ArgumentNullException.ThrowIfNull(metricas);

        _salida.WriteLine();
        _salida.WriteLine(new string('-', LongitudLineaSeparador));
        _salida.WriteLine(
            $"{"Part Name",-AnchoNombreParte}{"Number of Items",AnchoNumeroItems}{"Part Size",AnchoTamanioParte}{"Total Size",AnchoTamanioTotal}");
        _salida.WriteLine(new string('-', LongitudLineaSeparador));

        foreach (MetricasParte parte in metricas.Partes)
        {
            _salida.WriteLine(
                $"{parte.NombreParte,-AnchoNombreParte}{parte.NumeroElementos,AnchoNumeroItems}{parte.TamanioLOC,AnchoTamanioParte}{"",AnchoTamanioTotal}");
        }

        _salida.WriteLine(new string('-', LongitudLineaSeparador));
        _salida.WriteLine(
            $"{"",-AnchoNombreParte}{"",AnchoNumeroItems}{"",AnchoTamanioParte}{metricas.ObtenerTamanioTotal(),AnchoTamanioTotal}");
        _salida.WriteLine(new string('-', LongitudLineaSeparador));
        _salida.WriteLine();
    }

    /// <summary>
    /// Muestra un mensaje amigable de error garantizando el prefijo estandarizado.
    /// </summary>
    /// <param name="mensaje">Descripción del error ocurrido.</param>
    /// <exception cref="ArgumentException">Se lanza si el mensaje es nulo o vacío.</exception>
    public void MostrarError(string mensaje)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(mensaje);

        string mensajeLimpio = mensaje.Trim();
        string mensajeFinal = mensajeLimpio.StartsWith("ERROR:", StringComparison.OrdinalIgnoreCase)
            ? mensajeLimpio
            : $"ERROR: {mensajeLimpio}";

        _salida.WriteLine(mensajeFinal);
    }

    /// <summary>
    /// Pausa la ejecución solicitando la confirmación del usuario antes de finalizar el programa.
    /// </summary>
    public void EsperarSalida()
    {
        _salida.WriteLine("Presione Enter para salir...");
        _entrada.ReadLine();
    }
}

