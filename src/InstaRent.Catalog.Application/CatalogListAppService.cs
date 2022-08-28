using InstaRent.Catalog.Bags;
using InstaRent.Catalog.DailyClicks;
using InstaRent.Catalog.TotalClicks;
using InstaRent.Catalog.UserPreferences;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog
{
    public class CatalogListAppService : ApplicationService, ICatalogListAppService
    {
        private readonly IUserPreferenceRepository _userPreferenceRepository;
        private readonly ITotalClickRepository _totalClickRepository;
        private readonly IDailyClickRepository _dailyClickRepository;

        public CatalogListAppService(IDailyClickRepository dailyClickRepository, IUserPreferenceRepository userPreferenceRepository, ITotalClickRepository totalClickRepository)
        {
            _dailyClickRepository = dailyClickRepository;
            _userPreferenceRepository = userPreferenceRepository;
            _totalClickRepository = totalClickRepository;
        }

        public virtual async Task<PagedResultDto<DailyClickWithNavigationPropertiesDto>> GetTrendingListAsync(GetDailyClicksInput input)
        {
            var sortstr = " LastModificationTime DESC, clicks DESC";
            if (!string.IsNullOrEmpty(input.Sorting))
                sortstr = " LastModificationTime DESC, clicks DESC" + " ," + input.Sorting;

            var totalCount = await _dailyClickRepository.GetCountAsync(input.FilterText, input.clicksMin, input.clicksMax, input.lastModificationTimeMin, input.lastModificationTimeMax, input.BagId);
            var items = await _dailyClickRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.clicksMin, input.clicksMax, input.lastModificationTimeMin, input.lastModificationTimeMax, input.BagId, sortstr, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DailyClickWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DailyClickWithNavigationProperties>, List<DailyClickWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<PagedResultDto<TotalClickWithNavigationPropertiesDto>> GetMostVisitedListAsync(GetTotalClicksInput input)
        {
            var sortstr = " LastModificationTime DESC, clicks DESC";
            if (!string.IsNullOrEmpty(input.Sorting))
                sortstr = " LastModificationTime DESC, clicks DESC" + " ," + input.Sorting;

            var totalCount = await _totalClickRepository.GetCountAsync(input.FilterText, input.clicksMin, input.clicksMax, input.lastModificationTimeMin, input.lastModificationTimeMax, input.BagId);
            var items = await _totalClickRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.clicksMin, input.clicksMax, input.lastModificationTimeMin, input.lastModificationTimeMax, input.BagId, sortstr, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TotalClickWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TotalClickWithNavigationProperties>, List<TotalClickWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<PagedResultDto<BagDto>> GetRecommendationsAsync(string userId)
        {
            GetUserPreferencesInput input = new GetUserPreferencesInput()
            {
                UserId = userId
            };

            var totalCount = await _userPreferenceRepository.GetCountAsync(input.UserId);
            var items = await _userPreferenceRepository.GetListWithNavigationPropertiesAsync(input.UserId);
            //var testitems = ObjectMapper.Map<List<UserPreferenceWithNavigationProperties>, List<UserPreferenceWithNavigationPropertiesDto>>(items);

            var response = new PagedResultDto<BagDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Bag>, List<BagDto>>(items)
            };

            return response;
        }
    }

}
