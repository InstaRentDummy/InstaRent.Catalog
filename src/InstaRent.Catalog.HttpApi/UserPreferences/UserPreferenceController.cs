using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using InstaRent.Catalog.UserPreferences;

namespace InstaRent.Catalog.UserPreferences
{
    [RemoteService(Name = "Catalog")]
    [Area("catalog")]
    [ControllerName("UserPreference")]
    [Route("api/catalog/user-preferences")]
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
        public virtual Task<UserPreferenceDto> GetAsync(long id)
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
        public virtual Task<UserPreferenceDto> UpdateAsync(long id, UserPreferenceUpdateDto input)
        {
            return _userPreferencesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(long id)
        {
            return _userPreferencesAppService.DeleteAsync(id);
        }
    }
}