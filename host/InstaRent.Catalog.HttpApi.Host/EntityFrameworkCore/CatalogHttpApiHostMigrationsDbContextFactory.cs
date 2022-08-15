using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InstaRent.Catalog.EntityFrameworkCore;

public class CatalogHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<CatalogHttpApiHostMigrationsDbContext>
{
    public CatalogHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<CatalogHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Catalog"));

        return new CatalogHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
