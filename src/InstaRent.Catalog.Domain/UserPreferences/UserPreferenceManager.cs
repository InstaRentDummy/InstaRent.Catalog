using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceManager : DomainService
    {
        private readonly IUserPreferenceRepository _userPreferenceRepository;

        public UserPreferenceManager(IUserPreferenceRepository userPreferenceRepository)
        {
            _userPreferenceRepository = userPreferenceRepository;
        }

        public async Task<UserPreference> CreateAsync(
        string userId, string tags)
        {
            var userPreference = new UserPreference(

             userId, tags
             );

            return await _userPreferenceRepository.InsertAsync(userPreference);
        }

        public async Task<UserPreference> UpdateAsync(
            long id,
            string userId, string tags, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _userPreferenceRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var userPreference = await AsyncExecuter.FirstOrDefaultAsync(query);

            userPreference.UserId = userId;
            userPreference.Tags = tags;

            userPreference.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _userPreferenceRepository.UpdateAsync(userPreference);
        }

    }
}