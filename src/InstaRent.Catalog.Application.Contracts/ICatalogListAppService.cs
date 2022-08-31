using InstaRent.Catalog.Bags;
using InstaRent.Catalog.DailyClicks;
using InstaRent.Catalog.TotalClicks;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog
{
    public interface ICatalogListAppService : IApplicationService
    {

        Task<PagedResultDto<DailyClickWithNavigationPropertiesDto>> GetTrendingListAsync(GetDailyClicksInput input);

        Task<PagedResultDto<TotalClickWithNavigationPropertiesDto>> GetMostVisitedListAsync(GetTotalClicksInput input);

        Task<PagedResultDto<BagDto>> GetRecommendationsAsync(string userId);

        Task<string> IncreaseAsync(Guid bag_id);
    }
}
