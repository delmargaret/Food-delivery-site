using EzhaBy.Business.Categories.Dto;
using EzhaBy.Entities;
using System;

namespace EzhaBy.Business.Dishes.Dto
{
    public class DishDto
    {
        public Guid Id { get; set; }
        public string DishName { get; set; }
        public string DishIconUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DishStatuses DishStatus { get; set; }
        public virtual CategoryDto CateringFacilityCategory { get; set; }
    }
}
