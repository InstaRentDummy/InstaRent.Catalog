using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceUpdateDto : IHasConcurrencyStamp
    {
        [EmailAddress]
        [StringLength(UserPreferenceConsts.UserIdMaxLength)]
        public string UserId { get; set; }
        public List<ITag> Tags { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}