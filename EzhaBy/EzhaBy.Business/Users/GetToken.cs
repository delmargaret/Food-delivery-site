using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Users
{
    public static class GetToken
    {
        public class Query : IRequest<(ClaimsIdentity identity, Guid userId, string role)>
        {
            public Query(string email, string password)
            {
                Email = email;
                Password = password;
            }

            public string Email { get; set; }
            public string Password { get; set; }

        }

        public class Handler : IRequestHandler<Query, (ClaimsIdentity identity, Guid userId, string role)>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public Task<(ClaimsIdentity identity, Guid userId, string role)> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = context.Users.FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password);

                if (user == null)
                {
                    throw new Exception("Invalid username or password.");
                }

                return Task.FromResult(
                    (GetIdentity(user), user.Id, user.UserRole.ToString())
                );
            }

            private ClaimsIdentity GetIdentity(User user)
            {
                return new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.UserRole.ToString())
                    }
                );
            }
        }
    }
}
