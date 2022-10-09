using Volo.Abp.Application.Dtos;
using System;
using System.Text.Json.Serialization;

namespace InstaRent.Catalog.Bags
{
    public class GetBagsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string bag_name { get; set; }
        public string description { get; set; }
        public string image_urls { get; set; }
        public DateTime? rental_start_dateMin { get; set; }
        public DateTime? rental_start_dateMax { get; set; }
        public DateTime? rental_end_dateMin { get; set; }
        public DateTime? rental_end_dateMax { get; set; }
        public DateTime? creation_timeMin { get; set; }
        public DateTime? creation_timeMax { get; set; }
        public double? priceMin { get; set; }
        public double? priceMax { get; set; }
        public string tags { get; set; }
        public string status { get; set; }
        public string renter_id { get; set; }

        public double? AvgRatingMin { get; set; }
        public double? AvgRatingMax { get; set; }
        public double? TotalRatingMin { get; set; }
        public double? TotalRatingMax { get; set; }
        public int? TotalNumofRatingMin { get; set; }
        public int? TotalNumofRatingMax { get; set; }
        public bool? isdeleted { get; set; }

        public GetBagsInput()
        {

        }
    }
}