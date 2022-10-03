using InstaRent.Catalog.Bags;
using InstaRent.Catalog.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace InstaRent.Catalog.DailyClicks
{

    public class DailyClicksAppService : ApplicationService, IDailyClicksAppService
    {
        private readonly IDailyClickRepository _dailyClickRepository;
        private readonly DailyClickManager _dailyClickManager;
        private readonly IRepository<Bag, Guid> _bagRepository;

        public DailyClicksAppService(IDailyClickRepository dailyClickRepository, DailyClickManager dailyClickManager, IRepository<Bag, Guid> bagRepository)
        {
            _dailyClickRepository = dailyClickRepository;
            _dailyClickManager = dailyClickManager; _bagRepository = bagRepository;
        }

        public virtual async Task<PagedResultDto<DailyClickDto>> GetListAsync(GetDailyClicksInput input)
        {
            var totalCount = await _dailyClickRepository.GetCountAsync(input.FilterText, input.clicksMin, input.clicksMax, input.lastModificationTimeMin, input.lastModificationTimeMax,  input.BagId);
            var items = await _dailyClickRepository.GetListAsync(input.FilterText, input.clicksMin, input.clicksMax, input.lastModificationTimeMin, input.lastModificationTimeMax, input.BagId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DailyClickDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DailyClick>, List<DailyClickDto>>(items)
            };
        }

        public virtual async Task<DailyClickWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<DailyClickWithNavigationProperties, DailyClickWithNavigationPropertiesDto>
                (await _dailyClickRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<DailyClickDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<DailyClick, DailyClickDto>(await _dailyClickRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid?>>> GetBagLookupAsync(LookupRequestDto input)
        {
            var query = (await _bagRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.bag_name != null &&
                         x.bag_name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Bag>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid?>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Bag>, List<LookupDto<Guid?>>>(lookupData)
            };
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _dailyClickRepository.DeleteAsync(id);
        }

        public virtual async Task<DailyClickDto> CreateAsync(DailyClickCreateDto input)
        {

            var dailyClick = await _dailyClickManager.CreateAsync(
            input.BagId, input.clicks
            );

            return ObjectMapper.Map<DailyClick, DailyClickDto>(dailyClick);
        }

        public virtual async Task<DailyClickDto> UpdateAsync(Guid id, DailyClickUpdateDto input)
        {

            var dailyClick = await _dailyClickManager.UpdateAsync(
            id,
            input.BagId, input.clicks, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<DailyClick, DailyClickDto>(dailyClick);
        }

        public virtual async Task<DailyClickDto> IncreaseAsync(Guid bag_id)
        {
            var dailyClick = await  _dailyClickManager.IncreaseAsync(bag_id);
            return ObjectMapper.Map<DailyClick, DailyClickDto>(dailyClick);
        }
    }
}