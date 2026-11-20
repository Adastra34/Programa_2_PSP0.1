namespace Programa2.ContadorLOC.Tests;

using System;
using System.Collections.Generic;
using System.IO;
using Programa2.ContadorLOC.Dominio;

/// <summary>
/// Prueba mínima del kit: contar archivos fuente reales del programa con un conteo independiente y comparar.
/// </summary>
public sealed class KitFuenteTests
{
    private static readonly (string Archivo, int LocEsperado, string ParteEsperada, int ItemsEsperados, int LocParteEsperada)[] MetricasArchivos =
    {
        ("Program.cs", 9, "Program", 1, 7),
        ("Aplicacion.cs", 77, "Aplicacion", 5, 70),
        ("Presentacion/ConsolaUI.cs", 67, "ConsolaUI", 6, 63),
        ("Dominio/ContadorCodigo.cs", 250, "ContadorCodigo", 17, 245),
        ("Dominio/MetricasParte.cs", 17, "MetricasParte", 1, 15),
        ("Dominio/MetricasPrograma.cs", 32, "MetricasPrograma", 3, 28),
        ("Dominio/ArchivoInvalidoException.cs", 16, "ArchivoInvalidoException", 3, 14),
        ("Infraestructura/ArchivoNoEncontradoException.cs", 16, "ArchivoNoEncontradoException", 3, 14),
        ("Infraestructura/LectorArchivosCodigo.cs", 59, "LectorArchivosCodigo", 4, 53)
    };

    [Theory]
    [InlineData("Program.cs", 9, "Program", 1, 7)]
    [InlineData("Aplicacion.cs", 77, "Aplicacion", 5, 70)]
    [InlineData("Presentacion/ConsolaUI.cs", 67, "ConsolaUI", 6, 63)]
    [InlineData("Dominio/ContadorCodigo.cs", 250, "ContadorCodigo", 17, 245)]
    [InlineData("Dominio/MetricasParte.cs", 17, "MetricasParte", 1, 15)]
    [InlineData("Dominio/MetricasPrograma.cs", 32, "MetricasPrograma", 3, 28)]
    [InlineData("Dominio/ArchivoInvalidoException.cs", 16, "ArchivoInvalidoException", 3, 14)]
    [InlineData("Infraestructura/ArchivoNoEncontradoException.cs", 16, "ArchivoNoEncontradoException", 3, 14)]
    [InlineData("Infraestructura/LectorArchivosCodigo.cs", 59, "LectorArchivosCodigo", 4, 53)]
    public void CP38_PruebaKit_ArchivosFuenteReales(
        string relativo,
        int locEsperado,
        string parteEsperada,
        int itemsEsperados,
        int locParteEsperada)
    {
        string baseDir = AppContext.BaseDirectory;
        // Navegar desde bin/Debug/net9.0 hacia src/Programa2.ContadorLOC (5 niveles hasta raíz)
        string rutaSrc = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "..", "src", "Programa2.ContadorLOC"));
        string rutaCompleta = Path.Combine(rutaSrc, relativo.Replace('/', Path.DirectorySeparatorChar));

        Assert.True(File.Exists(rutaCompleta), $"El archivo '{rutaCompleta}' debe existir.");

        string[] lineas = File.ReadAllLines(rutaCompleta);
        int locReal = ContadorCodigo.ContarLineasCodigo(lineas);
        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo(rutaCompleta, lineas);

        Assert.Equal(locEsperado, locReal);
        Assert.Single(partes);
        Assert.Equal(parteEsperada, partes[0].NombreParte);
        Assert.Equal(itemsEsperados, partes[0].NumeroElementos);
        Assert.Equal(locParteEsperada, partes[0].TamanioLOC);
    }
}
