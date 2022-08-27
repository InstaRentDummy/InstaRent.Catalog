using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;

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
        string userId, List<Tag> tags)
        {
            var userPreference = new UserPreference(
                GuidGenerator.Create(), userId, tags
             );

            return await _userPreferenceRepository.InsertAsync(userPreference);
        }

        public async Task<UserPreference> UpdateAsync(
            Guid id,
            string userId, List<Tag> tags, [CanBeNull] string concurrencyStamp = null
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