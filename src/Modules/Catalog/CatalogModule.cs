using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Interceptors;


namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        //Add services
        
        // Api Endpoints
        
        // Application services
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        // Data - Infrastructure
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        
        services.AddDbContext<CatalogDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString);
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });

        services.AddScoped<IDataSeeder, CatalogDataSeeder>();
        
        return services;
    }
    
    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        //Use Api Endpoints
        
        //Use Application services
        
        //Use Data - Infrastructure
        app.UseMigration<CatalogDbContext>();
        
        
        return app;
    }
}