using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceCreateDto
    {
        [EmailAddress]
        [StringLength(UserPreferenceConsts.UserIdMaxLength)]
        public string UserId { get; set; }

        public List<TagDto> Tags { get; set; }
    }
}