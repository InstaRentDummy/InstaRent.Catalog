using InstaRent.Catalog.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog.TotalClicks
{
    public interface ITotalClicksAppService : IApplicationService
    {
        Task<PagedResultDto<TotalClickDto>> GetListAsync(GetTotalClicksInput input);

        Task<TotalClickWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<TotalClickDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetBagLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<TotalClickDto> CreateAsync(TotalClickCreateDto input);

        Task<TotalClickDto> UpdateAsync(Guid id, TotalClickUpdateDto input);

        Task<TotalClickDto> IncreaseAsync(Guid bag_id);
    }
}