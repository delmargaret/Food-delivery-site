using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Feedbacks
{
    public static class SetFeedbackStatus
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id, FeedbackStatuses status)
            {
                Id = id;
                FeedbackStatus = status;
            }

            public Guid Id { get; set; }

            public FeedbackStatuses FeedbackStatus { get; set; }
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
                var feedback = context.Feedbacks.Find(request.Id);
                if (feedback == null)
                {
                    throw new Exception("feedback isn't exists");
                }

                feedback.FeedbackStatus = request.FeedbackStatus;
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
