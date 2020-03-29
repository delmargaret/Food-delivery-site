using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Dishes
{
    public static class CreateDish
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                string name,
                string description,
                decimal price,
                string cateringFacilityCategoryId)
            {
                DishName = name;
                Description = description;
                Price = price;
                CateringFacilityCategoryId = Guid.Parse(cateringFacilityCategoryId);
            }

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
                var dishId = Guid.NewGuid();

                var dish = new Dish
                {
                    Id = dishId,
                    DishName = request.DishName,
                    DishIcon = null,
                    Description = request.Description,
                    Price = request.Price,
                    DishStatus = DishStatuses.InStock,
                    CateringFacilityCategoryId = request.CateringFacilityCategoryId
                };

                context.Dishes.Add(dish);
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
