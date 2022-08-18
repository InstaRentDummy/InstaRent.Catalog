using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreference : FullAuditedAggregateRoot<long>
    {
        [CanBeNull]
        public virtual string UserId { get; set; }

        [CanBeNull]
        public virtual string Tags { get; set; }

        public UserPreference()
        {

        }

        public UserPreference(string userId, string tags)
        {

            Check.Length(userId, nameof(userId), UserPreferenceConsts.UserIdMaxLength, 0);
            UserId = userId;
            Tags = tags;
        }

    }
}