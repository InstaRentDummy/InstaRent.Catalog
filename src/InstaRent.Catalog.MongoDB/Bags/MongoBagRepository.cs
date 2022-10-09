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
            double? priceMin = null,
            double? priceMax = null,
            string tags = null,
            string status = null,
            string renter_id = null,
            double? avgRatingMin = null,
            double? avgRatingMax = null,
            double? totalRatingMin = null,
            double? totalRatingMax = null,
            int? totalNumofRatingMin = null,
            int? totalNumofRatingMax = null,
            bool? isdeleted = null,
            DateTime? creation_timeMin = null,
            DateTime? creation_timeMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, bag_name, description, image_urls, rental_start_dateMin, rental_start_dateMax, rental_end_dateMin, rental_end_dateMax, priceMin, priceMax, tags, status, renter_id, avgRatingMin, avgRatingMax,totalRatingMin,totalRatingMax, totalNumofRatingMin, totalNumofRatingMax, isdeleted);
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
           double? priceMin = null,
           double? priceMax = null,
           string tags = null,
           string status = null,
           string renter_id = null,
            double? avgRatingMin = null,
            double? avgRatingMax = null,
            double? totalRatingMin = null,
            double? totalRatingMax = null,
            int? totalNumofRatingMin = null,
            int? totalNumofRatingMax = null,
           bool? isdeleted = null,
           DateTime? creation_timeMin = null,
           DateTime? creation_timeMax = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, bag_name, description, image_urls, rental_start_dateMin, rental_start_dateMax, rental_end_dateMin, rental_end_dateMax, priceMin, priceMax, tags, status, renter_id, avgRatingMin, avgRatingMax, totalRatingMin, totalRatingMax, totalNumofRatingMin, totalNumofRatingMax, isdeleted);
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
            double? priceMin = null,
            double? priceMax = null,
            string tags = null,
            string status = null,
            string renter_id = null,
            double? avgRatingMin = null,
            double? avgRatingMax = null,
            double? totalRatingMin = null,
            double? totalRatingMax = null,
            int? totalNumofRatingMin = null,
            int? totalNumofRatingMax = null,
            bool? isdeleted = null,
            DateTime? creation_timeMin = null,
            DateTime? creation_timeMax = null
            )
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.bag_name.ToLower().Contains(filterText.ToLower()) || e.description.ToLower().Contains(filterText.ToLower()) || e.image_urls.Any(i => i.Contains(filterText)) || e.tags.Any(t => t.ToLower().Contains(filterText.ToLower())) || e.status.Contains(filterText) || e.renter_id.ToLower().Contains(filterText.ToLower()))
                    .WhereIf(!string.IsNullOrWhiteSpace(bag_name), e => e.bag_name.ToLower().Contains(bag_name.ToLower()))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.description.ToLower().Contains(description.ToLower()))
                    .WhereIf(!string.IsNullOrWhiteSpace(image_urls), e => e.image_urls.Any(i => i.Contains(image_urls)))
                    .WhereIf(rental_start_dateMin.HasValue, e => e.rental_start_date >= rental_start_dateMin.Value)
                    .WhereIf(rental_start_dateMax.HasValue, e => e.rental_start_date <= rental_start_dateMax.Value)
                    .WhereIf(rental_end_dateMin.HasValue, e => e.rental_end_date >= rental_end_dateMin.Value)
                    .WhereIf(rental_end_dateMax.HasValue, e => e.rental_end_date <= rental_end_dateMax.Value)
                    .WhereIf(priceMin.HasValue, e => e.price >= priceMin.Value)
                    .WhereIf(priceMax.HasValue, e => e.price <= priceMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(tags), e => e.tags.Any(t => t.ToLower().Contains(tags.ToLower())))
                    .WhereIf(!string.IsNullOrWhiteSpace(status), e => e.status.ToLower().Contains(status.ToLower()))
                    .WhereIf(!string.IsNullOrWhiteSpace(renter_id), e => e.renter_id.ToLower().Contains(renter_id.ToLower()))
                    .WhereIf(isdeleted.HasValue, e => e.isdeleted.Equals(isdeleted.Value))
                    .WhereIf(creation_timeMin.HasValue, e => e.CreationTime >= creation_timeMin.Value)
                    .WhereIf(creation_timeMax.HasValue, e => e.CreationTime <= creation_timeMax.Value)
                    .WhereIf(avgRatingMin.HasValue, e => e.AvgRating >= avgRatingMin.Value)
                    .WhereIf(avgRatingMax.HasValue, e => e.AvgRating <= avgRatingMax.Value)
                    .WhereIf(totalRatingMin.HasValue, e => e.TotalRating >= totalRatingMin.Value)
                    .WhereIf(totalRatingMax.HasValue, e => e.TotalRating <= totalRatingMax.Value)
                    .WhereIf(totalNumofRatingMin.HasValue, e => e.TotalNumofRating >= totalNumofRatingMin.Value)
                    .WhereIf(totalNumofRatingMax.HasValue, e => e.TotalNumofRating <= totalNumofRatingMax.Value);
            ;
        }
    }
}