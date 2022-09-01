using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.Domain.Repositories;

namespace InstaRent.Catalog.Bags
{
    public interface IBagRepository : IRepository<Bag, Guid>
    {
        Task<List<Bag>> GetListAsync(
            string filterText = null,
            string bag_name = null,
            string description = null,
            string image_urls = null,
            DateTime? rental_start_dateMin = null,
            DateTime? rental_start_dateMax = null,
            DateTime? rental_end_dateMin = null,
            DateTime? rental_end_dateMax = null,
            double priceMin = 0.0,
            double priceMax = double.MaxValue,
            string tags = null,
            string status = null,
            string renter_id = null,
            bool? isdeleted = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string bag_name = null,
            string description = null,
            string image_urls = null,
            DateTime? rental_start_dateMin = null,
            DateTime? rental_start_dateMax = null,
            DateTime? rental_end_dateMin = null,
            DateTime? rental_end_dateMax = null,
            double priceMin = 0.0,
            double priceMax = double.MaxValue,
            string tags = null,
            string status = null,
            string renter_id = null,
            bool? isdeleted = null,
            CancellationToken cancellationToken = default);
    }
}
