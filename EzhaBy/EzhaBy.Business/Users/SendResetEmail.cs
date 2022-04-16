using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Users
{
    public static class SendResetEmail
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                string email)
            {
                Email = email;
            }

            public string Email { get; set; }
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
                var user = context.Users.FirstOrDefault(u => u.Email == request.Email);
                if (user == null)
                {
                    throw new ArgumentException("user isn't exists");
                }

                var url = "https://ezha-by.herokuapp.com//new-password?u=" + user.Id;

                EmailService.SendMail(
                    request.Email,
                    "Восстановление пароля ezha.by",
$@"Для того, чтобы восстановить пароль в приложении, перейдите на страницу {url}.
Для входа используйте логин <b>{request.Email}</b>.");

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
