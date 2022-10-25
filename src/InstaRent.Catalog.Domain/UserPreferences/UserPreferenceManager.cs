using InstaRent.Catalog.Bags;
using InstaRent.Catalog.TotalClicks;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Services;
using Volo.Abp.ObjectMapping;

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
                GuidGenerator.Create(), userId, tags);

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

        public  async Task<UserPreference> UpdateSearchTagAsync(string userId, List<string> tags, [CanBeNull] string concurrencyStamp = null)
        {
            var queryable = await _userPreferenceRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.UserId == userId);

            var userPreference = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (userPreference == null)
            {
                List<Tag> _tags = new List<Tag>();

                if (tags != null)
                    foreach (var tag in tags)
                    {
                        _tags.Add(new Tag()
                        {
                            tagname = tag,
                            weightage = 1
                        });
                    }


                userPreference = new UserPreference(
                              GuidGenerator.Create(), userId, _tags);

                return await _userPreferenceRepository.InsertAsync(userPreference);
            }
            else
            {
                foreach (var tag in tags)
                {
                    if (userPreference.Tags == null)
                    {
                        userPreference.Tags = new();
                        userPreference.Tags.Add(new Tag() { tagname = tag, weightage = 1 });
                    }
                    else if (userPreference.Tags.Where(x => x.tagname.ToLower() == tag.ToLower()).Any())
                    {
                        var _tag = userPreference.Tags.Where(x => x.tagname.ToLower() == tag.ToLower()).First();
                        _tag.weightage++;
                    }
                    else
                        userPreference.Tags.Add(new Tag() { tagname = tag, weightage = 1 });
                }

                userPreference.SetConcurrencyStampIfNotNull(concurrencyStamp);
                return await _userPreferenceRepository.UpdateAsync(userPreference);
            }
        }
    }
}