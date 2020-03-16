using System;

namespace EzhaBy.Entities
{
    public class CateringFacilityTag
    {
        public Guid Id { get; set; } 
        public Guid CateringFacilityId { get; set; }
        public virtual CateringFacility CateringFacility { get; set; }
        public Guid TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
