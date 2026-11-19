# Defect Log – Fase Compilación (Programa 2 PSP1)

## Resultado de compilación

```
Compilación correcta.
    0 Advertencia(s)
    0 Errores
```

## Defectos encontrados al revisar

| ID | Tipo | Fase de inyección | Fase de remoción | Descripción | Solución | Reparación (min) |
|----|------|-------------------|------------------|-------------|----------|------------------|
| D1 | 10 Documentación | Planeación | Compilación | RF-01 solo decía archivo; el diseño y el código aceptan archivo o directorio | Ampliar RF-01 y el objetivo | 5 |
| D2 | 10 Documentación | Diseño | Compilación | La trazabilidad citaba un método inexistente, AnalizarMetodos | Citar ProcesarLinea y EsDeclaracionElemento | 10 |
| D3 | 10 Documentación | Diseño | Compilación | E-12 decía omitir llaves sueltas, pero requisitos y programa las cuentan | Reescribir E-12 | 12 |
| D4 | 40 Asignación | Código | Compilación | Número mágico 2 en tres lugares (viola E-04) | Constantes LongitudMarcaComentario y LongitudSecuenciaEscape | (completar desde PSP Tracker) |
