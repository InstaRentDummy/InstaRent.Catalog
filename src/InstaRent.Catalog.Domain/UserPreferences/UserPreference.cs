using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreference : AuditedEntity<Guid>, IHasConcurrencyStamp
    {
        [CanBeNull]
        public virtual string UserId { get; set; }

        [CanBeNull]
        public virtual string[] Tags { get; set; }

        public string ConcurrencyStamp { get; set; }

        public UserPreference()
        {

        }

        public UserPreference(Guid id, string userId, string[] tags)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Check.Length(userId, nameof(userId), UserPreferenceConsts.UserIdMaxLength, 0);
            Id = id;
            UserId = userId;
            Tags = tags;
        }

    }
}