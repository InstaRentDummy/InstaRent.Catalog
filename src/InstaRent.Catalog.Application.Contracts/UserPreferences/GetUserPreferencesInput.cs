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
        public string Tags { get; set; }


        public GetUserPreferencesInput()
        {

        }
    }
}