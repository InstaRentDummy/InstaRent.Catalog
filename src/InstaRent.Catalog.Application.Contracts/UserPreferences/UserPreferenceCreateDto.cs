using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceCreateDto
    {
        [EmailAddress]
        [StringLength(UserPreferenceConsts.UserIdMaxLength)]
        public string UserId { get; set; }
        public string Tags { get; set; }
    }
}