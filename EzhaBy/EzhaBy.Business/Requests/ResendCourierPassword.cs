using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Requests
{
    public static class ResendCourierPassword
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
                var courierRequest = await context.CourierRequests.FindAsync(request.Id);
                if (courierRequest == null)
                {
                    throw new Exception("courier request isn't exists");
                }

                var user = await context.Couriers.FirstOrDefaultAsync(courier => courier.User.Email == courierRequest.Email);
                if (user == null)
                {
                    throw new Exception("courier isn't exists");
                }

                EmailService.SendMail(
                    user.User.Email,
                    "Учетные данные ezha.by",
$@"Для того, чтобы зайти в приложение, перейдите на страницу https://ezha-by-order-management.herokuapp.com/.
Для входа используйте логин <b>{user.User.Email}</b> и пароль <b>{user.User.Password}</b>");

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
