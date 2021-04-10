﻿using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Orders
{
    public static class SetOrderCourier
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = context.Orders.Find(request.Id);
                if (order == null)
                {
                    throw new Exception("order isn't exists");
                }
                if (order.IsOrderAccepted)
                {
                    throw new Exception("order is already accepted");
                }

                var newCourier = context.Couriers.FirstOrDefault(courier
                    => courier.Status == CourierStatuses.Active &&
                    courier.Id != order.CourierId);

                order.CourierId = newCourier?.Id;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
