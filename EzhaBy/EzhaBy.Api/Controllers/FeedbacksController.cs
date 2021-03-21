using System;
using System.Threading.Tasks;
using EzhaBy.Business.Feedbacks;
using EzhaBy.Entities;
using EzhaBy.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EzhaBy.Api.Controllers
{
    [Route("api/feedbacks")]
    [Authorize(Roles = AuthorizationRoles.Admin)]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IMediator mediator;

        public FeedbacksController(IMediator mediator) => this.mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetFeedbacks()
        {
            try
            {
                return Ok(await mediator.Send(new GetFeedbacks.Query()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> SetFeedbackStatus(string id, [FromBody] SetFeedbackStatus.Command command)
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