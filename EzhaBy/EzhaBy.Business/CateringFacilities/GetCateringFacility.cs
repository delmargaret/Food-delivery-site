using EzhaBy.Business.CateringFacilities.Dto;
using EzhaBy.Business.Tags.Dto;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.CateringFacilities
{
    public static class GetCateringFacility
    {
        public class Query : IRequest<CateringFacilityDto>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, CateringFacilityDto>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public Task<CateringFacilityDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var cateringFacility = context.CateringFacilities.Find(request.Id);
                if (cateringFacility == null)
                {
                    throw new Exception("cateringFacility isn't exists");
                }

                return Task.FromResult(new CateringFacilityDto
                {
                    Id = cateringFacility.Id,
                    CateringFacilityName = cateringFacility.CateringFacilityName,
                    DeliveryTime = cateringFacility.DeliveryTime,
                    DeliveryPrice = cateringFacility.DeliveryPrice,
                    WorkingHours = cateringFacility.WorkingHours,
                    CateringFacilityIcon = cateringFacility.CateringFacilityIcon,
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
            }
        }
    }
}
