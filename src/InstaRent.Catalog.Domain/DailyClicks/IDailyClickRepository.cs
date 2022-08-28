using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace InstaRent.Catalog.DailyClicks
{
    public interface IDailyClickRepository : IRepository<DailyClick, Guid>
    {
        Task<DailyClickWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<DailyClickWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            long? clicksMin = null,
            long? clicksMax = null,
            DateTime? lastModificationTimeMin = null,
            DateTime? lastModificationTimeMax = null,
            Guid? bagId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<DailyClick>> GetListAsync(
                    string filterText = null,
                    long? clicksMin = null,
                    long? clicksMax = null,
                    DateTime? lastModificationTimeMin = null,
                    DateTime? lastModificationTimeMax = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            long? clicksMin = null,
            long? clicksMax = null,
            DateTime? lastModificationTimeMin = null,
            DateTime? lastModificationTimeMax = null,
            Guid? bagId = null,
            CancellationToken cancellationToken = default);
    }
}