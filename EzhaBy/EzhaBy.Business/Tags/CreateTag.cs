using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using FluentValidation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Tags
{
    public static class CreateTag
    {
        public class Command : IRequest<Unit>
        {
            public Command(string tagName, byte[] tagIcon = null)
            {
                TagName = tagName;
                TagIcon = tagIcon;
            }

            public string TagName { get; set; }
            public byte[] TagIcon { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            //private readonly IValidator<string> tagValidator;
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                //this.tagValidator = tagValidator;
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var existingTags = context.Tags.Where(item => item.TagName == request.TagName).ToList();
                if (existingTags.Count != 0)
                {
                    throw new Exception("tag already exists");
                }

                var tag = new Tag
                {
                    Id = Guid.NewGuid(),
                    TagName = request.TagName,
                    TagIcon = request.TagIcon
                };

                context.Tags.Add(tag);
                await context.SaveChangesAsync();
                return Unit.Value;
            }

            //public async Task<Unit> Handle(Command request, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
            //{
            //    await tagValidator.ValidateAndThrowAsync(request.TagName, cancellationToken: cancellationToken);
            //    return await next();
            //}
        }
    }
}
