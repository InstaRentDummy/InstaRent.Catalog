using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClickDto : EntityDto<long>, IHasConcurrencyStamp
    {
        public long clicks { get; set; }
        public Guid? BagId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}