namespace Programa2.ContadorLOC.Tests;

using System;
using System.IO;
using System.Collections.Generic;
using Programa2.ContadorLOC.Dominio;
using Programa2.ContadorLOC.Infraestructura;

/// <summary>
/// Pruebas de lectura de archivos y rutas según RF-01, CP-01 a CP-04 y CP-08/CP-09 del diseño.
/// </summary>
public sealed class ArchivosYRutasTests : IDisposable
{
    private readonly string _dirTemporal;

    public ArchivosYRutasTests()
    {
        _dirTemporal = Path.Combine(Path.GetTempPath(), "ContadorLOC_Tests_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_dirTemporal);
    }

    public void Dispose()
    {
        if (Directory.Exists(_dirTemporal))
        {
            Directory.Delete(_dirTemporal, recursive: true);
        }
    }

    // ───────────── CP-24: Archivo .cs válido ─────────────

    [Fact]
    public void CP24_ArchivoCsValido_SeLeeCorrectamente()
    {
        string ruta = Path.Combine(_dirTemporal, "Ejemplo.cs");
        File.WriteAllText(ruta, "public class A\n{\n}\n");

        IReadOnlyList<string> archivos = LectorArchivosCodigo.ObtenerArchivosFuente(ruta);

        Assert.Single(archivos);
    }

    // ───────────── CP-25: Directorio con subdirectorios ─────────────

    [Fact]
    public void CP25_DirectorioConSubdirectorios_LeeTodosLosCs()
    {
        string sub = Path.Combine(_dirTemporal, "sub");
        Directory.CreateDirectory(sub);
        File.WriteAllText(Path.Combine(_dirTemporal, "A.cs"), "public class A { }");
        File.WriteAllText(Path.Combine(sub, "B.cs"), "public class B { }");

        IReadOnlyList<string> archivos = LectorArchivosCodigo.ObtenerArchivosFuente(_dirTemporal);

        Assert.Equal(2, archivos.Count);
    }

    // ───────────── CP-26: Directorio sin .cs → error controlado ─────────────

    [Fact]
    public void CP26_DirectorioSinCs_LanzaArchivoInvalidoException()
    {
        string dirVacio = Path.Combine(_dirTemporal, "vacio");
        Directory.CreateDirectory(dirVacio);
        File.WriteAllText(Path.Combine(dirVacio, "readme.txt"), "no es cs");

        Assert.Throws<ArchivoInvalidoException>(() =>
            LectorArchivosCodigo.ObtenerArchivosFuente(dirVacio));
    }

    // ───────────── CP-27: Ruta inexistente → error controlado ─────────────

    [Fact]
    public void CP27_RutaInexistente_LanzaArchivoNoEncontradoException()
    {
        string rutaFalsa = Path.Combine(_dirTemporal, "no_existe.cs");

        Assert.Throws<ArchivoNoEncontradoException>(() =>
            LectorArchivosCodigo.ObtenerArchivosFuente(rutaFalsa));
    }

    // ───────────── CP-28: Archivo vacío ─────────────

    [Fact]
    public void CP28_ArchivoVacio_CeroLoc()
    {
        string ruta = Path.Combine(_dirTemporal, "Vacio.cs");
        File.WriteAllText(ruta, "");

        IReadOnlyList<string> lineas = LectorArchivosCodigo.LeerLineas(ruta);
        int loc = ContadorCodigo.ContarLineasCodigo(lineas);

        Assert.Equal(0, loc);
    }

    // ───────────── CP-29: Archivo que no es .cs → error ─────────────

    [Fact]
    public void CP29_ArchivoNoCs_LanzaArchivoInvalidoException()
    {
        string ruta = Path.Combine(_dirTemporal, "datos.txt");
        File.WriteAllText(ruta, "contenido");

        Assert.Throws<ArchivoInvalidoException>(() =>
            LectorArchivosCodigo.ObtenerArchivosFuente(ruta));
    }

    // ───────────── CP-30: Ruta entre comillas ─────────────

    [Fact]
    public void CP30_RutaEntreComillas_SeProcesaCorrectamente()
    {
        string ruta = Path.Combine(_dirTemporal, "Comillas.cs");
        File.WriteAllText(ruta, "public class C { }");

        string rutaConComillas = $"\"{ruta}\"";
        IReadOnlyList<string> archivos = LectorArchivosCodigo.ObtenerArchivosFuente(rutaConComillas);

        Assert.Single(archivos);
    }
}

