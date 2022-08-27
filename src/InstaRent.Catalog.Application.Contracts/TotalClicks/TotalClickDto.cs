using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.TotalClicks
{
    public class TotalClickDto : EntityDto<Guid>, IHasConcurrencyStamp
    {
        public long clicks { get; set; }
        public Guid? BagId { get; set; }
        [JsonIgnore]
        public DateTime? LastModificationTime { get; set; }
        [JsonIgnore]
        public string ConcurrencyStamp { get; set; }
    }
}