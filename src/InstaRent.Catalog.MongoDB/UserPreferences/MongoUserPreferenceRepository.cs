using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using InstaRent.Catalog.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace InstaRent.Catalog.UserPreferences
{
    public class MongoUserPreferenceRepository : MongoDbRepository<CatalogMongoDbContext, UserPreference, long>, IUserPreferenceRepository
    {
        public MongoUserPreferenceRepository(IMongoDbContextProvider<CatalogMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<UserPreference>> GetListAsync(
            string filterText = null,
            string userId = null,
            string tags = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, userId, tags);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? UserPreferenceConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<UserPreference>>()
                .PageBy<UserPreference, IMongoQueryable<UserPreference>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
           string filterText = null,
           string userId = null,
           string tags = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, userId, tags);
            return await query.As<IMongoQueryable<UserPreference>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<UserPreference> ApplyFilter(
            IQueryable<UserPreference> query,
            string filterText,
            string userId = null,
            string tags = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.UserId.Contains(filterText) || e.Tags.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(userId), e => e.UserId.Contains(userId))
                    .WhereIf(!string.IsNullOrWhiteSpace(tags), e => e.Tags.Contains(tags));
        }
    }
}