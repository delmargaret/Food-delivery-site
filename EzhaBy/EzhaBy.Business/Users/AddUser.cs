using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Users
{
    public static class AddUser
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                string name,
                string surname,
                string patronymic,
                string phone,
                string email,
                string password)
            {
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
                Phone = phone;
                Email = email;
                Password = password;
            }

            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
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
                var userExists = context.Users.Any(x => x.Email == request.Email);
                if (userExists)
                {
                    throw new ArgumentException("user already exists");
                }

                var userId = Guid.NewGuid();
                context.Users.Add(new User
                {
                    Id = userId,
                    Email = request.Email,
                    Name = request.Name,
                    Patronymic = request.Patronymic,
                    Surname = request.Surname,
                    Password = request.Password,
                    Phone = request.Phone,
                    UserRole = UserRoles.User,
                });

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
