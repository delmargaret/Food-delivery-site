using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Requests
{
    public static class AddPartner
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                string cateringFacilityName,
                string name,
                string surname,
                string patronymic,
                string phone,
                string email,
                CateringFacilityTypes cateringFacilityType)
            {
                CateringFacilityName = cateringFacilityName;
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
                Phone = phone;
                Email = email;
                CateringFacilityType = cateringFacilityType;
            }

            public string CateringFacilityName { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public CateringFacilityTypes CateringFacilityType { get; set; }
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
                var partnerId = Guid.NewGuid();
                context.PartnerRequests.Add(new PartnerRequest
                {
                    Id = partnerId,
                    CateringFacilityName = request.CateringFacilityName,
                    Name = request.Name,
                    Surname = request.Surname,
                    Patronymic = request.Patronymic,
                    Email = request.Email,
                    Phone = request.Phone,
                    CateringFacilityType = request.CateringFacilityType,
                    IsExists = false,
                    RequestStatus = RequestStatuses.New
                });
                
                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
