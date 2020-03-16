using System.Collections.Generic;
using System.Linq;

namespace EzhaBy.Entities
{
    public class CateringFacility
    {
        public string Id { get; set; }
        public string CateringFacilityName { get; set; }
        public byte[] MainImage { get; set; }
        public byte[] BackgroundImage { get; set; }
        public string DeliveryTime { get; set; }
        public decimal DeliveryPrice { get; set; }
        public CafeTypes CafeType { get; set; }
        public string WorkingHours { get; set; }
        public float Rating { get; set; }
        public string AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual IQueryable<CateringFacilityTag> CateringFacilityTags { get; set; }
        public virtual List<CafeSection> Sections { get; set; }
    }
}
