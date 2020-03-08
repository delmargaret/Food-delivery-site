namespace EzhaBy.Entities
{
    public class OrderLine
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string DishId { get; set; }
        public virtual Dish Dish { get; set; }
        public int NumberOfDishes { get; set; }
    }
}
