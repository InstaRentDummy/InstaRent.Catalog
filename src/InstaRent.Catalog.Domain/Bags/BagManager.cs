using JetBrains.Annotations;
using System;
using System.Collections.Generic;
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
        string bag_name, string description, string image_urls, DateTime rental_start_date, DateTime rental_end_date, string tags, string status, string renter_id)
        {
            var bag = new Bag(
             GuidGenerator.Create(),
             bag_name, description, image_urls, rental_start_date, rental_end_date, tags, status, renter_id
             );

            return await _bagRepository.InsertAsync(bag);
        }

        public async Task<Bag> UpdateAsync(
            Guid id,
            string bag_name, string description, string image_urls, DateTime rental_start_date, DateTime rental_end_date, string tags, string status, string renter_id, [CanBeNull] string concurrencyStamp = null
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
            bag.tags = tags;
            bag.status = status;
            bag.renter_id = renter_id;

            bag.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _bagRepository.UpdateAsync(bag);
        }
    }
}
