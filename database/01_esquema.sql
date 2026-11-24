IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = N'ProductosBG')
    CREATE DATABASE ProductosBG;
GO

USE ProductosBG;
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Roles' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Roles (
        Id     INT          NOT NULL CONSTRAINT PK_Roles PRIMARY KEY,
        Nombre NVARCHAR(50) NOT NULL CONSTRAINT UQ_Roles_Nombre UNIQUE
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Users' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Users (
        Id               INT           NOT NULL CONSTRAINT PK_Users PRIMARY KEY IDENTITY(1,1),
        Nombre           NVARCHAR(100) NOT NULL,
        Correo           NVARCHAR(150) NOT NULL CONSTRAINT UQ_Users_Correo UNIQUE,
        [ContraseñaHash] NVARCHAR(256) NOT NULL,
        RoleId           INT           NOT NULL CONSTRAINT FK_Users_Roles FOREIGN KEY REFERENCES dbo.Roles(Id),
        FechaCreacion    DATETIME2     NOT NULL CONSTRAINT DF_Users_FechaCreacion DEFAULT GETUTCDATE()
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'Productos' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Productos (
        Id                 INT           NOT NULL CONSTRAINT PK_Productos PRIMARY KEY IDENTITY(1,1),
        Codigo             NVARCHAR(50)  NOT NULL CONSTRAINT UQ_Productos_Codigo UNIQUE,
        Nombre             NVARCHAR(150) NOT NULL,
        Descripcion        NVARCHAR(500) NOT NULL CONSTRAINT DF_Productos_Descripcion DEFAULT '',
        Estado             BIT           NOT NULL CONSTRAINT DF_Productos_Estado DEFAULT 1,
        FechaCreacion      DATETIME2     NOT NULL CONSTRAINT DF_Productos_FechaCreacion DEFAULT GETUTCDATE(),
        FechaActualizacion DATETIME2     NULL
    );

    CREATE INDEX IX_Productos_Nombre ON dbo.Productos (Nombre);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = N'LotesProductos' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.LotesProductos (
        Id                 INT           NOT NULL CONSTRAINT PK_LotesProductos PRIMARY KEY IDENTITY(1,1),
        ProductoId         INT           NOT NULL CONSTRAINT FK_LotesProductos_Productos FOREIGN KEY REFERENCES dbo.Productos(Id),
        NumeroLote         NVARCHAR(50)  NOT NULL,
        Precio             DECIMAL(18,2) NOT NULL,
        Stock              INT           NOT NULL CONSTRAINT DF_LotesProductos_Stock DEFAULT 0,
        Estado             BIT           NOT NULL CONSTRAINT DF_LotesProductos_Estado DEFAULT 1,
        FechaIngreso       DATETIME2     NOT NULL CONSTRAINT DF_LotesProductos_FechaIngreso DEFAULT GETUTCDATE(),
        FechaActualizacion DATETIME2     NULL,
        CONSTRAINT UQ_LotesProductos_Lote UNIQUE (ProductoId, NumeroLote)
    );

    CREATE INDEX IX_LotesProductos_ProductoId ON dbo.LotesProductos (ProductoId);
END
GO
