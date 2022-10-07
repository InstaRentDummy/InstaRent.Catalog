using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace InstaRent.Catalog.UserPreferences
{
    public class GetUserRecommendationInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        [JsonPropertyName("userID")]
        public string UserId { get; set; }

         

        public GetUserRecommendationInput()
        {

        }
    }
}