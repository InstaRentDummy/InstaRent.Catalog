using InstaRent.Catalog.Bags;
using InstaRent.Catalog.DailyClicks;
using InstaRent.Catalog.TotalClicks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace InstaRent.Catalog
{
    public class CatalogListAppService : ApplicationService, ICatalogListAppService
    {

        private readonly IDailyClickRepository _dailyClickRepository;

        public CatalogListAppService(IDailyClickRepository dailyClickRepository)
        {
            _dailyClickRepository = dailyClickRepository; 
        }

        public virtual async Task<PagedResultDto<DailyClickWithNavigationPropertiesDto>> GetTrendingListAsync(string period)
        {
            GetDailyClicksInput input = new GetDailyClicksInput();
            switch(period)
            {
                case "daily":
                    {
                        input.lastModificationTimeMin = DateTime.Now.Date;
                        input.lastModificationTimeMax = DateTime.Now;
                        break;
                    }
                case "weekly":
                    {
                        input.lastModificationTimeMin = DateTime.Now.AddDays(-7);
                        input.lastModificationTimeMax = DateTime.Now;
                        break;
                    }
             default:
                    {
                        input.lastModificationTimeMin = DateTime.Now.AddDays(-30);
                        input.lastModificationTimeMax = DateTime.Now;
                        break;
                    }
            }

            var totalCount = await _dailyClickRepository.GetCountAsync(input.FilterText, input.clicksMin, input.clicksMax, input.lastModificationTimeMin, input.lastModificationTimeMax, input.BagId);
            var items = await _dailyClickRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.clicksMin, input.clicksMax, input.lastModificationTimeMin, input.lastModificationTimeMax, input.BagId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DailyClickWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DailyClickWithNavigationProperties>, List<DailyClickWithNavigationPropertiesDto>>(items)
            };
        }
    }
     
}
