using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClickCreateDto
    {
    
        public long clicks { get; set; }

        [JsonPropertyName("bag_id")]
        public Guid? BagId { get; set; }
    }
}