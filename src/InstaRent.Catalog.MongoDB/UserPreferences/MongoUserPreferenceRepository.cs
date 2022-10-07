using InstaRent.Catalog.Bags;
using InstaRent.Catalog.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace InstaRent.Catalog.UserPreferences
{
    public class MongoUserPreferenceRepository : MongoDbRepository<CatalogMongoDbContext, UserPreference, Guid>, IUserPreferenceRepository
    {
        public MongoUserPreferenceRepository(IMongoDbContextProvider<CatalogMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Bag>> GetListWithNavigationPropertiesAsync(
            string userId = null, 
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyRecommendationFilter((await GetMongoQueryableAsync(cancellationToken)), userId);
            var userPreferences = await query
                .As<IMongoQueryable<UserPreference>>()
                .PageBy<UserPreference, IMongoQueryable<UserPreference>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            var tags = userPreferences.First().Tags.OrderBy(x => x.weightage).Select(y => y.tagname).ToList();

            var dbContext = await GetDbContextAsync(cancellationToken);
            //return tags.Select(s => new UserPreferenceWithNavigationProperties
            //{
            //    Tag = s,
            //    Bags = dbContext.Bags.AsQueryable().Where(e => e.tags == s).ToList(),
            //}).ToList();
            List<Bag> result = new();
            tags.ForEach(s =>
            {
                var bags = dbContext.Bags.AsQueryable().Where(e => e.tags.Contains(s)).ToList();

                bags.ForEach(x =>
                {
                    if (!result.Where(r => r.Id == x.Id).Any())
                        result.Add(x);
                });
            });

            return result.OrderBy(x => x.tags).ToList();
        }

        public async Task<List<UserPreference>> GetListAsync(
            string filterText = null,
            string userId = null,
            string tags = null,
            double? avgRatingMin = null,
            double? avgRatingMax = null,
            double? totalNumofRatingMin = null,
            double? totalNumofRatingMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, userId, tags,avgRatingMin,avgRatingMax,totalNumofRatingMin,totalNumofRatingMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? UserPreferenceConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<UserPreference>>()
                .PageBy<UserPreference, IMongoQueryable<UserPreference>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(
           string filterText = null,
           string userId = null,
           string tags = null,
            double? avgRatingMin = null,
            double? avgRatingMax = null,
            double? totalNumofRatingMin = null,
            double? totalNumofRatingMax = null,
           CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, userId, tags, avgRatingMin, avgRatingMax, totalNumofRatingMin, totalNumofRatingMax);
            return await query.As<IMongoQueryable<UserPreference>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<UserPreference> ApplyFilter(
            IQueryable<UserPreference> query,
            string filterText,
            string userId = null,
            string tags = null,
            double? avgRatingMin = null,
            double? avgRatingMax = null,
            double? totalNumofRatingMin = null,
            double? totalNumofRatingMax = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.UserId.Contains(filterText) ||  e.Tags.Any(t => t.tagname.Contains(filterText)))
                    .WhereIf(!string.IsNullOrWhiteSpace(userId), e => e.UserId.Contains(userId))
                    .WhereIf(!string.IsNullOrWhiteSpace(tags), e => e.Tags.Any(t => t.tagname.Contains(tags)))
                    .WhereIf(avgRatingMin.HasValue, e => e.AvgRating >= avgRatingMin.Value)
                    .WhereIf(avgRatingMax.HasValue, e => e.AvgRating <= avgRatingMax.Value)
                    .WhereIf(totalNumofRatingMin.HasValue, e => e.TotalNumofRating >= totalNumofRatingMin.Value)
                    .WhereIf(totalNumofRatingMax.HasValue, e => e.TotalNumofRating <= totalNumofRatingMax.Value);
        }


        protected virtual IQueryable<UserPreference> ApplyRecommendationFilter(
            IQueryable<UserPreference> query,
            string userId)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(userId), e => e.UserId == userId);
        }
    }
}