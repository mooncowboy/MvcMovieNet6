# MvcMovieNet6 Repository - Copilot Instructions

## Repository Overview

This repository contains a .NET 6 multi-project solution that demonstrates various .NET technologies and intentionally includes upgrade challenges. The codebase showcases ASP.NET Core MVC, Razor Pages, WPF desktop applications, and Azure Functions.

**Repository Size**: ~3MB, 7 projects, primarily C# and web technologies  
**Target Framework**: .NET 6.0 (with plans for future upgrade scenarios)  
**Languages**: C#, HTML, CSS, JavaScript  
**Database**: Entity Framework Core with SQL Server and SQLite support

## Solution Structure

```
MvcMovieNet6.sln (7 projects)
├── MvcMovie/                    # ASP.NET Core MVC web app (Primary)
├── MvcMovie.Tests/              # NUnit tests for MVC app
├── RazorMovie/                  # ASP.NET Core Razor Pages app
├── RazorMovie.Tests/            # MSTest tests for Razor app  
├── WpfMovie/                    # WPF desktop app (Windows-only)
├── WpfMovie.Tests/              # NUnit tests for WPF app
└── FunctionMovieApp/            # Azure Functions app
```

### Key Project Details

- **MvcMovie**: Main web application, Entity Framework with Movie model, CRUD operations, supports both SQL Server and SQLite
- **RazorMovie**: Uses HtmlSanitizer (intentionally outdated version 7.1.542 for upgrade testing), has WarningsAsErrors enabled
- **WpfMovie**: Uses BinaryFormatter (deprecated in .NET 8+), Windows-specific, cannot build on Linux
- **FunctionMovieApp**: Azure Functions v4, includes ServiceBus and Storage extensions

## Build Instructions

### Prerequisites
- .NET 8.0 SDK (for building .NET 6.0 projects)
- **.NET 6.0 Runtime required** for running applications and tests
- Entity Framework Core tools: `dotnet tool install --global dotnet-ef`
- SQL Server or SQLite for database operations  
- Windows environment required for WPF projects

**Critical**: While .NET 8.0 SDK can build .NET 6.0 projects, you must install .NET 6.0 runtime to run the applications.

### Restore Dependencies
```bash
dotnet restore
```
**Known Issue**: RazorMovie project will fail restore due to vulnerable HtmlSanitizer package with WarningsAsErrors enabled. This is intentional for upgrade scenario testing.

**Workaround**: Build individual projects:
```bash
dotnet restore MvcMovie/MvcMovie.csproj
dotnet restore FunctionMovieApp/FunctionMovieApp.csproj
dotnet restore MvcMovie.Tests/MvcMovie.Tests.csproj
```

### Build Commands

**Build individual projects (recommended):**
```bash
# MVC web app (primary project) - ~2 seconds
dotnet build MvcMovie/MvcMovie.csproj

# Azure Functions - ~3 seconds  
dotnet build FunctionMovieApp/FunctionMovieApp.csproj

# Test projects - ~2 seconds
dotnet build MvcMovie.Tests/MvcMovie.Tests.csproj
```

**Build entire solution:**
```bash
dotnet build MvcMovieNet6.sln
```
**Warning**: This will fail due to RazorMovie NU1902 error and WpfMovie Windows dependency.

### Test Commands

**Run specific test projects:**
```bash
# NUnit tests (MvcMovie) - requires .NET 6.0 runtime
dotnet test MvcMovie.Tests/MvcMovie.Tests.csproj

# MSTest tests (RazorMovie) - may fail due to RazorMovie build issues  
dotnet test RazorMovie.Tests/RazorMovie.Tests.csproj
```

**Note**: Tests require .NET 6.0 runtime installed, not just SDK.

**Expected test frameworks:**
- NUnit 3.13.3 (MvcMovie.Tests, WpfMovie.Tests)
- MSTest (RazorMovie.Tests)

### Run Applications

**MVC Web App:**
```bash
cd MvcMovie
dotnet run
```
Default URL: `https://localhost:7137` or `http://localhost:5137`

**Azure Functions:**
```bash
cd FunctionMovieApp
func start
```
Requires Azure Functions Core Tools.

