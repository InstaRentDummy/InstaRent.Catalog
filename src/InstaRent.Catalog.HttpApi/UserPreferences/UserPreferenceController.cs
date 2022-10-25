using InstaRent.Catalog.TotalClicks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace InstaRent.Catalog.UserPreferences
{
    [RemoteService(Name = "Catalog")]
    [Area("catalog")]
    [ControllerName("UserPreference")]
    [Route("api/catalog/userpreferences")]
    public class UserPreferenceController : AbpController, IUserPreferencesAppService
    {
        private readonly IUserPreferencesAppService _userPreferencesAppService;

        public UserPreferenceController(IUserPreferencesAppService userPreferencesAppService)
        {
            _userPreferencesAppService = userPreferencesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<UserPreferenceDto>> GetListAsync(GetUserPreferencesInput input)
        {
            return _userPreferencesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<UserPreferenceDto> GetAsync(Guid id)
        {
            return _userPreferencesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<UserPreferenceDto> CreateAsync(UserPreferenceCreateDto input)
        {
            return _userPreferencesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<UserPreferenceDto> UpdateAsync(Guid id, UserPreferenceUpdateDto input)
        {
            return _userPreferencesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _userPreferencesAppService.DeleteAsync(id);
        }

        [HttpPost]
        [Route("updatetag")]
        public virtual Task<UserPreferenceDto> UpdateSearchTagAsync(UserPreferenceTagUpdateDto input)
        {
            return _userPreferencesAppService.UpdateSearchTagAsync(input);
        }
    }
}