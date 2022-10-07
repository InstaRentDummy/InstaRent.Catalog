using InstaRent.Catalog.Bags;
using InstaRent.Catalog.DailyClicks;
using InstaRent.Catalog.TotalClicks;
using InstaRent.Catalog.UserPreferences;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet]
        [Route("most-visited")]
        public Task<PagedResultDto<TotalClickWithNavigationPropertiesDto>> GetMostVisitedListAsync(GetTotalClicksInput input)
        {
            return _catalogListAppService.GetMostVisitedListAsync(input);
        }

        //GetRecommendationList
        [HttpGet]
        [Route("recommendations/{userId}")]
        public Task<PagedResultDto<BagDto>> GetRecommendationsAsync(GetUserRecommendationInput input)
        {
            return _catalogListAppService.GetRecommendationsAsync(input);
        }

        [HttpPost]
        [Route("increasecount/{bag_id}")]
        public Task<string> IncreaseAsync(Guid bag_id)
        {
            return _catalogListAppService.IncreaseAsync(bag_id);
        }

    }
}

