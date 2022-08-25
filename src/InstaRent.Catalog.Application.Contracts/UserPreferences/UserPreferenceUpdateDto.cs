using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceUpdateDto : IHasConcurrencyStamp
    {
        [EmailAddress]
        [StringLength(UserPreferenceConsts.UserIdMaxLength)]
        public string UserId { get; set; }
        public string[] Tags { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}