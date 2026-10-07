# services-api

API en .NET 10 para el módulo Restaurants, organizada en Domain, Application, Infrastructure y API. PostgreSQL se conecta mediante EF Core y Npgsql; MongoDB queda fuera de esta primera etapa.

## Ejecutar

```bash
docker compose up -d
dotnet run --project src/Services.Api
```

La conexión local está en `src/Services.Api/appsettings.json`. Swagger/OpenAPI está disponible en desarrollo en `/openapi/v1.json`.

## Migraciones

Con `dotnet-ef` instalado (`dotnet tool install --global dotnet-ef --version 10.0.0`):

```bash
dotnet ef migrations add InitialRestaurants --project src/Services.Infrastructure --startup-project src/Services.Api --output-dir Persistence/PostgreSQL/Migrations
dotnet ef database update --project src/Services.Infrastructure --startup-project src/Services.Api
```

## Endpoints

- `POST /api/v1/restaurants`
- `GET /api/v1/restaurants`
- `GET /api/v1/restaurants/{id}`

El POST requiere `Name` (1–200 caracteres no blancos); `Description` es opcional (máximo 2000).
