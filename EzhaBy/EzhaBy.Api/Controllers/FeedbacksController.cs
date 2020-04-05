using System;
using System.Threading.Tasks;
using EzhaBy.Business.Feedbacks;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EzhaBy.Api.Controllers
{
    [Route("api/feedbacks")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IMediator mediator;

        public FeedbacksController(IMediator mediator) => this.mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetFeedbacks() =>
            Ok(await mediator.Send(new GetFeedbacks.Query()));

        [HttpPut("{id}/status")]
        public async Task<IActionResult> SetFeedbackStatus(string id, [FromBody] SetFeedbackStatus.Command command)
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