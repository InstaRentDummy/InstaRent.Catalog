using System;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClickDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public long clicks { get; set; }

        [JsonPropertyName("bag_id")]
        public Guid? BagId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}