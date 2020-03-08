﻿using System;
using System.Collections.Generic;

namespace EzhaBy.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string AddressId { get; set; }
        public virtual Address Address { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public string Comment { get; set; }
        public OrderStatuses OrderStatus { get; set; }
        public DateTimeOffset OrderDateTime { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string CourierId { get; set; }
        public virtual Courier Courier { get; set; }
        public List<OrderLine> OrderDishes { get; set; }
    }
}
