namespace Programa2.ContadorLOC.Tests;

using System;
using System.Collections.Generic;
using Programa2.ContadorLOC.Dominio;

/// <summary>
/// Pruebas de detección de partes (class, struct, interface, record) según RF-03 y CP-05 del diseño.
/// </summary>
public sealed class PartesTests
{
    // ───────────── CP-10: Una clase ─────────────

    [Fact]
    public void CP10_UnaClase_DetectaUnaParte()
    {
        string[] lineas =
        {
            "namespace MiApp;",
            "",
            "public class MiClase",
            "{",
            "    public void Metodo()",
            "    {",
            "        int x = 1;",
            "    }",
            "}"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Single(partes);
        Assert.Equal("MiClase", partes[0].NombreParte);
        Assert.Equal(7, partes[0].TamanioLOC);
        Assert.Equal(1, partes[0].NumeroElementos);
    }

    // ───────────── CP-11: Dos clases ─────────────

    [Fact]
    public void CP11_DosClases_ReportaCadaParte()
    {
        string[] lineas =
        {
            "public class Alfa",
            "{",
            "    public void M()",
            "    {",
            "    }",
            "}",
            "",
            "public class Beta",
            "{",
            "    public void N()",
            "    {",
            "    }",
            "}"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Equal(2, partes.Count);

        MetricasParte alfa = partes[0];
        Assert.Equal("Alfa", alfa.NombreParte);
        Assert.Equal(6, alfa.TamanioLOC);
        Assert.Equal(1, alfa.NumeroElementos);

        MetricasParte beta = partes[1];
        Assert.Equal("Beta", beta.NombreParte);
        Assert.Equal(6, beta.TamanioLOC);
        Assert.Equal(1, beta.NumeroElementos);
    }

    // ───────────── CP-12: Interface ─────────────

    [Fact]
    public void CP12_Interface_DetectadaComoParte()
    {
        string[] lineas =
        {
            "public interface IMiContrato",
            "{",
            "    void Operar();",
            "}"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Single(partes);
        Assert.Equal("IMiContrato", partes[0].NombreParte);
        Assert.Equal(4, partes[0].TamanioLOC);
    }

    // ───────────── CP-13: Struct ─────────────

    [Fact]
    public void CP13_Struct_DetectadaComoParte()
    {
        string[] lineas =
        {
            "public struct Punto",
            "{",
            "    public int X;",
            "    public int Y;",
            "}"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Single(partes);
        Assert.Equal("Punto", partes[0].NombreParte);
        Assert.Equal(5, partes[0].TamanioLOC);
    }

    // ───────────── CP-14: Record con cuerpo ─────────────

    [Fact]
    public void CP14_RecordConCuerpo_DetectadoComoParte()
    {
        string[] lineas =
        {
            "public record Persona",
            "{",
            "    public string Nombre { get; init; }",
            "}"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Single(partes);
        Assert.Equal("Persona", partes[0].NombreParte);
        Assert.Equal(4, partes[0].TamanioLOC);
    }

    // ───────────── CP-15: Record posicional terminado en ; ─────────────

    [Fact]
    public void CP15_RecordPosicional_DetectadoComoParte()
    {
        string[] lineas =
        {
            "public record Punto(int X, int Y);"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        // Un record posicional sin llaves no abre un bloque,
        // así que no se finaliza la parte por cierre de llave.
        // El programa lo maneja con FinalizarPartesRestantes.
        Assert.Single(partes);
        Assert.Equal("Punto", partes[0].NombreParte);
    }

    // ───────────── CP-16: Clase con llave Allman ─────────────

    [Fact]
    public void CP16_ClaseConLlaveAllman_DetectadaCorrectamente()
    {
        string[] lineas =
        {
            "public class MiClase",
            "{",
            "    public void Hacer()",
            "    {",
            "        int i = 0;",
            "    }",
            "}"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Single(partes);
        Assert.Equal("MiClase", partes[0].NombreParte);
        Assert.Equal(7, partes[0].TamanioLOC);
    }

    // ───────────── CP-17: Namespace file-scoped ─────────────

    [Fact]
    public void CP17_NamespaceFileScopedNoEsParte()
    {
        string[] lineas =
        {
            "namespace MiApp.Dominio;",
            "",
            "public class Servicio",
            "{",
            "    public void Ejecutar()",
            "    {",
            "    }",
            "}"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Single(partes);
        Assert.Equal("Servicio", partes[0].NombreParte);
    }

    // ───────────── CP-18: RF-05 suma de partes <= total ─────────────

    [Fact]
    public void CP18_SumaPartesNoSuperaTotal()
    {
        string[] lineas =
        {
            "using System;",
            "",
            "public class Alfa",
            "{",
            "    public void M()",
            "    {",
            "    }",
            "}",
            "",
            "public class Beta",
            "{",
            "    public void N()",
            "    {",
            "    }",
            "}"
        };

        int total = ContadorCodigo.ContarLineasCodigo(lineas);
        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);
        int sumaPartes = 0;
        foreach (MetricasParte p in partes)
        {
            sumaPartes += p.TamanioLOC;
        }

        Assert.True(sumaPartes <= total, $"Suma de partes ({sumaPartes}) debe ser <= total ({total})");
    }

    // ───────────── CP-18: Clase anidada (cada parte con su propio LOC; suma <= total, RF-05) ─────────────

    [Fact]
    public void CP18_ClaseAnidada_CadaParteConSuPropioLoc()
    {
        string[] lineas =
        {
            "public class Externa",
            "{",
            "    public void MetodoExterna()",
            "    {",
            "    }",
            "",
            "    public class Interna",
            "    {",
            "        public void MetodoInterna()",
            "        {",
            "        }",
            "    }",
            "}"
        };

        int total = ContadorCodigo.ContarLineasCodigo(lineas);
        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Equal(2, partes.Count);

        MetricasParte interna = partes[0];
        Assert.Equal("Interna", interna.NombreParte);
        Assert.Equal(1, interna.NumeroElementos);
        Assert.Equal(6, interna.TamanioLOC);

        MetricasParte externa = partes[1];
        Assert.Equal("Externa", externa.NombreParte);
        Assert.Equal(1, externa.NumeroElementos);
        Assert.Equal(6, externa.TamanioLOC);

        int sumaPartes = interna.TamanioLOC + externa.TamanioLOC;
        Assert.True(sumaPartes <= total, $"Suma de partes ({sumaPartes}) debe ser <= total ({total})");
    }
}

