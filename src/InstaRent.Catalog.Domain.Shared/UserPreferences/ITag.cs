using System;
using System.Collections.Generic;
using System.Text;

namespace InstaRent.Catalog.UserPreferences
{
    public interface ITag
    {
        string tagname { get; set; }
        int weightage { get; set; }
    }
}
