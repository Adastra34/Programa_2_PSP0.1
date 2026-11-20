# Resumen de Ejecución 5

- **Fecha y hora:** 2026-11-20 00:18
- **Tipo de prueba:** Manejo de errores controlados: ruta inexistente (RNF-05).
- **Entrada:** `C:\ruta_inexistente_prueba.cs`
- **Comando ejecutado:** `dotnet run --project src/Programa2.ContadorLOC -- C:\ruta_inexistente_prueba.cs`

## Comparación de Resultados

| Métrica | Planeado (Esperado) | Real (Obtenido) | Estado |
|---|---|---|---|
| Mensaje emitido | Inicia con `ERROR:` indicando que no se encontró | `ERROR: No se encontró la ruta o archivo: 'C:\ruta_inexistente_prueba.cs'.` | Pasa |
| Traza de excepción | Ninguna excepción visible al usuario | Sin excepciones o stack traces | Pasa |
| Código de salida | 1 | 1 | Pasa |
