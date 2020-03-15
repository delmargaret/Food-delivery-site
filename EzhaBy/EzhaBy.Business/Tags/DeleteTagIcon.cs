using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Tags
{
    public static class DeleteTagIcon
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IDataContext context;

            public Handler(IDataContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var tag = context.Tags.Find(request.Id);
                if (tag == null)
                {
                    throw new Exception("tag isn't exists");
                }

                tag.TagIcon = null;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
