using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClickCreateDto
    {
        public long clicks { get; set; }
        public Guid? BagId { get; set; }
    }
}