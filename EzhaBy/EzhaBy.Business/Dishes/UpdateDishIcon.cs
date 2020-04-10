using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Dishes
{
    public static class UpdateDishIcon
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                Guid id,
                string url)
            {
                Id = id;
                Url = url;
            }

            public Guid Id { get; set; }
            public string Url { get; set; }
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
                var dish = await context.Dishes.FindAsync(request.Id);
                if (dish == null)
                {
                    throw new Exception("dish isn't exists");
                }

                dish.DishIconUrl = request.Url;

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
