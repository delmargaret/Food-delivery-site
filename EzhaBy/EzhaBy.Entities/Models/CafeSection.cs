using System.Collections.Generic;

namespace EzhaBy.Entities
{
    public class CafeSection
    {
        public string Id { get; set; }
        public string CafeId { get; set; }
        public virtual CateringFacility Cafe { get; set; }
        public string SectionId { get; set; }
        public virtual Section Section { get; set; }
        public virtual List<Dish> Dishes { get; set; }
    }
}
