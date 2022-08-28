using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClick : Entity<Guid>, IHasConcurrencyStamp
    {
        public virtual long clicks { get; set; }
        public Guid? BagId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public string ConcurrencyStamp { get; set; }

        public DailyClick()
        {

        }

        public DailyClick(Guid id, Guid? bagId, long clicks, DateTime? lastModificationTime)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            this.clicks = clicks;
            BagId = bagId;
            LastModificationTime = lastModificationTime;
        }

    }
}