using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;
using InstaRent.Catalog.Bags;
using System.Collections.Generic;

namespace InstaRent.Catalog
{
    public class BagsAppServiceTests : CatalogApplicationTestBase
    {
        private readonly IBagsAppService _bagsAppService;
        private readonly IRepository<Bag, Guid> _bagRepository;

        public BagsAppServiceTests()
        {
            _bagsAppService = GetRequiredService<IBagsAppService>();
            _bagRepository = GetRequiredService<IRepository<Bag, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _bagsAppService.GetListAsync(new GetBagsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("edba497f-ec22-4773-bd69-9188fe5e7933")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _bagsAppService.GetAsync(Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BagCreateDto
            {
                bag_name = "New Bag",
                description = "New Bag Description",
                image_urls = new List<string>() { "http://url.image.com/image1" },
                rental_start_date = new DateTime(2022, 6, 27),
                rental_end_date = new DateTime(2022, 9, 15),
                tags = new List<string>() { "Tote" },
                status = "available",
                renter_id = "renter_1@gmail.com"
            };

            // Act
            var serviceResult = await _bagsAppService.CreateAsync(input);

            // Assert
            var result = await _bagRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.bag_name.ShouldBe("New Bag");
            result.description.ShouldBe("New Bag Description");
            result.image_urls[0].ShouldBe("http://url.image.com/image1");
            result.rental_start_date.ShouldBe(new DateTime(2022, 6, 27));
            result.rental_end_date.ShouldBe(new DateTime(2022, 9, 15));
            result.tags[0].ShouldBe("Tote");
            result.status.ShouldBe("available");
            result.renter_id.ShouldBe("renter_1@gmail.com");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BagUpdateDto()
            {
                bag_name = "Gucci Wallet",
                description = "Gucci Wallet updated",
                image_urls= new List<string>() { "http://imagerepo.url.com/image7765_1" , "http://imagerepo.url.com/image7765_2" },
                rental_start_date= new DateTime(2022, 3, 20),
                rental_end_date= new DateTime(2023, 9, 24),
                price = 100.00,
                tags = new List<string>() { "Wallet" },
                status = "available",
                renter_id= "renter_1@gmail.com"
            };

            // Act
            var serviceResult = await _bagsAppService.UpdateAsync(Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"), input);

            // Assert
            var result = await _bagRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.bag_name.ShouldBe("Gucci Wallet");
            result.description.ShouldBe("Gucci Wallet updated");
            result.image_urls[0].ShouldBe("http://imagerepo.url.com/image7765_1");
            result.image_urls[1].ShouldBe("http://imagerepo.url.com/image7765_2");
            result.rental_start_date.ShouldBe(new DateTime(2022, 3, 20));
            result.rental_end_date.ShouldBe(new DateTime(2023, 9, 24));
            result.price.ShouldBe(100.00);
            result.tags[0].ShouldBe("Wallet");
            result.status.ShouldBe("available");
            result.renter_id.ShouldBe("renter_1@gmail.com");
        }
        [Fact]
        public async Task RateAsync()
        {
            // Act
            await _bagsAppService.RateAsync(Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"),4.0);

            // Assert
            var result = await _bagRepository.FindAsync(c => c.Id == Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"));

            result.AvgRating = 4.0;
            result.TotalRating = 4.0;
            result.TotalNumofRating = 1;
             
        }
        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _bagsAppService.DeleteAsync(Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"));

            // Assert
            var result = await _bagRepository.FindAsync(c => c.Id == Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"));

            result.isdeleted.ShouldBeTrue();
        }
    }
}