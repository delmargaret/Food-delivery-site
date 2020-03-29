using EzhaBy.Business.Categories.Dto;
using EzhaBy.Business.Dishes.Dto;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Dishes
{
    public static class GetDish
    {
        public class Query : IRequest<DishDto>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, DishDto>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public Task<DishDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var dish = context.Dishes.Find(request.Id);
                if (dish == null)
                {
                    throw new Exception("dish isn't exists");
                }

                return Task.FromResult(new DishDto
                {
                    Id = dish.Id,
                    DishName = dish.DishName,
                    DishIcon = dish.DishIcon,
                    Description = dish.Description,
                    Price = dish.Price,
                    DishStatus = dish.DishStatus,
                    CateringFacilityCategory = new CategoryDto
                    {
                        Id = dish.CateringFacilityCategory.Id,
                        CategoryId = dish.CateringFacilityCategory.CategoryId,
                        CategoryName = dish.CateringFacilityCategory.Category.CategoryName
                    }
                });
            }
        }
    }
}
