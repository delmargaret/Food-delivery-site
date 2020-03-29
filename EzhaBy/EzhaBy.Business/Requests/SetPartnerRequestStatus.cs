using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Requests
{
    public static class SetPartnerRequestStatus
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
                var partnerRequest = context.PartnerRequests.Find(request.Id);
                if (partnerRequest == null)
                {
                    throw new Exception("request isn't exists");
                }

                partnerRequest.RequestStatus = request.RequestStatus;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
