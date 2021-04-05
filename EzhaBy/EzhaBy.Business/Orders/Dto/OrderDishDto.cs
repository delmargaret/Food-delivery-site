using System;

namespace EzhaBy.Business.Orders.Dto
{
    public class OrderDishDto
    {
        public Guid Id { get; set; }
        public string DishName { get; set; }
        public string DishIconUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int NumberOfDishes { get; set; }
    }
}
