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
                bag_name = "f7750d1bb7c843978aacb77a23de809ec1ac079b52414989962b97d44e0d8afdd5fd0943d8a541b29f26235a17220b4d9d426e9b3bf644558efde6bc89ca6407cbcc3163baa74ae8a7a1302afc63a95f2b34cfb567a4457588f5b89f4db1952dca8b9b21e8c34c798abc21041f5fc036bcaaffdf7b7a47f5bb0059c6905eaf49",
                description = "200aa04ccad14b32a69de014db89f",
                image_urls = new List<string>() { "0e9c7e56a2204e9eae7b7f209ef26cfc670a96d87c504756b86a994" },
                rental_start_date = new DateTime(2014, 6, 27),
                rental_end_date = new DateTime(2019, 4, 15),
                tags = new List<string>() { "b791f8214f7d459f9acb13f4fa92c2bba" },
                status = "f3fd2f695cae44b999ee30b3ed9b99ceac0b91e8fa4345018a5b686d3c42abde96afd35410874f5f856191d4d7c683f73b7df93398fe45c985b6a300e0ebd425",
                renter_id = "29f988197ef9480fba"
            };

            // Act
            var serviceResult = await _bagsAppService.CreateAsync(input);

            // Assert
            var result = await _bagRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.bag_name.ShouldBe("f7750d1bb7c843978aacb77a23de809ec1ac079b52414989962b97d44e0d8afdd5fd0943d8a541b29f26235a17220b4d9d426e9b3bf644558efde6bc89ca6407cbcc3163baa74ae8a7a1302afc63a95f2b34cfb567a4457588f5b89f4db1952dca8b9b21e8c34c798abc21041f5fc036bcaaffdf7b7a47f5bb0059c6905eaf49");
            result.description.ShouldBe("200aa04ccad14b32a69de014db89f");
            result.image_urls[0].ShouldBe("0e9c7e56a2204e9eae7b7f209ef26cfc670a96d87c504756b86a994");
            result.rental_start_date.ShouldBe(new DateTime(2014, 6, 27));
            result.rental_end_date.ShouldBe(new DateTime(2019, 4, 15));
            result.tags[0].ShouldBe("b791f8214f7d459f9acb13f4fa92c2bba");
            result.status.ShouldBe("f3fd2f695cae44b999ee30b3ed9b99ceac0b91e8fa4345018a5b686d3c42abde96afd35410874f5f856191d4d7c683f73b7df93398fe45c985b6a300e0ebd425");
            result.renter_id.ShouldBe("29f988197ef9480fba");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BagUpdateDto()
            {
                bag_name = "edc4bcc57a4d425283a6831486935121d46039861af24769ab735393b67eaeec21b40fe822d747ccbcc7e072c865dcfeacebdb28830245f693ff9480e6f066016e1632a46f3940139fce90a7461d213904abd89dfb5245b292c59f1f2cc94c79309471b851b7453bae498876e58517019efdcce4090b48df859895f4c50db8c1",
                description = "731f9da3de49433c9cb854c5f63e39fae2c91c8de4fe40eca8ea7064748b07bb81886d58a9f64495af77bc01c0c95e3ba9",
                image_urls = new List<string>() { "df1f863810c04e1ab2b3bad291f77331dbefa8ed33e844a884ff8cc483ba11" },
                rental_start_date = new DateTime(2000, 8, 2),
                rental_end_date = new DateTime(2001, 5, 19),
                tags = new List<string>() { "2239eeb6ef0c4a6abfb6" },
                status = "73b2b002179e4802a04fc8c8dad70ec0e75361db7bea48419f3ed0f255281869f02ffe7664534a2895c0b028ec014b6e7998fa0a1342477ba2bcf0c605d2d681",
                renter_id = "08301f4969984c25a621687"
            };

            // Act
            var serviceResult = await _bagsAppService.UpdateAsync(Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"), input);

            // Assert
            var result = await _bagRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.bag_name.ShouldBe("edc4bcc57a4d425283a6831486935121d46039861af24769ab735393b67eaeec21b40fe822d747ccbcc7e072c865dcfeacebdb28830245f693ff9480e6f066016e1632a46f3940139fce90a7461d213904abd89dfb5245b292c59f1f2cc94c79309471b851b7453bae498876e58517019efdcce4090b48df859895f4c50db8c1");
            result.description.ShouldBe("731f9da3de49433c9cb854c5f63e39fae2c91c8de4fe40eca8ea7064748b07bb81886d58a9f64495af77bc01c0c95e3ba9");
            result.image_urls[0].ShouldBe("df1f863810c04e1ab2b3bad291f77331dbefa8ed33e844a884ff8cc483ba11");
            result.rental_start_date.ShouldBe(new DateTime(2000, 8, 2));
            result.rental_end_date.ShouldBe(new DateTime(2001, 5, 19));
            result.tags[0].ShouldBe("2239eeb6ef0c4a6abfb6");
            result.status.ShouldBe("73b2b002179e4802a04fc8c8dad70ec0e75361db7bea48419f3ed0f255281869f02ffe7664534a2895c0b028ec014b6e7998fa0a1342477ba2bcf0c605d2d681");
            result.renter_id.ShouldBe("08301f4969984c25a621687");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _bagsAppService.DeleteAsync(Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"));

            // Assert
            var result = await _bagRepository.FindAsync(c => c.Id == Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"));

            result.ShouldBeNull();
        }
    }
}