# Informe de Pruebas – Programa 2 (PSP1)

## 1. Información General y Entorno

- **Proyecto:** Programa 2 – Contador de Líneas de Código (LOC) en C#
- **Proceso:** Proceso de Software Personal (SEI PSP1) – Fase de Pruebas
- **Sistema Operativo:** Windows 11 (Arquitectura x64)
- **Plataforma de Ejecución:** .NET 9.0 SDK (.NETCoreApp,Version=v9.0)
- **Framework de Pruebas:** xUnit 2.9.2, Microsoft.NET.Test.Sdk 17.12.0, VSTest 17.14.1
- **Fecha de Ejecución:** 20 de noviembre de 2026

---

## 2. Comandos Exactos para Repetir las Pruebas

Para reproducir la compilación, pruebas automatizadas y ejecuciones manuales desde la raíz del repositorio:

```powershell
# 1. Compilación limpia de la solución completa (0 errores, 0 advertencias)
dotnet build Programa2.ContadorLOC.sln

# 2. Ejecución de la suite completa de pruebas automatizadas con xUnit
dotnet test --logger "console;verbosity=normal"

# 3. Ejecución de pruebas manuales con fixtures individuales
dotnet run --project src/Programa2.ContadorLOC -- docs/Pruebas/evidencias/ejecucion1_20261120_0017/entrada.cs

# 4. Ejecución sobre directorio completo
dotnet run --project src/Programa2.ContadorLOC -- docs/Pruebas/evidencias/ejecucion4_20261120_0017/entrada

# 5. Ejecución del kit sobre el propio código fuente del programa
dotnet run --project src/Programa2.ContadorLOC -- src/Programa2.ContadorLOC
```

---

## 3. Resumen Ejecutivo de Pruebas

| Métrica | Valor |
|---|---|
| **Total casos de prueba planeados** | 38 casos conceptuales (47 pruebas automatizadas en xUnit) |
| **Casos ejecutados** | 47 automatizados + 7 ejecuciones manuales independientes |
| **Casos superados (pasados)** | 47 de 47 (100%) |
| **Casos fallados inicialmente** | 1 (CP18 – Clases anidadas, registrado como D5) |
| **Defectos encontrados y removidos** | 1 defecto de función (D5 en [defect_log_pruebas.md](defect_log_pruebas.md)) |
| **Tiempo de ejecución de la suite** | ~0.5 segundos |

---

## 4. Matriz de Cobertura de Requisitos (PSP1)

| Requisito | Descripción | Casos de Prueba Asociados | Estado de Cobertura |
|---|---|---|---|
| **RF-01** | Lectura de archivo individual o directorio recursivo con extensión `.cs` | CP24, CP25, CP26, CP29, CP30, CP37 | 100% Cubierto |
| **RF-02** | Conteo de LOC física (excluyendo comentarios y líneas vacías; contando llaves y código con comentarios) | CP01 a CP09 | 100% Cubierto |
| **RF-03** | Detección de partes (`class`, `struct`, `interface`, `record`, clases anidadas) | CP10 a CP18 | 100% Cubierto |
| **RF-04** | Detección de elementos (métodos y constructores; exclusión de propiedades y estructuras internas) | CP19 a CP23 | 100% Cubierto |
| **RF-05** | Conteo de LOC de partes (suma de partes $\le$ total del programa) | CP10, CP11, CP18, CP38-A..I | 100% Cubierto |
| **RF-06** | Formato de reporte tabular alineado con 4 columnas y total al final (Tabla 1 del kit) | CP31, Ejecuciones manuales 1 a 7 | 100% Cubierto |
| **RF-07** | Evidencias de trazabilidad y salida de pruebas | Ejecuciones 1 a 7 con `resumen.md` y `salida.txt` | 100% Cubierto |
| **RNF-01** | Aplicación de consola en C# .NET 9 | CP32 | 100% Cubierto |
| **RNF-02** | Arquitectura por capas desacoplada | Pruebas unitarias directas sobre Dominio, Infraestructura y Aplicación | 100% Cubierto |
| **RNF-03** | Simplicidad algorítmica sin dependencias pesadas | Verificado en diseño y código | 100% Cubierto |
| **RNF-04** | Cumplimiento estricto del estándar de codificación y conteo | Sin números mágicos, estilo Allman, nullable | 100% Cubierto |
| **RNF-05** | Errores controlados con prefijo `ERROR:` sin excepciones visibles | CP26, CP27, CP28, CP29, CP33, CP34, CP36 | 100% Cubierto |
| **RNF-06** | Determinismo (mismas entradas generan idénticas salidas) | CP35 | 100% Cubierto |

---

## 5. Tabla Comparativa: Planeado vs. Real

El detalle exhaustivo de cada caso de prueba con su fixture de entrada y conteo a mano se documenta en [casos_prueba.md](casos_prueba.md). A continuación se resumen los resultados consolidados por grupo:

