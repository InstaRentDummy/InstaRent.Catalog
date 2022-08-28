using Volo.Abp.Application.Dtos;
using System;
using System.Collections.Generic;

namespace InstaRent.Catalog.UserPreferences
{
    public class GetUserPreferencesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
        public string UserId { get; set; }
        public List<TagDto> Tags { get; set; }

        public GetUserPreferencesInput()
        {

        }
    }
}