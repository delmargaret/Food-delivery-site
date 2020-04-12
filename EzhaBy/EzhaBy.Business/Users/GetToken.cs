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
        public class Query : IRequest<ClaimsIdentity>
        {
            public Query(string email, string password)
            {
                Email = email;
                Password = password;
            }

            public string Email { get; set; }
            public string Password { get; set; }

        }

        public class Handler : IRequestHandler<Query, ClaimsIdentity>
        {
            private readonly DataContext context;

            public Handler(DataContext context) => this.context = context;

            public Task<ClaimsIdentity> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = context.Users.FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password);

                var identity = GetIdentity(user);
                if (identity == null)
                {
                    throw new Exception("Invalid username or password.");
                }

                return Task.FromResult(identity);
            }

            private ClaimsIdentity GetIdentity(User user)
            {
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim("userId", user.Id.ToString()),
                        new Claim("role", user.UserRole.ToString())
                    };
                    return new ClaimsIdentity(claims);
                }
                return null;
            }
        }
    }
}
