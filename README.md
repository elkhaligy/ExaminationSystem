Essential Terminal Commands
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