## Common Build Issues & Workarounds

### 1. RazorMovie NU1902 Security Warning
**Issue**: `Package 'HtmlSanitizer' 7.1.542 has a known moderate severity vulnerability`  
**Cause**: Intentionally outdated package + WarningsAsErrors in project file  
**Workaround**: Skip RazorMovie project or temporarily disable WarningsAsErrors

### 2. WPF Project Build Failures on Linux
**Issue**: `Microsoft.NET.Sdk.WindowsDesktop.targets not found`  
**Cause**: WPF requires Windows Desktop SDK  
**Workaround**: Build only on Windows or exclude WPF projects

### 3. Package Version Conflicts (NU1603)
**Issue**: Multiple NU1603 warnings about version mismatches  
**Expected**: These warnings are normal for this repository's upgrade scenario testing

### 4. .NET 6.0 Runtime Required for Execution
**Issue**: Applications and tools require .NET 6.0 runtime but may have .NET 8.0 SDK installed  
**Error**: `You must install or update .NET to run this application... Framework: Microsoft.NETCore.App, version '6.0.0'`  
**Solution**: Install .NET 6.0 runtime alongside .NET 8.0 SDK

### 6. NUnit Analyzer Warnings
**Issue**: `Consider using Assert.That instead of Assert.AreEqual`  
**Expected**: Part of the upgrade challenge scenarios

## Key Configuration Files

**Root level:**
- `MvcMovieNet6.sln` - Solution file with 7 projects
- `.mcp.json` - MCP configuration
- `.gitignore` - Standard Visual Studio .gitignore  
- `README.md` - Basic project description

**MvcMovie project:**
- `appsettings.json` - Standard ASP.NET Core configuration  
- `appsettings_SQLite.json` - SQLite-specific configuration
- `Program.cs` - Application entry point and service configuration
- `Data/MvcMovieContext.cs` - Entity Framework DbContext
- `Models/Movie.cs` - Movie entity model
- `Controllers/MoviesController.cs` - MVC controller with CRUD operations
- `Controllers/HelloWorldController.cs` - Sample controller
- `Views/Movies/` - Razor views for Movie CRUD
- `Migrations/` - Entity Framework migrations

**Azure Functions:**  
- `host.json` - Azure Functions configuration
- `local.settings.json` - Local development settings
- `FileMoverFunction.cs` - Sample function implementation

**Test Projects:**
- Use NUnit (MvcMovie.Tests, WpfMovie.Tests) and MSTest (RazorMovie.Tests)
- Basic unit tests following Arrange-Act-Assert pattern

## Database & Migrations

**Entity Framework Context**: `MvcMovieContext` in MvcMovie/Data/  
**Model**: `Movie` class with Title, ReleaseDate, Genre, Price, Rating  
**Migrations**: Located in MvcMovie/Migrations/  

**Database Commands:**
```bash
cd MvcMovie
# Install EF tools first: dotnet tool install --global dotnet-ef
# Note: EF tools also require .NET 6.0 runtime
dotnet ef database update              # Apply migrations
dotnet ef migrations add <name>        # Add new migration  
dotnet ef migrations list              # List migrations
```

**Runtime Dependency**: All EF commands require .NET 6.0 runtime to be installed.

## Testing Patterns

**NUnit Projects**: Use `[Test]`, `[SetUp]` attributes  
**MSTest Projects**: Use `[TestMethod]`, `[TestInitialize]` attributes  
**Test Structure**: Arrange-Act-Assert pattern consistently used

## Security Considerations

- Nullable reference types enabled across projects
- SQL injection protection via Entity Framework
- **Known vulnerability**: HtmlSanitizer 7.1.542 in RazorMovie (intentional)

## Development Notes

**When making changes:**
1. Always run `dotnet restore` on specific projects if solution restore fails
2. Build and test individual projects rather than entire solution
3. Expect warnings about package versions - these are intentional
4. WPF-related changes require Windows environment
5. Database changes require Entity Framework migrations

**Trust these instructions** - the build issues mentioned are well-documented and expected. Only explore alternatives if these specific commands fail in unexpected ways.