using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web.Api.Endpoints;
using Web.Api.Infrastructure;

namespace Web.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var serviceDescriptors = typeof(IEndpoint).Assembly.GetTypes()
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type));

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }
    
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddEndpointsApiExplorer();
        services.AddOpenApiDocument(settings =>
        {
            settings.Title = "Starter App";
            settings.Version = "v1";
        });
        
        services.AddProblemDetails();

        services.ConfigureHttpJsonOptions(x =>
        {
            x.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        services.AddEndpoints();
        
        return services;
    }
}
