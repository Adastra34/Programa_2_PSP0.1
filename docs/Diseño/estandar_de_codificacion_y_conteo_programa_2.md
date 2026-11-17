# Estándar de codificación y conteo C# (Programa 2)

Basado en las convenciones de Microsoft para C# y las especificaciones del SEI PSP1 para el conteo de LOC y partes en software modular.

| # | Regla de Código / Conteo | Ejemplo correcto | Incorrecto |
|---|---|---|---|
| E-01 | Clases, métodos y propiedades en **PascalCase**, en español o inglés técnico y claros. | `ContadorCodigo.ProcesarArchivo()` | `cc.pa()` |
| E-02 | Variables locales y parámetros en **camelCase**. | `lineasArchivo` | `lineas_archivo`, `LA` |
| E-03 | Campos privados con guion bajo. | `_lectorArchivos` | `lectorArchivos`, `m_lector` |
| E-04 | Constantes nombradas sin números mágicos. | `ExtensionArchivoCs = ".cs"` | `if (ext == ".cs")` |
| E-05 | Un tipo por archivo; el archivo se llama exactamente como el tipo. | `ContadorCodigo.cs` | `ClasesVarias.cs` |
| E-06 | Espacios de nombres por carpeta (file-scoped). | `namespace Programa2.ContadorLOC.Dominio;` | `namespace Global` |
| E-07 | Llaves siempre en bloques condicionales y ciclos; estilo Allman. | `if (condicion) { ... }` en línea propia | `if (condicion) return;` |
| E-08 | Métodos de **30 líneas o menos** y una sola responsabilidad. | `ContarLineas()` separado de `LeerArchivo()` | Un método de 100 líneas |
| E-09 | Comentarios XML `///` en miembros públicos; comentarios `//` solo para explicar el **porqué**. | `/// <summary>Analiza las líneas...</summary>` | `// suma uno` |
| E-10 | Validar argumentos públicos al inicio del método. | `ArgumentNullException.ThrowIfNull(ruta);` | — |
| E-11 | Errores manejados con **excepciones propias**; nunca capturar `Exception` de forma vacía. | `ArchivoNoEncontradoException` | `catch { }` |
| E-12 | Conteo de LOC físicas: se excluyen líneas en blanco y comentarios puros según el estándar de conteo PSP. | Omitir `//` y `}` sueltas si aplica el estándar | Contar líneas vacías como código |
| E-13 | Tipos anulables activados (`<Nullable>enable</Nullable>`). | `string?` | Manejar `null` con referencias sin control |
| E-14 | Sin código comentado ni sentencias `Console.WriteLine` de depuración en commits. | Código limpio | `// Console.WriteLine(x);` |
| E-15 | La capa de dominio no utiliza operaciones de E/S ni la consola (`Console`). | `ContadorCodigo` recibe un `IEnumerable<string>` | Leer archivos dentro del dominio |

## Reglas de Commits PSP1

* Un commit al **cerrar cada fase PSP**, con prefijo estricto: `planeacion:`, `diseno:`, `codigo:`, `compilacion:`, `pruebas:`, `postmortem:`.
* Mensaje imperativo indicando qué cambió y el número del defecto asociado si aplica (ej. `compilacion: corrige D1-D3`).
* Nunca subir directorios `bin/` o `obj/` (respetar `.gitignore`).