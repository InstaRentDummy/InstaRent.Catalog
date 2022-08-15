using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace InstaRent.Catalog.EntityFrameworkCore;

public class CatalogHttpApiHostMigrationsDbContext : AbpDbContext<CatalogHttpApiHostMigrationsDbContext>
{
    public CatalogHttpApiHostMigrationsDbContext(DbContextOptions<CatalogHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureCatalog();
    }
}
