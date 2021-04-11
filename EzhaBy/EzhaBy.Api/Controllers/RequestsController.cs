using System;
using System.Threading.Tasks;
using EzhaBy.Business.Requests;
using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EzhaBy.Api.Controllers
{
    [Route("api/requests")]
    [Authorize(Roles = AuthorizationRoles.Admin)]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IMediator mediator;

        public RequestsController(IMediator mediator) => this.mediator = mediator;

        [HttpGet("partners")]
        public async Task<IActionResult> GetPartnerRequests()
        {
            try
            {
                return Ok(await mediator.Send(new GetPartnerRequests.Query()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("partners/{id}/status")]
        public async Task<IActionResult> SetPartnerRequestStatus(string id, [FromBody] SetPartnerRequestStatus.Command command)
        {
            try
            {
                command.Id = Guid.Parse(id);
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("partners/{id}/account")]
        public async Task<IActionResult> AddPartnerAccount(string id, [FromBody] AddPartnerAccount.Command command)
        {
            try
            {
                command.Id = Guid.Parse(id);
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("partners/{id}/resend-password")]
        public async Task<IActionResult> ResendPartnerPassword(string id, [FromBody] ResendPartnerPassword.Command command)
        {
            try
            {
                command.Id = Guid.Parse(id);
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("couriers")]
        public async Task<IActionResult> GetCourierRequests()
        {
            try
            {
                return Ok(await mediator.Send(new GetCourierRequests.Query()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("couriers/{id}/status")]
        public async Task<IActionResult> SetCourierRequestStatus(string id, [FromBody] SetCourierRequestStatus.Command command)
        {
            try
            {
                command.Id = Guid.Parse(id);
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("couriers/{id}/account")]
        public async Task<IActionResult> AddCourierAccount(string id, [FromBody] AddCourierAccount.Command command)
        {
            try
            {
                command.Id = Guid.Parse(id);
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("couriers/{id}/resend-password")]
        public async Task<IActionResult> ResendCourierPassword(string id, [FromBody] ResendCourierPassword.Command command)
        {
            try
            {
                command.Id = Guid.Parse(id);
                await mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("email")]
        public IActionResult SendEmail([FromBody] EmailDto email)
        {
            try
            {
                EmailService.SendMail(email.Email, email.Subject, email.Body);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}