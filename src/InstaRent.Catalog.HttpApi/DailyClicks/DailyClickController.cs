using InstaRent.Catalog.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using InstaRent.Catalog.DailyClicks;

namespace InstaRent.Catalog.DailyClicks
{
    [RemoteService(Name = "Catalog")]
    [Area("catalog")]
    [ControllerName("DailyClick")]
    [Route("api/catalog/daily-clicks")]
    public class DailyClickController : AbpController, IDailyClicksAppService
    {
        private readonly IDailyClicksAppService _dailyClicksAppService;

        public DailyClickController(IDailyClicksAppService dailyClicksAppService)
        {
            _dailyClicksAppService = dailyClicksAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<DailyClickWithNavigationPropertiesDto>> GetListAsync(GetDailyClicksInput input)
        {
            return _dailyClicksAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<DailyClickWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(long id)
        {
            return _dailyClicksAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<DailyClickDto> GetAsync(long id)
        {
            return _dailyClicksAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("bag-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetBagLookupAsync(LookupRequestDto input)
        {
            return _dailyClicksAppService.GetBagLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<DailyClickDto> CreateAsync(DailyClickCreateDto input)
        {
            return _dailyClicksAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<DailyClickDto> UpdateAsync(long id, DailyClickUpdateDto input)
        {
            return _dailyClicksAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(long id)
        {
            return _dailyClicksAppService.DeleteAsync(id);
        }
    }
}