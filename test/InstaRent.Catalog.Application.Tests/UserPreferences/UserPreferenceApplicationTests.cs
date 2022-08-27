using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferencesAppServiceTests : CatalogApplicationTestBase
    {
        private readonly IUserPreferencesAppService _userPreferencesAppService;
        private readonly IRepository<UserPreference, long> _userPreferenceRepository;

        public UserPreferencesAppServiceTests()
        {
            _userPreferencesAppService = GetRequiredService<IUserPreferencesAppService>();
            _userPreferenceRepository = GetRequiredService<IRepository<UserPreference, long>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _userPreferencesAppService.GetListAsync(new GetUserPreferencesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == 1).ShouldBe(true);
            result.Items.Any(x => x.Id == 2).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _userPreferencesAppService.GetAsync(1);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new UserPreferenceCreateDto
            {
                UserId = "296fd23b785042369a4652a0e580894ff50276687b904605bc21d7015f5bf1faa24065616d5a44529fb3d2183da0843a9b28946ec3564ed3b5d88ecda3f9a@d443b4ab3ab24a7b98877b737922c8ac4e0d42df274e45329d03124065a562e5fbd482ee01bf41c9843dc824c2d4a5270bfdcc50bd8447ea91ff609038d88.com",
                Tags = new System.Collections.Generic.List<ITag> { new TagDto("a161a978c6a94053a8868209faa511bead844c2f7327484cbdc91e632", 0) }
            };

            // Act
            var serviceResult = await _userPreferencesAppService.CreateAsync(input);

            // Assert
            var result = await _userPreferenceRepository.FindAsync(c => c.UserId == serviceResult.UserId);

            result.ShouldNotBe(null);
            result.UserId.ShouldBe("296fd23b785042369a4652a0e580894ff50276687b904605bc21d7015f5bf1faa24065616d5a44529fb3d2183da0843a9b28946ec3564ed3b5d88ecda3f9a@d443b4ab3ab24a7b98877b737922c8ac4e0d42df274e45329d03124065a562e5fbd482ee01bf41c9843dc824c2d4a5270bfdcc50bd8447ea91ff609038d88.com");
            result.Tags[0].tagname.ShouldBe("a161a978c6a94053a8868209faa511bead844c2f7327484cbdc91e632");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new UserPreferenceUpdateDto()
            {
                UserId = "d9909fcaee1a4fc482cc411cc118b9cac405e44c6d264188a281852bd0f84471d6d584e38a4b4c7a9f1c44e1e2a65e638e7634df05af44f7a3a368f680a84@989b3001a84f4792a2cb91d9ac91036afab768fe53d94e5b819860912494be5537e18320d222456d813565ee300aa269e44c1421527c4c038336307b095c4.com",
                Tags = new System.Collections.Generic.List<ITag> { new TagDto("75a7d867aaca438d85f07a3ad4d59941b5c500d5c4d", 0) }
            };

            // Act
            var serviceResult = await _userPreferencesAppService.UpdateAsync(1, input);

            // Assert
            var result = await _userPreferenceRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.UserId.ShouldBe("d9909fcaee1a4fc482cc411cc118b9cac405e44c6d264188a281852bd0f84471d6d584e38a4b4c7a9f1c44e1e2a65e638e7634df05af44f7a3a368f680a84@989b3001a84f4792a2cb91d9ac91036afab768fe53d94e5b819860912494be5537e18320d222456d813565ee300aa269e44c1421527c4c038336307b095c4.com");
            result.Tags[0].tagname.ShouldBe("75a7d867aaca438d85f07a3ad4d59941b5c500d5c4d");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _userPreferencesAppService.DeleteAsync(1);

            // Assert
            var result = await _userPreferenceRepository.FindAsync(c => c.Id == 1);

            result.ShouldBeNull();
        }
    }
}