using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow; 

namespace InstaRent.Catalog.Bags
{
    public class BagsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IBagRepository _bagRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public BagsDataSeedContributor(IBagRepository bagRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _bagRepository = bagRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _bagRepository.InsertAsync(new Bag
            (
                id: Guid.Parse("4a2d4f7e-c8ee-4495-984b-3eda432a7765"),
                bag_name: "Gucci Wallet",
                description: "Gucci Wallet 1",
                image_urls: new List<string>() { "http://imagerepo.url.com/image7765" },
                rental_start_date: new DateTime(2022, 3, 20),
                rental_end_date: new DateTime(2023, 9, 24),
                price: 100.00,
                tags: new List<string>() { "Wallet" },
                status: "available",
                renter_id: "renter_1@gmail.com",
                isdeleted: false
                 
            )); 

            await _bagRepository.InsertAsync(new Bag
            (
                id: Guid.Parse("edba497f-ec22-4773-bd69-9188fe5e7933"),
                bag_name: "Balenciaga",
                description: "Crossbody",
                image_urls: new List<string>() { "http://imagerepo.url.com/image7933" },
                rental_start_date: new DateTime(2022, 7, 11),
                rental_end_date: new DateTime(2023, 8, 26),
                price: 500.00,
                tags: new List<string>() { "Crossbody" },
                status: "available",
                renter_id: "renter_2@gmail.com",
                isdeleted: false 
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}