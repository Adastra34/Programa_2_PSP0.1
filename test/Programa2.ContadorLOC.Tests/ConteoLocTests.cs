namespace Programa2.ContadorLOC.Tests;

using System;
using System.Collections.Generic;
using System.IO;
using Programa2.ContadorLOC.Dominio;

/// <summary>
/// Pruebas del conteo de LOC según el estándar de conteo E-12 (RF-02).
/// </summary>
public sealed class ConteoLocTests
{
    // ───────────── CP-01: Líneas en blanco excluidas ─────────────

    [Fact]
    public void CP01_LineasEnBlanco_NoSeCuentan()
    {
        string[] lineas =
        {
            "using System;",
            "",
            "public class A",
            "{",
            "",
            "    int x = 1;",
            "",
            "}"
        };

        int loc = ContadorCodigo.ContarLineasCodigo(lineas);

        Assert.Equal(5, loc);
    }

    // ───────────── CP-02: Comentarios // excluidos ─────────────

    [Fact]
    public void CP02_ComentarioLineaSimple_Excluido()
    {
        string[] lineas =
        {
            "// Este es un comentario",
            "int x = 1;",
            "// Otro comentario",
            "int y = 2;"
        };

        int loc = ContadorCodigo.ContarLineasCodigo(lineas);

        Assert.Equal(2, loc);
    }

    // ───────────── CP-03: Comentarios /// excluidos ─────────────

    [Fact]
    public void CP03_ComentarioXmlDoc_Excluido()
    {
        string[] lineas =
        {
            "/// <summary>",
            "/// Descripción del método.",
            "/// </summary>",
            "public void Metodo()",
            "{",
            "}"
        };

        int loc = ContadorCodigo.ContarLineasCodigo(lineas);

        Assert.Equal(3, loc);
    }

    // ───────────── CP-04: Comentario bloque /* */ una línea ─────────────

    [Fact]
    public void CP04_ComentarioBloqueUnaLinea_Excluido()
    {
        string[] lineas =
        {
            "/* comentario de bloque */",
            "int x = 1;"
        };

        int loc = ContadorCodigo.ContarLineasCodigo(lineas);

        Assert.Equal(1, loc);
    }

    // ───────────── CP-05: Comentario bloque /* */ varias líneas ─────────────

    [Fact]
    public void CP05_ComentarioBloqueMultilinea_Excluido()
    {
        string[] lineas =
        {
            "/* inicio",
            "   medio",
            "   fin */",
            "int x = 1;"
        };

        int loc = ContadorCodigo.ContarLineasCodigo(lineas);

        Assert.Equal(1, loc);
    }

    // ───────────── CP-06: Código con comentario al final ─────────────

    [Fact]
    public void CP06_CodigoConComentarioAlFinal_CuentaUno()
    {
        string[] lineas =
        {
            "int x = 1; // asignación"
        };

        int loc = ContadorCodigo.ContarLineasCodigo(lineas);

        Assert.Equal(1, loc);
    }

    // ───────────── CP-07: Llave suelta cuenta 1 ─────────────

    [Fact]
    public void CP07_LlaveSuelta_CuentaUno()
    {
        string[] lineas =
        {
            "public class A",
            "{",
            "}"
        };

        int loc = ContadorCodigo.ContarLineasCodigo(lineas);

        Assert.Equal(3, loc);
    }

    // ───────────── CP-08: Cadena con "//" cuenta como código ─────────────

    [Fact]
    public void CP08_CadenaConDobleSlash_CuentaComoCodigo()
    {
        string[] lineas =
        {
            "string url = \"http://ejemplo.com\";"
        };

        int loc = ContadorCodigo.ContarLineasCodigo(lineas);

        Assert.Equal(1, loc);
    }

    // ───────────── CP-09: Código después de */ en la misma línea ─────────────

    [Fact]
    public void CP09_CodigoDespuesDeCierreBloque_Cuenta()
    {
        string[] lineas =
        {
            "/* comentario */ int x = 1;"
        };

        int loc = ContadorCodigo.ContarLineasCodigo(lineas);

        Assert.Equal(1, loc);
    }
}

