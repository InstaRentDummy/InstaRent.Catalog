using InstaRent.Catalog.Bags;
using System.Collections.Generic;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceWithNavigationPropertiesDto
    {
        public string Tag { get; set; }
        public List<BagDto> Bags { get; set; }
    }
}
