using Volo.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaRent.Catalog.UserPreferences
{
    public class GetUserPreferencesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        [JsonPropertyName("userID")]
        public string UserId { get; set; }

        [JsonPropertyName("tags")]
        public List<TagDto> Tags { get; set; }

        [JsonPropertyName("avg_rating_min")]
        public double? AvgRatingMin { get; set; }

        [JsonPropertyName("avg_rating_max")]
        public double? AvgRatingMax { get; set; }

        [JsonPropertyName("total_num_of_rating_min")]
        public double? TotalNumofRatingMin { get; set; }

        [JsonPropertyName("total_num_of_rating_max")]
        public double? TotalNumofRatingMax { get; set; }

        public GetUserPreferencesInput()
        {

        }
    }
}