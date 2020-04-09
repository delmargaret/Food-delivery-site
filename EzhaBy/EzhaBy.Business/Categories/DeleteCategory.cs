using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Categories
{
    public static class DeleteCategory
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
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = context.Categories.Find(request.Id);
                if (category == null)
                {
                    throw new Exception("category isn't exists");
                }

                var isAssigned = context.CateringFacilityCategories.First(item 
                    => item.CategoryId == request.Id).Dishes.Any();

                if (isAssigned)
                {
                    throw new Exception("category is assigned and can't be deleted");
                }

                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
