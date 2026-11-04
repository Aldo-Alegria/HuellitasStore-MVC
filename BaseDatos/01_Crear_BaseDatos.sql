/* Reconstrucción a partir de Models/VentasC.cs, no recuperación de los registros de 2023.
   Ejecutar en SQL Server Management Studio o Azure Data Studio sobre una instancia local.
   No elimina tablas existentes. Los datos son ilustrativos y editables. */
IF DB_ID(N'VentasProdMascotasF') IS NULL
    EXEC(N'CREATE DATABASE [VentasProdMascotasF]');
GO
USE [VentasProdMascotasF];
GO
IF OBJECT_ID(N'dbo.TB_CATEGORIA',N'U') IS NULL
CREATE TABLE dbo.TB_CATEGORIA (
    COD_CATEGORIA char(5) NOT NULL CONSTRAINT PK_TB_CATEGORIA PRIMARY KEY,
    NOM_CAT varchar(50) NOT NULL
);
GO
IF OBJECT_ID(N'dbo.TB_CLIENTE',N'U') IS NULL
CREATE TABLE dbo.TB_CLIENTE (
    COD_CLIENTE char(5) NOT NULL CONSTRAINT PK_TB_CLIENTE PRIMARY KEY,
    NOM_CLI varchar(50) NOT NULL,
    APE_CLI varchar(50) NOT NULL,
    DIR_CLI varchar(100) NOT NULL,
    DNI char(8) NULL
);
GO
IF OBJECT_ID(N'dbo.TB_USUARIO',N'U') IS NULL
CREATE TABLE dbo.TB_USUARIO (
    Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_TB_USUARIO PRIMARY KEY,
    UsuNombre varchar(8) NOT NULL,
    UsuClave varchar(8) NOT NULL
);
GO
IF OBJECT_ID(N'dbo.TB_PRODUCTO',N'U') IS NULL
CREATE TABLE dbo.TB_PRODUCTO (
    COD_PRODUCTO char(5) NOT NULL CONSTRAINT PK_TB_PRODUCTO PRIMARY KEY,
    NOM_PRODUCTO varchar(100) NOT NULL,
    PRE_PRODUCTO money NOT NULL,
    STK_PRODUCTO int NOT NULL,
    ESTA_PRODUCTO varchar(20) NOT NULL,
    COD_CATEGORIA char(5) NOT NULL,
    IMAGENES varchar(100) NULL,
    CONSTRAINT FK_TB_PRODUCTO_CATEGORIA FOREIGN KEY (COD_CATEGORIA) REFERENCES dbo.TB_CATEGORIA (COD_CATEGORIA)
);
GO
IF OBJECT_ID(N'dbo.TB_VENTA',N'U') IS NULL
CREATE TABLE dbo.TB_VENTA (
    COD_VENTA char(5) NOT NULL CONSTRAINT PK_TB_VENTA PRIMARY KEY,
    COD_CLIENTE char(5) NOT NULL,
    FEC_VENTA date NOT NULL,
    TOTAL_VENTA decimal(10,2) NOT NULL,
    ESTADO_VENTA varchar(20) NOT NULL,
    CONSTRAINT FK_TB_VENTA_CLIENTE FOREIGN KEY (COD_CLIENTE) REFERENCES dbo.TB_CLIENTE (COD_CLIENTE)
);
GO
IF OBJECT_ID(N'dbo.TB_DETALLEVENTA',N'U') IS NULL
CREATE TABLE dbo.TB_DETALLEVENTA (
    COD_VENTA char(5) NOT NULL,
    COD_PRODUCTO char(5) NOT NULL,
    Cantidad int NOT NULL,
    Precio decimal(10,2) NOT NULL,
    CONSTRAINT PK_TB_DETALLEVENTA PRIMARY KEY (COD_VENTA,COD_PRODUCTO),
    CONSTRAINT FK_TB_DETALLEVENTA_VENTA FOREIGN KEY (COD_VENTA) REFERENCES dbo.TB_VENTA (COD_VENTA),
    CONSTRAINT FK_TB_DETALLEVENTA_PRODUCTO FOREIGN KEY (COD_PRODUCTO) REFERENCES dbo.TB_PRODUCTO (COD_PRODUCTO)
);
GO
-- Categorías ilustrativas correspondientes a las fotografías en wwwroot/imagen.
IF NOT EXISTS (SELECT 1 FROM dbo.TB_CATEGORIA WHERE COD_CATEGORIA='CA01')
 INSERT dbo.TB_CATEGORIA (COD_CATEGORIA,NOM_CAT) VALUES
 ('CA01','Alimentos'),('CA02','Accesorios'),('CA03','Higiene'),('CA04','Descanso'),('CA05','Juguetes');
