# Casos de Prueba – Programa 2 (PSP1)

Definición de casos de prueba derivados de los requisitos funcionales ([requisitos.md](../Planeación/requisitos.md)), el estándar de codificación y conteo ([estandar_de_codificacion_y_conteo_programa_2.md](../Diseño/estandar_de_codificacion_y_conteo_programa_2.md)) y la trazabilidad del diseño ([diseño_programa_2.md](../Diseño/diseño_programa_2.md)).

Todos los resultados planeados han sido calculados estrictamente a mano aplicando las reglas del estándar E-12 (líneas físicas con código, incluyendo llaves `{` o `}`, excluyendo líneas vacías y comentarios puros).

---

## 1. Conteo de Líneas de Código (LOC) – RF-02 / E-12

| ID | Requisito | Descripción | Entrada (Fixture) | Resultado esperado (PLANEADO a mano) | Resultado real | Estado |
|---|---|---|---|---|---|---|
| CP01 | RF-02 | Líneas en blanco no se cuentan | `using System;\n\npublic class A\n{\n\n    int x = 1;\n\n}` (8 líneas brutas, 3 vacías) | 4 LOC contables (`using`, `public class A`, `{`, `int x = 1;`, `}`) -> 5 líneas con código | 5 LOC (excluye las 3 líneas en blanco) | Pasa |
| CP02 | RF-02 | Comentarios de línea simple `//` no se cuentan | `// Comentario 1\nint x = 1;\n// Comentario 2\nint y = 2;` | 2 LOC contables (`int x = 1;`, `int y = 2;`) | 2 LOC | Pasa |
| CP03 | RF-02 | Comentarios de documentación XML `///` no se cuentan | `/// <summary>\n/// Doc\n/// </summary>\npublic void M()\n{\n}` | 3 LOC contables (`public void M()`, `{`, `}`) | 3 LOC | Pasa |
| CP04 | RF-02 | Comentario de bloque `/* */` en una sola línea no se cuenta | `/* comentario bloque */\nint x = 1;` | 1 LOC contable (`int x = 1;`) | 1 LOC | Pasa |
| CP05 | RF-02 | Comentario de bloque `/* */` multilínea no se cuenta | `/* inicio\n   medio\n   fin */\nint x = 1;` | 1 LOC contable (`int x = 1;`) | 1 LOC | Pasa |
| CP06 | RF-02 | Línea con código y comentario al final cuenta 1 | `int x = 1; // asignación` | 1 LOC contable | 1 LOC | Pasa |
| CP07 | RF-02 | Llaves `{` o `}` en línea propia cuentan 1 cada una | `public class A\n{\n}` | 3 LOC contables | 3 LOC | Pasa |
| CP08 | RF-02 | Cadena literal que contiene `"//"` cuenta como código | `string url = "http://ejemplo.com";` | 1 LOC contable | 1 LOC | Pasa |
| CP09 | RF-02 | Código situado después de `*/` en la misma línea cuenta 1 | `/* comentario */ int x = 1;` | 1 LOC contable | 1 LOC | Pasa |

---

## 2. Detección y Conteo de Partes – RF-03 / RF-05 / E-05

| ID | Requisito | Descripción | Entrada (Fixture) | Resultado esperado (PLANEADO a mano) | Resultado real | Estado |
|---|---|---|---|---|---|---|
| CP10 | RF-03, RF-05 | Archivo con una sola clase | `namespace MiApp;\n\npublic class MiClase\n{\n    public void Metodo()\n    {\n        int x = 1;\n    }\n}` | 1 parte (`MiClase`), 1 ítem, 7 LOC de parte; Total = 8 LOC | 1 parte (`MiClase`), 1 ítem, 7 LOC | Pasa |
| CP11 | RF-03, RF-05 | Archivo con dos clases secuenciales | `public class Alfa { public void M() { } }\n\npublic class Beta { public void N() { } }` | 2 partes: `Alfa` (1 ítem, 6 LOC), `Beta` (1 ítem, 6 LOC); Total = 12 LOC | 2 partes: `Alfa` (1 ítem, 6 LOC), `Beta` (1 ítem, 6 LOC) | Pasa |
| CP12 | RF-03 | Detección de `interface` como parte | `public interface IMiContrato\n{\n    void Operar();\n}` | 1 parte (`IMiContrato`), 1 ítem, 4 LOC de parte | 1 parte (`IMiContrato`), 1 ítem, 4 LOC | Pasa |
| CP13 | RF-03 | Detección de `struct` como parte | `public struct Punto\n{\n    public int X;\n    public int Y;\n}` | 1 parte (`Punto`), 0 ítems, 5 LOC de parte | 1 parte (`Punto`), 0 ítems, 5 LOC | Pasa |
| CP14 | RF-03 | Detección de `record` con bloque de llaves | `public record Persona\n{\n    public string Nombre { get; init; }\n}` | 1 parte (`Persona`), 0 ítems, 4 LOC de parte | 1 parte (`Persona`), 0 ítems, 4 LOC | Pasa |
| CP15 | RF-03 | Detección de `record` posicional terminado en `;` | `public record Punto(int X, int Y);` | 1 parte (`Punto`), 0 ítems, 1 LOC de parte | 1 parte (`Punto`), 0 ítems, 1 LOC | Pasa |
| CP16 | RF-03 | Clase con llave de apertura en línea siguiente (Allman) | `public class MiClase\n{\n    public void Hacer()\n    {\n        int i = 0;\n    }\n}` | 1 parte (`MiClase`), 1 ítem, 7 LOC de parte | 1 parte (`MiClase`), 1 ítem, 7 LOC | Pasa |
| CP17 | RF-03 | Namespace con ámbito de archivo (file-scoped) no se cuenta como parte | `namespace MiApp.Dominio;\n\npublic class Servicio\n{\n    public void Ejecutar()\n    {\n    }\n}` | 1 parte (`Servicio`), el namespace no genera parte | 1 parte (`Servicio`) | Pasa |
| CP18 | RF-03, RF-05 | Clase anidada (cada parte con su propio LOC; suma $\le$ total) | `public class Externa { public void M1() {} public class Interna { public void M2() {} } }` | 2 partes: `Interna` (1 ítem, 6 LOC), `Externa` (1 ítem, 6 LOC). Suma partes (12) $\le$ Total (12) | 2 partes: `Interna` (1 ítem, 6 LOC), `Externa` (1 ítem, 6 LOC) *(corregido D5)* | Pasa |

