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

namespace InstaRent.Catalog.TotalClicks
{

    public class TotalClicksAppService : ApplicationService, ITotalClicksAppService
    {
        private readonly ITotalClickRepository _totalClickRepository;
        private readonly TotalClickManager _totalClickManager;
        private readonly IRepository<Bag, Guid> _bagRepository;

        public TotalClicksAppService(ITotalClickRepository totalClickRepository, TotalClickManager totalClickManager, IRepository<Bag, Guid> bagRepository)
        {
            _totalClickRepository = totalClickRepository;
            _totalClickManager = totalClickManager; _bagRepository = bagRepository;
        }

        public virtual async Task<PagedResultDto<TotalClickWithNavigationPropertiesDto>> GetListAsync(GetTotalClicksInput input)
        {
            var totalCount = await _totalClickRepository.GetCountAsync(input.FilterText, input.clicksMin, input.clicksMax, input.BagId);
            var items = await _totalClickRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.clicksMin, input.clicksMax, input.BagId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<TotalClickWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<TotalClickWithNavigationProperties>, List<TotalClickWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<TotalClickWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<TotalClickWithNavigationProperties, TotalClickWithNavigationPropertiesDto>
                (await _totalClickRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<TotalClickDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<TotalClick, TotalClickDto>(await _totalClickRepository.GetAsync(id));
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
            await _totalClickRepository.DeleteAsync(id);
        }

        public virtual async Task<TotalClickDto> CreateAsync(TotalClickCreateDto input)
        {

            var totalClick = await _totalClickManager.CreateAsync(
            input.BagId, input.clicks
            );

            return ObjectMapper.Map<TotalClick, TotalClickDto>(totalClick);
        }

        public virtual async Task<TotalClickDto> UpdateAsync(Guid id, TotalClickUpdateDto input)
        {

            var totalClick = await _totalClickManager.UpdateAsync(
            id,
            input.BagId, input.clicks, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<TotalClick, TotalClickDto>(totalClick);
        }
    }
}