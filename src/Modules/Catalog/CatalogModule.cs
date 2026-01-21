using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;

namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
    {
        //Add services
        
        // Api Endpoints
        
        // Application services
        
        // Data - Infrastructure
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<CatalogDbContext>(options =>
            options.UseNpgsql(connectionString));
        
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