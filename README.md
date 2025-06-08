# Employee Admin Portal

A .NET 8 web app with MVC frontend and API backend for employee management.

### Features
- **RESTful API** built with ASP.NET Core
- **MVC Web Application** for admin interface
- **Entity Framework Core** for data access
- **SQL Server** database integration
- **Swagger/OpenAPI** documentation
- **Dual-project architecture** (API + MVC)
- **HTTP Client integration** between MVC and API
- **Configuration management** with appsettings.json

## Technologies

- .NET 8.0
- ASP.NET Core Web API
- ASP.NET Core MVC
- Entity Framework Core 9.0
- SQL Server
- Swashbuckle (Swagger)
- HttpClient for service communication
- 
### Setup
1. Update connection string in `appsettings.json`  
2. Run both projects:  
   ```bash
   dotnet run --project EmployeeAdminPortal
   dotnet run --project EmployeeAdminPortal.mvc

### Access
- API Docs: https://localhost:7144/swagger
- MVC App: https://localhost:[PORT]/Employees

- Replace `[PORT]` with your MVC app’s actual port
  
