using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Orders
{
    public static class RejectOrder
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid orderId, Guid userId)
            {
                OrderId = orderId;
                UserId = userId;
            }

            public Guid OrderId { get; set; }

            public Guid UserId { get; set; }
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
                var oldCourier = context.Couriers.FirstOrDefault(user => user.UserId == request.UserId);
                if (oldCourier == null)
                {
                    throw new Exception("unknown courier");
                }

                var order = context.Orders.Find(request.OrderId);
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
                    courier.Id != oldCourier.Id);

                order.IsOrderAccepted = false;
                order.CourierId = newCourier?.Id;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
