using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace InstaRent.Catalog.Bags
{
    public class BagCreateDto
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
        public double? AvgRating { get; set; }
        public double? TotalRating  { get; set; }
        public int? TotalNumofRating { get; set; }
    }
}