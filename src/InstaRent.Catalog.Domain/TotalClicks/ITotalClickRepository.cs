using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace InstaRent.Catalog.TotalClicks
{
    public interface ITotalClickRepository : IRepository<TotalClick, Guid>
    {
        Task<TotalClickWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<TotalClickWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            long? clicksMin = null,
            long? clicksMax = null,
            Guid? bagId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<TotalClick>> GetListAsync(
                    string filterText = null,
                    long? clicksMin = null,
                    long? clicksMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            long? clicksMin = null,
            long? clicksMax = null,
            Guid? bagId = null,
            CancellationToken cancellationToken = default);
    }
}