using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClickUpdateDto : IHasConcurrencyStamp
    {
        public long clicks { get; set; }
        public Guid? BagId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}