namespace EzhaBy.Entities
{
    public class CourierRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public VehicleTypes VehicleType { get; set; }
        public float FuelConsumption { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
