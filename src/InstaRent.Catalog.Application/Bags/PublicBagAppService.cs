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
     
     
        public class PublicBagAppService : ApplicationService, IPublicBagsAppService
        {
            private readonly IBagRepository _bagRepository; 

            public PublicBagAppService(IBagRepository bagRepository)
            {
                _bagRepository = bagRepository; 
            }

            public virtual async Task<ListResultDto<BagDto>> GetListAsync(GetBagsInput input)
            {
                var totalCount = await _bagRepository.GetCountAsync(input.FilterText, input.bag_name, input.description, input.image_urls, input.rental_start_dateMin, input.rental_start_dateMax, input.rental_end_dateMin, input.rental_end_dateMax, input.priceMin,input.priceMax, input.tags, input.status, input.renter_id, input.isdeleted);
                var items = await _bagRepository.GetListAsync(input.FilterText, input.bag_name, input.description, input.image_urls, input.rental_start_dateMin, input.rental_start_dateMax, input.rental_end_dateMin, input.rental_end_dateMax, input.priceMin, input.priceMax, input.tags, input.status, input.renter_id, input.isdeleted, input.Sorting, input.MaxResultCount, input.SkipCount);

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
         
            
        }
    }
 
