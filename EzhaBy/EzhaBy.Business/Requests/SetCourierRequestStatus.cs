using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Requests
{
    public static class SetCourierRequestStatus
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id, RequestStatuses status)
            {
                Id = id;
                RequestStatus = status;
            }

            public Guid Id { get; set; }

            public RequestStatuses RequestStatus { get; set; }
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
                var courierRequest = context.CourierRequests.Find(request.Id);
                if (courierRequest == null)
                {
                    throw new Exception("request isn't exists");
                }

                courierRequest.RequestStatus = request.RequestStatus;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
