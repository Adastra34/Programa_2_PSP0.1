# Resumen del Plan del Proyecto (PPS) – Programa 2 (PSP1)

**Estudiante / Desarrollador:** Proceso de Software Personal (SEI PSP1)  
**Fecha:** 20 de noviembre de 2026  
**Proyecto:** Programa 2 – Contador de Líneas de Código (LOC) en C# (.NET 9)  
**Repositorio:** [Programa_2_PSP0.1](https://github.com/Adastra34/Programa_2_PSP0.1.git) (rama `main`)

---

## 1. Resumen de Tiempos (Plan vs. Real)

| Fase PSP1 | Tiempo Plan Estimado (min)* | Tiempo Real Tracker (min) | % del Total Tracker (4 h 39 min)** | % Consolidado Final (299 min) | Diferencia vs. Plan Base (Δ min) |
|---|---|---|---|---|---|
| **Planificación** | 40 | 50 | 18 % | 16,7 % | +10 min (+25,0 %) |
| **Diseño** | 60 | 48 | 17 % | 16,1 % | −12 min (−20,0 %) |
| **Código** | 100 | 83 (1 h 23 min) | 30 % | 27,8 % | −17 min (−17,0 %) |
| **Revisión de Código** | — (incluida en Código) | 18 | 6 % | 6,0 % | +18 min |
| **Compilación** | 25 | 25 | 9 % | 8,4 % | 0 min (0,0 %) |
| **Pruebas** | 70 | 55 | 20 % | 18,4 % | −15 min (−21,4 %) |
| **Post-Mortem** | 30 | 20 | — (en curso) | 6,7 % | −10 min (−33,3 %) |
| **TOTAL** | **300 min (5 h 00 min)*** | **299 min (4 h 59 min)** | **100 %** | **100,0 %** | **−1 min (−0,3 %)** |

> [!NOTE]
> * **Plan Global en PSP Tracker:** El tracker establece un plan global de **5 h 00 min (300 min)**. Los minutos plan por fase corresponden a la estimación base de [estimacion.md](../Planeación/estimacion.md) ajustada a la meta del proyecto.
> * **Datos del PSP Tracker:** El monitor registra **4 h 39 min reales (279 min)** previo al cierre de Post-Mortem con un **−7 % de error de estimación**. Al incorporar los 20 min de Post-Mortem, el tiempo total es de **299 minutos (4 h 59 min)**, con una variación de apenas **−1 minuto (−0,3 %)** frente al plan de 300 min.
> * **Interrupciones:** Se registraron y descontaron **68 minutos** de interrupciones externas (excluidas del tiempo neto de trabajo).

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
| **D5** | 80 Función | Código | Pruebas | 5 | Restricción `pilaPartes.Count == 0` impedía registrar clases anidadas; se retiró para apilamiento dinámico. |

> [!NOTE]
> * **Sincronización de Defectos:** El PSP Tracker refleja **4 defectos** cerrados en Compilación con **42 minutos** acumulados de reparación (D1: 5 min, D2: 10 min, D3: 12 min, D4: 15 min).
> * El defecto **D5** corresponde a la corrección del soporte de clases anidadas detectado durante la automatización de pruebas unitarias, sumando **5 minutos** de reparación para un total general de 5 defectos y 47 minutos de reparación.

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
