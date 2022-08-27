using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;


namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string UserId { get; set; }
        public List<TagDto> Tags { get; set; }

        public string ConcurrencyStamp { get; set; }
    }

}