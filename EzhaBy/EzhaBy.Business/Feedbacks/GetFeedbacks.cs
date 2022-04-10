using EzhaBy.Business.CateringFacilities.Dto;
using EzhaBy.Business.Feedbacks.Dto;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Feedbacks
{
    public static class GetFeedbacks
    {
        public class Query : IRequest<IQueryable<FeedbackDto>>
        {
        }

        public class Handler : IRequestHandler<Query, IQueryable<FeedbackDto>>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public Task<IQueryable<FeedbackDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var feedbacks = context.Feedbacks.AsNoTracking()
                    .Select(feedback => new FeedbackDto
                    {
                        Id = feedback.Id,
                        FeedbackCategory = feedback.FeedbackCategory,
                        CateringFacility = new CateringFacilityDto
                        {
                            Id = feedback.CateringFacility.Id,
                            CateringFacilityName = feedback.CateringFacility.CateringFacilityName
                        },
                        Name = feedback.Name,
                        Surname = feedback.Surname,
                        Patronymic = feedback.Patronymic,
                        Email = feedback.Email,
                        Text = feedback.Text,
                        FeedbackStatus = feedback.FeedbackStatus
                    });

                if (feedbacks.Count() == 0)
                {
                    throw new Exception("feedbacks not found");
                }

                return Task.FromResult(feedbacks);
            }
        }
    }
}
