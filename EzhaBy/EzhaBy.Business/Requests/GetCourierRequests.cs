using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Requests
{
    public static class GetCourierRequests
    {
        public class Query : IRequest<CourierRequest[]>
        {
        }

        public class Handler : IRequestHandler<Query, CourierRequest[]>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public async Task<CourierRequest[]> Handle(Query request, CancellationToken cancellationToken)
            {
                var requests = await context.CourierRequests.AsNoTracking().ToListAsync();

                if (requests.Count() == 0)
                {
                    throw new Exception("courier requests not found");
                }

                foreach (var req in requests)
                {
                    req.IsExists = context.Couriers.Any(user => user.User.Email == req.Email);
                }

                return requests.ToArray();
            }
        }
    }
}
