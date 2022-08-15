using InstaRent.Catalog.MongoDB;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver;

namespace InstaRent.Catalog.Bags
{
    public class MongoBagRepository : MongoDbRepository<CatalogMongoDbContext, Bag, Guid>, IBagRepository
    {
        public MongoBagRepository(IMongoDbContextProvider<CatalogMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Bag>> GetListAsync(
            string filterText = null,
            string bag_name = null,
            string description = null,
            string image_urls = null,
            DateTime? rental_start_dateMin = null,
            DateTime? rental_start_dateMax = null,
            DateTime? rental_end_dateMin = null,
            DateTime? rental_end_dateMax = null,
            string tags = null,
            string status = null,
            string renter_id = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, bag_name, description, image_urls, rental_start_dateMin, rental_start_dateMax, rental_end_dateMin, rental_end_dateMax, tags, status, renter_id);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BagConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<Bag>>()
                .PageBy<Bag, IMongoQueryable<Bag>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
           string filterText = null,
           string bag_name = null,
           string description = null,
           string image_urls = null,
           DateTime? rental_start_dateMin = null,
           DateTime? rental_start_dateMax = null,
           DateTime? rental_end_dateMin = null,
           DateTime? rental_end_dateMax = null,
           string tags = null,
           string status = null,
           string renter_id = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, bag_name, description, image_urls, rental_start_dateMin, rental_start_dateMax, rental_end_dateMin, rental_end_dateMax, tags, status, renter_id);
            return await query.As<IMongoQueryable<Bag>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Bag> ApplyFilter(
            IQueryable<Bag> query,
            string filterText,
            string bag_name = null,
            string description = null,
            string image_urls = null,
            DateTime? rental_start_dateMin = null,
            DateTime? rental_start_dateMax = null,
            DateTime? rental_end_dateMin = null,
            DateTime? rental_end_dateMax = null,
            string tags = null,
            string status = null,
            string renter_id = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.bag_name.Contains(filterText) || e.description.Contains(filterText) || e.image_urls.Contains(filterText) || e.tags.Contains(filterText) || e.status.Contains(filterText) || e.renter_id.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(bag_name), e => e.bag_name.Contains(bag_name))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(image_urls), e => e.image_urls.Contains(image_urls))
                    .WhereIf(rental_start_dateMin.HasValue, e => e.rental_start_date >= rental_start_dateMin.Value)
                    .WhereIf(rental_start_dateMax.HasValue, e => e.rental_start_date <= rental_start_dateMax.Value)
                    .WhereIf(rental_end_dateMin.HasValue, e => e.rental_end_date >= rental_end_dateMin.Value)
                    .WhereIf(rental_end_dateMax.HasValue, e => e.rental_end_date <= rental_end_dateMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(tags), e => e.tags.Contains(tags))
                    .WhereIf(!string.IsNullOrWhiteSpace(status), e => e.status.Contains(status))
                    .WhereIf(!string.IsNullOrWhiteSpace(renter_id), e => e.renter_id.Contains(renter_id));
        }
    }
}
