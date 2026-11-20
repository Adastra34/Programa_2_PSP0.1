namespace Programa2.ContadorLOC.Tests;

using System;
using System.Collections.Generic;
using Programa2.ContadorLOC.Dominio;

/// <summary>
/// Pruebas de detección de elementos (métodos y constructores) según RF-04 y CP-06 del diseño.
/// </summary>
public sealed class ElementosTests
{
    // ───────────── CP-19: Métodos y constructores cuentan ─────────────

    [Fact]
    public void CP19_MetodoYConstructor_CuentanComoElementos()
    {
        string[] lineas =
        {
            "public class MiClase",
            "{",
            "    public MiClase()",
            "    {",
            "    }",
            "",
            "    public void Hacer()",
            "    {",
            "    }",
            "}"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Single(partes);
        Assert.Equal(2, partes[0].NumeroElementos);
    }

    // ───────────── CP-20: Propiedades no cuentan ─────────────

    [Fact]
    public void CP20_Propiedades_NoCuentanComoElementos()
    {
        string[] lineas =
        {
            "public class MiClase",
            "{",
            "    public int Valor { get; set; }",
            "    public string Nombre { get; init; }",
            "}"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Single(partes);
        Assert.Equal(0, partes[0].NumeroElementos);
    }

    // ───────────── CP-21: if/for/while/foreach dentro de método no cuentan ─────────────

    [Fact]
    public void CP21_EstructurasControl_NoCuentanComoElementos()
    {
        string[] lineas =
        {
            "public class MiClase",
            "{",
            "    public void Proceso()",
            "    {",
            "        if (true)",
            "        {",
            "        }",
            "        for (int i = 0; i < 10; i++)",
            "        {",
            "        }",
            "        while (false)",
            "        {",
            "        }",
            "        foreach (var x in new int[0])",
            "        {",
            "        }",
            "    }",
            "}"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Single(partes);
        Assert.Equal(1, partes[0].NumeroElementos);
    }

    // ───────────── CP-22: Método con cuerpo de expresión ─────────────

    [Fact]
    public void CP22_MetodoCuerpoExpresion_CuentaComoElemento()
    {
        string[] lineas =
        {
            "public class MiClase",
            "{",
            "    public int Sumar(int a, int b) => a + b;",
            "}"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Single(partes);
        Assert.Equal(1, partes[0].NumeroElementos);
    }

    // ───────────── CP-23: Lambdas y llamadas no cuentan ─────────────

    [Fact]
    public void CP23_LambdasYLlamadas_NoCuentanComoElementos()
    {
        string[] lineas =
        {
            "public class MiClase",
            "{",
            "    public void Hacer()",
            "    {",
            "        var lista = new List<int>();",
            "        lista.ForEach(x => Console.WriteLine(x));",
            "        Console.WriteLine(\"hola\");",
            "    }",
            "}"
        };

        IReadOnlyList<MetricasParte> partes = ContadorCodigo.ProcesarArchivo("test.cs", lineas);

        Assert.Single(partes);
        Assert.Equal(1, partes[0].NumeroElementos);
    }
}

