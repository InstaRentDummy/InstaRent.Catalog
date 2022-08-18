using InstaRent.Catalog.Bags;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

using Volo.Abp;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClick : Entity<long>, IHasConcurrencyStamp
    {
        public virtual long clicks { get; set; }
        public Guid? BagId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public DailyClick()
        {

        }

        public DailyClick(Guid? bagId, long clicks)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");

            clicks = clicks;
            BagId = bagId;
        }

    }
}