namespace EzhaBy.Entities
{
    public class CafeDescription
    {
        public string CafeId { get; set; }
        public Cafe Cafe { get; set; }
        public string DescriptionKeyWordId { get; set; }
        public DescriptionKeyWord KeyWord { get; set; }
    }
}
