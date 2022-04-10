using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Users
{
    public static class ResetPassword
    {
        public class Command : IRequest<Unit>
        {
            public Command(string password, string id)
            {
                Password = password;
                Id = Guid.Parse(id);
            }

            public Guid Id { get; set; }
            public string Password { get; set; }
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
                var user = await context.Users.FindAsync(request.Id);
                if (user == null)
                {
                    throw new ArgumentException("user isn't exists");
                }

                user.Password = request.Password;

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
