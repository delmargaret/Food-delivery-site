using EzhaBy.Business.Helpers;
using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Requests
{
    public static class AddPartnerAccount
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                Guid id,
                string cateringFacilityId)
            {
                Id = id;
                CateringFacilityId = Guid.Parse(cateringFacilityId);
            }

            public Guid Id { get; set; }
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
                var cateringFacility = await context.CateringFacilities.FindAsync(request.CateringFacilityId);
                if (cateringFacility == null)
                {
                    throw new Exception("catering facility isn't exists");
                }

                var partner = await context.PartnerRequests.FindAsync(request.Id);
                if (partner == null)
                {
                    throw new Exception("partner isn't exists");
                }

                var userId = Guid.NewGuid();
                var password = PasswordHelper.RandomPassword(10);
                context.Users.Add(new User
                {
                    Id = userId,
                    Email = partner.Email,
                    Name = partner.Name,
                    Patronymic = partner.Patronymic,
                    Surname = partner.Surname,
                    Password = password,
                    Phone = partner.Phone,
                    UserRole = UserRoles.CafeAdmin
                });

                context.CafeUsers.Add(new CafeUser
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    CateringFacilityId = cateringFacility.Id
                });
                EmailService.SendMail(
                    partner.Email,
                    "Аккаунт ezha.by создан",
$@"Для вас был соднан аккаунт в сервисе ezha.by. 
Для того, чтобы зайти в приложение, перейдите на страницу https://ezha-by-order-management.herokuapp.com/.
Для входа используйте логин <b>{partner.Email}</b> и пароль <b>{password}</b>");

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