---

## 3. Detección y Conteo de Elementos – RF-04

| ID | Requisito | Descripción | Entrada (Fixture) | Resultado esperado (PLANEADO a mano) | Resultado real | Estado |
|---|---|---|---|---|---|---|
| CP19 | RF-04 | Constructores y métodos cuentan como elementos | `public class MiClase\n{\n    public MiClase() { }\n    public void Hacer() { }\n}` | 2 elementos (`MiClase`, `Hacer`) | 2 elementos | Pasa |
| CP20 | RF-04 | Propiedades (con get/set) no cuentan como elementos | `public class MiClase\n{\n    public int Valor { get; set; }\n    public string Nombre { get; init; }\n}` | 0 elementos | 0 elementos | Pasa |
| CP21 | RF-04 | Estructuras de control (`if`, `for`, `while`, `foreach`) dentro de un método no cuentan | `public class MiClase\n{\n    public void Proceso()\n    {\n        if(true) {}\n        for(;;) {}\n        while(false) {}\n        foreach(var x in a) {}\n    }\n}` | 1 elemento (solo `Proceso`) | 1 elemento | Pasa |
| CP22 | RF-04 | Método con cuerpo de expresión (`=>`) cuenta como elemento | `public class MiClase\n{\n    public int Sumar(int a, int b) => a + b;\n}` | 1 elemento (`Sumar`) | 1 elemento | Pasa |
| CP23 | RF-04 | Lambdas y llamadas anidadas a métodos dentro de un método no cuentan | `public class MiClase\n{\n    public void Hacer()\n    {\n        lista.ForEach(x => Console.WriteLine(x));\n    }\n}` | 1 elemento (solo `Hacer`) | 1 elemento | Pasa |

---

## 4. Archivos, Rutas y Errores de E/S – RF-01 / RNF-05

| ID | Requisito | Descripción | Entrada (Fixture) | Resultado esperado (PLANEADO a mano) | Resultado real | Estado |
|---|---|---|---|---|---|---|
| CP24 | RF-01 | Archivo `.cs` individual existente y válido | Archivo físico `Ejemplo.cs` en carpeta temporal | Retorna lista con 1 ruta absoluta normalizada | 1 archivo retornado | Pasa |
| CP25 | RF-01 | Directorio con subdirectorios (búsqueda recursiva) | Directorio temporal con `A.cs` en raíz y `sub/B.cs` | Retorna lista con 2 rutas ordenadas alfabéticamente | 2 archivos retornados | Pasa |
| CP26 | RF-01, RNF-05 | Directorio sin archivos `.cs` | Directorio temporal que contiene únicamente `readme.txt` | Lanza `ArchivoInvalidoException` con mensaje explicativo | Lanza excepción controlada | Pasa |
| CP27 | RNF-05 | Ruta inexistente en el sistema | Ruta `Path.Combine(temp, "no_existe.cs")` | Lanza `ArchivoNoEncontradoException` con mensaje explicativo | Lanza excepción controlada | Pasa |
| CP28 | RNF-05 | Archivo `.cs` vacío (0 bytes) | Archivo físico `Vacio.cs` sin contenido | 0 LOC contables, 0 partes | 0 LOC, 0 partes | Pasa |
| CP29 | RF-01 | Archivo que no posee extensión `.cs` | Archivo `datos.txt` con texto plano | Lanza `ArchivoInvalidoException` indicando extensión incorrecta | Lanza excepción controlada | Pasa |
| CP30 | RF-01 | Ruta encerrada entre comillas dobles | `"C:\ruta temporal\Comillas.cs"` con espacios y comillas | Normaliza la ruta removiendo comillas y la procesa con éxito | Ruta normalizada y procesada | Pasa |

