using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Tags
{
    public static class UpdateTagIcon
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id, byte[] tagIcon)
            {
                Id = id;
                TagIcon = tagIcon;
            }

            public Guid Id { get; set; }

            public byte[] TagIcon { get; set; }
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

                tag.TagIcon = request.TagIcon;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
