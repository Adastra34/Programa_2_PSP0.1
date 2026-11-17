# Diseño · Programa 2 (Contador de Líneas de Código - LOC)

Entradas de esta fase: Requisitos de PSP1 (Assignment Kit - Program 2).
Salidas: este documento, `pseudocodigo.md`, `estandar_codificacion.md` y los diagramas de `diagramas/`.

## 1. Arquitectura en capas

![Arquitectura](diagramas/arquitectura_capas.svg)

| Capa | Carpeta | Responsabilidad | Requisitos PSP1 |
|---|---|---|---|
| Presentación | `Presentacion/` | Pedir ruta de directorios/archivos y mostrar el reporte tabular o errores en consola. | Interfaz CLI, Salida Tabular |
| Aplicación | `Aplicacion.cs` | Orquestar: argumentos → leer archivos → procesar métricas → mostrar reporte; traducir excepciones a códigos de salida. | Gestión de errores, Códigos `0` y `1` |
| Infraestructura | `Infraestructura/` | Buscar archivos fuente `*.cs` en el sistema de archivos y leer su contenido línea por línea. | Acceso a archivos I/O |
| Dominio | `Dominio/` | Analizador algorítmico (`ContadorCodigo`), cálculo de partes (clases), ítems (métodos) y tamaño (LOC). No conoce la consola ni archivos. | Conteo físico y de partes |

**Regla de dependencia:** Las flechas apuntan hacia el dominio. El dominio no depende de ninguna otra capa, por lo que se prueba de forma aislada sin E/S ni consola.

## 2. Diagrama de clases (UML)

![Clases](diagramas/uml_clases.svg)

## 3. Flujo del programa (diagrama de actividad)

![Flujo](diagramas/flujo_programa.svg)

## 4. Diseño de la interfaz (consola)

![Interfaz](diagramas/interfaz_consola.svg)

* Encabezado fijo de tres líneas identificando el programa PSP1.
* Salida en formato tabular alineado con las columnas: `Part Name`, `Number of Items`, `Part Size`, y `Total Size`.
* Mensajes de error en una sola línea que empiezan por `ERROR:` indicando la causa y acción. Código de salida `1` en caso de error y `0` en éxito.

## 5. Datos y Persistencia

**No se usa base de datos.** El programa analiza el código fuente directamente desde el sistema de archivos.
Formato de entrada: Archivos de texto plano `*.cs` en codificación UTF-8.

## 6. Trazabilidad requisito → diseño

| Requisito PSP1 | Clase / método | Caso de prueba |
|---|---|---|
| Obtención de ruta y argumentos | `Aplicacion.Ejecutar`, `ConsolaUI.PedirRuta` | CP-01, CP-02 |
| Búsqueda y lectura de archivos `.cs` | `LectorArchivosCodigo.ObtenerArchivosFuente`, `LeerLineas` | CP-03, CP-04 |
| Identificación y conteo de partes (clases) | `ContadorCodigo.ProcesarArchivo` | CP-05 |
| Identificación y conteo de ítems (métodos) | `ContadorCodigo.AnalizarMetodos` | CP-06 |
| Cálculo de tamaño físico y total (LOC) | `MetricasPrograma.ObtenerTamanioTotal` | CP-07 |
| Gestión de excepciones y errores de E/S | `ArchivoNoEncontradoException`, `Aplicacion` | CP-08, CP-09 |