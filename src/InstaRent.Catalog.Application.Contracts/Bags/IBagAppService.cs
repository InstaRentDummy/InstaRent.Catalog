using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog.Bags
{
    public interface IBagsAppService : IApplicationService
    {
        Task<PagedResultDto<BagDto>> GetListAsync(GetBagsInput input);

        Task<BagDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<BagDto> CreateAsync(BagCreateDto input);

        Task<BagDto> UpdateAsync(Guid id, BagUpdateDto input);
    }
}