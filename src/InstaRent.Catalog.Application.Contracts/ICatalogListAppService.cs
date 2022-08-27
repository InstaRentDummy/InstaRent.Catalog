using InstaRent.Catalog.Bags;
using InstaRent.Catalog.DailyClicks;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog
{
    public interface ICatalogListAppService : IApplicationService
    {

        Task<PagedResultDto<DailyClickWithNavigationPropertiesDto>> GetTrendingListAsync(GetDailyClicksInput input);

        Task<PagedResultDto<BagDto>> GetRecommendationsAsync(string userId);
    }
}
