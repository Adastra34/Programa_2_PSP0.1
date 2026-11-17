# Diseño · Pseudocódigo · Programa 2

Se escribe antes de codificar. Cada bloque corresponde a un componente del diagrama de clases (`diagramas/uml_clases.svg`).

## 1. Programa principal (`Aplicacion.Ejecutar`)

```text
INICIO Ejecutar(args, entrada, salida)
    ui ← nueva ConsolaUI(entrada, salida)
    ui.MostrarEncabezado()
    SI args tiene al menos 1 elemento ENTONCES
        ruta ← args[0]
    SINO
        ruta ← ui.PedirRuta()
    FIN SI
    INTENTAR
        archivos  ← LectorArchivosCodigo.ObtenerArchivosFuente(ruta)
        metricas  ← nuevo MetricasPrograma()
        PARA CADA archivo EN archivos
            lineas    ← LectorArchivosCodigo.LeerLineas(archivo)
            parteM    ← ContadorCodigo.ProcesarArchivo(archivo, lineas)
            metricas.AgregarParte(parteM)
        FIN PARA
        ui.MostrarReporte(metricas)
        RETORNAR 0
    CAPTURAR ArchivoNoEncontrado O ArchivoInvalido COMO e
        ui.MostrarError(e.message)
        RETORNAR 1
    FIN INTENTAR
FIN
```

## 2. Lectura y búsqueda de archivos (`LectorArchivosCodigo`)

```text
FUNCIÓN ObtenerArchivosFuente(ruta) : Lista de cadenas
    SI ruta es directorio ENTONCES
        RETORNAR lista de archivos "*.cs" en ruta
    SINO SI archivo existe(ruta) Y extensión es ".cs" ENTONCES
        RETORNAR lista con [ruta]
    SINO
        LANZAR ArchivoNoEncontrado("No se encontró la ruta o archivo «ruta».")
    FIN SI
FIN

FUNCIÓN LeerLineas(rutaArchivo) : Lista de cadenas
    SI NO existe archivo(rutaArchivo) ENTONCES
        LANZAR ArchivoNoEncontrado("No se pudo abrir el archivo «rutaArchivo».")
    FIN SI
    RETORNAR todas las líneas del archivo de texto
FIN
```

## 3. Análisis y conteo de código (`ContadorCodigo`)

```text
FUNCIÓN ProcesarArchivo(nombreArchivo, lineas) : MetricasParte
    nombreParte ← ObtenerNombreDesdeRuta(nombreArchivo)
    cantidadItems ← 0
    tamanioLoc ← 0
    
    PARA CADA linea EN lineas
        lineaLim, ← quitar espacios(linea)
        SI EsLineaContable(lineaLim) ENTONCES
            tamanioLoc ← tamanioLoc + 1
        FIN SI
        SI EsInicioMetodo(lineaLim) ENTONCES
            cantidadItems ← cantidadItems + 1
        FIN SI
    FIN PARA
    
    RETORNAR nueva MetricasParte(nombreParte, cantidadItems, tamanioLoc)
FIN

FUNCIÓN EsLineaContable(linea) : booleano
    // Ignorar líneas vacías, llaves solitarias y comentarios puros
    SI linea está vacía O linea empieza con "//" O linea es "{" O linea es "}" ENTONCES
        RETORNAR FALSO
    FIN SI
    RETORNAR VERDADERO
FIN
```

## 4. Estructuras de Métricas (`MetricasPrograma` y `MetricasParte`)

```text
CLASE MetricasParte
    Atributos: NombreParte (cadena), NumeroElementos (entero), TamanioLOC (entero)
FIN CLASE

CLASE MetricasPrograma
    Atributos: Partes (Lista de MetricasParte)
    
    PROCEDIMIENTO AgregarParte(parte)
        Partes.Agregar(parte)
    FIN
    
    FUNCIÓN ObtenerTamanioTotal() : entero
        total ← 0
        PARA CADA p EN Partes
            total ← total + p.TamanioLOC
        FIN PARA
        RETORNAR total
    FIN
FIN CLASE
```