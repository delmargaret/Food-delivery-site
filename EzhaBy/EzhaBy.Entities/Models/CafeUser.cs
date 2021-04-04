using System;

namespace EzhaBy.Entities
{
    public class CafeUser
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid CateringFacilityId { get; set; }
        public virtual CateringFacility CateringFacility { get; set; }
    }
}
