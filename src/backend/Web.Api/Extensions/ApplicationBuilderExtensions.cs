using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Web.Api.Endpoints;
using Web.Api.Middlewares;

namespace Web.Api.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(this WebApplication app)
    {
        app.UseMiddleware<LoggingMiddleware>();
        return app;
    }
    
    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;
        
        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
    
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext =
            scope.ServiceProvider.GetRequiredService<DataContext>();

        if (dbContext.Database.IsRelational())
        {
            dbContext.Database.Migrate();
        }
    }
}
