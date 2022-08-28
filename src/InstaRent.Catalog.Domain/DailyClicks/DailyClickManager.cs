using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClickManager : DomainService
    {
        private readonly IDailyClickRepository _dailyClickRepository;

        public DailyClickManager(IDailyClickRepository dailyClickRepository)
        {
            _dailyClickRepository = dailyClickRepository;
        }

        public async Task<DailyClick> CreateAsync(
        Guid? bagId, long clicks)
        {
            var dailyClick = new DailyClick(
             GuidGenerator.Create(),
             bagId, clicks, DateTime.Now
             );

            return await _dailyClickRepository.InsertAsync(dailyClick);
        }

        public async Task<DailyClick> UpdateAsync(
            Guid id,
            Guid? bagId, long clicks, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _dailyClickRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var dailyClick = await AsyncExecuter.FirstOrDefaultAsync(query);

            dailyClick.BagId = bagId;
            dailyClick.clicks = clicks;
            dailyClick.LastModificationTime = DateTime.Now;

            dailyClick.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _dailyClickRepository.UpdateAsync(dailyClick);
        }

    }
}