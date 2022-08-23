using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClick : AuditedEntity<long>, IHasConcurrencyStamp
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

            this.clicks = clicks;
            BagId = bagId;
        }

    }
}