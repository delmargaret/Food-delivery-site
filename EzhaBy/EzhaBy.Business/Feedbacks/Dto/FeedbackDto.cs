using EzhaBy.Business.CateringFacilities.Dto;
using EzhaBy.Entities;
using System;

namespace EzhaBy.Business.Feedbacks.Dto
{
    public class FeedbackDto
    {
        public Guid Id { get; set; }
        public FeedbackCategories FeedbackCategory { get; set; }
        public CateringFacilityDto CateringFacility { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public FeedbackStatuses FeedbackStatus { get; set; }
    }
}
