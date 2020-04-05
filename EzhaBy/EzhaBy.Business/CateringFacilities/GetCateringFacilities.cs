using EzhaBy.Business.CateringFacilities.Dto;
using EzhaBy.Business.Tags.Dto;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.CateringFacilities
{
    public static class GetCateringFacilities
    {
        public class Query : IRequest<IQueryable<CateringFacilityDto>>
        {
        }

        public class Handler : IRequestHandler<Query, IQueryable<CateringFacilityDto>>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }

            public Task<IQueryable<CateringFacilityDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var cateringFacilities = context.CateringFacilities
                    .AsNoTracking()
                    .Select(cateringFacility => new CateringFacilityDto
                    {
                        Id = cateringFacility.Id,
                        CateringFacilityName = cateringFacility.CateringFacilityName,
                        DeliveryTime = cateringFacility.DeliveryTime,
                        DeliveryPrice = cateringFacility.DeliveryPrice,
                        WorkingHours = cateringFacility.WorkingHours,
                        CateringFacilityIconUrl = cateringFacility.CateringFacilityIconUrl,
                        CateringFacilityStatus = cateringFacility.CateringFacilityStatus,
                        CateringFacilityType = cateringFacility.CateringFacilityType,
                        Town = cateringFacility.Town,
                        Street = cateringFacility.Street,
                        HouseNumber = cateringFacility.HouseNumber,
                        CateringFacilityTags = cateringFacility.CateringFacilityTags.Select(tag => new TagDto
                        {
                            Id = tag.TagId,
                            TagName = tag.Tag.TagName,
                        }).ToList()
                    });

                if (cateringFacilities == null)
                {
                    throw new Exception("tags not found");
                }

                return Task.FromResult(cateringFacilities);
            }
        }
    }
}
