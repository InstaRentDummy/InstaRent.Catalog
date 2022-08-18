using InstaRent.Catalog.Bags;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace InstaRent.Catalog.DailyClicks
{
    public class DailyClickWithNavigationPropertiesDto
    {
        public DailyClickDto DailyClick { get; set; }

        public BagDto Bag { get; set; }

    }
}