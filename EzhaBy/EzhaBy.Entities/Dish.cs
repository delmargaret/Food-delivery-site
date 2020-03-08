namespace EzhaBy.Entities
{
    public class Dish
    {
        public string Id { get; set; }
        public string DishName { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CafeSectionId { get; set; }
        public virtual CafeSection CafeSection { get; set; }
    }
}
