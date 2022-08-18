using InstaRent.Catalog.Bags;
using InstaRent.Catalog.DailyClicks;
using InstaRent.Catalog.TotalClicks;
using InstaRent.Catalog.UserPreferences;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace InstaRent.Catalog.MongoDB;

[DependsOn(
    typeof(CatalogDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class CatalogMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<CatalogMongoDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, MongoQuestionRepository>();
             */
            options.AddRepository<Bag, Bags.MongoBagRepository>();
            options.AddRepository<DailyClick, DailyClicks.MongoDailyClickRepository>();
            options.AddRepository<TotalClick, TotalClicks.MongoTotalClickRepository>();
            options.AddRepository<UserPreference, UserPreferences.MongoUserPreferenceRepository>();
            
        });
    }
}
