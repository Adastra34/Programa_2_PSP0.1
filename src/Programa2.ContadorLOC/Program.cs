namespace Programa2.ContadorLOC;

using System;

/// <summary>
/// Punto de entrada del programa de consola para el Contador de Líneas de Código (LOC).
/// </summary>
public static class Program
{
    /// <summary>
    /// Método principal que inicia la ejecución delegando en la capa de aplicación.
    /// </summary>
    /// <param name="args">Argumentos recibidos desde la línea de comandos.</param>
    /// <returns>Código de salida del proceso (0 para éxito, 1 para error).</returns>
    public static int Main(string[] args)
    {
        return Aplicacion.Ejecutar(args, Console.In, Console.Out);
    }
}

