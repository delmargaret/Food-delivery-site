using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Users
{
    public class UpdateUser
    {
        public class Command : IRequest<Unit>
        {
            public Command(
                Guid id,
                string name,
                string surname,
                string patronymic,
                string phone,
                string password,
                Towns town,
                string street,
                string houseNumber,
                string flatNumber)
            {
                Id = id;
                Name = name;
                Surname = surname;
                Patronymic = patronymic;
                Phone = phone;
                Password = password;
                Town = town;
                Street = street;
                HouseNumber = houseNumber;
                FlatNumber = flatNumber;
            }

            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Patronymic { get; set; }
            public string Phone { get; set; }
            public string Password { get; set; }
            public Towns Town { get; set; }
            public string Street { get; set; }
            public string HouseNumber { get; set; }
            public string FlatNumber { get; set; }
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
                    throw new Exception("user isn't exists");
                }

                user.Name = request.Name;
                user.Surname = request.Surname;
                user.Patronymic = request.Patronymic;
                user.Phone = request.Phone;
                user.Town = request.Town;
                user.Street = request.Street;
                user.HouseNumber = request.HouseNumber;
                user.FlatNumber = request.FlatNumber;

                if (!string.IsNullOrWhiteSpace(request.Password))
                {
                    user.Password = request.Password;
                }

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
