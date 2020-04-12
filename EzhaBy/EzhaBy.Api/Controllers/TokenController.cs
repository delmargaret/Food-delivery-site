using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using EzhaBy.Business.Users;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EzhaBy.Api.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator mediator;

        public TokenController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> GetTokenAsync([FromBody] GetToken.Query query)
        {
            try
            {
                var identity = await mediator.Send(query);

                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(TokenParams.Lifetime)),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(TokenParams.Key)), SecurityAlgorithms.HmacSha256Signature));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);


                return Ok(encodedJwt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}