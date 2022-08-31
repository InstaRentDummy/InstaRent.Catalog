using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;
using System.Text.Json.Serialization;

namespace InstaRent.Catalog.TotalClicks
{
    public class TotalClickUpdateDto : IHasConcurrencyStamp
    {
        public long clicks { get; set; }

        [JsonPropertyName("bag_id")]
        public Guid? BagId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}