GO
-- Ejemplos de catálogo; precios y cantidades no corresponden a datos históricos.
IF NOT EXISTS (SELECT 1 FROM dbo.TB_PRODUCTO WHERE COD_PRODUCTO='PR01')
 INSERT dbo.TB_PRODUCTO (COD_PRODUCTO,NOM_PRODUCTO,PRE_PRODUCTO,STK_PRODUCTO,ESTA_PRODUCTO,COD_CATEGORIA,IMAGENES) VALUES
 ('PR01','Croquetas para perro',69.90,30,'Activo','CA01','CroquetasP.jpg'),
 ('PR02','Comedero para gato',25.00,24,'Activo','CA02','ComederoG.jpg'),
 ('PR03','Rascador para gato',119.90,12,'Activo','CA02','RascadorG.jpg'),
 ('PR04','Collar para gato',15.90,40,'Activo','CA02','CollarG.jpg'),
 ('PR05','Cepillo para gato',19.90,25,'Activo','CA03','CepilloG.jpg'),
 ('PR06','Cama para perro',89.90,14,'Activo','CA04','CamaP.jpg'),
 ('PR07','Correa para perro',24.90,35,'Activo','CA02','CorreaP.jpg'),
 ('PR08','Juguete para mascota',16.90,50,'Activo','CA05','JugueteMP.jpg'),
 ('PR09','Arena para gato',29.90,20,'Activo','CA03','ArenaG.jpg'),
 ('PR10','Pelota para perro',12.90,48,'Activo','CA05','JugueteP.jpg');
GO
-- Referencia CL01 requerida por DVentaController.TransferirDetalles.
IF NOT EXISTS (SELECT 1 FROM dbo.TB_CLIENTE WHERE COD_CLIENTE='CL01')
 INSERT dbo.TB_CLIENTE (COD_CLIENTE,NOM_CLI,APE_CLI,DIR_CLI,DNI)
 VALUES ('CL01','Cliente','Demostracion','Direccion de prueba',NULL);
GO
-- Usuario SOLO para prueba local; el proyecto de 2023 compara contraseñas en texto plano.
-- Cambiar/eliminar inmediatamente al terminar las pruebas y NO usar en producción.
IF NOT EXISTS (SELECT 1 FROM dbo.TB_USUARIO WHERE UsuNombre='demo')
 INSERT dbo.TB_USUARIO (UsuNombre,UsuClave) VALUES ('demo','demo123');
GO
SELECT 'TB_CATEGORIA' AS Tabla, COUNT(*) AS Registros FROM dbo.TB_CATEGORIA
UNION ALL SELECT 'TB_PRODUCTO', COUNT(*) FROM dbo.TB_PRODUCTO
UNION ALL SELECT 'TB_CLIENTE', COUNT(*) FROM dbo.TB_CLIENTE
UNION ALL SELECT 'TB_USUARIO', COUNT(*) FROM dbo.TB_USUARIO
UNION ALL SELECT 'TB_VENTA', COUNT(*) FROM dbo.TB_VENTA
UNION ALL SELECT 'TB_DETALLEVENTA', COUNT(*) FROM dbo.TB_DETALLEVENTA;
GO
