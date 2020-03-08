namespace EzhaBy.Entities
{
    public class PartnerRequest
    {
        public string Id { get; set; }
        public string CafeName { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public CafeTypes CafeType { get; set; }
    }
}
