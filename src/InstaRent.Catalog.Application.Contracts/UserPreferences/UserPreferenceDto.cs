using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceDto : AuditedEntityDto<long>, IHasConcurrencyStamp
    {
        public string UserId { get; set; }
        public string Tags { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}