using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Orders
{
    public static class AcceptOrder
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
                var courier = context.Couriers.FirstOrDefault(user => user.UserId == request.UserId);
                if (courier == null)
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

                order.IsOrderAccepted = true;
                order.CourierId = courier.Id;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
