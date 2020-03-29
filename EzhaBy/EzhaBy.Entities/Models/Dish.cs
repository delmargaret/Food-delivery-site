using System;

namespace EzhaBy.Entities
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string DishName { get; set; }
        public byte[] DishIcon { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DishStatuses DishStatus { get; set; }
        public Guid CateringFacilityCategoryId { get; set; }
        public virtual CateringFacilityCategory CateringFacilityCategory { get; set; }
    }
}
