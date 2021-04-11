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
    public static class GetPartnerRequests
    {
        public class Query : IRequest<IQueryable<PartnerRequest>>
        {
        }

        public class Handler : IRequestHandler<Query, IQueryable<PartnerRequest>>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public Task<IQueryable<PartnerRequest>> Handle(Query request, CancellationToken cancellationToken)
            {
                var requests = context.PartnerRequests.AsNoTracking();

                if (requests.Count() == 0)
                {
                    throw new Exception("parner requests not found");
                }

                requests.ToList().ForEach(req =>
                {
                    req.IsExists = context.CafeUsers.Any(user => user.User.Email == req.Email);
                });

                return Task.FromResult(requests);
            }
        }
    }
}
