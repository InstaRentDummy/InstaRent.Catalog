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

namespace InstaRent.Catalog.DailyClicks
{
    public class MongoDailyClickRepository : MongoDbRepository<CatalogMongoDbContext, DailyClick, Guid>, IDailyClickRepository
    {
        public MongoDailyClickRepository(IMongoDbContextProvider<CatalogMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<DailyClickWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dailyClick = await (await GetMongoQueryableAsync(cancellationToken))
                .FirstOrDefaultAsync(e => e.Id == id, GetCancellationToken(cancellationToken));

            var bag = await (await GetDbContextAsync(cancellationToken)).Bags.AsQueryable().FirstOrDefaultAsync(e => e.Id == dailyClick.BagId, cancellationToken: cancellationToken);

            return new DailyClickWithNavigationProperties
            {
                DailyClick = dailyClick,
                Bag = bag,

            };
        }

        public async Task<List<DailyClickWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            long? clicksMin = null,
            long? clicksMax = null,
            DateTime? lastModificationTimeMin = null,
            DateTime? lastModificationTimeMax = null, 
            Guid? bagId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, clicksMin, clicksMax, lastModificationTimeMin, lastModificationTimeMax, bagId);
            var dailyClicks = await query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DailyClickConsts.GetDefaultSorting(false) : sorting.Split('.').Last())
                .As<IMongoQueryable<DailyClick>>()
                .PageBy<DailyClick, IMongoQueryable<DailyClick>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            var dbContext = await GetDbContextAsync(cancellationToken);
            return dailyClicks.Select(s => new DailyClickWithNavigationProperties
            {
                DailyClick = s,
                Bag = dbContext.Bags.AsQueryable().FirstOrDefault(e => e.Id == s.BagId),

            }).ToList();
        }

        public async Task<List<DailyClick>> GetListAsync(
            string filterText = null,
            long? clicksMin = null,
            long? clicksMax = null,
            DateTime? lastModificationTimeMin = null,
            DateTime? lastModificationTimeMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, clicksMin, clicksMax, lastModificationTimeMin, lastModificationTimeMax );
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DailyClickConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<DailyClick>>()
                .PageBy<DailyClick, IMongoQueryable<DailyClick>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
           string filterText = null,
           long? clicksMin = null,
           long? clicksMax = null,
           DateTime? lastModificationTimeMin = null,
           DateTime? lastModificationTimeMax = null,
           Guid? bagId = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, clicksMin, clicksMax, lastModificationTimeMin, lastModificationTimeMax, bagId);
            return await query.As<IMongoQueryable<DailyClick>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<DailyClick> ApplyFilter(
            IQueryable<DailyClick> query,
            string filterText,
            long? clicksMin = null,
            long? clicksMax = null,
            DateTime? lastModificationTimeMin = null,
            DateTime? lastModificationTimeMax = null,
            Guid? bagId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(clicksMin.HasValue, e => e.clicks >= clicksMin.Value)
                    .WhereIf(clicksMax.HasValue, e => e.clicks <= clicksMax.Value)
                    .WhereIf(lastModificationTimeMin.HasValue, e => e.LastModificationTime <= lastModificationTimeMin.Value)
                    .WhereIf(lastModificationTimeMax.HasValue, e => e.LastModificationTime <= lastModificationTimeMax.Value)
                    .WhereIf(bagId != null && bagId != Guid.Empty, e => e.BagId == bagId);
        }
    }
}