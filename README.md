# MvcMovieSampleNet8

This repository hosts a .NET 8 web app derived from the ASP.NET Core MVC movie sample. It demonstrates fundamental practices for building an MVC application, including models, views, and controllers.

## Purpose

This project explores .NET 8 features and demonstrates best practices for building modern web applications. It includes modifications to showcase various learning opportunities and Azure App Service deployment readiness.

## Getting Started

1. Clone the repository.
2. Restore dependencies with `dotnet restore`.
3. Run the app using `dotnet run`.
4. Visit the provided URL in your browser.

## Notes

* This project is based on the original ASP.NET Core MVC movie sample from Microsoft.  
* Minimal changes have been introduced to learn about upgrade issues and best practices.  
* For documentation, refer to the official ASP.NET Core guides.
* For research purposes, this project intentionally references an out of date version of HtmlSanitizer.

## Solution structure

1. MvcMovie: an ASP.NET Core 8.0 MVC web app. This app performs CRUD operations on the `Movie` model in SQL Server.
1. MvcMovie.Tests: an nUnit test project for the MVC web app.
1. RazorMovie: an ASP.NET Core 8.0 Razor Pages web app. This app uses HtmlSanitizer.
1. RazorMovie.Tests: an MSTest project for the Razor web app.
1. WpfMovie: a Windows Presentation Framework app that presents a form for editing in-memory `Movie` models.
1. WpfMovie.Tests: an nUnit test project for the WPF project.
1. FunctionMovieApp: an Azure Functions app targeting .NET 8.0.

## Azure App Service Deployment

This solution is now ready for Azure App Service deployment:

1. **MvcMovie Web App**: Configure the `MvcMovieContext` connection string in Azure App Settings to point to Azure SQL Database
2. **RazorMovie Web App**: Ready for deployment with rate limiting and HTML sanitization features
3. **FunctionMovieApp**: Can be deployed to Azure Functions with .NET 8 runtime

### Connection String Configuration

For production deployment, replace the connection string template in `appsettings.json`:
```json
"MvcMovieContext": "Server=tcp:{your-server}.database.windows.net,1433;Database=MvcMovieContext;User ID={your-username};Password={your-password};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

## Upgrade scenarios addressed

1. ✅ The WpfMovie project uses BinaryFormatter which is deprecated in .NET8 (removed in .NET9)
1. ✅ The upgrade correctly chose the correct TFM for the WPF project, and retained the OS specific TFM for the test project.
1. ✅ The HtmlSanitizer reference in the RazorMovie project was updated from the vulnerable version, and namespace changes were fixed (Ganss.XSS → Ganss.Xss).
1. ✅ The upgrade of the MvcMovie project resolved transitive challenges from Microsoft.Data.SqlClient and updated to stable Entity Framework Core 8.0 packages.
1. ✅ The upgrade updated NuGet packages and resolved NUnit API breaking changes (Assert.AreEqual → Assert.That).
1. ✅ Azure Functions project upgraded to .NET 8 with correct NuGet packages and removed conflicting Microsoft.CSharp reference.
1. ✅ Added Azure App Service deployment support with web.config files and Azure SQL Database connection string templates.