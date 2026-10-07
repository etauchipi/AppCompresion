# AppCompresion

## Descripción

AppCompresion es una biblioteca de clases (DLL) desarrollada en C# que
proporciona funcionalidades para comprimir y descomprimir objetos `DataSet` de
ADO.NET. Utiliza `DeflateStream` para la compresión y codificación en Base64
para facilitar el transporte y almacenamiento eficiente de los datos en memoria.

## Pila Tecnológica (Tech Stack)

- **Lenguaje:** C#
- **Framework:** .NET Framework 4.5
- **Tecnologías:** ADO.NET (`System.Data`), `System.IO.Compression`

## Instalación y Configuración

Para compilar y utilizar esta biblioteca en tu proyecto local, sigue
estos pasos:

1. **Clonar el repositorio:**

   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd AppCompresion
   ```

2. **Requisitos Previos:**
   - Visual Studio, MSBuild o el SDK de .NET con soporte para
     .NET Framework 4.5 (Targeting Pack / Developer Pack).

3. **Compilar el Proyecto:**
   Puedes abrir el archivo de la solución `AppCompresion.sln` en Visual Studio
   y compilarla (Build) o usar `msbuild` desde la línea de comandos:

   ```bash
   msbuild AppCompresion.sln /p:Configuration=Release
   ```

4. **Configuración de Variables y Dependencias:**
   - No requiere variables de entorno especiales.
   - Las dependencias principales (`System.Data`, `System.Xml`, etc.) son
     nativas de .NET Framework.
   - Una vez compilado, toma el archivo `.dll` generado en la ruta
     `AppCompresion/bin/Release/` o `AppCompresion/bin/Debug/`.
   - Agrega la referencia a tu proyecto en Visual Studio
     (Add Reference -> Browse -> AppCompresion.dll).

## Estructura de Carpetas

La estructura principal del repositorio es la siguiente:

```text
/
├── AppCompresion.sln        # Archivo de solución de Visual Studio.
├── README.md                # Documentación del proyecto (este archivo).
└── AppCompresion/           # Carpeta del proyecto de la biblioteca principal.
    ├── AppCompresion.csproj # Archivo de configuración del proyecto C#.
    ├── AppCompresion.cs     # Código fuente (compresión/descompresión).
    └── Properties/          # Propiedades y metadatos (AssemblyInfo.cs).
```

## Guía de Uso y Ejemplos

La clase principal `AppCompresion` expone tres métodos públicos principales.
A continuación, se muestran ejemplos básicos de uso:

### 1. Comprimir un DataSet

Convierte un objeto `DataSet` en un `MemoryStream`, lo comprime mediante
`DeflateStream` y devuelve el resultado como una cadena codificada en Base64.

```csharp
using System;
using System.Data;

// Instanciar la clase de compresión
AppCompresion.AppCompresion compresor = new AppCompresion.AppCompresion();

// Supongamos que ya tienes un DataSet llamado 'miDataSet'
DataSet miDataSet = new DataSet();
// ... (código para inicializar y llenar el DataSet)

// Ejecutar el método de compresión
string datosComprimidos = compresor.ComprimirDataset(miDataSet);
Console.WriteLine("Datos comprimidos en Base64: " + datosComprimidos);
```

### 2. Descomprimir un DataSet (Síncrono)

Recibe una cadena Base64 previamente comprimida con esta misma utilidad y la
convierte nuevamente en un objeto `DataSet`.

```csharp
// Ejecutar el método de descompresión síncrono
DataSet datasetRestaurado = compresor.DesomprimirDataset(datosComprimidos);

if (datasetRestaurado != null)
{
    Console.WriteLine("El DataSet ha sido restaurado con éxito.");
}
```

### 3. Descomprimir un DataSet (Asíncrono)

Para escenarios o aplicaciones donde la descompresión pueda tomar más tiempo
y no se desee bloquear el hilo principal (ej. aplicaciones UI o APIs), se puede
utilizar la versión asíncrona del método.

```csharp
using System.Threading.Tasks;

public async Task RestaurarDatosAsync(string datosBase64)
{
    AppCompresion.AppCompresion compresor = new AppCompresion.AppCompresion();

    // Ejecutar el método de descompresión asíncrono
    DataSet datasetRestaurado = await compresor.DesomprimirDatasetAsync(datosBase64);

    // Trabajar con el datasetRestaurado
}
```
