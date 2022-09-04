using InstaRent.Catalog.Bags;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClicksDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IDailyClickRepository _dailyClickRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly BagsDataSeedContributor _bagsDataSeedContributor;

        public DailyClicksDataSeedContributor(IDailyClickRepository dailyClickRepository, IUnitOfWorkManager unitOfWorkManager, BagsDataSeedContributor bagsDataSeedContributor)
        {
            _dailyClickRepository = dailyClickRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _bagsDataSeedContributor = bagsDataSeedContributor;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _bagsDataSeedContributor.SeedAsync(context);

            await _dailyClickRepository.InsertAsync(new DailyClick
            (
                 id: Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8"),
                clicks: 1000,
                bagId: null,
                lastModificationTime: DateTime.Now
            ));

            await _dailyClickRepository.InsertAsync(new DailyClick
            (
                id: Guid.Parse("753aee59-d0a4-44f4-bbcc-8e10f452e347"),
                clicks: 1500,
                bagId: null,
                lastModificationTime: DateTime.Now

            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}