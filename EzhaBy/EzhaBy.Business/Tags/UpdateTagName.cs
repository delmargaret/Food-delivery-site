using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Tags
{
    public static class UpdateTagName
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id, string tagName)
            {
                Id = id;
                TagName = tagName;
            }

            public Guid Id { get; set; }

            public string TagName { get; set; }
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
                var tag = context.Tags.Find(request.Id);
                if (tag == null)
                {
                    throw new Exception("tag isn't exists");
                }

                tag.TagName = request.TagName;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
