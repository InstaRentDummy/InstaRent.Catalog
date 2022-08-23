using Volo.Abp.Application.Dtos;
using System;

namespace InstaRent.Catalog.DailyClicks
{
    public class GetDailyClicksInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public long? clicksMin { get; set; }
        public long? clicksMax { get; set; }
        public Guid? BagId { get; set; }
        public DateTime? lastModificationTimeMin { get; set; }
        public DateTime? lastModificationTimeMax { get; set; }

        public GetDailyClicksInput()
        {

        }
    }
}