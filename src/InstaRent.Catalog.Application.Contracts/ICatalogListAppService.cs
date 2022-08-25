using InstaRent.Catalog.Bags;
using InstaRent.Catalog.DailyClicks;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog
{
    public interface ICatalogListAppService : IApplicationService
    {

        Task<PagedResultDto<DailyClickWithNavigationPropertiesDto>> GetTrendingListAsync(string period);

        Task<PagedResultDto<BagDto>> GetRecommendationsAsync(string userId);
    }
}
