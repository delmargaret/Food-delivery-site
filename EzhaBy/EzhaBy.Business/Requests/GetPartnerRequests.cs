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
    public static class GetPartnerRequests
    {
        public class Query : IRequest<PartnerRequest[]>
        {
        }

        public class Handler : IRequestHandler<Query, PartnerRequest[]>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public async Task<PartnerRequest[]> Handle(Query request, CancellationToken cancellationToken)
            {
                var requests = await context.PartnerRequests.AsNoTracking().ToListAsync();

                if (requests.Count() == 0)
                {
                    throw new Exception("parner requests not found");
                }

                foreach(var req in requests)
                {
                    req.IsExists = context.CafeUsers.Any(user => user.User.Email == req.Email);
                }

                return requests.ToArray();
            }
        }
    }
}
