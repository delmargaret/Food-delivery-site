using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzhaBy.Business.Requests;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetPartnerRequests() =>
            Ok(await mediator.Send(new GetPartnerRequests.Query()));

        [HttpPut("partners/{id}/status")]
        public async Task<IActionResult> SetPartnerRequestStatus(string id, [FromBody] SetPartnerRequestStatus.Command command)
        {
            command.Id = Guid.Parse(id);
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet("couriers")]
        public async Task<IActionResult> GetCourierRequests() =>
            Ok(await mediator.Send(new GetCourierRequests.Query()));

        [HttpPut("couriers/{id}/status")]
        public async Task<IActionResult> SetCourierRequestStatus(string id, [FromBody] SetCourierRequestStatus.Command command)
        {
            command.Id = Guid.Parse(id);
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut("email")]
        public IActionResult SendEmail([FromBody] EmailDto email)
        {
            EmailService.SendMail(email.Email, email.Subject, email.Body);
            return Ok();
        }

    }
}