namespace Programa2.ContadorLOC.Tests;

using System;
using System.IO;
using Programa2.ContadorLOC.Dominio;
using Programa2.ContadorLOC.Infraestructura;

/// <summary>
/// Pruebas de la salida tabular, códigos de salida y flujo de la aplicación (RF-06, RNF-05, RNF-06).
/// </summary>
public sealed class SalidaYFlujoTests : IDisposable
{
    private readonly string _dirTemporal;

    public SalidaYFlujoTests()
    {
        _dirTemporal = Path.Combine(Path.GetTempPath(), "ContadorLOC_Flujo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_dirTemporal);
    }

    public void Dispose()
    {
        if (Directory.Exists(_dirTemporal))
        {
            Directory.Delete(_dirTemporal, recursive: true);
        }
    }

    // ───────────── CP-31: Tabla muestra columnas correctas (RF-06) ─────────────

    [Fact]
    public void CP31_SalidaTabular_ContieneColumnasEsperadas()
    {
        string ruta = Path.Combine(_dirTemporal, "Tabla.cs");
        File.WriteAllText(ruta, "public class Demo\n{\n    public void M()\n    {\n    }\n}\n");

        using var salida = new StringWriter();
        using var entrada = new StringReader("\n");

        int codigo = Aplicacion.Ejecutar(new[] { ruta }, entrada, salida);
        string texto = salida.ToString();

        Assert.Equal(0, codigo);
        Assert.Contains("Part Name", texto);
        Assert.Contains("Number of Items", texto);
        Assert.Contains("Part Size", texto);
        Assert.Contains("Total Size", texto);
        Assert.Contains("Demo", texto);
    }

    // ───────────── CP-32: Código de salida 0 en éxito ─────────────

    [Fact]
    public void CP32_Exito_CodigoSalidaCero()
    {
        string ruta = Path.Combine(_dirTemporal, "Ok.cs");
        File.WriteAllText(ruta, "public class Ok { }\n");

        using var salida = new StringWriter();
        using var entrada = new StringReader("\n");

        int codigo = Aplicacion.Ejecutar(new[] { ruta }, entrada, salida);

        Assert.Equal(0, codigo);
    }

    // ───────────── CP-33: Código de salida 1 en error ─────────────

    [Fact]
    public void CP33_Error_CodigoSalidaUno()
    {
        string rutaFalsa = Path.Combine(_dirTemporal, "inexistente.cs");

        using var salida = new StringWriter();
        using var entrada = new StringReader("\n");

        int codigo = Aplicacion.Ejecutar(new[] { rutaFalsa }, entrada, salida);

        Assert.Equal(1, codigo);
    }

    // ───────────── CP-34: Error muestra "ERROR:" sin excepción visible ─────────────

    [Fact]
    public void CP34_RutaInexistente_MuestraErrorSinExcepcion()
    {
        string rutaFalsa = Path.Combine(_dirTemporal, "fantasma.cs");

        using var salida = new StringWriter();
        using var entrada = new StringReader("\n");

        int codigo = Aplicacion.Ejecutar(new[] { rutaFalsa }, entrada, salida);
        string texto = salida.ToString();

        Assert.Equal(1, codigo);
        Assert.Contains("ERROR:", texto);
        Assert.DoesNotContain("Exception", texto);
    }

    // ───────────── CP-35: Resultado determinista (RNF-06) ─────────────

    [Fact]
    public void CP35_Determinismo_DosEjecucionesIguales()
    {
        string ruta = Path.Combine(_dirTemporal, "Det.cs");
        File.WriteAllText(ruta, "public class Det\n{\n    public void A()\n    {\n        int x = 1;\n    }\n}\n");

        string Ejecutar()
        {
            using var salida = new StringWriter();
            using var entrada = new StringReader("\n");
            Aplicacion.Ejecutar(new[] { ruta }, entrada, salida);
            return salida.ToString();
        }

        string ejecucion1 = Ejecutar();
        string ejecucion2 = Ejecutar();

        Assert.Equal(ejecucion1, ejecucion2);
    }

    // ───────────── CP-36: Ruta vacía → error controlado ─────────────

    [Fact]
    public void CP36_RutaVacia_CodigoError()
    {
        using var salida = new StringWriter();
        using var entrada = new StringReader("\n");

        int codigo = Aplicacion.Ejecutar(new[] { "   " }, entrada, salida);

        Assert.Equal(1, codigo);
    }

    // ───────────── CP-37: Entrada interactiva (PedirRuta) ─────────────

    [Fact]
    public void CP37_SinArgumentos_PideRutaInteractivamente()
    {
        string ruta = Path.Combine(_dirTemporal, "Inter.cs");
        File.WriteAllText(ruta, "public class Inter { }\n");

        using var salida = new StringWriter();
        using var entrada = new StringReader(ruta + "\n\n");

        int codigo = Aplicacion.Ejecutar(Array.Empty<string>(), entrada, salida);
        string texto = salida.ToString();

        Assert.Equal(0, codigo);
        Assert.Contains("Ingrese la ruta", texto);
        Assert.Contains("Inter", texto);
    }
}

