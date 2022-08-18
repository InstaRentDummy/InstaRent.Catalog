using System;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClickBag : Entity
    {

        public long DailyClickId { get; protected set; }

        public Guid BagId { get; protected set; }

        private DailyClickBag()
        {

        }

        public DailyClickBag(long dailyClickId, Guid bagId)
        {
            DailyClickId = dailyClickId;
            BagId = bagId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    DailyClickId,
                    BagId
                };
        }
    }
}