---

## 5. Salida, Flujo y Determinismo – RF-06 / RNF-01 / RNF-05 / RNF-06

| ID | Requisito | Descripción | Entrada (Fixture) | Resultado esperado (PLANEADO a mano) | Resultado real | Estado |
|---|---|---|---|---|---|---|
| CP31 | RF-06 | Formato tabular idéntico a la Tabla 1 del kit | Ejecución con archivo `Demo.cs` (1 clase, 1 método) | Salida contiene columnas: `Part Name`, `Number of Items`, `Part Size` y `Total Size` con total al final | Formato tabular estándar PSP1 completo | Pasa |
| CP32 | RNF-01 | Código de salida 0 en ejecución exitosa | Ejecución con archivo válido `Ok.cs` | Código de retorno = 0 | Retorna 0 | Pasa |
| CP33 | RNF-05 | Código de salida 1 ante error controlado | Ejecución con ruta inexistente | Código de retorno = 1 | Retorna 1 | Pasa |
| CP34 | RNF-05 | Mensaje amigable `ERROR:` sin trazas de excepción | Ejecución con archivo inexistente `fantasma.cs` | Salida inicia con `ERROR:` y no muestra traza de pila (`StackTrace` / `Exception`) | Mensaje amigable en 1 línea | Pasa |
| CP35 | RNF-06 | Determinismo ante ejecuciones idénticas | Ejecutar dos veces consecutivas sobre el mismo archivo | Ambas salidas en texto son exactamente idénticas carácter por carácter | Salidas 1 y 2 100% idénticas | Pasa |
| CP36 | RNF-05 | Argumento con espacios o vacío | Ejecución con argumento `"   "` | Código de retorno = 1 y mensaje `ERROR:` | Retorna 1 y mensaje de error | Pasa |
| CP37 | RF-01 | Entrada interactiva por teclado si no hay argumentos | Ejecución con `args` vacío y ruta inyectada por `Console.In` | Solicita ruta interactivamente e imprime reporte tabular con éxito | Solicita ruta y genera reporte | Pasa |

---

## 6. Prueba Mínima del Kit – Archivos Fuente Reales del Programa

| ID | Requisito | Archivo Fuente Analizado | Conteo Independiente (Script / Manual) | Resultado del Programa (`dotnet test`) | Estado |
|---|---|---|---|---|---|
| CP38-A | RF-02..RF-05 | `Program.cs` | Total: 9 LOC \| Parte: `Program` (1 ítem, 7 LOC) | Total: 9 LOC \| Parte: `Program` (1 ítem, 7 LOC) | Pasa |
| CP38-B | RF-02..RF-05 | `Aplicacion.cs` | Total: 77 LOC \| Parte: `Aplicacion` (5 ítems, 70 LOC) | Total: 77 LOC \| Parte: `Aplicacion` (5 ítems, 70 LOC) | Pasa |
| CP38-C | RF-02..RF-05 | `Presentacion/ConsolaUI.cs` | Total: 67 LOC \| Parte: `ConsolaUI` (6 ítems, 63 LOC) | Total: 67 LOC \| Parte: `ConsolaUI` (6 ítems, 63 LOC) | Pasa |
| CP38-D | RF-02..RF-05 | `Dominio/ContadorCodigo.cs` | Total: 250 LOC \| Parte: `ContadorCodigo` (17 ítems, 245 LOC) | Total: 250 LOC \| Parte: `ContadorCodigo` (17 ítems, 245 LOC) | Pasa |
| CP38-E | RF-02..RF-05 | `Dominio/MetricasParte.cs` | Total: 17 LOC \| Parte: `MetricasParte` (1 ítem, 15 LOC) | Total: 17 LOC \| Parte: `MetricasParte` (1 ítem, 15 LOC) | Pasa |
| CP38-F | RF-02..RF-05 | `Dominio/MetricasPrograma.cs` | Total: 32 LOC \| Parte: `MetricasPrograma` (3 ítems, 28 LOC) | Total: 32 LOC \| Parte: `MetricasPrograma` (3 ítems, 28 LOC) | Pasa |
| CP38-G | RF-02..RF-05 | `Dominio/ArchivoInvalidoException.cs` | Total: 16 LOC \| Parte: `ArchivoInvalidoException` (3 ítems, 14 LOC) | Total: 16 LOC \| Parte: `ArchivoInvalidoException` (3 ítems, 14 LOC) | Pasa |
| CP38-H | RF-02..RF-05 | `Infraestructura/ArchivoNoEncontradoException.cs` | Total: 16 LOC \| Parte: `ArchivoNoEncontradoException` (3 ítems, 14 LOC) | Total: 16 LOC \| Parte: `ArchivoNoEncontradoException` (3 ítems, 14 LOC) | Pasa |
| CP38-I | RF-02..RF-05 | `Infraestructura/LectorArchivosCodigo.cs` | Total: 59 LOC \| Parte: `LectorArchivosCodigo` (4 ítems, 53 LOC) | Total: 59 LOC \| Parte: `LectorArchivosCodigo` (4 ítems, 53 LOC) | Pasa |
