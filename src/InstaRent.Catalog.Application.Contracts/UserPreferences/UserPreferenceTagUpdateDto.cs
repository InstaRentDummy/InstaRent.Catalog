using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceTagUpdateDto : IHasConcurrencyStamp
    {
        [EmailAddress]
        [StringLength(UserPreferenceConsts.UserIdMaxLength)]
        public string UserId { get; set; }

        public List<string> Tags { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}