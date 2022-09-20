using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferencesDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IUserPreferenceRepository _userPreferenceRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UserPreferencesDataSeedContributor(IUserPreferenceRepository userPreferenceRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _userPreferenceRepository = userPreferenceRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _userPreferenceRepository.InsertAsync(new UserPreference
            (
                id: Guid.Parse("6d2bffa7-5afe-44bb-91ca-464daa3ff5ea"),
                userId: "renter12@gmail.com",
                tags: new List<Tag>() { new Tag("Tote", 10) },
                avgRating: 10,
                totalNumofRating: 100
            )); ;

            await _userPreferenceRepository.InsertAsync(new UserPreference
            (
                 id: Guid.Parse("8a537d6a-3142-49fd-86b8-5230cfcb4d62"),
                userId: "renter34@gmail.com",
                 tags: new List<Tag>() { new Tag("Wallet", 5) },
                 avgRating: 5,
                totalNumofRating: 500
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}