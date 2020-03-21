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
        public async Task<IActionResult> UpdateTagName(string id, [FromBody] UpdateTagName.Command command)
        {
            command.Id = Guid.Parse(id);
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}/icon")]
        public async Task<IActionResult> UpdateTagIcon(string id, [FromBody] UpdateTagIcon.Command command)
        {
            command.Id = Guid.Parse(id);
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}/icon")]
        public async Task<IActionResult> DeleteTagIcon(string id)
        {
            await mediator.Send(new DeleteTagIcon.Command(Guid.Parse(id)));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(string id)
        {
            await mediator.Send(new DeleteTag.Command(Guid.Parse(id)));
            return Ok();
        }
    }
}
