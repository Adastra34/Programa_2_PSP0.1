# Resumen del Plan del Proyecto (PPS) – Programa 2 (PSP1)

**Estudiante / Desarrollador:** Proceso de Software Personal (SEI PSP1)  
**Fecha:** 20 de noviembre de 2026  
**Proyecto:** Programa 2 – Contador de Líneas de Código (LOC) en C# (.NET 9)  
**Repositorio:** [Programa_2_PSP0.1](https://github.com/Adastra34/Programa_2_PSP0.1.git) (rama `main`)

---

## 1. Resumen de Tiempos (Plan vs. Real)

| Fase PSP1 | Tiempo Plan (min) | Tiempo Real (min) | % del Tiempo Real | Diferencia (Δ min) |
|---|---|---|---|---|
| **Planificación** | (completar desde PSP Tracker) | 50 | 16,7 % | (completar desde PSP Tracker) |
| **Diseño** | (completar desde PSP Tracker) | 48 | 16,1 % | (completar desde PSP Tracker) |
| **Código** | (completar desde PSP Tracker) | 83 | 27,8 % | (completar desde PSP Tracker) |
| **Revisión de Código** | (completar desde PSP Tracker) | 18 | 6,0 % | (completar desde PSP Tracker) |
| **Compilación** | (completar desde PSP Tracker) | 25 | 8,4 % | (completar desde PSP Tracker) |
| **Pruebas** | (completar desde PSP Tracker) | 55 | 18,4 % | (completar desde PSP Tracker) |
| **Post-Mortem** | (completar desde PSP Tracker) | 20 | 6,7 % | (completar desde PSP Tracker) |
| **TOTAL** | **300** | **299** | **100,0 %** | **−1 min (−0,3 %)** |

> [!NOTE]
> * El tiempo total planeado en el PSP Tracker fue de **300 minutos** (mientras que en `docs/Planeación/estimacion.md` se registraron 360 min incluyendo contingencia de 35 min y 25 min base adicionales).
> * El tiempo total real fue de **299 minutos**, registrando una variación de apenas **−1 minuto (−0,3 %)** respecto al plan del tracker.
> * Se registraron **68 minutos de interrupciones** externas durante el desarrollo, las cuales fueron debidamente excluidas del tiempo neto de trabajo.

---

## 2. Resumen de Tamaño (Plan vs. Real)

| Métrica | Plan | Real | Variación (%) |
|---|---|---|---|
| **Partes (Tipos)** | 7 | 9 | +28,6 % |
| **Elementos (Métodos / Constructores)** | 24 | 43 | +79,2 % |
| **Líneas de Código (LOC contables)** | 240 | 543 | **+126,3 %** |

### Desglose por Archivo Fuente Real

| Archivo / Componente | LOC Total | Elementos (Métodos / Ctor) | LOC de Parte | Código Fuera de Parte |
|---|---|---|---|---|
| `Program.cs` | 9 | 1 | 7 | 2 |
| `Aplicacion.cs` | 77 | 5 | 70 | 7 |
| `Presentacion/ConsolaUI.cs` | 67 | 6 | 63 | 4 |
| `Dominio/ContadorCodigo.cs` | 250 | 17 | 245 | 5 |
| `Dominio/MetricasParte.cs` | 17 | 1 | 15 | 2 |
| `Dominio/MetricasPrograma.cs` | 32 | 3 | 28 | 4 |
| `Dominio/ArchivoInvalidoException.cs` | 16 | 3 | 14 | 2 |
| `Infraestructura/ArchivoNoEncontradoException.cs` | 16 | 3 | 14 | 2 |
| `Infraestructura/LectorArchivosCodigo.cs` | 59 | 4 | 53 | 6 |
| **TOTAL CONSOLIDADO** | **543** | **43** | **509** | **34** |

> [!IMPORTANT]
> Cumplimiento de **RF-05**: La suma de LOC de las partes ($509$) es menor o igual al total general de LOC contables del programa ($543$). Las 34 líneas restantes corresponden a directivas `namespace` y `using` fuera del cuerpo de los tipos.

---

## 3. Productividad

| Parámetro | Plan | Real | Variación |
|---|---|---|---|
| **Tiempo de desarrollo (horas)** | 5,00 h (300 min) | 4,98 h (299 min) | −0,02 h |
| **Tamaño desarrollado (LOC)** | 240 LOC | 543 LOC | +303 LOC |
| **Productividad (LOC/hora)** | **48 LOC/h** | **≈ 109 LOC/h** (108,96) | **+127,1 %** |

---

## 4. Resumen de Defectos

### Registro Consolidado de Defectos (D1 – D5)

| ID | Tipo de Defecto | Fase Inyección | Fase Remoción | Tiempo Rep. (min) | Descripción Resumida |
|---|---|---|---|---|---|
| **D1** | 10 Documentación | Planeación | Compilación | 5 | RF-01 restringía a archivo; se amplió a archivo o directorio en requisitos y objetivo. |
| **D2** | 10 Documentación | Diseño | Compilación | 10 | Trazabilidad citaba método inexistente `AnalizarMetodos`; corregido a `ProcesarLinea` y `EsDeclaracionElemento`. |
| **D3** | 10 Documentación | Diseño | Compilación | 12 | Estándar E-12 indicaba omitir llaves sueltas; se reescribió para contarlas según requisitos y programa. |
| **D4** | 40 Asignación | Código | Compilación | 15 | Números mágicos `2` en tres puntos violando estándar E-04; reemplazados por constantes nombradas. |
| **D5** | 80 Función | Código | Pruebas | (completar desde PSP Tracker) | Restricción `pilaPartes.Count == 0` impedía registrar clases anidadas; se retiró para apilamiento dinámico. |

### Métricas de Inyección y Remoción de Defectos

| Fase | Defectos Inyectados | Defectos Removidos |
|---|---|---|
| **Planeación** | 1 (D1) | 0 |
| **Diseño** | 2 (D2, D3) | 0 |
| **Código** | 2 (D4, D5) | 0 |
| **Compilación** | 0 | 4 (D1, D2, D3, D4) |
| **Pruebas** | 0 | 1 (D5) |
| **TOTAL** | **5** | **5** |

- **Densidad de Defectos:**  
  $$\text{Densidad} = \frac{5\text{ defectos}}{0,543\text{ KLOC}} \approx 9,2\text{ defectos/KLOC}$$

---

## 5. Resumen de Pruebas

- **Pruebas Automatizadas:** **47 superadas de 47 (100%)** en xUnit ([dotnet_test_20261120_0017.txt](../Pruebas/evidencias/dotnet_test_20261120_0017.txt)).
- **Pruebas Manuales:** 7 ejecuciones documentadas con fixtures, salida de consola completa y resumen planeado vs. real en `docs/Pruebas/evidencias/ejecucionN_20261120_0017/`.
- **Detalle completo:** Consultar [informe_pruebas.md](../Pruebas/informe_pruebas.md) y [casos_prueba.md](../Pruebas/casos_prueba.md).
