namespace InstaRent.Catalog.DailyClicks
{
    public static class DailyClickConsts
    {
        private const string DefaultSorting = "{0}Clicks desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "DailyClick." : string.Empty);
        }

    }
}