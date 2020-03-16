using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EzhaBy.Business.Tags;

namespace EzhaBy.Api.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IMediator mediator;

        public TagsController(IMediator mediator) => this.mediator = mediator;

        [HttpPost("isAssigned")]
        public async Task<IActionResult> IsAssigned(IsTagAssigned.Query query) =>
            Ok(await mediator.Send(query));


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTag.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTags() => Ok(await mediator.Send(new GetTags.Query()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(string id) => Ok(await mediator.Send(new GetTag.Query(Guid.Parse(id))));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTagName([FromBody] UpdateTagName.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}/icon")]
        public async Task<IActionResult> UpdateTagIcon([FromBody] UpdateTagIcon.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}/icon")]
        public async Task<IActionResult> DeleteTagIcon(DeleteTagIcon.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(DeleteTag.Command command)
        {
            await mediator.Send(command);
            return Ok();
        }
    }
}
