using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog.Bags
{
    public interface IPublicBagsAppService : IApplicationService
    {
        Task<ListResultDto<BagDto>> GetListAsync(GetBagsInput input);

        Task<BagDto> GetAsync(Guid id); 
    }
}