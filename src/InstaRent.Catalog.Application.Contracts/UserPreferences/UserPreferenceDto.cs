using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;


namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceDto : AuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string UserId { get; set; }
        public List<ITag> Tags { get; set; }

        public string ConcurrencyStamp { get; set; }
    }

    public class TagDto : ITag
    {
        public string tagname { get; set; }
        public int weightage { get; set; }

        public TagDto()
        {

        }

        public TagDto(string tagname, int weightage)
        {
            this.tagname = tagname;
            this.weightage = weightage;
        }
    }

}