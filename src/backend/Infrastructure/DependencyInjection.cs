using Infrastructure.Persistence;
using Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;

        return services
            .AddDbContext<DataContext>(connectionString)
            .AddScoped<IDataContext>(sp => sp.GetRequiredService<DataContext>())
            .AddDbContext<ReadOnlyDataContext>(connectionString, QueryTrackingBehavior.NoTracking)
            .AddScoped<IReadOnlyDataContext>(sp => sp.GetRequiredService<ReadOnlyDataContext>())
            .AddIdentity();
    }

    private static IServiceCollection AddDbContext<TContext>(this IServiceCollection services, string connectionString, QueryTrackingBehavior trackingBehavior = QueryTrackingBehavior.TrackAll) where TContext : DbContext
    {
        return services.AddDbContext<TContext>(
            options => options
                .UseNpgsql(connectionString, b =>
                {
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                })
                .UseQueryTrackingBehavior(trackingBehavior)
                .UseSnakeCaseNamingConvention());
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 12;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        services.AddAuthentication()
            .AddCookie();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.LoginPath = "/login";
            options.AccessDeniedPath = null;
        });
        
        return services;
    }
}
