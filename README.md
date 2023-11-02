
# Migraciones de Base de Datos

Este README describe cómo gestionar y aplicar migraciones de base de datos para nuestra aplicación.

## Crear una nueva migración:

Para crear una nueva migración, usa el siguiente comando:

```
dotnet ef migrations add [NombreDeLaMigracion]
```

Por ejemplo, para crear una migración llamada "Inicial", ejecuta:

```
dotnet ef migrations add Inicial
```

## Aplicar migraciones:

Para aplicar todas las migraciones pendientes a tu base de datos, usa el siguiente comando:

```
dotnet ef database update
```

## Revertir migraciones:

Si necesitas revertir una migración, puedes hacerlo usando el siguiente comando:

```
dotnet ef database update [NombreDeLaMigracionAnterior]
```

Por ejemplo, para revertir a la migración anterior a "Inicial", ejecuta:

```
dotnet ef database update NombreDeLaMigracionAnterior
```

Nota: Reemplaza "NombreDeLaMigracionAnterior" con el nombre de la migración real a la que deseas revertir.

# Ejecutar la aplicación en .NET 7.0

## Prerrequisitos:

- Asegúrate de tener instalado .NET SDK 7.0 en tu máquina.

## Pasos:

1. Navega al directorio raíz del proyecto usando la terminal o línea de comandos.

2. Ejecuta el siguiente comando para restaurar los paquetes:

```
dotnet restore
```

3. Luego, para compilar y ejecutar la aplicación, usa:

```
dotnet run
```

La aplicación debería iniciar y escuchar en un puerto específico (por lo general 5000 para HTTP y 5001 para HTTPS).

Abre tu navegador y navega a `http://localhost:5000` para acceder a la aplicación.

# Importar JSON a Postman:

Para importar una colección o una solicitud en formato JSON a Postman:

1. Abre Postman.
2. Haz clic en el botón "Import" en la esquina superior izquierda.
3. Se abrirá una ventana emergente. Aquí, puedes arrastrar y soltar tu archivo JSON o hacer clic en "Upload Files" para buscar y seleccionar tu archivo.
4. Una vez que hayas seleccionado tu archivo JSON, haz clic en el botón "Import".
5. Postman importará el archivo y deberías ver tu colección o solicitud en la interfaz de Postman listo para ser utilizado.
