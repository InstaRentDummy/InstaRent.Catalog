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
        private readonly IRepository<DailyClick, long> _dailyClickRepository;

        public DailyClicksAppServiceTests()
        {
            _dailyClicksAppService = GetRequiredService<IDailyClicksAppService>();
            _dailyClickRepository = GetRequiredService<IRepository<DailyClick, long>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _dailyClicksAppService.GetListAsync(new GetDailyClicksInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.DailyClick.Id == 1).ShouldBe(true);
            result.Items.Any(x => x.DailyClick.Id == 2).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _dailyClicksAppService.GetAsync(1);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DailyClickCreateDto
            {
                clicks = 1807566977
            };

            // Act
            var serviceResult = await _dailyClicksAppService.CreateAsync(input);

            // Assert
            var result = await _dailyClickRepository.FindAsync(c => c.clicks == serviceResult.clicks);

            result.ShouldNotBe(null);
            result.clicks.ShouldBe(1807566977);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DailyClickUpdateDto()
            {
                clicks = 69636145
            };

            // Act
            var serviceResult = await _dailyClicksAppService.UpdateAsync(1, input);

            // Assert
            var result = await _dailyClickRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.clicks.ShouldBe(69636145);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _dailyClicksAppService.DeleteAsync(1);

            // Assert
            var result = await _dailyClickRepository.FindAsync(c => c.Id == 1);

            result.ShouldBeNull();
        }
    }
}