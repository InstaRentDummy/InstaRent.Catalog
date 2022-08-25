using InstaRent.Catalog.DailyClicks;
using InstaRent.Catalog.UserPreferences;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace InstaRent.Catalog
{


    [RemoteService(Name = "Catalog")]
    [Area("catalog")]
    [ControllerName("CatalogList")]
    [Route("api/catalog")]
    public class CatalogListController : AbpController, ICatalogListAppService
    {
        private readonly ICatalogListAppService _catalogListAppService;

        public CatalogListController(ICatalogListAppService catalogListAppService)
        {
            _catalogListAppService = catalogListAppService;
        }

        [HttpGet]
        [Route("trending/{period}")]
        public Task<PagedResultDto<DailyClickWithNavigationPropertiesDto>> GetTrendingListAsync(string period)
        {
            return _catalogListAppService.GetTrendingListAsync(period);
        }

        //GetRecommendationList
        [HttpGet]
        [Route("recommendations/{userId}")]
        public Task<PagedResultDto<UserPreferenceWithNavigationPropertiesDto>> GetRecommendationsAsync(string userId)
        {
            return _catalogListAppService.GetRecommendationsAsync(userId);
        }

    }

}