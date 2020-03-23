using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Categories
{
    public static class CreateCategory
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id, string name)
            {
                CateringFacilityId = id;
                CategoryName = name;
            }

            public Guid CateringFacilityId { get; set; }
            public string CategoryName { get; set; }
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
                var existingCategories = context.CateringFacilities
                    .Find(request.CateringFacilityId).CateringFacilityCategories
                    .Where(item => item.Category.CategoryName == request.CategoryName).ToList();
                if (existingCategories.Count != 0)
                {
                    throw new Exception("category already exists");
                }

                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = request.CategoryName,
                };

                context.Categories.Add(category);
                context.CateringFacilityCategories.Add(new CateringFacilityCategory
                {
                    Id = Guid.NewGuid(),
                    CategoryId = category.Id,
                    CateringFacilityId = request.CateringFacilityId
                });
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
