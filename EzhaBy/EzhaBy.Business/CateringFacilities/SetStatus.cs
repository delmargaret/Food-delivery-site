using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.CateringFacilities
{
    public static class SetStatus
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id, CateringFacilityStatuses status)
            {
                Id = id;
                CateringFacilityStatus = status;
            }

            public Guid Id { get; set; }

            public CateringFacilityStatuses CateringFacilityStatus { get; set; }
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
                var cateringFacility = context.CateringFacilities.Find(request.Id);
                if (cateringFacility == null)
                {
                    throw new Exception("cateringFacility isn't exists");
                }

                cateringFacility.CateringFacilityStatus = request.CateringFacilityStatus;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
