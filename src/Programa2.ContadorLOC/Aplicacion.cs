namespace Programa2.ContadorLOC;

using System;
using System.Collections.Generic;
using System.IO;
using Programa2.ContadorLOC.Dominio;
using Programa2.ContadorLOC.Infraestructura;
using Programa2.ContadorLOC.Presentacion;

/// <summary>
/// Orquesta el flujo de ejecución principal del programa entre la interfaz, infraestructura y dominio.
/// </summary>
public static class Aplicacion
{
    private const int CodigoExito = 0;
    private const int CodigoError = 1;

    /// <summary>
    /// Ejecuta el flujo principal del contador de LOC.
    /// </summary>
    /// <param name="args">Argumentos recibidos por la línea de comandos.</param>
    /// <param name="entrada">Flujo de entrada para interactuar con el usuario.</param>
    /// <param name="salida">Flujo de salida para reportar información y errores.</param>
    /// <returns>0 si la ejecución concluye exitosamente; 1 en caso de error controlado.</returns>
    public static int Ejecutar(string[] args, TextReader entrada, TextWriter salida)
    {
        ArgumentNullException.ThrowIfNull(args);
        ArgumentNullException.ThrowIfNull(entrada);
        ArgumentNullException.ThrowIfNull(salida);

        var ui = new ConsolaUI(entrada, salida);

        try
        {
            return ProcesarFlujo(args, ui);
        }
        catch (Exception ex)
        {
            return ManejarExcepcion(ex, ui);
        }
    }

    private static int ProcesarFlujo(string[] args, ConsolaUI ui)
    {
        ui.MostrarEncabezado();

        string ruta = ResolverRuta(args, ui);
        if (string.IsNullOrWhiteSpace(ruta))
        {
            ui.MostrarError("La ruta ingresada no puede estar vacía.");
            return CodigoError;
        }

        IReadOnlyList<string> archivos = LectorArchivosCodigo.ObtenerArchivosFuente(ruta);
        MetricasPrograma metricas = ProcesarArchivosFuente(archivos);

        ui.MostrarReporte(metricas);
        return CodigoExito;
    }

    private static int ManejarExcepcion(Exception ex, ConsolaUI ui)
    {
        if (ex is ArchivoNoEncontradoException or ArchivoInvalidoException)
        {
            ui.MostrarError(ex.Message);
            return CodigoError;
        }

        ui.MostrarError($"Ocurrió un error inesperado al procesar el código: {ex.Message}");
        return CodigoError;
    }

    private static string ResolverRuta(string[] args, ConsolaUI ui)
    {
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            return args[0].Trim();
        }

        return ui.PedirRuta();
    }

    private static MetricasPrograma ProcesarArchivosFuente(IReadOnlyList<string> archivos)
    {
        var metricas = new MetricasPrograma();
        int totalLocPrograma = 0;

        foreach (string archivo in archivos)
        {
            IReadOnlyList<string> lineas = LectorArchivosCodigo.LeerLineas(archivo);
            int locArchivo = ContadorCodigo.ContarLineasCodigo(lineas);
            totalLocPrograma += locArchivo;

            IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo(archivo, lineas);
            metricas.AgregarPartes(partes);
        }

        metricas.TamanioTotalLOC = totalLocPrograma;
        return metricas;
    }
}
