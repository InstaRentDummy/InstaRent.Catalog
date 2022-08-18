using System;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.TotalClicks
{
    public class TotalClickBag : Entity
    {

        public Guid TotalClickId { get; protected set; }

        public Guid BagId { get; protected set; }

        private TotalClickBag()
        {

        }

        public TotalClickBag(Guid totalClickId, Guid bagId)
        {
            TotalClickId = totalClickId;
            BagId = bagId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    TotalClickId,
                    BagId
                };
        }
    }
}