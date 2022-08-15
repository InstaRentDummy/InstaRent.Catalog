using System;
using System.Collections.Generic;
using System.Text;

namespace InstaRent.Catalog
{
    public static class BagConsts
    {
        private const string DefaultSorting = "{0}bag_name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Bag." : string.Empty);
        }

        public const int bag_nameMaxLength = 256;
        public const int statusMaxLength = 128;
    }
}
