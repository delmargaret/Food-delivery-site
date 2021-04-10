using System;
using System.Collections.Generic;

namespace EzhaBy.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public Towns Town { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public string Comment { get; set; }
        public OrderStatuses OrderStatus { get; set; }
        public DateTimeOffset OrderDateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
        public Guid? CourierId { get; set; }
        public virtual Courier Courier { get; set; }
        public bool IsOrderAccepted { get; set; }
        public virtual List<OrderLine> OrderDishes { get; set; }
    }
}
