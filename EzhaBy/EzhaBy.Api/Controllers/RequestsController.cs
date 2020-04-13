using System;
using System.Threading.Tasks;
using EzhaBy.Business.Requests;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EzhaBy.Api.Controllers
{
    [Route("api/requests")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IMediator mediator;

        public RequestsController(IMediator mediator) => this.mediator = mediator;

        [HttpGet("partners")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [HttpGet("couriers")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [HttpPut("email")]
        [Authorize(Roles = "Admin")]
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