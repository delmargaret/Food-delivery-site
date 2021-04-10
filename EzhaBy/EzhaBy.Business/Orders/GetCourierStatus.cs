using EzhaBy.Business.Orders.Dto;
using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Orders
{
    public static class GetCourierStatus
    {
        public class Query : IRequest<CourierStatuses>
        {
            public Query(Guid userId)
            {
                UserId = userId;
            }

            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, CourierStatuses>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public Task<CourierStatuses> Handle(Query request, CancellationToken cancellationToken)
            {
                var courier = context.Couriers.FirstOrDefault(user => user.UserId == request.UserId);
                if (courier == null)
                {
                    throw new Exception("unknown courier");
                }

                var status = courier.Status;

                return Task.FromResult(status);
            }
        }
    }
}
