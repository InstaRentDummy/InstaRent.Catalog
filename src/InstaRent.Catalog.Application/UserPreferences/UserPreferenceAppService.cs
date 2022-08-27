using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace InstaRent.Catalog.UserPreferences
{

    public class UserPreferencesAppService : ApplicationService, IUserPreferencesAppService
    {
        private readonly IUserPreferenceRepository _userPreferenceRepository;
        private readonly UserPreferenceManager _userPreferenceManager;

        public UserPreferencesAppService(IUserPreferenceRepository userPreferenceRepository, UserPreferenceManager userPreferenceManager)
        {
            _userPreferenceRepository = userPreferenceRepository;
            _userPreferenceManager = userPreferenceManager;
        }

        public virtual async Task<PagedResultDto<UserPreferenceDto>> GetListAsync(GetUserPreferencesInput input)
        {
            var inputTag = string.Empty;
            if (input.Tags != null)
                inputTag = input.Tags[0].tagname;

            var totalCount = await _userPreferenceRepository.GetCountAsync(input.FilterText, input.UserId, inputTag);
            var items = await _userPreferenceRepository.GetListAsync(input.FilterText, input.UserId, inputTag, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<UserPreferenceDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<UserPreference>, List<UserPreferenceDto>>(items)
            };
        }

        public virtual async Task<UserPreferenceDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<UserPreference, UserPreferenceDto>(await _userPreferenceRepository.GetAsync(id));
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _userPreferenceRepository.DeleteAsync(id);
        }

        public virtual async Task<UserPreferenceDto> CreateAsync(UserPreferenceCreateDto input)
        {

            var userPreference = await _userPreferenceManager.CreateAsync(
            input.UserId, input.Tags
            );

            return ObjectMapper.Map<UserPreference, UserPreferenceDto>(userPreference);
        }


        public virtual async Task<UserPreferenceDto> UpdateAsync(Guid id, UserPreferenceUpdateDto input)
        {

            var userPreference = await _userPreferenceManager.UpdateAsync(
            id,
            input.UserId, input.Tags, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<UserPreference, UserPreferenceDto>(userPreference);
        }
    }
}