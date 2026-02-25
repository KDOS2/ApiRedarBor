Esta solución implementa una API basada en .NET + Clean Architecture + CQRS + MediatR, tambien se combina Repository y singleton, incluye autenticación OAuth2, mediante IdentityServer (Duende) y pruebas unitarias.

Se ha creado un proyecto IdentityServer encargado de la autenticación OAuth2.
Dentro de la API, todos los controladores requieren autorización, excepto el endpoint:

    POST /api/redarbor

Este endpoint se mantiene abierto para operaciones iniciales (Creacion de usuario), todos los demás endpoints requieren un token Bearer válido:

    Authorization: Bearer {access_token}

Se desarrolló un proyecto de pruebas unitarias que actualmente cubre tres clases:

    CreateEmployeeHandlerTests
    GetEmployeesByIdHandlerTests
    EmailTests

La base de datos puede inicializarse de dos formas:

 Opción 1 — Scripts SQL, ambos scripts se encuentran en: Infrastructure/Scripts: 
    
    Ejecutar el script DB_User_Create.sql
    Ejecutar el script DB_Tables.sql

Opción 2 — Script + Migraciones EF Core: Ejecutar DB_User_Create.sql
  Aplicar migraciones de Entity Framework Core: dotnet ef database update
