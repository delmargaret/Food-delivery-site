using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.CateringFacilities
{
    public static class UpdateCateringFacilityIcon
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
                var cateringFacility = await context.CateringFacilities.FindAsync(request.Id);
                if (cateringFacility == null)
                {
                    throw new Exception("catering facility isn't exists");
                }

                cateringFacility.CateringFacilityIconUrl = request.Url;

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
