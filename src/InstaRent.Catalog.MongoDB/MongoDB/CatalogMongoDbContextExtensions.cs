using Volo.Abp;
using Volo.Abp.MongoDB;

namespace InstaRent.Catalog.MongoDB;

public static class CatalogMongoDbContextExtensions
{
    public static void ConfigureCatalog(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
