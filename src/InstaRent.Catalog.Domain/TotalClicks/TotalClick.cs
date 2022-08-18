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

namespace InstaRent.Catalog.TotalClicks
{
    public class TotalClick : Entity<Guid>, IHasConcurrencyStamp
    {
        public virtual long clicks { get; set; }
        public Guid? BagId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public TotalClick()
        {

        }

        public TotalClick(Guid id, Guid? bagId, long clicks)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            clicks = clicks;
            BagId = bagId;
        }

    }
}