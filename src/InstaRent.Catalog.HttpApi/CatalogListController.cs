using InstaRent.Catalog.Bags;
using InstaRent.Catalog.DailyClicks;
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
        [Route("trending")]
        public Task<PagedResultDto<DailyClickWithNavigationPropertiesDto>> GetTrendingListAsync(GetDailyClicksInput input)
        {
            return _catalogListAppService.GetTrendingListAsync(input);
        }

        //GetRecommendationList
        [HttpGet]
        [Route("recommendations/{userId}")]
        public Task<PagedResultDto<BagDto>> GetRecommendationsAsync(string userId)
        {
            return _catalogListAppService.GetRecommendationsAsync(userId);
        }

    }

}