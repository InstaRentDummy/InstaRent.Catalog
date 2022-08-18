using InstaRent.Catalog.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using InstaRent.Catalog.TotalClicks;

namespace InstaRent.Catalog.TotalClicks
{
    [RemoteService(Name = "Catalog")]
    [Area("catalog")]
    [ControllerName("TotalClick")]
    [Route("api/catalog/total-clicks")]
    public class TotalClickController : AbpController, ITotalClicksAppService
    {
        private readonly ITotalClicksAppService _totalClicksAppService;

        public TotalClickController(ITotalClicksAppService totalClicksAppService)
        {
            _totalClicksAppService = totalClicksAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<TotalClickWithNavigationPropertiesDto>> GetListAsync(GetTotalClicksInput input)
        {
            return _totalClicksAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<TotalClickWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _totalClicksAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<TotalClickDto> GetAsync(Guid id)
        {
            return _totalClicksAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("bag-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetBagLookupAsync(LookupRequestDto input)
        {
            return _totalClicksAppService.GetBagLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<TotalClickDto> CreateAsync(TotalClickCreateDto input)
        {
            return _totalClicksAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TotalClickDto> UpdateAsync(Guid id, TotalClickUpdateDto input)
        {
            return _totalClicksAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _totalClicksAppService.DeleteAsync(id);
        }
    }
}