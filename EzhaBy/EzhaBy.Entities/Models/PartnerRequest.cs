using System;

namespace EzhaBy.Entities
{
    public class PartnerRequest
    {
        public Guid Id { get; set; }
        public string CateringFacilityName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public CateringFacilityTypes CateringFacilityType { get; set; }
        public RequestStatuses RequestStatus { get; set; } 
    }
}
