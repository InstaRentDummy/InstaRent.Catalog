using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.Bags
{
    public class BagDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string bag_name { get; set; }
        public string description { get; set; }
        public List<string> image_urls { get; set; }
        public DateTime rental_start_date { get; set; }
        public DateTime rental_end_date { get; set; }
        public List<string> tags { get; set; }
        public string status { get; set; }
        public string renter_id { get; set; }
        [JsonIgnore]
        public string ConcurrencyStamp { get; set; }
    }
}