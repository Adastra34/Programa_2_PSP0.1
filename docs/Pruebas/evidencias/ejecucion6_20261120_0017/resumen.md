# Resumen de Ejecución 6

- **Fecha y hora:** 2026-11-20 00:18
- **Tipo de prueba:** Manejo de errores controlados: directorio sin archivos `.cs` (RF-01, RNF-05).
- **Entrada:** Directorio `docs/Pruebas/evidencias/ejecucion6_20261120_0017/entrada` (contiene solo `readme.txt`).
- **Comando ejecutado:** `dotnet run --project src/Programa2.ContadorLOC -- docs/Pruebas/evidencias/ejecucion6_20261120_0017/entrada`

## Comparación de Resultados

| Métrica | Planeado (Esperado) | Real (Obtenido) | Estado |
|---|---|---|---|
| Mensaje emitido | Inicia con `ERROR:` indicando que no se encontraron archivos `.cs` | `ERROR: No se encontraron archivos con extensión '.cs' en '...'` | Pasa |
| Traza de excepción | Ninguna excepción visible al usuario | Sin excepciones o stack traces | Pasa |
| Código de salida | 1 | 1 | Pasa |
