using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.CateringFacilities
{
    public static class UpdateCateringFacility
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                Guid id, 
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
                Id = id;
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

            public Guid Id { get; set; }
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
                var cateringFacility = await context.CateringFacilities.FindAsync(request.Id);
                if (cateringFacility == null)
                {
                    throw new Exception("catering facility isn't exists");
                }

                cateringFacility.CateringFacilityName = request.CateringFacilityName;
                cateringFacility.DeliveryTime = request.DeliveryTime;
                cateringFacility.DeliveryPrice = request.DeliveryPrice;
                cateringFacility.CateringFacilityType = request.CateringFacilityType;
                cateringFacility.WorkingHours = request.WorkingHours;
                cateringFacility.Town = request.Town;
                cateringFacility.Street = request.Street;
                cateringFacility.HouseNumber = request.HouseNumber;

                context.CateringFacilityTags.RemoveRange(context.CateringFacilityTags.Where(x => x.CateringFacilityId == cateringFacility.Id));

                var tagIds = request.CateringFacilityTagIds;
                if (tagIds != null && tagIds.Count > 0)
                {
                    foreach (var tagId in tagIds)
                    {
                        await context.CateringFacilityTags.AddAsync(new CateringFacilityTag
                        {
                            Id = Guid.NewGuid(),
                            CateringFacilityId = request.Id,
                            TagId = tagId
                        }, cancellationToken);
                    }
                }
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
