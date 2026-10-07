# CRUDSolution

An ASP.NET Core MVC application for managing people and countries, built as a layered solution with Entity Framework Core and SQL Server.

## About

CRUDSolution demonstrates a clean, testable CRUD architecture:

- Create, read, update and delete persons, with validation
- Search, sort and filter the persons grid
- Export persons to PDF, CSV and Excel
- Import countries from an Excel file
- Async services behind interfaces, covered by unit tests
- EF Core migrations with seed data loaded from `countries.json` and `persons.json`

## Projects

| Project | Purpose |
|---|---|
| `CRUDExample` | ASP.NET Core MVC web app (controllers, views) |
| `ServiceContracts` | Service interfaces and DTOs |
| `Services` | Business logic implementations |
| `Entities` | EF Core `DbContext`, entities and migrations |
| `CRUDTests` | xUnit tests |

## Getting started

Requirements: .NET 10 SDK and SQL Server LocalDB.

```bash
# apply migrations (creates PersonsDatabase on LocalDB)
dotnet ef database update --project Entities --startup-project CRUDExample

# run the app
dotnet run --project CRUDExample

# run tests
dotnet test
```

The connection string is in `CRUDExample/appsettings.json` (`DefaultConnection`).

## Adding a migration

```bash
dotnet ef migrations add <Name> --project Entities --startup-project CRUDExample
dotnet ef database update --project Entities --startup-project CRUDExample
```
