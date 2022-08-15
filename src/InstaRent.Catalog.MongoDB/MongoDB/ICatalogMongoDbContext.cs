using InstaRent.Catalog.Bags;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace InstaRent.Catalog.MongoDB;

[ConnectionStringName(CatalogDbProperties.ConnectionStringName)]
public interface ICatalogMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
    IMongoCollection<Bag> Bags { get; }
}
