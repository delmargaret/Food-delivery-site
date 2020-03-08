namespace EzhaBy.Entities
{
    public class CafeDescription
    {
        public string CafeId { get; set; }
        public Cafe Cafe { get; set; }
        public string DescriptionKeywordId { get; set; }
        public DescriptionKeyword Keyword { get; set; }
    }
}
