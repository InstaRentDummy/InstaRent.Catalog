using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaRent.Catalog.Bags
{
    public class BagEto
    {
        public Guid Id { get; set; }

        public string bag_name { get; set; }

        public string description { get; set; }

        public List<string> image_urls { get; set; }

        public DateTime rental_start_date { get; set; }

        public DateTime rental_end_date { get; set; }


        public List<string> tags { get; set; }


        public string status { get; set; }


        public string renter_id { get; set; }
    }
}
