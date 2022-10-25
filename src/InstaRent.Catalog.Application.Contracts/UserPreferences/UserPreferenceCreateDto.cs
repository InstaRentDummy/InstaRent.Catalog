using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceCreateDto
    {
        [EmailAddress]
        [StringLength(UserPreferenceConsts.UserIdMaxLength)]
        [JsonPropertyName("userID")]
        public string UserId { get; set; }

        [JsonPropertyName("tags")]
        public List<TagDto> Tags { get; set; }

     
    }
}