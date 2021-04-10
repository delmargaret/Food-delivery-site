using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Orders
{
    public static class SetCourierStatus
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id, CourierStatuses status)
            {
                Id = id;
                CourierStatus = status;
            }

            public Guid Id { get; set; }

            public CourierStatuses CourierStatus { get; set; }
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
                var courier = context.Couriers.FirstOrDefault(user => user.UserId == request.Id);
                if (courier == null)
                {
                    throw new Exception("unknown courier");
                }

                courier.Status = request.CourierStatus;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
