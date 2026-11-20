# Defect Log – Fase Pruebas (Programa 2 PSP1)

## Resumen de la Fase de Pruebas

Durante la ejecución de las pruebas unitarias automatizadas con xUnit se detectó 1 defecto en el núcleo analítico de conteo (`ContadorCodigo.cs`). Tras la confirmación del usuario, el defecto fue corregido y verificado satisfactoriamente con la suite completa de 47 pruebas pasando.

## Registro de Defectos

| ID | Tipo | Fase de inyección | Fase de remoción | Descripción | Solución | Defecto origen (Fix Ref) | Reparación (min) |
|---|---|---|---|---|---|---|---|
| D5 | 80 Función | Código | Pruebas | La condición `if (pilaPartes.Count == 0)` en `ProcesarLinea` impedía detectar declaraciones de partes anidadas (clases, structs, interfaces o records dentro de otra clase), reportando únicamente la clase contenedora externa y violando la detección completa de partes (RF-03, RF-05). | Se removió la restricción `if (pilaPartes.Count == 0)` para permitir que cualquier parte detectada sea apilada en `pilaPartes`. Al cerrar el bloque de llaves de la parte interna, se desapila y se añade a las partes finalizadas, restaurando la parte externa en el tope de la pila. | — | (completar desde PSP Tracker) |
