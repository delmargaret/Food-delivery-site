using EzhaBy.Business.Categories.Dto;
using EzhaBy.Business.Dishes.Dto;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Dishes
{
    public static class GetDishes
    {
        public class Query : IRequest<IQueryable<DishDto>>
        {
            public Query(Guid cateringFacilityId)
            {
                CateringFacilityId = cateringFacilityId;
            }

            public Guid CateringFacilityId { get; set; }
        }

        public class Handler : IRequestHandler<Query, IQueryable<DishDto>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public Task<IQueryable<DishDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var dishes = context.Dishes.Where(dish 
                    => dish.CateringFacilityCategory.CateringFacilityId == request.CateringFacilityId)
                    .AsNoTracking()
                    .Select(dish => new DishDto
                    {
                        Id = dish.Id,
                        DishName = dish.DishName,
                        DishIconUrl = dish.DishIconUrl,
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

                if (dishes == null)
                {
                    throw new Exception("dishes not found");
                }

                return Task.FromResult(dishes);
            }
        }
    }
}
