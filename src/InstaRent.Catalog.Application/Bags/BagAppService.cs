using InstaRent.Catalog.DailyClicks;
using InstaRent.Catalog.TotalClicks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog.Bags
{


    public class BagsAppService : ApplicationService, IBagsAppService
    {
        private readonly IBagRepository _bagRepository;
        private readonly BagManager _bagManager;
        private readonly DailyClickManager _dailyClickManager;
        private readonly TotalClickManager _totalClickManager;

        public BagsAppService(IBagRepository bagRepository, BagManager bagManager,
             DailyClickManager dailyClickManager, TotalClickManager totalClickManager)
        {
            _bagRepository = bagRepository;
            _bagManager = bagManager;
            _dailyClickManager = dailyClickManager;
            _totalClickManager = totalClickManager;
        }


        public virtual async Task<PagedResultDto<BagDto>> GetListAsync(GetBagsInput input)
        {
            var totalCount = await _bagRepository.GetCountAsync(input.FilterText, input.bag_name, input.description, input.image_urls
                , input.rental_start_dateMin, input.rental_start_dateMax, input.rental_end_dateMin, input.rental_end_dateMax
                , input.priceMin, input.priceMax, input.tags, input.status, input.renter_id
                , input.AvgRatingMin, input.AvgRatingMax, input.TotalRatingMin, input.TotalRatingMax, input.TotalNumofRatingMin, input.TotalNumofRatingMax
                , input.isdeleted, input.creation_timeMin, input.creation_timeMax);
            var items = await _bagRepository.GetListAsync(input.FilterText, input.bag_name, input.description, input.image_urls
                , input.rental_start_dateMin, input.rental_start_dateMax, input.rental_end_dateMin, input.rental_end_dateMax
                , input.priceMin, input.priceMax, input.tags, input.status, input.renter_id
                , input.AvgRatingMin, input.AvgRatingMax, input.TotalRatingMin, input.TotalRatingMax, input.TotalNumofRatingMin, input.TotalNumofRatingMax
                , input.isdeleted, input.creation_timeMin, input.creation_timeMax, input.Sorting, input.MaxResultCount, input.SkipCount);

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
            await _bagManager.DeleteAsync(id);
        }

        public virtual async Task<BagDto> CreateAsync(BagCreateDto input)
        {

            var bag = await _bagManager.CreateAsync(
            input.bag_name, input.description, input.image_urls, input.rental_start_date, input.rental_end_date, input.price, input.tags, input.status, input.renter_id
            , input.AvgRating, input.TotalRating, input.TotalNumofRating);

            await _dailyClickManager.IncreaseAsync(bag.Id);
            await _totalClickManager.IncreaseAsync(bag.Id);

            return ObjectMapper.Map<Bag, BagDto>(bag);
        }

        public virtual async Task<BagDto> UpdateAsync(Guid id, BagUpdateDto input)
        {

            var bag = await _bagManager.UpdateAsync(
            id,
            input.bag_name, input.description, input.image_urls, input.rental_start_date, input.rental_end_date, input.price, input.tags, input.status, input.renter_id
            , input.AvgRating, input.TotalRating, input.TotalNumofRating, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Bag, BagDto>(bag);
        }

        public virtual async Task<BagDto> RateAsync(BagRatingDto input)
        {
            var bag = await _bagManager.RateAsync(input.BagId, input.Rating);
            return ObjectMapper.Map<Bag, BagDto>(bag);


        }
    }
}

