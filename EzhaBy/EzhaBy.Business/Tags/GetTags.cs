using EzhaBy.Business.Tags.Dto;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Tags
{
    public static class GetTags
    {
        public class Query : IRequest<IQueryable<TagDto>>
        {
        }

        public class Handler : IRequestHandler<Query, IQueryable<TagDto>>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public Task<IQueryable<TagDto>> Handle(Query request, CancellationToken cancellationToken) =>
                Task.FromResult(context.Tags
                    .AsNoTracking()
                    .Select(tag => new TagDto
                    {
                        Id = tag.Id,
                        TagName = tag.TagName,
                        TagIcon = tag.TagIcon
                    }));
        }
    }
}
