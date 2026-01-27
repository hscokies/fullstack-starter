using Application;
using Infrastructure;
using NLog;
using NLog.Web;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var logger = LogManager.Setup()
    .LoadConfigurationFromAppSettings()
    .GetCurrentClassLogger();

try
{
    builder.Services
        .AddPersistence(configuration)
        .AddApplication()
        .AddPresentation();
    

    var app = builder.Build();

    var group = app.MapGroup("/api");
    app.MapEndpoints(group);
    
    app.MapGet("/api/ping", (c) => c.Response.WriteAsync("Ok")).WithOpenApi();
    
    app.UseExceptionHandler();
    app.UseLoggingMiddleware();
    
    if (!app.Environment.IsProduction())
    {
        app.UseOpenApi(c => { c.Path = "/api/swagger/{documentName}/swagger.json"; });
        app.UseSwaggerUi(c =>
        {
            c.DocumentPath = "/api/swagger/{documentName}/swagger.json";
            c.Path = "/api/swagger";
        });

        // https://devblogs.microsoft.com/dotnet/introducing-devops-friendly-ef-core-migration-bundles/
        app.ApplyMigrations();
    }

    app.UseHttpsRedirection();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Exception occured during startup {message}.", ex.Message);
}
finally
{
    LogManager.Shutdown();
}
