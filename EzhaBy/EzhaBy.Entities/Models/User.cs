namespace EzhaBy.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public UserRoles UserRole { get; set; }
        public string AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
