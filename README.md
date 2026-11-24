# Inventario de Productos

Sistema de gestión de inventario con backend en .NET 9 y frontend en Angular 22.

---

## Requisitos

- .NET 9 SDK
- Node.js 22
- SQL Server accesible en `localhost,1433`

---

## 1. Base de datos

```bash
sqlcmd -S localhost,1433 -U sa -P "tu_password" -i database/01_esquema.sql
sqlcmd -S localhost,1433 -U sa -P "tu_password" -d ProductosBG -i database/02_datos.sql -f 65001
```

---

## 2. Backend

Crear el archivo `back-productos/back-productos/appsettings.Development.json` copiando `appsettings.Example.json` y completando los valores reales:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=ProductosBG;User Id=sa;Password=tu_password;TrustServerCertificate=True;Encrypt=False;"
  },
  "Jwt": {
    "Key": "clave-de-al-menos-32-caracteres-aqui",
    "Issuer": "back-productos",
    "Audience": "front-productos",
    "Minutos": 60
  },
  "Cors": {
    "Origenes": "http://localhost:4200"
  }
}
```

```bash
cd back-productos
dotnet run --project back-productos
```

Corre en `http://localhost:5030`  
Swagger en `http://localhost:5030/swagger`

---

## 3. Frontend

```bash
cd front-productos
npm install
npx ng serve
```

Corre en `http://localhost:4200`

---

## Usuarios de prueba

| Correo | Contraseña | Rol |
|---|---|---|
| admin@bg.com | Admin123! | Admin |
| cliente@bg.com | Cliente123! | Cliente |

El **Admin** puede crear, editar y eliminar productos y lotes.  
El **Cliente** solo puede ver los productos activos.

---

## Notas

- Eliminar un producto o lote es baja lógica, el registro queda en la base con estado `N`.
- Los productos tienen estado `A` (activo), `I` (inactivo, solo admin lo ve) y `N` (eliminado).
- Para crear un admin nuevo hay que insertarlo directo en la base de datos.
