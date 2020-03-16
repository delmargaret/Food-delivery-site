using System;

namespace EzhaBy.Entities
{
    public class CateringFacilityTag
    {
        public Guid CateringFacilityId { get; set; }
        public CateringFacility CateringFacility { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
