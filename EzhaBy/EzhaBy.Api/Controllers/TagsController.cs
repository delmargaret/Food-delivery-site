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
            try
            {
                await mediator.Send(command);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            try
            {
                return Ok(await mediator.Send(new GetTags.Query()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(string id)
        {
            try
            {
                return Ok(await mediator.Send(new GetTag.Query(Guid.Parse(id))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTagName(string id, [FromBody] UpdateTagName.Command command)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(string id)
        {
            try
            {
                await mediator.Send(new DeleteTag.Command(Guid.Parse(id)));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
