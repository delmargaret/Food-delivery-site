using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Requests
{
    public static class AddFeedback
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                string name,
                string surname,
                string patronymic,
                string email,
                string text,
                FeedbackCategories feedbackCategory,
                string cateringFacilityId)
            {
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
                Email = email;
                Text = text;
                FeedbackCategory = feedbackCategory;
                CateringFacilityId = Guid.Parse(cateringFacilityId);
            }

            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }
            public string Email { get; set; }
            public string Text { get; set; }
            public FeedbackCategories FeedbackCategory { get; set; }
            public Guid CateringFacilityId { get; set; }
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
                var feedbackId = Guid.NewGuid();

                var cateringFacility = await context.CateringFacilities.FindAsync(request.CateringFacilityId);
                if (cateringFacility == null)
                {
                    throw new Exception("catering facility doesn't exist");
                }

                context.Feedbacks.Add(new Feedback
                {
                    Id = feedbackId,
                    Name = request.Name,
                    Surname = request.Surname,
                    Patronymic = request.Patronymic,
                    Email = request.Email,
                    FeedbackCategory = request.FeedbackCategory,
                    CateringFacilityId = cateringFacility.Id,
                    FeedbackStatus = FeedbackStatuses.New,
                    Text = request.Text
                });

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
