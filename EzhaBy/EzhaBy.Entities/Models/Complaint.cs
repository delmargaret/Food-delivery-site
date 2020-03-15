namespace EzhaBy.Entities
{
    public class Complaint
    {
        public string Id { get; set; }
        public ComplaintCategories Category { get; set; }
        public string CafeId { get; set; }
        public virtual CateringFacility Cafe { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public ComplaintStatuses ComplaintStatus { get; set; }
    }
}
