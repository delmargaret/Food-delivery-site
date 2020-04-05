using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Feedbacks
{
    public static class GetFeedbacks
    {
        public class Query : IRequest<IQueryable<Feedback>>
        {
        }

        public class Handler : IRequestHandler<Query, IQueryable<Feedback>>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public Task<IQueryable<Feedback>> Handle(Query request, CancellationToken cancellationToken)
            {
                var feedbacks = context.Feedbacks.AsNoTracking();

                if (feedbacks == null)
                {
                    throw new Exception("feedbacks not found");
                }

                return Task.FromResult(feedbacks);
            }
        }
    }
}
