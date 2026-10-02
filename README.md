# Task Manager API

A well-structured **RESTful API** for managing personal tasks, built with **ASP.NET Core**, **Entity Framework Core**, and **SQL Server**. Features JWT-based authentication, full CRUD operations, and interactive Swagger documentation.

---

## Skills Demonstrated

> This project was built to showcase hands-on experience with a modern .NET backend stack.

`ASP.NET Core Web API` • `C#` • `Entity Framework Core` • `SQL Server` • `JWT Authentication` • `REST APIs` • `Repository Pattern` • `Service Layer` • `BCrypt` • `Swagger / OpenAPI`

---

## Features

| Feature | Details |
|---|---|
| **Authentication** | Register and Login with JWT Bearer tokens |
| **Task CRUD** | Create, Read, Update, Delete tasks |
| **Database** | SQL Server via EF Core with Code-First Migrations |
| **Architecture** | Repository / Service pattern with DTOs |
| **Validation** | Data annotations on all DTOs |
| **Swagger** | Interactive API docs with JWT support |
| **Security** | BCrypt password hashing, scoped data access |

---

## Project Structure

```
TaskManagerAPI/
├── Controllers/
│   ├── AuthController.cs       # POST /api/auth/register, /api/auth/login
│   └── TasksController.cs      # CRUD /api/tasks
├── Data/
│   └── AppDbContext.cs         # EF Core DbContext
├── DTOs/
│   ├── Auth/
│   │   ├── RegisterDto.cs
│   │   ├── LoginDto.cs
│   │   └── AuthResponseDto.cs
│   └── Tasks/
│       ├── CreateTaskDto.cs
│       ├── UpdateTaskDto.cs
│       └── TaskResponseDto.cs
├── Migrations/                 # EF Core migrations (auto-applied on startup)
├── Models/
│   ├── User.cs
│   └── TaskItem.cs             # Includes TaskStatus and TaskPriority enums
├── Repositories/
│   ├── Interfaces/
│   │   ├── IUserRepository.cs
│   │   └── ITaskRepository.cs
│   ├── UserRepository.cs
│   └── TaskRepository.cs
├── Services/
│   ├── Interfaces/
│   │   ├── IAuthService.cs
│   │   └── ITaskService.cs
│   ├── AuthService.cs
│   └── TaskService.cs
├── appsettings.json
└── Program.cs
```

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB, Express, or full)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/)

---

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/your-username/TaskManagerAPI.git
cd TaskManagerAPI
```

### 2. Configure appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TaskManagerDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!@#$",
    "Issuer": "TaskManagerAPI",
    "Audience": "TaskManagerAPIClient",
    "ExpiresInDays": "7"
  }
}
```

> **Security Note:** The `appsettings.json` in this repo uses placeholder values only — no real secrets are committed. Copy `appsettings.Example.json`, rename it to `appsettings.json`, and fill in your own values locally.

### 3. Run the Application

Press **F5** in Visual Studio. The API **auto-migrates** the database on startup.

Swagger UI opens at: **http://localhost:5000**

---

## API Endpoints

### Auth Endpoints

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `POST` | `/api/auth/register` | Register a new user | No |
| `POST` | `/api/auth/login` | Login and get JWT token | No |

### Task Endpoints

| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| `GET` | `/api/tasks` | Get all tasks for current user | Yes |
| `GET` | `/api/tasks/{id}` | Get a specific task | Yes |
| `POST` | `/api/tasks` | Create a new task | Yes |
| `PUT` | `/api/tasks/{id}` | Update an existing task | Yes |
| `DELETE` | `/api/tasks/{id}` | Delete a task | Yes |

---

## Request / Response Examples

### Register

```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "ahmed",
  "email": "ahmed@example.com",
  "password": "SecurePass123"
}
```

Response `201 Created`:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "ahmed",
  "email": "ahmed@example.com",
  "expiresAt": "2026-10-07T14:20:00Z"
}
```

### Create Task

```http
POST /api/tasks
Authorization: Bearer <your_jwt_token>
Content-Type: application/json

{
  "title": "Complete project documentation",
  "description": "Write README and API docs",
  "priority": 2,
  "dueDate": "2026-10-15T00:00:00Z"
}
```

Response `201 Created`:
```json
{
  "id": 1,
  "title": "Complete project documentation",
  "description": "Write README and API docs",
  "status": "Pending",
  "priority": "High",
  "dueDate": "2026-10-15T00:00:00Z",
  "createdAt": "2026-09-30T14:20:00Z",
  "updatedAt": "2026-09-30T14:20:00Z",
  "userId": 1
}
```

---

## Enums

### TaskStatus
| Value | Name | Description |
|-------|------|-------------|
| `0` | `Pending` | Task not started |
| `1` | `InProgress` | Task in progress |
| `2` | `Completed` | Task finished |

### TaskPriority
| Value | Name | Description |
|-------|------|-------------|
| `0` | `Low` | Low priority |
| `1` | `Medium` | Medium priority (default) |
| `2` | `High` | High priority |

---

## Using Swagger with JWT

1. Run the API and open **http://localhost:5000**
2. Call `POST /api/auth/register` or `POST /api/auth/login` to get a token
3. Click the **Authorize** button at the top of Swagger UI
4. Enter: `Bearer <your_token_here>`
5. Click **Authorize** then **Close**
6. All protected endpoints now work automatically

---

## Architecture

```
Client
  |
  v
Controller  (HTTP routing, model validation)
  |
  v
Service     (business logic, DTO mapping)
  |
  v
Repository  (data access abstraction)
  |
  v
EF Core     (ORM)
  |
  v
SQL Server  (database)
```

---

## Tech Stack

| Technology | Version | Purpose |
|---|---|---|
| ASP.NET Core | 10.0 | Web framework |
| Entity Framework Core | 9.0 | ORM |
| SQL Server | Any | Database |
| JWT Bearer | 9.0 | Authentication |
| BCrypt.Net | 4.0 | Password hashing |
| Swashbuckle | 7.3 | Swagger / OpenAPI |

---

## License

This project is licensed under the **MIT License**.
