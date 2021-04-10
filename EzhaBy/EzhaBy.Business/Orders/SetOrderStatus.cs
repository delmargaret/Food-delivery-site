using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Orders
{
    public static class SetOrderStatus
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id, OrderStatuses status)
            {
                Id = id;
                OrderStatus = status;
            }

            public Guid Id { get; set; }

            public OrderStatuses OrderStatus { get; set; }
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

                order.OrderStatus = request.OrderStatus;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
