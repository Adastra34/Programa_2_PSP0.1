# Requisitos – Programa 2 PSP1

Contador de líneas de código (LOC) en C# (.NET 9) con arquitectura por capas.

**Objetivo:** contar LOC de un archivo fuente `.cs` y mostrar el total, el tamaño de cada parte y el número de elementos de cada parte.

## 1. Requisitos funcionales

| ID | Requisito | Criterio de aceptación |
|---|---|---|
| RF-01 | Leer un archivo `.cs` desde una ruta | Un archivo válido se lee completo |
| RF-02 | Contar LOC según el estándar de conteo | Se excluyen líneas en blanco y líneas solo de comentario |
| RF-03 | Identificar partes (class, record, struct, interface) | Cada parte aparece con su nombre |
| RF-04 | Contar elementos por parte (métodos y constructores) | El conteo coincide con el conteo manual |
| RF-05 | Contar el LOC de cada parte | La suma de partes es igual o menor que el total (el código fuera de partes no se atribuye) |
| RF-06 | Imprimir la tabla del kit: Parte, N.º de elementos, Tamaño y total al final | Formato igual a la Tabla 1 del kit |
| RF-07 | Opcional (pista del kit): imprimir el archivo con numeración de línea, sin numerar los comentarios | Sirve como evidencia de las pruebas |

## 2. Requisitos no funcionales

| ID | Requisito |
|---|---|
| RNF-01 | C# .NET 9, aplicación de consola |
| RNF-02 | Arquitectura por capas: Presentación, Aplicación, Dominio, Infraestructura |
| RNF-03 | Sencillo: el kit indica no escribir un contador sofisticado |
| RNF-04 | Cumple el estándar de codificación y de conteo, actualizado y reportado |
| RNF-05 | Errores controlados (archivo inexistente o vacío) sin excepciones visibles al usuario |
| RNF-06 | Resultado determinista para la misma entrada |

## 3. Estándar de conteo propuesto

- **Cuenta:** toda línea física con código, incluidas las que solo tienen `{` o `}`.
- **No cuenta:** líneas en blanco y líneas que solo tienen comentario (`//`, `///`, `/* */`).
- **Parte:** declaración de tipo (class, record, struct, interface).
- **Elemento:** método o constructor, no propiedades.
- La detección usa expresiones regulares más conteo de llaves, sin comentarios especiales; así el Programa 1 no hay que modificarlo.

## 4. Pruebas mínimas exigidas por el kit

Contar el tamaño total y el de las partes de los programas 1 y 2.

## 5. Salida esperada (formato de la Tabla 1 del kit)

| Parte | N.º de elementos | Tamaño de la parte | Tamaño total |
|---|---|---|---|
| ABC | 3 | 86 | |
| DEF | 2 | 8 | |
| GHI | 4 | 92 | |
| | | | 212 |
