using EzhaBy.Business.Helpers;
using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Requests
{
    public static class AddCourierAccount
    {
        public class Command : IRequest<Unit>
        {
            public Command(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
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
                var courier = await context.CourierRequests.FindAsync(request.Id);
                if (courier == null)
                {
                    throw new Exception("courier isn't exists");
                }

                var userId = Guid.NewGuid();
                var password = PasswordHelper.RandomPassword(10);
                context.Users.Add(new User
                {
                    Id = userId,
                    Email = courier.Email,
                    Name = courier.Name,
                    Patronymic = courier.Patronymic,
                    Surname = courier.Surname,
                    Password = password,
                    Phone = courier.Phone,
                    UserRole = UserRoles.Courier
                });

                context.Couriers.Add(new Courier
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Status = CourierStatuses.Away
                });
                EmailService.SendMail(
                    courier.Email,
                    "Аккаунт ezha.by создан",
$@"Для вас был соднан аккаунт в сервисе ezha.by. 
Для того, чтобы зайти в приложение, перейдите на страницу http://localhost:4200/.
Для входа используйте логин <b>{courier.Email}</b> и пароль <b>{password}</b>");

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
