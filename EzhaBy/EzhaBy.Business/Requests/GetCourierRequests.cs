using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Requests
{
    public static class GetCourierRequests
    {
        public class Query : IRequest<IQueryable<CourierRequest>>
        {
        }

        public class Handler : IRequestHandler<Query, IQueryable<CourierRequest>>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public Task<IQueryable<CourierRequest>> Handle(Query request, CancellationToken cancellationToken)
            {
                var requests = context.CourierRequests.AsNoTracking();

                if (requests.Count() == 0)
                {
                    throw new Exception("courier requests not found");
                }

                return Task.FromResult(requests);
            }
        }
    }
}
