using System;
using System.Collections.Generic;

namespace EzhaBy.Entities
{
    public class CateringFacilityCategory
    {
        public Guid Id { get; set; }
        public Guid CateringFacilityId { get; set; }
        public virtual CateringFacility CateringFacility { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Dish> Dishes { get; set; }
    }
}
