namespace InstaRent.Catalog.UserPreferences
{
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
