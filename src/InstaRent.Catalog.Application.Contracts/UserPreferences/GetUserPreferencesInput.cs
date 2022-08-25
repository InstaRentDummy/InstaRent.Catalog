using Volo.Abp.Application.Dtos;

namespace InstaRent.Catalog.UserPreferences
{
    public class GetUserPreferencesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }
        public string UserId { get; set; }
        public string Tags { get; set; }

        public GetUserPreferencesInput()
        {

        }
    }
}