using InstaRent.Catalog.Bags;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace InstaRent.Catalog.MongoDB;

[ConnectionStringName(CatalogDbProperties.ConnectionStringName)]
public class CatalogMongoDbContext : AbpMongoDbContext, ICatalogMongoDbContext
{
    public IMongoCollection<Bag> Bags => Collection<Bag>();
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureCatalog();

        modelBuilder.Entity<Bag>(b => { b.CollectionName = CatalogDbProperties.DbTablePrefix + "Bags"; });

    }
}
