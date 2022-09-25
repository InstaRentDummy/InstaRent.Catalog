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
using InstaRent.Catalog.DailyClicks;

namespace InstaRent.Catalog.TotalClicks
{
    public class MongoTotalClickRepository : MongoDbRepository<CatalogMongoDbContext, TotalClick, Guid>, ITotalClickRepository
    {
        public MongoTotalClickRepository(IMongoDbContextProvider<CatalogMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<TotalClickWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var totalClick = await (await GetMongoQueryableAsync(cancellationToken))
                .FirstOrDefaultAsync(e => e.Id == id, GetCancellationToken(cancellationToken));

            var bag = await (await GetDbContextAsync(cancellationToken)).Bags.AsQueryable().FirstOrDefaultAsync(e => e.Id == totalClick.BagId, cancellationToken: cancellationToken);

            return new TotalClickWithNavigationProperties
            {
                TotalClick = totalClick,
                Bag = bag,

            };
        }

        public async Task<List<TotalClickWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
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
            var totalClicks = await query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TotalClickConsts.GetDefaultSorting(false) : sorting.Split('.').Last())
                .As<IMongoQueryable<TotalClick>>()
                .PageBy<TotalClick, IMongoQueryable<TotalClick>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            var dbContext = await GetDbContextAsync(cancellationToken);
            return totalClicks.Select(s => new TotalClickWithNavigationProperties
            {
                TotalClick = s,
                Bag = dbContext.Bags.AsQueryable().FirstOrDefault(e => e.Id == s.BagId),

            }).Where(b => b.Bag.isdeleted.Equals(false))
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), b => b.Bag.bag_name.Contains(filterText) || b.Bag.description.Contains(filterText) || b.Bag.image_urls.Any(i => i.Contains(filterText)) || b.Bag.tags.Any(t => t.Contains(filterText)) || b.Bag.status.Contains(filterText) || b.Bag.renter_id.Contains(filterText))
            .ToList();
        }

        public async Task<long> GetActiveCountAsync(
           string filterText = null,
           long? clicksMin = null,
           long? clicksMax = null,
           DateTime? lastModificationTimeMin = null,
           DateTime? lastModificationTimeMax = null,
           Guid? bagId = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, clicksMin, clicksMax, lastModificationTimeMin, lastModificationTimeMax, bagId);

            var totalClicks = await query.As<IMongoQueryable<TotalClick>>().ToListAsync(GetCancellationToken(cancellationToken));

            var dbContext = await GetDbContextAsync(cancellationToken);
            return totalClicks.Select(s => new TotalClickWithNavigationProperties
            {
                TotalClick = s,
                Bag = dbContext.Bags.AsQueryable()
                .FirstOrDefault(e => e.Id == s.BagId)

            }).Where(b => b.Bag.isdeleted.Equals(false))
              .WhereIf(!string.IsNullOrWhiteSpace(filterText), b => b.Bag.bag_name.Contains(filterText) || b.Bag.description.Contains(filterText) || b.Bag.image_urls.Any(i => i.Contains(filterText)) || b.Bag.tags.Any(t => t.Contains(filterText)) || b.Bag.status.Contains(filterText) || b.Bag.renter_id.Contains(filterText))

            .LongCount();
        }

        public async Task<List<TotalClick>> GetListAsync(
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
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TotalClickConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<TotalClick>>()
                .PageBy<TotalClick, IMongoQueryable<TotalClick>>(skipCount, maxResultCount)
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
            return await query.As<IMongoQueryable<TotalClick>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }



        protected virtual IQueryable<TotalClick> ApplyFilter(
            IQueryable<TotalClick> query,
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