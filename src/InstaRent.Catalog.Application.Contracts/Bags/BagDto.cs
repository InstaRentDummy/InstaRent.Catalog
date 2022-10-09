using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.Bags
{
    public class BagDto : EntityDto<Guid>, IHasConcurrencyStamp,IHasCreationTime
    {
        public string bag_name { get; set; }
        public string description { get; set; }
        public List<string> image_urls { get; set; }
        public DateTime rental_start_date { get; set; }
        public DateTime rental_end_date { get; set; }

        public double price { get; set; }
        public List<string> tags { get; set; }
        public string status { get; set; }
        public string renter_id { get; set; }
        public double? AvgRating { get; set; }
        public double? TotalRating { get; set; }
        public int? TotalNumofRating { get; set; }
        [JsonIgnore]
        public string ConcurrencyStamp { get; set; }
        [JsonPropertyName("creation_time")]
        public DateTime CreationTime { get; set; }
        [JsonIgnore]
        public DateTime? LastModificationTime { get; set; }
        public bool? isdeleted { get; set; }
    }
}