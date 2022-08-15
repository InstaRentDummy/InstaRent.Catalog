using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp;

namespace InstaRent.Catalog.Bags
{
    [RemoteService(Name = "Catalog")]
    [Area("catalog")]
    [ControllerName("Bag")]
    [Route("api/catalog/bags")]
    public class BagController : AbpController, IBagsAppService
    {
        private readonly IBagsAppService _bagsAppService;

        public BagController(IBagsAppService bagsAppService)
        {
            _bagsAppService = bagsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<BagDto>> GetListAsync(GetBagsInput input)
        {
            return _bagsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<BagDto> GetAsync(Guid id)
        {
            return _bagsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<BagDto> CreateAsync(BagCreateDto input)
        {
            return _bagsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<BagDto> UpdateAsync(Guid id, BagUpdateDto input)
        {
            return _bagsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _bagsAppService.DeleteAsync(id);
        }
    }
}
