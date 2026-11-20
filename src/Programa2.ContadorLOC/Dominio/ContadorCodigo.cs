namespace Programa2.ContadorLOC.Dominio;

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Núcleo analítico puro para el conteo de líneas de código (LOC) y detección de partes y elementos en C#.
/// </summary>
public static partial class ContadorCodigo
{
    private static readonly HashSet<string> PalabrasClaveExcluidas = new(StringComparer.Ordinal)
    {
        "if", "else", "while", "for", "foreach", "switch", "catch", "using",
        "lock", "fixed", "return", "throw", "new", "get", "set", "init",
        "class", "record", "struct", "interface", "enum", "delegate", "sizeof", "typeof"
    };

    private const int LongitudMarcaComentario = 2;
    private const int LongitudSecuenciaEscape = 2;

    /// <summary>
    /// Cuenta las líneas físicas de código contables en una secuencia de líneas, descartando comentarios y líneas vacías.
    /// </summary>
    /// <param name="lineas">Secuencia de líneas del archivo a analizar.</param>
    /// <returns>Cantidad total de líneas físicas de código contables.</returns>
    /// <exception cref="ArgumentNullException">Se lanza si la colección de líneas es nula.</exception>
    public static int ContarLineasCodigo(IEnumerable<string> lineas)
    {
        ArgumentNullException.ThrowIfNull(lineas);

        int totalLoc = 0;
        bool enBloque = false;

        foreach (string linea in lineas)
        {
            (bool esContable, bool nuevoEnBloque, _) = AnalizarComentariosYCodigo(linea, enBloque);
            enBloque = nuevoEnBloque;

            if (esContable)
            {
                totalLoc++;
            }
        }

        return totalLoc;
    }

