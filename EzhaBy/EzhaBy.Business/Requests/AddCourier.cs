using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Requests
{
    public static class AddCourier
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                string name,
                string surname,
                string patronymic,
                string phone,
                string email,
                float fuelConsumption,
                VehicleTypes vehicleType)
            {
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
                Phone = phone;
                Email = email;
                VehicleType = vehicleType;
                FuelConsumption = fuelConsumption;
            }

            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public VehicleTypes VehicleType { get; set; }
            public float FuelConsumption { get; set; }
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
                var courierId = Guid.NewGuid();
                context.CourierRequests.Add(new CourierRequest
                {
                    Id = courierId,
                    Name = request.Name,
                    Surname = request.Surname,
                    Patronymic = request.Patronymic,
                    Email = request.Email,
                    Phone = request.Phone,
                    VehicleType = request.VehicleType,
                    IsExists = false,
                    RequestStatus = RequestStatuses.New,
                    FuelConsumption = request.FuelConsumption,
                });

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
