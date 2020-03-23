

using EzhaBy.Business.Categories.Dto;
using EzhaBy.Business.CateringFacilities.Dto;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Categories
{
    public static class GetCategories
    {
        public class Query : IRequest<IQueryable<CategoryDto>>
        {
            public Query(Guid id)
            {
                CateringFacilityId = id;
            }

            public Guid CateringFacilityId { get; set; }
        }

        public class Handler : IRequestHandler<Query, IQueryable<CategoryDto>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public Task<IQueryable<CategoryDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var categories = context.CateringFacilities.Find(request.CateringFacilityId).CateringFacilityCategories
                    .Select(category => new CategoryDto
                    {
                        Id = category.CategoryId,
                        CategoryName = category.Category.CategoryName,
                        isAssigned = category.Dishes.Any()
                    });

                if (categories == null)
                {
                    throw new Exception("categories not found");
                }

                return Task.FromResult(categories.AsQueryable());
            }
        }
    }
}
