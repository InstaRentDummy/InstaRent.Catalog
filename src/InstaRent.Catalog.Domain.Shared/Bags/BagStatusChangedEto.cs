using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace InstaRent.Catalog.Bags
{
    [Serializable]
    public class BagStatusChangedEto : EtoBase
    {
        public Guid Id { get; }

        public string OldStatus { get; set; }

        public string CurrentStatus { get; set; }

        private BagStatusChangedEto()
        {
            //Default constructor is needed for deserialization.
        }

        public BagStatusChangedEto(Guid id, string oldStatus, string currentStatus)
        {
            Id = id;
            OldStatus = oldStatus;
            CurrentStatus = currentStatus;
        }
   
    }
}
