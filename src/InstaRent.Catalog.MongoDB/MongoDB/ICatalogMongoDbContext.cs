using InstaRent.Catalog.Bags;
using InstaRent.Catalog.DailyClicks;
using InstaRent.Catalog.TotalClicks;
using InstaRent.Catalog.UserPreferences;
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
    IMongoCollection<DailyClick> DailyClicks { get; }
    IMongoCollection<TotalClick> TotalClicks { get; }
    IMongoCollection<UserPreference> UserPreferences { get; }
}