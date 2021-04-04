using System;

namespace EzhaBy.Entities
{
    public class OrderLine
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public Guid DishId { get; set; }
        public virtual Dish Dish { get; set; }
        public int NumberOfDishes { get; set; }
    }
}
