using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.Data.Seed;

namespace Catalog.Data.Seed;

public class CatalogDataSeeder(CatalogDbContext catalogDbContext) 
    : IDataSeeder
{
    public async Task SeedAllAsync()
    {
        if (!await catalogDbContext.Products.AnyAsync())
        {
            await catalogDbContext.Products.AddRangeAsync(InitialData.Products);
            await catalogDbContext.SaveChangesAsync();
        }
    }
}