using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.CateringFacilities
{
    public static class CreateCateringFacility
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                string name, 
                string deliveryTime,
                decimal deliveryPrice,
                CateringFacilityTypes type,
                string workingHours,
                Towns town,
                string street,
                string houseNumber,
                IList<Guid> tagIds = null)
            {
                CateringFacilityName = name;
                DeliveryTime = deliveryTime;
                DeliveryPrice = deliveryPrice;
                CateringFacilityType = type;
                WorkingHours = workingHours;
                Town = town;
                Street = street;
                HouseNumber = houseNumber;
                CateringFacilityTagIds = tagIds;
            }

            public string CateringFacilityName { get; set; }
            public string DeliveryTime { get; set; }
            public decimal DeliveryPrice { get; set; }
            public CateringFacilityTypes CateringFacilityType { get; set; }
            public string WorkingHours { get; set; }
            public Towns Town { get; set; }
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public IList<Guid> CateringFacilityTagIds { get; set; }
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
                var cateringFacilityId = Guid.NewGuid();

                var cateringFacility = new CateringFacility
                {
                    Id = cateringFacilityId,
                    CateringFacilityName = request.CateringFacilityName,
                    CateringFacilityIconUrl = "",
                    DeliveryTime = request.DeliveryTime,
                    DeliveryPrice = request.DeliveryPrice,
                    CateringFacilityType = request.CateringFacilityType,
                    WorkingHours = request.WorkingHours,
                    Town = request.Town,
                    Street = request.Street,
                    HouseNumber = request.HouseNumber,
                    CateringFacilityStatus = CateringFacilityStatuses.Active
                };

                var tagIds = request.CateringFacilityTagIds;
                if (tagIds != null && tagIds.Count > 0)
                {
                    foreach (var tagId in tagIds)
                    {
                        context.CateringFacilityTags.Add(new CateringFacilityTag
                        {
                            Id = Guid.NewGuid(),
                            CateringFacilityId = cateringFacilityId,
                            TagId = tagId
                        });
                    }
                }
                
                context.CateringFacilities.Add(cateringFacility);
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
