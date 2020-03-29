using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Dishes
{
    public static class UpdateDish
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                Guid id,
                string name,
                string description,
                decimal price,
                string cateringFacilityCategoryId)
            {
                Id = id;
                DishName = name;
                Description = description;
                Price = price;
                CateringFacilityCategoryId = Guid.Parse(cateringFacilityCategoryId);
            }

            public Guid Id { get; set; }
            public string DishName { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public Guid CateringFacilityCategoryId { get; set; }
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

                dish.DishName = request.DishName;
                dish.Description = request.Description;
                dish.Price = request.Price;
                dish.CateringFacilityCategoryId = request.CateringFacilityCategoryId;

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
