USE ProductosBG;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Id = 1)
    INSERT INTO dbo.Roles (Id, Nombre) VALUES (1, N'Admin');
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Id = 2)
    INSERT INTO dbo.Roles (Id, Nombre) VALUES (2, N'Cliente');
GO

-- admin@bg.com / Admin123!
IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Correo = N'admin@bg.com')
    INSERT INTO dbo.Users (Nombre, Correo, [ContraseñaHash], RoleId, FechaCreacion)
    VALUES (N'Administrador', N'admin@bg.com',
            N'$2a$11$qZiwGHH4BSqPVZR/xLrR6O5m2j1IYrgHTbfUbwKZQsK5/DJr.li2O',
            1, GETUTCDATE());

-- cliente@bg.com / Cliente123!
IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Correo = N'cliente@bg.com')
    INSERT INTO dbo.Users (Nombre, Correo, [ContraseñaHash], RoleId, FechaCreacion)
    VALUES (N'Cliente Demo', N'cliente@bg.com',
            N'$2a$11$RXIi4P2jsWMSxJ/aLlD.SeuYHPyJb63GSXyeJEeRQ1bt2E6HtFTfG',
            2, GETUTCDATE());
GO

DECLARE @p1 INT, @p2 INT, @p3 INT, @p4 INT;

IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Codigo = N'PROD-001')
    INSERT INTO dbo.Productos (Codigo, Nombre, Descripcion, Estado, FechaCreacion)
    VALUES (N'PROD-001', N'Aceite de motor 5W-30', N'Aceite sintético para motores a gasolina y diesel, presentación 1 litro.', 'A', GETUTCDATE());
SELECT @p1 = Id FROM dbo.Productos WHERE Codigo = N'PROD-001';

IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Codigo = N'PROD-002')
    INSERT INTO dbo.Productos (Codigo, Nombre, Descripcion, Estado, FechaCreacion)
    VALUES (N'PROD-002', N'Filtro de aire universal', N'Filtro de aire de alto flujo, compatible con la mayoría de modelos sedan.', 'A', GETUTCDATE());
SELECT @p2 = Id FROM dbo.Productos WHERE Codigo = N'PROD-002';

IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Codigo = N'PROD-003')
    INSERT INTO dbo.Productos (Codigo, Nombre, Descripcion, Estado, FechaCreacion)
    VALUES (N'PROD-003', N'Pastillas de freno delanteras', N'Juego de pastillas de freno cerámicas para eje delantero.', 'A', GETUTCDATE());
SELECT @p3 = Id FROM dbo.Productos WHERE Codigo = N'PROD-003';

IF NOT EXISTS (SELECT 1 FROM dbo.Productos WHERE Codigo = N'PROD-004')
    INSERT INTO dbo.Productos (Codigo, Nombre, Descripcion, Estado, FechaCreacion)
    VALUES (N'PROD-004', N'Batería 12V 60Ah', N'Batería de arranque sellada libre de mantenimiento, 12 voltios 60 amperios.', 'A', GETUTCDATE());
SELECT @p4 = Id FROM dbo.Productos WHERE Codigo = N'PROD-004';

IF @p1 IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.LotesProductos WHERE ProductoId = @p1 AND NumeroLote = N'L-2024-01')
    INSERT INTO dbo.LotesProductos (ProductoId, NumeroLote, Precio, Stock, Estado, FechaIngreso)
    VALUES (@p1, N'L-2024-01', 18.50, 120, 'A', '2024-01-15');

IF @p1 IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.LotesProductos WHERE ProductoId = @p1 AND NumeroLote = N'L-2024-07')
    INSERT INTO dbo.LotesProductos (ProductoId, NumeroLote, Precio, Stock, Estado, FechaIngreso)
    VALUES (@p1, N'L-2024-07', 19.00, 80, 'A', '2024-07-10');

IF @p1 IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.LotesProductos WHERE ProductoId = @p1 AND NumeroLote = N'L-2025-01')
    INSERT INTO dbo.LotesProductos (ProductoId, NumeroLote, Precio, Stock, Estado, FechaIngreso)
    VALUES (@p1, N'L-2025-01', 20.50, 200, 'A', '2025-01-03');

IF @p2 IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.LotesProductos WHERE ProductoId = @p2 AND NumeroLote = N'L-2024-03')
    INSERT INTO dbo.LotesProductos (ProductoId, NumeroLote, Precio, Stock, Estado, FechaIngreso)
    VALUES (@p2, N'L-2024-03', 12.00, 60, 'A', '2024-03-20');

IF @p2 IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.LotesProductos WHERE ProductoId = @p2 AND NumeroLote = N'L-2025-02')
    INSERT INTO dbo.LotesProductos (ProductoId, NumeroLote, Precio, Stock, Estado, FechaIngreso)
    VALUES (@p2, N'L-2025-02', 13.50, 95, 'A', '2025-02-11');

IF @p3 IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.LotesProductos WHERE ProductoId = @p3 AND NumeroLote = N'L-2024-11')
    INSERT INTO dbo.LotesProductos (ProductoId, NumeroLote, Precio, Stock, Estado, FechaIngreso)
    VALUES (@p3, N'L-2024-11', 35.00, 40, 'A', '2024-11-05');

IF @p4 IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.LotesProductos WHERE ProductoId = @p4 AND NumeroLote = N'L-2024-06')
    INSERT INTO dbo.LotesProductos (ProductoId, NumeroLote, Precio, Stock, Estado, FechaIngreso)
    VALUES (@p4, N'L-2024-06', 85.00, 25, 'A', '2024-06-18');

IF @p4 IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.LotesProductos WHERE ProductoId = @p4 AND NumeroLote = N'L-2025-03')
    INSERT INTO dbo.LotesProductos (ProductoId, NumeroLote, Precio, Stock, Estado, FechaIngreso)
    VALUES (@p4, N'L-2025-03', 92.00, 30, 'A', '2025-03-02');
GO
