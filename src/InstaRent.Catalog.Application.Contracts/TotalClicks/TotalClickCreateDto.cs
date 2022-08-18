using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace InstaRent.Catalog.TotalClicks
{
    public class TotalClickCreateDto
    {
        public long clicks { get; set; }
        public Guid? BagId { get; set; }
    }
}