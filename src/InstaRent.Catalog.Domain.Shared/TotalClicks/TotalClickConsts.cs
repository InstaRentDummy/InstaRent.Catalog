namespace InstaRent.Catalog.TotalClicks
{
    public static class TotalClickConsts
    {
        private const string DefaultSorting = "{0}clicks asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "TotalClick." : string.Empty);
        }

    }
}