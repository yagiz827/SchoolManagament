# School Management API

A **layered ASP.NET Core Web API** for managing a school: teachers, students, classes and weekly schedules. It uses **role-based JWT authentication**, so teachers and students get different permissions. Data is stored in **SQL Server** with Entity Framework Core, and the schema is managed with EF migrations.

> A learning project (2022) focused on layered architecture and authorization.

## Architecture

```
WebApi            → controllers, Swagger, JWT authentication setup
   ↓
Bussiness         → services / business rules (classes, students, teachers)
   ↓
DataAccessLayer   → EF Core data access + migrations
   ↓
Entities          → domain models (Teacher, Student, Class, StudentClass, Schedule) and DTOs
```

## Features

- **Two user types, Teacher and Student**, each with registration and login
- **Password hashing** with a per-user salt; login returns a **JWT** containing the user's role
- **Role-based authorization:** endpoints are restricted with `[Authorize(Roles = "Teacher")]` or `"Teacher,Student"`
- **Students:** enrol in courses, see their classes, check pass/fail, calculate GPA and view their weekly schedule
- **Teachers:** create and delete classes, and look up students and teachers
- **Swagger UI** with JWT support for testing protected endpoints

## Main endpoints

| Area | Endpoints | Who can call them |
|---|---|---|
| Auth | Student and teacher `Register` / `Login` | Anyone |
| Classes | Get all, get by name, `AddClass`, `Delete Class` | Adding and deleting: teachers only |
| Students | `GetClasses`, `AddCourses`, `CheckifPass`, `LearnGPA`, `LearnSuchedule` | Teachers and students |
| Teachers | Get all, get by name, add and delete teachers | Varies |

## Built with

C# · .NET 6 · ASP.NET Core Web API · Entity Framework Core (with migrations) · SQL Server (LocalDB) · JWT · Swagger

## Running

1. Update the connection string in `DataAccessLayer/Concrete/Database.cs` for your SQL Server instance.
2. Create the database: `dotnet ef database update --project DataAccessLayer --startup-project WebApi`
3. Open the solution in Visual Studio, set **WebApi** as the startup project and run it. Register a user in Swagger, log in, and paste the token into **Authorize**.
