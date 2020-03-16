using System.Collections.Generic;

namespace EzhaBy.Entities
{
    public class CafeSection
    {
        public string Id { get; set; }
        public string CafeId { get; set; }
        public CateringFacility Cafe { get; set; }
        public string SectionId { get; set; }
        public Section Section { get; set; }
        public virtual List<Dish> Dishes { get; set; }
    }
}
