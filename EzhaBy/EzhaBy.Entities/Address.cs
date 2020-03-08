namespace EzhaBy.Entities
{
    public class Address
    {
        public string Id { get; set; }
        public Towns Town { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
    }
}
