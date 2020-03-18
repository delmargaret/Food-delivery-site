using EzhaBy.Business.Tags.Dto;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Tags
{
    public static class GetTag
    {
        public class Query : IRequest<TagDto>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, TagDto>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public Task<TagDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var tag = context.Tags.Find(request.Id);
                if (tag == null)
                {
                    throw new Exception("tag isn't exists");
                }

                return Task.FromResult(new TagDto
                {
                    Id = tag.Id,
                    TagName = tag.TagName,
                    TagIcon = tag.TagIcon
                });
            }
        }
    }
}
