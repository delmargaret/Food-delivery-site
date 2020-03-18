using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Tags
{
    public static class IsTagAssigned
    {
        public class Query : IRequest<bool>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, bool>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                var tag = context.Tags.Find(request.Id);
                if (tag == null)
                {
                    throw new Exception("tag isn't exists");
                }

                return Task.FromResult(tag.CateringFacilityTags.Any());
            }
        }
    }
}
