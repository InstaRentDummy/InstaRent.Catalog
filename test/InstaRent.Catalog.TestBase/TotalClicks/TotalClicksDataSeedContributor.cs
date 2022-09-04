 
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow; 

namespace InstaRent.Catalog.TotalClicks
{
    public class TotalClicksDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly ITotalClickRepository _totalClickRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager; 

        public TotalClicksDataSeedContributor(ITotalClickRepository totalClickRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _totalClickRepository = totalClickRepository;
            _unitOfWorkManager = unitOfWorkManager; 
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

          

            await _totalClickRepository.InsertAsync(new TotalClick
            (
                id: Guid.Parse("12069481-5215-46cc-b5a8-05e03014b6d8"),
                clicks: 717524104,
                bagId: null,
                lastModificationTime: DateTime.Now 
            ));

            await _totalClickRepository.InsertAsync(new TotalClick
            (
                id: Guid.Parse("753aee59-d0a4-44f4-bbcc-8e10f452e347"),
                clicks: 307181189,
                bagId: null,
                lastModificationTime: DateTime.Now
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}