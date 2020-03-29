using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Dishes
{
    public static class SetDishStatus
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id, DishStatuses status)
            {
                Id = id;
                DishStatus = status;
            }

            public Guid Id { get; set; }
            public DishStatuses DishStatus { get; set; }
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
                var dish = context.Dishes.Find(request.Id);
                if (dish == null)
                {
                    throw new Exception("dish isn't exists");
                }

                dish.DishStatus = request.DishStatus;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
