using System.Reflection;
using Application.Common;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        return services.AddValidatorsFromAssembly(assembly)
            .AddFluentValidationAutoValidation()
            .AddHandlersFromAssembly(assembly);
    }
    
    private static IServiceCollection AddHandlersFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var domainTypes = new HashSet<Type>
        {
            typeof(IBaseCommandHandler),
            typeof(IQueryHandler),
        };


        var handlers = assembly.GetTypes()
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.GetTypeInfo().ImplementedInterfaces.Intersect(domainTypes).Any());
        
        foreach (var handler in handlers)
        {
            services.AddScoped(handler);
        }
        
        return services;
    }
}
