using InstaRent.Catalog.DailyClicks;
using InstaRent.Catalog.UserPreferences;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog
{
    public interface ICatalogListAppService : IApplicationService
    {

        Task<PagedResultDto<DailyClickWithNavigationPropertiesDto>> GetTrendingListAsync(string period);
    }
}
