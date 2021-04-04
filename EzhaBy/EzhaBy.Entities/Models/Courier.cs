﻿using System;
using System.Collections.Generic;

namespace EzhaBy.Entities
{
    public class Courier
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public CourierStatuses Status { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
