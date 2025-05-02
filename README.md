## 🏗️ Project Architecture

This project follows a **Layered Architecture** approach, separating responsibilities across multiple layers to ensure clean organization, maintainability, and testability.

### ✅ Layer Overview

| Layer                          | Project                     | Responsibilities                                                                                   |
| ------------------------------ | --------------------------- | -------------------------------------------------------------------------------------------------- |
| **Presentation Layer**         | `ExaminationSystem.API`     | Exposes HTTP endpoints, handles requests/responses, delegates to the service layer.                |
| **Business Logic Layer (BLL)** | `ExaminationSystem.Service` | Contains application-specific business logic, coordinates repositories and validation.             |
| **Data Access Layer (DAL)**    | `ExaminationSystem.Data`    | Manages interaction with the database using Entity Framework Core and implements repository logic. |
| **Domain/Core Layer**          | `ExaminationSystem.Core`    | Defines entities, DTOs, and interfaces for services and repositories. Shared across all layers.    |

---

### 🔁 Dependency Flow

Each layer depends **only on the layer directly beneath it**, ensuring a clean separation of concerns:

```
API → Service → Core
            ↑
        Data → Core
```

* The `API` project depends on `Service` and `Core`.
* The `Service` project depends on `Core`.
* The `Data` project implements interfaces defined in `Core`, such as `IStudentRepository`.

---

### 🧩 Key Design Principles

* **Separation of Concerns:** Each layer has a focused responsibility.
* **Dependency Injection:** Services and repositories are injected via interfaces.
* **Inversion of Control:** The `Service` layer works with abstractions (`IRepository`) rather than concrete EF classes.
* **Testability:** The use of interfaces allows for easy unit testing of services and repositories.

## 🛠 EF Core & Setup Terminal Commands

### 🔧 Project Setup (One-Time)

```bash
# Create solution and projects
dotnet new sln -n ExaminationSystem

dotnet new webapi -n ExaminationSystem.API
dotnet new classlib -n ExaminationSystem.Core
dotnet new classlib -n ExaminationSystem.Data
dotnet new classlib -n ExaminationSystem.Service

# Add projects to solution
dotnet sln add ExaminationSystem.API
dotnet sln add ExaminationSystem.Core
dotnet sln add ExaminationSystem.Data
dotnet sln add ExaminationSystem.Service

# Add project references
dotnet add ExaminationSystem.API reference ExaminationSystem.Service
dotnet add ExaminationSystem.Service reference ExaminationSystem.Data
dotnet add ExaminationSystem.Service reference ExaminationSystem.Core
dotnet add ExaminationSystem.Data reference ExaminationSystem.Core
```

### 📦 EF Core Packages (Installed in ExaminationSystem.Data)
``` bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Proxies
```

### 🎯 EF Core Design Package (Installed in ExaminationSystem.API)
``` bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### 🛠 Global EF Tools
``` bash
dotnet tool install --global dotnet-ef
```
### 📤 Migrations
#### Create migration
``` bash
dotnet ef migrations add InitialCreate --project ExaminationSystem.Data --startup-project ExaminationSystem.API
```
#### Apply migration
``` bash
dotnet ef database update --project ExaminationSystem.Data --startup-project ExaminationSystem.API
```