| Grupo de Pruebas | Casos | Planeado (Esperado) | Real (Obtenido) | Estado |
|---|---|---|---|---|
| **Conteo LOC (E-12)** | CP01 - CP09 | Descarte de líneas vacías, comentarios simples `//`, documentación `///` y bloques `/* */`. Conteo de `{`, `}` y líneas mixtas. | Conteo exacto coincidente con el cálculo a mano en todos los casos. | Superado (9/9) |
| **Partes (E-05)** | CP10 - CP18 | Reconocimiento de `class`, `interface`, `struct`, `record` (bloque y posicional) y clases anidadas. Suma de partes $\le$ total. | CP18 falló inicialmente (solo detectaba clase externa). Tras remover restricción de nivel en `ContadorCodigo.cs`, todas las partes se detectan con su LOC independiente. | Superado (9/9) |
| **Elementos** | CP19 - CP23 | Métodos normales, constructores y métodos con expresión `=>`. Exclusión de propiedades, `if/for/while/foreach` y lambdas. | Detección exacta de elementos por parte sin falsos positivos en propiedades ni instrucciones internas. | Superado (5/5) |
| **Archivos y Rutas** | CP24 - CP30 | Archivos válidos, directorios con subdirectorios, rutas entre comillas. Errores controlados en ausencia de `.cs` o archivo inexistente. | Lectura recursiva correcta y lanzamiento de excepciones de dominio/infraestructura controladas. | Superado (7/7) |
| **Salida y Flujo** | CP31 - CP37 | Tabla alineada PSP1 (`Part Name`, `Number of Items`, `Part Size`, `Total Size`), código 0 en éxito, 1 en error, sin stack traces. | Formato visual 100% idéntico a especificación. Códigos de salida y determinismo confirmados. | Superado (7/7) |
| **Prueba Mínima del Kit** | CP38 (A a I) | Conteo de los 9 archivos fuente reales del programa comparados contra conteo independiente por script y manual. | Coincidencia exacta del 100% en LOC total, ítems y LOC de cada parte en los 9 archivos analizados. | Superado (9/9) |

---

## 6. Defectos Encontrados y Solucionados

Durante las pruebas automatizadas se descubrió un defecto funcional:

- **ID:** **D5**
- **Tipo:** 80 Función
- **Inyección:** Fase de Código
- **Remoción:** Fase de Pruebas
- **Descripción:** En `ContadorCodigo.ProcesarLinea`, la instrucción `if (pilaPartes.Count == 0)` impedía detectar partes anidadas cuando el analizador ya se encontraba dentro de una clase contenedora.
- **Solución:** Se retiró la comprobación de pila vacía, permitiendo apilar dinámicamente partes internas. Al salir del ámbito (`}`), la parte interna se finaliza y se reanuda la parte padre.
- **Registro:** Detallado en [defect_log_pruebas.md](defect_log_pruebas.md).

---

## 7. Evidencias Generadas

Las evidencias físicas de la ejecución se encuentran archivadas en `docs/Pruebas/evidencias/`:

1. **Suite Automatizada:**
   - [dotnet_test_20261120_0017.txt](evidencias/dotnet_test_20261120_0017.txt) – Registro completo de la ejecución de `dotnet test` (47 pruebas superadas, 0 advertencias, 0 errores).

2. **Ejecuciones Manuales del Programa (`dotnet run`):**
   - **Ejecución 1:** Clase individual simple (`Calculadora.cs`) $\rightarrow$ [resumen.md](evidencias/ejecucion1_20261120_0017/resumen.md), [salida.txt](evidencias/ejecucion1_20261120_0017/salida.txt)
   - **Ejecución 2:** Clases anidadas (`ModuloInterno` en `Contenedor`) $\rightarrow$ [resumen.md](evidencias/ejecucion2_20261120_0017/resumen.md), [salida.txt](evidencias/ejecucion2_20261120_0017/salida.txt)
   - **Ejecución 3:** Tipos múltiples (`interface`, `struct`, `record`) $\rightarrow$ [resumen.md](evidencias/ejecucion3_20261120_0017/resumen.md), [salida.txt](evidencias/ejecucion3_20261120_0017/salida.txt)
   - **Ejecución 4:** Directorio con subdirectorios recursivos $\rightarrow$ [resumen.md](evidencias/ejecucion4_20261120_0017/resumen.md), [salida.txt](evidencias/ejecucion4_20261120_0017/salida.txt)
   - **Ejecución 5:** Manejo de ruta inexistente (error controlado) $\rightarrow$ [resumen.md](evidencias/ejecucion5_20261120_0017/resumen.md), [salida.txt](evidencias/ejecucion5_20261120_0017/salida.txt)
   - **Ejecución 6:** Directorio sin archivos `.cs` (error controlado) $\rightarrow$ [resumen.md](evidencias/ejecucion6_20261120_0017/resumen.md), [salida.txt](evidencias/ejecucion6_20261120_0017/salida.txt)
   - **Ejecución 7:** Archivo fuente del programa (`Program.cs`) $\rightarrow$ [resumen.md](evidencias/ejecucion7_20261120_0017/resumen.md), [salida.txt](evidencias/ejecucion7_20261120_0017/salida.txt)

---

## 8. Conclusiones

1. **Calidad y Robustez:** El Programa 2 cumple a cabalidad con todos los requerimientos funcionales (RF-01 a RF-06) y no funcionales (RNF-01 a RNF-06) del SEI PSP1.
2. **Efectividad del Proceso PSP:** La fase de pruebas demostró su valor al detectar una sutil discrepancia en clases anidadas (defecto D5), la cual fue diagnosticada, autorizada y resuelta sistemáticamente antes de la entrega final.
3. **Cero Regresiones:** Tras la corrección de D5, el 100% de las pruebas automatizadas y las pruebas de fuentes reales del kit pasan sin discrepancias, con 0 errores y 0 advertencias de compilación.
4. **Listo para Postmortem:** El proyecto cuenta con trazabilidad completa de requisitos a pruebas, log de defectos y evidencias documentadas, quedando preparado para el análisis postmortem de PSP1.
