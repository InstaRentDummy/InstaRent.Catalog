using InstaRent.Catalog.MongoDB;
using InstaRent.Catalog.UserPreferences;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace InstaRent.Catalog.Recommendations
{
    public class MongoRecommendationsRepository : MongoDbRepository<CatalogMongoDbContext, UserPreference, Guid>
    {
        public MongoRecommendationsRepository(IMongoDbContextProvider<CatalogMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<UserPreferenceWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string userId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), userId);
            var userPreferences = await query
                .As<IMongoQueryable<UserPreference>>()
                .PageBy<UserPreference, IMongoQueryable<UserPreference>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            var tags = userPreferences.First().Tags.OrderBy(x => x).ToList();

            var dbContext = await GetDbContextAsync(cancellationToken);
            return tags.Select(s => new UserPreferenceWithNavigationProperties
            {
                Tag = s,
                Bags = dbContext.Bags.AsQueryable().Where(e => e.tags == s).ToList(),

            }).ToList();
        }

        public async Task<List<UserPreference>> GetListAsync(
            string userId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), userId);
            //query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DailyClickConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<UserPreference>>()
                .PageBy<UserPreference, IMongoQueryable<UserPreference>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
           string userId = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), userId);
            return await query.As<IMongoQueryable<UserPreference>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<UserPreference> ApplyFilter(
            IQueryable<UserPreference> query,
            string userId)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(userId), e => e.UserId == userId);
        }
    }
}
