namespace EzhaBy.Entities
{
    public class Courier
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public CourierStatuses Status { get; set; }
    }
}
