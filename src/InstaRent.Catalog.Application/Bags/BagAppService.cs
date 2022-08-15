using AutoMapper.Internal.Mappers;
using InstaRent.Catalog.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services; 

namespace InstaRent.Catalog.Bags
{
     
     
        public class BagsAppService : ApplicationService, IBagsAppService
        {
            private readonly IBagRepository _bagRepository;
            private readonly BagManager _bagManager;

            public BagsAppService(IBagRepository bagRepository, BagManager bagManager)
            {
                _bagRepository = bagRepository;
                _bagManager = bagManager;
            }

            public virtual async Task<PagedResultDto<BagDto>> GetListAsync(GetBagsInput input)
            {
                var totalCount = await _bagRepository.GetCountAsync(input.FilterText, input.bag_name, input.description, input.image_urls, input.rental_start_dateMin, input.rental_start_dateMax, input.rental_end_dateMin, input.rental_end_dateMax, input.tags, input.status, input.renter_id);
                var items = await _bagRepository.GetListAsync(input.FilterText, input.bag_name, input.description, input.image_urls, input.rental_start_dateMin, input.rental_start_dateMax, input.rental_end_dateMin, input.rental_end_dateMax, input.tags, input.status, input.renter_id, input.Sorting, input.MaxResultCount, input.SkipCount);

                return new PagedResultDto<BagDto>
                {
                    TotalCount = totalCount,
                    Items = ObjectMapper.Map<List<Bag>, List<BagDto>>(items)
                };
            }

            public virtual async Task<BagDto> GetAsync(Guid id)
            {
                return ObjectMapper.Map<Bag, BagDto>(await _bagRepository.GetAsync(id));
            }
         
            public virtual async Task DeleteAsync(Guid id)
            {
                await _bagRepository.DeleteAsync(id);
            }
         
            public virtual async Task<BagDto> CreateAsync(BagCreateDto input)
            {

                var bag = await _bagManager.CreateAsync(
                input.bag_name, input.description, input.image_urls, input.rental_start_date, input.rental_end_date, input.tags, input.status, input.renter_id
                );

                return ObjectMapper.Map<Bag, BagDto>(bag);
            }
         
            public virtual async Task<BagDto> UpdateAsync(Guid id, BagUpdateDto input)
            {

                var bag = await _bagManager.UpdateAsync(
                id,
                input.bag_name, input.description, input.image_urls, input.rental_start_date, input.rental_end_date, input.tags, input.status, input.renter_id, input.ConcurrencyStamp
                );

                return ObjectMapper.Map<Bag, BagDto>(bag);
            }
        }
    }
 
