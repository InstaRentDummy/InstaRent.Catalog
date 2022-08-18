using InstaRent.Catalog.Bags;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace InstaRent.Catalog.TotalClicks
{
    public class TotalClickWithNavigationPropertiesDto
    {
        public TotalClickDto TotalClick { get; set; }

        public BagDto Bag { get; set; }

    }
}