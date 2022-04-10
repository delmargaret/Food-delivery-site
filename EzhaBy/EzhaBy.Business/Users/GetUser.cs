using EzhaBy.Business.Users.Dto;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Users
{
    public static class GetUser
    {
        public class Query : IRequest<UserDto>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, UserDto>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = context.Users.Find(request.Id);
                if (user == null)
                {
                    throw new Exception("user isn't exists");
                }

                return Task.FromResult(new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Patronymic = user.Patronymic,
                    Phone = user.Phone,
                    Town = user.Town,
                    Street = user.Street,
                    HouseNumber = user.HouseNumber,
                    FlatNumber = user.FlatNumber
                });
            }
        }
    }
}
