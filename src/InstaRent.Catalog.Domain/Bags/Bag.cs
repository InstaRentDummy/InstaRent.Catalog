using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace InstaRent.Catalog.Bags
{
    public class Bag : AuditedEntity<Guid>, IHasConcurrencyStamp
    {
        [NotNull]
        public virtual string bag_name { get; set; }

        [NotNull]
        public virtual string description { get; set; }

        [CanBeNull]
        public virtual List<string> image_urls { get; set; }

        public virtual DateTime rental_start_date { get; set; }

        public virtual DateTime rental_end_date { get; set; }

        [CanBeNull]
        public virtual List<string> tags { get; set; }

        [CanBeNull]
        public virtual string status { get; set; }

        [NotNull]
        public virtual string renter_id { get; set; }

        public string ConcurrencyStamp { get; set; }

        public Bag()
        {

        }

        public Bag(Guid id, string bag_name, string description, List<string> image_urls, DateTime rental_start_date, DateTime rental_end_date, List<string> tags, string status, string renter_id)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            Check.NotNull(bag_name, nameof(bag_name));
            Check.Length(bag_name, nameof(bag_name), BagConsts.bag_nameMaxLength, 0);
            Check.NotNull(description, nameof(description));
            Check.Length(status, nameof(status), BagConsts.statusMaxLength, 0);
            Check.NotNull(renter_id, nameof(renter_id));
            this.bag_name = bag_name;
            this.description = description;
            this.image_urls = image_urls;
            this.rental_start_date = rental_start_date;
            this.rental_end_date = rental_end_date;
            this.tags = tags;
            this.status = status;
            this.renter_id = renter_id;
        }
    }
}
