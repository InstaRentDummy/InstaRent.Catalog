using InstaRent.Catalog.Bags;
using System.Collections.Generic;

namespace InstaRent.Catalog.UserPreferences
{
    public class UserPreferenceWithNavigationProperties
    {
        public string Tag { get; set; }
        public List<Bag> Bags { get; set; }
    }
}
