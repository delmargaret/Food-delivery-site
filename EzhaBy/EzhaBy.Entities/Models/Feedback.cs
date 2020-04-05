using System;

namespace EzhaBy.Entities
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public FeedbackCategories FeedbackCategory { get; set; }
        public Guid CateringFacilityId { get; set; }
        public virtual CateringFacility CateringFacility { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public FeedbackStatuses FeedbackStatus { get; set; }
    }
}
