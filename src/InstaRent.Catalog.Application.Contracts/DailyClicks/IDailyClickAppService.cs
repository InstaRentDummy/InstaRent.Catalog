using InstaRent.Catalog.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog.DailyClicks
{
    public interface IDailyClicksAppService : IApplicationService
    {
        Task<PagedResultDto<DailyClickDto>> GetListAsync(GetDailyClicksInput input);

        Task<DailyClickWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<DailyClickDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid?>>> GetBagLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<DailyClickDto> CreateAsync(DailyClickCreateDto input);

        Task<DailyClickDto> UpdateAsync(Guid id, DailyClickUpdateDto input);

        Task<DailyClickDto> IncreaseAsync(Guid bag_id);
    }
}