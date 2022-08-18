using InstaRent.Catalog.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog.DailyClicks
{
    public interface IDailyClicksAppService : IApplicationService
    {
        Task<PagedResultDto<DailyClickWithNavigationPropertiesDto>> GetListAsync(GetDailyClicksInput input);

        Task<DailyClickWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(long id);

        Task<DailyClickDto> GetAsync(long id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetBagLookupAsync(LookupRequestDto input);

        Task DeleteAsync(long id);

        Task<DailyClickDto> CreateAsync(DailyClickCreateDto input);

        Task<DailyClickDto> UpdateAsync(long id, DailyClickUpdateDto input);
    }
}