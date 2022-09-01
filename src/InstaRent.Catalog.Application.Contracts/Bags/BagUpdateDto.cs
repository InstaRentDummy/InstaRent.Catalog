using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.Bags
{
    public class BagUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(BagConsts.bag_nameMaxLength)]
        public string bag_name { get; set; }
        [Required]
        public string description { get; set; }
        public List<string> image_urls { get; set; }

        public DateTime rental_start_date { get; set; }
        public DateTime rental_end_date { get; set; }

        public double price { get; set; }
        public List<string> tags { get; set; }
        [StringLength(BagConsts.statusMaxLength)]
        public string status { get; set; }
        [Required]
        public string renter_id { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}