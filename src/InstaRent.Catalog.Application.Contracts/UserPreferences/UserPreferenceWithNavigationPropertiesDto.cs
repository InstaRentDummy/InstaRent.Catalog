using InstaRent.Catalog.Bags;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceWithNavigationPropertiesDto
    {
        [JsonPropertyName("tags")]
        public string Tag { get; set; }

        [JsonPropertyName("bags")]
        public List<BagDto> Bags { get; set; }

    }
}
