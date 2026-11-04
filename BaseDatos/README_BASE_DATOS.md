# Base de datos reconstruida para pruebas locales

El archivo `01_Crear_BaseDatos.sql` recrea el esquema SQL Server deducido de `Models/VentasC.cs` del proyecto de 2023. **No recupera registros históricos**. Incluye categorías, productos asociados a las imágenes que sí se conservan, un cliente ficticio `CL01` (requerido por `DVentaController`) y un usuario de prueba.

## 1. Instalación

Instala una instancia de SQL Server (por ejemplo SQL Server Express) y una interfaz para consultas SQL (SQL Server Management Studio o extensión MSSQL de VS Code). Conéctate al servidor, abre `BaseDatos/01_Crear_BaseDatos.sql` y ejecuta **todo el script**. Verifica que aparezca `VentasProdMascotasF` y las seis tablas.

## 2. Conexión con la web

En una terminal **PowerShell** de VS Code, sustituye el nombre de instancia según corresponda:

```powershell
$env:ConnectionStrings__VentasDB = 'Server=localhost\SQLEXPRESS;Database=VentasProdMascotasF;Trusted_Connection=True;TrustServerCertificate=True;'
dotnet restore
dotnet build
dotnet run --launch-profile VSCode
```

Si tu servidor SQL Server se instaló como instancia predeterminada, usa `Server=localhost;`. La autenticación integrada funciona en Windows con permisos adecuados; si no, hay que configurar un usuario SQL específico. No publiques contraseñas en archivos compartidos.

## 3. Datos de demostración

- Usuario: `demo` / Contraseña: `demo123` (solo local y temporal).
- Cliente: `CL01` (ficticio).
- Diez productos con imágenes existentes en `wwwroot/imagen`, precios ilustrativos en soles y stock de prueba.
- Ventas: sin registros iniciales. El proyecto genera códigos a partir del último detalle de venta.

## 4. Limitaciones ya identificadas en el código heredado

1. El sistema almacena/compara contraseñas en texto plano y limita a ocho caracteres. **No apto para producción**. Debe migrarse a hashing fuerte antes de usar cuentas reales.
2. `DVentaController` guarda PDF en una ruta absoluta de otro equipo (`C:\Users\dell\Desktop\PDF_DBP\`), lo cual puede fallar en tu computadora. Se recomienda utilizar `Path.Combine` y una carpeta configurable en la máquina actual.
3. La generación del siguiente código de venta consulta el último **detalle**, no la última **venta**. Si falla el detalle puede reutilizar código; conviene corregirlo y registrar venta + detalle en una transacción.
4. La app apunta a .NET 6, fuera de soporte; se debe planificar migración a una versión soportada de .NET.
5. Se recuperó la estructura, **no las filas originales**; no se garantiza funcionamiento integral hasta compilar en Windows y probar la conexión.

La secuencia de inserciones asume una base vacía. Si ya tienes tablas/datos parciales, haz respaldo y revisa el script antes de aplicarlo.
