using EzhaBy.Business.Categories.Dto;
using EzhaBy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzhaBy.Business.Dishes.Dto
{
    public class DishDto
    {
        public Guid Id { get; set; }
        public string DishName { get; set; }
        public byte[] DishIcon { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DishStatuses DishStatus { get; set; }
        public virtual CategoryDto CateringFacilityCategory { get; set; }
    }
}
