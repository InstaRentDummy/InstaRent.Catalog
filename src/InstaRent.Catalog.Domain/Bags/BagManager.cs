using InstaRent.Catalog.DailyClicks;
using InstaRent.Catalog.UserPreferences;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace InstaRent.Catalog.Bags
{
    public class BagManager : DomainService
    {
        private readonly IBagRepository _bagRepository;

        public BagManager(IBagRepository bagRepository)
        {
            _bagRepository = bagRepository;
        }

        public async Task<Bag> CreateAsync(
        string bag_name, string description, List<string> image_urls, DateTime rental_start_date, DateTime rental_end_date, double price, List<string> tags, string status, string renter_id, double? avgRating, double? totalRating, int? totalNumOfRating)
        {
            var bag = new Bag(
             GuidGenerator.Create(),
             bag_name, description, image_urls, rental_start_date, rental_end_date, price, tags, status, renter_id,avgRating,totalRating, totalNumOfRating ,false 
             );

            return await _bagRepository.InsertAsync(bag);
        }

        public async Task<Bag> UpdateAsync(
            Guid id,
            string bag_name, string description, List<string> image_urls, DateTime rental_start_date, DateTime rental_end_date,double price, List<string> tags, string status, string renter_id, double? avgRating, double? totalRating, int? totalNumOfRating, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _bagRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var bag = await AsyncExecuter.FirstOrDefaultAsync(query);

            bag.bag_name = bag_name;
            bag.description = description;
            bag.image_urls = image_urls;
            bag.rental_start_date = rental_start_date;
            bag.rental_end_date = rental_end_date;
            bag.price = price;
            bag.tags = tags;
            bag.status = status;
            bag.renter_id = renter_id;
            bag.AvgRating = avgRating;
            bag.TotalRating = totalRating;
            bag.TotalNumofRating = totalNumOfRating;
            bag.LastModificationTime = DateTime.Now;
            

            bag.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _bagRepository.UpdateAsync(bag);
        }

        public async Task DeleteAsync(Guid id, [CanBeNull] string concurrencyStamp = null)
        {
            var queryable = await _bagRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var bag = await AsyncExecuter.FirstOrDefaultAsync(query);

            bag.isdeleted = true; 
            bag.LastModificationTime = DateTime.Now;
            bag.SetConcurrencyStampIfNotNull(concurrencyStamp);
            await _bagRepository.UpdateAsync(bag);
        }

        public async Task<Bag> RateAsync(
            Guid id, double rating, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _bagRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var bag = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (bag == null) return null;

            double avgRating = 0;

            avgRating = ((bag.TotalRating ?? 0.0) + rating) / ((bag.TotalNumofRating ?? 0) + 1);
            bag.AvgRating = avgRating;
            bag.TotalRating = (bag.TotalRating ?? 0.0) + rating;
            bag.TotalNumofRating = (bag.TotalNumofRating ?? 0) + 1;
            bag.LastModificationTime = DateTime.Now;

            return await _bagRepository.UpdateAsync(bag);

        }
    }
}
