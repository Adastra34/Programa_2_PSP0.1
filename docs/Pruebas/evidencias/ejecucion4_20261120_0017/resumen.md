# Resumen de Ejecución 4

- **Fecha y hora:** 2026-11-20 00:18
- **Tipo de prueba:** Directorio con subdirectorios y múltiples archivos `.cs` (RF-01).
- **Entrada:** `docs/Pruebas/evidencias/ejecucion4_20261120_0017/entrada/`
- **Comando ejecutado:** `dotnet run --project src/Programa2.ContadorLOC -- docs/Pruebas/evidencias/ejecucion4_20261120_0017/entrada`

## Comparación de Resultados

| Métrica | Planeado (Esperado) | Real (Obtenido) | Estado |
|---|---|---|---|
| Partes detectadas | 2 (`ServicioA`, `ServicioB`) | 2 (`ServicioA`, `ServicioB`) | Pasa |
| Ítems y tamaño `ServicioA` | 1 ítem, 6 LOC | 1 ítem, 6 LOC | Pasa |
| Ítems y tamaño `ServicioB` | 1 ítem, 6 LOC | 1 ítem, 6 LOC | Pasa |
| Tamaño total programa | 12 LOC | 12 LOC | Pasa |
| Recursión de directorios | Encuentra `.cs` en subcarpetas | Lee correctamente `sub/ServicioB.cs` | Pasa |
| Código de salida | 0 | 0 | Pasa |
