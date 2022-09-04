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
        private readonly IRepository<UserPreference, Guid> _userPreferenceRepository;

        public UserPreferencesAppServiceTests()
        {
            _userPreferencesAppService = GetRequiredService<IUserPreferencesAppService>();
            _userPreferenceRepository = GetRequiredService<IRepository<UserPreference, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _userPreferencesAppService.GetListAsync(new GetUserPreferencesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("6d2bffa7-5afe-44bb-91ca-464daa3ff5ea")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("8a537d6a-3142-49fd-86b8-5230cfcb4d62")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _userPreferencesAppService.GetAsync(Guid.Parse("6d2bffa7-5afe-44bb-91ca-464daa3ff5ea"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("6d2bffa7-5afe-44bb-91ca-464daa3ff5ea"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new UserPreferenceCreateDto
            {
                UserId = "renter45@gmail.com",
                Tags = new System.Collections.Generic.List<TagDto> { new TagDto("Crossbody", 10) }
            };

            // Act
            var serviceResult = await _userPreferencesAppService.CreateAsync(input);

            // Assert
            var result = await _userPreferenceRepository.FindAsync(c => c.UserId == serviceResult.UserId);

            result.ShouldNotBe(null);
            result.UserId.ShouldBe("renter45@gmail.com");
            result.Tags[0].tagname.ShouldBe("Crossbody");
            result.Tags[0].weightage.ShouldBe(10);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new UserPreferenceUpdateDto()
            {
                UserId = "renter34@gmail.com",
                Tags = new System.Collections.Generic.List<TagDto> { new TagDto("Tote", 15) }
            };

            // Act
            var serviceResult = await _userPreferencesAppService.UpdateAsync(Guid.Parse("6d2bffa7-5afe-44bb-91ca-464daa3ff5ea"), input);

            // Assert
            var result = await _userPreferenceRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.UserId.ShouldBe("renter34@gmail.com");
            result.Tags[0].tagname.ShouldBe("Tote");
            result.Tags[0].weightage.ShouldBe(15);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _userPreferencesAppService.DeleteAsync(Guid.Parse("6d2bffa7-5afe-44bb-91ca-464daa3ff5ea"));

            // Assert
            var result = await _userPreferenceRepository.FindAsync(c => c.Id == Guid.Parse("6d2bffa7-5afe-44bb-91ca-464daa3ff5ea"));

            result.ShouldBeNull();
        }
    }
}