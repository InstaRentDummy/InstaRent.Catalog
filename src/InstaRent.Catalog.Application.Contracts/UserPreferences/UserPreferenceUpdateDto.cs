using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceUpdateDto : IHasConcurrencyStamp
    {
        [EmailAddress]
        [StringLength(UserPreferenceConsts.UserIdMaxLength)]
        [JsonPropertyName("userID")]
        public string UserId { get; set; }

        [JsonPropertyName("tags")]
        public List<TagDto> Tags { get; set; }

        [JsonPropertyName("avg_rating")]
        public double? AvgRating { get; set; }

        [JsonPropertyName("total_num_of_rating")]
        public double? TotalNumofRating { get; set; }
        [JsonIgnore]
        public string ConcurrencyStamp { get; set; }
    }
}