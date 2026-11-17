# Estimación – Programa 2 LOC PSP1

## 1. Tamaño estimado (LOC añadidas y modificadas)

Estimación por juicio propio. Base, eliminado, modificado y reutilizado valen 0.

| Parte | Tipo | Elementos | LOC est. |
|---|---|---|---|
| ResultadoConteo + ParteContada | Datos | 4 | 25 |
| ClasificadorLineas | Cálculo | 4 | 45 |
| AnalizadorPartes | Cálculo | 6 | 70 |
| LectorArchivos | E/S | 2 | 20 |
| ContarLocCasoDeUso | Lógica | 3 | 30 |
| ConsolaUI | E/S | 4 | 40 |
| Program | Lógica | 1 | 10 |
| **Total (nuevo)** | | **24** | **≈ 240** |

## 2. Tiempo estimado por fase

Estimación por fases

| Fase | Minutos | Qué incluye |
|---|---|---|
| Planeación | 40 | Requisitos, diseño conceptual, estimación |
| Diseño | 60 | Pseudocódigo, traza de escritorio, UML y capas |
| Código | 100 | Implementación capa por capa, 2 sesiones |
| Compilación | 25 | Leer errores CSxxxx y corregir |
| Pruebas | 70 | Casos, ejecución y corrección de fallas |
| Postmortem | 30 | PPS, defectos, PIP |
| **Subtotal** | **325** | |
| Contingencia (≈ 10 %) | 35 | Interrupciones y repetición de pruebas |
| **Total planeado** | **360** | 6 horas |

Productividad planeada: 240 LOC ÷ 6 h = 40 LOC/hora.

## 4. Intervalo de tiempo

| Escenario | Minutos |
|---|---|
| Mínimo (sin contingencia) | 325 |
| Esperado | 360 |
| Máximo (contingencia ≈ 25 %) | 405 |


