using EzhaBy.Entities;
using System;
using System.Collections.Generic;

namespace EzhaBy.Business.Orders.Dto
{
    public class CafeOrderDto
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
        public bool IsOrderAccepted { get; set; }
        public virtual List<OrderDishDto> OrderDishes { get; set; }
    }
}
