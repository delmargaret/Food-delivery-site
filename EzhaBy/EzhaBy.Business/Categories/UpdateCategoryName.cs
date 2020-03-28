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
    public static class UpdateCategoryName
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id, string categoryName)
            {
                Id = id;
                CategoryName = categoryName;
            }

            public Guid Id { get; set; }

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
                var category = context.Categories.Find(request.Id);
                if (category == null)
                {
                    throw new Exception("category isn't exists");
                }

                category.CategoryName = request.CategoryName;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
