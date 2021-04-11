﻿using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EzhaBy.Business.Requests
{
    public static class ResendPartnerPassword
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
                var partnerRequest = await context.PartnerRequests.FindAsync(request.Id);
                if (partnerRequest == null)
                {
                    throw new Exception("partner request isn't exists");
                }

                var user = await context.CafeUsers.FirstOrDefaultAsync(partner => partner.User.Email == partnerRequest.Email);
                if (user == null)
                {
                    throw new Exception("courier isn't exists");
                }

                EmailService.SendMail(
                    user.User.Email,
                    "Учетные данные ezha.by",
$@"Для того, чтобы зайти в приложение, перейдите на страницу http://localhost:4200/.
Для входа используйте логин <b>{user.User.Email}</b> и пароль <b>{user.User.Password}</b>");

                await context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