    /// <summary>
    /// Analiza las líneas de un archivo y extrae las métricas de las partes y elementos detectados.
    /// </summary>
    /// <param name="nombreArchivo">Ruta o nombre del archivo analizado.</param>
    /// <param name="lineas">Secuencia de líneas del archivo.</param>
    /// <returns>Lista de métricas por cada parte detectada.</returns>
    /// <exception cref="ArgumentException">Se lanza si el nombre del archivo es nulo o vacío.</exception>
    /// <exception cref="ArgumentNullException">Se lanza si la colección de líneas es nula.</exception>
    public static IReadOnlyList<MetricasParte> ProcesarArchivo(string nombreArchivo, IEnumerable<string> lineas)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nombreArchivo);
        ArgumentNullException.ThrowIfNull(lineas);

        var partesFinalizadas = new List<MetricasParte>();
        var pilaPartes = new Stack<(string NombreParte, int ProfundidadBase, int TamanioLOC, int NumeroElementos)>();
        int profundidadLlaves = 0;
        bool enBloque = false;

        foreach (string linea in lineas)
        {
            (bool esContable, bool nuevoEnBloque, string codigoLimpio) = AnalizarComentariosYCodigo(linea, enBloque);
            enBloque = nuevoEnBloque;

            ProcesarLinea(codigoLimpio, esContable, ref profundidadLlaves, pilaPartes, partesFinalizadas);
        }

        FinalizarPartesRestantes(pilaPartes, partesFinalizadas);
        return partesFinalizadas;
    }

    private static void ProcesarLinea(
        string codigoLimpio,
        bool esContable,
        ref int profundidadLlaves,
        Stack<(string NombreParte, int ProfundidadBase, int TamanioLOC, int NumeroElementos)> pilaPartes,
        List<MetricasParte> partesFinalizadas)
    {
        string? parteDetectada = DetectarDeclaracionParte(codigoLimpio);
        if (parteDetectada is not null)
        {
            pilaPartes.Push((parteDetectada, profundidadLlaves, 0, 0));
        }

        if (esContable && pilaPartes.Count > 0)
        {
            var top = pilaPartes.Pop();
            pilaPartes.Push((top.NombreParte, top.ProfundidadBase, top.TamanioLOC + 1, top.NumeroElementos));
        }

        if (pilaPartes.Count > 0)
        {
            var top = pilaPartes.Peek();
            if (profundidadLlaves == top.ProfundidadBase + 1 && EsDeclaracionElemento(codigoLimpio, top.NombreParte))
            {
                var elemento = pilaPartes.Pop();
                pilaPartes.Push((elemento.NombreParte, elemento.ProfundidadBase, elemento.TamanioLOC, elemento.NumeroElementos + 1));
            }
        }

        ActualizarProfundidad(codigoLimpio, ref profundidadLlaves, pilaPartes, partesFinalizadas);
    }

    private static void ActualizarProfundidad(
        string codigoLimpio,
        ref int profundidadLlaves,
        Stack<(string NombreParte, int ProfundidadBase, int TamanioLOC, int NumeroElementos)> pilaPartes,
        List<MetricasParte> partesFinalizadas)
    {
        foreach (char c in codigoLimpio)
        {
            if (c == '{')
            {
                profundidadLlaves++;
            }
            else if (c == '}')
            {
                profundidadLlaves--;
                if (pilaPartes.Count > 0 && profundidadLlaves <= pilaPartes.Peek().ProfundidadBase)
                {
                    var parte = pilaPartes.Pop();
                    partesFinalizadas.Add(new MetricasParte(parte.NombreParte, parte.NumeroElementos, parte.TamanioLOC));
                }
            }
        }
    }

    private static void FinalizarPartesRestantes(
        Stack<(string NombreParte, int ProfundidadBase, int TamanioLOC, int NumeroElementos)> pilaPartes,
        List<MetricasParte> partesFinalizadas)
    {
        while (pilaPartes.Count > 0)
        {
            var parte = pilaPartes.Pop();
            partesFinalizadas.Add(new MetricasParte(parte.NombreParte, parte.NumeroElementos, parte.TamanioLOC));
        }
    }

    private static (bool esContable, bool continuaEnBloque, string codigoLimpio) AnalizarComentariosYCodigo(
        string linea,
        bool enBloque)
    {
        if (string.IsNullOrWhiteSpace(linea) && !enBloque)
        {
            return (false, false, string.Empty);
        }

        var sb = new StringBuilder(linea.Length);
        bool hayCodigo = false;
        int i = 0;
        int n = linea.Length;

        while (i < n)
        {
            if (enBloque)
            {
                i = ConsumirComentarioBloque(linea, i, ref enBloque);
            }
            else
            {
                i = ConsumirCaracteresCodigo(linea, i, n, sb, ref hayCodigo, ref enBloque);
            }
        }

        return (hayCodigo, enBloque, sb.ToString());
    }

    private static int ConsumirComentarioBloque(string linea, int indice, ref bool enBloque)
    {
        if (indice + 1 < linea.Length && linea[indice] == '*' && linea[indice + 1] == '/')
        {
            enBloque = false;
            return indice + LongitudMarcaComentario;
        }

        return indice + 1;
    }

    private static int ConsumirCaracteresCodigo(
        string linea,
        int indice,
        int longitud,
        StringBuilder sb,
        ref bool hayCodigo,
        ref bool enBloque)
    {
        if (char.IsWhiteSpace(linea[indice]))
        {
            sb.Append(linea[indice]);
            return indice + 1;
        }

        if (indice + 1 < longitud && linea[indice] == '/' && linea[indice + 1] == '/')
        {
            return longitud;
        }

        if (indice + 1 < longitud && linea[indice] == '/' && linea[indice + 1] == '*')
        {
            enBloque = true;
            return indice + LongitudMarcaComentario;
        }

        if (linea[indice] == '"' || linea[indice] == '\'')
        {
            hayCodigo = true;
            return ConsumirLiteralCadena(linea, indice, longitud, sb);
        }

        hayCodigo = true;
        sb.Append(linea[indice]);
        return indice + 1;
    }

    private static int ConsumirLiteralCadena(string linea, int indice, int longitud, StringBuilder sb)
    {
        char delimitador = linea[indice];
        sb.Append(delimitador);
        int pos = indice + 1;

        while (pos < longitud)
        {
            if (linea[pos] == '\\' && pos + 1 < longitud)
            {
                pos += LongitudSecuenciaEscape;
            }
            else if (linea[pos] == delimitador)
            {
                sb.Append(delimitador);
                return pos + 1;
            }
            else
            {
                pos++;
            }
        }

        return pos;
    }

    private static string? DetectarDeclaracionParte(string codigoLimpio)
    {
        Match match = RegexDeclaracionParte().Match(codigoLimpio);
        return match.Success ? match.Groups[1].Value : null;
    }

    private static bool EsDeclaracionElemento(string codigoLimpio, string nombreParte)
    {
        string texto = codigoLimpio.Trim();
        if (string.IsNullOrEmpty(texto) || (texto.StartsWith('[') && texto.EndsWith(']')))
        {
            return false;
        }

        string sinAtributos = RegexAtributos().Replace(texto, string.Empty).Trim();
        int indiceParentesis = sinAtributos.IndexOf('(');
        if (indiceParentesis < 0)
        {
            return false;
        }

        int indiceIgual = sinAtributos.IndexOf('=');
        if (indiceIgual >= 0 && indiceIgual < indiceParentesis)
        {
            return false;
        }

        return EsConstructor(sinAtributos, nombreParte) || EsMetodoValido(sinAtributos);
    }

    private static bool EsConstructor(string texto, string nombreParte)
    {
        Match match = RegexConstructor().Match(texto);
        if (!match.Success)
        {
            return false;
        }

        return string.Equals(match.Groups[1].Value, nombreParte, StringComparison.Ordinal);
    }

    private static bool EsMetodoValido(string texto)
    {
        Match match = RegexMetodo().Match(texto);
        if (!match.Success)
        {
            return false;
        }

        string identificador = match.Groups[2].Value;
        return !PalabrasClaveExcluidas.Contains(identificador);
    }

    [GeneratedRegex(@"\b(?:class|struct|interface|record(?:\s+(?:class|struct))?)\s+([A-Za-z_][A-Za-z0-9_]*)", RegexOptions.Compiled)]
    private static partial Regex RegexDeclaracionParte();

    [GeneratedRegex(@"\[[^\]]*\]", RegexOptions.Compiled)]
    private static partial Regex RegexAtributos();

    [GeneratedRegex(@"^\s*(?:(?:public|private|protected|internal|static)\s+)*([A-Za-z_][A-Za-z0-9_]*)\s*\(", RegexOptions.Compiled)]
    private static partial Regex RegexConstructor();

    [GeneratedRegex(@"^\s*(?:(?:public|private|protected|internal|static|virtual|override|abstract|async|sealed|new|partial)\s+)*(?:\([^\)]+\)|[\w<>\[\],\?]+)\s+([A-Za-z_][A-Za-z0-9_]*)\s*(?:<[^>]+>)?\s*\(", RegexOptions.Compiled)]
    private static partial Regex RegexMetodo();
}
