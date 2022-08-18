using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog.UserPreferences
{
    public interface IUserPreferencesAppService : IApplicationService
    {
        Task<PagedResultDto<UserPreferenceDto>> GetListAsync(GetUserPreferencesInput input);

        Task<UserPreferenceDto> GetAsync(long id);

        Task DeleteAsync(long id);

        Task<UserPreferenceDto> CreateAsync(UserPreferenceCreateDto input);

        Task<UserPreferenceDto> UpdateAsync(long id, UserPreferenceUpdateDto input);
    }
}