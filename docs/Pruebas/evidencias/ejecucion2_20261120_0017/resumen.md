# Resumen de Ejecución 2

- **Fecha y hora:** 2026-11-20 00:18
- **Tipo de prueba:** Archivo `.cs` con clase anidada (`ModuloInterno` dentro de `Contenedor`).
- **Entrada:** `docs/Pruebas/evidencias/ejecucion2_20261120_0017/entrada.cs`
- **Comando ejecutado:** `dotnet run --project src/Programa2.ContadorLOC -- docs/Pruebas/evidencias/ejecucion2_20261120_0017/entrada.cs`

## Comparación de Resultados

| Métrica | Planeado (Esperado) | Real (Obtenido) | Estado |
|---|---|---|---|
| Partes detectadas | 2 (`ModuloInterno`, `Contenedor`) | 2 (`ModuloInterno`, `Contenedor`) | Pasa |
| Ítems `ModuloInterno` | 1 (`Procesar`) | 1 | Pasa |
| Tamaño `ModuloInterno` | 6 LOC | 6 LOC | Pasa |
| Ítems `Contenedor` | 1 (`MetodoContenedor`) | 1 | Pasa |
| Tamaño `Contenedor` | 6 LOC | 6 LOC | Pasa |
| Tamaño total programa | 12 LOC | 12 LOC | Pasa |
| Cumplimiento RF-05 | Suma partes (12) $\le$ Total (12) | Suma partes (12) = Total (12) | Pasa |
| Código de salida | 0 | 0 | Pasa |
