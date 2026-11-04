# Huellitas Store — reconstrucción MVC

## ¿Qué se corrigió?
- Catálogo, búsqueda y filtro por categorías.
- Carrito por sesión de cada navegador, con validación de cantidades, eliminación y actualización.
- Precios y stock se obtienen de SQL Server (no de campos ocultos controlados por el usuario).
- Pedido de **demostración**: crea `TB_VENTA` y `TB_DETALLEVENTA` en una transacción y descuenta el stock.
- Navegación y diseño responsive con Bootstrap y hoja `wwwroot/css/tienda.css`.
- Inicio directo en `/Producto/Catalogo`.

## Ejecutar
1. Guarda una copia de seguridad de tu base `VentasProdMascotasF`. **No necesitas volver a ejecutar el script SQL**.
2. Abre `ProyectoDesarrollo_VSCode` en VS Code y usa PowerShell desde la carpeta del `.csproj`.
3. Configura tu conexión en la sesión de la terminal:
   ```powershell
   $env:ConnectionStrings__VentasDB = 'Server=localhost\SQLEXPRESS;Database=VentasProdMascotasF;Trusted_Connection=True;TrustServerCertificate=True;'
   ```
4. `dotnet restore`, `dotnet build`, `dotnet run --launch-profile VSCode`.
5. Abre la URL indicada por `Now listening on`, normalmente `http://localhost:5265`.

## Prueba rápida
- Filtra o busca un producto y agrégalo.
- Comprueba que aparece en el carrito y que puedes cambiar cantidad o eliminarlo.
- Registra un pedido **de demostración**; revisa `TB_VENTA`, `TB_DETALLEVENTA` y `TB_PRODUCTO.STK_PRODUCTO` en SQL Server.
- El carrito queda vacío tras confirmar el pedido.

## Alcance y pendientes
Esta entrega no incluye pagos reales, registro de clientes reales, facturación electrónica ni validación de identidad del cliente. Los pedidos utilizan **CL01**, un cliente ficticio de pruebas. El controlador histórico `DVentaController` y algunas vistas antiguas permanecen en el proyecto, pero no forman parte del nuevo flujo. No se ha podido ejecutar el compilador .NET ni conectar a tu SQL Server desde este entorno; valida primero `dotnet build` y las pruebas anteriores en tu PC. La versión .NET 6 está fuera de soporte y debe actualizarse antes de publicar el sistema en Internet.
