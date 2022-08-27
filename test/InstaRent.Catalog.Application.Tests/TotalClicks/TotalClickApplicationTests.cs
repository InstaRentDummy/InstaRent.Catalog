using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace InstaRent.Catalog.TotalClicks
{
    public class TotalClicksAppServiceTests : CatalogApplicationTestBase
    {
        private readonly ITotalClicksAppService _totalClicksAppService;
        private readonly IRepository<TotalClick, Guid> _totalClickRepository;

        public TotalClicksAppServiceTests()
        {
            _totalClicksAppService = GetRequiredService<ITotalClicksAppService>();
            _totalClickRepository = GetRequiredService<IRepository<TotalClick, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _totalClicksAppService.GetListAsync(new GetTotalClicksInput());

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
            var result = await _totalClicksAppService.GetAsync(Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new TotalClickCreateDto
            {
                clicks = 1310701062
            };

            // Act
            var serviceResult = await _totalClicksAppService.CreateAsync(input);

            // Assert
            var result = await _totalClickRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.clicks.ShouldBe(1310701062);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new TotalClickUpdateDto()
            {
                clicks = 1444798950
            };

            // Act
            var serviceResult = await _totalClicksAppService.UpdateAsync(Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8"), input);

            // Assert
            var result = await _totalClickRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.clicks.ShouldBe(1444798950);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _totalClicksAppService.DeleteAsync(Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8"));

            // Assert
            var result = await _totalClickRepository.FindAsync(c => c.Id == Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8"));

            result.ShouldBeNull();
        }
    }
}