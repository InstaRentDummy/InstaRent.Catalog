using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using InstaRent.Catalog.DailyClicks;

namespace InstaRent.Catalog.TotalClicks
{
    public class TotalClickManager : DomainService
    {
        private readonly ITotalClickRepository _totalClickRepository;

        public TotalClickManager(ITotalClickRepository totalClickRepository)
        {
            _totalClickRepository = totalClickRepository;
        }

        public async Task<TotalClick> CreateAsync(
        Guid? bagId, long clicks)
        {
            var totalClick = new TotalClick(
             GuidGenerator.Create(),
             bagId, clicks, DateTime.Now 
             );

            return await _totalClickRepository.InsertAsync(totalClick);
        }

        public async Task<TotalClick> UpdateAsync(
            Guid id,
            Guid? bagId, long clicks, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _totalClickRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var totalClick = await AsyncExecuter.FirstOrDefaultAsync(query);

            totalClick.BagId = bagId;
            totalClick.clicks = clicks;
            totalClick.LastModificationTime = DateTime.Now;

            totalClick.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _totalClickRepository.UpdateAsync(totalClick);
        }

        public async Task<TotalClick> IncreaseAsync(
            Guid? bagId
        )
        {
            var queryable = await _totalClickRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.BagId == bagId );

            var totalClick = await AsyncExecuter.FirstOrDefaultAsync(query);

            if (totalClick != null)
            {
                totalClick.BagId = bagId;
                totalClick.clicks = totalClick.clicks + 1;
                totalClick.LastModificationTime = DateTime.Now;

                return await _totalClickRepository.UpdateAsync(totalClick);
            }
            else
            {

                totalClick = new TotalClick(
                               GuidGenerator.Create(),
                               bagId, 1, DateTime.Now
                               );
                return await _totalClickRepository.InsertAsync(totalClick);
            }
        }

    }
}