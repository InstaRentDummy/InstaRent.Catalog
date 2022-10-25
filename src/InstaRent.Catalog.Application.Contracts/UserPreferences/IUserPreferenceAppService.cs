using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog.UserPreferences
{
    public interface IUserPreferencesAppService : IApplicationService
    {
        Task<PagedResultDto<UserPreferenceDto>> GetListAsync(GetUserPreferencesInput input);

        Task<UserPreferenceDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<UserPreferenceDto> CreateAsync(UserPreferenceCreateDto input);

        Task<UserPreferenceDto> UpdateAsync(Guid id, UserPreferenceUpdateDto input);

        Task<UserPreferenceDto> UpdateSearchTagAsync(UserPreferenceTagUpdateDto input);
    }
}