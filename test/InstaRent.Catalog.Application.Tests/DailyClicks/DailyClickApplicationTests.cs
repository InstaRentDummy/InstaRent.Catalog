using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClicksAppServiceTests : CatalogApplicationTestBase
    {
        private readonly IDailyClicksAppService _dailyClicksAppService;
        private readonly IRepository<DailyClick, Guid> _dailyClickRepository;

        public DailyClicksAppServiceTests()
        {
            _dailyClicksAppService = GetRequiredService<IDailyClicksAppService>();
            _dailyClickRepository = GetRequiredService<IRepository<DailyClick, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _dailyClicksAppService.GetListAsync(new GetDailyClicksInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("753aee59-d0a4-44f4-bbcc-8e10f452e347")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _dailyClicksAppService.GetAsync(Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DailyClickCreateDto
            {
                clicks = 1807
            };

            // Act
            var serviceResult = await _dailyClicksAppService.CreateAsync(input);

            // Assert
            var result = await _dailyClickRepository.FindAsync(c => c.clicks == serviceResult.clicks);

            result.ShouldNotBe(null);
            result.clicks.ShouldBe(1807);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DailyClickUpdateDto()
            {
                clicks = 6963
            };

            // Act
            var serviceResult = await _dailyClicksAppService.UpdateAsync(Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8"), input);

            // Assert
            var result = await _dailyClickRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.clicks.ShouldBe(6963);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _dailyClicksAppService.DeleteAsync(Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8"));

            // Assert
            var result = await _dailyClickRepository.FindAsync(c => c.Id == Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8"));

            result.ShouldBeNull();
        }
    }
}