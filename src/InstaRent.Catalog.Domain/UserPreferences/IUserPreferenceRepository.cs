using InstaRent.Catalog.Bags;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace InstaRent.Catalog.UserPreferences
{
    public interface IUserPreferenceRepository : IRepository<UserPreference, Guid>
    {
        Task<List<Bag>> GetListWithNavigationPropertiesAsync(
           string userId = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<UserPreference>> GetListAsync(
            string filterText = null,
            string userId = null,
            string tags = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
           string filterText = null,
           string userId = null,
           string tags = null,
            CancellationToken cancellationToken = default);
    }
}