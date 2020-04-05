using System;
using System.Collections.Generic;

namespace EzhaBy.Entities
{
    public class CateringFacility
    {
        public Guid Id { get; set; }
        public string CateringFacilityName { get; set; }
        public string CateringFacilityIconUrl { get; set; }
        public string DeliveryTime { get; set; }
        public decimal DeliveryPrice { get; set; }
        public CateringFacilityTypes CateringFacilityType { get; set; }
        public string WorkingHours { get; set; }
        public Towns Town { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public CateringFacilityStatuses CateringFacilityStatus { get; set; }
        public virtual IList<CateringFacilityTag> CateringFacilityTags { get; set; }
        public virtual IList<CateringFacilityCategory> CateringFacilityCategories { get; set; }
    }
}
