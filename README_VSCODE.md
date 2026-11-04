# ProyectoDesarrollo.B.P — preparado para Visual Studio Code

Este es el proyecto ASP.NET Core MVC **original, en C#**, preparado para abrirse desde VS Code. Se tomó la **versión interna más completa** del ZIP original y se eliminaron los archivos temporales de Visual Studio (`bin`, `obj`, `.vs`) y el historial `.git` para reducir tamaño. Se conservaron controladores, modelos, servicios, vistas y recursos de wwwroot.

## Requisitos en Windows

1. **Visual Studio Code** con la extensión **C# Dev Kit** de Microsoft (y extensión C# si se solicita).
2. **SDK .NET 6.0** (comprobar con `dotnet --list-sdks`). El proyecto original apunta a `net6.0`. **.NET 6 ya no tiene soporte**; la migración a .NET 8/10 requiere pruebas aparte.
3. **SQL Server**: puedes crear una base nueva `VentasProdMascotasF` ejecutando `BaseDatos/01_Crear_BaseDatos.sql`. Lee `BaseDatos/README_BASE_DATOS.md` antes de empezar. Los registros de 2023 no se pueden recuperar desde el código fuente.

## Abrir y ejecutar

1. Extrae el ZIP y abre la carpeta `ProyectoDesarrollo_VSCode` en VS Code (Archivo > Abrir carpeta). NO abras solamente el `.sln`.
2. Abre la terminal integrada: Terminal > Nuevo terminal.
3. Configura la conexión SQL Server para **esta terminal**. Si usas SQL Server Express en el mismo PC con autenticación Windows (PowerShell):

   ```powershell
   $env:ConnectionStrings__VentasDB = 'Server=localhost\SQLEXPRESS;Database=VentasProdMascotasF;Trusted_Connection=True;TrustServerCertificate=True;'
   ```

   Si tu instancia es distinta, cambia `localhost\SQLEXPRESS`. Otra opción es:

   ```powershell
   $env:ConnectionStrings__VentasDB = 'Server=localhost;Database=VentasProdMascotasF;Trusted_Connection=True;TrustServerCertificate=True;'
   ```

4. En la **misma terminal**, ejecuta:

   ```powershell
   dotnet restore
   dotnet build
   dotnet run --launch-profile VSCode
   ```

5. Abre `http://localhost:5265` en el navegador. Para detener el servidor, presiona Ctrl+C.

## Ejecutar con F5

La carpeta `.vscode` trae `tasks.json` (restore, build, run) y `launch.json` (depuración C#). **La variable de conexión debe llegar al proceso de VS Code** para usar F5: configura la variable como variable de entorno de Windows y **reinicia VS Code**, o lanza VS Code desde la misma terminal PowerShell donde ejecutaste la asignación de la variable mediante `code .`.

## Cambios realizados

- Se seleccionó la versión interna con controladores adicionales de ventas y usuarios.
- Se añadió el perfil de lanzamiento `VSCode` para HTTP local sin certificado HTTPS.
- Se crearon configuraciones `.vscode` para compilar, ejecutar y depurar.
- En `Models/VentasC.cs`, el servidor SQL Server fijo de otra computadora se sustituyó por la variable de entorno `ConnectionStrings__VentasDB`.
- No se modificaron los flujos de venta o sus vistas.

## Si falla

- `dotnet no se reconoce`: instala .NET SDK y vuelve a abrir la terminal.
- `NETSDK...`: verifica que esté instalado el SDK compatible y `dotnet --list-sdks`.
- `Login failed` o `error 26`: configura SQL Server y verifica nombre de instancia, existencia de la base de datos y permisos.
- `Invalid object name`: te falta restaurar las tablas originales.
- `Falta configurar ConnectionStrings__VentasDB`: define esa variable en el proceso que ejecuta la aplicación.
- El código depende de paquetes NuGet externos y de SQL Server; **no se ha podido comprobar aquí la compilación ni el funcionamiento de extremo a extremo**.

## Nota

VS Code es el editor; **no es necesario reescribir el proyecto en HTML/JavaScript o crear uno nuevo**. El ejecutable lo proporciona el SDK .NET. Las dependencias permanecen en el archivo `.csproj` original.

## Reconstrucción de base de datos (2026)

Incluye `BaseDatos/01_Crear_BaseDatos.sql` con seis tablas derivadas de `Models/VentasC.cs` y datos ficticios para pruebas. Instrucciones en `BaseDatos/README_BASE_DATOS.md`.
